using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.SystemCore.Entities;

namespace Fintrak.Data.SystemCore
{
    [Export(typeof(IUserSessionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserSessionRepository : DataRepositoryBase<UserSession>, IUserSessionRepository
    {

        protected override UserSession AddEntity(SystemCoreContext entityContext,UserSession entity)
        {
            return entityContext.Set<UserSession>().Add(entity);
        }

        protected override UserSession UpdateEntity(SystemCoreContext entityContext,UserSession entity)
        {
            return (from e in entityContext.Set<UserSession>() 
                    where e.UserSessionId == entity.UserSessionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<UserSession> GetEntities(SystemCoreContext entityContext)
        {
            return from e in entityContext.Set<UserSession>()
                   select e;
        }

        protected override UserSession GetEntity(SystemCoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<UserSession>()
                         where e.UserSessionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<UserSessionInfo> GetUserSessions()
        {
            using (SystemCoreContext entityContext = new SystemCoreContext())
            {
                var query = from a in entityContext.UserSessionSet
                            join b in entityContext.UserSetupSet on a.UserId equals b.LoginID
                            join c in entityContext.DatabaseSet on a.DatabaseId equals c.DatabaseId
                            select new UserSessionInfo()
                            {
                                UserSession = a,
                                UserSetup = b,
                                Database = c
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
