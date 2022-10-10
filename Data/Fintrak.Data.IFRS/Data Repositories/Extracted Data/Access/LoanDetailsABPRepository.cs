using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IRawLoanDetailsABPRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RawLoanDetailsABPRepository : DataRepositoryBase<RawLoanDetailsABP>, IRawLoanDetailsABPRepository
    {
        protected override RawLoanDetailsABP AddEntity(IFRSContext entityContext, RawLoanDetailsABP entity)
        {
            return entityContext.Set<RawLoanDetailsABP>().Add(entity);
        }

        protected override RawLoanDetailsABP UpdateEntity(IFRSContext entityContext, RawLoanDetailsABP entity)
        {
            return (from e in entityContext.Set<RawLoanDetailsABP>()
                    where e.LoanDetailId == entity.LoanDetailId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<RawLoanDetailsABP> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<RawLoanDetailsABP>()
                   select e;
        }

        protected override RawLoanDetailsABP GetEntity(IFRSContext entityContext, int loanDetailId)
        {
            var query = (from e in entityContext.Set<RawLoanDetailsABP>()
                         where e.LoanDetailId == loanDetailId
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

      
    }
}