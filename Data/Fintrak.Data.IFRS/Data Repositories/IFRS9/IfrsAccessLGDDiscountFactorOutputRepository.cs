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
    [Export(typeof(IIfrsAccessLGDDiscountFactorOutputRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsAccessLGDDiscountFactorOutputRepository : DataRepositoryBase<IfrsAccessLGDDiscountFactorOutput>, IIfrsAccessLGDDiscountFactorOutputRepository
    {
        protected override IfrsAccessLGDDiscountFactorOutput AddEntity(IFRSContext entityContext, IfrsAccessLGDDiscountFactorOutput entity)
        {
            return entityContext.Set<IfrsAccessLGDDiscountFactorOutput>().Add(entity);
        }

        protected override IfrsAccessLGDDiscountFactorOutput UpdateEntity(IFRSContext entityContext, IfrsAccessLGDDiscountFactorOutput entity)
        {
            return (from e in entityContext.Set<IfrsAccessLGDDiscountFactorOutput>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsAccessLGDDiscountFactorOutput> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<IfrsAccessLGDDiscountFactorOutput>()
                        select e;
            query = query.OrderBy(a => a.ID).GroupBy(e => e.HistoryQuarter).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override IfrsAccessLGDDiscountFactorOutput GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsAccessLGDDiscountFactorOutput>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsAccessLGDDiscountFactorOutput> GetIfrsAccessLGDDiscountFactorOutputBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<IfrsAccessLGDDiscountFactorOutput>()
                                 where searchParam.Contains(e.Sector)
                                 orderby e.HistoryQuarter
                                 select new
                                 {
                                     e.Seq,
                                     e.Sector,
                                     e.HistoryQuarter,
                                     e.Discount
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var accounts = (from e in query select new { e.Sector }).Distinct();
                        var count = accounts.Count();
                        var ExportHandler = new ExcelService(path);
                        var accountNo = count > 0 ? accounts.ToList().ElementAt(0).Sector : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            accountNo = accounts.ToList().ElementAt(i).Sector;
                            response = ExportHandler.Export(query.Where(e => e.Sector == accountNo).ToList(), path + accountNo.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<IfrsAccessLGDDiscountFactorOutput>().Take(0).ToArray().OrderBy(c => c.HistoryQuarter).ThenBy(c => c.Sector); ;
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsAccessLGDDiscountFactorOutput>()
                                 where e.Sector == searchParam
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<IfrsAccessLGDDiscountFactorOutput> GetIfrsAccessLGDDiscountFactorOutputs(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsAccessLGDDiscountFactorOutput>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<IfrsAccessLGDDiscountFactorOutput> ExportIfrsAccessLGDDiscountFactorOutput(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsAccessLGDDiscountFactorOutput>()
                                 select new
                                 {
                                     e.Seq,
                                     e.Sector,
                                     e.HistoryQuarter,
                                     e.Discount
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsAccessLGDDiscountFactorOutput>().Take(0).ToArray().OrderBy(c => c.HistoryQuarter).ThenBy(c => c.Sector);

                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsAccessLGDDiscountFactorOutput>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
