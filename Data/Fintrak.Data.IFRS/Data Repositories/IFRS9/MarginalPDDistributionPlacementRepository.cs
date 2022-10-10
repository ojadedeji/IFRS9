using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IMarginalPDDistributionPlacementRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MarginalPDDistributionPlacementRepository : DataRepositoryBase<MarginalPDDistributionPlacement>, IMarginalPDDistributionPlacementRepository
    {
        protected override MarginalPDDistributionPlacement AddEntity(IFRSContext entityContext, MarginalPDDistributionPlacement entity)
        {
            return entityContext.Set<MarginalPDDistributionPlacement>().Add(entity);
        }

        protected override MarginalPDDistributionPlacement UpdateEntity(IFRSContext entityContext, MarginalPDDistributionPlacement entity)
        {
            return (from e in entityContext.Set<MarginalPDDistributionPlacement>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<MarginalPDDistributionPlacement> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MarginalPDDistributionPlacement>()
                   select e;
        }

        protected override MarginalPDDistributionPlacement GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MarginalPDDistributionPlacement>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}