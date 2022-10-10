using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.SystemCore.Entities;
using Fintrak.Data.SystemCore.Contracts;

namespace Fintrak.Data.SystemCore
{
    [Export(typeof(IMenuRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MenuRepository : DataRepositoryBase<Menu>, IMenuRepository
    {
        protected override Menu AddEntity(SystemCoreContext entityContext, Menu entity)
        {
            return entityContext.Set<Menu>().Add(entity);
        }

        protected override Menu UpdateEntity(SystemCoreContext entityContext, Menu entity)
        {
            return (from e in entityContext.Set<Menu>()
                    where e.MenuId == entity.MenuId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Menu> GetEntities(SystemCoreContext entityContext)
        {
            return from e in entityContext.Set<Menu>()
                   select e;
        }

        protected override Menu GetEntity(SystemCoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Menu>()
                         where e.MenuId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MenuInfo> GetMenuInfoByLoginID(string loginID)
        {
            using (SystemCoreContext entityContext = new SystemCoreContext())
            {
                var roleQuery = from a in entityContext.MenuRoleSet 
                                join b in entityContext.UserRoleSet on a.RoleId equals b.RoleId 
                                join c in entityContext.UserSetupSet on b.UserSetupId equals c.UserSetupId
                                where c.LoginID == loginID
                                select a;

                var menuIds = roleQuery.Select(c => c.MenuId).Distinct().ToArray();

                var query = from a in entityContext.MenuSet
                            join b in entityContext.ModuleSet on a.ModuleId equals b.ModuleId
                            join c in entityContext.MenuSet on a.ParentId equals c.MenuId into parents
                            from pt in parents.DefaultIfEmpty()
                           
                            where b.Active && a.Active && menuIds.Contains(a.MenuId)
                            select new MenuInfo()
                            {
                                Menu = a,
                                Module = b,
                                Parent = pt
                            };

                return query.ToFullyLoaded();
            }
        }

        // join b in entityContext.TeamSet on a.ParentCode equals b.Code into parents
        // from pt in parents.Where(c=>(a.Year==c.Year)).DefaultIfEmpty()

        public IEnumerable<MenuInfo> GetMenuInfo()
        {
            using (SystemCoreContext entityContext = new SystemCoreContext())
            {
                var query = from a in entityContext.MenuSet
                            join b in entityContext.ModuleSet on a.ModuleId equals b.ModuleId
                            join c in entityContext.MenuSet on a.ParentId equals c.MenuId into parents
                            from pt in parents.DefaultIfEmpty()
                            select new MenuInfo()
                            {
                                Menu = a,
                                Module = b,
                                Parent = pt
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}






//using System;
//using System.Collections.Generic;
//using System.ComponentModel.Composition;
//using System.Linq;
//using Fintrak.Shared.Common.Extensions;
//using Fintrak.Shared.Basic.Entities;
//using Fintrak.Data.Basic.Contracts;



//using System;
//using System.Collections.Generic;
//using System.ComponentModel.Composition;
//using System.Linq;
//using Fintrak.Shared.SystemCore.Entities;
//using Fintrak.Data.SystemCore.Contracts;



