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
    [Export(typeof(IMarginalOutputRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MarginalOutputRepository : DataRepositoryBase<MarginalOutput>, IMarginalOutputRepository
    {
        protected override MarginalOutput AddEntity(IFRSContext entityContext, MarginalOutput entity)
        {
            return entityContext.Set<MarginalOutput>().Add(entity);
        }

        protected override MarginalOutput UpdateEntity(IFRSContext entityContext, MarginalOutput entity)
        {
            return (from e in entityContext.Set<MarginalOutput>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MarginalOutput> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MarginalOutput>()
                   select e;
        }

        protected override MarginalOutput GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MarginalOutput>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MarginalOutput> GetAvailableMarginalOutput(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<MarginalOutput>()
                                 select new
                                 {
                                     e.AssetDescription,
                                     e.AssetType,
                                     e.Counterparty,
                                     e.RatingAgency,
                                     e.CreditRating,
                                     e.Rating,
                                     e.MO1,
                                     e.MO2,
                                     e.MO3,
                                     e.MO4,
                                     e.MO5,
                                     e.MO6,
                                     e.MO7,
                                     e.MO8,
                                     e.MO9,
                                     e.MO10,
                                     e.MO11,
                                     e.MO12,
                                     e.MO13,
                                     e.MO14,
                                     e.MO15
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<MarginalOutput>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<MarginalOutput>().Take(defaultCount) 
                                 select e);

                    return query.ToArray();
                }
            }
        }


        public IEnumerable<MarginalOutput> GetMarginalOutputBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<MarginalOutput>()
                                 where searchParam.Contains(e.AssetDescription)
                                 select new
                                 {
                                     e.AssetDescription,
                                     e.AssetType,
                                     e.Counterparty,
                                     e.RatingAgency,
                                     e.CreditRating,
                                     e.Rating,
                                     e.MO1,
                                     e.MO2,
                                     e.MO3,
                                     e.MO4,
                                     e.MO5,
                                     e.MO6,
                                     e.MO7,
                                     e.MO8,
                                     e.MO9,
                                     e.MO10,
                                     e.MO11,
                                     e.MO12,
                                     e.MO13,
                                     e.MO14,
                                     e.MO15
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var products = (from e in query select new { e.AssetDescription }).Distinct();
                        var count = products.Count();
                        var ExportHandler = new ExcelService(path);
                        var AssetDescription = count > 0 ? products.ToList().ElementAt(0).AssetDescription : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            AssetDescription = products.ToList().ElementAt(i).AssetDescription;
                            response = ExportHandler.Export(query.Where(e => e.AssetDescription == AssetDescription).ToList(), path + AssetDescription.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<MarginalOutput>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<MarginalOutput>()
                                 where e.AssetDescription == searchParam
                                 select e);
                    return query.ToArray();
                }
            }
        }


        public IEnumerable<MarginalOutput> GetMarginalOutputByCreditRating(string CreditRatingVal)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<MarginalOutput>()
                             where e.CreditRating == CreditRatingVal
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<string> GetDistinctCreditRating()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (entityContext.MarginalOutputSet.Select(r => r.CreditRating)).Distinct();

                return query.ToFullyLoaded();
            }
        }
    }
}