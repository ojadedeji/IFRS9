using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IRatiosRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RatiosRepository : DataRepositoryBase<Ratios>, IRatiosRepository
    {

        protected override Ratios AddEntity(MPRContext entityContext, Ratios entity)
        {
            return entityContext.Set<Ratios>().Add(entity);
        }

        protected override Ratios UpdateEntity(MPRContext entityContext, Ratios entity)
        {
            return (from e in entityContext.Set<Ratios>()
                    where e.RatiosId == entity.RatiosId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Ratios> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<Ratios>()
                   select e;
        }

        protected override Ratios GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Ratios>()
                         where e.RatiosId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
