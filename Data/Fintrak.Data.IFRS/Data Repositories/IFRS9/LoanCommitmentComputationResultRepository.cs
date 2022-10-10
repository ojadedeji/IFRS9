using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILoanCommitmentComputationResultRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoanCommitmentComputationResultRepository : DataRepositoryBase<LoanCommitmentComputationResult>, ILoanCommitmentComputationResultRepository
    {
        protected override LoanCommitmentComputationResult AddEntity(IFRSContext entityContext, LoanCommitmentComputationResult entity)
        {
            return entityContext.Set<LoanCommitmentComputationResult>().Add(entity);
        }

        protected override LoanCommitmentComputationResult UpdateEntity(IFRSContext entityContext, LoanCommitmentComputationResult entity)
        {
            return (from e in entityContext.Set<LoanCommitmentComputationResult>()
                    where e.LoanCommitmentComputationResult_Id == entity.LoanCommitmentComputationResult_Id
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<LoanCommitmentComputationResult> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LoanCommitmentComputationResult>()
                   select e;
        }

        protected override LoanCommitmentComputationResult GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LoanCommitmentComputationResult>()
                         where e.LoanCommitmentComputationResult_Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}