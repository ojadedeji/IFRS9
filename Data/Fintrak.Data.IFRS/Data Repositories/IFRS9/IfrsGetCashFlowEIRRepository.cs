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
    [Export(typeof(IIfrsGetCashFlowEIRRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsGetCashFlowEIRRepository : DataRepositoryBase<IfrsGetCashFlowEIR>, IIfrsGetCashFlowEIRRepository
    {
        protected override IfrsGetCashFlowEIR AddEntity(IFRSContext entityContext, IfrsGetCashFlowEIR entity)
        {
            return entityContext.Set<IfrsGetCashFlowEIR>().Add(entity);
        }

        protected override IfrsGetCashFlowEIR UpdateEntity(IFRSContext entityContext, IfrsGetCashFlowEIR entity)
        {
            return (from e in entityContext.Set<IfrsGetCashFlowEIR>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsGetCashFlowEIR> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<IfrsGetCashFlowEIR>()
                        select e;
            query = query.OrderBy(a => a.ID).GroupBy(e => e.Refno).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override IfrsGetCashFlowEIR GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsGetCashFlowEIR>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsGetCashFlowEIR> GetIfrsGetCashFlowEIRBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<IfrsGetCashFlowEIR>()
                                 where searchParam.Contains(e.Refno)
                                 orderby e.Refno
                                 select new
                                 {
                                     e.Refno,
                                     e.DATE,
                                     e.DaysInMonth,
                                     e.PrincipalReypayment,
                                     e.InterestPayment,
                                     e.AmountDue,
                                     e.CummulativeDate,
                                     e.DaysInYear,
                                     e.YearsInDecimal,
                                     e.DiscountFactor,
                                     e.RevisedCASHFLOW,
                                     e.PVCashflow
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var accounts = (from e in query select new { e.Refno }).Distinct();
                        var count = accounts.Count();
                        var ExportHandler = new ExcelService(path);
                        var accountNo = count > 0 ? accounts.ToList().ElementAt(0).Refno : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            accountNo = accounts.ToList().ElementAt(i).Refno;
                            response = ExportHandler.Export(query.Where(e => e.Refno == accountNo).ToList(), path + accountNo.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<IfrsGetCashFlowEIR>().Take(0).ToArray().OrderBy(c => c.Refno).ThenBy(c => c.Refno); ;
                }
                else
                {
                    DateTime searchpar = Convert.ToDateTime(searchParam);
                    var query = (from e in entityContext.Set<IfrsGetCashFlowEIR>()
                                 where e.DATE == searchpar
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<IfrsGetCashFlowEIR> GetIfrsGetCashFlowEIRs(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsGetCashFlowEIR>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<IfrsGetCashFlowEIR> ExportIfrsGetCashFlowEIR(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsGetCashFlowEIR>()
                                 select new
                                 {
                                     e.Refno,
                                     e.DATE,
                                     e.DaysInMonth,
                                     e.PrincipalReypayment,
                                     e.InterestPayment,
                                     e.AmountDue,
                                     e.CummulativeDate,
                                     e.DaysInYear,
                                     e.YearsInDecimal,
                                     e.DiscountFactor,
                                     e.RevisedCASHFLOW,
                                     e.PVCashflow
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsGetCashFlowEIR>().Take(0).ToArray().OrderBy(c => c.Refno).ThenBy(c => c.Refno);

                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsGetCashFlowEIR>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
