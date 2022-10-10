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
    [Export(typeof(IIfrsConfidenceIntervalAbpRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsConfidenceIntervalAbpRepository : DataRepositoryBase<IfrsConfidenceIntervalAbp>, IIfrsConfidenceIntervalAbpRepository
    {
        protected override IfrsConfidenceIntervalAbp AddEntity(IFRSContext entityContext, IfrsConfidenceIntervalAbp entity)
        {
            return entityContext.Set<IfrsConfidenceIntervalAbp>().Add(entity);
        }

        protected override IfrsConfidenceIntervalAbp UpdateEntity(IFRSContext entityContext, IfrsConfidenceIntervalAbp entity)
        {
            return (from e in entityContext.Set<IfrsConfidenceIntervalAbp>()
                    where e.IntervalId == entity.IntervalId
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsConfidenceIntervalAbp> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<IfrsConfidenceIntervalAbp>()
                        select e;
            query = query.OrderBy(a => a.IntervalId).GroupBy(e => e.Ci_level).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override IfrsConfidenceIntervalAbp GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsConfidenceIntervalAbp>()
                         where e.IntervalId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsConfidenceIntervalAbp> GetIfrsConfidenceIntervalAbpBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<IfrsConfidenceIntervalAbp>()
                                 where searchParam.Contains(e.Ci_level.ToString())
                                 //orderby e.Periodic_date
                                 select new
                                 {
                                     e.Ci_level,
                                     e.Z_score
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var accounts = (from e in query select new { e.Ci_level }).Distinct();
                        var count = accounts.Count();
                        var ExportHandler = new ExcelService(path);
                        var accountNo = count > 0 ? accounts.ToList().ElementAt(0).Ci_level.ToString() : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            accountNo = accounts.ToList().ElementAt(i).Ci_level.ToString();
                            response = ExportHandler.Export(query.Where(e => e.Ci_level.ToString() == accountNo).ToList(), path + accountNo.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<IfrsConfidenceIntervalAbp>().Take(0).ToArray().OrderBy(c => c.Z_score).ThenBy(c => c.Ci_level); ;
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsConfidenceIntervalAbp>()
                                 where e.Ci_level.ToString() == searchParam
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<IfrsConfidenceIntervalAbp> GetIfrsConfidenceIntervalAbps(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsConfidenceIntervalAbp>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<double> GetIfrsDistinctCiLevel()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsConfidenceIntervalAbp>() //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e.Ci_level).Distinct();
                return query.ToArray();
            }
        }

        public IEnumerable<IfrsConfidenceIntervalAbp> ExportIfrsConfidenceIntervalAbp(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsConfidenceIntervalAbp>()
                                 select new
                                 {
                                     e.Ci_level,
                                     e.Z_score
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsConfidenceIntervalAbp>().Take(0).ToArray().OrderBy(c => c.Ci_level).ThenBy(c => c.Z_score);

                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsConfidenceIntervalAbp>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
