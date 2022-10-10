using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core
{
    [Export(typeof(IDefaultUserRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DefaultUserRepository : DataRepositoryBase<DefaultUser>, IDefaultUserRepository
    {
        protected override DefaultUser AddEntity(CoreContext entityContext, DefaultUser entity)
        {
            return entityContext.Set<DefaultUser>().Add(entity);
        }

        protected override DefaultUser UpdateEntity(CoreContext entityContext, DefaultUser entity)
        {
            return (from e in entityContext.Set<DefaultUser>()
                    where e.DefaultUserId == entity.DefaultUserId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<DefaultUser> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<DefaultUser>()
                   select e;
        }

        protected override DefaultUser GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<DefaultUser>()
                         where e.DefaultUserId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<DefaultUserInfo> GetDefaultUseres()
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.DefaultUserSet

                            select new DefaultUserInfo()
                            {
                                DefaultUser = a
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
