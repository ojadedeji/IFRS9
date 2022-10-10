using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.SystemCore.Entities;
using Fintrak.Data.SystemCore.Contracts;

namespace Fintrak.Data.SystemCore
{
    [Export(typeof(ISolutionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SolutionRepository : DataRepositoryBase<Solution>, ISolutionRepository
    {
        protected override Solution AddEntity(SystemCoreContext entityContext, Solution entity)
        {
            return entityContext.Set<Solution>().Add(entity);
        }

        protected override Solution UpdateEntity(SystemCoreContext entityContext, Solution entity)
        {
            return (from e in entityContext.Set<Solution>()
                    where e.SolutionId == entity.SolutionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Solution> GetEntities(SystemCoreContext entityContext)
        {
            return from e in entityContext.Set<Solution>()
                   select e;
        }

        protected override Solution GetEntity(SystemCoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Solution>()
                         where e.SolutionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
