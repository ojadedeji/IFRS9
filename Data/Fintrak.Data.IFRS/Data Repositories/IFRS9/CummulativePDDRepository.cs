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
    [Export(typeof(ICummulativePDDRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CummulativePDDRepository : DataRepositoryBase<CummulativePDD>, ICummulativePDDRepository
    {
        protected override CummulativePDD AddEntity(IFRSContext entityContext, CummulativePDD entity)
        {
            return entityContext.Set<CummulativePDD>().Add(entity);
        }

        protected override CummulativePDD UpdateEntity(IFRSContext entityContext, CummulativePDD entity)
        {
            return (from e in entityContext.Set<CummulativePDD>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CummulativePDD> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<CummulativePDD>()
                   select e;
        }

        protected override CummulativePDD GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CummulativePDD>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<CummulativePDD> GetAvailableCummulativePDD(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<CummulativePDD>()
                                 select new
                                 {
                                     e.AssetDescription,
                                     e.AssetType,
                                     e.Counterparty,
                                     e.IssueDate,
                                     e.MaturityDate,
                                     e.DaysToMaturity,
                                     e.YearsToMaturity,
                                     e.RatingAgency,
                                     e.CreditRating,
                                     e.SandPRating,
                                     e.PD1,
                                     e.PD2,
                                     e.PD3,
                                     e.PD4,
                                     e.PD5,
                                     e.PD6,
                                     e.PD7,
                                     e.PD8,
                                     e.PD9,
                                     e.PD10,
                                     e.PD11,
                                     e.PD12,
                                     e.PD13,
                                     e.PD14,
                                     e.PD15
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<CummulativePDD>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<CummulativePDD>().Take(defaultCount) 
                                 select e);

                    return query.ToArray();
                }
            }
        }


        public IEnumerable<CummulativePDD> GetCummulativePDDBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<CummulativePDD>()
                                 where searchParam.Contains(e.AssetDescription)
                                 select new
                                 {
                                     e.AssetDescription,
                                     e.AssetType,
                                     e.Counterparty,
                                     e.IssueDate,
                                     e.MaturityDate,
                                     e.DaysToMaturity,
                                     e.YearsToMaturity,
                                     e.RatingAgency,
                                     e.CreditRating,
                                     e.SandPRating,
                                     e.PD1,
                                     e.PD2,
                                     e.PD3,
                                     e.PD4,
                                     e.PD5,
                                     e.PD6,
                                     e.PD7,
                                     e.PD8,
                                     e.PD9,
                                     e.PD10,
                                     e.PD11,
                                     e.PD12,
                                     e.PD13,
                                     e.PD14,
                                     e.PD15
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

                    return new List<CummulativePDD>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<CummulativePDD>()
                                 where e.AssetDescription == searchParam
                                 select e);
                    return query.ToArray();
                }
            }
        }


        public IEnumerable<CummulativePDD> GetCummulativePDDByRatingAgency(string RatingAgencyVal)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<CummulativePDD>()
                             where e.RatingAgency == RatingAgencyVal
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<string> GetDistinctRatingAgency()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (entityContext.CummulativePDDSet.Select(r => r.RatingAgency)).Distinct();

                return query.ToFullyLoaded();
            }
        }
    }
}