using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fintrak.Shared.Common.PlaceHolders;

namespace Fintrak.Shared.SystemCore.Framework
{
    public static class FeeModuleDefinition
    {
        public const string SOLUTION_NAME = "BUDGET";
        public const string SOLUTION_ALIAS = "Budget";

        public const string MODULE_NAME = "FIN_BUDGET_FEE";
        public const string MODULE_ALIAS = "Fintrak Budget Fee";

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
            var staff = new MenuPlaceHolder() { Name = "FEE", Alias = "Fee", Action = "", ActionUrl = "", ImageUrl = "fee_image", Parent = root.Name, ParentModule = "FIN_BUDGET_CORE" };
            list.Add(staff);

            list.Add(new MenuPlaceHolder() { Name = "FEE_GROUP", Alias = "Fee Groups", Action = "FEE_GROUP", ActionUrl = "budget-feegroup-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "FEE_CATEGORY", Alias = "Fee Categories", Action = "FEE_CATEGORY", ActionUrl = "budget-feecategory-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "FEE_CAPTION", Alias = "Fee Captions", Action = "FEE_CAPTION", ActionUrl = "budget-feecaption-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "FEE_CALCULATION_TYPE", Alias = "Fee Calculation Types", Action = "FEE_CALCULATION_TYPE", ActionUrl = "budget-feecaption-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "FEE_MOVEMENT", Alias = "Fee Movements", Action = "FEE_MOVEMENT", ActionUrl = "budget-feemovement-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "FEE_ITEM", Alias = "Fee Items", Action = "FEE_ITEM", ActionUrl = "budget-feeitem-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "FEE_VOLUME_BASED_SETUP", Alias = "Volume Based Setup", Action = "FEE_VOLUME_BASED_SETUP", ActionUrl = "budget-feevolumebasedsetup-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "FEE_VOLUME_BASED_RATE", Alias = "Volume Based Rates", Action = "FEE_VOLUME_BASED_RATE", ActionUrl = "budget-feevolumebasedrate-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "FEE_SHARED_EXEMPTION", Alias = "Fee Shared Exemptions", Action = "FEE_SHARED_EXEMPTION", ActionUrl = "budget-feesharedexemption-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "FEE_SHARED_RATIO", Alias = "Fee Shared Ratios", Action = "FEE_SHARED_RATIO", ActionUrl = "budget-feesharedratio-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "FEE_GROUP_ENTRIES", Alias = "Fee Group Entries", Action = "FEE_GROUP_ENTRIES", ActionUrl = "budget-feegroupentry-list", ImageUrl = "action_image", Parent = staff.Name });

            list.Add(new MenuPlaceHolder() { Name = "FEE_ENTRIES", Alias = "Fee Entries", Action = "FEE_ENTRIES", ActionUrl = "budget-feeentry-list", ImageUrl = "action_image", Parent = staff.Name });

            return list;
        }

        public static List<MenuRolePlaceHolder> GetMenuRoles()
        {
            var list = new List<MenuRolePlaceHolder>();

            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE", RoleName = GROUP_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_GROUP", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_GROUP", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_CATEGORY", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_CATEGORY", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_CAPTION", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_CAPTION", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_CALCULATION_TYPE", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_CALCULATION_TYPE", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_MOVEMENT", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_MOVEMENT", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_ITEM", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_ITEM", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_SHARED_EXEMPTION", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_SHARED_EXEMPTION", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_SHARED_RATIO", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_SHARED_RATIO", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_VOLUME_BASED_SETUP", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_VOLUME_BASED_SETUP", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_VOLUME_BASED_RATE", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_VOLUME_BASED_RATE", RoleName = GROUP_BUSINESS });

            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_GROUP_ENTRIES", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_GROUP_ENTRIES", RoleName = GROUP_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_GROUP_ENTRIES", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_ENTRIES", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_ENTRIES", RoleName = GROUP_BUSINESS });
            list.Add(new MenuRolePlaceHolder() { MenuName = "FEE_ENTRIES", RoleName = GROUP_USER });
           
            return list;
        }
    }
}
