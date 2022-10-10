using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IImpairmentOverrideRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ImpairmentOverrideRepository : DataRepositoryBase<ImpairmentOverride>, IImpairmentOverrideRepository
    {

        protected override ImpairmentOverride AddEntity(IFRSContext entityContext, ImpairmentOverride entity)
        {
            return entityContext.Set<ImpairmentOverride>().Add(entity);
        }

        protected override ImpairmentOverride UpdateEntity(IFRSContext entityContext, ImpairmentOverride entity)
        {
            return (from e in entityContext.Set<ImpairmentOverride>() 
                    where e.ImpairmentOverrideId == entity.ImpairmentOverrideId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ImpairmentOverride> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<ImpairmentOverride>()
                   select e;
        }

        protected override ImpairmentOverride GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ImpairmentOverride>()
                         where e.ImpairmentOverrideId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
 
    }
}
