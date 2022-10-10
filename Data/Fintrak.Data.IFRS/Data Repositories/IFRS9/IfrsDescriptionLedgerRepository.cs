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
    [Export(typeof(IIfrsDescriptionLedgerRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsDescriptionLedgerRepository : DataRepositoryBase<IfrsDescriptionLedger>, IIfrsDescriptionLedgerRepository
    {
        protected override IfrsDescriptionLedger AddEntity(IFRSContext entityContext, IfrsDescriptionLedger entity)
        {
            return entityContext.Set<IfrsDescriptionLedger>().Add(entity);
        }

        protected override IfrsDescriptionLedger UpdateEntity(IFRSContext entityContext, IfrsDescriptionLedger entity)
        {
            return (from e in entityContext.Set<IfrsDescriptionLedger>()
                    where e.id == entity.id
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsDescriptionLedger> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<IfrsDescriptionLedger>()
                        select e;
            query = query.OrderBy(a => a.id).GroupBy(e => e.refno).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override IfrsDescriptionLedger GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsDescriptionLedger>()
                         where e.id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsDescriptionLedger> GetIfrsDescriptionLedgerBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<IfrsDescriptionLedger>()
                                 where searchParam.Contains(e.refno)
                                 orderby e.refno
                                 select new
                                 {
                                     e.id,
                                     e.refno,
                                     e.Description,
                                     e.Ledger
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

                    return new List<IfrsDescriptionLedger>().Take(0).ToArray().OrderBy(c => c.refno).ThenBy(c => c.refno); ;
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsDescriptionLedger>()
                                 where e.refno == searchParam
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<IfrsDescriptionLedger> GetIfrsDescriptionLedgers(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsDescriptionLedger>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<IfrsDescriptionLedger> ExportIfrsDescriptionLedger(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsDescriptionLedger>()
                                 select new
                                 {
                                     e.id,
                                     e.refno,
                                     e.Description,
                                     e.Ledger
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsDescriptionLedger>().Take(0).ToArray().OrderBy(c => c.refno).ThenBy(c => c.refno);

                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsDescriptionLedger>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
