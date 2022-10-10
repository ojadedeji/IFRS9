using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.SystemCore.Entities;
using Fintrak.Data.SystemCore.Contracts;

namespace Fintrak.Data.SystemCore
{
    [Export(typeof(IUserRoleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserRoleRepository : DataRepositoryBase<UserRole>, IUserRoleRepository
    {
        protected override UserRole AddEntity(SystemCoreContext entityContext, UserRole entity)
        {
            return entityContext.Set<UserRole>().Add(entity);
        }

        protected override UserRole UpdateEntity(SystemCoreContext entityContext, UserRole entity)
        {
            return (from e in entityContext.Set<UserRole>()
                    where e.UserRoleId == entity.UserRoleId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<UserRole> GetEntities(SystemCoreContext entityContext)
        {
            return from e in entityContext.Set<UserRole>()
                   select e;
        }

        protected override UserRole GetEntity(SystemCoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<UserRole>()
                         where e.UserRoleId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<UserRoleInfo> GetUserRoleInfo(string solutionName, string loginID, List<string> roleNames)
        {
            using (SystemCoreContext entityContext = new SystemCoreContext())
            {
                var query = from a in entityContext.UserRoleSet
                            join b in entityContext.UserSetupSet on a.UserSetupId equals b.UserSetupId
                            join c in entityContext.RoleSet on a.RoleId equals c.RoleId
                            join d in entityContext.SolutionSet on c.SolutionId equals d.SolutionId
                            where d.Name == solutionName && b.LoginID == loginID && roleNames.Contains(c.Name)
                            select new UserRoleInfo()
                            {
                                UserRole = a,
                                UserSetup = b,
                                Role = c,
                                Solution = d
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<UserRoleInfo> GetUserRoleInfo()
        {
            using (SystemCoreContext entityContext = new SystemCoreContext())
            {
                var query = from a in entityContext.UserRoleSet
                            join b in entityContext.UserSetupSet on a.UserSetupId equals b.UserSetupId
                            join c in entityContext.RoleSet on a.RoleId equals c.RoleId
                            join d in entityContext.SolutionSet on c.SolutionId equals d.SolutionId
                            select new UserRoleInfo()
                            {
                                UserRole = a,
                                UserSetup = b,
                                Role = c,
                                Solution = d
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
