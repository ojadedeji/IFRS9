using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fintrak.Shared.Common.PlaceHolders;


namespace Fintrak.Shared.IFRS.Framework
{
    public static class FinancialInstrumentModuleDefinition
    {
        public const string SOLUTION_NAME = "FIN_IFRS";
        public const string SOLUTION_ALIAS = "IFRS";
        public const string MODULE_NAME = "FIN_FINANCIAL_INSTRUMENT";
        public const string MODULE_ALIAS = "Other Financial Instruments";

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

            var rootName = "IFRS_FINANCIAL_INSTRUMENT";
            //var root = new MenuPlaceHolder() { Name = "IFRS_EXTRACTED_DATA", Alias = "IFRS Extracted Data", Action = "IFRS_EXTRACTED_DATA", ActionUrl = "", ImageUrl = "ifrs_image", Parent = "" };
            //list.Add(root);

            list.Add(new MenuPlaceHolder() { Name = "FAIR_VALUE_BASIS_MAP", Alias = "Fair Value Basis Map", Action = "FAIR_VALUE_BASIS_MAP", ActionUrl = "ifrsfi-fairvaluebasismap-list", ImageUrl = "action_image", Parent = rootName, ParentModule = "FIN_IFRS_CORE" });

            list.Add(new MenuPlaceHolder() { Name = "FAIR_VALUE_BASIS_EXEMPTION", Alias = "Fair Value Basis Exemption", Action = "FAIR_VALUE_BASIS_EXEMPTION", ActionUrl = "ifrsfi-fairvaluebasisexemption-list", ImageUrl = "action_image", Parent = rootName, ParentModule = "FIN_IFRS_CORE" });

            list.Add(new MenuPlaceHolder() { Name = "INSTRUMENT_TYPES", Alias = "Instrument Types", Action = "INSTRUMENT_TYPES", ActionUrl = "finstat-instrumenttype-list", ImageUrl = "action_image", Parent = rootName, ParentModule = "FIN_IFRS_CORE" });

            list.Add(new MenuPlaceHolder() { Name = "GL_TYPES", Alias = "GL Types", Action = "GL_TYPES", ActionUrl = "finstat-gltype-list", ImageUrl = "action_image", Parent = rootName, ParentModule = "FIN_IFRS_CORE" });

            list.Add(new MenuPlaceHolder() { Name = "INSTRUMENT_GL_MAP", Alias = "Instrument GL Maps", Action = "INSTRUMENT_GL_MAP", ActionUrl = "finstat-instrumenttypeglmap-list", ImageUrl = "action_image", Parent = rootName, ParentModule = "FIN_IFRS_CORE" });
                

            return list;
        }

        public static List<MenuRolePlaceHolder> GetMenuRoles()
        {
            var list = new List<MenuRolePlaceHolder>();

            list.Add(new MenuRolePlaceHolder() { MenuName = "FAIR_VALUE_BASIS_MAP", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "FAIR_VALUE_BASIS_MAP", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "FAIR_VALUE_BASIS_EXEMPTION", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "FAIR_VALUE_BASIS_EXEMPTION", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "INSTRUMENT_TYPES", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "INSTRUMENT_TYPES", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "GL_TYPES", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "GL_TYPES", RoleName = GROUP_USER });

            list.Add(new MenuRolePlaceHolder() { MenuName = "INSTRUMENT_GL_MAP", RoleName = GROUP_ADMINISTRATOR });
            list.Add(new MenuRolePlaceHolder() { MenuName = "INSTRUMENT_GL_MAP", RoleName = GROUP_USER });
          
            return list;
        }
    }
}
