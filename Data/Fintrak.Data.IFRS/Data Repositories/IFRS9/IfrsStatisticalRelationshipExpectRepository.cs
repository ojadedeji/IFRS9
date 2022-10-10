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
    [Export(typeof(IIfrsStatisticalRelationshipExpectRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsStatisticalRelationshipExpectRepository : DataRepositoryBase<IfrsStatisticalRelationshipExpect>, IIfrsStatisticalRelationshipExpectRepository
    {
        protected override IfrsStatisticalRelationshipExpect AddEntity(IFRSContext entityContext, IfrsStatisticalRelationshipExpect entity)
        {
            return entityContext.Set<IfrsStatisticalRelationshipExpect>().Add(entity);
        }

        protected override IfrsStatisticalRelationshipExpect UpdateEntity(IFRSContext entityContext, IfrsStatisticalRelationshipExpect entity)
        {
            return (from e in entityContext.Set<IfrsStatisticalRelationshipExpect>()
                    where e.sta_id == entity.sta_id
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsStatisticalRelationshipExpect> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<IfrsStatisticalRelationshipExpect>()
                        select e;
            query = query.OrderBy(a => a.sta_id).GroupBy(e => e.mev_code).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override IfrsStatisticalRelationshipExpect GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsStatisticalRelationshipExpect>()
                         where e.sta_id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsStatisticalRelationshipExpect> GetIfrsStatisticalRelationshipExpectBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<IfrsStatisticalRelationshipExpect>()
                                 where searchParam.Contains(e.mev_code)
                                 orderby e.mev_code
                                 select new
                                 {
                                     e.mev_code,
                                     e.mev_decs,
                                     e.rel_exp
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var accounts = (from e in query select new { e.mev_code }).Distinct();
                        var count = accounts.Count();
                        var ExportHandler = new ExcelService(path);
                        var accountNo = count > 0 ? accounts.ToList().ElementAt(0).mev_code : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            accountNo = accounts.ToList().ElementAt(i).mev_code;
                            response = ExportHandler.Export(query.Where(e => e.mev_code == accountNo).ToList(), path + accountNo.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<IfrsStatisticalRelationshipExpect>().Take(0).ToArray().OrderBy(c => c.mev_code).ThenBy(c => c.mev_code); ;
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsStatisticalRelationshipExpect>()
                                 where e.mev_code == searchParam
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<IfrsStatisticalRelationshipExpect> GetIfrsStatisticalRelationshipExpects(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsStatisticalRelationshipExpect>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<IfrsStatisticalRelationshipExpect> ExportIfrsStatisticalRelationshipExpect(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsStatisticalRelationshipExpect>()
                                 select new
                                 {
                                     e.mev_code,
                                     e.mev_decs,
                                     e.rel_exp
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsStatisticalRelationshipExpect>().Take(0).ToArray().OrderBy(c => c.mev_code).ThenBy(c => c.mev_code);

                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsStatisticalRelationshipExpect>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
