using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.SystemCore.Entities;
using Fintrak.Data.SystemCore.Contracts;

namespace Fintrak.Data.SystemCore
{
    [Export(typeof(IRoleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RoleRepository : DataRepositoryBase<Role>, IRoleRepository
    {
        protected override Role AddEntity(SystemCoreContext entityContext, Role entity)
        {
            return entityContext.Set<Role>().Add(entity);
        }

        protected override Role UpdateEntity(SystemCoreContext entityContext, Role entity)
        {
            return (from e in entityContext.Set<Role>()
                    where e.RoleId == entity.RoleId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Role> GetEntities(SystemCoreContext entityContext)
        {
            return from e in entityContext.Set<Role>()
                   select e;
        }

        protected override Role GetEntity(SystemCoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Role>()
                         where e.RoleId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<RoleInfo> GetRoles()
        {
            using (SystemCoreContext entityContext = new SystemCoreContext())
            {
                var query = from a in entityContext.RoleSet
                            join b in entityContext.SolutionSet on a.SolutionId equals b.SolutionId
                            
                            select new RoleInfo()
                            {
                                Role = a,
                                Solution = b
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
