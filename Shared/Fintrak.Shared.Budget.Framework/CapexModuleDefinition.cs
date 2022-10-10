using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fintrak.Shared.Common.PlaceHolders;

namespace Fintrak.Shared.SystemCore.Framework
{
    public static class CapexModuleDefinition
    {
        public const string SOLUTION_NAME = "BUDGET";
        public const string SOLUTION_ALIAS = "Budget";

        public const string MODULE_NAME = "FIN_BUDGET_CAPEX";
        public const string MODULE_ALIAS = "Fintrak Budget Capex";

        public const string GROUP_ADMINISTRATOR = "Administrator";
        public const string GROUP_BUSINESS = "Business";
        public const string GROUP_READONLY = "ReadOnly";
        public const string GROUP_USER = "User";

        public static List<RolePlaceHolder> GetRoles()
        {
            var list = new List<RolePlaceHolder>();

            return list;
        }

        public static List<MenuPlaceHolder> GetMenus()
        {
            var list = new List<MenuPlaceHolder>();

            var root = new MenuPlaceHolder() { Name = "BUDGET", Alias = "Budget", Action = "", ActionUrl = "", ImageUrl = "budget_image", Parent = "", ParentModule = "FIN_BUDGET_CORE" };
            //list.Add(root);

            //Staff
            var staff = new MenuPlaceHolder() { Name = "CAPEX", Alias = "Capex", Action = "", ActionUrl = "", ImageUrl = "capex_image", Parent = root.Name, ParentModule = "FIN_BUDGET_CORE" };
            list.Add(staff);

            list.Add(new MenuPlaceHolder() { Name = "CAPEX_CATEGORY", Alias = "Capex Categories", Action = "CAPEX_CATEGORY", ActionUrl = "budget-capexcategory-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "CAPEX_ITEM", Alias = "Capex Items", Action = "CAPEX_ITEM", ActionUrl = "budget-capexitem-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "DEPRECIATION_RATE", Alias = "Depreciation Rates", Action = "DEPRECIATION_RATE", ActionUrl = "budget-depreciationrate-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "CAPEX_ENTRIES", Alias = "Capex Entries", Action = "CAPEX_ENTRIES", ActionUrl = "budget-capexentry-list", ImageUrl = "action_image", Parent = staff.Name });

            return list;
        }

        public static List<MenuRolePlaceHolder> GetMenuRoles()
        {
            var list = new List<MenuRolePlaceHolder>();

            list.Add(new MenuRolePlaceHolder() { MenuName = "CAPEX", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "CAPEX", RoleName = GROUP_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "CAPEX", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "CAPEX_CATEGORY", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "CAPEX_CATEGORY", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "CAPEX_ITEM", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "CAPEX_ITEM", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "CAPEX_ITEM", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "CAPEX_ITEM", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "CAPEX_ENTRIES", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "CAPEX_ENTRIES", RoleName = GROUP_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "CAPEX_ENTRIES", RoleName = GROUP_USER });
           
            return list;
        }
    }
}
