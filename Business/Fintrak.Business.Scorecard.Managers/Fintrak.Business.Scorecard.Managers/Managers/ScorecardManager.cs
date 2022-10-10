using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq;
using System.ServiceModel;
using System.Transactions;
using Fintrak.Data.Scorecard.Contracts;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Business.Scorecard.Contracts;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Scorecard.Entities;
using Fintrak.Shared.Common.Utils;
using Fintrak.Shared.Core.Entities;

using roleType = Fintrak.Shared.SystemCore.Framework;
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;


namespace Fintrak.Business.Scorecard.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class ScorecardManager : ManagerBase, IScorecardService
    {
        public ScorecardManager()
        {
        }

        public ScorecardManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        const string SOLUTION_NAME = "FIN_SCORECARD";
        const string SOLUTION_ALIAS = "Scorecard";
        const string MODULE_NAME = "FIN_SCORECARD";
        const string MODULE_ALIAS = "Scorecard";

        const string GROUP_ADMINISTRATOR = "Administrator";
        const string GROUP_BUSINESS = "Super User";
        const string GROUP_USER = "User";

        [OperationBehavior(TransactionScopeRequired = true)]
        public override void RegisterModule()
        {
            ExecuteFaultHandledOperation(() =>
            {
                systemCoreData.ISolutionRepository solutionRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.ISolutionRepository>();
                systemCoreData.IModuleRepository moduleRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IModuleRepository>();
                systemCoreData.IMenuRepository menuRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IMenuRepository>();
                systemCoreData.IRoleRepository roleRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IRoleRepository>();
                systemCoreData.IMenuRoleRepository menuRoleRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IMenuRoleRepository>();
              
                using (TransactionScope ts = new TransactionScope())
                {
                    //check if module has been installed
                    systemCoreEntities.Module module = moduleRepository.Get().Where(c => c.Name == MODULE_NAME).FirstOrDefault();
                    if (module == null)
                    {
                        //check if module category exit
                        systemCoreEntities.Solution solution = solutionRepository.Get().Where(c => c.Name == SOLUTION_NAME).FirstOrDefault();
                        if (solution == null)
                        {
                            //register solution
                            solution = new systemCoreEntities.Solution()
                            {
                                Name = SOLUTION_NAME,
                                Alias = SOLUTION_ALIAS,
                                Active = true,
                                Deleted = false,
                                CreatedBy = "Auto",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "Auto",
                                UpdatedOn = DateTime.Now
                            };

                            solution = solutionRepository.Add(solution);
                        }

                        //register module
                        module = new systemCoreEntities.Module()
                        {
                            Name = MODULE_NAME,
                            Alias = MODULE_ALIAS,
                            SolutionId = solution.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        module = moduleRepository.Add(module);

                      //Role
                        var adminRole = new systemCoreEntities.Role()
                        {
                            SolutionId = solution.SolutionId,
                            Name = GROUP_ADMINISTRATOR,
                            Description = "For Scorecard solution unlimited users",
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now,
                            Type = roleType.RoleType.Application
                        };

                        roleRepository.Add(adminRole);

                        var superRole = new systemCoreEntities.Role()
                        {
                            SolutionId = solution.SolutionId,
                            Name =  GROUP_BUSINESS,
                            Description = "For Scorecard solution partial unlimited users",
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now,
                            Type = roleType.RoleType.Application
                        };

                        roleRepository.Add(superRole);

                        var userRole = new systemCoreEntities.Role()
                        {
                            SolutionId = solution.SolutionId,
                            Name = GROUP_USER,
                            Description = "For Scorecard solution limited users",
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now,
                            Type = roleType.RoleType.Application
                        };

                        roleRepository.Add(userRole);

                        int menuIndex = 0;

                        //register menu
                        var root = new systemCoreEntities.Menu()
                        {
                            Name = "SCORECARD",
                            Alias = "Scorecard",
                            Action = "",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "scd_image",
                            ModuleId = module.EntityId,
                            ParentId = null,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        root = menuRepository.Add(root);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = root.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = root.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = root.EntityId,
                            RoleId = userRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });
                        var actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_DASHBOARDS",
                            Alias = "Dashboards",
                            Action = "SCD_DASHBOARDS",
                            ActionUrl = "scd-dashboard-graph",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = root.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });
                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_ANALYTIC",
                            Alias = "Analytics",
                            Action = "SCD_ANALYTIC",
                            ActionUrl = "scd-analytic-graph",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = root.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });
                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_DATAENTRY",
                            Alias = "Data Entry",
                            Action = "SCD_DATAENTRY",
                            ActionUrl = "scd-dataentry-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = root.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        var config = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_CONFIGURATION",
                            Alias = "Configuration",
                            Action = "SCD_CONFIGURATION",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "scd_configuration_image",
                            ModuleId = module.EntityId,
                            ParentId = root.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(config);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = config.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = config.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_SETUP",
                            Alias = "Setup",
                            Action = "SCD_SETUP",
                            ActionUrl = "scd-setup-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = config.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_TEAM_CLASSIFICATION",
                            Alias = "Team Classifications",
                            Action = "SCD_TEAM_CLASSIFICATION",
                            ActionUrl = "scd-teamclassification-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = config.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_TEAM_MAPPING",
                            Alias = "Team Mappings",
                            Action = "SCD_TEAM_MAPPING",
                            ActionUrl = "scd-teammapping-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = config.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        var kpi = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_KPI",
                            Alias = "KPIs",
                            Action = "SCD_KPI",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = root.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(kpi);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = kpi.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = kpi.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_CATEGORY",
                            Alias = "Categories",
                            Action = "SCD_CATEGORY",
                            ActionUrl = "scd-category-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = kpi.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_METRIC",
                            Alias = "Metrics",
                            Action = "SCD_METRIC",
                            ActionUrl = "scd-metric-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = kpi.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_CLASSIFICATION",
                            Alias = "Classifications",
                            Action = "SCD_CLASSIFICATION",
                            ActionUrl = "scd-classification-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = kpi.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_PARTICIPANT",
                            Alias = "Participants",
                            Action = "SCD_PARTICIPANT",
                            ActionUrl = "scd-participant-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = kpi.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_THRESHOLD",
                            Alias = "Thresholds",
                            Action = "SCD_THRESHOLD",
                            ActionUrl = "scd-threshold-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = kpi.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        var edata = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_EXTERNAL_DATA",
                            Alias = "External Data",
                            Action = "SCD_EXTERNAL_DATA",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = root.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(edata);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = edata.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = edata.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_ACTUAL_DATA",
                            Alias = "Actual Data",
                            Action = "SCD_ACTUAL_DATA",
                            ActionUrl = "scd-actualdata-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = edata.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_ACTUAL_MAPPING",
                            Alias = "Actual Mapping",
                            Action = "SCD_ACTUAL_MAPPING",
                            ActionUrl = "scd-actualmapping-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = edata.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });


                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_TARGET_DATA",
                            Alias = "Target Data",
                            Action = "SCD_TARGET_DATA",
                            ActionUrl = "scd-targetdata-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = edata.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_TARGET_MAPPING",
                            Alias = "Target Mappings",
                            Action = "SCD_TARGET_MAPPING",
                            ActionUrl = "scd-targetmapping-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = edata.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        var rpt = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_REPORT",
                            Alias = "Reports",
                            Action = "SCD_REPORT",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = root.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(rpt);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = rpt.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = rpt.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_TOP_PERFORMING_KPI",
                            Alias = "Top Performing KPIs",
                            Action = "SCD_TOP_PERFORMING_KPI",
                            ActionUrl = "scd-topperformingkpi-report",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = rpt.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_WORST_PERFORMING_KPI",
                            Alias = "Worst Performing KPIs",
                            Action = "SCD_WORST_PERFORMING_KPI",
                            ActionUrl = "scd-worstperformingkpi-report",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = rpt.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_MULTIPLE_KPI",
                            Alias = "Mulitple KPIs",
                            Action = "SCD_MULTIPLE_KPI",
                            ActionUrl = "scd-multiplekpi-report",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = rpt.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_KPI_PERFORMANCE",
                            Alias = "KPI Performance",
                            Action = "SCD_KPI_PERFORMANCE",
                            ActionUrl = "scd-kpiperformance-report",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = rpt.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_USER_KPI",
                            Alias = "User KPIs",
                            Action = "SCD_USER_KPI",
                            ActionUrl = "scd-userkpi-report",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = rpt.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_DATA_ENTRY_BYKPI",
                            Alias = "Data Entries By KPI",
                            Action = "SCD_DATA_ENTRY_BYKPI",
                            ActionUrl = "scd-dataentrybykpi-report",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = rpt.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });


                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_DATA_ENTRY_BY_USER",
                            Alias = "Data Entries By User",
                            Action = "SCD_DATA_ENTRY_BY_USER",
                            ActionUrl = "scd-dataentrybyuser-report",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = rpt.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SCD_ALL_DATA_ENTRY",
                            Alias = "All Data Entries",
                            Action = "SCD_ALL_DATA_ENTRY",
                            ActionUrl = "scd-alldataentry-report",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = rpt.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                    }

                    ts.Complete();
                }

            });

        }


        #region SCDActual operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SCDActual UpdateSCDActual(SCDActual scdActual)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDActualRepository scdActualRepository = _DataRepositoryFactory.GetDataRepository<ISCDActualRepository>();

                SCDActual updatedEntity = null;

                if (scdActual.ActualId == 0)
                    updatedEntity = scdActualRepository.Add(scdActual);
                else
                    updatedEntity = scdActualRepository.Update(scdActual);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSCDActual(int actualId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDActualRepository scdActualRepository = _DataRepositoryFactory.GetDataRepository<ISCDActualRepository>();

                scdActualRepository.Remove(actualId);
            });
        }

        public SCDActual GetSCDActual(int actualId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDActualRepository scdActualRepository = _DataRepositoryFactory.GetDataRepository<ISCDActualRepository>();

                SCDActual scdActualEntity = scdActualRepository.Get(actualId);
                if (scdActualEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SCDActual with ID of {0} is not in database", actualId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scdActualEntity;
            });
        }

        public SCDActual[] GetAllSCDActuals()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDActualRepository scdActualRepository = _DataRepositoryFactory.GetDataRepository<ISCDActualRepository>();

                IEnumerable<SCDActual> scdActuales = scdActualRepository.Get().ToArray();

                return scdActuales.ToArray();
            });
        }

        public SCDActual[] GetCaption()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDActualRepository scdActualRepository = _DataRepositoryFactory.GetDataRepository<ISCDActualRepository>();
                IEnumerable<SCDActual> scdActuales = scdActualRepository.GetCaption();


                return scdActuales.ToArray();
            });
        }


        #endregion

        #region SCDCategory operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SCDCategory UpdateSCDCategory(SCDCategory scdCategory)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDCategoryRepository scdCategoryRepository = _DataRepositoryFactory.GetDataRepository<ISCDCategoryRepository>();

                SCDCategory updatedEntity = null;

                if (scdCategory.CategoryId == 0)
                    updatedEntity = scdCategoryRepository.Add(scdCategory);
                else
                    updatedEntity = scdCategoryRepository.Update(scdCategory);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSCDCategory(int categoryId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDCategoryRepository scdCategoryRepository = _DataRepositoryFactory.GetDataRepository<ISCDCategoryRepository>();

                scdCategoryRepository.Remove(categoryId);
            });
        }

        public SCDCategory GetSCDCategory(int categoryId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDCategoryRepository scdCategoryRepository = _DataRepositoryFactory.GetDataRepository<ISCDCategoryRepository>();

                SCDCategory scdCategoryEntity = scdCategoryRepository.Get(categoryId);
                if (scdCategoryEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SCDCategory with ID of {0} is not in database", categoryId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scdCategoryEntity;
            });
        }


        public SCDCategoryData[] GetAllSCDCategorys()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDCategoryRepository scdCategoryRepository = _DataRepositoryFactory.GetDataRepository<ISCDCategoryRepository>();

                List<SCDCategoryData> scdCategorys = new List<SCDCategoryData>();

                IEnumerable<SCDCategoryInfo> scdCategoryInfos = scdCategoryRepository.GetSCDCategorys().ToArray();

                foreach (var scdCategoryInfo in scdCategoryInfos)
                {
                    scdCategorys.Add(
                        new SCDCategoryData
                        {
                            CategoryId = scdCategoryInfo.SCDCategory.EntityId,
                            Code = scdCategoryInfo.SCDCategory.Code,
                            Name = scdCategoryInfo.SCDCategory.Name,
                            ParentCode = scdCategoryInfo.SCDCategory.ParentCode,
                            ParentName = scdCategoryInfo.Parent != null ? scdCategoryInfo.Parent.Name : string.Empty,
                            Period = scdCategoryInfo.SCDCategory.Period,
                            Year = scdCategoryInfo.SCDCategory.Year,
                            Active = scdCategoryInfo.SCDCategory.Active
                        });
                }

                return scdCategorys.ToArray();
            });
        }

        #endregion

        #region SCDConfiguration operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SCDConfiguration UpdateSCDConfiguration(SCDConfiguration scdConfiguration)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDConfigurationRepository scdConfigurationRepository = _DataRepositoryFactory.GetDataRepository<ISCDConfigurationRepository>();

                SCDConfiguration updatedEntity = null;

                if (scdConfiguration.ConfigurationId == 0)
                    updatedEntity = scdConfigurationRepository.Add(scdConfiguration);
                else
                    updatedEntity = scdConfigurationRepository.Update(scdConfiguration);

                return updatedEntity;
            });
        }

        public SCDConfiguration GetFirstSetup()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDConfigurationRepository setUpRepository = _DataRepositoryFactory.GetDataRepository<ISCDConfigurationRepository>();

                SCDConfiguration setUpEntity = setUpRepository.Get().FirstOrDefault();
               
                return setUpEntity;
            });
        }

        #endregion

        #region SCDKPIClassification operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SCDKPIClassification UpdateSCDKPIClassification(SCDKPIClassification scdClassification)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDKPIClassificationRepository scdClassificationRepository = _DataRepositoryFactory.GetDataRepository<ISCDKPIClassificationRepository>();

                SCDKPIClassification updatedEntity = null;

                if (scdClassification.ClassificationId == 0)
                    updatedEntity = scdClassificationRepository.Add(scdClassification);
                else
                    updatedEntity = scdClassificationRepository.Update(scdClassification);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSCDKPIClassification(int classificationId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDKPIClassificationRepository scdClassificationRepository = _DataRepositoryFactory.GetDataRepository<ISCDKPIClassificationRepository>();

                scdClassificationRepository.Remove(classificationId);
            });
        }

        public SCDKPIClassification GetSCDKPIClassification(int classificationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDKPIClassificationRepository scdClassificationRepository = _DataRepositoryFactory.GetDataRepository<ISCDKPIClassificationRepository>();

                SCDKPIClassification scdClassificationEntity = scdClassificationRepository.Get(classificationId);
                if (scdClassificationEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SCDKPIClassification with ID of {0} is not in database", classificationId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scdClassificationEntity;
            });
        }

        public SCDKPIClassificationData[] GetAllSCDKPIClassifications()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDKPIClassificationRepository scdClassificationRepository = _DataRepositoryFactory.GetDataRepository<ISCDKPIClassificationRepository>();

                List<SCDKPIClassificationData> scdClassifications = new List<SCDKPIClassificationData>();

                IEnumerable<SCDKPIClassificationInfo> scdClassificationInfos = scdClassificationRepository.GetSCDKPIClassifications().ToArray();

                foreach (var scdClassificationInfo in scdClassificationInfos)
                {
                    scdClassifications.Add(
                        new SCDKPIClassificationData
                        {
                             ClassificationId = scdClassificationInfo.SCDKPIClassification.EntityId,
                             KPICode = scdClassificationInfo.SCDKPI.Code,
                             KPIName = scdClassificationInfo.SCDKPI.Name,
                             TeamClassificationCode = scdClassificationInfo.SCDTeamClassification.Code,
                             TeamClassificationName = scdClassificationInfo.SCDTeamClassification.Name,
                             CategoryCode = scdClassificationInfo.SCDKPI.CategoryCode,
                             CategoryName=scdClassificationInfo.SCDCategory != null ? scdClassificationInfo.SCDCategory.Name : string.Empty ,
                             Period = scdClassificationInfo.SCDKPIClassification.Period,                            
                             Year = scdClassificationInfo.SCDKPIClassification.Year,
                             Active = scdClassificationInfo.SCDKPIClassification.Active
                        });
                }

                return scdClassifications.ToArray();
            });
        }

        #endregion

        #region SCDKPI operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SCDKPI UpdateSCDKPI(SCDKPI scdKPI)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDKPIRepository scdKPIRepository = _DataRepositoryFactory.GetDataRepository<ISCDKPIRepository>();

                SCDKPI updatedEntity = null;

                if (scdKPI.KPIId == 0)
                    updatedEntity = scdKPIRepository.Add(scdKPI);
                else
                    updatedEntity = scdKPIRepository.Update(scdKPI);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSCDKPI(int configurationId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDKPIRepository scdKPIRepository = _DataRepositoryFactory.GetDataRepository<ISCDKPIRepository>();

                scdKPIRepository.Remove(configurationId);
            });
        }

        public SCDKPI GetSCDKPI(int configurationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDKPIRepository scdKPIRepository = _DataRepositoryFactory.GetDataRepository<ISCDKPIRepository>();

                SCDKPI scdKPIEntity = scdKPIRepository.Get(configurationId);
                if (scdKPIEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SCDKPI with ID of {0} is not in database", configurationId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scdKPIEntity;
            });
        }

        public SCDKPIData[] GetAllSCDKPIs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDKPIRepository scdKPIRepository = _DataRepositoryFactory.GetDataRepository<ISCDKPIRepository>();

                List<SCDKPIData> scdKPIs = new List<SCDKPIData>();

                IEnumerable<SCDKPIInfo> scdKPIInfos = scdKPIRepository.GetSCDKPIs().ToArray();

                foreach (var scdKPIInfo in scdKPIInfos)
                {
                    scdKPIs.Add(
                        new SCDKPIData
                        {
                          
                            KPIId = scdKPIInfo.SCDKPI.EntityId,
                            Code = scdKPIInfo.SCDKPI.Code,
                            Name = scdKPIInfo.SCDKPI.Name,
                            Description = scdKPIInfo.SCDKPI.Description,
                            PeriodType = scdKPIInfo.SCDKPI.PeriodType,
                            PeriodTypeName = scdKPIInfo.SCDKPI.PeriodType.ToString(),
                            Direction = scdKPIInfo.SCDKPI.Direction,
                            DirectionName = scdKPIInfo.SCDKPI.Direction.ToString(),
                            CategoryCode = scdKPIInfo.SCDKPI.CategoryCode,
                            CategoryName = scdKPIInfo.SCDCategory.Name,
                            IsKPICalculated = scdKPIInfo.SCDKPI.IsKPICalculated,
                            IsTargetCalculated = scdKPIInfo.SCDKPI.IsTargetCalculated,
                            Formula = scdKPIInfo.SCDKPI.Formula,
                            AggregateMethod = scdKPIInfo.SCDKPI.AggregateMethod,
                            AggregateMethodName = scdKPIInfo.SCDKPI.AggregateMethod.ToString(),
                            Active = scdKPIInfo.SCDKPI.Active
                        });
                }

                return scdKPIs.ToArray();
            });
        }

        #endregion

        #region SCDKPIActualMap operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SCDKPIActualMap UpdateSCDKPIActualMap(SCDKPIActualMap scdKPIActualMap)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDKPIActualMapRepository scdKPIActualMapRepository = _DataRepositoryFactory.GetDataRepository<ISCDKPIActualMapRepository>();

                SCDKPIActualMap updatedEntity = null;

                if (scdKPIActualMap.MapId == 0)
                    updatedEntity = scdKPIActualMapRepository.Add(scdKPIActualMap);
                else
                    updatedEntity = scdKPIActualMapRepository.Update(scdKPIActualMap);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSCDKPIActualMap(int mapId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDKPIActualMapRepository scdKPIActualMapRepository = _DataRepositoryFactory.GetDataRepository<ISCDKPIActualMapRepository>();

                scdKPIActualMapRepository.Remove(mapId);
            });
        }

        public SCDKPIActualMap GetSCDKPIActualMap(int mapId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDKPIActualMapRepository scdKPIActualMapRepository = _DataRepositoryFactory.GetDataRepository<ISCDKPIActualMapRepository>();

                SCDKPIActualMap scdKPIActualMapEntity = scdKPIActualMapRepository.Get(mapId);
                if (scdKPIActualMapEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SCDKPIActualMap with ID of {0} is not in database", mapId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scdKPIActualMapEntity;
            });
        }

        //public SCDKPIActualMap[] GetAllSCDKPIActualMaps()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        ISCDKPIActualMapRepository scdKPIActualMapRepository = _DataRepositoryFactory.GetDataRepository<ISCDKPIActualMapRepository>();

        //        IEnumerable<SCDKPIActualMap> scdKPIActualMapes = scdKPIActualMapRepository.Get().ToArray();

        //        return scdKPIActualMapes.ToArray();
        //    });
        //}

        public SCDKPIActualMapData[] GetAllSCDKPIActualMaps()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDKPIActualMapRepository scdTeamMapRepository = _DataRepositoryFactory.GetDataRepository<ISCDKPIActualMapRepository>();

                List<SCDKPIActualMapData> scdactualMaps = new List<SCDKPIActualMapData>();

                IEnumerable<SCDKPIActualMapInfo> scdactualmapInfos = scdTeamMapRepository.GetSCDKPIActualMaps().ToArray();

                foreach (var scdactualmapInfo in scdactualmapInfos)
                {
                    scdactualMaps.Add(
                        new SCDKPIActualMapData
                        {
                            MapId = scdactualmapInfo.scdkpiactualmap.EntityId,
                            KPICode = scdactualmapInfo.scdkpi.Code,
                            KPIName = scdactualmapInfo.scdkpi != null ? scdactualmapInfo.scdkpi.Name : string.Empty,
                            Formula = scdactualmapInfo.scdkpiactualmap.Formula,
                            Period = scdactualmapInfo.scdkpiactualmap.Period,
                            Year = scdactualmapInfo.scdkpiactualmap.Year,
                            Active = scdactualmapInfo.scdkpiactualmap.Active
                        });
                }

                return scdactualMaps.ToArray();
            });
        }
        #endregion

        #region SCDKPIEntry operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SCDKPIEntry UpdateSCDKPIEntry(SCDKPIEntry scdKPIEntry)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDKPIEntryRepository scdKPIEntryRepository = _DataRepositoryFactory.GetDataRepository<ISCDKPIEntryRepository>();

                SCDKPIEntry updatedEntity = null;

                if (scdKPIEntry.EntryId == 0)
                    updatedEntity = scdKPIEntryRepository.Add(scdKPIEntry);
                else
                    updatedEntity = scdKPIEntryRepository.Update(scdKPIEntry);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSCDKPIEntry(int entryId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDKPIEntryRepository scdKPIEntryRepository = _DataRepositoryFactory.GetDataRepository<ISCDKPIEntryRepository>();

                scdKPIEntryRepository.Remove(entryId);
            });
        }

        public SCDKPIEntry GetSCDKPIEntry(int entryId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDKPIEntryRepository scdKPIEntryRepository = _DataRepositoryFactory.GetDataRepository<ISCDKPIEntryRepository>();

                SCDKPIEntry scdKPIEntryEntity = scdKPIEntryRepository.Get(entryId);
                if (scdKPIEntryEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SCDKPIEntry with ID of {0} is not in database", entryId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scdKPIEntryEntity;
            });
        }

        public SCDKPIEntryData[] GetAllSCDKPIEntrys()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDKPIEntryRepository scdKPIEntryRepository = _DataRepositoryFactory.GetDataRepository<ISCDKPIEntryRepository>();

                List<SCDKPIEntryData> scdKPIEntrys = new List<SCDKPIEntryData>();

                IEnumerable<SCDKPIEntryInfo> scdKPIEntryInfos = scdKPIEntryRepository.GetSCDKPIEntrys().ToArray();

                foreach (var scdKPIEntryInfo in scdKPIEntryInfos)
                {
                    scdKPIEntrys.Add(
                        new SCDKPIEntryData
                        {
                            EntryId = scdKPIEntryInfo.SCDKPIEntry.EntityId,
                            StaffCode = scdKPIEntryInfo.SCDKPIEntry.StaffCode,
                            StaffName = scdKPIEntryInfo.Staff.Name,
                            MISCode = scdKPIEntryInfo.SCDKPIEntry.MISCode,
                            MISName = scdKPIEntryInfo.SCDTeamMap.MISName,
                            KPICode = scdKPIEntryInfo.SCDKPI.Code,
                            KPIName = scdKPIEntryInfo.SCDKPI.Name,
                            Actual = scdKPIEntryInfo.SCDKPIEntry.Actual,
                            Target = scdKPIEntryInfo.SCDKPIEntry.Target,
                            Score = scdKPIEntryInfo.SCDKPIEntry.Score,
                            Date = scdKPIEntryInfo.SCDKPIEntry.Date,
                            Period = scdKPIEntryInfo.SCDKPIEntry.Period,
                            Year = scdKPIEntryInfo.SCDKPIEntry.Year,
                            Active = scdKPIEntryInfo.SCDKPIEntry.Active
                        });
                }

                return scdKPIEntrys.ToArray();
            });
        }

        #endregion

        #region SCDKPITargetMap operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SCDKPITargetMap UpdateSCDKPITargetMap(SCDKPITargetMap scdKPITargetMap)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDKPITargetMapRepository scdKPITargetMapRepository = _DataRepositoryFactory.GetDataRepository<ISCDKPITargetMapRepository>();

                SCDKPITargetMap updatedEntity = null;

                if (scdKPITargetMap.MapId == 0)
                    updatedEntity = scdKPITargetMapRepository.Add(scdKPITargetMap);
                else
                    updatedEntity = scdKPITargetMapRepository.Update(scdKPITargetMap);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSCDKPITargetMap(int mapId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDKPITargetMapRepository scdKPITargetMapRepository = _DataRepositoryFactory.GetDataRepository<ISCDKPITargetMapRepository>();

                scdKPITargetMapRepository.Remove(mapId);
            });
        }

        public SCDKPITargetMap GetSCDKPITargetMap(int mapId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDKPITargetMapRepository scdKPITargetMapRepository = _DataRepositoryFactory.GetDataRepository<ISCDKPITargetMapRepository>();

                SCDKPITargetMap scdKPITargetMapEntity = scdKPITargetMapRepository.Get(mapId);
                if (scdKPITargetMapEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SCDKPITargetMap with ID of {0} is not in database", mapId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scdKPITargetMapEntity;
            });
        }

        //public SCDKPITargetMap[] GetAllSCDKPITargetMaps()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        ISCDKPITargetMapRepository scdKPITargetMapRepository = _DataRepositoryFactory.GetDataRepository<ISCDKPITargetMapRepository>();

        //        IEnumerable<SCDKPITargetMap> scdKPITargetMapes = scdKPITargetMapRepository.Get().ToArray();

        //        return scdKPITargetMapes.ToArray();
        //    });
        //}




        public SCDKPITargetMapData[] GetAllSCDKPITargetMaps()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDKPITargetMapRepository scdTeamMapRepository = _DataRepositoryFactory.GetDataRepository<ISCDKPITargetMapRepository>();

                List<SCDKPITargetMapData> scdtargetMaps = new List<SCDKPITargetMapData>();

                IEnumerable<SCDKPITargetMapInfo> scdtargetmapInfos = scdTeamMapRepository.GetSCDKPITargetMaps().ToArray();

                foreach (var scdtargetmapInfo in scdtargetmapInfos)
                {
                    scdtargetMaps.Add(
                        new SCDKPITargetMapData
                        {
                            MapId = scdtargetmapInfo.scdkpitargetmap.EntityId,
                            KPICode = scdtargetmapInfo.scdkpi.Code,
                            KPIName = scdtargetmapInfo.scdkpi != null ? scdtargetmapInfo.scdkpi.Name : string.Empty,
                            Formula = scdtargetmapInfo.scdkpitargetmap.Formula,
                            Period = scdtargetmapInfo.scdkpitargetmap.Period,
                            Year = scdtargetmapInfo.scdkpitargetmap.Year,
                            Active = scdtargetmapInfo.scdkpitargetmap.Active
                        });
                }

                return scdtargetMaps.ToArray();
            });
        }


        #endregion
        
        #region SCDParticipant operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SCDParticipant UpdateSCDParticipant(SCDParticipant scdParticipant)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDParticipantRepository scdParticipantRepository = _DataRepositoryFactory.GetDataRepository<ISCDParticipantRepository>();

                SCDParticipant updatedEntity = null;

                if (scdParticipant.ParticipantId == 0)
                    updatedEntity = scdParticipantRepository.Add(scdParticipant);
                else
                    updatedEntity = scdParticipantRepository.Update(scdParticipant);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSCDParticipant(int participantId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDParticipantRepository scdParticipantRepository = _DataRepositoryFactory.GetDataRepository<ISCDParticipantRepository>();

                scdParticipantRepository.Remove(participantId);
            });
        }

        public SCDParticipant GetSCDParticipant(int participantId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDParticipantRepository scdParticipantRepository = _DataRepositoryFactory.GetDataRepository<ISCDParticipantRepository>();

                SCDParticipant scdParticipantEntity = scdParticipantRepository.Get(participantId);
                if (scdParticipantEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SCDParticipant with ID of {0} is not in database", participantId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scdParticipantEntity;
            });
        }

        public SCDParticipantData[] GetAllSCDParticipants()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDParticipantRepository scdParticipantRepository = _DataRepositoryFactory.GetDataRepository<ISCDParticipantRepository>();

                List<SCDParticipantData> scdParticipants = new List<SCDParticipantData>();

                IEnumerable<SCDParticipantInfo> scdParticipantInfos = scdParticipantRepository.GetSCDParticipants().ToArray();

                foreach (var scdParticipantInfo in scdParticipantInfos)
                {
                    scdParticipants.Add(
                        new SCDParticipantData
                        {
                            ParticipantId = scdParticipantInfo.SCDParticipant.EntityId,
                            KPICode = scdParticipantInfo.SCDParticipant.KPICode,
                            KPIName = scdParticipantInfo.SCDKPI.Name,
                            TeamClassificationCode = scdParticipantInfo.Classification.Code,
                            TeamClassificationName = scdParticipantInfo.Classification.Name,
                            StaffCode = scdParticipantInfo.SCDParticipant.StaffCode,
                            StaffName = scdParticipantInfo.Staff != null?  scdParticipantInfo.Staff.Name : "",
                            Status = scdParticipantInfo.SCDParticipant.Status,
                            StatusName = scdParticipantInfo.SCDParticipant.Status.ToString(),
                            Period = scdParticipantInfo.SCDParticipant.Period,
                            Year = scdParticipantInfo.SCDParticipant.Year,
                            Active = scdParticipantInfo.SCDParticipant.Active
                        });
                }

                return scdParticipants.ToArray();
            });
        }

        #endregion

        #region SCDTarget operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SCDTarget UpdateSCDTarget(SCDTarget scdTarget)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDTargetRepository scdTargetRepository = _DataRepositoryFactory.GetDataRepository<ISCDTargetRepository>();

                SCDTarget updatedEntity = null;

                if (scdTarget.TargetId == 0)
                    updatedEntity = scdTargetRepository.Add(scdTarget);
                else
                    updatedEntity = scdTargetRepository.Update(scdTarget);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSCDTarget(int targetId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDTargetRepository scdTargetRepository = _DataRepositoryFactory.GetDataRepository<ISCDTargetRepository>();

                scdTargetRepository.Remove(targetId);
            });
        }

        public SCDTarget GetSCDTarget(int targetId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDTargetRepository scdTargetRepository = _DataRepositoryFactory.GetDataRepository<ISCDTargetRepository>();

                SCDTarget scdTargetEntity = scdTargetRepository.Get(targetId);
                if (scdTargetEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SCDTarget with ID of {0} is not in database", targetId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scdTargetEntity;
            });
        }

        public SCDTarget[] GetAllSCDTargets()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDTargetRepository scdTargetRepository = _DataRepositoryFactory.GetDataRepository<ISCDTargetRepository>();

                IEnumerable<SCDTarget> scdTargetes = scdTargetRepository.Get().ToArray();

                return scdTargetes.ToArray();
            });
        }

        public SCDTarget[] GetCaptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDTargetRepository scdActualRepository = _DataRepositoryFactory.GetDataRepository<ISCDTargetRepository>();
                IEnumerable<SCDTarget> scdActuales = scdActualRepository.GetCaptions();


                return scdActuales.ToArray();
            });
        }



        #endregion

        #region SCDTeamMap operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SCDTeamMap UpdateSCDTeamMap(SCDTeamMap scdTeamMap)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDTeamMapRepository scdTeamMapRepository = _DataRepositoryFactory.GetDataRepository<ISCDTeamMapRepository>();

                SCDTeamMap updatedEntity = null;

                if (scdTeamMap.TeamMapId == 0)
                    updatedEntity = scdTeamMapRepository.Add(scdTeamMap);
                else
                    updatedEntity = scdTeamMapRepository.Update(scdTeamMap);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSCDTeamMap(int teamMapId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDTeamMapRepository scdTeamMapRepository = _DataRepositoryFactory.GetDataRepository<ISCDTeamMapRepository>();

                scdTeamMapRepository.Remove(teamMapId);
            });
        }

        public SCDTeamMap GetSCDTeamMap(int teamMapId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDTeamMapRepository scdTeamMapRepository = _DataRepositoryFactory.GetDataRepository<ISCDTeamMapRepository>();

                SCDTeamMap scdTeamMapEntity = scdTeamMapRepository.Get(teamMapId);
                if (scdTeamMapEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SCDTeamMap with ID of {0} is not in database", teamMapId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scdTeamMapEntity;
            });
        }

            public SCDTeamMapData[] GetAllSCDTeamMaps()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDTeamMapRepository scdTeamMapRepository = _DataRepositoryFactory.GetDataRepository<ISCDTeamMapRepository>();

                List<SCDTeamMapData> scdTeamMaps = new List<SCDTeamMapData>();

                IEnumerable<SCDTeamMapInfo> scdTeamMapInfos = scdTeamMapRepository.GetSCDTeamMaps().ToArray();

                foreach (var scdTeamMapInfo in scdTeamMapInfos)
                {
                    scdTeamMaps.Add(
                        new SCDTeamMapData
                        {
                            TeamMapId = scdTeamMapInfo.SCDTeamMap.EntityId,
                            Centre = scdTeamMapInfo.SCDTeamMap.Centre,
                            CentreName = scdTeamMapInfo.SCDTeamMap.Centre.ToString(),
                            TeamDefinitionCode = scdTeamMapInfo.SCDTeamMap.TeamDefinitionCode,
                            TeamClassificationCode = scdTeamMapInfo.TeamClassification != null? scdTeamMapInfo.TeamClassification.Code:string.Empty,
                            TeamClassificationName = scdTeamMapInfo.TeamClassification != null?scdTeamMapInfo.TeamClassification.Name:string.Empty ,
                            MISCode = scdTeamMapInfo.SCDTeamMap.MISCode,
                            MISName = scdTeamMapInfo.SCDTeamMap.MISName,
                            Grade = scdTeamMapInfo.SCDTeamMap.Grade,
                            Period = scdTeamMapInfo.SCDTeamMap.Period ,
                            Year = scdTeamMapInfo.SCDTeamMap.Year,
                            StaffCode = scdTeamMapInfo.SCDTeamMap.StaffCode,
                            StaffName = scdTeamMapInfo.Staff != null? scdTeamMapInfo.Staff.Name: string.Empty ,
                            Active = scdTeamMapInfo.SCDTeamMap.Active
                        });
                }

                return scdTeamMaps.ToArray();
            });
        }

        #endregion

        #region SCDThreshold operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SCDThreshold UpdateSCDThreshold(SCDThreshold scdThreshold)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDThresholdRepository scdThresholdRepository = _DataRepositoryFactory.GetDataRepository<ISCDThresholdRepository>();

                SCDThreshold updatedEntity = null;

                if (scdThreshold.ThresholdId == 0)
                    updatedEntity = scdThresholdRepository.Add(scdThreshold);
                else
                    updatedEntity = scdThresholdRepository.Update(scdThreshold);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSCDThreshold(int thresholdId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR , GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDThresholdRepository scdThresholdRepository = _DataRepositoryFactory.GetDataRepository<ISCDThresholdRepository>();

                scdThresholdRepository.Remove(thresholdId);
            });
        }

        public SCDThreshold GetSCDThreshold(int thresholdId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDThresholdRepository scdThresholdRepository = _DataRepositoryFactory.GetDataRepository<ISCDThresholdRepository>();

                SCDThreshold scdThresholdEntity = scdThresholdRepository.Get(thresholdId);
                if (scdThresholdEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SCDThreshold with ID of {0} is not in database", thresholdId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scdThresholdEntity;
            });
        }


        public SCDThresholdData[] GetAllSCDThresholds()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDThresholdRepository scdThresholdRepository = _DataRepositoryFactory.GetDataRepository<ISCDThresholdRepository>();

                List<SCDThresholdData> scdThresholds = new List<SCDThresholdData>();

                IEnumerable<SCDThresholdInfo> scdThresholdInfos = scdThresholdRepository.GetSCDThresholds().ToArray();

                foreach (var scdThresholdInfo in scdThresholdInfos)
                {
                    scdThresholds.Add(
                        new SCDThresholdData
                        {
                            ThresholdId = scdThresholdInfo.SCDThreshold.EntityId,
                            Color = scdThresholdInfo.SCDThreshold.Color,
                            KPICode = scdThresholdInfo.SCDKPI.Code,
                            KPIName = scdThresholdInfo.SCDKPI.Name,
                            Maximum = scdThresholdInfo.SCDThreshold.Maximum,
                            Minimum = scdThresholdInfo.SCDThreshold.Minimum,
                            TeamClassificationCode = scdThresholdInfo.SCDTeamClassification.Code,
                            TeamClassificationName = scdThresholdInfo.SCDTeamClassification.Name,
                            Period = scdThresholdInfo.SCDThreshold.Period,
                            Year = scdThresholdInfo.SCDThreshold.Year,
                            StaffCode = scdThresholdInfo.SCDThreshold.StaffCode,
                            StaffName = scdThresholdInfo.Staff != null? scdThresholdInfo.Staff.Name: "",
                            Active = scdThresholdInfo.SCDThreshold.Active
                        });
                }

                return scdThresholds.ToArray();
            });
        }

        #endregion

        #region SCDTeamClassification operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SCDTeamClassification UpdateSCDTeamClassification(SCDTeamClassification scdTeamClassification)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDTeamClassificationRepository scdTeamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ISCDTeamClassificationRepository>();

                SCDTeamClassification updatedEntity = null;

                if (scdTeamClassification.TeamClassificationId == 0)
                    updatedEntity = scdTeamClassificationRepository.Add(scdTeamClassification);
                else
                    updatedEntity = scdTeamClassificationRepository.Update(scdTeamClassification);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSCDTeamClassification(int teamClassificationId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDTeamClassificationRepository scdTeamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ISCDTeamClassificationRepository>();

                scdTeamClassificationRepository.Remove(teamClassificationId);
            });
        }

        public SCDTeamClassification GetSCDTeamClassification(int teamClassificationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDTeamClassificationRepository scdTeamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ISCDTeamClassificationRepository>();

                SCDTeamClassification scdTeamClassificationEntity = scdTeamClassificationRepository.Get(teamClassificationId);
                if (scdTeamClassificationEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SCDTeamClassification with ID of {0} is not in database", teamClassificationId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scdTeamClassificationEntity;
            });
        }

        public SCDTeamClassification[] GetAllSCDTeamClassifications()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISCDTeamClassificationRepository scdTeamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ISCDTeamClassificationRepository>();

                IEnumerable<SCDTeamClassification> scdTeamClassificationes = scdTeamClassificationRepository.Get().ToArray();

                return scdTeamClassificationes.ToArray();
            });
        }


        #endregion

        #region Helper

        protected override bool AllowAccessToOperation(string solutionName, List<string> groupNames)
        {
            if (groupNames.Count == 0)
                return true;

            systemCoreData.IUserRoleRepository accountRoleRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IUserRoleRepository>();
            var accountRoles = accountRoleRepository.GetUserRoleInfo(solutionName, _LoginName, groupNames);

            if (accountRoles == null || accountRoles.Count() <= 0)
            {
                AuthorizationValidationException ex = new AuthorizationValidationException(string.Format("Access denied for {0}.", _LoginName));
                throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
            }

            return true;
        }


        

        #endregion

    }
}
