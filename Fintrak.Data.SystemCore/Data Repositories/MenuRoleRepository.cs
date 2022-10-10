using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.SystemCore.Entities;
using Fintrak.Data.SystemCore.Contracts;

namespace Fintrak.Data.SystemCore
{
    [Export(typeof(IMenuRoleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MenuRoleRepository : DataRepositoryBase<MenuRole>, IMenuRoleRepository
    {
        protected override MenuRole AddEntity(SystemCoreContext entityContext, MenuRole entity)
        {
            return entityContext.Set<MenuRole>().Add(entity);
        }

        protected override MenuRole UpdateEntity(SystemCoreContext entityContext, MenuRole entity)
        {
            return (from e in entityContext.Set<MenuRole>()
                    where e.MenuRoleId == entity.MenuRoleId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MenuRole> GetEntities(SystemCoreContext entityContext)
        {
            return from e in entityContext.Set<MenuRole>()
                   select e;
        }

        protected override MenuRole GetEntity(SystemCoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MenuRole>()
                         where e.MenuRoleId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MenuRoleInfo> GetMenuRoleInfo()
        {
            using (SystemCoreContext entityContext = new SystemCoreContext())
            {
                var query = from a in entityContext.MenuRoleSet
                            join b in entityContext.MenuSet on a.MenuId equals b.MenuId
                            join c in entityContext.RoleSet on a.RoleId equals c.RoleId
                            join d in entityContext.SolutionSet on c.SolutionId equals d.SolutionId
                            select new MenuRoleInfo()
                            {
                                MenuRole = a,
                                Menu = b,
                                Role = c,
                                Solution = d
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<MenuRoleInfo> GetMenuRoleInfo(string loginID)
        {
            using (SystemCoreContext entityContext = new SystemCoreContext())
            {
                var query = from a in entityContext.MenuRoleSet
                            join b in entityContext.MenuSet on a.MenuId equals b.MenuId
                            join c in entityContext.RoleSet on a.RoleId equals c.RoleId
                            join d in entityContext.UserRoleSet on a.RoleId equals d.RoleId
                            join e in entityContext.UserSetupSet on d.UserSetupId equals e.UserSetupId
                            join f in entityContext.SolutionSet on c.SolutionId equals f.SolutionId
                            where e.LoginID == loginID
                            select new MenuRoleInfo()
                            {
                                MenuRole = a,
                                Menu = b,
                                Role = c,
                                Solution = f
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
