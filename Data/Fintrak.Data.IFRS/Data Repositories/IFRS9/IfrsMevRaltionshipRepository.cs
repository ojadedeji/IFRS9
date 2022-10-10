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
    [Export(typeof(IIfrsMevRaltionshipRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsMevRaltionshipRepository : DataRepositoryBase<IfrsMevRaltionship>, IIfrsMevRaltionshipRepository
    {
        protected override IfrsMevRaltionship AddEntity(IFRSContext entityContext, IfrsMevRaltionship entity)
        {
            return entityContext.Set<IfrsMevRaltionship>().Add(entity);
        }

        protected override IfrsMevRaltionship UpdateEntity(IFRSContext entityContext, IfrsMevRaltionship entity)
        {
            return (from e in entityContext.Set<IfrsMevRaltionship>()
                    where e.rel_id == entity.rel_id
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsMevRaltionship> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<IfrsMevRaltionship>()
                        select e;
            query = query.OrderBy(a => a.rel_id).GroupBy(e => e.mev).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override IfrsMevRaltionship GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsMevRaltionship>()
                         where e.rel_id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsMevRaltionship> GetIfrsMevRaltionshipBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<IfrsMevRaltionship>()
                                 where searchParam.Contains(e.mev)
                                 orderby e.mev
                                 select new
                                 {
                                     e.mev,
                                     e.independent_variable,
                                     e.corr_to_dependent_variable,
                                     e.corr_relationship,
                                     e.actual_relationship,
                                     e.relationship_interpret,
                                     e.remark
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var accounts = (from e in query select new { e.mev }).Distinct();
                        var count = accounts.Count();
                        var ExportHandler = new ExcelService(path);
                        var accountNo = count > 0 ? accounts.ToList().ElementAt(0).mev : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            accountNo = accounts.ToList().ElementAt(i).mev;
                            response = ExportHandler.Export(query.Where(e => e.mev == accountNo).ToList(), path + accountNo.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<IfrsMevRaltionship>().Take(0).ToArray().OrderBy(c => c.mev).ThenBy(c => c.mev); ;
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsMevRaltionship>()
                                 where e.mev == searchParam
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<IfrsMevRaltionship> GetIfrsMevRaltionships(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsMevRaltionship>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<IfrsMevRaltionship> ExportIfrsMevRaltionship(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsMevRaltionship>()
                                 select new
                                 {
                                     e.mev,
                                     e.independent_variable,
                                     e.corr_to_dependent_variable,
                                     e.corr_relationship,
                                     e.actual_relationship,
                                     e.relationship_interpret,
                                     e.remark
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsMevRaltionship>().Take(0).ToArray().OrderBy(c => c.mev).ThenBy(c => c.mev);

                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsMevRaltionship>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
