using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IBondEclComputationResultRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BondEclComputationResultRepository : DataRepositoryBase<BondEclComputationResult>, IBondEclComputationResultRepository
    {
        protected override BondEclComputationResult AddEntity(IFRSContext entityContext, BondEclComputationResult entity)
        {
            return entityContext.Set<BondEclComputationResult>().Add(entity);
        }

        protected override BondEclComputationResult UpdateEntity(IFRSContext entityContext, BondEclComputationResult entity)
        {
            return (from e in entityContext.Set<BondEclComputationResult>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<BondEclComputationResult> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<BondEclComputationResult>()
                   select e;
        }

        protected override BondEclComputationResult GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BondEclComputationResult>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}