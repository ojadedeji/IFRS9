using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Linq;
using System.ServiceModel;
using System.Transactions;
using Fintrak.Data.Basic.Contracts;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Business.Basic.Contracts;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Common.Utils;
using Fintrak.Shared.Core.Entities;
using System.Data.SqlClient;

using systemCoreFramework = Fintrak.Shared.SystemCore.Framework;
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;

namespace Fintrak.Business.Basic.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class MPROPEXManager : ManagerBase, IMPROPEXService
    {
        public MPROPEXManager()
        {
        }

        public MPROPEXManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }
        /// <summary>
        /// </summary>
        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        const string SOLUTION_NAME = "FIN_MPR";
        const string SOLUTION_ALIAS = "MPR";
        const string MODULE_NAME = "FIN_MPR_OPEX";
        const string MODULE_ALIAS = "Operating Expense";

        const string GROUP_ADMINISTRATOR = "Administrator";
        const string GROUP_USER = "User";


        //[OperationBehavior(TransactionScopeRequired = true)]
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
                        var adminRole = roleRepository.Get().Where(c => c.Name == GROUP_ADMINISTRATOR && c.SolutionId == solution.SolutionId).FirstOrDefault();
                        var userRole = roleRepository.Get().Where(c => c.Name == GROUP_USER && c.SolutionId == solution.SolutionId).FirstOrDefault();

                        int menuIndex = 0;

                        //register menu
                        //get the root for BalanceSheet
                        var root = menuRepository.Get().Where(c => c.Alias == "MPR").FirstOrDefault();

                        var op = new systemCoreEntities.Menu()
                        {
                            Name = "OPEX",
                            Alias = "OPEX",
                            Action = "",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "opex_image",
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

                        op = menuRepository.Add(op);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = op.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        var actionMenu = new systemCoreEntities.Menu()
                          {
                              Name = "COST_CENTER_DEFINITION",
                              Alias = "Cost Center Definition",
                              Action = "COST_CENTER_DEFINITION",
                              ActionUrl = "mpr-costcentredefinition-list",
                              Image = null,
                              ImageUrl = "action_image",
                              ModuleId = module.EntityId,
                              ParentId = op.EntityId,
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

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "COST_CENTER",
                            Alias = "Cost Center",
                            Action = "COST_CENTER",
                            ActionUrl = "mpr-costcentre-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = op.EntityId,
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

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "EXPENSE_BASIS",
                            Alias = "Expense Basis",
                            Action = "EXPENSE_BASIS",
                            ActionUrl = "mpr-expensebasis-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = op.EntityId,
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

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "EXPENSE_MAPPING",
                            Alias = "Expense Mapping",
                            Action = "EXPENSE_MAPPING",
                            ActionUrl = "mpr-expensemapping-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = op.EntityId,
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

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "OPEX_GL_MAPPING",
                            Alias = "Opex GL Mappings",
                            Action = "OPEX_GL_MAPPING",
                            ActionUrl = "opex-opexglmapping-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = op.EntityId,
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

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "UNMAPPED_OPEX_GL",
                            Alias = "Un-Mapped Opex GL",
                            Action = "UNMAPPED_OPEX_GL",
                            ActionUrl = "opex-unmappedgl-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = op.EntityId,
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

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "EXPENSE_RAW_BASIS",
                            Alias = "Expense Raw Basis",
                            Action = "EXPENSE_RAW_BASIS",
                            ActionUrl = "mpr-expenserawbasis-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = op.EntityId,
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

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "STAFF_COST",
                            Alias = "Staff Cost",
                            Action = "STAFF_COST",
                            ActionUrl = "mpr-staffcost-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = op.EntityId,
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
                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "OPEX_MANAGEMENT_TREE",
                            Alias = "Management Tree",
                            Action = "OPEX_MANAGEMENT_TREE",
                            ActionUrl = "mpr-opexmanagementtree-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = op.EntityId,
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

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "ACTIVITY_BASE",
                            Alias = "Activity Base",
                            Action = "ACTIVITY_BASE",
                            ActionUrl = "mpr-activitybase-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = op.EntityId,
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

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "ACTIVITY_BASE_RATIO",
                            Alias = "Activity Base Ratio",
                            Action = "ACTIVITY_BASE_RATIO",
                            ActionUrl = "mpr-activitybaseratio-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = op.EntityId,
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

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "OPEX_MIS_REPLACEMENT",
                            Alias = "Opex MIS Replacement",
                            Action = "OPEX_MIS_REPLACEMENT",
                            ActionUrl = "mpr-opexmisreplacement-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = op.EntityId,
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

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "OPEX_ABC_EXEMPTION",
                            Alias = "Opex ABC Exemption",
                            Action = "OPEX_ABC_EXEMPTION",
                            ActionUrl = "mpr-opexabcexemption-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = op.EntityId,
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

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "OPEX_BUSINESS_RULE",
                            Alias = "Opex Business Rule",
                            Action = "OPEX_BUSINESS_RULE",
                            ActionUrl = "mpr-opexbusinessrule-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = op.EntityId,
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

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "OPEX_GL_BASIS",
                            Alias = "Opex GL Basis",
                            Action = "OPEX_GL_BASIS",
                            ActionUrl = "mpr-opexglbasis-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = op.EntityId,
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

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "OPEX_BASIS_MAPPING",
                            Alias = "Opex Basis Mapping",
                            Action = "OPEX_BASIS_MAPPING",
                            ActionUrl = "mpr-opexbasismapping-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = op.EntityId,
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

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "OPEX_CHECKLIST",
                            Alias = "Opex Checklist",
                            Action = "OPEX_CHECKLIST",
                            ActionUrl = "mpr-opexchecklist-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = op.EntityId,
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
                    }

                  ts.Complete();
                }

            });

        }

        #region CostCentreDefinition operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CostCentreDefinition UpdateCostCentreDefinition(CostCentreDefinition costCentreDefinition)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICostCentreDefinitionRepository costCentreDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ICostCentreDefinitionRepository>();

                CostCentreDefinition updatedEntity = null;

                if (costCentreDefinition.CCDefinitionId == 0)
                    updatedEntity = costCentreDefinitionRepository.Add(costCentreDefinition);
                else
                    updatedEntity = costCentreDefinitionRepository.Update(costCentreDefinition);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCostCentreDefinition(int ccDefinitionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICostCentreDefinitionRepository costCentreDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ICostCentreDefinitionRepository>();

                costCentreDefinitionRepository.Remove(ccDefinitionId);
            });
        }

        public CostCentreDefinition GetCostCentreDefinition(int ccDefinitionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICostCentreDefinitionRepository costCentreDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ICostCentreDefinitionRepository>();

                CostCentreDefinition costCentreDefinitionEntity = costCentreDefinitionRepository.Get(ccDefinitionId);
                if (costCentreDefinitionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CostCentreDefinition with ID of {0} is not in database", ccDefinitionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return costCentreDefinitionEntity;
            });
        }

        public CostCentreDefinition[] GetAllCostCentreDefinitions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICostCentreDefinitionRepository ccDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ICostCentreDefinitionRepository>();

                IEnumerable<CostCentreDefinition> ccDefinitions = ccDefinitionRepository.Get().ToArray();

                return ccDefinitions.ToArray();
            });
        }
       
        #endregion

        #region CostCentre operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CostCentre UpdateCostCentre(CostCentre costCentre)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICostCentreRepository costCentreRepository = _DataRepositoryFactory.GetDataRepository<ICostCentreRepository>();

                CostCentre updatedEntity = null;

                if (costCentre.CostCentreId == 0)
                    updatedEntity = costCentreRepository.Add(costCentre);
                else
                    updatedEntity = costCentreRepository.Update(costCentre);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCostCentre(int costCentreId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICostCentreRepository costCentreRepository = _DataRepositoryFactory.GetDataRepository<ICostCentreRepository>();

                costCentreRepository.Remove(costCentreId);
            });
        }

        public CostCentre GetCostCentre(int costCentreId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICostCentreRepository costCentreRepository = _DataRepositoryFactory.GetDataRepository<ICostCentreRepository>();

                CostCentre costCentreEntity = costCentreRepository.Get(costCentreId);
                if (costCentreEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CostCentre with ID of {0} is not in database", costCentreId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return costCentreEntity;
            });
        }

        public CostCentreData[] GetAllCostCentres()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICostCentreRepository costCentreRepository = _DataRepositoryFactory.GetDataRepository<ICostCentreRepository>();


                List<CostCentreData> costCentre = new List<CostCentreData>();
                IEnumerable<CostCentreInfo> costCentreInfos = costCentreRepository.GetCostCentres().ToArray();

                foreach (var costCentreInfo in costCentreInfos)
                {
                    costCentre.Add(
                        new CostCentreData
                        {
                            CostCentreId = costCentreInfo.CostCentre.EntityId,
                            Code = costCentreInfo.CostCentre.Code,
                            Name = costCentreInfo.CostCentre.Name,
                            DefinitionCode = costCentreInfo.CostCentre.DefinitionCode,
                            DefinitionName = costCentreInfo.CostCentreDefinition.Name,
                           // Parent=costCentreInfo.CostCentre.Parent.ToString(),
                            Parent = costCentreInfo.CostCentre.Parent != null ? costCentreInfo.Parent.Name : string.Empty,
                            Active = costCentreInfo.CostCentre.Active
                        });
                }

                return costCentre.ToArray();
            });
        }

        public CostCentre[] GetParentCostCentres(string definitionCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICostCentreDefinitionRepository costCentreDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ICostCentreDefinitionRepository>();
                ICostCentreRepository costCentreRepository = _DataRepositoryFactory.GetDataRepository<ICostCentreRepository>();

                ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
                var setUp = setupRepository.Get().FirstOrDefault();

                var costCentreDefinition = costCentreDefinitionRepository.Get().Where(c => c.Code == definitionCode && c.Year == setUp.Year).FirstOrDefault();
                var parentDefinition = costCentreDefinitionRepository.Get().Where(c => c.Position == (costCentreDefinition.Position + 1)).FirstOrDefault();

                CostCentre[] costCentres = costCentreRepository.Get().Where(c => c.DefinitionCode == parentDefinition.Code && c.Year == setUp.Year).ToArray();

                return costCentres;
            });
        }

        public CostCentre[] GetCostCentreByLevel(int level)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICostCentreDefinitionRepository costCentreDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ICostCentreDefinitionRepository>();
                ICostCentreRepository costCentreRepository = _DataRepositoryFactory.GetDataRepository<ICostCentreRepository>();

                ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
                var setUp = setupRepository.Get().FirstOrDefault();

                var costCentreDefinition = costCentreDefinitionRepository.Get().Where(c => c.Position == level).FirstOrDefault();

                CostCentre[] costCentres = null;

                if (costCentreDefinition != null)
                    costCentres = costCentreRepository.Get().Where(c => c.DefinitionCode == costCentreDefinition.Code && c.Year == setUp.Year).ToArray();

                return costCentres;
            });
        }

        public CostCentre[] GetCostCentreByDefinition(string definitionCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                //  ICostCentreDefinitionRepository costCentreDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ICostCentreDefinitionRepository>();
                ICostCentreRepository costCentreRepository = _DataRepositoryFactory.GetDataRepository<ICostCentreRepository>();

                ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
                var setUp = setupRepository.Get().FirstOrDefault();

                CostCentre[] costCentres = costCentreRepository.Get().Where(c => c.DefinitionCode == definitionCode && c.Year == setUp.Year).ToArray();

                return costCentres;
            });
        }

        #endregion

        #region ExpenseBasis operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ExpenseBasis UpdateExpenseBasis(ExpenseBasis expenseBasis)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExpenseBasisRepository expenseBasisRepository = _DataRepositoryFactory.GetDataRepository<IExpenseBasisRepository>();

                ExpenseBasis updatedEntity = null;

                if (expenseBasis.ExpenseBasisId == 0)
                    updatedEntity = expenseBasisRepository.Add(expenseBasis);
                else
                    updatedEntity = expenseBasisRepository.Update(expenseBasis);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteExpenseBasis(int expenseBasisId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExpenseBasisRepository expenseBasisRepository = _DataRepositoryFactory.GetDataRepository<IExpenseBasisRepository>();

                expenseBasisRepository.Remove(expenseBasisId);
            });
        }

        public ExpenseBasis GetExpenseBasis(int expenseBasisId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExpenseBasisRepository expenseBasisRepository = _DataRepositoryFactory.GetDataRepository<IExpenseBasisRepository>();

                ExpenseBasis expenseBasisEntity = expenseBasisRepository.Get(expenseBasisId);
                if (expenseBasisEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ExpenseBasis with ID of {0} is not in database", expenseBasisId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return expenseBasisEntity;
            });
        }

        public ExpenseBasis[] GetAllExpenseBasisInfo()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExpenseBasisRepository expenseBasisRepository = _DataRepositoryFactory.GetDataRepository<IExpenseBasisRepository>();

                IEnumerable<ExpenseBasis> expenseBasis = expenseBasisRepository.Get().ToArray();

                return expenseBasis.ToArray();
            });
        }

        #endregion

        #region ExpenseMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ExpenseMapping UpdateExpenseMapping(ExpenseMapping expenseMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExpenseMappingRepository expenseMappingRepository = _DataRepositoryFactory.GetDataRepository<IExpenseMappingRepository>();

                ExpenseMapping updatedEntity = null;

                if (expenseMapping.ExpenseMappingId == 0)
                    updatedEntity = expenseMappingRepository.Add(expenseMapping);
                else
                    updatedEntity = expenseMappingRepository.Update(expenseMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteExpenseMapping(int expenseMappingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExpenseMappingRepository expenseMappingRepository = _DataRepositoryFactory.GetDataRepository<IExpenseMappingRepository>();

                expenseMappingRepository.Remove(expenseMappingId);
            });
        }

        public ExpenseMapping GetExpenseMapping(int expenseMappingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExpenseMappingRepository expenseMappingRepository = _DataRepositoryFactory.GetDataRepository<IExpenseMappingRepository>();

                ExpenseMapping expenseMappingEntity = expenseMappingRepository.Get(expenseMappingId);
                if (expenseMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ExpenseMapping with ID of {0} is not in database", expenseMappingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return expenseMappingEntity;
            });
        }

        public ExpenseMappingData[] GetAllExpenseMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExpenseMappingRepository expenseMappingRepository = _DataRepositoryFactory.GetDataRepository<IExpenseMappingRepository>();


                List<ExpenseMappingData> expenseMapping = new List<ExpenseMappingData>();
                IEnumerable<ExpenseMappingInfo> expenseMappingInfos = expenseMappingRepository.GetExpenseMappings().ToArray();

                foreach (var expenseMappingInfo in expenseMappingInfos)
                {
                    expenseMapping.Add(
                        new ExpenseMappingData
                        {
                            ExpenseMappingId = expenseMappingInfo.ExpenseMapping.EntityId,
                            BasisCode = expenseMappingInfo.ExpenseMapping.BasisCode,
                            BasisName = expenseMappingInfo.ExpenseBasis.Name,
                            TeamDefinitionCode = expenseMappingInfo.ExpenseBasis.TeamDefinitionCode,
                            ItemCode = expenseMappingInfo.ExpenseMapping.ItemCode,
                            CategoryName = expenseMappingInfo.ExpenseBasis.Category.ToString(),
                            ValueTypeName = expenseMappingInfo.ExpenseBasis.ValueType.ToString(),
                            ItemTypeName = expenseMappingInfo.ExpenseBasis.ItemType.ToString(),
                            Active = expenseMappingInfo.ExpenseMapping.Active
                        });
                }

                return expenseMapping.ToArray();
            });
        }
        #endregion

        #region ExpenseProductMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ExpenseProductMapping UpdateExpenseProductMapping(ExpenseProductMapping expenseProductMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExpenseProductMappingRepository expenseProductMappingRepository = _DataRepositoryFactory.GetDataRepository<IExpenseProductMappingRepository>();

                ExpenseProductMapping updatedEntity = null;

                if (expenseProductMapping.ExpenseProductId == 0)
                    updatedEntity = expenseProductMappingRepository.Add(expenseProductMapping);
                else
                    updatedEntity = expenseProductMappingRepository.Update(expenseProductMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteExpenseProductMapping(int expenseProductId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExpenseProductMappingRepository expenseProductMappingRepository = _DataRepositoryFactory.GetDataRepository<IExpenseProductMappingRepository>();

                expenseProductMappingRepository.Remove(expenseProductId);
            });
        }

        public ExpenseProductMapping GetExpenseProductMapping(int expenseProductId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExpenseProductMappingRepository expenseProductMappingRepository = _DataRepositoryFactory.GetDataRepository<IExpenseProductMappingRepository>();

                ExpenseProductMapping expenseProductMappingEntity = expenseProductMappingRepository.Get(expenseProductId);
                if (expenseProductMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ExpenseProductMapping with ID of {0} is not in database", expenseProductId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return expenseProductMappingEntity;
            });
        }

        public ExpenseProductMappingData[] GetAllExpenseProductMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExpenseProductMappingRepository expenseProductMappingRepository = _DataRepositoryFactory.GetDataRepository<IExpenseProductMappingRepository>();


                List<ExpenseProductMappingData> expenseProductMapping = new List<ExpenseProductMappingData>();
                IEnumerable<ExpenseProductMappingInfo> expenseProductMappingInfos = expenseProductMappingRepository.GetExpenseProductMappings().ToArray();
               
                foreach (var expenseProductMappingInfo in expenseProductMappingInfos)
                {
                    expenseProductMapping.Add(
                        new ExpenseProductMappingData
                        {
                            ExpenseProductId = expenseProductMappingInfo.ExpenseProductMapping.EntityId,
                            BasisCode = expenseProductMappingInfo.ExpenseProductMapping.BasisCode,
                            BasisName = expenseProductMappingInfo.ExpenseBasis.Name,
                            ProductCode = expenseProductMappingInfo.Product.Code,
                            ProductName = expenseProductMappingInfo.Product.Name,
                            Active = expenseProductMappingInfo.ExpenseProductMapping.Active
                        });
                }

                return expenseProductMapping.ToArray();
            });
        }
        #endregion

        #region ExpenseGLMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ExpenseGLMapping UpdateExpenseGLMapping(ExpenseGLMapping expenseGLMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExpenseGLMappingRepository expenseGLMappingRepository = _DataRepositoryFactory.GetDataRepository<IExpenseGLMappingRepository>();

                ExpenseGLMapping updatedEntity = null;

                if (expenseGLMapping.ExpenseGLId == 0)
                    updatedEntity = expenseGLMappingRepository.Add(expenseGLMapping);
                else
                    updatedEntity = expenseGLMappingRepository.Update(expenseGLMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteExpenseGLMapping(int expenseGLId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExpenseGLMappingRepository expenseGLMappingRepository = _DataRepositoryFactory.GetDataRepository<IExpenseGLMappingRepository>();

                expenseGLMappingRepository.Remove(expenseGLId);
            });
        }

        public ExpenseGLMapping GetExpenseGLMapping(int expenseGLId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExpenseGLMappingRepository expenseGLMappingRepository = _DataRepositoryFactory.GetDataRepository<IExpenseGLMappingRepository>();

                ExpenseGLMapping expenseGLMappingEntity = expenseGLMappingRepository.Get(expenseGLId);
                if (expenseGLMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ExpenseGLMapping with ID of {0} is not in database", expenseGLId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return expenseGLMappingEntity;
            });
        }

        public ExpenseGLMappingData[] GetAllExpenseGLMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExpenseGLMappingRepository expenseGLMappingRepository = _DataRepositoryFactory.GetDataRepository<IExpenseGLMappingRepository>();


                List<ExpenseGLMappingData> expenseGLMapping = new List<ExpenseGLMappingData>();
                IEnumerable<ExpenseGLMappingInfo> expenseGLMappingInfos = expenseGLMappingRepository.GetExpenseGLMappings().ToArray();

                foreach (var expenseGLMappingInfo in expenseGLMappingInfos)
                {
                    expenseGLMapping.Add(
                        new ExpenseGLMappingData
                        {
                            ExpenseGLId = expenseGLMappingInfo.ExpenseGLMapping.EntityId,
                            BasisCode = expenseGLMappingInfo.ExpenseGLMapping.BasisCode,
                            BasisName = expenseGLMappingInfo.ExpenseBasis.Name,
                            GLCode=expenseGLMappingInfo.ExpenseGLMapping.GLCode,
                            GLName = expenseGLMappingInfo.GLDefinition != null ? expenseGLMappingInfo.GLDefinition.Description : string.Empty,    
                            //GLName = string.Empty,   
                            Active = expenseGLMappingInfo.ExpenseGLMapping.Active
                        });
                }

                return expenseGLMapping.ToArray();
            });
        }
        #endregion

        #region ExpenseRawBasis operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ExpenseRawBasis UpdateExpenseRawBasis(ExpenseRawBasis expenseRawBasis)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExpenseRawBasisRepository expenseRawBasisRepository = _DataRepositoryFactory.GetDataRepository<IExpenseRawBasisRepository>();

                ExpenseRawBasis updatedEntity = null;

                if (expenseRawBasis.ExpenseRawBasisId == 0)
                    updatedEntity = expenseRawBasisRepository.Add(expenseRawBasis);
                else
                    updatedEntity = expenseRawBasisRepository.Update(expenseRawBasis);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteExpenseRawBasis(int expenseRawBasisId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExpenseRawBasisRepository expenseRawBasisRepository = _DataRepositoryFactory.GetDataRepository<IExpenseRawBasisRepository>();

                expenseRawBasisRepository.Remove(expenseRawBasisId);
            });
        }

        public ExpenseRawBasis GetExpenseRawBasis(int expenseRawBasisId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExpenseRawBasisRepository expenseRawBasisRepository = _DataRepositoryFactory.GetDataRepository<IExpenseRawBasisRepository>();

                ExpenseRawBasis expenseRawBasisEntity = expenseRawBasisRepository.Get(expenseRawBasisId);
                if (expenseRawBasisEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ExpenseRawBasis with ID of {0} is not in database", expenseRawBasisId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return expenseRawBasisEntity;
            });
        }

        public ExpenseRawBasisData[] GetAllExpenseRawBasisInfo()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExpenseRawBasisRepository expenseRawBasisRepository = _DataRepositoryFactory.GetDataRepository<IExpenseRawBasisRepository>();


                List<ExpenseRawBasisData> expenseRawBasis = new List<ExpenseRawBasisData>();
                IEnumerable<ExpenseRawBasisInfo> expenseRawBasisInfos = expenseRawBasisRepository.GetExpenseRawBasis().ToArray();

                foreach (var expenseRawBasisInfo in expenseRawBasisInfos)
                {
                    expenseRawBasis.Add(
                        new ExpenseRawBasisData
                        {
                            ExpenseRawBasisId = expenseRawBasisInfo.ExpenseRawBasis.EntityId,
                            BasisCode = expenseRawBasisInfo.ExpenseRawBasis.BasisCode,
                            BasisName = expenseRawBasisInfo.ExpenseBasis.Name,
                            MISCode = expenseRawBasisInfo.CostCentre.Code,
                            MISName = expenseRawBasisInfo.CostCentre.Name,
                            Weight=   expenseRawBasisInfo.ExpenseRawBasis.Weight,  
                            Active = expenseRawBasisInfo.ExpenseRawBasis.Active
                        });
                }

                return expenseRawBasis.ToArray();
            });
        }
        #endregion

        #region StaffCost operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public StaffCost UpdateStaffCost(StaffCost staffCost)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IStaffCostRepository staffCostRepository = _DataRepositoryFactory.GetDataRepository<IStaffCostRepository>();

                StaffCost updatedEntity = null;

                if (staffCost.StaffCostId == 0)
                    updatedEntity = staffCostRepository.Add(staffCost);
                else
                    updatedEntity = staffCostRepository.Update(staffCost);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteStaffCost(int staffCostId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IStaffCostRepository staffCostRepository = _DataRepositoryFactory.GetDataRepository<IStaffCostRepository>();

                staffCostRepository.Remove(staffCostId);
            });
        }

        public StaffCost GetStaffCost(int staffCostId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IStaffCostRepository staffCostRepository = _DataRepositoryFactory.GetDataRepository<IStaffCostRepository>();

                StaffCost staffCostEntity = staffCostRepository.Get(staffCostId);
                if (staffCostEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("StaffCost with ID of {0} is not in database", staffCostId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return staffCostEntity;
            });
        }

        public StaffCostData[] GetAllStaffCosts()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IStaffCostRepository staffCostRepository = _DataRepositoryFactory.GetDataRepository<IStaffCostRepository>();


                List<StaffCostData> staffCost = new List<StaffCostData>();
                IEnumerable<StaffCostInfo> staffCostInfos = staffCostRepository.GetStaffCosts().ToArray();

                foreach (var staffCostInfo in staffCostInfos)
                {
                    staffCost.Add(
                        new StaffCostData
                        {
                            StaffCostId = staffCostInfo.StaffCost.EntityId,
                            EmployeeCode = staffCostInfo.StaffCost.EmployeeCode,
                            EmployeeName = staffCostInfo.StaffCost.EmployeeName,
                            Level = staffCostInfo.StaffCost.Level,
                            Amount = staffCostInfo.StaffCost.Amount,
                            BranchCode = staffCostInfo.StaffCost.BranchCode,
                            MISCode=staffCostInfo.CostCentre.Code,
                            MISName=staffCostInfo.CostCentre.Name,
                            Active = staffCostInfo.StaffCost.Active
                        });
                }

                return staffCost.ToArray();
            });
        }
        #endregion

        #region OpexManagementTree operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public OpexManagementTree UpdateOpexManagementTree(OpexManagementTree opexMgtTreeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexManagementTreeRepository opexMgtTreeIdRepository = _DataRepositoryFactory.GetDataRepository<IOpexManagementTreeRepository>();

                OpexManagementTree updatedEntity = null;

                if (opexMgtTreeId.OpexMgtTreeId == 0)
                    updatedEntity = opexMgtTreeIdRepository.Add(opexMgtTreeId);
                else
                    updatedEntity = opexMgtTreeIdRepository.Update(opexMgtTreeId);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOpexManagementTree(int opexMgtTreeIdId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexManagementTreeRepository opexMgtTreeIdRepository = _DataRepositoryFactory.GetDataRepository<IOpexManagementTreeRepository>();

                opexMgtTreeIdRepository.Remove(opexMgtTreeIdId);
            });
        }

        public OpexManagementTree GetOpexManagementTree(int opexMgtTreeIdId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexManagementTreeRepository opexMgtTreeIdRepository = _DataRepositoryFactory.GetDataRepository<IOpexManagementTreeRepository>();

                OpexManagementTree opexMgtTreeIdEntity = opexMgtTreeIdRepository.Get(opexMgtTreeIdId);
                if (opexMgtTreeIdEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OpexManagementTree with ID of {0} is not in database", opexMgtTreeIdId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return opexMgtTreeIdEntity;
            });
        }

        public OpexManagementTreeData[] GetAllOpexManagementTrees()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexManagementTreeRepository opexMgtTreeIdRepository = _DataRepositoryFactory.GetDataRepository<IOpexManagementTreeRepository>();


                List<OpexManagementTreeData> opexMgtTreeId = new List<OpexManagementTreeData>();
                IEnumerable<OpexManagementTreeInfo> opexMgtTreeIdInfos = opexMgtTreeIdRepository.GetOpexManagementTrees().ToArray();

                foreach (var opexMgtTreeIdInfo in opexMgtTreeIdInfos)
                {
                    opexMgtTreeId.Add(
                        new OpexManagementTreeData
                        {
                            OpexMgtTreeId = opexMgtTreeIdInfo.OpexManagementTree.EntityId,
                            CostCentreMISCode = opexMgtTreeIdInfo.CostCentre.Code,
                            TeamDefinitionCode = opexMgtTreeIdInfo.TeamDefinition.Code,
                            TeamDefinitionName = opexMgtTreeIdInfo.TeamDefinition.Name,
                            TeamCode = opexMgtTreeIdInfo.Team.Code,
                            TeamName = opexMgtTreeIdInfo.Team.Name,
                            AccountOfficerDefinitionCode = opexMgtTreeIdInfo.AccountOfficerDefinition.Code,
                            AccountOfficerDefinitionName = opexMgtTreeIdInfo.AccountOfficerDefinition.Name,
                            AccountOfficerCode = opexMgtTreeIdInfo.AccountOfficerMis.Code,
                            AccountOfficerName=opexMgtTreeIdInfo.AccountOfficerMis.Name,
                            Ratio = opexMgtTreeIdInfo.OpexManagementTree.Ratio,
                            Active = opexMgtTreeIdInfo.OpexManagementTree.Active
                        });
                }

                return opexMgtTreeId.ToArray();
            });
        }
        #endregion

        #region ActivityBase operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ActivityBase UpdateActivityBase(ActivityBase activityBase)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IActivityBaseRepository activityBaseRepository = _DataRepositoryFactory.GetDataRepository<IActivityBaseRepository>();

                ActivityBase updatedEntity = null;

                if (activityBase.ActivityBaseId == 0)
                    updatedEntity = activityBaseRepository.Add(activityBase);
                else
                    updatedEntity = activityBaseRepository.Update(activityBase);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteActivityBase(int activityBaseId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IActivityBaseRepository activityBaseRepository = _DataRepositoryFactory.GetDataRepository<IActivityBaseRepository>();

                activityBaseRepository.Remove(activityBaseId);
            });
        }

        public ActivityBase GetActivityBase(int activityBaseId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IActivityBaseRepository activityBaseRepository = _DataRepositoryFactory.GetDataRepository<IActivityBaseRepository>();

                ActivityBase activityBaseEntity = activityBaseRepository.Get(activityBaseId);
                if (activityBaseEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ActivityBase with ID of {0} is not in database", activityBaseId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return activityBaseEntity;
            });
        }

        public ActivityBase[] GetAllActivityBases()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IActivityBaseRepository activityBaseRepository = _DataRepositoryFactory.GetDataRepository<IActivityBaseRepository>();
                IEnumerable<ActivityBase> activityBase = activityBaseRepository.Get().ToArray();

                 return activityBase.ToArray();
            });
        }
        #endregion

        #region ActivityBaseRatio operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ActivityBaseRatio UpdateActivityBaseRatio(ActivityBaseRatio activityBaseRatio)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IActivityBaseRatioRepository activityBaseRatioRepository = _DataRepositoryFactory.GetDataRepository<IActivityBaseRatioRepository>();

                ActivityBaseRatio updatedEntity = null;

                if (activityBaseRatio.ActivityBaseRatioId == 0)
                    updatedEntity = activityBaseRatioRepository.Add(activityBaseRatio);
                else
                    updatedEntity = activityBaseRatioRepository.Update(activityBaseRatio);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteActivityBaseRatio(int activityBaseRatioId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IActivityBaseRatioRepository activityBaseRatioRepository = _DataRepositoryFactory.GetDataRepository<IActivityBaseRatioRepository>();

                activityBaseRatioRepository.Remove(activityBaseRatioId);
            });
        }

        public ActivityBaseRatio GetActivityBaseRatio(int activityBaseRatioId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IActivityBaseRatioRepository activityBaseRatioRepository = _DataRepositoryFactory.GetDataRepository<IActivityBaseRatioRepository>();

                ActivityBaseRatio activityBaseRatioEntity = activityBaseRatioRepository.Get(activityBaseRatioId);
                if (activityBaseRatioEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ActivityBaseRatio with ID of {0} is not in database", activityBaseRatioId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return activityBaseRatioEntity;
            });
        }

        public ActivityBaseRatio[] GetAllActivityBaseRatios()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IActivityBaseRatioRepository activityBaseRatioRepository = _DataRepositoryFactory.GetDataRepository<IActivityBaseRatioRepository>();
                IEnumerable<ActivityBaseRatio> activityBaseRatio = activityBaseRatioRepository.Get().ToArray();

                return activityBaseRatio.ToArray();
            });
        }
        #endregion

        #region OpexMISReplacement operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public OpexMISReplacement UpdateOpexMISReplacement(OpexMISReplacement opexMISReplacementId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexMISReplacementRepository opexMISReplacementIdRepository = _DataRepositoryFactory.GetDataRepository<IOpexMISReplacementRepository>();

                OpexMISReplacement updatedEntity = null;

                if (opexMISReplacementId.OpexMISReplacementId == 0)
                    updatedEntity = opexMISReplacementIdRepository.Add(opexMISReplacementId);
                else
                    updatedEntity = opexMISReplacementIdRepository.Update(opexMISReplacementId);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOpexMISReplacement(int opexMISReplacementId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexMISReplacementRepository opexMISReplacementIdRepository = _DataRepositoryFactory.GetDataRepository<IOpexMISReplacementRepository>();

                opexMISReplacementIdRepository.Remove(opexMISReplacementId);
            });
        }

        public OpexMISReplacement GetOpexMISReplacement(int opexMISReplacementId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexMISReplacementRepository opexMISReplacementIdRepository = _DataRepositoryFactory.GetDataRepository<IOpexMISReplacementRepository>();

                OpexMISReplacement opexMISReplacementIdEntity = opexMISReplacementIdRepository.Get(opexMISReplacementId);
                if (opexMISReplacementIdEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OpexMISReplacement with ID of {0} is not in database", opexMISReplacementId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return opexMISReplacementIdEntity;
            });
        }

        public OpexMISReplacementData[] GetAllOpexMISReplacements()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexMISReplacementRepository opexMISReplacementIdRepository = _DataRepositoryFactory.GetDataRepository<IOpexMISReplacementRepository>();


                List<OpexMISReplacementData> opexMISReplacementId = new List<OpexMISReplacementData>();
                IEnumerable<OpexMISReplacementInfo> opexMISReplacementIdInfos = opexMISReplacementIdRepository.GetOpexMISReplacements().ToArray();

                foreach (var opexMISReplacementIdInfo in opexMISReplacementIdInfos)
                {
                    opexMISReplacementId.Add(
                        new OpexMISReplacementData
                        {
                            OpexMISReplacementId = opexMISReplacementIdInfo.OpexMISReplacement.EntityId,
                            OldMISCode=opexMISReplacementIdInfo.OpexMISReplacement.OldMISCode,
                            MISCode=opexMISReplacementIdInfo.CostCentre.Code,
                            MISName=opexMISReplacementIdInfo.CostCentre.Name,
                            Active = opexMISReplacementIdInfo.OpexMISReplacement.Active
                        });
                }

                return opexMISReplacementId.ToArray();
            });
        }
        #endregion

        #region OpexBusinessRule operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public OpexBusinessRule UpdateOpexBusinessRule(OpexBusinessRule opexBusinessRule)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexBusinessRuleRepository opexBusinessRuleRepository = _DataRepositoryFactory.GetDataRepository<IOpexBusinessRuleRepository>();

                OpexBusinessRule updatedEntity = null;

                if (opexBusinessRule.OpexBusinessRuleId == 0)
                    updatedEntity = opexBusinessRuleRepository.Add(opexBusinessRule);
                else
                    updatedEntity = opexBusinessRuleRepository.Update(opexBusinessRule);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOpexBusinessRule(int opexBusinessRuleId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexBusinessRuleRepository opexBusinessRuleRepository = _DataRepositoryFactory.GetDataRepository<IOpexBusinessRuleRepository>();

                opexBusinessRuleRepository.Remove(opexBusinessRuleId);
            });
        }

        public OpexBusinessRule GetOpexBusinessRule(int opexBusinessRuleId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexBusinessRuleRepository opexBusinessRuleRepository = _DataRepositoryFactory.GetDataRepository<IOpexBusinessRuleRepository>();

                OpexBusinessRule opexBusinessRuleEntity = opexBusinessRuleRepository.Get(opexBusinessRuleId);
                if (opexBusinessRuleEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OpexBusinessRule with ID of {0} is not in database", opexBusinessRuleId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return opexBusinessRuleEntity;
            });
        }

        public OpexBusinessRule[] GetAllOpexBusinessRules()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexBusinessRuleRepository opexBusinessRuleRepository = _DataRepositoryFactory.GetDataRepository<IOpexBusinessRuleRepository>();
                IEnumerable<OpexBusinessRule> opexBusinessRule = opexBusinessRuleRepository.Get().ToArray();

                return opexBusinessRule.ToArray();
            });
        }
        #endregion

        #region OpexAbcExemption operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public OpexAbcExemption UpdateOpexAbcExemption(OpexAbcExemption opexAbcExemption)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexAbcExemptionRepository opexAbcExemptionRepository = _DataRepositoryFactory.GetDataRepository<IOpexAbcExemptionRepository>();

                OpexAbcExemption updatedEntity = null;

                if (opexAbcExemption.OpexAbcExemptionId == 0)
                    updatedEntity = opexAbcExemptionRepository.Add(opexAbcExemption);
                else
                    updatedEntity = opexAbcExemptionRepository.Update(opexAbcExemption);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOpexAbcExemption(int opexAbcExemptionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexAbcExemptionRepository opexAbcExemptionRepository = _DataRepositoryFactory.GetDataRepository<IOpexAbcExemptionRepository>();

                opexAbcExemptionRepository.Remove(opexAbcExemptionId);
            });
        }

        public OpexAbcExemption GetOpexAbcExemption(int opexAbcExemptionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexAbcExemptionRepository opexAbcExemptionRepository = _DataRepositoryFactory.GetDataRepository<IOpexAbcExemptionRepository>();

                OpexAbcExemption opexAbcExemptionEntity = opexAbcExemptionRepository.Get(opexAbcExemptionId);
                if (opexAbcExemptionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OpexAbcExemption with ID of {0} is not in database", opexAbcExemptionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return opexAbcExemptionEntity;
            });
        }

        //public OpexAbcExemption[] GetAllOpexAbcExemptions()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IOpexAbcExemptionRepository opexAbcExemptionRepository = _DataRepositoryFactory.GetDataRepository<IOpexAbcExemptionRepository>();

        //        IEnumerable<OpexAbcExemption> opexAbcExemption = opexAbcExemptionRepository.Get().ToArray();

        //        return opexAbcExemption.ToArray();
        //    });
        //}

        public OpexAbcExemptionData[] GetAllOpexAbcExemptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexAbcExemptionRepository opexAbcExemptionRepository = _DataRepositoryFactory.GetDataRepository<IOpexAbcExemptionRepository>();


                List<OpexAbcExemptionData> opexAbcExemption = new List<OpexAbcExemptionData>();
                IEnumerable<OpexAbcExemptionInfo> opexAbcExemptionInfos = opexAbcExemptionRepository.GetOpexAbcExemptions().ToArray();

                foreach (var opexAbcExemptionInfo in opexAbcExemptionInfos)
                {
                    opexAbcExemption.Add(
                        new OpexAbcExemptionData
                        {
                            OpexAbcExemptionId = opexAbcExemptionInfo.OpexAbcExemption.EntityId,
                            MisCode = opexAbcExemptionInfo.OpexAbcExemption.MisCode,
                            MisName = opexAbcExemptionInfo.CostCentre.Name,
                            CompanyCode = opexAbcExemptionInfo.OpexAbcExemption.CompanyCode,
                            Active = opexAbcExemptionInfo.OpexAbcExemption.Active
                        });
                }

                return opexAbcExemption.ToArray();
            });
        }
        #endregion

        #region OpexRawExpense operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public OpexRawExpense UpdateOpexRawExpense(OpexRawExpense OpexRawExpense)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexRawExpenseRepository OpexRawExpenseRepository = _DataRepositoryFactory.GetDataRepository<IOpexRawExpenseRepository>();

                OpexRawExpense updatedEntity = null;

                if (OpexRawExpense.OpexRawExpenseId == 0)
                    updatedEntity = OpexRawExpenseRepository.Add(OpexRawExpense);
                else
                    updatedEntity = OpexRawExpenseRepository.Update(OpexRawExpense);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOpexRawExpense(int OpexRawExpenseId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexRawExpenseRepository OpexRawExpenseRepository = _DataRepositoryFactory.GetDataRepository<IOpexRawExpenseRepository>();

                OpexRawExpenseRepository.Remove(OpexRawExpenseId);
            });
        }

        public OpexRawExpense GetOpexRawExpense(int OpexRawExpenseId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexRawExpenseRepository OpexRawExpenseRepository = _DataRepositoryFactory.GetDataRepository<IOpexRawExpenseRepository>();

                OpexRawExpense OpexRawExpenseEntity = OpexRawExpenseRepository.Get(OpexRawExpenseId);
                if (OpexRawExpenseEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OpexRawExpense with ID of {0} is not in database", OpexRawExpenseId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return OpexRawExpenseEntity;
            });
        }

        public OpexRawExpense[] GetAllOpexRawExpenses()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexRawExpenseRepository OpexRawExpenseRepository = _DataRepositoryFactory.GetDataRepository<IOpexRawExpenseRepository>();

                IEnumerable<OpexRawExpense> OpexRawExpense = OpexRawExpenseRepository.Get().ToArray();

                return OpexRawExpense.ToArray();
            });
        }

        #endregion

        #region OpexGLMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public OpexGLMapping UpdateOpexGLMapping(OpexGLMapping opexGLMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexGLMappingRepository OpexGLMappingRepository = _DataRepositoryFactory.GetDataRepository<IOpexGLMappingRepository>();

                OpexGLMapping updatedEntity = null;

                if (opexGLMapping.GLMappingId == 0)
                    updatedEntity = OpexGLMappingRepository.Add(opexGLMapping);
                else
                    updatedEntity = OpexGLMappingRepository.Update(opexGLMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOpexGLMapping(int opexGLMappingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexGLMappingRepository OpexGLMappingRepository = _DataRepositoryFactory.GetDataRepository<IOpexGLMappingRepository>();

                OpexGLMappingRepository.Remove(opexGLMappingId);
            });
        }

        public OpexGLMapping GetOpexGLMapping(int opexGLMappingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexGLMappingRepository OpexGLMappingRepository = _DataRepositoryFactory.GetDataRepository<IOpexGLMappingRepository>();

                OpexGLMapping OpexGLMappingEntity = OpexGLMappingRepository.Get(opexGLMappingId);
                if (OpexGLMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OpexGLMapping with ID of {0} is not in database", opexGLMappingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return OpexGLMappingEntity;
            });
        }

        public OpexGLMapping[] GetAllOpexGLMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexGLMappingRepository OpexGLMappingRepository = _DataRepositoryFactory.GetDataRepository<IOpexGLMappingRepository>();

                IEnumerable<OpexGLMapping> OpexGLMapping = OpexGLMappingRepository.Get().ToArray();

                return OpexGLMapping.ToArray();
            });
        }

        public KeyValueData[] GetUnMappedOpexGLs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                var results = new List<KeyValueData>();

                var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

                using (var con = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand("spp_mpr_opex_getunmappedgl", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                   
                    con.Open();

                   var  reader = cmd.ExecuteReader();

                   while (reader.Read())
                   {
                       var data = new KeyValueData();

                       if (reader["GLCode"] != DBNull.Value)
                           data.Key = reader["GLCode"].ToString();

                       if (reader["Description"] != DBNull.Value)
                           data.Value = reader["Description"].ToString();

                       results.Add(data);
                   }

                    con.Close();
                }
            
                return results.ToArray();
            });
        }

        #endregion

        #region OpexReport operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public OpexReport UpdateOpexReport(OpexReport OpexReport)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexReportRepository OpexReportRepository = _DataRepositoryFactory.GetDataRepository<IOpexReportRepository>();

                OpexReport updatedEntity = null;

                if (OpexReport.ReportId == 0)
                    updatedEntity = OpexReportRepository.Add(OpexReport);
                else
                    updatedEntity = OpexReportRepository.Update(OpexReport);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOpexReport(int OpexReportId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexReportRepository OpexReportRepository = _DataRepositoryFactory.GetDataRepository<IOpexReportRepository>();

                OpexReportRepository.Remove(OpexReportId);
            });
        }

        public OpexReport GetOpexReport(int OpexReportId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexReportRepository OpexReportRepository = _DataRepositoryFactory.GetDataRepository<IOpexReportRepository>();

                OpexReport OpexReportEntity = OpexReportRepository.Get(OpexReportId);
                if (OpexReportEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OpexReport with ID of {0} is not in database", OpexReportId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return OpexReportEntity;
            });
        }

        public OpexReportData[] GetAllOpexReports()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexReportRepository OpexReportRepository = _DataRepositoryFactory.GetDataRepository<IOpexReportRepository>();


                List<OpexReportData> opexReport = new List<OpexReportData>();
                IEnumerable<OpexReportInfo> opexReportInfos = OpexReportRepository.GetOpexReports().ToArray();

                foreach (var opexReportInfo in opexReportInfos)
                {
                    opexReport.Add(
                        new OpexReportData
                        {
                            ReportId = opexReportInfo.OpexReport.EntityId,
                            GLCode = opexReportInfo.OpexReport.GLCode,
                            GLDescription = opexReportInfo.GLDefinition!=null? opexReportInfo.GLDefinition.Description:string.Empty,
                            BranchCode = opexReportInfo.OpexReport.BranchCode,
                            BranchName = opexReportInfo.Branch!=null? opexReportInfo.Branch.Name:string.Empty,
                            Amount = opexReportInfo.OpexReport.Amount,
                            Currency = opexReportInfo.OpexReport.Currency,
                            CompanyCode = opexReportInfo.OpexReport.CompanyCode,
                            RunDate=opexReportInfo.OpexReport.RunDate,
                            Active = opexReportInfo.OpexReport.Active
                        });
                }

                return opexReport.ToArray();
            });
        }

        
        
        #endregion

        #region OpexGLBasis operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public OpexGLBasis UpdateOpexGLBasis(OpexGLBasis opexGLBasis)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexGLBasisRepository opexGLBasisRepository = _DataRepositoryFactory.GetDataRepository<IOpexGLBasisRepository>();

                OpexGLBasis updatedEntity = null;

                if (opexGLBasis.OpexGLBasisId == 0)
                    updatedEntity = opexGLBasisRepository.Add(opexGLBasis);
                else
                    updatedEntity = opexGLBasisRepository.Update(opexGLBasis);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOpexGLBasis(int opexGLBasisId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexGLBasisRepository opexGLBasisRepository = _DataRepositoryFactory.GetDataRepository<IOpexGLBasisRepository>();

                opexGLBasisRepository.Remove(opexGLBasisId);
            });
        }

        public OpexGLBasis GetOpexGLBasis(int opexGLBasisId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexGLBasisRepository opexGLBasisRepository = _DataRepositoryFactory.GetDataRepository<IOpexGLBasisRepository>();

                OpexGLBasis opexGLBasisEntity = opexGLBasisRepository.Get(opexGLBasisId);
                if (opexGLBasisEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OpexGLBasis with ID of {0} is not in database", opexGLBasisId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return opexGLBasisEntity;
            });
        }

        public OpexGLBasis[] GetAllOpexGLBasisInfo()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexGLBasisRepository opexGLBasisRepository = _DataRepositoryFactory.GetDataRepository<IOpexGLBasisRepository>();

                IEnumerable<OpexGLBasis> opexGLBasis = opexGLBasisRepository.Get().ToArray();

                return opexGLBasis.ToArray();
            });
        }

        #endregion

        #region OpexBasisMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public OpexBasisMapping UpdateOpexBasisMapping(OpexBasisMapping opexBasisMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexBasisMappingRepository opexBasisMappingRepository = _DataRepositoryFactory.GetDataRepository<IOpexBasisMappingRepository>();

                OpexBasisMapping updatedEntity = null;

                if (opexBasisMapping.OpexBasisMappingId == 0)
                    updatedEntity = opexBasisMappingRepository.Add(opexBasisMapping);
                else
                    updatedEntity = opexBasisMappingRepository.Update(opexBasisMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOpexBasisMapping(int opexBasisMappingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexBasisMappingRepository opexBasisMappingRepository = _DataRepositoryFactory.GetDataRepository<IOpexBasisMappingRepository>();

                opexBasisMappingRepository.Remove(opexBasisMappingId);
            });
        }

        public OpexBasisMapping GetOpexBasisMapping(int opexBasisMappingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexBasisMappingRepository opexBasisMappingRepository = _DataRepositoryFactory.GetDataRepository<IOpexBasisMappingRepository>();

                OpexBasisMapping opexBasisMappingEntity = opexBasisMappingRepository.Get(opexBasisMappingId);
                if (opexBasisMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OpexBasisMapping with ID of {0} is not in database", opexBasisMappingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return opexBasisMappingEntity;
            });
        }

        public OpexBasisMapping[] GetAllOpexBasisMappingInfo()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOpexBasisMappingRepository opexBasisMappingRepository = _DataRepositoryFactory.GetDataRepository<IOpexBasisMappingRepository>();

                IEnumerable<OpexBasisMapping> opexBasisMapping = opexBasisMappingRepository.Get().ToArray();

                return opexBasisMapping.ToArray();
            });
        }

        #endregion

        #region Helper

        protected override bool AllowAccessToOperation(string solutionName, List<string> groupNames)
        {
            systemCoreData.IUserRoleRepository accountRoleRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IUserRoleRepository>();
            var accountRoles = accountRoleRepository.GetUserRoleInfo(solutionName, _LoginName, groupNames);

            if (accountRoles == null || accountRoles.Count() <= 0)
            {
                AuthorizationValidationException ex = new AuthorizationValidationException(string.Format("Access denied for {0}.", _LoginName));
                throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
            }

            return true;
        }

        private SetUp GetSetup()
        {
            ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();

            var setup = setupRepository.Get().FirstOrDefault();
            if (setup == null)
            {
                NotFoundException ex = new NotFoundException(string.Format("MPR setup information is not in database"));
                throw new FaultException<NotFoundException>(ex, ex.Message);
            }

            return setup;
        }
        

        #endregion

    }
}
