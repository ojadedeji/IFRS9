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
    [Export(typeof(IIfrsHistoricalMEVAbpRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsHistoricalMEVAbpRepository : DataRepositoryBase<IfrsHistoricalMEVAbp>, IIfrsHistoricalMEVAbpRepository
    {
        protected override IfrsHistoricalMEVAbp AddEntity(IFRSContext entityContext, IfrsHistoricalMEVAbp entity)
        {
            return entityContext.Set<IfrsHistoricalMEVAbp>().Add(entity);
        }

        protected override IfrsHistoricalMEVAbp UpdateEntity(IFRSContext entityContext, IfrsHistoricalMEVAbp entity)
        {
            return (from e in entityContext.Set<IfrsHistoricalMEVAbp>()
                    where e.HistoricalMevId == entity.HistoricalMevId
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsHistoricalMEVAbp> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<IfrsHistoricalMEVAbp>()
                        select e;
            query = query.OrderBy(a => a.HistoricalMevId).GroupBy(e => e.Periodic_date).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override IfrsHistoricalMEVAbp GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsHistoricalMEVAbp>()
                         where e.HistoricalMevId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsHistoricalMEVAbp> GetIfrsHistoricalMEVAbpBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<IfrsHistoricalMEVAbp>()
                                 where searchParam.Contains(e.MevId)
                                 orderby e.Periodic_date
                                 select new
                                 {
                                     e.Periodic_date,
                                     e.Period_in_quarter,
                                     e.Factor,
                                     e.MevId
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var accounts = (from e in query select new { e.MevId }).Distinct();
                        var count = accounts.Count();
                        var ExportHandler = new ExcelService(path);
                        var accountNo = count > 0 ? accounts.ToList().ElementAt(0).MevId : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            accountNo = accounts.ToList().ElementAt(i).MevId;
                            response = ExportHandler.Export(query.Where(e => e.MevId == accountNo).ToList(), path + accountNo.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<IfrsHistoricalMEVAbp>().Take(0).ToArray().OrderBy(c => c.Periodic_date).ThenBy(c => c.MevId); ;
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsHistoricalMEVAbp>()
                                 where e.MevId == searchParam
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<IfrsHistoricalMEVAbp> GetIfrsHistoricalMEVAbps(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsHistoricalMEVAbp>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<IfrsHistoricalMEVAbp> ExportIfrsHistoricalMEVAbp(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsHistoricalMEVAbp>()
                                 select new
                                 {
                                     e.Periodic_date,
                                     e.Period_in_quarter,
                                     e.Factor,
                                     e.MevId
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsHistoricalMEVAbp>().Take(0).ToArray().OrderBy(c => c.Periodic_date).ThenBy(c => c.MevId);

                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsHistoricalMEVAbp>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
