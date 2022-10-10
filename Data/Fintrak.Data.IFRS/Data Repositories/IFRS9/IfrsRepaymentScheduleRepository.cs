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
    [Export(typeof(IIfrsRepaymentScheduleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsRepaymentScheduleRepository : DataRepositoryBase<IfrsRepaymentSchedule>, IIfrsRepaymentScheduleRepository
    {
        protected override IfrsRepaymentSchedule AddEntity(IFRSContext entityContext, IfrsRepaymentSchedule entity)
        {
            return entityContext.Set<IfrsRepaymentSchedule>().Add(entity);
        }

        protected override IfrsRepaymentSchedule UpdateEntity(IFRSContext entityContext, IfrsRepaymentSchedule entity)
        {
            return (from e in entityContext.Set<IfrsRepaymentSchedule>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsRepaymentSchedule> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<IfrsRepaymentSchedule>()
                        select e;
            query = query.OrderBy(a => a.ID).GroupBy(e => e.PaymentDate).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override IfrsRepaymentSchedule GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsRepaymentSchedule>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsRepaymentSchedule> GetIfrsRepaymentScheduleBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<IfrsRepaymentSchedule>()
                                 where searchParam.Contains(e.Refno)
                                 orderby e.PaymentDate
                                 select new
                                 {
                                     e.ID,
                                     e.Refno,
                                     e.num_pmt,
                                     e.PaymentDate,
                                     e.BeginingBalance,
                                     e.GrossInterest,
                                     e.NetInterest,
                                     e.Principal,
                                     e.InterestPrincipal,
                                     e.ResidualValue,
                                     e.TotalBiAnnualPayment,
                                     e.EndingBalance,
                                     e.CummulativeInterest,
                                     e.ProductName
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

                    return new List<IfrsRepaymentSchedule>().Take(0).ToArray().OrderBy(c => c.Refno).ThenBy(c => c.PaymentDate); ;
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsRepaymentSchedule>()
                                 where e.Refno == searchParam
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<IfrsRepaymentSchedule> GetIfrsRepaymentSchedules(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsRepaymentSchedule>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<IfrsRepaymentSchedule> ExportIfrsRepaymentSchedule(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsRepaymentSchedule>()
                                 select new
                                 {
                                     e.ID,
                                     e.Refno,
                                     e.num_pmt,
                                     e.PaymentDate,
                                     e.BeginingBalance,
                                     e.GrossInterest,
                                     e.NetInterest,
                                     e.Principal,
                                     e.InterestPrincipal,
                                     e.ResidualValue,
                                     e.TotalBiAnnualPayment,
                                     e.EndingBalance,
                                     e.CummulativeInterest,
                                     e.ProductName
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsRepaymentSchedule>().Take(0).ToArray().OrderBy(c => c.Refno).ThenBy(c => c.PaymentDate);

                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsRepaymentSchedule>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
