using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fintrak.Shared.Common.PlaceHolders;

namespace Fintrak.Shared.SystemCore.Framework
{
    public static class BudgetModuleDefinition
    {
        public const string SOLUTION_NAME = "BUDGET";
        public const string SOLUTION_ALIAS = "Budget";

        public const string MODULE_NAME = "FIN_BUDGET";
        public const string MODULE_ALIAS = "Fintrak Budget";

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

            list.Add(new MenuPlaceHolder() { Name = "BUDGETING_LEVELS", Alias = "Budgeting Levels", Action = "BUDGETING_LEVELS", ActionUrl = "budget-budgetlevel-list", ImageUrl = "action_image", Parent = settings.Name });

            list.Add(new MenuPlaceHolder() { Name = "POLICY_LEVELS", Alias = "Policy Levels", Action = "POLICY_LEVELS", ActionUrl = "budget-policylevel-list", ImageUrl = "action_image", Parent = settings.Name });

            list.Add(new MenuPlaceHolder() { Name = "MODIFICATION_LEVELS", Alias = "Modification Levels", Action = "MODIFICATION_LEVELS", ActionUrl = "budget-modificationlevel-list", ImageUrl = "action_image", Parent = settings.Name });

            list.Add(new MenuPlaceHolder() { Name = "SECONDARY_LOCK_LEVELS", Alias = "Secondary Lock Levels", Action = "SECONDARY_LOCK_LEVELS", ActionUrl = "budget-secondarylocklevel-list", ImageUrl = "action_image", Parent = settings.Name });

            list.Add(new MenuPlaceHolder() { Name = "SECONDARY_LOCKS", Alias = "Secondary Locks", Action = "SECONDARY_LOCKS", ActionUrl = "budget-secondarylock-list", ImageUrl = "action_image", Parent = settings.Name });

            list.Add(new MenuPlaceHolder() { Name = "PRIMARY_LOCKS", Alias = "Primary Locks", Action = "PRIMARY_LOCKS", ActionUrl = "budget-primarylock-list", ImageUrl = "action_image", Parent = settings.Name });

            list.Add(new MenuPlaceHolder() { Name = "CURRENCIES", Alias = "Currencies", Action = "CURRENCIES", ActionUrl = "budget-currency-list", ImageUrl = "action_image", Parent = settings.Name });

            list.Add(new MenuPlaceHolder() { Name = "GENERAL_SETTINGS", Alias = "General Settings", Action = "GENERAL_SETTINGS", ActionUrl = "budget-generalsetting-list", ImageUrl = "action_image", Parent = settings.Name });

            //Team
            var teams = new MenuPlaceHolder() { Name = "TEAM_MANAGER", Alias = "Team Manager", Action = "", ActionUrl = "", ImageUrl = "team_manager_image", Parent = root.Name };
            list.Add(teams);

            list.Add(new MenuPlaceHolder() { Name = "CLASSIFICATION_TYPES", Alias = "Classification Types", Action = "CLASSIFICATION_TYPES", ActionUrl = "budget-classificationtype-list", ImageUrl = "action_image", Parent = teams.Name });

            list.Add(new MenuPlaceHolder() { Name = "CLASSIFICATIONS", Alias = "Classifications", Action = "CLASSIFICATIONS", ActionUrl = "budget-classification-list", ImageUrl = "action_image", Parent = teams.Name });

            list.Add(new MenuPlaceHolder() { Name = "DEFINITIONS", Alias = "Definitions", Action = "DEFINITIONS", ActionUrl = "budget-definition-list", ImageUrl = "action_image", Parent = teams.Name });

            list.Add(new MenuPlaceHolder() { Name = "TEAMS", Alias = "Teams", Action = "TEAMS", ActionUrl = "budget-team-list", ImageUrl = "action_image", Parent = teams.Name });

            list.Add(new MenuPlaceHolder() { Name = "OFFICERS", Alias = "Officers", Action = "OFFICERS", ActionUrl = "budget-officer-list", ImageUrl = "action_image", Parent = teams.Name });

            list.Add(new MenuPlaceHolder() { Name = "TEAM_USERS", Alias = "Team Users", Action = "TEAM_USERS", ActionUrl = "budget-teamuser-list", ImageUrl = "action_image", Parent = teams.Name });

            list.Add(new MenuPlaceHolder() { Name = "TEAM_SETTINGS", Alias = "Team Settings", Action = "TEAM_SETTINGS", ActionUrl = "budget-teamsetting-list", ImageUrl = "action_image", Parent = teams.Name });
            
            //Staff
            var staff = new MenuPlaceHolder() { Name = "STAFF_EXPENSE", Alias = "Staff Expense", Action = "", ActionUrl = "", ImageUrl = "staff_expense_image", Parent = root.Name };
            list.Add(staff);

            list.Add(new MenuPlaceHolder() { Name = "GRADES", Alias = "Grades", Action = "GRADES", ActionUrl = "budget-grade-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "PAY_CLASSIFICATIONS", Alias = "Pay Classifications", Action = "PAY_CLASSIFICATIONS", ActionUrl = "budget-payclassification-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "PAY_STRUCTURES", Alias = "Pay Structures", Action = "PAY_STRUCTURES", ActionUrl = "budget-paystructures-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "STAFF_COUNTS", Alias = "Pay Counts", Action = "STAFF_COUNTS", ActionUrl = "budget-staffcounts-list", ImageUrl = "action_image", Parent = staff.Name });

            //Capex
            var capex = new MenuPlaceHolder() { Name = "CAPEX", Alias = "Staff Expense", Action = "", ActionUrl = "", ImageUrl = "staff_expense_image", Parent = root.Name };
            list.Add(staff);

            list.Add(new MenuPlaceHolder() { Name = "GRADES", Alias = "Grades", Action = "GRADES", ActionUrl = "budget-grade-list", ImageUrl = "action_image", Parent = staff.Name });

            return list;
        }

        public static List<MenuRolePlaceHolder> GetMenuRoles()
        {
            var list = new List<MenuRolePlaceHolder>();

            list.Add(new MenuRolePlaceHolder() { MenuName = "BUDGET", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "BUDGET", RoleName = GROUP_READONLY });
            list.Add(new MenuRolePlaceHolder() { MenuName = "BUDGET", RoleName = GROUP_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "BUDGET", RoleName = GROUP_USER });

           
            return list;
        }
    }
}
