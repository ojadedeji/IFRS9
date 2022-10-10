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
    [Export(typeof(IIfrsModicationSummaryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsModicationSummaryRepository : DataRepositoryBase<IfrsModicationSummary>, IIfrsModicationSummaryRepository
    {
        protected override IfrsModicationSummary AddEntity(IFRSContext entityContext, IfrsModicationSummary entity)
        {
            return entityContext.Set<IfrsModicationSummary>().Add(entity);
        }

        protected override IfrsModicationSummary UpdateEntity(IFRSContext entityContext, IfrsModicationSummary entity)
        {
            return (from e in entityContext.Set<IfrsModicationSummary>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsModicationSummary> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<IfrsModicationSummary>()
                        select e;
            query = query.OrderBy(a => a.ID).GroupBy(e => e.refno).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override IfrsModicationSummary GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsModicationSummary>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsModicationSummary> GetIfrsModicationSummaryBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<IfrsModicationSummary>()
                                 where searchParam.Contains(e.refno)
                                 orderby e.refno
                                 select new
                                 {
                                     e.refno,
                                     e.Modificationgain_loss,
                                     e.Treatment,
                                     e.comment
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var accounts = (from e in query select new { e.refno }).Distinct();
                        var count = accounts.Count();
                        var ExportHandler = new ExcelService(path);
                        var accountNo = count > 0 ? accounts.ToList().ElementAt(0).refno : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            accountNo = accounts.ToList().ElementAt(i).refno;
                            response = ExportHandler.Export(query.Where(e => e.refno == accountNo).ToList(), path + accountNo.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<IfrsModicationSummary>().Take(0).ToArray().OrderBy(c => c.refno).ThenBy(c => c.refno); ;
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsModicationSummary>()
                                 where e.refno == searchParam
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<IfrsModicationSummary> GetIfrsModicationSummarys(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsModicationSummary>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<IfrsModicationSummary> ExportIfrsModicationSummary(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsModicationSummary>()
                                 select new
                                 {
                                     e.refno,
                                     e.Modificationgain_loss,
                                     e.Treatment,
                                     e.comment
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsModicationSummary>().Take(0).ToArray().OrderBy(c => c.refno).ThenBy(c => c.Modificationgain_loss);

                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsModicationSummary>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
