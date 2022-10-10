using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fintrak.Shared.Common.PlaceHolders;

namespace Fintrak.Shared.SystemCore.Framework
{
    public static class OpexModuleDefinition
    {
        public const string SOLUTION_NAME = "BUDGET";
        public const string SOLUTION_ALIAS = "Budget";

        public const string MODULE_NAME = "FIN_BUDGET_OPEX";
        public const string MODULE_ALIAS = "Fintrak Budget Opex";

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
            var staff = new MenuPlaceHolder() { Name = "OPEX", Alias = "Opex", Action = "", ActionUrl = "", ImageUrl = "opex_image", Parent = root.Name, ParentModule = "FIN_BUDGET_CORE" };
            list.Add(staff);

            list.Add(new MenuPlaceHolder() { Name = "OPEX_CATEGORY", Alias = "Opex Categories", Action = "OPEX_CATEGORY", ActionUrl = "budget-opexcategory-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "OPEX_ITEM", Alias = "Opex Items", Action = "OPEX_ITEM", ActionUrl = "budget-opexitem-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "OPEX_VOLUME_BASED_SETUP", Alias = "Volume Based Setup", Action = "OPEX_VOLUME_BASED_SETUP", ActionUrl = "budget-opexvolumebasedsetup-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "OPEX_VOLUME_BASED_RATE", Alias = "Volume Based Rates", Action = "OPEX_VOLUME_BASED_RATE", ActionUrl = "budget-opexvolumebasedrate-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "OPEX_ENTRIES", Alias = "Opex Entries", Action = "OPEX_ENTRIES", ActionUrl = "budget-opexentry-list", ImageUrl = "action_image", Parent = staff.Name });

            return list;
        }

        public static List<MenuRolePlaceHolder> GetMenuRoles()
        {
            var list = new List<MenuRolePlaceHolder>();

            list.Add(new MenuRolePlaceHolder() { MenuName = "OPEX", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "OPEX", RoleName = GROUP_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "OPEX", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "OPEX_CATEGORY", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "OPEX_CATEGORY", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "OPEX_ITEM", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "OPEX_ITEM", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "OPEX_ITEM", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "OPEX_ITEM", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "OPEX_VOLUME_BASED_SETUP", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "OPEX_VOLUME_BASED_SETUP", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "OPEX_VOLUME_BASED_RATE", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "OPEX_VOLUME_BASED_RATE", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "OPEX_ENTRIES", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "OPEX_ENTRIES", RoleName = GROUP_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "OPEX_ENTRIES", RoleName = GROUP_USER });
           
            return list;
        }
    }
}
