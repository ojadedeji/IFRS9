using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fintrak.Shared.Common.PlaceHolders;


namespace Fintrak.Shared.IFRS.Framework
{
    public static class ExtractedDataModuleDefinition
    {
        public const string SOLUTION_NAME = "FIN_IFRS";
        public const string SOLUTION_ALIAS = "IFRS";
        public const string MODULE_NAME = "FIN_IFRS_EXTRACTED_DATA";
        public const string MODULE_ALIAS = "IFRS Extracted Data";

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

            var rootName = "IFRS_EXTRACTED_DATA";
            //var root = new MenuPlaceHolder() { Name = "IFRS_EXTRACTED_DATA", Alias = "IFRS Extracted Data", Action = "IFRS_EXTRACTED_DATA", ActionUrl = "", ImageUrl = "ifrs_image", Parent = "" };
            //list.Add(root);

            list.Add(new MenuPlaceHolder() { Name = "IFRS_BOND", Alias = "Bonds", Action = "IFRS_BOND", ActionUrl = "ifrs-bonddata-list", ImageUrl = "action_image", Parent = rootName, ParentModule = "FIN_IFRS_CORE" });

            list.Add(new MenuPlaceHolder() { Name = "IFRS_TBILLS", Alias = "Treasury Bills", Action = "IFRS_TBILLS", ActionUrl = "ifrs-tbills-list", ImageUrl = "action_image", Parent = rootName, ParentModule = "FIN_IFRS_CORE" });

            list.Add(new MenuPlaceHolder() { Name = "LOAN_PRY_DATA", Alias = "Loan Pry Data", Action = "LOAN_PRY_DATA", ActionUrl = "ifrs-loanprydata-list", ImageUrl = "action_image", Parent = rootName, ParentModule = "FIN_IFRS_CORE" });

            list.Add(new MenuPlaceHolder() { Name = "INTEGRAL_FEE", Alias = "Integral Fee", Action = "INTEGRAL_FEE", ActionUrl = "ifrs-integralfee-list", ImageUrl = "action_image", Parent = rootName, ParentModule = "FIN_IFRS_CORE" });

            list.Add(new MenuPlaceHolder() { Name = "LOAN_DETAILS", Alias = "Loan Details", Action = "LOAN_DETAILS", ActionUrl = "ifrs-loandetail-list", ImageUrl = "action_image", Parent = rootName, ParentModule = "FIN_IFRS_CORE" });

            list.Add(new MenuPlaceHolder() { Name = "IFRS_CUSTOMER", Alias = "Customer Data", Action = "IFRS_CUSTOMER", ActionUrl = "ifrs-ifrscustomer-list", ImageUrl = "action_image", Parent = rootName, ParentModule = "FIN_IFRS_CORE" });

            list.Add(new MenuPlaceHolder() { Name = "IFRS_UNMAPPED_PRODUCT", Alias = "Un-Mapped Products", Action = "IFRS_UNMAPPED_PRODUCT", ActionUrl = "ifrs-unmappedproduct-list", ImageUrl = "action_image", Parent = rootName, ParentModule = "FIN_IFRS_CORE" });

            list.Add(new MenuPlaceHolder() { Name = "LOAN_PRY_DATA_MORATORIUM", Alias = "Loan Pry Data Moratorium", Action = "LOAN_PRY_DATA_MORATORIUM", ActionUrl = "ifrs-loanprymoratoriumdata-list", ImageUrl = "action_image", Parent = rootName, ParentModule = "FIN_IFRS_CORE" });

            list.Add(new MenuPlaceHolder() { Name = "BORROWING_PRY_DATA", Alias = "Borrowings Pry Data", Action = "BORROWING_PRY_DATA", ActionUrl = "ifrs-borrowingdata-list", ImageUrl = "action_image", Parent = rootName, ParentModule = "FIN_IFRS_CORE" });
        
            return list;
        }

        public static List<MenuRolePlaceHolder> GetMenuRoles()
        {
            var list = new List<MenuRolePlaceHolder>();

            list.Add(new MenuRolePlaceHolder() { MenuName = "IFRS_BOND", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "IFRS_BOND", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "IFRS_TBILLS", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "IFRS_TBILLS", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "LOAN_PRY_DATA", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "LOAN_PRY_DATA", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "INTEGRAL_FEE", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "INTEGRAL_FEE", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "LOAN_DETAILS", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "LOAN_DETAILS", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "IFRS_CUSTOMER", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "IFRS_CUSTOMER", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "IFRS_UNMAPPED_PRODUCT", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "IFRS_UNMAPPED_PRODUCT", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "LOAN_PRY_DATA_MORATORIUM", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "LOAN_PRY_DATA_MORATORIUM", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "BORROWING_PRY_DATA", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "BORROWING_PRY_DATA", RoleName = GROUP_USER });

            
            return list;
        }
    }
}
