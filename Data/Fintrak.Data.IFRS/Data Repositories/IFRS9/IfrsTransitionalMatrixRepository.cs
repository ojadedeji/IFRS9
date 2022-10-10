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
    [Export(typeof(IIfrsTransitionalMatrixRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsTransitionalMatrixRepository : DataRepositoryBase<IfrsTransitionalMatrix>, IIfrsTransitionalMatrixRepository
    {
        protected override IfrsTransitionalMatrix AddEntity(IFRSContext entityContext, IfrsTransitionalMatrix entity)
        {
            return entityContext.Set<IfrsTransitionalMatrix>().Add(entity);
        }

        protected override IfrsTransitionalMatrix UpdateEntity(IFRSContext entityContext, IfrsTransitionalMatrix entity)
        {
            return (from e in entityContext.Set<IfrsTransitionalMatrix>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsTransitionalMatrix> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<IfrsTransitionalMatrix>()
                        select e;
            query = query.OrderBy(a => a.ID).GroupBy(e => e.pdstage).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override IfrsTransitionalMatrix GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsTransitionalMatrix>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsTransitionalMatrix> GetIfrsTransitionalMatrixBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<IfrsTransitionalMatrix>()
                                 where searchParam.Contains(e.pdstage.ToString())
                                 orderby e.pdstage
                                 select new
                                 {
                                     e.Stage,
                                     e.a,
                                     e.b,
                                     e.Rowno,
                                     e.pdstage
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var accounts = (from e in query select new { e.pdstage }).Distinct();
                        var count = accounts.Count();
                        var ExportHandler = new ExcelService(path);
                        var accountNo = count > 0 ? accounts.ToList().ElementAt(0).pdstage.ToString() : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            accountNo = accounts.ToList().ElementAt(i).pdstage.ToString();
                            response = ExportHandler.Export(query.Where(e => e.pdstage.ToString() == accountNo).ToList(), path + accountNo.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<IfrsTransitionalMatrix>().Take(0).ToArray().OrderBy(c => c.pdstage).ThenBy(c => c.pdstage); ;
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsTransitionalMatrix>()
                                 where e.Stage.ToString() == searchParam
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<IfrsTransitionalMatrix> GetIfrsTransitionalMatrixs(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsTransitionalMatrix>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<IfrsTransitionalMatrix> ExportIfrsTransitionalMatrix(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsTransitionalMatrix>()
                                 select new
                                 {
                                     e.Stage,
                                     e.a,
                                     e.b,
                                     e.Rowno,
                                     e.pdstage
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsTransitionalMatrix>().Take(0).ToArray().OrderBy(c => c.pdstage).ThenBy(c => c.pdstage);

                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsTransitionalMatrix>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
