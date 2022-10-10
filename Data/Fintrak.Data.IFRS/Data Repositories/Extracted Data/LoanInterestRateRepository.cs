using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILoanInterestRateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoanInterestRateRepository : DataRepositoryBase<LoanInterestRate>, ILoanInterestRateRepository
    {
        protected override LoanInterestRate AddEntity(IFRSContext entityContext, LoanInterestRate entity)
        {
            return entityContext.Set<LoanInterestRate>().Add(entity);
        }

        protected override LoanInterestRate UpdateEntity(IFRSContext entityContext, LoanInterestRate entity)
        {
            return (from e in entityContext.Set<LoanInterestRate>()
                    where e.LoanInterestRate_Id == entity.LoanInterestRate_Id
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<LoanInterestRate> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LoanInterestRate>()
                   select e;
        }

        protected override LoanInterestRate GetEntity(IFRSContext entityContext, int LoanInterestRate_Id)
        {
            var query = (from e in entityContext.Set<LoanInterestRate>()
                         where e.LoanInterestRate_Id == LoanInterestRate_Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        //public IEnumerable<LoanInterestRate> GetLoanInterestRateByRefNo(string RefNo)
        //{
        //    using(IFRSContext entityContext = new IFRSContext())
        //    {
        //    var query = (from e in entityContext.Set<LoanInterestRate>()
        //                 where e.RefNo.Contains(RefNo)
        //                 select e);

        //    return query.ToArray();
        //    }
        //}
       
    }
}