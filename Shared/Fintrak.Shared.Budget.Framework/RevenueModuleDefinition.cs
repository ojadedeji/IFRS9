using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fintrak.Shared.Common.PlaceHolders;

namespace Fintrak.Shared.SystemCore.Framework
{
    public static class RevenueModuleDefinition
    {
        public const string SOLUTION_NAME = "BUDGET";
        public const string SOLUTION_ALIAS = "Budget";

        public const string MODULE_NAME = "FIN_BUDGET_REVENUE";
        public const string MODULE_ALIAS = "Fintrak Budget Revenue";

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
            var staff = new MenuPlaceHolder() { Name = "REVENUE", Alias = "Revenue", Action = "", ActionUrl = "", ImageUrl = "revenue_image", Parent = root.Name, ParentModule = "FIN_BUDGET_CORE" };
            list.Add(staff);

            list.Add(new MenuPlaceHolder() { Name = "REVENUE_GROUP", Alias = "Revenue Groups", Action = "REVENUE_GROUP", ActionUrl = "budget-revenuegroup-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "REVENUE_CATEGORY", Alias = "Revenue Categories", Action = "REVENUE_CATEGORY", ActionUrl = "budget-revenuecategory-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "REVENUE_CLASSIFICATION", Alias = "Revenue Classifications", Action = "REVENUE_CLASSIFICATION", ActionUrl = "budget-revenueclassification-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "REVENUE_CAPTION", Alias = "Revenue Captions", Action = "REVENUE_CAPTION", ActionUrl = "budget-revenuecaption-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "REVENUE_PRODUCT", Alias = "Revenue Products", Action = "REVENUE_PRODUCT", ActionUrl = "budget-revenueproduct-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "REVENUE_CUSTOMER", Alias = "Customers", Action = "REVENUE_CUSTOMER", ActionUrl = "budget-revenuecustomer-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "REVENUE_CUSTOMER_ACCOUNT", Alias = "Customer Accounts", Action = "REVENUE_CUSTOMER_ACCOUNT", ActionUrl = "budget-revenueitem-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "REVENUE_CUSTOMER_PRODUCT", Alias = "Customer Products", Action = "REVENUE_CUSTOMER_PRODUCT", ActionUrl = "budget-revenueproduct-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "REVENUE_VOLUME_BASED_SETUP", Alias = "Volume Based Setup", Action = "REVENUE_VOLUME_BASED_SETUP", ActionUrl = "budget-revenuevolumebasedsetup-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "REVENUE_VOLUME_BASED_RATE", Alias = "Volume Based Rates", Action = "REVENUE_VOLUME_BASED_RATE", ActionUrl = "budget-revenuevolumebasedrate-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "REVENUE_SHARED_EXEMPTION", Alias = "Revenue Shared Exemptions", Action = "REVENUE_SHARED_EXEMPTION", ActionUrl = "budget-revenuesharedexemption-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "REVENUE_SHARED_RATIO", Alias = "Revenue Shared Ratios", Action = "REVENUE_SHARED_RATIO", ActionUrl = "budget-revenuesharedratio-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "REVENUE_INTEREST_RATE", Alias = "Interest Rates", Action = "REVENUE_INTEREST_RATE", ActionUrl = "budget-revenueinterestrate-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "REVENUE_POOL_RATE", Alias = "Pool Rates", Action = "REVENUE_POOL_RATE", ActionUrl = "budget-revenuepoolrate-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "REVENUE_GROUP_ENTRIES", Alias = "Revenue Group Entries", Action = "REVENUE_GROUP_ENTRIES", ActionUrl = "budget-revenuegroupentry-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "REVENUE_ENTRIES", Alias = "Revenue Entries", Action = "REVENUE_ENTRIES", ActionUrl = "budget-revenueentry-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "REVENUE_SETTING", Alias = "Revenue Settings", Action = "REVENUE_SETTING", ActionUrl = "budget-revenuesetting-list", ImageUrl = "action_image", Parent = staff.Name });

            return list;
        }

        public static List<MenuRolePlaceHolder> GetMenuRoles()
        {
            var list = new List<MenuRolePlaceHolder>();

            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE", RoleName = GROUP_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_GROUP", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_GROUP", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_CATEGORY", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_CATEGORY", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_CLASSIFICATION", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_CLASSIFICATION", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_CAPTION", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_CAPTION", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_PRODUCT", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_PRODUCT", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_CUSTOMER", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_CUSTOMER", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_CUSTOMER_ACCOUNT", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_CUSTOMER_ACCOUNT", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_CUSTOMER_PRODUCT", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_CUSTOMER_PRODUCT", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_VOLUME_BASED_SETUP", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_VOLUME_BASED_SETUP", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_VOLUME_BASED_RATE", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_VOLUME_BASED_RATE", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_SHARED_EXEMPTION", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_SHARED_EXEMPTION", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_SHARED_RATIO", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_SHARED_RATIO", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_INTEREST_RATE", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_INTEREST_RATE", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_POOL_RATE", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_POOL_RATE", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_GROUP_ENTRIES", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_GROUP_ENTRIES", RoleName = GROUP_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_GROUP_ENTRIES", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_ENTRIES", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_ENTRIES", RoleName = GROUP_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_ENTRIES", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_SETTING", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "REVENUE_SETTING", RoleName = GROUP_BUSINESS });
           
            return list;
        }
    }
}
