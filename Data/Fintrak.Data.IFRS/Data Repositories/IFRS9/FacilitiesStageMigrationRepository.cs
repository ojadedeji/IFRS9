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
    [Export(typeof(IFacilitiesStageMigrationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FacilitiesStageMigrationRepository : DataRepositoryBase<FacilitiesStageMigration>, IFacilitiesStageMigrationRepository
    {
        protected override FacilitiesStageMigration AddEntity(IFRSContext entityContext, FacilitiesStageMigration entity)
        {
            return entityContext.Set<FacilitiesStageMigration>().Add(entity);
        }

        protected override FacilitiesStageMigration UpdateEntity(IFRSContext entityContext, FacilitiesStageMigration entity)
        {
            return (from e in entityContext.Set<FacilitiesStageMigration>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FacilitiesStageMigration> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<FacilitiesStageMigration>()
                   select e;
        }

        protected override FacilitiesStageMigration GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FacilitiesStageMigration>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<FacilitiesStageMigration> GetAvailableFacilitiesStageMigration(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<FacilitiesStageMigration>()
                                 select new
                                 {
                                     e.Refno,
                                     e.Product,
                                     e.PassDueDays,
                                     e.StageDueToPDD,
                                     e.StageDueToProbationalPeriod
                                   
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<FacilitiesStageMigration>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<FacilitiesStageMigration>().Take(defaultCount) 
                                 select e);

                    return query.ToArray();
                }
            }
        }


        public IEnumerable<FacilitiesStageMigration> GetFacilitiesStageMigrationBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<FacilitiesStageMigration>()
                                 where searchParam.Contains(e.Product) || searchParam.Contains(e.Refno)
                                 select new
                                 {
                                     e.Refno,
                                     e.Product,
                                     e.PassDueDays,
                                     e.StageDueToPDD,
                                     e.StageDueToProbationalPeriod
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var products = (from e in query select new { e.Product }).Distinct();
                        var count = products.Count();
                        var ExportHandler = new ExcelService(path);
                        var product = count > 0 ? products.ToList().ElementAt(0).Product : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            product = products.ToList().ElementAt(i).Product;
                            response = ExportHandler.Export(query.Where(e => e.Product == product).ToList(), path + product.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<FacilitiesStageMigration>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<FacilitiesStageMigration>()
                                 where e.Product == searchParam || e.Refno == searchParam
                                 select e);
                    return query.ToArray();
                }
            }
        }


        public IEnumerable<FacilitiesStageMigration> GetFacilitiesStageMigrationByProduct(string ProductVal)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<FacilitiesStageMigration>()
                             where e.Product == ProductVal || e.Refno == ProductVal
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<string> GetDistinctProduct()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (entityContext.FacilitiesStageMigrationSet.Select(r => r.Product)).Distinct();

                return query.ToFullyLoaded();
            }
        }
    }
}