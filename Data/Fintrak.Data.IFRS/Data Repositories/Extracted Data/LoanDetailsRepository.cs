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
    [Export(typeof(IRawLoanDetailsRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RawLoanDetailsRepository : DataRepositoryBase<RawLoanDetails>, IRawLoanDetailsRepository
    {
        protected override RawLoanDetails AddEntity(IFRSContext entityContext, RawLoanDetails entity)
        {
            return entityContext.Set<RawLoanDetails>().Add(entity);
        }

        protected override RawLoanDetails UpdateEntity(IFRSContext entityContext, RawLoanDetails entity)
        {
            return (from e in entityContext.Set<RawLoanDetails>()
                    where e.LoanDetailId == entity.LoanDetailId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<RawLoanDetails> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<RawLoanDetails>().Take(200)
                   select e;
        }

        protected override RawLoanDetails GetEntity(IFRSContext entityContext, int loanDetailId)
        {
            var query = (from e in entityContext.Set<RawLoanDetails>()
                         where e.LoanDetailId == loanDetailId
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<RawLoanDetails> GetLoanDetailsBySearch(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<RawLoanDetails>()
                             where e.RefNo == searchParam || e.AccountNo == searchParam || e.CUSTOMER_NAME == searchParam
                             select e);

                return query.ToArray();
            }
        }

        public string[] GetDistinctLoanDetailsRefNos(int count)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = count == 0 
                    ? (from e in entityContext.Set<RawLoanDetails>() select e.RefNo).Distinct()
                    : (from e in entityContext.Set<RawLoanDetails>() select e.RefNo).Take(count).Distinct();

                return query.ToArray();
            }
        }


        public IEnumerable<RawLoanDetails> GetLoanDetails(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<RawLoanDetails>()
                                 select new
                                 {
                                     CustomerID = e.AccountNo,
                                     ReferenceNo = e.RefNo,
                                     e.ProductCode,
                                     e.ProductName,
                                     e.Sector,
                                     e.Currency,
                                     e.ExchangeRate,
                                     e.Amount,
                                     e.Rate,
                                     e.PrincipalOutstandingBal,
                                     e.ODLimit,
                                     e.PastDueAmount,
                                     e.FirstRepaymentdate,
                                     e.MaturityDate,
                                     e.CollateralValue,
                                     e.Classification,
                                     e.SubClassification,
                                     e.Interest_Receiv_Pay_UnEarn
                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<RawLoanDetails>().Take(defaultCount).ToArray();

                }
                else
                {
                    var query = (from e in entityContext.Set<RawLoanDetails>().Take(defaultCount)
                                 select e);

                    return query.ToArray();
                }
            }
        }

    }
}