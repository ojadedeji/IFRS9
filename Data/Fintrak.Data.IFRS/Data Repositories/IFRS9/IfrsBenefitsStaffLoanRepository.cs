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
    [Export(typeof(IIfrsBenefitsStaffLoanRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsBenefitsStaffLoanRepository : DataRepositoryBase<IfrsBenefitsStaffLoan>, IIfrsBenefitsStaffLoanRepository
    {
        protected override IfrsBenefitsStaffLoan AddEntity(IFRSContext entityContext, IfrsBenefitsStaffLoan entity)
        {
            return entityContext.Set<IfrsBenefitsStaffLoan>().Add(entity);
        }

        protected override IfrsBenefitsStaffLoan UpdateEntity(IFRSContext entityContext, IfrsBenefitsStaffLoan entity)
        {
            return (from e in entityContext.Set<IfrsBenefitsStaffLoan>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsBenefitsStaffLoan> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<IfrsBenefitsStaffLoan>()
                        select e;
            query = query.OrderBy(a => a.ID).GroupBy(e => e.AccountNo).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override IfrsBenefitsStaffLoan GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsBenefitsStaffLoan>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsBenefitsStaffLoan> GetIfrsBenefitsStaffLoanBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<IfrsBenefitsStaffLoan>()
                                 where searchParam.Contains(e.AccountNo)
                                 orderby e.AccountNo
                                 select new
                                 {
                                     e.ID,
                                     e.Refno,
                                     e.AccountNo,
                                     e.Startdate,
                                     e.ProductCategory,
                                     e.ProductCode,
                                     e.ProductName,
                                     e.PRINCIPALREPAYMENT,
                                     e.LoanAmount,
                                     e.Rate,
                                     e.Tenor,
                                     e.MaturityDate,
                                     e.TOTALOUTSTANDINGEXPOSURE,
                                     e.RUNDATE,
                                     e.INTERESTRECEIVABLES,
                                     e.CURRENCY,
                                     e.EXCHANGERATE,
                                     e.CUSTID,
                                     e.CONTRACTUALCASHFLOW,
                                     e.PRIMELENDINGRATE,
                                     e.USINGCONTRACTRATE,
                                     e.SIGNIFICANTTHRESHOLD,
                                     e.PASTPERIOD,
                                     e.OSPERIOD,
                                     e.StaffLoansBenefitFairValueLoss,
                                     e.FAIRVALUE,
                                     e.DaysToMaturity,
                                     e.BenefitPerDaytoMaturity,
                                     e.PRINCIPALOUTSTANDINGBAL,
                                     e.FacilityTenor,
                                     e.PL_impact,
                                     e.UNAMORTIZED
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

                    return new List<IfrsBenefitsStaffLoan>().Take(0).ToArray().OrderBy(c => c.AccountNo).ThenBy(c => c.AccountNo); ;
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsBenefitsStaffLoan>()
                                 where e.Refno == searchParam
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<IfrsBenefitsStaffLoan> GetIfrsBenefitsStaffLoans(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsBenefitsStaffLoan>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<IfrsBenefitsStaffLoan> ExportIfrsBenefitsStaffLoan(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsBenefitsStaffLoan>()
                                 select new
                                 {
                                     e.ID,
                                     e.Refno,
                                     e.AccountNo,
                                     e.Startdate,
                                     e.ProductCategory,
                                     e.ProductCode,
                                     e.ProductName,
                                     e.PRINCIPALREPAYMENT,
                                     e.LoanAmount,
                                     e.Rate,
                                     e.Tenor,
                                     e.MaturityDate,
                                     e.TOTALOUTSTANDINGEXPOSURE,
                                     e.RUNDATE,
                                     e.INTERESTRECEIVABLES,
                                     e.CURRENCY,
                                     e.EXCHANGERATE,
                                     e.CUSTID,
                                     e.CONTRACTUALCASHFLOW,
                                     e.PRIMELENDINGRATE,
                                     e.USINGCONTRACTRATE,
                                     e.SIGNIFICANTTHRESHOLD,
                                     e.PASTPERIOD,
                                     e.OSPERIOD,
                                     e.StaffLoansBenefitFairValueLoss,
                                     e.FAIRVALUE,
                                     e.DaysToMaturity,
                                     e.BenefitPerDaytoMaturity,
                                     e.PRINCIPALOUTSTANDINGBAL,
                                     e.FacilityTenor,
                                     e.PL_impact,
                                     e.UNAMORTIZED
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsBenefitsStaffLoan>().Take(0).ToArray().OrderBy(c => c.AccountNo).ThenBy(c => c.AccountNo);

                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsBenefitsStaffLoan>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
