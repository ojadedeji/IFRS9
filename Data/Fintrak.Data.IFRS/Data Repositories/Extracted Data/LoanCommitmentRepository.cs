using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILoanCommitmentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoanCommitmentRepository : DataRepositoryBase<LoanCommitment>, ILoanCommitmentRepository
    {
        protected override LoanCommitment AddEntity(IFRSContext entityContext, LoanCommitment entity)
        {
            return entityContext.Set<LoanCommitment>().Add(entity);
        }

        protected override LoanCommitment UpdateEntity(IFRSContext entityContext, LoanCommitment entity)
        {
            return (from e in entityContext.Set<LoanCommitment>()
                    where e.LoanCommitmentId == entity.LoanCommitmentId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LoanCommitment> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LoanCommitment>()
                   select e;
        }

        protected override LoanCommitment GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LoanCommitment>()
                         where e.LoanCommitmentId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}