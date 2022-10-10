using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fintrak.Shared.Common.PlaceHolders;

namespace Fintrak.Shared.SystemCore.Framework
{
    public static class SystemCoreModuleDefinition

    {
        public const string SOLUTION_NAME = "CORE";
        public const string SOLUTION_ALIAS = "Core";

        public const string MODULE_NAME = "FIN_CORE";
        public const string MODULE_ALIAS = "Fintrak Core";

        public const string GROUP_ADMINISTRATOR = "Administrator";
        public const string GROUP_SUPER_BUSINESS = "Super Business";
        public const string GROUP_BUSINESS = "Business";
        public const string GROUP_USER = "User";

        public static List<RolePlaceHolder> GetRoles()
        {
            var list = new List<RolePlaceHolder>();

            list.Add(new RolePlaceHolder() { Name = GROUP_ADMINISTRATOR, Description = "For Core solution unlimited users" });
            list.Add(new RolePlaceHolder() { Name = GROUP_SUPER_BUSINESS, Description = "For Core solution semi-limited admin users" });
            list.Add(new RolePlaceHolder() { Name = GROUP_BUSINESS, Description = "For Core solution semi-limited users" });
            list.Add(new RolePlaceHolder() { Name = GROUP_USER, Description = "For Core solution limited users" });

            return list;
        }

        public static List<MenuPlaceHolder> GetMenus()
        {
            var list = new List<MenuPlaceHolder>();

            var root = new MenuPlaceHolder() { Name = "SETTINGS", Alias = "Settings", Action = "", ActionUrl = "", ImageUrl = "settings_image", Parent = "" };
            list.Add(root);

            list.Add(new MenuPlaceHolder() { Name = "MODULES", Alias = "Modules", Action = "MODULES", ActionUrl = "core-module-list", ImageUrl = "action_image", Parent = root.Name });

            list.Add(new MenuPlaceHolder() { Name = "APPS", Alias = "Apps", Action = "APPS", ActionUrl = "core-apps-list", ImageUrl = "action_image", Parent = root.Name });

            var configuration = new MenuPlaceHolder() { Name = "CONFIGURATION", Alias = "Configuration", Action = "", ActionUrl = "", ImageUrl = "configuration_image", Parent = root.Name };
            list.Add(configuration);

            list.Add(new MenuPlaceHolder() { Name = "GENERAL", Alias = "General", Action = "GENERAL", ActionUrl = "core-general-edit", ImageUrl = "action_image", Parent = configuration.Name });

            list.Add(new MenuPlaceHolder() { Name = "CLIENT", Alias = "Client", Action = "CLIENT", ActionUrl = "core-client-list", ImageUrl = "action_image", Parent = configuration.Name });

            list.Add(new MenuPlaceHolder() { Name = "DATABASE", Alias = "Database", Action = "DATABASE", ActionUrl = "core-database-list", ImageUrl = "action_image", Parent = configuration.Name });

            list.Add(new MenuPlaceHolder() { Name = "COMPANY_SECURITY", Alias = "Company Security", Action = "COMPANY_SECURITY", ActionUrl = "core-companysecurity-list", ImageUrl = "action_image", Parent = configuration.Name });

            list.Add(new MenuPlaceHolder() { Name = "COMPANIES", Alias = "Companies", Action = "COMPANIES", ActionUrl = "core-company-list", ImageUrl = "action_image", Parent = configuration.Name });

            list.Add(new MenuPlaceHolder() { Name = "COMPANY_MODULE", Alias = "Company Modules", Action = "COMPANY_MODULE", ActionUrl = "core-companymodule-list", ImageUrl = "action_image", Parent = configuration.Name });

            var user = new MenuPlaceHolder() { Name = "USERS", Alias = "Users", Action = "", ActionUrl = "", ImageUrl = "users_image", Parent = root.Name };
            list.Add(user);

            list.Add(new MenuPlaceHolder() { Name = "ACCOUNTS", Alias = "Accounts", Action = "ACCOUNTS", ActionUrl = "core-usersetup-list", ImageUrl = "action_image", Parent = user.Name });

            list.Add(new MenuPlaceHolder() { Name = "USER_MANAGER", Alias = "User Manager", Action = "USER_MANAGER", ActionUrl = "core-usersetup-list", ImageUrl = "action_image", Parent = user.Name });

            list.Add(new MenuPlaceHolder() { Name = "ROLES", Alias = "Roles", Action = "ROLES", ActionUrl = "core-role-edit", ImageUrl = "action_image", Parent = user.Name });

            var accessibilityMenu = new MenuPlaceHolder() { Name = "ACCESSIBILITY", Alias = "Accessibility", Action = "", ActionUrl = "", ImageUrl = "accessibility_image", Parent = root.Name };
            list.Add(accessibilityMenu);

            list.Add(new MenuPlaceHolder() { Name = "MENUS", Alias = "Menus", Action = "MENUS", ActionUrl = "core-menurole-list", ImageUrl = "action_image", Parent = accessibilityMenu.Name });

            var manageAuditTrailMenu = new MenuPlaceHolder() { Name = "MANAGE_AUDITRAIL", Alias = "Manage Audittrail", Action = "", ActionUrl = "", ImageUrl = "manage_audittrail_image", Parent = root.Name };
            list.Add(manageAuditTrailMenu);

            list.Add(new MenuPlaceHolder() { Name = "AUDITTRAILS", Alias = "Audittrails", Action = "AUDITTRAILS", ActionUrl = "core-audittrail-list", ImageUrl = "action_image", Parent = manageAuditTrailMenu.Name });

            var activityMenu = new MenuPlaceHolder() { Name = "FINTRAK_ACTIVITIES", Alias = "Fintrak Activities", Action = "", ActionUrl = "", ImageUrl = "fintrak_activity_image", Parent = root.Name };
            list.Add(activityMenu);

            list.Add(new MenuPlaceHolder() { Name = "MANAGE_ACTIVITIES", Alias = "Activities", Action = "MANAGE_ACTIVITIES", ActionUrl = "core-activities-list", ImageUrl = "action_image", Parent = activityMenu.Name });
            
            return list;
        }

        public static List<MenuRolePlaceHolder> GetMenuRoles()
        {
            var list = new List<MenuRolePlaceHolder>();

            list.Add(new MenuRolePlaceHolder() { MenuName = "SETTINGS", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "SETTINGS", RoleName = GROUP_SUPER_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "SETTINGS", RoleName = GROUP_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "SETTINGS", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "MODULES", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "MODULES", RoleName = GROUP_SUPER_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "MODULES", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "APPS", RoleName = GROUP_SUPER_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "APPS", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "CONFIGURATION", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "CONFIGURATION", RoleName = GROUP_SUPER_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "CONFIGURATION", RoleName = GROUP_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "CONFIGURATION", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "GENERAL", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "CLIENT", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "DATABASE", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "COMPANY_SECURITY", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "COMPANIES", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "COMPANY_MODULE", RoleName = GROUP_ADMINISTRATOR });

            list.Add(new MenuRolePlaceHolder() { MenuName = "USERS", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "USERS", RoleName = GROUP_SUPER_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "ACCOUNTS", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "ACCOUNTS", RoleName = GROUP_SUPER_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "USER_MANAGER", RoleName = GROUP_SUPER_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "ROLES", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "ROLES", RoleName = GROUP_SUPER_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "ACCESSIBILITY", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "MANAGE_AUDITRAIL", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "AUDITTRAILS", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "FINTRAK_ACTIVITIES", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "MANAGE_ACTIVITIES", RoleName = GROUP_ADMINISTRATOR });
            
            return list;
        }
    }
}
