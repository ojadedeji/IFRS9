using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fintrak.Shared.Common.PlaceHolders;

namespace Fintrak.Shared.SystemCore.Framework
{
    public static class CoreModuleDefinition
    {
        public const string SOLUTION_NAME = "BUDGET";
        public const string SOLUTION_ALIAS = "Budget";

        public const string MODULE_NAME = "FIN_BUDGET_CORE";
        public const string MODULE_ALIAS = "Fintrak Budget Core";

        public const string GROUP_ADMINISTRATOR = "Administrator";
        public const string GROUP_BUSINESS = "Business";
        public const string GROUP_READONLY = "ReadOnly";
        public const string GROUP_USER = "User";

        public static List<RolePlaceHolder> GetRoles()
        {
            var list = new List<RolePlaceHolder>();

            list.Add(new RolePlaceHolder() { Name = GROUP_ADMINISTRATOR, Description = "For budget solution unlimited users" });
            list.Add(new RolePlaceHolder() { Name = GROUP_READONLY, Description = "For budget solution read only users" });
            list.Add(new RolePlaceHolder() { Name = GROUP_BUSINESS, Description = "For budget solution semi-limited users" });
            list.Add(new RolePlaceHolder() { Name = GROUP_USER, Description = "For budget solution limited users" });

            return list;
        }

        public static List<MenuPlaceHolder> GetMenus()
        {
            var list = new List<MenuPlaceHolder>();

            var root = new MenuPlaceHolder() { Name = "BUDGET", Alias = "Budget", Action = "", ActionUrl = "", ImageUrl = "budget_image", Parent = "" };
            list.Add(root);

            var settings = new MenuPlaceHolder() { Name = "SETTINGS", Alias = "Settings", Action = "", ActionUrl = "", ImageUrl = "settings_image", Parent = root.Name };
            list.Add(settings);

            list.Add(new MenuPlaceHolder() { Name = "OPERATION", Alias = "Operations", Action = "OPERATION", ActionUrl = "budget-operation-list", ImageUrl = "action_image", Parent = settings.Name });

            list.Add(new MenuPlaceHolder() { Name = "BUDGETING_LEVELS", Alias = "Budgeting Levels", Action = "BUDGETING_LEVELS", ActionUrl = "budget-budgetinglevel-list", ImageUrl = "action_image", Parent = settings.Name });

            list.Add(new MenuPlaceHolder() { Name = "POLICY_LEVELS", Alias = "Policy Levels", Action = "POLICY_LEVELS", ActionUrl = "budget-policylevel-list", ImageUrl = "action_image", Parent = settings.Name });

            list.Add(new MenuPlaceHolder() { Name = "MODIFICATION_LEVELS", Alias = "Modification Levels", Action = "MODIFICATION_LEVELS", ActionUrl = "budget-modificationlevel-list", ImageUrl = "action_image", Parent = settings.Name });

            list.Add(new MenuPlaceHolder() { Name = "SECONDARY_LOCK_LEVELS", Alias = "Secondary Lock Levels", Action = "SECONDARY_LOCK_LEVELS", ActionUrl = "budget-secondarylocklevel-list", ImageUrl = "action_image", Parent = settings.Name });

            list.Add(new MenuPlaceHolder() { Name = "SECONDARY_LOCKS", Alias = "Secondary Locks", Action = "SECONDARY_LOCKS", ActionUrl = "budget-secondarylock-list", ImageUrl = "action_image", Parent = settings.Name });

            list.Add(new MenuPlaceHolder() { Name = "PRIMARY_LOCKS", Alias = "Primary Locks", Action = "PRIMARY_LOCKS", ActionUrl = "budget-primarylock-list", ImageUrl = "action_image", Parent = settings.Name });

            list.Add(new MenuPlaceHolder() { Name = "CURRENCIES", Alias = "Currencies", Action = "CURRENCIES", ActionUrl = "budget-currency-list", ImageUrl = "action_image", Parent = settings.Name });

            list.Add(new MenuPlaceHolder() { Name = "GENERAL_SETTINGS", Alias = "General Settings", Action = "GENERAL_SETTINGS", ActionUrl = "budget-generalsetting-list", ImageUrl = "action_image", Parent = settings.Name });

            return list;
        }

        public static List<MenuRolePlaceHolder> GetMenuRoles()
        {
            var list = new List<MenuRolePlaceHolder>();

            list.Add(new MenuRolePlaceHolder() { MenuName = "BUDGET", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "BUDGET", RoleName = GROUP_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "BUDGET", RoleName = GROUP_READONLY });
            list.Add(new MenuRolePlaceHolder() { MenuName = "BUDGET", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "SETTINGS", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "SETTINGS", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "OPERATION", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "OPERATION", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "BUDGETING_LEVELS", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "BUDGETING_LEVELS", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "POLICY_LEVELS", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "POLICY_LEVELS", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "MODIFICATION_LEVELS", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "MODIFICATION_LEVELS", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "SECONDARY_LOCK_LEVELS", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "SECONDARY_LOCK_LEVELS", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "SECONDARY_LOCKS", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "SECONDARY_LOCKS", RoleName = GROUP_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "SECONDARY_LOCKS", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "PRIMARY_LOCKS", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "PRIMARY_LOCKS", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "CURRENCIES", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "CURRENCIES", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "GENERAL_SETTINGS", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "GENERAL_SETTINGS", RoleName = GROUP_BUSINESS });
           
            return list;
        }
    }
}
