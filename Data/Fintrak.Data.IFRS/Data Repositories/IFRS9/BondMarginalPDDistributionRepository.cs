using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IBondMarginalPDDistributionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BondMarginalPDDistributionRepository : DataRepositoryBase<BondMarginalPDDistribution>, IBondMarginalPDDistributionRepository
    {
        protected override BondMarginalPDDistribution AddEntity(IFRSContext entityContext, BondMarginalPDDistribution entity)
        {
            return entityContext.Set<BondMarginalPDDistribution>().Add(entity);
        }

        protected override BondMarginalPDDistribution UpdateEntity(IFRSContext entityContext, BondMarginalPDDistribution entity)
        {
            return (from e in entityContext.Set<BondMarginalPDDistribution>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<BondMarginalPDDistribution> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<BondMarginalPDDistribution>()
                   select e;
        }

        protected override BondMarginalPDDistribution GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BondMarginalPDDistribution>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}