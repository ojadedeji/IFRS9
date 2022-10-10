using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.SystemCore.Entities;

namespace Fintrak.Data.SystemCore
{
    [Export(typeof(IGeneralRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GeneralRepository : DataRepositoryBase<General>, IGeneralRepository
    {
        protected override General AddEntity(SystemCoreContext entityContext, General entity)
        {
            return entityContext.Set<General>().Add(entity);
        }

        protected override General UpdateEntity(SystemCoreContext entityContext, General entity)
        {
            return (from e in entityContext.Set<General>()
                    where e.GeneralId == entity.GeneralId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<General> GetEntities(SystemCoreContext entityContext)
        {
            return from e in entityContext.Set<General>()
                   select e;
        }

        protected override General GetEntity(SystemCoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<General>()
                         where e.GeneralId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
