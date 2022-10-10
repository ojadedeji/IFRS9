using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IMarginalPDDistributionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MarginalPDDistributionRepository : DataRepositoryBase<MarginalPDDistribution>, IMarginalPDDistributionRepository
    {
        protected override MarginalPDDistribution AddEntity(IFRSContext entityContext, MarginalPDDistribution entity)
        {
            return entityContext.Set<MarginalPDDistribution>().Add(entity);
        }

        protected override MarginalPDDistribution UpdateEntity(IFRSContext entityContext, MarginalPDDistribution entity)
        {
            return (from e in entityContext.Set<MarginalPDDistribution>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<MarginalPDDistribution> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MarginalPDDistribution>()
                   select e;
        }

        protected override MarginalPDDistribution GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MarginalPDDistribution>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}