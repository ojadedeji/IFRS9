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
    [Export(typeof(IIfrsStaffBenefitsLoansReportRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsStaffBenefitsLoansReportRepository : DataRepositoryBase<IfrsStaffBenefitsLoansReport>, IIfrsStaffBenefitsLoansReportRepository
    {
        protected override IfrsStaffBenefitsLoansReport AddEntity(IFRSContext entityContext, IfrsStaffBenefitsLoansReport entity)
        {
            return entityContext.Set<IfrsStaffBenefitsLoansReport>().Add(entity);
        }

        protected override IfrsStaffBenefitsLoansReport UpdateEntity(IFRSContext entityContext, IfrsStaffBenefitsLoansReport entity)
        {
            return (from e in entityContext.Set<IfrsStaffBenefitsLoansReport>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsStaffBenefitsLoansReport> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<IfrsStaffBenefitsLoansReport>()
                        select e;
            query = query.OrderBy(a => a.ID).GroupBy(e => e.AccountNo).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override IfrsStaffBenefitsLoansReport GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsStaffBenefitsLoansReport>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsStaffBenefitsLoansReport> GetIfrsStaffBenefitsLoansReportBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<IfrsStaffBenefitsLoansReport>()
                                 where searchParam.Contains(e.AccountNo)
                                 orderby e.AccountNo
                                 select new
                                 {
                                     e.ID,
                                     e.RefNo,
                                     e.AccountNo,
                                     e.StaffName,
                                     e.LoanType,
                                     e.producttype,
                                     e.LoanAmount,
                                     e.OutstandingPrincBal,
                                     e.InterestRate,
                                     e.Startdate,
                                     e.MaturityDate,
                                     e.MarketRate,
                                     e.FairValue,
                                     e.EmployeeBenefit,
                                     e.EmployeeBenefitBalance,
                                     e.IFRSAdjustedStaffLoanBalances
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var accounts = (from e in query select new { e.AccountNo }).Distinct();
                        var count = accounts.Count();
                        var ExportHandler = new ExcelService(path);
                        var accountNo = count > 0 ? accounts.ToList().ElementAt(0).AccountNo : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            accountNo = accounts.ToList().ElementAt(i).AccountNo;
                            response = ExportHandler.Export(query.Where(e => e.AccountNo == accountNo).ToList(), path + accountNo.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<IfrsStaffBenefitsLoansReport>().Take(0).ToArray().OrderBy(c => c.AccountNo).ThenBy(c => c.AccountNo); ;
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsStaffBenefitsLoansReport>()
                                 where e.RefNo == searchParam
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<IfrsStaffBenefitsLoansReport> GetIfrsStaffBenefitsLoansReport(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsStaffBenefitsLoansReport>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<IfrsStaffBenefitsLoansReport> ExportIfrsStaffBenefitsLoansReport(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsStaffBenefitsLoansReport>()
                                 select new
                                 {
                                     e.ID,
                                     e.RefNo,
                                     e.AccountNo,
                                     e.StaffName,
                                     e.LoanType,
                                     e.producttype,
                                     e.LoanAmount,
                                     e.OutstandingPrincBal,
                                     e.InterestRate,
                                     e.Startdate,
                                     e.MaturityDate,
                                     e.MarketRate,
                                     e.FairValue,
                                     e.EmployeeBenefit,
                                     e.EmployeeBenefitBalance,
                                     e.IFRSAdjustedStaffLoanBalances
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsStaffBenefitsLoansReport>().Take(0).ToArray().OrderBy(c => c.AccountNo).ThenBy(c => c.AccountNo);

                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsStaffBenefitsLoansReport>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
