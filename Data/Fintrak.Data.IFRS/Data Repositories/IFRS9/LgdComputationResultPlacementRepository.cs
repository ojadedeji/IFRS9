using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILgdComputationResultPlacementRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LgdComputationResultPlacementRepository : DataRepositoryBase<LgdComputationResultPlacement>, ILgdComputationResultPlacementRepository
    {
        protected override LgdComputationResultPlacement AddEntity(IFRSContext entityContext, LgdComputationResultPlacement entity)
        {
            return entityContext.Set<LgdComputationResultPlacement>().Add(entity);
        }

        protected override LgdComputationResultPlacement UpdateEntity(IFRSContext entityContext, LgdComputationResultPlacement entity)
        {
            return (from e in entityContext.Set<LgdComputationResultPlacement>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<LgdComputationResultPlacement> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LgdComputationResultPlacement>()
                   select e;
        }

        protected override LgdComputationResultPlacement GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LgdComputationResultPlacement>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}