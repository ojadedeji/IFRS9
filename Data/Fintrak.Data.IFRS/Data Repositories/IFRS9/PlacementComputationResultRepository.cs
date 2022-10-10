using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IPlacementComputationResultRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PlacementComputationResultRepository : DataRepositoryBase<PlacementComputationResult>, IPlacementComputationResultRepository
    {
        protected override PlacementComputationResult AddEntity(IFRSContext entityContext, PlacementComputationResult entity)
        {
            return entityContext.Set<PlacementComputationResult>().Add(entity);
        }

        protected override PlacementComputationResult UpdateEntity(IFRSContext entityContext, PlacementComputationResult entity)
        {
            return (from e in entityContext.Set<PlacementComputationResult>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<PlacementComputationResult> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<PlacementComputationResult>()
                   select e;
        }

        protected override PlacementComputationResult GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PlacementComputationResult>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}