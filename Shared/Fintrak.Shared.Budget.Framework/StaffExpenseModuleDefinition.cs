using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fintrak.Shared.Common.PlaceHolders;

namespace Fintrak.Shared.SystemCore.Framework
{
    public static class StaffExpenseModuleDefinition
    {
        public const string SOLUTION_NAME = "BUDGET";
        public const string SOLUTION_ALIAS = "Budget";

        public const string MODULE_NAME = "FIN_BUDGET_STAFF_EXPENSE";
        public const string MODULE_ALIAS = "Fintrak Budget Staff Expense";

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
            var staff = new MenuPlaceHolder() { Name = "STAFF_EXPENSE", Alias = "Staff Expense", Action = "", ActionUrl = "", ImageUrl = "staff_expense_image", Parent = root.Name, ParentModule = "FIN_BUDGET_CORE" };
            list.Add(staff);

            list.Add(new MenuPlaceHolder() { Name = "GRADES", Alias = "Grades", Action = "GRADES", ActionUrl = "budget-grade-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "PAY_CLASSIFICATIONS", Alias = "Pay Classifications", Action = "PAY_CLASSIFICATIONS", ActionUrl = "budget-payclassification-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "PAY_STRUCTURES", Alias = "Pay Structures", Action = "PAY_STRUCTURES", ActionUrl = "budget-paystructures-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "STAFF_ENTRIES", Alias = "Staff Entries", Action = "STAFF_ENTRIES", ActionUrl = "budget-staffentry-list", ImageUrl = "action_image", Parent = staff.Name });

            return list;
        }

        public static List<MenuRolePlaceHolder> GetMenuRoles()
        {
            var list = new List<MenuRolePlaceHolder>();

            list.Add(new MenuRolePlaceHolder() { MenuName = "STAFF_EXPENSE", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "STAFF_EXPENSE", RoleName = GROUP_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "STAFF_EXPENSE", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "GRADES", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "GRADES", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "PAY_CLASSIFICATIONS", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "PAY_CLASSIFICATIONS", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "PAY_STRUCTURES", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "PAY_STRUCTURES", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "STAFF_ENTRIES", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "STAFF_ENTRIES", RoleName = GROUP_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "STAFF_ENTRIES", RoleName = GROUP_USER });
           
            return list;
        }
    }
}
