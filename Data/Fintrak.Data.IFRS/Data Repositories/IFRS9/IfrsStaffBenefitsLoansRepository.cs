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
    [Export(typeof(IIfrsStaffBenefitsLoansRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsStaffBenefitsLoansRepository : DataRepositoryBase<IfrsStaffBenefitsLoans>, IIfrsStaffBenefitsLoansRepository
    {
        protected override IfrsStaffBenefitsLoans AddEntity(IFRSContext entityContext, IfrsStaffBenefitsLoans entity)
        {
            return entityContext.Set<IfrsStaffBenefitsLoans>().Add(entity);
        }

        protected override IfrsStaffBenefitsLoans UpdateEntity(IFRSContext entityContext, IfrsStaffBenefitsLoans entity)
        {
            return (from e in entityContext.Set<IfrsStaffBenefitsLoans>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsStaffBenefitsLoans> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<IfrsStaffBenefitsLoans>()
                        select e;
            query = query.OrderBy(a => a.ID).GroupBy(e => e.AccountNo).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override IfrsStaffBenefitsLoans GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsStaffBenefitsLoans>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsStaffBenefitsLoans> GetIfrsStaffBenefitsLoansBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<IfrsStaffBenefitsLoans>()
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
                                     e.TenorInMonths,
                                     e.PastPeriodInMonthsPreviousYearEnd,
                                     e.PastPeriodInMonthsPreviousYearEndPlusOneMonth,
                                     e.OutstandingPeriodInMonths,
                                     e.NoOfDays,
                                     e.NoOfMonths,
                                     e.ContractrualCashflow,
                                     e.FairValue,
                                     e.EmployeeBenefit,
                                     e.MonthlyAmortization,
                                     e.PastPeriodRepayments,
                                     e.PastPeriodAmortization,
                                     e.AmortisedPrepaidBenefit,
                                     e.EmployeeBenefitBalance,
                                     e.MarketRateInterestIncome,
                                     e.OffMarketRateInterestIncome,
                                     e.InterestDifferential,
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

                    return new List<IfrsStaffBenefitsLoans>().Take(0).ToArray().OrderBy(c => c.AccountNo).ThenBy(c => c.AccountNo); ;
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsStaffBenefitsLoans>()
                                 where e.RefNo == searchParam
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<IfrsStaffBenefitsLoans> GetIfrsStaffBenefitsLoans(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsStaffBenefitsLoans>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<IfrsStaffBenefitsLoans> ExportIfrsStaffBenefitsLoans(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsStaffBenefitsLoans>()
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
                                     e.TenorInMonths,
                                     e.PastPeriodInMonthsPreviousYearEnd,
                                     e.PastPeriodInMonthsPreviousYearEndPlusOneMonth,
                                     e.OutstandingPeriodInMonths,
                                     e.NoOfDays,
                                     e.NoOfMonths,
                                     e.ContractrualCashflow,
                                     e.FairValue,
                                     e.EmployeeBenefit,
                                     e.MonthlyAmortization,
                                     e.PastPeriodRepayments,
                                     e.PastPeriodAmortization,
                                     e.AmortisedPrepaidBenefit,
                                     e.EmployeeBenefitBalance,
                                     e.MarketRateInterestIncome,
                                     e.OffMarketRateInterestIncome,
                                     e.InterestDifferential,
                                     e.IFRSAdjustedStaffLoanBalances
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsStaffBenefitsLoans>().Take(0).ToArray().OrderBy(c => c.AccountNo).ThenBy(c => c.AccountNo);

                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsStaffBenefitsLoans>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
