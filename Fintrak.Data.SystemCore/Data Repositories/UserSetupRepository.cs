using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.SystemCore.Entities;
using Fintrak.Data.SystemCore.Contracts;

namespace Fintrak.Data.SystemCore
{
    [Export(typeof(IUserSetupRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserSetupRepository : DataRepositoryBase<UserSetup>, IUserSetupRepository
    {
        protected override UserSetup AddEntity(SystemCoreContext entityContext, UserSetup entity)
        {
            return entityContext.Set<UserSetup>().Add(entity);
        }

        protected override UserSetup UpdateEntity(SystemCoreContext entityContext, UserSetup entity)
        {
            return (from e in entityContext.Set<UserSetup>()
                    where e.UserSetupId == entity.UserSetupId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<UserSetup> GetEntities(SystemCoreContext entityContext)
        {
            return from e in entityContext.Set<UserSetup>()
                   select e;
        }

        protected override UserSetup GetEntity(SystemCoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<UserSetup>()
                         where e.UserSetupId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public UserSetup GetByLoginID(string loginID)
        {
            using (SystemCoreContext entityContext = new SystemCoreContext())
            {
                var query = from a in entityContext.UserSetupSet
                            where a.LoginID == loginID
                            select a;

                return query.FirstOrDefault();
            }
        }
    }
}
