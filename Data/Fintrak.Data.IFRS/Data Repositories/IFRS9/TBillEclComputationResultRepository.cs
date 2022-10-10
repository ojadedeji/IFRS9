using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ITBillEclComputationResultRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TBillEclComputationResultRepository : DataRepositoryBase<TBillEclComputationResult>, ITBillEclComputationResultRepository
    {
        protected override TBillEclComputationResult AddEntity(IFRSContext entityContext, TBillEclComputationResult entity)
        {
            return entityContext.Set<TBillEclComputationResult>().Add(entity);
        }

        protected override TBillEclComputationResult UpdateEntity(IFRSContext entityContext, TBillEclComputationResult entity)
        {
            return (from e in entityContext.Set<TBillEclComputationResult>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<TBillEclComputationResult> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<TBillEclComputationResult>()
                   select e;
        }

        protected override TBillEclComputationResult GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TBillEclComputationResult>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}