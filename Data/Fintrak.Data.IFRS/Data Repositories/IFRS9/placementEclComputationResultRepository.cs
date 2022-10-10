using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IPlacementEclComputationResultRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PlacementEclComputationResultRepository : DataRepositoryBase<PlacementEclComputationResult>, IPlacementEclComputationResultRepository
    {
        protected override PlacementEclComputationResult AddEntity(IFRSContext entityContext, PlacementEclComputationResult entity)
        {
            return entityContext.Set<PlacementEclComputationResult>().Add(entity);
        }

        protected override PlacementEclComputationResult UpdateEntity(IFRSContext entityContext, PlacementEclComputationResult entity)
        {
            return (from e in entityContext.Set<PlacementEclComputationResult>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<PlacementEclComputationResult> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<PlacementEclComputationResult>()
                   select e;
        }

        protected override PlacementEclComputationResult GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PlacementEclComputationResult>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}