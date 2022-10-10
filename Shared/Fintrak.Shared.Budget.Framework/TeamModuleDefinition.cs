using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fintrak.Shared.Common.PlaceHolders;

namespace Fintrak.Shared.SystemCore.Framework
{
    public static class TeamModuleDefinition
    {
        public const string SOLUTION_NAME = "BUDGET";
        public const string SOLUTION_ALIAS = "Budget";

        public const string MODULE_NAME = "FIN_BUDGET_TEAM";
        public const string MODULE_ALIAS = "Fintrak Budget Team";

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

            var root = new MenuPlaceHolder() { Name = "BUDGET", Alias = "Budget", Action = "", ActionUrl = "", ImageUrl = "budget_image", Parent = "", ParentModule = "FIN_BUDGET_CORE" };
            //list.Add(root);

            //Team
            var teams = new MenuPlaceHolder() { Name = "TEAM_MANAGER", Alias = "Team Manager", Action = "", ActionUrl = "", ImageUrl = "team_manager_image", Parent = root.Name, ParentModule = "FIN_BUDGET_CORE" };
            list.Add(teams);

            list.Add(new MenuPlaceHolder() { Name = "CLASSIFICATION_TYPES", Alias = "Classification Types", Action = "CLASSIFICATION_TYPES", ActionUrl = "budget-classificationtype-list", ImageUrl = "action_image", Parent = teams.Name });

            list.Add(new MenuPlaceHolder() { Name = "CLASSIFICATIONS", Alias = "Classifications", Action = "CLASSIFICATIONS", ActionUrl = "budget-classification-list", ImageUrl = "action_image", Parent = teams.Name });

            list.Add(new MenuPlaceHolder() { Name = "DEFINITIONS", Alias = "Definitions", Action = "DEFINITIONS", ActionUrl = "budget-definition-list", ImageUrl = "action_image", Parent = teams.Name });

            list.Add(new MenuPlaceHolder() { Name = "TEAMS", Alias = "Teams", Action = "TEAMS", ActionUrl = "budget-team-list", ImageUrl = "action_image", Parent = teams.Name });

            list.Add(new MenuPlaceHolder() { Name = "OFFICERS", Alias = "Officers", Action = "OFFICERS", ActionUrl = "budget-officer-list", ImageUrl = "action_image", Parent = teams.Name });

            list.Add(new MenuPlaceHolder() { Name = "TEAM_USERS", Alias = "Team Users", Action = "TEAM_USERS", ActionUrl = "budget-teamuser-list", ImageUrl = "action_image", Parent = teams.Name });

            list.Add(new MenuPlaceHolder() { Name = "TEAM_SETTINGS", Alias = "Team Settings", Action = "TEAM_SETTINGS", ActionUrl = "budget-teamsetting-list", ImageUrl = "action_image", Parent = teams.Name });
            
            return list;
        }

        public static List<MenuRolePlaceHolder> GetMenuRoles()
        {
            var list = new List<MenuRolePlaceHolder>();

            list.Add(new MenuRolePlaceHolder() { MenuName = "TEAM_MANAGER", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "TEAM_MANAGER", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "CLASSIFICATION_TYPES", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "CLASSIFICATION_TYPES", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "CLASSIFICATIONS", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "CLASSIFICATIONS", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "DEFINITIONS", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "DEFINITIONS", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "TEAMS", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "TEAMS", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "OFFICERS", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "OFFICERS", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "TEAM_USERS", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "TEAM_USERS", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "TEAM_SETTINGS", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "TEAM_SETTINGS", RoleName = GROUP_BUSINESS });

            return list;
        }
    }
}
