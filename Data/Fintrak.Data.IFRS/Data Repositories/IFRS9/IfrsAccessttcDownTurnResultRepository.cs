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
    [Export(typeof(IIfrsAccessttcDownTurnResultRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsAccessttcDownTurnResultRepository : DataRepositoryBase<IfrsAccessttcDownTurnResult>, IIfrsAccessttcDownTurnResultRepository
    {
        protected override IfrsAccessttcDownTurnResult AddEntity(IFRSContext entityContext, IfrsAccessttcDownTurnResult entity)
        {
            return entityContext.Set<IfrsAccessttcDownTurnResult>().Add(entity);
        }

        protected override IfrsAccessttcDownTurnResult UpdateEntity(IFRSContext entityContext, IfrsAccessttcDownTurnResult entity)
        {
            return (from e in entityContext.Set<IfrsAccessttcDownTurnResult>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsAccessttcDownTurnResult> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<IfrsAccessttcDownTurnResult>()
                        select e;
            query = query.OrderBy(a => a.ID).GroupBy(e => e.DownTurnLGD).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override IfrsAccessttcDownTurnResult GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsAccessttcDownTurnResult>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsAccessttcDownTurnResult> GetIfrsAccessttcDownTurnResultBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<IfrsAccessttcDownTurnResult>()
                                 where searchParam.Contains(e.Sector)
                                 orderby e.DownTurnLGD
                                 select new
                                 {
                                     e.Sector,
                                     e.TTCLGD,
                                     e.DownTurnLGD
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

                    return new List<IfrsAccessttcDownTurnResult>().Take(0).ToArray().OrderBy(c => c.DownTurnLGD).ThenBy(c => c.Sector); ;
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsAccessttcDownTurnResult>()
                                 where e.Sector == searchParam
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<IfrsAccessttcDownTurnResult> GetIfrsAccessttcDownTurnResults(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsAccessttcDownTurnResult>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<IfrsAccessttcDownTurnResult> ExportIfrsAccessttcDownTurnResult(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsAccessttcDownTurnResult>()
                                 select new
                                 {
                                     e.Sector,
                                     e.TTCLGD,
                                     e.DownTurnLGD
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsAccessttcDownTurnResult>().Take(0).ToArray().OrderBy(c => c.DownTurnLGD).ThenBy(c => c.Sector);

                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsAccessttcDownTurnResult>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
