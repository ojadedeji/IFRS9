using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IImpairmentOverrideRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ImpairmentOverrideRepository : DataRepositoryBase<ImpairmentOverride>, IImpairmentOverrideRepository
    {

        protected override ImpairmentOverride AddEntity(BasicContext entityContext, ImpairmentOverride entity)
        {
            return entityContext.Set<ImpairmentOverride>().Add(entity);
        }

        protected override ImpairmentOverride UpdateEntity(BasicContext entityContext, ImpairmentOverride entity)
        {
            return (from e in entityContext.Set<ImpairmentOverride>() 
                    where e.ImpairmentOverrideId == entity.ImpairmentOverrideId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ImpairmentOverride> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<ImpairmentOverride>()
                   select e;
        }

        protected override ImpairmentOverride GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ImpairmentOverride>()
                         where e.ImpairmentOverrideId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
 
    }
}
