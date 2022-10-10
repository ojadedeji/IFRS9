using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fintrak.Shared.Common.PlaceHolders;

namespace Fintrak.Shared.IFRS.Framework
{
    public static class IFRSCoreModuleDefinition
    {
        public const string SOLUTION_NAME = "FIN_IFRS";
        public const string SOLUTION_ALIAS = "IFRS";

        public const string MODULE_NAME = "FIN_IFRS_CORE";
        public const string MODULE_ALIAS = "IFRS Core";

        public const string GROUP_ADMINISTRATOR = "Administrator";
        public const string GROUP_USER = "User";

        public static List<RolePlaceHolder> GetRoles()
        {
            var list = new List<RolePlaceHolder>();

            list.Add(new RolePlaceHolder() { Name = GROUP_ADMINISTRATOR, Description = "For IFRS solution unlimited users" });
            list.Add(new RolePlaceHolder() { Name = GROUP_USER, Description = "For IFRS solution limited users" });

            return list;
        }

        public static List<MenuPlaceHolder> GetMenus()
        {
            var list = new List<MenuPlaceHolder>();

            var root = new MenuPlaceHolder() { Name = "IFRS", Alias = "IFRS", Action = "", ActionUrl = "", ImageUrl = "ifrs_image", Parent = "" };
            list.Add(root);

            list.Add(new MenuPlaceHolder() { Name = "IFRS_LOAN", Alias = "Loans", Action = "IFRS_LOAN", ActionUrl = "", ImageUrl = "ifrs_loan_image", Parent = root.Name });

            list.Add(new MenuPlaceHolder() { Name = "IFRS_FINANCIAL_INSTRUMENT", Alias = "Financial Instrument", Action = "IFRS_FINANCIAL_INSTRUMENT", ActionUrl = "", ImageUrl = "action_image", Parent = root.Name });

            list.Add(new MenuPlaceHolder() { Name = "IFRS_EXTRACTED_DATA", Alias = "IFRS Extracted Data", Action = "IFRS_EXTRACTED_DATA", ActionUrl = "", ImageUrl = "action_image", Parent = root.Name });

            list.Add(new MenuPlaceHolder() { Name = "IFRS_DATA_VIEW", Alias = "IFRS Processed Data", Action = "IFRS_DATA_VIEW", ActionUrl = "", ImageUrl = "action_image", Parent = root.Name });

            list.Add(new MenuPlaceHolder() { Name = "FINSTAT", Alias = "Finstat", Action = "FINSTAT", ActionUrl = "core-companysecurity-list", ImageUrl = "action_image", Parent = root.Name });

            
            return list;
        }

        public static List<MenuRolePlaceHolder> GetMenuRoles()
        {
            var list = new List<MenuRolePlaceHolder>();

            list.Add(new MenuRolePlaceHolder() { MenuName = "IFRS", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "IFRS", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "IFRS_LOAN", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "IFRS_LOAN", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "IFRS_FINANCIAL_INSTRUMENT", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "IFRS_FINANCIAL_INSTRUMENT", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "IFRS_EXTRACTED_DATA", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "IFRS_EXTRACTED_DATA", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "IFRS_DATA_VIEW", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "IFRS_DATA_VIEW", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "FINSTAT", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "FINSTAT", RoleName = GROUP_USER });
            
            return list;
        }
    }
}
