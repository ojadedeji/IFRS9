using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IPDMigrationMatrixFinalRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PDMigrationMatrixFinalRepository : DataRepositoryBase<PDMigrationMatrixFinal>, IPDMigrationMatrixFinalRepository
    {
        protected override PDMigrationMatrixFinal AddEntity(IFRSContext entityContext, PDMigrationMatrixFinal entity)
        {
            return entityContext.Set<PDMigrationMatrixFinal>().Add(entity);
        }

        protected override PDMigrationMatrixFinal UpdateEntity(IFRSContext entityContext, PDMigrationMatrixFinal entity)
        {
            return (from e in entityContext.Set<PDMigrationMatrixFinal>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<PDMigrationMatrixFinal> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<PDMigrationMatrixFinal>()
                   select e;
        }

        protected override PDMigrationMatrixFinal GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PDMigrationMatrixFinal>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<PDMigrationMatrixFinal> GetAvailablePDMigrationMatrixFinal(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<PDMigrationMatrixFinal>()
                                 select new
                                 {
                                     e.ProductType,
                                     e.CurrentDate,
                                     e.Rating,
                                     e.RG1,
                                     e.RG2,
                                     e.RGD
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<PDMigrationMatrixFinal>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<PDMigrationMatrixFinal>().Take(defaultCount)
                                 select e);

                    return query.ToArray();
                }
            }
        }


        public IEnumerable<PDMigrationMatrixFinal> GetPDMigrationMatrixFinalBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<PDMigrationMatrixFinal>()
                                 where searchParam.Contains(e.ProductType)
                                 select new
                                 {
                                     e.ProductType,
                                     e.CurrentDate,
                                     e.Rating,
                                     e.RG1,
                                     e.RG2,
                                     e.RGD
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var products = (from e in query select new { e.ProductType }).Distinct();
                        var count = products.Count();
                        var ExportHandler = new ExcelService(path);
                        var producttype = count > 0 ? products.ToList().ElementAt(0).ProductType : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            producttype = products.ToList().ElementAt(i).ProductType;
                            response = ExportHandler.Export(query.Where(e => e.ProductType == producttype).ToList(), path + producttype.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<PDMigrationMatrixFinal>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<PDMigrationMatrixFinal>()
                                 where e.ProductType == searchParam
                                 select e);
                    return query.ToArray();
                }
            }
        }


        public IEnumerable<PDMigrationMatrixFinal> GetPDMigrationMatrixFinalByRating(string ratingVal)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<PDMigrationMatrixFinal>()
                             where e.Rating == ratingVal
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<string> GetDistinctRatings()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (entityContext.PDMigrationMatrixFinalSet.Select(r => r.Rating)).Distinct();

                return query.ToFullyLoaded();
            }
        }
    }
}