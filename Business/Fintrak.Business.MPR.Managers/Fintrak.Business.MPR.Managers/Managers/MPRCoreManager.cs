using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Transactions;
using Fintrak.Data.MPR.Contracts;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Business.MPR.Contracts;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Data;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.MPR.Entities;
using systemCoreFramework = Fintrak.Shared.SystemCore.Framework;
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Business.MPR.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class MPRCoreManager : ManagerBase, IMPRCoreService
    {
        public MPRCoreManager()
        {
        }

        public MPRCoreManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }
        /// <summary>
        /// </summary>
        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        const string SOLUTION_NAME = "FIN_MPR";
        const string SOLUTION_ALIAS = "MPR";
        const string MODULE_NAME = "FIN_MPR_CORE";
        const string MODULE_ALIAS = "MPR CORE";

        const string GROUP_ADMINISTRATOR = "Administrator";
        const string GROUP_USER = "User";
        const string GROUP_SUPER_BUSINESS = "Super Business";
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
                            Description = "For MPR solution unlimited users",
                            Type = systemCoreFramework.RoleType.Application,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        roleRepository.Add(adminRole);

                        var userRole = new systemCoreEntities.Role()
                        {
                            SolutionId = solution.SolutionId,
                            Name = GROUP_USER,
                            Description = "For MPR solution limited users",
                            Type = systemCoreFramework.RoleType.Application,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        roleRepository.Add(userRole);


                        var menuIndex = 0;

                        //register menu
                        var root = new systemCoreEntities.Menu()
                        {
                            Name = "MPR",
                            Alias = "MPR",
                            Action = "",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "mpr_image",
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
                            RoleId = userRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        var teamManager = new systemCoreEntities.Menu()
                        {
                            Name = "TEAM_MANAGER",
                            Alias = "Team Manager",
                            Action = "",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "team_manager_image",
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

                        teamManager = menuRepository.Add(teamManager);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = teamManager.EntityId,
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
                            Name = "CLASSIFICATION_TYPE",
                            Alias = "Classification Types",
                            Action = "CLASSIFICATION_TYPE",
                            ActionUrl = "mpr-teamclassificationtype-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
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
                            Name = "CLASSIFICATION",
                            Alias = "Classification",
                            Action = "CLASSIFICATION",
                            ActionUrl = "mpr-teamclassification-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
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
                            Name = "TEAM_DEFINITION",
                            Alias = "Team Definition",
                            Action = "TEAM_DEFINITION",
                            ActionUrl = "mpr-teamdefinition-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
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
                            Name = "TEAMS",
                            Alias = "Teams",
                            Action = "TEAMS",
                            ActionUrl = "mpr-team-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
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
                            Name = "OFFICERS_PROFILE",
                            Alias = "Officer's Profile",
                            Action = "OFFICERS_PROFILE",
                            ActionUrl = "mpr-accountofficerdetail-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
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
                            Name = "USER_MIS",
                            Alias = "User's MIS",
                            Action = "USER_MIS",
                            ActionUrl = "mpr-usermis-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
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
                            Name = "BRANCH_DEFAULT_MIS",
                            Alias = "Branch Default MIS",
                            Action = "BRANCH_DEFAULT_MIS",
                            ActionUrl = "mpr-branchdefaultmis-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
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
                            Name = "MANAGEMENT_TREE",
                            Alias = "Management Tree",
                            Action = "MANAGEMENT_TREE",
                            ActionUrl = "mpr-managementtree-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
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
                            Name = "ACCOUNT_MIS",
                            Alias = "Account MIS",
                            Action = "ACCOUNT_MIS",
                            ActionUrl = "mpr-accountmis-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
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
                            Name = "MIS_REPLACEMENT",
                            Alias = "MIS Replacement",
                            Action = "MIS_REPLACEMENT",
                            ActionUrl = "mpr-misreplacement-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
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
                            Name = "MPR_SETUP",
                            Alias = "General Settings",
                            Action = "MPR_SETUP",
                            ActionUrl = "mpr-mprsetup-edit",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
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
                            Name = "CUSTOMER_ACCOUNT",
                            Alias = "Customer Account",
                            Action = "CUSTOMER_ACCOUNT",
                            ActionUrl = "mpr-custaccount-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
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
                            Name = "MEMO_UNITS",
                            Alias = "Memo Units",
                            Action = "MEMO_UNITS",
                            ActionUrl = "mpr-memounit-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
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

        // #region UserMIS operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public UserMIS UpdateUserMIS(UserMIS userMIS)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IUserMISRepository userMISRepository = _DataRepositoryFactory.GetDataRepository<IUserMISRepository>();

        //         UserMIS updatedEntity = null;

        //         if (userMIS.UserMISId == 0)
        //         {
        //             updatedEntity = userMISRepository.Add(userMIS);
        //         }
        //         else
        //             updatedEntity = userMISRepository.Update(userMIS);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteUserMIS(int userMISId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IUserMISRepository userMISRepository = _DataRepositoryFactory.GetDataRepository<IUserMISRepository>();

        //         userMISRepository.Remove(userMISId);
        //     });
        // }

        // public UserMIS GetUserMIS(int userMISId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IUserMISRepository userMISRepository = _DataRepositoryFactory.GetDataRepository<IUserMISRepository>();

        //         UserMIS userMISEntity = userMISRepository.Get(userMISId);
        //         if (userMISEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("UserMIS with ID of {0} is not in database", userMISId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return userMISEntity;
        //     });
        // }

        // public UserMIS GetUserMISByLoginID(string loginID)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IUserMISRepository userMISRepository = _DataRepositoryFactory.GetDataRepository<IUserMISRepository>();

        //         var setUp = GetSetup();

        //         UserMIS userMISEntity = userMISRepository.Get().Where(c => c.LoginID == loginID).FirstOrDefault();

        //         return userMISEntity;
        //     });
        // }

        // public UserMIS[] GetAllUserMISs()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IUserMISRepository userMISRepository = _DataRepositoryFactory.GetDataRepository<IUserMISRepository>();

        //         var setup = GetSetup();

        //         IEnumerable<UserMIS> userMIS = userMISRepository.Get().ToArray();

        //         return userMIS.ToArray();
        //     });
        // }


        // #endregion

        // #region UserClassificationMap operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public UserClassificationMap UpdateUserClassificationMap(UserClassificationMap userClassificationMap)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IUserClassificationMapRepository userClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<IUserClassificationMapRepository>();

        //         UserClassificationMap updatedEntity = null;

        //         if (userClassificationMap.UserClassificationMapId == 0)
        //         {
        //             updatedEntity = userClassificationMapRepository.Add(userClassificationMap);
        //         }
        //         else
        //             updatedEntity = userClassificationMapRepository.Update(userClassificationMap);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteUserClassificationMap(int userClassificationMapId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IUserClassificationMapRepository userClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<IUserClassificationMapRepository>();

        //         userClassificationMapRepository.Remove(userClassificationMapId);
        //     });
        // }

        // public UserClassificationMap GetUserClassificationMap(int userClassificationMapId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IUserClassificationMapRepository userClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<IUserClassificationMapRepository>();

        //         UserClassificationMap userClassificationMapEntity = userClassificationMapRepository.Get(userClassificationMapId);
        //         if (userClassificationMapEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("UserClassificationMap with ID of {0} is not in database", userClassificationMapId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return userClassificationMapEntity;
        //     });
        // }

        // public UserClassificationMap[] GetAllUserClassificationMaps(string loginID)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IUserClassificationMapRepository userClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<IUserClassificationMapRepository>();

        //         var setup = GetSetup();

        //         IEnumerable<UserClassificationMap> userClassificationMap = userClassificationMapRepository.GetUserClassificationMaps(loginID).ToArray();

        //         return userClassificationMap.ToArray();
        //     });
        // }


        // #endregion

        // #region TeamDefinition operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public TeamDefinition UpdateTeamDefinition(TeamDefinition teamDefinition)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

        //         TeamDefinition updatedEntity = null;

        //         if (teamDefinition.TeamDefinitionId == 0)
        //         {
        //             teamDefinition.Year = GetSetup().Year;
        //             updatedEntity = teamDefinitionRepository.Add(teamDefinition);
        //         }      
        //         else
        //             updatedEntity = teamDefinitionRepository.Update(teamDefinition);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteTeamDefinition(int teamDefinitionId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

        //         teamDefinitionRepository.Remove(teamDefinitionId);
        //     });
        // }

        // public TeamDefinition GetTeamDefinition(int teamDefinitionId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

        //         TeamDefinition teamDefinitionEntity = teamDefinitionRepository.Get(teamDefinitionId);
        //         if (teamDefinitionEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("TeamDefinition with ID of {0} is not in database", teamDefinitionId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return teamDefinitionEntity;
        //     });
        // }

        // public TeamDefinition GetTeamDefinitionByCode(string code)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

        //         var setUp = GetSetup();

        //         TeamDefinition teamDefinitionEntity = teamDefinitionRepository.Get().Where(c=>c.Code == code && c.Year == setUp.Year).FirstOrDefault();

        //         return teamDefinitionEntity;
        //     });
        // }

        // public TeamDefinition[] GetAllTeamDefinitions()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

        //         var setup = GetSetup();

        //         IEnumerable<TeamDefinition> teamDefinition = teamDefinitionRepository.Get().Where(c => c.Year == setup.Year).ToArray();

        //         return teamDefinition.ToArray();
        //     });
        // }


        // #endregion

        // #region TeamClassificationType operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public TeamClassificationType UpdateTeamClassificationType(TeamClassificationType teamClassificationType)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();

        //         TeamClassificationType updatedEntity = null;

        //         if (teamClassificationType.TeamClassificationTypeId == 0)
        //         {
        //             teamClassificationType.Year = GetSetup().Year;
        //             updatedEntity = teamClassificationTypeRepository.Add(teamClassificationType);
        //         }
        //         else
        //             updatedEntity = teamClassificationTypeRepository.Update(teamClassificationType);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteTeamClassificationType(int teamClassificationTypeId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();

        //         teamClassificationTypeRepository.Remove(teamClassificationTypeId);
        //     });
        // }

        // public TeamClassificationType GetTeamClassificationType(int teamClassificationTypeId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();

        //         TeamClassificationType teamClassificationTypeEntity = teamClassificationTypeRepository.Get(teamClassificationTypeId);
        //         if (teamClassificationTypeEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("TeamClassificationType with ID of {0} is not in database", teamClassificationTypeId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return teamClassificationTypeEntity;
        //     });
        // }

        // public TeamClassificationType[] GetAllTeamClassificationTypes()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();

        //         var setup = GetSetup();

        //         IEnumerable<TeamClassificationType> teamClassificationType = teamClassificationTypeRepository.Get().Where(c => c.Year == setup.Year).ToArray();

        //         return teamClassificationType.ToArray();
        //     });
        // }

        // #endregion

        // #region TeamClassification operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public TeamClassification UpdateTeamClassification(TeamClassification teamClassification)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

        //         TeamClassification updatedEntity = null;

        //         if (teamClassification.TeamClassificationId == 0)
        //         {
        //             teamClassification.Year = GetSetup().Year;
        //             updatedEntity = teamClassificationRepository.Add(teamClassification);
        //         }
        //         else
        //             updatedEntity = teamClassificationRepository.Update(teamClassification);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteTeamClassification(int teamClassificationId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

        //         teamClassificationRepository.Remove(teamClassificationId);
        //     });
        // }

        // public TeamClassification GetTeamClassification(int teamClassificationId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

        //         TeamClassification teamClassificationEntity = teamClassificationRepository.Get(teamClassificationId);
        //         if (teamClassificationEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("TeamClassification with ID of {0} is not in database", teamClassificationId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return teamClassificationEntity;
        //     });
        // }

        // public TeamClassification[] GetAllTeamClassifications( )
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

        //         var setup = GetSetup();

        //         IEnumerable<TeamClassification> teamClassification = teamClassificationRepository.Get().Where(c => c.Year == setup.Year).ToArray();

        //         return teamClassification.ToArray();
        //     });
        // }

        // public TeamClassification[] GetTeamClassifications(string typeCode)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();
        //         ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

        //         var setup = GetSetup();

        //         var classificationType = teamClassificationTypeRepository.Get().Where(c => c.Code == typeCode).FirstOrDefault();

        //         IEnumerable<TeamClassification> teamClassification = null;

        //         if (classificationType != null)
        //             teamClassification = teamClassificationRepository.Get().Where(c => c.Year == setup.Year && c.ClassificationTypeCode == typeCode && c.Level == classificationType.MaximumLevel ).ToArray();

        //         return teamClassification.ToArray();
        //     });
        // }

        // #endregion        

        // #region Team operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public Team UpdateTeam(Team team)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

        //         Team updatedEntity = null;

        //         if (team.TeamId == 0)
        //         {
        //             team.Year = GetSetup().Year;
        //             updatedEntity = teamRepository.Add(team);
        //         }
        //         else
        //             updatedEntity = teamRepository.Update(team);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteTeam(int teamId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

        //         teamRepository.Remove(teamId);
        //     });
        // }

        // public Team GetTeam(int teamId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

        //         Team teamEntity = teamRepository.Get(teamId);
        //         if (teamEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("Team with ID of {0} is not in database", teamId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return teamEntity;
        //     });
        // }

        // public Team[] GetParentTeams(string definitionCode)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();
        //         ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

        //         ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
        //         var setUp = setupRepository.Get().FirstOrDefault();

        //         var teamDefinition = teamDefinitionRepository.Get().Where(c => c.Code == definitionCode && c.Year == setUp.Year).FirstOrDefault();
        //         var parentDefinition = teamDefinitionRepository.Get().Where(c => c.Position == (teamDefinition.Position + 1)).FirstOrDefault();

        //         Team[] teams = teamRepository.Get().Where(c => c.DefinitionCode == parentDefinition.Code && c.Year == setUp.Year).ToArray();

        //         return teams;
        //     });
        // }

        // public Team[] GetTeamByLevel(int level)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();
        //         ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

        //         ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
        //         var setUp = setupRepository.Get().FirstOrDefault();

        //         var teamDefinition = teamDefinitionRepository.Get().Where(c=>c.Position == level).FirstOrDefault();

        //          Team[] teams = null;

        //         if (teamDefinition != null)
        //             teams = teamRepository.Get().Where(c => c.DefinitionCode == teamDefinition.Code && c.Year == setUp.Year).ToArray();

        //         return teams;
        //     });
        // }

        // public Team[] GetTeamByDefinition(string definitionCode)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //       //  ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();
        //         ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

        //         ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
        //         var setUp = setupRepository.Get().FirstOrDefault();

        //         Team[] teams = teamRepository.Get().Where(c => c.DefinitionCode == definitionCode && c.Year == setUp.Year).ToArray();

        //         return teams;
        //     });
        // }

        // public TeamData[] GetTeams()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

        //         ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
        //         var setUp = setupRepository.Get().FirstOrDefault();

        //         List<TeamData> team = new List<TeamData>();
        //         IEnumerable<TeamInfo> teamInfos = teamRepository.GetTeams().OrderByDescending(c => c.Team.DefinitionCode).Where(c => c.Team.Year == setUp.Year).ToArray();

        //         foreach (var teamInfo in teamInfos)
        //         {
        //             team.Add(
        //                 new TeamData
        //                 {
        //                     TeamId = teamInfo.Team.EntityId,
        //                     Code = teamInfo.Team.Code,
        //                     Name = teamInfo.Team.Name,
        //                     //ParentId = teamInfo.Team.ParentId,
        //                     ParentCode = teamInfo.Parent != null ? teamInfo.Parent.Code : string.Empty,
        //                     ParentName = teamInfo.Parent != null ? teamInfo.Parent.Name : "",
        //                     DefinitionCode = teamInfo.Team.DefinitionCode,
        //                     CanClassified = true,
        //                     CanUseStaffsId = true,
        //                     StaffsId = teamInfo.Team.StaffsId,
        //                     Position = 1
        //                 });
        //         }

        //         return team.ToArray();
        //     });
        // }

        // #endregion

        // #region TeamClassificationMap operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public TeamClassificationMap UpdateTeamClassificationMap(TeamClassificationMap teamClassificationMap)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationMapRepository teamClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationMapRepository>();

        //         TeamClassificationMap updatedEntity = null;

        //         if (teamClassificationMap.TeamClassificationMapId == 0)
        //         {
        //             teamClassificationMap.Year = GetSetup().Year;
        //             updatedEntity = teamClassificationMapRepository.Add(teamClassificationMap);
        //         }
        //         else
        //             updatedEntity = teamClassificationMapRepository.Update(teamClassificationMap);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteTeamClassificationMap(int teamClassificationMapId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationMapRepository teamClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationMapRepository>();

        //         teamClassificationMapRepository.Remove(teamClassificationMapId);
        //     });
        // }

        // public TeamClassificationMap GetTeamClassificationMap(int teamClassificationMapId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationMapRepository teamClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationMapRepository>();

        //         TeamClassificationMap teamClassificationMapEntity = teamClassificationMapRepository.Get(teamClassificationMapId);
        //         if (teamClassificationMapEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("TeamClassificationMap with ID of {0} is not in database", teamClassificationMapId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return teamClassificationMapEntity;
        //     });
        // }

        // public TeamClassificationMap[] GetAllTeamClassificationMaps(string misCode,string definitionCode)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationMapRepository teamClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationMapRepository>();

        //         var setup = GetSetup();

        //         IEnumerable<TeamClassificationMap> teamClassificationMap = teamClassificationMapRepository.Get().Where(c => c.Year == setup.Year && c.DefinitionCode == definitionCode && c.MisCode == misCode).ToArray();

        //         return teamClassificationMap.ToArray();
        //     });
        // }

        //#endregion

        // #region AccountOfficerDetail operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public AccountOfficerDetail UpdateAccountOfficerDetail(AccountOfficerDetail accountOfficerDetail)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountOfficerDetailRepository accountOfficerDetailRepository = _DataRepositoryFactory.GetDataRepository<IAccountOfficerDetailRepository>();

        //         AccountOfficerDetail updatedEntity = null;

        //         if (accountOfficerDetail.AccountOfficerDetailId == 0)
        //         {
        //             accountOfficerDetail.Year = GetSetup().Year;
        //             updatedEntity = accountOfficerDetailRepository.Add(accountOfficerDetail);
        //         }
        //         else
        //             updatedEntity = accountOfficerDetailRepository.Update(accountOfficerDetail);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteAccountOfficerDetail(int accountOfficerDetailId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountOfficerDetailRepository accountOfficerDetailRepository = _DataRepositoryFactory.GetDataRepository<IAccountOfficerDetailRepository>();

        //         accountOfficerDetailRepository.Remove(accountOfficerDetailId);
        //     });
        // }

        // public AccountOfficerDetail GetAccountOfficerDetail(int accountOfficerDetailId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountOfficerDetailRepository accountOfficerDetailRepository = _DataRepositoryFactory.GetDataRepository<IAccountOfficerDetailRepository>();

        //         AccountOfficerDetail accountOfficerDetailEntity = accountOfficerDetailRepository.Get(accountOfficerDetailId);
        //         if (accountOfficerDetailEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("AccountOfficerDetail with ID of {0} is not in database", accountOfficerDetailId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return accountOfficerDetailEntity;
        //     });
        // }

        // public AccountOfficerDetail[] GetAllAccountOfficerDetails()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountOfficerDetailRepository accountOfficerDetailRepository = _DataRepositoryFactory.GetDataRepository<IAccountOfficerDetailRepository>();

        //         var setup = GetSetup();

        //         IEnumerable<AccountOfficerDetail> accountOfficerDetail = accountOfficerDetailRepository.Get().Where(c => c.Year == setup.Year).ToArray();

        //         return accountOfficerDetail.ToArray();
        //     });
        // }

        // #endregion

        // #region BranchDefaultMIS operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public BranchDefaultMIS UpdateBranchDefaultMIS(BranchDefaultMIS branchDefaultMIS)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IBranchDefaultMISRepository branchDefaultMISRepository = _DataRepositoryFactory.GetDataRepository<IBranchDefaultMISRepository>();

        //         BranchDefaultMIS updatedEntity = null;

        //         if (branchDefaultMIS.BranchDefaultMISId == 0)
        //         {
        //             branchDefaultMIS.Year = GetSetup().Year;
        //             updatedEntity = branchDefaultMISRepository.Add(branchDefaultMIS);
        //         }
        //         else
        //             updatedEntity = branchDefaultMISRepository.Update(branchDefaultMIS);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteBranchDefaultMIS(int branchDefaultMISId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IBranchDefaultMISRepository branchDefaultMISRepository = _DataRepositoryFactory.GetDataRepository<IBranchDefaultMISRepository>();

        //         branchDefaultMISRepository.Remove(branchDefaultMISId);
        //     });
        // }

        // public BranchDefaultMIS GetBranchDefaultMIS(int branchDefaultMISId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IBranchDefaultMISRepository branchDefaultMISRepository = _DataRepositoryFactory.GetDataRepository<IBranchDefaultMISRepository>();

        //         BranchDefaultMIS branchDefaultMISEntity = branchDefaultMISRepository.Get(branchDefaultMISId);
        //         if (branchDefaultMISEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("BranchDefaultMIS with ID of {0} is not in database", branchDefaultMISId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return branchDefaultMISEntity;
        //     });
        // }

        // public BranchDefaultMIS[] GetAllBranchDefaultMISs()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IBranchDefaultMISRepository branchDefaultMISRepository = _DataRepositoryFactory.GetDataRepository<IBranchDefaultMISRepository>();

        //         var setup = GetSetup();

        //         IEnumerable<BranchDefaultMIS> branchDefaultMIS = branchDefaultMISRepository.Get().Where(c => c.Year == setup.Year).ToArray();

        //         return branchDefaultMIS.ToArray();
        //     });
        // }

        //   #endregion

        // #region ManagementTree operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public ManagementTree UpdateManagementTree(ManagementTree managementTree)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IManagementTreeRepository managementTreeRepository = _DataRepositoryFactory.GetDataRepository<IManagementTreeRepository>();

        //         ManagementTree updatedEntity = null;

        //         if (managementTree.ManagementTreeId == 0)
        //         {
        //             managementTree.Year = GetSetup().Year;
        //             updatedEntity = managementTreeRepository.Add(managementTree);
        //         }
        //         else
        //             updatedEntity = managementTreeRepository.Update(managementTree);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteManagementTree(int managementTreeId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IManagementTreeRepository managementTreeRepository = _DataRepositoryFactory.GetDataRepository<IManagementTreeRepository>();

        //         managementTreeRepository.Remove(managementTreeId);
        //     });
        // }

        // public ManagementTree GetManagementTree(int managementTreeId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IManagementTreeRepository managementTreeRepository = _DataRepositoryFactory.GetDataRepository<IManagementTreeRepository>();

        //         ManagementTree managementTreeEntity = managementTreeRepository.Get(managementTreeId);
        //         if (managementTreeEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("ManagementTree with ID of {0} is not in database", managementTreeId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return managementTreeEntity;
        //     });
        // }

        // public ManagementTreeData[] GetAllManagementTrees()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IManagementTreeRepository managementTreeRepository = _DataRepositoryFactory.GetDataRepository<IManagementTreeRepository>();

        //         //var setup = GetSetup();

        //         //IEnumerable<ManagementTreeData> managementTree = managementTreeRepository.Get().Where(c => c.Year == setup.Year).ToArray();

        //         List<ManagementTreeData> managementTree = new List<ManagementTreeData>();
        //         IEnumerable<ManagementTreeInfo> managementTreeInfos = managementTreeRepository.GetManagementTrees().ToArray();

        //         foreach (var managementTreeInfo in managementTreeInfos)
        //         {
        //             managementTree.Add(
        //                 new ManagementTreeData
        //                 {
        //                     ManagementTreeId = managementTreeInfo.ManagementTree.EntityId,
        //                     AccountNo=managementTreeInfo.ManagementTree.AccountNo,
        //                     TeamDefinitionCode = managementTreeInfo.ManagementTree.TeamDefinitionCode,
        //                     TeamDefinitionName = managementTreeInfo.TeamDefinition.Name,
        //                     TeamCode = managementTreeInfo.ManagementTree.TeamCode,
        //                     TeamName = managementTreeInfo.Team.Name,
        //                     AccountOfficerDefinitionCode = managementTreeInfo.ManagementTree != null ?  managementTreeInfo.ManagementTree.AccountOfficerDefinitionCode: string.Empty,
        //                     AccountOfficerDefinitionName = managementTreeInfo.AccountOfficerDefinition != null ? managementTreeInfo.AccountOfficerDefinition.Name : string.Empty,
        //                     AccountOfficerCode = managementTreeInfo.ManagementTree != null ? managementTreeInfo.ManagementTree.AccountOfficerCode : string.Empty,
        //                     AccountOfficerName = managementTreeInfo.AccountOfficer != null ? managementTreeInfo.AccountOfficer.Name : string.Empty,
        //                     Rate= managementTreeInfo.ManagementTree.Rate,
        //                     Year = managementTreeInfo.ManagementTree.Year,
        //                     Active = managementTreeInfo.ManagementTree.Active
        //                 });
        //         }


        //         return managementTree.ToArray();
        //     });
        // }

        // #endregion

        // #region AccountMIS operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public AccountMIS UpdateAccountMIS(AccountMIS accountMIS)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountMISRepository accountMISRepository = _DataRepositoryFactory.GetDataRepository<IAccountMISRepository>();

        //         AccountMIS updatedEntity = null;

        //         if (accountMIS.AccountMISId == 0)
        //         {
        //             accountMIS.Year = GetSetup().Year;
        //             updatedEntity = accountMISRepository.Add(accountMIS);
        //         }
        //         else
        //             updatedEntity = accountMISRepository.Update(accountMIS);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteAccountMIS(int accountMISId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountMISRepository accountMISRepository = _DataRepositoryFactory.GetDataRepository<IAccountMISRepository>();

        //         accountMISRepository.Remove(accountMISId);
        //     });
        // }

        // public AccountMIS GetAccountMIS(int accountMISId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountMISRepository accountMISRepository = _DataRepositoryFactory.GetDataRepository<IAccountMISRepository>();

        //         AccountMIS accountMISEntity = accountMISRepository.Get(accountMISId);
        //         if (accountMISEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("AccountMIS with ID of {0} is not in database", accountMISId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return accountMISEntity;
        //     });
        // }

        // public AccountMISData[] GetAllAccountMISs()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountMISRepository accountMISRepository = _DataRepositoryFactory.GetDataRepository<IAccountMISRepository>();

        //         //var setup = GetSetup();

        //         //IEnumerable<AccountMIS> accountMIS = accountMISRepository.Get().Where(c => c.Year == setup.Year).ToArray();

        //         //return accountMIS.ToArray();

        //         ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
        //         var setUp = setupRepository.Get().FirstOrDefault();

        //         List<AccountMISData> accountMIS = new List<AccountMISData>();
        //         IEnumerable<AccountMISInfo> accountMISInfos = accountMISRepository.GetAccountMISs().Where(c => c.AccountMIS.Year == setUp.Year).ToArray();

        //         foreach (var accountMISInfo in accountMISInfos)
        //         {
        //             accountMIS.Add(
        //                 new AccountMISData
        //                 {
        //                     AccountMISId = accountMISInfo.AccountMIS.EntityId,
        //                     AccountNo = accountMISInfo.AccountMIS.AccountNo,
        //                     TeamDefinitionCode = accountMISInfo.AccountMIS.TeamDefinitionCode,
        //                     TeamDefinitionName = accountMISInfo.TeamDefinition.Name,
        //                     TeamCode = accountMISInfo.AccountMIS.TeamCode,
        //                     TeamName = accountMISInfo.Team.Name,
        //                     AccountOfficerDefinitionCode = accountMISInfo.AccountMIS != null ? accountMISInfo.AccountMIS.AccountOfficerDefinitionCode : string.Empty,
        //                     AccountOfficerDefinitionName = accountMISInfo.AccountOfficerDefinition != null ? accountMISInfo.AccountOfficerDefinition.Name : string.Empty,
        //                     AccountOfficerCode = accountMISInfo.AccountMIS != null ? accountMISInfo.AccountMIS.AccountOfficerCode : string.Empty,
        //                     AccountOfficerName = accountMISInfo.AccountOfficer != null ? accountMISInfo.AccountOfficer.Name : string.Empty,
        //                     AccountName = accountMISInfo.CustAccount != null ? accountMISInfo.CustAccount.AccountName : string.Empty, 
        //                     //Rate = accountMISInfo.AccountMIS.Rate,
        //                     Year = accountMISInfo.AccountMIS.Year,
        //                     Active = accountMISInfo.AccountMIS.Active
        //                 });
        //         }


        //         return accountMIS.ToArray();
        //     });
        // }

        // public void DeleteSelectedIds(string selectedIds)
        // {
        //     var connectionString = GetDataConnection();

        //     using (var con = new SqlConnection(connectionString))
        //     {
        //         var cmd = new SqlCommand("MultipleDeletion", con);
        //         cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //         cmd.Parameters.Add(new SqlParameter
        //         {
        //             ParameterName = "@IdLists",
        //             Value = selectedIds,
        //         });
        //         cmd.Parameters.Add(new SqlParameter
        //         {
        //             ParameterName = "@pageOwner",
        //             Value = "AcctMIS"
        //         });
        //         cmd.CommandTimeout = 0;

        //         con.Open();

        //         cmd.ExecuteNonQuery();

        //         con.Close();
        //     }

        // }

        // #endregion

        // #region MISReplacement operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public MISReplacement UpdateMISReplacement(MISReplacement misReplacement)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMISReplacementRepository misReplacementRepository = _DataRepositoryFactory.GetDataRepository<IMISReplacementRepository>();

        //         MISReplacement updatedEntity = null;

        //         if (misReplacement.MISReplacementId == 0)
        //         {
        //             misReplacement.Year = GetSetup().Year;
        //             updatedEntity = misReplacementRepository.Add(misReplacement);
        //         }     
        //         else
        //             updatedEntity = misReplacementRepository.Update(misReplacement);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteMISReplacement(int misReplacementId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMISReplacementRepository misReplacementRepository = _DataRepositoryFactory.GetDataRepository<IMISReplacementRepository>();

        //         misReplacementRepository.Remove(misReplacementId);
        //     });
        // }

        // public MISReplacement GetMISReplacement(int misReplacementId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMISReplacementRepository misReplacementRepository = _DataRepositoryFactory.GetDataRepository<IMISReplacementRepository>();

        //         MISReplacement misReplacementEntity = misReplacementRepository.Get(misReplacementId);
        //         if (misReplacementEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("MISReplacement with ID of {0} is not in database", misReplacementId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return misReplacementEntity;
        //     });
        // }

        // public MISReplacement[] GetAllMISReplacements()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMISReplacementRepository misReplacementRepository = _DataRepositoryFactory.GetDataRepository<IMISReplacementRepository>();

        //         var setup = GetSetup();

        //         IEnumerable<MISReplacement> misReplacement = misReplacementRepository.Get().Where(c => c.Year == setup.Year).ToArray();

        //         return misReplacement.ToArray();
        //     });
        // }

        //  #endregion

        // #region SetUp operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public SetUp UpdateMPRSetup(SetUp setUp)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ISetUpRepository setUpRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();

        //         SetUp updatedEntity = null;

        //         if (setUp.SetupId == 0)
        //             updatedEntity = setUpRepository.Add(setUp);
        //         else
        //             updatedEntity = setUpRepository.Update(setUp);

        //         return updatedEntity;
        //     });
        // }

        // public SetUp GetFirstMPRSetup()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ISetUpRepository setUpRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();

        //         SetUp setUpEntity = setUpRepository.Get().FirstOrDefault();
        //         //if (setUpEntity == null)
        //         //{
        //         //    NotFoundException ex = new NotFoundException(string.Format("SetUp with ID of {0} is not in database", setUpId));
        //         //    throw new FaultException<NotFoundException>(ex, ex.Message);
        //         //}

        //         return setUpEntity;
        //     });
        // }

        // #endregion

        // #region TransferPrice operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public TransferPrice UpdateTransferPrice(TransferPrice transferPrice)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITransferPriceRepository transferPriceRepository = _DataRepositoryFactory.GetDataRepository<ITransferPriceRepository>();

        //         TransferPrice updatedEntity = null;

        //         if (transferPrice.TransferPriceId == 0)
        //         {
        //             transferPrice.Year = GetSetup().Year;
        //             updatedEntity = transferPriceRepository.Add(transferPrice);
        //         }
        //         else
        //             updatedEntity = transferPriceRepository.Update(transferPrice);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteTransferPrice(int transferPriceId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITransferPriceRepository transferPriceRepository = _DataRepositoryFactory.GetDataRepository<ITransferPriceRepository>();

        //         transferPriceRepository.Remove(transferPriceId);
        //     });
        // }

        // public TransferPrice GetTransferPrice(int transferPriceId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITransferPriceRepository transferPriceRepository = _DataRepositoryFactory.GetDataRepository<ITransferPriceRepository>();

        //         TransferPrice transferPriceEntity = transferPriceRepository.Get(transferPriceId);
        //         if (transferPriceEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("TransferPrice with ID of {0} is not in database", transferPriceId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return transferPriceEntity;
        //     });
        // }

        // public TransferPriceData[] GetAllTransferPrices()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITransferPriceRepository transferPriceRepository = _DataRepositoryFactory.GetDataRepository<ITransferPriceRepository>();

        //         //var setUp = GetSetup();

        //         //IEnumerable<TransferPrice> transferPrice = transferPriceRepository.Get().Where (c=>c.Year == setUp.Year).ToArray();

        //         List<TransferPriceData> transferPrice = new List<TransferPriceData>();
        //         IEnumerable<TransferPriceInfo> transferPriceInfos = transferPriceRepository.GetTransferPrices().ToArray();

        //         foreach (var transferPriceInfo in transferPriceInfos)
        //         {
        //             transferPrice.Add(
        //                 new TransferPriceData
        //                 {
        //                     TransferPriceId = transferPriceInfo.TransferPrice.EntityId,
        //                     ProductCode = transferPriceInfo.TransferPrice.ProductCode,
        //                     ProductName = transferPriceInfo.TransferPrice != null ? transferPriceInfo.Product.Name : string.Empty,
        //                     CaptionCode = transferPriceInfo.TransferPrice.CaptionCode,
        //                     CaptionName = transferPriceInfo.TransferPrice != null ? transferPriceInfo.BSCaption.CaptionName: string.Empty,
        //                     Rate = transferPriceInfo.TransferPrice.Rate,
        //                     DefinitionCode = transferPriceInfo.TransferPrice.DefinitionCode,
        //                     DefinitionName = transferPriceInfo.TransferPrice != null ? transferPriceInfo.TeamDefinition.Name : string.Empty,
        //                     MisCode = transferPriceInfo.TransferPrice.MisCode,
        //                     MisName = transferPriceInfo.TransferPrice != null ? transferPriceInfo.Team.Name : string.Empty,
        //                     Year = transferPriceInfo.TransferPrice.Year,
        //                     Period = transferPriceInfo.TransferPrice.Period,
        //                     SolutionId = transferPriceInfo.TransferPrice.SolutionId,
        //                     SolutionName = string.Empty,
        //                     CompanyCode = transferPriceInfo.TransferPrice.CompanyCode,
        //                     Active = transferPriceInfo.TransferPrice.Active
        //                 });
        //         }


        //         return transferPrice.ToArray();
        //     });
        // }
        // #endregion

        // #region AccountTransferPrice operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public AccountTransferPrice UpdateAccountTransferPrice(AccountTransferPrice accountTransferPrice)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountTransferPriceRepository accountTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IAccountTransferPriceRepository>();

        //         AccountTransferPrice updatedEntity = null;

        //         if (accountTransferPrice.AccountTransferPriceId == 0)
        //         {
        //             accountTransferPrice.Year = GetSetup().Year;
        //             updatedEntity = accountTransferPriceRepository.Add(accountTransferPrice);
        //         }
        //         else
        //             updatedEntity = accountTransferPriceRepository.Update(accountTransferPrice);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteAccountTransferPrice(int accountTransferPriceId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountTransferPriceRepository accountTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IAccountTransferPriceRepository>();

        //         accountTransferPriceRepository.Remove(accountTransferPriceId);
        //     });
        // }

        // public AccountTransferPrice GetAccountTransferPrice(int accountTransferPriceId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountTransferPriceRepository accountTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IAccountTransferPriceRepository>();

        //         AccountTransferPrice accountTransferPriceEntity = accountTransferPriceRepository.Get(accountTransferPriceId);
        //         if (accountTransferPriceEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("AccountTransferPrice with ID of {0} is not in database", accountTransferPriceId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return accountTransferPriceEntity;
        //     });
        // }

        // public AccountTransferPriceData[] GetAllAccountTransferPrices()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountTransferPriceRepository accountTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IAccountTransferPriceRepository>();

        //        // ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
        //         //var setUp = setupRepository.Get().FirstOrDefault();

        //         List<AccountTransferPriceData> AccountTransferPrice = new List<AccountTransferPriceData>();
        //         IEnumerable<AccountTransferPriceInfo> accountTransferPriceInfos = accountTransferPriceRepository.GetAccountTransferPrices().ToArray();

        //         foreach (var accountTransferPriceInfo in accountTransferPriceInfos)
        //         {
        //             AccountTransferPrice.Add(
        //                 new AccountTransferPriceData
        //                 {
        //                     AccountTransferPriceId = accountTransferPriceInfo.AccountTransferPrice.EntityId,
        //                     AccountNo = accountTransferPriceInfo.AccountTransferPrice.AccountNo,
        //                     Category = accountTransferPriceInfo.AccountTransferPrice.Category,
        //                     CategoryName = accountTransferPriceInfo.AccountTransferPrice.Category.ToString(),
        //                     Rate = accountTransferPriceInfo.AccountTransferPrice.Rate,
        //                     Year = accountTransferPriceInfo.AccountTransferPrice.Year,
        //                     Period = accountTransferPriceInfo.AccountTransferPrice.Period,
        //                     SolutionId = accountTransferPriceInfo.AccountTransferPrice.SolutionId,
        //                     SolutionName= "",
        //                     AccountName = accountTransferPriceInfo.CustAccount !=null ?accountTransferPriceInfo.CustAccount.AccountName : string.Empty ,
        //                     Active = true,

        //                 });
        //         }

        //         return AccountTransferPrice.ToArray();
        //     });
        // }

        // #endregion

        // #region GeneralTransferPrice operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public GeneralTransferPrice UpdateGeneralTransferPrice(GeneralTransferPrice generalTransferPrice)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IGeneralTransferPriceRepository generalTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IGeneralTransferPriceRepository>();

        //         GeneralTransferPrice updatedEntity = null;

        //         if (generalTransferPrice.GeneralTransferPriceId == 0)
        //         {
        //             generalTransferPrice.Year = GetSetup().Year;
        //             updatedEntity = generalTransferPriceRepository.Add(generalTransferPrice);
        //         }
        //         else
        //             updatedEntity = generalTransferPriceRepository.Update(generalTransferPrice);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteGeneralTransferPrice(int generalTransferPriceId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IGeneralTransferPriceRepository generalTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IGeneralTransferPriceRepository>();

        //         generalTransferPriceRepository.Remove(generalTransferPriceId);
        //     });
        // }

        // public GeneralTransferPrice GetGeneralTransferPrice(int generalTransferPriceId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IGeneralTransferPriceRepository generalTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IGeneralTransferPriceRepository>();

        //         GeneralTransferPrice generalTransferPriceEntity = generalTransferPriceRepository.Get(generalTransferPriceId);
        //         if (generalTransferPriceEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("General Transfer Price with ID of {0} is not in database", generalTransferPriceId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return generalTransferPriceEntity;
        //     });
        // }

        // public GeneralTransferPriceData[] GetAllGeneralTransferPrices()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IGeneralTransferPriceRepository generalTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IGeneralTransferPriceRepository>();

        //         // ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
        //         //var setUp = setupRepository.Get().FirstOrDefault();

        //         List<GeneralTransferPriceData> GeneralTransferPrice = new List<GeneralTransferPriceData>();
        //         IEnumerable<GeneralTransferPrice> generalTransferPriceInfos = generalTransferPriceRepository.Get().ToArray();

        //         foreach (var generalTransferPriceInfo in generalTransferPriceInfos)
        //         {
        //             GeneralTransferPrice.Add(
        //                 new GeneralTransferPriceData
        //                 {
        //                     GeneralTransferPriceId = generalTransferPriceInfo.EntityId,
        //                     Category = generalTransferPriceInfo.Category,
        //                     CategoryName = generalTransferPriceInfo.Category.ToString(),
        //                     CurrencyType = generalTransferPriceInfo.CurrencyType,
        //                     CurrencyTypeName = generalTransferPriceInfo.CurrencyType.ToString(),
        //                     Rate = generalTransferPriceInfo.Rate,
        //                     Year = generalTransferPriceInfo.Year,
        //                     Period = generalTransferPriceInfo.Period,
        //                     DefinitionCode = generalTransferPriceInfo.DefinitionCode,
        //                     MISCode = generalTransferPriceInfo.MISCode,
        //                     CompanyCode = generalTransferPriceInfo.CompanyCode,
        //                     Active = true,

        //                 });
        //         }

        //         return GeneralTransferPrice.ToArray();
        //     });
        // }

        // public void DeleteGTPSelectedIds(string selectedIds)
        // {
        //     var connectionString = GetDataConnection();

        //     using (var con = new SqlConnection(connectionString))
        //     {
        //         var cmd = new SqlCommand("MultipleDeletion", con);
        //         cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //         cmd.Parameters.Add(new SqlParameter
        //         {
        //             ParameterName = "@IdLists",
        //             Value = selectedIds,
        //         });
        //         cmd.Parameters.Add(new SqlParameter
        //         {
        //             ParameterName = "@pageOwner",
        //             Value = "GTP"
        //         });
        //         cmd.CommandTimeout = 0;

        //         con.Open();

        //         cmd.ExecuteNonQuery();

        //         con.Close();
        //     }

        // }

        // #endregion

        // #region CustAccount operations

        // [OperationBehavior(TransactionScopeRequired = true)]


        // public CustAccount[] GetAllCustAccounts()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ICustAccountRepository CustAccountRepository = _DataRepositoryFactory.GetDataRepository<ICustAccountRepository>();

        //         IEnumerable<CustAccount> CustAccounts = CustAccountRepository.Get().ToArray();

        //         return CustAccounts.ToArray();
        //     });
        // }

        // public CustAccount[] GetCustAccounts(string searchType, string searchValue, int number)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR,  GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ICustAccountRepository CustAccountRepository = _DataRepositoryFactory.GetDataRepository<ICustAccountRepository>();
        //         List<CustAccount> CustAccounts = CustAccountRepository.GetCustomerAccountBySearch(searchType, searchValue, number);


        //         return CustAccounts.ToArray();
        //     });
        // }


        // #endregion

        // #region BSExemption operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public BSExemption UpdateBSExemption(BSExemption bsExemption)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IBSExemptionRepository bsExemptionRepository = _DataRepositoryFactory.GetDataRepository<IBSExemptionRepository>();

        //         BSExemption updatedEntity = null;

        //         if (bsExemption.BSExemptionId == 0)
        //         {

        //             updatedEntity = bsExemptionRepository.Add(bsExemption);
        //         }
        //         else
        //             updatedEntity = bsExemptionRepository.Update(bsExemption);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteBSExemption(int bsExemptionId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IBSExemptionRepository bsExemptionRepository = _DataRepositoryFactory.GetDataRepository<IBSExemptionRepository>();

        //         bsExemptionRepository.Remove(bsExemptionId);
        //     });
        // }

        // public BSExemption GetBSExemption(int bsExemptionId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IBSExemptionRepository bsExemptionRepository = _DataRepositoryFactory.GetDataRepository<IBSExemptionRepository>();

        //         BSExemption bsExemptionEntity = bsExemptionRepository.Get(bsExemptionId);
        //         if (bsExemptionEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("BSExemption with ID of {0} is not in database", bsExemptionId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return bsExemptionEntity;
        //     });
        // }

        // public BSExemption[] GetAllBSExemptions()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IBSExemptionRepository bsExemptionRepository = _DataRepositoryFactory.GetDataRepository<IBSExemptionRepository>();


        //         IEnumerable<BSExemption> bsExemption = bsExemptionRepository.Get().ToArray();

        //         return bsExemption.ToArray();
        //     });
        // }

        // #endregion

        // #region MemoAccountMap operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public MemoAccountMap UpdateMemoAccountMap(MemoAccountMap memoAccountMap)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoAccountMapRepository memoAccountMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoAccountMapRepository>();

        //         MemoAccountMap updatedEntity = null;

        //         if (memoAccountMap.MemoAccountMapId == 0)
        //         {
        //            updatedEntity = memoAccountMapRepository.Add(memoAccountMap);
        //         }
        //         else
        //             updatedEntity = memoAccountMapRepository.Update(memoAccountMap);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteMemoAccountMap(int memoAccountMapId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoAccountMapRepository memoAccountMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoAccountMapRepository>();

        //         memoAccountMapRepository.Remove(memoAccountMapId);
        //     });
        // }

        // public MemoAccountMap GetMemoAccountMap(int memoAccountMapId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoAccountMapRepository memoAccountMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoAccountMapRepository>();

        //         MemoAccountMap memoAccountMapEntity = memoAccountMapRepository.Get(memoAccountMapId);
        //         if (memoAccountMapEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("MemoAccountMap with ID of {0} is not in database", memoAccountMapId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return memoAccountMapEntity;
        //     });
        // }

        // public MemoAccountMapData[] GetAllMemoAccountMaps()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoAccountMapRepository memoAccountMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoAccountMapRepository>();

        //         //var setUp = GetSetup();

        //         //IEnumerable<MemoAccountMap> memoAccountMap = memoAccountMapRepository.Get().Where (c=>c.Year == setUp.Year).ToArray();

        //         List<MemoAccountMapData> memoAccountMap = new List<MemoAccountMapData>();
        //         IEnumerable<MemoAccountMapInfo> memoAccountMapInfos = memoAccountMapRepository.GetMemoAccountMaps().ToArray();

        //         foreach (var memoAccountMapInfo in memoAccountMapInfos)
        //         {
        //             memoAccountMap.Add(
        //                 new MemoAccountMapData
        //                 {
        //                      MemoAccountMapId = memoAccountMapInfo.MemoAccountMap.EntityId,
        //                      AccountNo = memoAccountMapInfo.MemoAccountMap.AccountNo,
        //                      Code = memoAccountMapInfo.MemoAccountMap.Code,
        //                      Name = memoAccountMapInfo.MemoUnits != null ? memoAccountMapInfo.MemoUnits.Name : string.Empty,
        //                      AccountName = memoAccountMapInfo.CustAccount !=null? memoAccountMapInfo.CustAccount.AccountName: string.Empty,
        //                    //  bsCaptionInfo.Parent != null ? bsCaptionInfo.Parent.EntityId : 0,
        //                      Active = memoAccountMapInfo.MemoAccountMap.Active
        //                 });
        //         }



        //         return memoAccountMap.ToArray();
        //     });
        // }
        // #endregion

        // #region MemoGLMap operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public MemoGLMap UpdateMemoGLMap(MemoGLMap memoGLMap)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoGLMapRepository memoGLMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoGLMapRepository>();

        //         MemoGLMap updatedEntity = null;

        //         if (memoGLMap.MemoGLMapId == 0)
        //         {

        //             updatedEntity = memoGLMapRepository.Add(memoGLMap);
        //         }
        //         else
        //             updatedEntity = memoGLMapRepository.Update(memoGLMap);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteMemoGLMap(int memoGLMapId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoGLMapRepository memoGLMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoGLMapRepository>();

        //         memoGLMapRepository.Remove(memoGLMapId);
        //     });
        // }

        // public MemoGLMap GetMemoGLMap(int memoGLMapId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoGLMapRepository memoGLMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoGLMapRepository>();

        //         MemoGLMap memoGLMapEntity = memoGLMapRepository.Get(memoGLMapId);
        //         if (memoGLMapEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("MemoGLMap with ID of {0} is not in database", memoGLMapId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return memoGLMapEntity;
        //     });
        // }


        // public MemoGLMapData[] GetAllMemoGLMaps()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoGLMapRepository memoGLMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoGLMapRepository>();

        //         List<MemoGLMapData> memoGLMap = new List<MemoGLMapData>();
        //         IEnumerable<MemoGLMapInfo> memoGLMapInfos = memoGLMapRepository.GetMemoGLMaps().ToArray();

        //         foreach (var memoGLMapInfo in memoGLMapInfos)
        //         {
        //             memoGLMap.Add(
        //                 new MemoGLMapData
        //                 {
        //                     MemoGLMapId = memoGLMapInfo.MemoGLMap.EntityId,
        //                     Code = memoGLMapInfo.MemoGLMap.Code,
        //                     Name = memoGLMapInfo.MemoUnits.Name,
        //                     GLCode= memoGLMapInfo.GLDefinition != null ? memoGLMapInfo.GLDefinition.GL_Code : string.Empty,
        //                     GLDescription = memoGLMapInfo.GLDefinition != null ? memoGLMapInfo.GLDefinition.Description : string.Empty,
        //                     Active = memoGLMapInfo.MemoGLMap.Active
        //                 });
        //         }

        //         //memoGLMapInfo.GLDefinition != null ? memoGLMapInfo.GLDefinition.GL_Code : string.Empty,
        //         //memoGLMapInfo.GLDefinition != null ? memoGLMapInfo.GLDefinition.Description : string.Empty,

        //         return memoGLMap.ToArray();
        //     });
        // }

        // #endregion

        // #region MemoProductMap operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public MemoProductMap UpdateMemoProductMap(MemoProductMap memoProductMap)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoProductMapRepository memoProductMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoProductMapRepository>();

        //         MemoProductMap updatedEntity = null;

        //         if (memoProductMap.MemoProductMapId == 0)
        //         {
        //             updatedEntity = memoProductMapRepository.Add(memoProductMap);
        //         }
        //         else
        //             updatedEntity = memoProductMapRepository.Update(memoProductMap);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteMemoProductMap(int memoProductMapId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoProductMapRepository memoProductMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoProductMapRepository>();

        //         memoProductMapRepository.Remove(memoProductMapId);
        //     });
        // }

        // public MemoProductMap GetMemoProductMap(int memoProductMapId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoProductMapRepository memoProductMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoProductMapRepository>();

        //         MemoProductMap memoProductMapEntity = memoProductMapRepository.Get(memoProductMapId);
        //         if (memoProductMapEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("MemoProductMap with ID of {0} is not in database", memoProductMapId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return memoProductMapEntity;
        //     });
        // }

        // public MemoProductMapData[] GetAllMemoProductMaps()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoProductMapRepository memoProductMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoProductMapRepository>();

        //         List<MemoProductMapData> memoProductMap = new List<MemoProductMapData>();
        //         IEnumerable<MemoProductMapInfo> memoProductMapInfos = memoProductMapRepository.GetMemoProductMaps().ToArray();

        //         foreach (var memoProductMapInfo in memoProductMapInfos)
        //         {
        //             memoProductMap.Add(
        //                 new MemoProductMapData
        //                 {
        //                     MemoProductMapId = memoProductMapInfo.MemoProductMap.EntityId,
        //                     ProductCode = memoProductMapInfo.MemoProductMap.ProductCode,
        //                     ProductName = memoProductMapInfo.Product!=null ? memoProductMapInfo.Product.Name:string.Empty,
        //                      Code = memoProductMapInfo.MemoProductMap.Code,
        //                      UnitName = memoProductMapInfo.MemoUnits.Name,
        //                     Active = memoProductMapInfo.MemoProductMap.Active
        //                 });
        //         }


        //         return memoProductMap.ToArray();
        //     });
        // }
        // #endregion

        // #region MemoUnits operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public MemoUnits UpdateMemoUnits(MemoUnits memoUnit)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoUnitsRepository memoUnitRepository = _DataRepositoryFactory.GetDataRepository<IMemoUnitsRepository>();

        //         MemoUnits updatedEntity = null;

        //         if (memoUnit.MemoUnitsId == 0)
        //         {

        //             updatedEntity = memoUnitRepository.Add(memoUnit);
        //         }
        //         else
        //             updatedEntity = memoUnitRepository.Update(memoUnit);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteMemoUnits(int memoUnitId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoUnitsRepository memoUnitRepository = _DataRepositoryFactory.GetDataRepository<IMemoUnitsRepository>();

        //         memoUnitRepository.Remove(memoUnitId);
        //     });
        // }

        // public MemoUnits GetMemoUnits(int memoUnitId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoUnitsRepository memoUnitRepository = _DataRepositoryFactory.GetDataRepository<IMemoUnitsRepository>();

        //         MemoUnits memoUnitEntity = memoUnitRepository.Get(memoUnitId);
        //         if (memoUnitEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("MemoUnits with ID of {0} is not in database", memoUnitId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return memoUnitEntity;
        //     });
        // }

        // public MemoUnits[] GetAllMemoUnits()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoUnitsRepository memoUnitRepository = _DataRepositoryFactory.GetDataRepository<IMemoUnitsRepository>();


        //         IEnumerable<MemoUnits> memoUnit = memoUnitRepository.Get().ToArray();

        //         return memoUnit.ToArray();
        //     });
        // }

        // #endregion

        #region UserMIS operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public UserMIS UpdateUserMIS(UserMIS userMIS)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUserMISRepository userMISRepository = _DataRepositoryFactory.GetDataRepository<IUserMISRepository>();

                UserMIS updatedEntity = null;

                if (userMIS.UserMISId == 0)
                {
                    updatedEntity = userMISRepository.Add(userMIS);
                }
                else
                    updatedEntity = userMISRepository.Update(userMIS);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteUserMIS(int userMISId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUserMISRepository userMISRepository = _DataRepositoryFactory.GetDataRepository<IUserMISRepository>();

                userMISRepository.Remove(userMISId);
            });
        }

        public UserMIS GetUserMIS(int userMISId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUserMISRepository userMISRepository = _DataRepositoryFactory.GetDataRepository<IUserMISRepository>();

                UserMIS userMISEntity = userMISRepository.Get(userMISId);
                if (userMISEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("UserMIS with ID of {0} is not in database", userMISId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return userMISEntity;
            });
        }

        public UserMIS GetUserMISByLoginID(string loginID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUserMISRepository userMISRepository = _DataRepositoryFactory.GetDataRepository<IUserMISRepository>();

                var setUp = GetSetup();

                UserMIS userMISEntity = userMISRepository.Get().Where(c => c.LoginID == loginID).FirstOrDefault();

                return userMISEntity;
            });
        }

        public UserMIS[] GetAllUserMISs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUserMISRepository userMISRepository = _DataRepositoryFactory.GetDataRepository<IUserMISRepository>();

                var setup = GetSetup();

                IEnumerable<UserMIS> userMIS = userMISRepository.Get().ToArray();

                return userMIS.ToArray();
            });
        }


        #endregion

        #region UserClassificationMap operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public UserClassificationMap UpdateUserClassificationMap(UserClassificationMap userClassificationMap)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUserClassificationMapRepository userClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<IUserClassificationMapRepository>();

                UserClassificationMap updatedEntity = null;

                if (userClassificationMap.UserClassificationMapId == 0)
                {
                    updatedEntity = userClassificationMapRepository.Add(userClassificationMap);
                }
                else
                    updatedEntity = userClassificationMapRepository.Update(userClassificationMap);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteUserClassificationMap(int userClassificationMapId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUserClassificationMapRepository userClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<IUserClassificationMapRepository>();

                userClassificationMapRepository.Remove(userClassificationMapId);
            });
        }

        public UserClassificationMap GetUserClassificationMap(int userClassificationMapId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUserClassificationMapRepository userClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<IUserClassificationMapRepository>();

                UserClassificationMap userClassificationMapEntity = userClassificationMapRepository.Get(userClassificationMapId);
                if (userClassificationMapEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("UserClassificationMap with ID of {0} is not in database", userClassificationMapId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return userClassificationMapEntity;
            });
        }

        public UserClassificationMap[] GetAllUserClassificationMaps(string loginID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUserClassificationMapRepository userClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<IUserClassificationMapRepository>();

                var setup = GetSetup();

                IEnumerable<UserClassificationMap> userClassificationMap = userClassificationMapRepository.GetUserClassificationMaps(loginID).ToArray();

                return userClassificationMap.ToArray();
            });
        }


        #endregion

        #region TeamDefinition operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public TeamDefinition UpdateTeamDefinition(TeamDefinition teamDefinition)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

                TeamDefinition updatedEntity = null;

                if (teamDefinition.TeamDefinitionId == 0)
                {
                    if (GetSetup().Period == null)
                    {
                        teamDefinition.Year = GetSetup().Year;
                        updatedEntity = teamDefinitionRepository.Add(teamDefinition);
                    }
                    else
                    {
                        teamDefinition.Year = GetSetup().Year;
                        teamDefinition.Period = GetSetup().Period;
                        updatedEntity = teamDefinitionRepository.Add(teamDefinition);
                    }

                }
                else
                    updatedEntity = teamDefinitionRepository.Update(teamDefinition);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTeamDefinition(int teamDefinitionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

                teamDefinitionRepository.Remove(teamDefinitionId);
            });
        }

        public TeamDefinition GetTeamDefinition(int teamDefinitionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();
                ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

                var setup = GetSetup();

                if (setup.Period == null)
                {
                    if (GetSetup().SwithMode == "Team Classification")
                    {

                        var query = (from b in teamClassificationRepository.Get().Take(1) select new { TeamDefinitionId = setup.LevelId.Value, Code = setup.LevelId.ToString(), Name = "Team", Position = 2, Year = setup.Year, CanClassified = false, CanUseStaffId = false, Period = b.Period, CompanyCode = b.CompanyCode, Active = b.Active, Deleted = b.Deleted, CreatedBy = b.CreatedBy, CreatedOn = b.CreatedOn, UpdatedBy = b.UpdatedBy, UpdatedOn = b.UpdatedOn, RowVersion = b.RowVersion })
                             .Concat(from a in teamDefinitionRepository.Get() select new { TeamDefinitionId = a.TeamDefinitionId, Code = a.Code, Name = a.Name, Position = a.Position, Year = a.Year, CanClassified = a.CanClassified, CanUseStaffId = a.CanUseStaffId, Period = a.Period, CompanyCode = a.CompanyCode, Active = a.Active, Deleted = a.Deleted, CreatedBy = a.CreatedBy, CreatedOn = a.CreatedOn, UpdatedBy = a.UpdatedBy, UpdatedOn = a.UpdatedOn, RowVersion = a.RowVersion }).Where(u => u.TeamDefinitionId == teamDefinitionId && u.Year == setup.Year).FirstOrDefault();

                        TeamDefinition teamDefinitionEntity = new TeamDefinition();

                        teamDefinitionEntity.TeamDefinitionId = query.TeamDefinitionId;
                        teamDefinitionEntity.Code = query.Code;
                        teamDefinitionEntity.Name = query.Name;
                        teamDefinitionEntity.Position = query.Position;
                        teamDefinitionEntity.Year = query.Year;
                        teamDefinitionEntity.CanClassified = query.CanClassified;
                        teamDefinitionEntity.CanUseStaffId = query.CanUseStaffId;
                        teamDefinitionEntity.Period = query.Period;
                        teamDefinitionEntity.CompanyCode = query.CompanyCode;
                        teamDefinitionEntity.Active = query.Active;
                        teamDefinitionEntity.Deleted = query.Deleted;
                        teamDefinitionEntity.CreatedBy = query.CreatedBy;
                        teamDefinitionEntity.CreatedOn = query.CreatedOn;
                        teamDefinitionEntity.UpdatedBy = query.UpdatedBy;
                        teamDefinitionEntity.UpdatedOn = query.UpdatedOn;
                        teamDefinitionEntity.RowVersion = query.RowVersion;

                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("TeamDefinition with ID of {0} is not in database", teamDefinitionId));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;

                    }
                    else
                    {
                        TeamDefinition teamDefinitionEntity = teamDefinitionRepository.Get().Where(c => c.TeamDefinitionId == teamDefinitionId && c.Year == setup.Year).FirstOrDefault();
                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("TeamDefinition with ID of {0} is not in database", teamDefinitionId));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;
                    }

                }
                else
                {
                    if (GetSetup().SwithMode == "Team Classification")
                    {

                        var query = (from b in teamClassificationRepository.Get().Take(1) select new { TeamDefinitionId = setup.LevelId.Value, Code = setup.LevelId.ToString(), Name = "Team", Position = 5, Year = setup.Year, CanClassified = false, CanUseStaffId = false, Period = setup.Period, CompanyCode = b.CompanyCode, Active = b.Active, Deleted = b.Deleted, CreatedBy = b.CreatedBy, CreatedOn = b.CreatedOn, UpdatedBy = b.UpdatedBy, UpdatedOn = b.UpdatedOn, RowVersion = b.RowVersion })
                             .Concat(from a in teamDefinitionRepository.Get() select new { TeamDefinitionId = a.TeamDefinitionId, Code = a.Code, Name = a.Name, Position = a.Position, Year = a.Year, CanClassified = a.CanClassified, CanUseStaffId = a.CanUseStaffId, Period = a.Period, CompanyCode = a.CompanyCode, Active = a.Active, Deleted = a.Deleted, CreatedBy = a.CreatedBy, CreatedOn = a.CreatedOn, UpdatedBy = a.UpdatedBy, UpdatedOn = a.UpdatedOn, RowVersion = a.RowVersion }).Where(u => u.TeamDefinitionId == teamDefinitionId && u.Year == setup.Year && u.Period == setup.Period).FirstOrDefault();

                        TeamDefinition teamDefinitionEntity = new TeamDefinition();

                        teamDefinitionEntity.TeamDefinitionId = query.TeamDefinitionId;
                        teamDefinitionEntity.Code = query.Code;
                        teamDefinitionEntity.Name = query.Name;
                        teamDefinitionEntity.Position = query.Position;
                        teamDefinitionEntity.Year = query.Year;
                        teamDefinitionEntity.CanClassified = query.CanClassified;
                        teamDefinitionEntity.CanUseStaffId = query.CanUseStaffId;
                        teamDefinitionEntity.Period = query.Period;
                        teamDefinitionEntity.CompanyCode = query.CompanyCode;
                        teamDefinitionEntity.Active = query.Active;
                        teamDefinitionEntity.Deleted = query.Deleted;
                        teamDefinitionEntity.CreatedBy = query.CreatedBy;
                        teamDefinitionEntity.CreatedOn = query.CreatedOn;
                        teamDefinitionEntity.UpdatedBy = query.UpdatedBy;
                        teamDefinitionEntity.UpdatedOn = query.UpdatedOn;
                        teamDefinitionEntity.RowVersion = query.RowVersion;

                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("TeamDefinition with ID of {0} is not in database", teamDefinitionId));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;

                    }
                    else
                    {
                        TeamDefinition teamDefinitionEntity = teamDefinitionRepository.Get().Where(c => c.TeamDefinitionId == teamDefinitionId && c.Year == setup.Year && c.Period == setup.Period).FirstOrDefault();
                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("TeamDefinition with ID of {0} is not in database", teamDefinitionId));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;
                    }

                }

                //ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

                //TeamDefinition teamDefinitionEntity = teamDefinitionRepository.Get(teamDefinitionId);
                //if (teamDefinitionEntity == null)
                //{
                //    NotFoundException ex = new NotFoundException(string.Format("TeamDefinition with ID of {0} is not in database", teamDefinitionId));
                //    throw new FaultException<NotFoundException>(ex, ex.Message);
                //}

                //return teamDefinitionEntity;
            });
        }

        public TeamDefinition GetTeamDefinitionByCode(string code)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();
                ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

                var setup = GetSetup();

                if (setup.Period == null)
                {
                    if (GetSetup().SwithMode == "Team Classification")
                    {

                        var query = (from b in teamClassificationRepository.Get().Take(1) select new { TeamDefinitionId = setup.LevelId.Value, Code = setup.LevelId.ToString(), Name = "Team", Position = 5, Year = setup.Year, CanClassified = false, CanUseStaffId = false, Period = b.Period, CompanyCode = b.CompanyCode, Active = b.Active, Deleted = b.Deleted, CreatedBy = b.CreatedBy, CreatedOn = b.CreatedOn, UpdatedBy = b.UpdatedBy, UpdatedOn = b.UpdatedOn, RowVersion = b.RowVersion })
                             .Concat(from a in teamDefinitionRepository.Get() select new { TeamDefinitionId = a.TeamDefinitionId, Code = a.Code, Name = a.Name, Position = a.Position, Year = a.Year, CanClassified = a.CanClassified, CanUseStaffId = a.CanUseStaffId, Period = a.Period, CompanyCode = a.CompanyCode, Active = a.Active, Deleted = a.Deleted, CreatedBy = a.CreatedBy, CreatedOn = a.CreatedOn, UpdatedBy = a.UpdatedBy, UpdatedOn = a.UpdatedOn, RowVersion = a.RowVersion }).Where(u => u.Code == code && u.Year == setup.Year).FirstOrDefault();

                        TeamDefinition teamDefinitionEntity = new TeamDefinition();

                        teamDefinitionEntity.TeamDefinitionId = query.TeamDefinitionId;
                        teamDefinitionEntity.Code = query.Code;
                        teamDefinitionEntity.Name = query.Name;
                        teamDefinitionEntity.Position = query.Position;
                        teamDefinitionEntity.Year = query.Year;
                        teamDefinitionEntity.CanClassified = query.CanClassified;
                        teamDefinitionEntity.CanUseStaffId = query.CanUseStaffId;
                        teamDefinitionEntity.Period = query.Period;
                        teamDefinitionEntity.CompanyCode = query.CompanyCode;
                        teamDefinitionEntity.Active = query.Active;
                        teamDefinitionEntity.Deleted = query.Deleted;
                        teamDefinitionEntity.CreatedBy = query.CreatedBy;
                        teamDefinitionEntity.CreatedOn = query.CreatedOn;
                        teamDefinitionEntity.UpdatedBy = query.UpdatedBy;
                        teamDefinitionEntity.UpdatedOn = query.UpdatedOn;
                        teamDefinitionEntity.RowVersion = query.RowVersion;

                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("TeamDefinition with Code of {0} is not in database", code));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;

                    }
                    else
                    {
                        TeamDefinition teamDefinitionEntity = teamDefinitionRepository.Get().Where(c => c.Code == code && c.Year == setup.Year).FirstOrDefault();
                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("TeamDefinition with Code of {0} is not in database", code));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;
                    }

                }
                else
                {
                    if (GetSetup().SwithMode == "Team Classification")
                    {

                        var query = (from b in teamClassificationRepository.Get().Take(1) select new { TeamDefinitionId = setup.LevelId.Value, Code = setup.LevelId.ToString(), Name = "Team", Position = 5, Year = setup.Year, CanClassified = false, CanUseStaffId = false, Period = setup.Period, CompanyCode = b.CompanyCode, Active = b.Active, Deleted = b.Deleted, CreatedBy = b.CreatedBy, CreatedOn = b.CreatedOn, UpdatedBy = b.UpdatedBy, UpdatedOn = b.UpdatedOn, RowVersion = b.RowVersion })
                             .Concat(from a in teamDefinitionRepository.Get() select new { TeamDefinitionId = a.TeamDefinitionId, Code = a.Code, Name = a.Name, Position = a.Position, Year = a.Year, CanClassified = a.CanClassified, CanUseStaffId = a.CanUseStaffId, Period = a.Period, CompanyCode = a.CompanyCode, Active = a.Active, Deleted = a.Deleted, CreatedBy = a.CreatedBy, CreatedOn = a.CreatedOn, UpdatedBy = a.UpdatedBy, UpdatedOn = a.UpdatedOn, RowVersion = a.RowVersion }).Where(u => u.Code == code && u.Year == setup.Year && u.Period == setup.Period).FirstOrDefault();

                        TeamDefinition teamDefinitionEntity = new TeamDefinition();

                        teamDefinitionEntity.TeamDefinitionId = query.TeamDefinitionId;
                        teamDefinitionEntity.Code = query.Code;
                        teamDefinitionEntity.Name = query.Name;
                        teamDefinitionEntity.Position = query.Position;
                        teamDefinitionEntity.Year = query.Year;
                        teamDefinitionEntity.CanClassified = query.CanClassified;
                        teamDefinitionEntity.CanUseStaffId = query.CanUseStaffId;
                        teamDefinitionEntity.Period = query.Period;
                        teamDefinitionEntity.CompanyCode = query.CompanyCode;
                        teamDefinitionEntity.Active = query.Active;
                        teamDefinitionEntity.Deleted = query.Deleted;
                        teamDefinitionEntity.CreatedBy = query.CreatedBy;
                        teamDefinitionEntity.CreatedOn = query.CreatedOn;
                        teamDefinitionEntity.UpdatedBy = query.UpdatedBy;
                        teamDefinitionEntity.UpdatedOn = query.UpdatedOn;
                        teamDefinitionEntity.RowVersion = query.RowVersion;

                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("TeamDefinition with Code of {0} is not in database", code));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;

                    }
                    else
                    {
                        TeamDefinition teamDefinitionEntity = teamDefinitionRepository.Get().Where(c => c.Code == code && c.Year == setup.Year && c.Period == setup.Period).FirstOrDefault();
                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("TeamDefinition with Code of {0} is not in database", code));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;
                    }

                }


                //ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

                //var setUp = GetSetup();

                //if (setUp.Period == null)
                //{
                //    TeamDefinition teamDefinitionEntity = teamDefinitionRepository.Get().Where(c => c.Code == code && c.Year == setUp.Year).FirstOrDefault();

                //    return teamDefinitionEntity;
                //}
                //else
                //{
                //    TeamDefinition teamDefinitionEntity = teamDefinitionRepository.Get().Where(c => c.Code == code && c.Year == setUp.Year && c.Period == setUp.Period).FirstOrDefault();

                //    return teamDefinitionEntity;
                //}


            });
        }

        public IEnumerable<TeamDefinition> GetAllTeamDefinitions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                var setup = GetSetup();

                ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();
                ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

                if (setup.Period == null)
                {
                    if (GetSetup().SwithMode == "Team Classification")
                    {

                        var query = (from b in teamClassificationRepository.Get().Take(1) select new { TeamDefinitionId = setup.LevelId.Value, Code = setup.LevelId.ToString(), Name = "Team", Position = 2, Year = setup.Year, CanClassified = false, CanUseStaffId = false, Period = b.Period, CompanyCode = b.CompanyCode, Active = b.Active, Deleted = b.Deleted, CreatedBy = b.CreatedBy, CreatedOn = b.CreatedOn, UpdatedBy = b.UpdatedBy, UpdatedOn = b.UpdatedOn, RowVersion = b.RowVersion })
                             .Concat(from a in teamDefinitionRepository.Get() select new { TeamDefinitionId = a.TeamDefinitionId, Code = a.Code, Name = a.Name, Position = a.Position, Year = a.Year, CanClassified = a.CanClassified, CanUseStaffId = a.CanUseStaffId, Period = a.Period, CompanyCode = a.CompanyCode, Active = a.Active, Deleted = a.Deleted, CreatedBy = a.CreatedBy, CreatedOn = a.CreatedOn, UpdatedBy = a.UpdatedBy, UpdatedOn = a.UpdatedOn, RowVersion = a.RowVersion }).Where(u => u.Year == setup.Year);

                        var teamDefinitionEntity = from r in query
                                                   select new TeamDefinition()
                                                   {
                                                       TeamDefinitionId = r.TeamDefinitionId,
                                                       Code = r.Code,
                                                       Name = r.Name,
                                                       Position = r.Position,
                                                       Year = r.Year,
                                                       CanClassified = r.CanClassified,
                                                       CanUseStaffId = r.CanUseStaffId,
                                                       Period = r.Period,
                                                       CompanyCode = r.CompanyCode,
                                                       Active = r.Active,
                                                       Deleted = r.Deleted,
                                                       CreatedBy = r.CreatedBy,
                                                       CreatedOn = r.CreatedOn,
                                                       UpdatedBy = r.UpdatedBy,
                                                       UpdatedOn = r.UpdatedOn,
                                                       RowVersion = r.RowVersion,
                                                   };



                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("Data not in database"));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;

                    }
                    else
                    {
                        IEnumerable<TeamDefinition> teamDefinitionEntity = teamDefinitionRepository.Get().Where(c => c.Year == setup.Year).ToList();
                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("Data not in database"));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;
                    }

                }
                else
                {
                    if (GetSetup().SwithMode == "Team Classification")
                    {


                        var query = (from b in teamClassificationRepository.Get().Take(1) select new { TeamDefinitionId = setup.LevelId.Value, Code = setup.LevelId.ToString(), Name = "Team", Position = 5, Year = setup.Year, CanClassified = false, CanUseStaffId = false, Period = b.Period, CompanyCode = b.CompanyCode, Active = b.Active, Deleted = b.Deleted, CreatedBy = b.CreatedBy, CreatedOn = b.CreatedOn, UpdatedBy = b.UpdatedBy, UpdatedOn = b.UpdatedOn, RowVersion = b.RowVersion })
                           .Concat(from a in teamDefinitionRepository.Get() select new { TeamDefinitionId = a.TeamDefinitionId, Code = a.Code, Name = a.Name, Position = a.Position, Year = a.Year, CanClassified = a.CanClassified, CanUseStaffId = a.CanUseStaffId, Period = a.Period, CompanyCode = a.CompanyCode, Active = a.Active, Deleted = a.Deleted, CreatedBy = a.CreatedBy, CreatedOn = a.CreatedOn, UpdatedBy = a.UpdatedBy, UpdatedOn = a.UpdatedOn, RowVersion = a.RowVersion }).Where(u => u.Year == setup.Year && u.Period == setup.Period);


                        var teamDefinitionEntity = from r in query
                                                   select new TeamDefinition()
                                                   {
                                                       TeamDefinitionId = r.TeamDefinitionId,
                                                       Code = r.Code,
                                                       Name = r.Name,
                                                       Position = r.Position,
                                                       Year = r.Year,
                                                       CanClassified = r.CanClassified,
                                                       CanUseStaffId = r.CanUseStaffId,
                                                       Period = r.Period,
                                                       CompanyCode = r.CompanyCode,
                                                       Active = r.Active,
                                                       Deleted = r.Deleted,
                                                       CreatedBy = r.CreatedBy,
                                                       CreatedOn = r.CreatedOn,
                                                       UpdatedBy = r.UpdatedBy,
                                                       UpdatedOn = r.UpdatedOn,
                                                       RowVersion = r.RowVersion,
                                                   };

                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("Data not in database"));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;

                    }
                    else
                    {
                        IEnumerable<TeamDefinition> teamDefinitionEntity = teamDefinitionRepository.Get().Where(c => c.Year == setup.Year && c.Period == setup.Period).ToList();
                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("Data not in database"));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;
                    }

                }


                //ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

                //var setup = GetSetup();

                //if (setup.Period == null)
                //{
                //    IEnumerable<TeamDefinition> teamDefinition = teamDefinitionRepository.Get().Where(c => c.Year == setup.Year).ToArray();

                //    return teamDefinition.ToArray();
                //}
                //else
                //{
                //    IEnumerable<TeamDefinition> teamDefinition = teamDefinitionRepository.Get().Where(c => c.Year == setup.Year && c.Period == setup.Period).ToArray();

                //    return teamDefinition.ToArray();
                //}

            });
        }


        #endregion

        #region TeamClassificationType operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public TeamClassificationType UpdateTeamClassificationType(TeamClassificationType teamClassificationType)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();

                TeamClassificationType updatedEntity = null;

                if (teamClassificationType.TeamClassificationTypeId == 0)
                {
                    teamClassificationType.Year = GetSetup().Year;
                    updatedEntity = teamClassificationTypeRepository.Add(teamClassificationType);
                }
                else
                    updatedEntity = teamClassificationTypeRepository.Update(teamClassificationType);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTeamClassificationType(int teamClassificationTypeId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();

                teamClassificationTypeRepository.Remove(teamClassificationTypeId);
            });
        }

        public TeamClassificationType GetTeamClassificationType(int teamClassificationTypeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();

                TeamClassificationType teamClassificationTypeEntity = teamClassificationTypeRepository.Get(teamClassificationTypeId);
                if (teamClassificationTypeEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("TeamClassificationType with ID of {0} is not in database", teamClassificationTypeId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return teamClassificationTypeEntity;
            });
        }

        public TeamClassificationType[] GetAllTeamClassificationTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();

                var setup = GetSetup();

                IEnumerable<TeamClassificationType> teamClassificationType = teamClassificationTypeRepository.Get().Where(c => c.Year == setup.Year).ToArray();

                return teamClassificationType.ToArray();
            });
        }

        #endregion

        #region TeamClassification operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public TeamClassification UpdateTeamClassification(TeamClassification teamClassification)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

                TeamClassification updatedEntity = null;

                if (teamClassification.TeamClassificationId == 0)
                {
                    if (GetSetup().Period == null)
                    {

                        teamClassification.Year = GetSetup().Year;
                        updatedEntity = teamClassificationRepository.Add(teamClassification);

                    }
                    else
                    {

                        teamClassification.Year = GetSetup().Year;
                        teamClassification.Period = GetSetup().Period;
                        updatedEntity = teamClassificationRepository.Add(teamClassification);
                    }

                }
                else
                    updatedEntity = teamClassificationRepository.Update(teamClassification);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTeamClassification(int teamClassificationId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

                teamClassificationRepository.Remove(teamClassificationId);
            });
        }

        public TeamClassification GetTeamClassification(int teamClassificationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

                TeamClassification teamClassificationEntity = teamClassificationRepository.Get(teamClassificationId);
                if (teamClassificationEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("TeamClassification with ID of {0} is not in database", teamClassificationId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return teamClassificationEntity;
            });
        }

        public TeamClassification[] GetAllTeamClassifications()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

                var setup = GetSetup();

                if (setup.Period == null)
                {
                    IEnumerable<TeamClassification> teamClassification = teamClassificationRepository.Get().Where(c => c.Year == setup.Year).ToArray();

                    return teamClassification.ToArray();
                }
                else
                {
                    IEnumerable<TeamClassification> teamClassification = teamClassificationRepository.Get().Where(c => c.Year == setup.Year && c.Period == setup.Period).ToArray();

                    return teamClassification.ToArray();
                }


            });
        }

        public TeamClassification[] GetTeamClassifications(string typeCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();
                ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

                var setup = GetSetup();

                var classificationType = teamClassificationTypeRepository.Get().Where(c => c.Code == typeCode).FirstOrDefault();

                IEnumerable<TeamClassification> teamClassification = null;

                if (setup.Period == null)
                {
                    if (classificationType != null)
                        teamClassification = teamClassificationRepository.Get().Where(c => c.Year == setup.Year && c.ClassificationTypeCode == typeCode && c.Level == classificationType.MaximumLevel).ToArray();

                    return teamClassification.ToArray();
                }
                else
                {
                    if (classificationType != null)
                        teamClassification = teamClassificationRepository.Get().Where(c => c.Year == setup.Year && c.ClassificationTypeCode == typeCode && c.Level == classificationType.MaximumLevel && c.Period == setup.Period).ToArray();

                    return teamClassification.ToArray();
                }

            });
        }

        #endregion

        #region Team operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Team UpdateTeam(Team team)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();
                IDatabaseRepository databaseRepository = _DataRepositoryFactory.GetDataRepository<IDatabaseRepository>();
                var companydb = databaseRepository.Get().Where(c => c.CompanyCode == DataConnector.CompanyCode).FirstOrDefault();

                Team updatedEntity = null;

                if (team.TeamId == 0)
                {

                    if (GetSetup().Period == null)
                    {
                        team.Year = GetSetup().Year;
                        team.CompanyCode = companydb.CompanyCode;
                        updatedEntity = teamRepository.Add(team);
                    }
                    else
                    {
                        team.Year = GetSetup().Year;
                        team.Period = GetSetup().Period;
                        team.CompanyCode = companydb.CompanyCode;
                        updatedEntity = teamRepository.Add(team);
                    }

                }
                else
                    team.CompanyCode = companydb.CompanyCode;
                updatedEntity = teamRepository.Update(team);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTeam(int teamId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

                teamRepository.Remove(teamId);
            });
        }

        public Team GetTeam(int teamId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

                Team teamEntity = teamRepository.Get(teamId);
                if (teamEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Team with ID of {0} is not in database", teamId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return teamEntity;
            });
        }

        public Team[] GetParentTeams(string definitionCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();
                ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

                ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
                var setUp = setupRepository.Get().FirstOrDefault();

                if (setUp.Period == null)
                {
                    var teamDefinition = teamDefinitionRepository.Get().Where(c => c.Code == definitionCode && c.Year == setUp.Year).FirstOrDefault();
                    var parentDefinition = teamDefinitionRepository.Get().Where(c => c.Position == (teamDefinition.Position + 1)).FirstOrDefault();

                    Team[] teams = teamRepository.Get().Where(c => c.DefinitionCode == parentDefinition.Code && c.Year == setUp.Year).OrderBy(c => c.Name).ToArray();

                    return teams;
                }
                else
                {
                    var teamDefinition = teamDefinitionRepository.Get().Where(c => c.Code == definitionCode && c.Year == setUp.Year && c.Period == setUp.Period).FirstOrDefault();
                    var parentDefinition = teamDefinitionRepository.Get().Where(c => c.Position == (teamDefinition.Position + 1)).FirstOrDefault();

                    Team[] teams = teamRepository.Get().Where(c => c.DefinitionCode == parentDefinition.Code && c.Year == setUp.Year && c.Period == setUp.Period).OrderBy(c => c.Name).ToArray();

                    return teams;
                }


            });
        }

        public Team[] GetTeamByLevel(int level)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();
                ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

                ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
                var setUp = setupRepository.Get().FirstOrDefault();

                var teamDefinition = teamDefinitionRepository.Get().Where(c => c.Position == level).FirstOrDefault();

                Team[] teams = null;

                if (setUp.Period == null)
                {
                    if (teamDefinition != null)
                        teams = teamRepository.Get().Where(c => c.DefinitionCode == teamDefinition.Code && c.Year == setUp.Year).OrderBy(c => c.Name).ToArray();

                    return teams;
                }
                else
                {
                    if (teamDefinition != null)
                        teams = teamRepository.Get().Where(c => c.DefinitionCode == teamDefinition.Code && c.Year == setUp.Year && c.Period == setUp.Period).OrderBy(c => c.Name).ToArray();

                    return teams;
                }


            });
        }

        public IEnumerable<Team> GetTeamByDefinition(string definitionCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                var setup = GetSetup();

                ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();
                ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

                if (setup.Period == null)
                {

                    if (GetSetup().SwithMode == "Team Classification")
                    {
                        var query = (from b in teamClassificationRepository.Get() select new { TeamId = b.TeamClassificationId, Code = b.Code, Name = b.Name, ParentCode = b.ParentCode, DefinitionCode = b.Level.ToString(), StaffId = "", CompanyCode = b.CompanyCode, Year = b.Year, Period = b.Period, Active = b.Active, Deleted = b.Deleted, CreatedBy = b.CreatedBy, CreatedOn = b.CreatedOn, UpdatedBy = b.UpdatedBy, UpdatedOn = b.UpdatedOn, RowVersion = b.RowVersion, ModuleOwnerType = ModuleOwnerType.MPR })
                            .Concat(from a in teamRepository.Get() select new { TeamId = a.TeamId, Code = a.Code, Name = a.Name, ParentCode = a.ParentCode, DefinitionCode = a.DefinitionCode, StaffId = a.StaffId, CompanyCode = a.CompanyCode, Year = a.Year, Period = a.Period, Active = a.Active, Deleted = a.Deleted, CreatedBy = a.CreatedBy, CreatedOn = a.CreatedOn, UpdatedBy = a.UpdatedBy, UpdatedOn = a.UpdatedOn, RowVersion = a.RowVersion, ModuleOwnerType = ModuleOwnerType.MPR }).Where(u => u.Year == setup.Year && u.DefinitionCode == definitionCode);

                        var teams = from r in query
                                    select new Team()
                                    {
                                        TeamId = r.TeamId,
                                        Code = r.Code,
                                        Name = r.Name,
                                        ParentCode = r.ParentCode,
                                        DefinitionCode = r.DefinitionCode,
                                        StaffId = r.StaffId,
                                        ModuleOwnerType = ModuleOwnerType.MPR,
                                        Year = r.Year,
                                        Period = r.Period,
                                        CompanyCode = r.CompanyCode,
                                        Active = r.Active,
                                        Deleted = r.Deleted,
                                        CreatedBy = r.CreatedBy,
                                        CreatedOn = r.CreatedOn,
                                        UpdatedBy = r.UpdatedBy,
                                        UpdatedOn = r.UpdatedOn,
                                        RowVersion = r.RowVersion,
                                    };



                        if (teams == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("Data not in database"));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }

                        return teams;

                    }
                    else
                    {
                        Team[] teams = teamRepository.Get().Where(c => c.DefinitionCode == definitionCode && c.Year == setup.Year).OrderBy(c => c.Name).ToArray();

                        return teams;
                    }

                }
                else
                {

                    if (GetSetup().SwithMode == "Team Classification")
                    {
                        var query = (from b in teamClassificationRepository.Get() select new { TeamId = b.TeamClassificationId, Code = b.Code, Name = b.Name, ParentCode = b.ParentCode, DefinitionCode = b.Level.ToString(), StaffId = "", CompanyCode = b.CompanyCode, Year = b.Year, Period = b.Period, Active = b.Active, Deleted = b.Deleted, CreatedBy = b.CreatedBy, CreatedOn = b.CreatedOn, UpdatedBy = b.UpdatedBy, UpdatedOn = b.UpdatedOn, RowVersion = b.RowVersion, ModuleOwnerType = ModuleOwnerType.MPR })
                           .Concat(from a in teamRepository.Get() select new { TeamId = a.TeamId, Code = a.Code, Name = a.Name, ParentCode = a.ParentCode, DefinitionCode = a.DefinitionCode, StaffId = a.StaffId, CompanyCode = a.CompanyCode, Year = a.Year, Period = a.Period, Active = a.Active, Deleted = a.Deleted, CreatedBy = a.CreatedBy, CreatedOn = a.CreatedOn, UpdatedBy = a.UpdatedBy, UpdatedOn = a.UpdatedOn, RowVersion = a.RowVersion, ModuleOwnerType = ModuleOwnerType.MPR }).Where(u => u.Year == setup.Year && u.DefinitionCode == definitionCode && u.Period == setup.Period);

                        var teams = from r in query
                                    select new Team()
                                    {
                                        TeamId = r.TeamId,
                                        Code = r.Code,
                                        Name = r.Name,
                                        ParentCode = r.ParentCode,
                                        DefinitionCode = r.DefinitionCode,
                                        StaffId = r.StaffId,
                                        ModuleOwnerType = ModuleOwnerType.MPR,
                                        Year = r.Year,
                                        Period = r.Period,
                                        CompanyCode = r.CompanyCode,
                                        Active = r.Active,
                                        Deleted = r.Deleted,
                                        CreatedBy = r.CreatedBy,
                                        CreatedOn = r.CreatedOn,
                                        UpdatedBy = r.UpdatedBy,
                                        UpdatedOn = r.UpdatedOn,
                                        RowVersion = r.RowVersion,
                                    };



                        if (teams == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("Data not in database"));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }

                        return teams;

                    }
                    else
                    {
                        Team[] teams = teamRepository.Get().Where(c => c.DefinitionCode == definitionCode && c.Year == setup.Year && c.Period == setup.Period).OrderBy(c => c.Name).ToArray();

                        return teams;
                    }

                }

                ////  ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();
                //ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

                //ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
                //var setUp = setupRepository.Get().FirstOrDefault();

                //if (setUp.Period == null)
                //{
                //    Team[] teams = teamRepository.Get().Where(c => c.DefinitionCode == definitionCode && c.Year == setUp.Year).OrderBy(c => c.Name).ToArray();

                //    return teams;
                //}
                //else
                //{
                //    Team[] teams = teamRepository.Get().Where(c => c.DefinitionCode == definitionCode && c.Year == setUp.Year && c.Period == setUp.Period).OrderBy(c => c.Name).ToArray();

                //    return teams;
                //}


            });
        }

        public TeamData[] GetTeams()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

                ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
                var setUp = setupRepository.Get().FirstOrDefault();

                List<TeamData> team = new List<TeamData>();

                if (setUp.Period == null)
                {
                    IEnumerable<TeamInfo> teamInfos = teamRepository.GetTeams().OrderByDescending(c => c.Team.DefinitionCode).Where(c => c.Team.Year == setUp.Year).ToArray();

                    foreach (var teamInfo in teamInfos)
                    {
                        team.Add(
                            new TeamData
                            {
                                TeamId = teamInfo.Team.EntityId,
                                Code = teamInfo.Team.Code,
                                Name = teamInfo.Team.Name,
                                ModuleOwnerType = teamInfo.Team.ModuleOwnerType,
                                ModuleName = teamInfo.Team.ModuleOwnerType.ToString(),
                                //ParentId = teamInfo.Team.ParentId,
                                ParentCode = teamInfo.Parent != null ? teamInfo.Parent.Code : string.Empty,
                                ParentName = teamInfo.Parent != null ? teamInfo.Parent.Name : "",
                                DefinitionCode = teamInfo.Team.DefinitionCode,
                                CanClassified = true,
                                CanUseStaffId = true,
                                StaffId = teamInfo.Team.StaffId,
                                Position = 1
                            });
                    }

                    return team.Take(50).ToArray();
                }
                else
                {
                    IEnumerable<TeamInfo> teamInfos = teamRepository.GetTeams().OrderByDescending(c => c.Team.DefinitionCode).Where(c => c.Team.Year == setUp.Year && c.Team.Period == setUp.Period).ToArray();

                    foreach (var teamInfo in teamInfos)
                    {
                        team.Add(
                            new TeamData
                            {
                                TeamId = teamInfo.Team.EntityId,
                                Code = teamInfo.Team.Code,
                                Name = teamInfo.Team.Name,
                                ModuleOwnerType = teamInfo.Team.ModuleOwnerType,
                                ModuleName = teamInfo.Team.ModuleOwnerType.ToString(),
                                //ParentId = teamInfo.Team.ParentId,
                                ParentCode = teamInfo.Parent != null ? teamInfo.Parent.Code : string.Empty,
                                ParentName = teamInfo.Parent != null ? teamInfo.Parent.Name : "",
                                DefinitionCode = teamInfo.Team.DefinitionCode,
                                Period = teamInfo.Team.Period,
                                CanClassified = true,
                                CanUseStaffId = true,
                                StaffId = teamInfo.Team.StaffId,
                                Position = 1
                            });
                    }

                    return team.Take(50).ToArray();
                }


            });
        }



        public TeamData[] GetTeamsBySearch(string SearchValue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

                ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
                var setUp = setupRepository.Get().FirstOrDefault();

                List<TeamData> team = new List<TeamData>();

                if (setUp.Period == null)
                {
                    IEnumerable<TeamInfo> teamInfos = teamRepository.GetTeamsBySearch(SearchValue).OrderByDescending(c => c.Team.DefinitionCode).Where(c => c.Team.Year == setUp.Year).ToArray();

                    foreach (var teamInfo in teamInfos)
                    {
                        team.Add(
                            new TeamData
                            {
                                TeamId = teamInfo.Team.EntityId,
                                Code = teamInfo.Team.Code,
                                Name = teamInfo.Team.Name,
                                ModuleOwnerType = teamInfo.Team.ModuleOwnerType,
                                ModuleName = teamInfo.Team.ModuleOwnerType.ToString(),
                                //ParentId = teamInfo.Team.ParentId,
                                ParentCode = teamInfo.Parent != null ? teamInfo.Parent.Code : string.Empty,
                                ParentName = teamInfo.Parent != null ? teamInfo.Parent.Name : "",
                                DefinitionCode = teamInfo.Team.DefinitionCode,
                                CanClassified = true,
                                CanUseStaffId = true,
                                StaffId = teamInfo.Team.StaffId,
                                Position = 1
                            });
                    }

                    return team.ToArray();
                }
                else
                {
                    IEnumerable<TeamInfo> teamInfos = teamRepository.GetTeamsBySearch(SearchValue).OrderByDescending(c => c.Team.DefinitionCode).Where(c => c.Team.Year == setUp.Year && c.Team.Period == setUp.Period).ToArray();

                    foreach (var teamInfo in teamInfos)
                    {
                        team.Add(
                            new TeamData
                            {
                                TeamId = teamInfo.Team.EntityId,
                                Code = teamInfo.Team.Code,
                                Name = teamInfo.Team.Name,
                                ModuleOwnerType = teamInfo.Team.ModuleOwnerType,
                                ModuleName = teamInfo.Team.ModuleOwnerType.ToString(),
                                //ParentId = teamInfo.Team.ParentId,
                                ParentCode = teamInfo.Parent != null ? teamInfo.Parent.Code : string.Empty,
                                ParentName = teamInfo.Parent != null ? teamInfo.Parent.Name : "",
                                DefinitionCode = teamInfo.Team.DefinitionCode,
                                Period = teamInfo.Team.Period,
                                CanClassified = true,
                                CanUseStaffId = true,
                                StaffId = teamInfo.Team.StaffId,
                                Position = 1
                            });
                    }

                    return team.ToArray();
                }


            });
        }

        #endregion

        #region TeamClassificationMap operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public TeamClassificationMap UpdateTeamClassificationMap(TeamClassificationMap teamClassificationMap)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationMapRepository teamClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationMapRepository>();

                TeamClassificationMap updatedEntity = null;

                if (teamClassificationMap.TeamClassificationMapId == 0)
                {
                    teamClassificationMap.Year = GetSetup().Year;
                    updatedEntity = teamClassificationMapRepository.Add(teamClassificationMap);
                }
                else
                    updatedEntity = teamClassificationMapRepository.Update(teamClassificationMap);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTeamClassificationMap(int teamClassificationMapId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationMapRepository teamClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationMapRepository>();

                teamClassificationMapRepository.Remove(teamClassificationMapId);
            });
        }

        public TeamClassificationMap GetTeamClassificationMap(int teamClassificationMapId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationMapRepository teamClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationMapRepository>();

                TeamClassificationMap teamClassificationMapEntity = teamClassificationMapRepository.Get(teamClassificationMapId);
                if (teamClassificationMapEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("TeamClassificationMap with ID of {0} is not in database", teamClassificationMapId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return teamClassificationMapEntity;
            });
        }

        public TeamClassificationMap[] GetAllTeamClassificationMaps(string misCode, string definitionCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationMapRepository teamClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationMapRepository>();

                var setup = GetSetup();

                IEnumerable<TeamClassificationMap> teamClassificationMap = teamClassificationMapRepository.Get().Where(c => c.Year == setup.Year && c.DefinitionCode == definitionCode && c.MisCode == misCode).ToArray();

                return teamClassificationMap.ToArray();
            });
        }

        #endregion

        #region AccountOfficerDetail operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public AccountOfficerDetail UpdateAccountOfficerDetail(AccountOfficerDetail accountOfficerDetail)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountOfficerDetailRepository accountOfficerDetailRepository = _DataRepositoryFactory.GetDataRepository<IAccountOfficerDetailRepository>();

                AccountOfficerDetail updatedEntity = null;

                if (accountOfficerDetail.AccountOfficerDetailId == 0)
                {
                    accountOfficerDetail.Year = GetSetup().Year;
                    updatedEntity = accountOfficerDetailRepository.Add(accountOfficerDetail);
                }
                else
                    updatedEntity = accountOfficerDetailRepository.Update(accountOfficerDetail);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteAccountOfficerDetail(int accountOfficerDetailId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountOfficerDetailRepository accountOfficerDetailRepository = _DataRepositoryFactory.GetDataRepository<IAccountOfficerDetailRepository>();

                accountOfficerDetailRepository.Remove(accountOfficerDetailId);
            });
        }

        public AccountOfficerDetail GetAccountOfficerDetail(int accountOfficerDetailId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountOfficerDetailRepository accountOfficerDetailRepository = _DataRepositoryFactory.GetDataRepository<IAccountOfficerDetailRepository>();

                AccountOfficerDetail accountOfficerDetailEntity = accountOfficerDetailRepository.Get(accountOfficerDetailId);
                if (accountOfficerDetailEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("AccountOfficerDetail with ID of {0} is not in database", accountOfficerDetailId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return accountOfficerDetailEntity;
            });
        }

        public AccountOfficerDetail[] GetAllAccountOfficerDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountOfficerDetailRepository accountOfficerDetailRepository = _DataRepositoryFactory.GetDataRepository<IAccountOfficerDetailRepository>();

                var setup = GetSetup();

                IEnumerable<AccountOfficerDetail> accountOfficerDetail = accountOfficerDetailRepository.Get().Where(c => c.Year == setup.Year).ToArray();

                return accountOfficerDetail.ToArray();
            });
        }

        #endregion

        #region BranchDefaultMIS operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public BranchDefaultMIS UpdateBranchDefaultMIS(BranchDefaultMIS branchDefaultMIS)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBranchDefaultMISRepository branchDefaultMISRepository = _DataRepositoryFactory.GetDataRepository<IBranchDefaultMISRepository>();

                BranchDefaultMIS updatedEntity = null;

                if (branchDefaultMIS.BranchDefaultMISId == 0)
                {
                    branchDefaultMIS.Year = GetSetup().Year;
                    updatedEntity = branchDefaultMISRepository.Add(branchDefaultMIS);
                }
                else
                    updatedEntity = branchDefaultMISRepository.Update(branchDefaultMIS);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBranchDefaultMIS(int branchDefaultMISId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBranchDefaultMISRepository branchDefaultMISRepository = _DataRepositoryFactory.GetDataRepository<IBranchDefaultMISRepository>();

                branchDefaultMISRepository.Remove(branchDefaultMISId);
            });
        }

        public BranchDefaultMIS GetBranchDefaultMIS(int branchDefaultMISId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBranchDefaultMISRepository branchDefaultMISRepository = _DataRepositoryFactory.GetDataRepository<IBranchDefaultMISRepository>();

                BranchDefaultMIS branchDefaultMISEntity = branchDefaultMISRepository.Get(branchDefaultMISId);
                if (branchDefaultMISEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BranchDefaultMIS with ID of {0} is not in database", branchDefaultMISId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return branchDefaultMISEntity;
            });
        }

        public BranchDefaultMIS[] GetAllBranchDefaultMISs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBranchDefaultMISRepository branchDefaultMISRepository = _DataRepositoryFactory.GetDataRepository<IBranchDefaultMISRepository>();

                var setup = GetSetup();

                IEnumerable<BranchDefaultMIS> branchDefaultMIS = branchDefaultMISRepository.Get().Where(c => c.Year == setup.Year).ToArray();

                return branchDefaultMIS.ToArray();
            });
        }

        #endregion

        #region ManagementTree operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ManagementTree UpdateManagementTree(ManagementTree managementTree)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IManagementTreeRepository managementTreeRepository = _DataRepositoryFactory.GetDataRepository<IManagementTreeRepository>();

                ManagementTree updatedEntity = null;

                if (managementTree.ManagementTreeId == 0)
                {
                    managementTree.Year = GetSetup().Year;
                    updatedEntity = managementTreeRepository.Add(managementTree);
                }
                else
                    updatedEntity = managementTreeRepository.Update(managementTree);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteManagementTree(int managementTreeId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IManagementTreeRepository managementTreeRepository = _DataRepositoryFactory.GetDataRepository<IManagementTreeRepository>();

                managementTreeRepository.Remove(managementTreeId);
            });
        }

        public ManagementTree GetManagementTree(int managementTreeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IManagementTreeRepository managementTreeRepository = _DataRepositoryFactory.GetDataRepository<IManagementTreeRepository>();

                ManagementTree managementTreeEntity = managementTreeRepository.Get(managementTreeId);
                if (managementTreeEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ManagementTree with ID of {0} is not in database", managementTreeId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return managementTreeEntity;
            });
        }

        public ManagementTreeData[] GetAllManagementTrees()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IManagementTreeRepository managementTreeRepository = _DataRepositoryFactory.GetDataRepository<IManagementTreeRepository>();

                var setup = GetSetup();
                string curYr = setup.Year;
                //IEnumerable<ManagementTreeData> managementTree = managementTreeRepository.Get().Where(c => c.Year == setup.Year).ToArray();

                List<ManagementTreeData> managementTree = new List<ManagementTreeData>();
                IEnumerable<ManagementTreeInfo> managementTreeInfos = managementTreeRepository.GetManagementTrees(curYr).ToArray();

                foreach (var managementTreeInfo in managementTreeInfos)
                {
                    managementTree.Add(
                        new ManagementTreeData
                        {
                            ManagementTreeId = managementTreeInfo.ManagementTree.EntityId,
                            AccountNo = managementTreeInfo.ManagementTree.AccountNo,
                            TeamDefinitionCode = managementTreeInfo.ManagementTree.TeamDefinitionCode,
                            TeamDefinitionName = managementTreeInfo.TeamDefinition.Name,
                            TeamCode = managementTreeInfo.ManagementTree.TeamCode,
                            TeamName = managementTreeInfo.Team.Name,
                            AccountOfficerDefinitionCode = managementTreeInfo.ManagementTree != null ? managementTreeInfo.ManagementTree.AccountOfficerDefinitionCode : string.Empty,
                            AccountOfficerDefinitionName = managementTreeInfo.AccountOfficerDefinition != null ? managementTreeInfo.AccountOfficerDefinition.Name : string.Empty,
                            AccountOfficerCode = managementTreeInfo.ManagementTree != null ? managementTreeInfo.ManagementTree.AccountOfficerCode : string.Empty,
                            AccountOfficerName = managementTreeInfo.AccountOfficer != null ? managementTreeInfo.AccountOfficer.Name : string.Empty,
                            Rate = managementTreeInfo.ManagementTree.Rate,
                            Year = managementTreeInfo.ManagementTree.Year,
                            Active = managementTreeInfo.ManagementTree.Active
                        });
                }


                return managementTree.ToArray();
            });
        }

        #endregion

        #region AccountMIS operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public AccountMIS UpdateAccountMIS(AccountMIS accountMIS)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountMISRepository accountMISRepository = _DataRepositoryFactory.GetDataRepository<IAccountMISRepository>();

                AccountMIS updatedEntity = null;

                if (accountMIS.AccountMISId == 0)
                {
                    accountMIS.Year = GetSetup().Year;
                    updatedEntity = accountMISRepository.Add(accountMIS);
                }
                else
                    updatedEntity = accountMISRepository.Update(accountMIS);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteAccountMIS(int accountMISId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountMISRepository accountMISRepository = _DataRepositoryFactory.GetDataRepository<IAccountMISRepository>();

                accountMISRepository.Remove(accountMISId);
            });
        }

        public AccountMIS GetAccountMIS(int accountMISId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountMISRepository accountMISRepository = _DataRepositoryFactory.GetDataRepository<IAccountMISRepository>();

                AccountMIS accountMISEntity = accountMISRepository.Get(accountMISId);
                if (accountMISEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("AccountMIS with ID of {0} is not in database", accountMISId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return accountMISEntity;
            });
        }

        public AccountMISData[] GetAllAccountMISs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountMISRepository accountMISRepository = _DataRepositoryFactory.GetDataRepository<IAccountMISRepository>();

                //var setup = GetSetup();

                //IEnumerable<AccountMIS> accountMIS = accountMISRepository.Get().Where(c => c.Year == setup.Year).ToArray();

                //return accountMIS.ToArray();

                ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
                var setUp = setupRepository.Get().FirstOrDefault();

                List<AccountMISData> accountMIS = new List<AccountMISData>();
                IEnumerable<AccountMISInfo> accountMISInfos = accountMISRepository.GetAccountMISs().Where(c => c.AccountMIS.Year == setUp.Year).ToArray();

                foreach (var accountMISInfo in accountMISInfos)
                {
                    accountMIS.Add(
                        new AccountMISData
                        {
                            AccountMISId = accountMISInfo.AccountMIS.EntityId,
                            AccountNo = accountMISInfo.AccountMIS.AccountNo,
                            TeamDefinitionCode = accountMISInfo.AccountMIS.TeamDefinitionCode,
                            TeamDefinitionName = accountMISInfo.TeamDefinition.Name,
                            TeamCode = accountMISInfo.AccountMIS.TeamCode,
                            TeamName = accountMISInfo.Team.Name,
                            AccountOfficerDefinitionCode = accountMISInfo.AccountMIS != null ? accountMISInfo.AccountMIS.AccountOfficerDefinitionCode : string.Empty,
                            AccountOfficerDefinitionName = accountMISInfo.AccountOfficerDefinition != null ? accountMISInfo.AccountOfficerDefinition.Name : string.Empty,
                            AccountOfficerCode = accountMISInfo.AccountMIS != null ? accountMISInfo.AccountMIS.AccountOfficerCode : string.Empty,
                            AccountOfficerName = accountMISInfo.AccountOfficer != null ? accountMISInfo.AccountOfficer.Name : string.Empty,
                            AccountName = accountMISInfo.CustAccount != null ? accountMISInfo.CustAccount.AccountName : string.Empty,
                            //Rate = accountMISInfo.AccountMIS.Rate,
                            Year = accountMISInfo.AccountMIS.Year,
                            Active = accountMISInfo.AccountMIS.Active
                        });
                }


                return accountMIS.ToArray();
            });
        }

        public void DeleteSelectedIds(string selectedIds)
        {
            var connectionString = GetDataConnection();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("MultipleDeletion", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@IdLists",
                    Value = selectedIds,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@pageOwner",
                    Value = "AcctMIS"
                });
                cmd.CommandTimeout = 0;

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }

        }

        #endregion

        #region MISReplacement operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MISReplacement UpdateMISReplacement(MISReplacement misReplacement)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMISReplacementRepository misReplacementRepository = _DataRepositoryFactory.GetDataRepository<IMISReplacementRepository>();

                MISReplacement updatedEntity = null;

                if (misReplacement.MISReplacementId == 0)
                {
                    misReplacement.Year = GetSetup().Year;
                    updatedEntity = misReplacementRepository.Add(misReplacement);
                }
                else
                    updatedEntity = misReplacementRepository.Update(misReplacement);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMISReplacement(int misReplacementId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMISReplacementRepository misReplacementRepository = _DataRepositoryFactory.GetDataRepository<IMISReplacementRepository>();

                misReplacementRepository.Remove(misReplacementId);
            });
        }

        public MISReplacement GetMISReplacement(int misReplacementId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMISReplacementRepository misReplacementRepository = _DataRepositoryFactory.GetDataRepository<IMISReplacementRepository>();

                MISReplacement misReplacementEntity = misReplacementRepository.Get(misReplacementId);
                if (misReplacementEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MISReplacement with ID of {0} is not in database", misReplacementId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return misReplacementEntity;
            });
        }

        public MISReplacement[] GetAllMISReplacements()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMISReplacementRepository misReplacementRepository = _DataRepositoryFactory.GetDataRepository<IMISReplacementRepository>();

                var setup = GetSetup();

                IEnumerable<MISReplacement> misReplacement = misReplacementRepository.Get().Where(c => c.Year == setup.Year).ToArray();

                return misReplacement.ToArray();
            });
        }

        #endregion

        #region SetUp operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SetUp UpdateMPRSetup(SetUp mprMPRSetup)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISetUpRepository setUpRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();

                SetUp updatedEntity = null;

                if (mprMPRSetup.SetupId == 0)
                    updatedEntity = setUpRepository.Add(mprMPRSetup);
                else
                    updatedEntity = setUpRepository.Update(mprMPRSetup);

                return updatedEntity;
            });
        }

        public SetUp GetFirstMPRSetup()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISetUpRepository setUpRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();

                SetUp setUpEntity = setUpRepository.Get().FirstOrDefault();

                //}

                return setUpEntity;
            });

        }

        public MPRSetupData[] GetFirstMPRSetups()
        {

            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRSetUpRepository setUpRepository = _DataRepositoryFactory.GetDataRepository<IMPRSetUpRepository>();


                List<MPRSetupData> mprSetUp = new List<MPRSetupData>();
                IEnumerable<MPRSetUpInfo> mprSetupInfos = setUpRepository.GetFirstMPRSetUps().ToArray();

                foreach (var mprSetupInfo in mprSetupInfos)
                {
                    mprSetUp.Add(
                        new MPRSetupData
                        {
                            SetupId = mprSetupInfo.SetUp.EntityId,
                            ExcoDefinitionCode = mprSetupInfo.SetUp.ExcoDefinitionCode,
                            ExcoTeamCode = mprSetupInfo.SetUp.ExcoTeamCode,
                            AccountLenght = mprSetupInfo.SetUp.AccountLenght,
                            Year = mprSetupInfo.SetUp.Year,
                            PoolOption = mprSetupInfo.SetUp.PoolOption,
                            PoolName = mprSetupInfo.SetUp.PoolOption.ToString(),
                            Period = mprSetupInfo.SetUp.Period,
                            SwithMode = mprSetupInfo.SetUp.SwithMode,
                            LevelId = mprSetupInfo.SetUp.LevelId,
                            Active = mprSetupInfo.SetUp.Active
                        });
                }

                return mprSetUp.ToArray();
            });
        }

        #endregion

        #region TransferPrice operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public TransferPrice UpdateTransferPrice(TransferPrice transferPrice)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITransferPriceRepository transferPriceRepository = _DataRepositoryFactory.GetDataRepository<ITransferPriceRepository>();

                TransferPrice updatedEntity = null;

                if (transferPrice.TransferPriceId == 0)
                {
                    transferPrice.Year = GetSetup().Year;
                    updatedEntity = transferPriceRepository.Add(transferPrice);
                }
                else
                    updatedEntity = transferPriceRepository.Update(transferPrice);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTransferPrice(int transferPriceId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITransferPriceRepository transferPriceRepository = _DataRepositoryFactory.GetDataRepository<ITransferPriceRepository>();

                transferPriceRepository.Remove(transferPriceId);
            });
        }

        public TransferPrice GetTransferPrice(int transferPriceId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITransferPriceRepository transferPriceRepository = _DataRepositoryFactory.GetDataRepository<ITransferPriceRepository>();

                TransferPrice transferPriceEntity = transferPriceRepository.Get(transferPriceId);
                if (transferPriceEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("TransferPrice with ID of {0} is not in database", transferPriceId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return transferPriceEntity;
            });
        }

        public TransferPriceData[] GetAllTransferPrices()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITransferPriceRepository transferPriceRepository = _DataRepositoryFactory.GetDataRepository<ITransferPriceRepository>();

                //var setUp = GetSetup();

                //IEnumerable<TransferPrice> transferPrice = transferPriceRepository.Get().Where (c=>c.Year == setUp.Year).ToArray();

                List<TransferPriceData> transferPrice = new List<TransferPriceData>();
                IEnumerable<TransferPriceInfo> transferPriceInfos = transferPriceRepository.GetTransferPrices().ToArray();

                foreach (var transferPriceInfo in transferPriceInfos)
                {
                    transferPrice.Add(
                        new TransferPriceData
                        {
                            TransferPriceId = transferPriceInfo.TransferPrice.EntityId,
                            ProductCode = transferPriceInfo.TransferPrice.ProductCode,
                            ProductName = transferPriceInfo.TransferPrice != null ? transferPriceInfo.Product.Name : string.Empty,
                            CaptionCode = transferPriceInfo.TransferPrice.CaptionCode,
                            CaptionName = transferPriceInfo.TransferPrice != null ? transferPriceInfo.BSCaption.CaptionName : string.Empty,
                            Rate = transferPriceInfo.TransferPrice.Rate,
                            DefinitionCode = transferPriceInfo.TransferPrice.DefinitionCode,
                            DefinitionName = transferPriceInfo.TransferPrice != null ? transferPriceInfo.TeamDefinition.Name : string.Empty,
                            MisCode = transferPriceInfo.TransferPrice.MisCode,
                            MisName = transferPriceInfo.TransferPrice != null ? transferPriceInfo.Team.Name : string.Empty,
                            Year = transferPriceInfo.TransferPrice.Year,
                            Period = transferPriceInfo.TransferPrice.Period,
                            SolutionId = transferPriceInfo.TransferPrice.SolutionId,
                            SolutionName = string.Empty,
                            CompanyCode = transferPriceInfo.TransferPrice.CompanyCode,
                            Active = transferPriceInfo.TransferPrice.Active
                        });
                }


                return transferPrice.ToArray();
            });
        }
        #endregion

        #region AccountTransferPrice operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public AccountTransferPrice UpdateAccountTransferPrice(AccountTransferPrice accountTransferPrice)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountTransferPriceRepository accountTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IAccountTransferPriceRepository>();

                AccountTransferPrice updatedEntity = null;

                if (accountTransferPrice.AccountTransferPriceId == 0)
                {
                    accountTransferPrice.Year = GetSetup().Year;
                    updatedEntity = accountTransferPriceRepository.Add(accountTransferPrice);
                }
                else
                    updatedEntity = accountTransferPriceRepository.Update(accountTransferPrice);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteAccountTransferPrice(int accountTransferPriceId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountTransferPriceRepository accountTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IAccountTransferPriceRepository>();

                accountTransferPriceRepository.Remove(accountTransferPriceId);
            });
        }

        public AccountTransferPrice GetAccountTransferPrice(int accountTransferPriceId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountTransferPriceRepository accountTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IAccountTransferPriceRepository>();

                AccountTransferPrice accountTransferPriceEntity = accountTransferPriceRepository.Get(accountTransferPriceId);
                if (accountTransferPriceEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("AccountTransferPrice with ID of {0} is not in database", accountTransferPriceId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return accountTransferPriceEntity;
            });
        }

        public AccountTransferPriceData[] GetAllAccountTransferPrices()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountTransferPriceRepository accountTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IAccountTransferPriceRepository>();

                // ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
                //var setUp = setupRepository.Get().FirstOrDefault();

                List<AccountTransferPriceData> AccountTransferPrice = new List<AccountTransferPriceData>();
                IEnumerable<AccountTransferPriceInfo> accountTransferPriceInfos = accountTransferPriceRepository.GetAccountTransferPrices().ToArray();

                foreach (var accountTransferPriceInfo in accountTransferPriceInfos)
                {
                    AccountTransferPrice.Add(
                        new AccountTransferPriceData
                        {
                            AccountTransferPriceId = accountTransferPriceInfo.AccountTransferPrice.EntityId,
                            AccountNo = accountTransferPriceInfo.AccountTransferPrice.AccountNo,
                            Category = accountTransferPriceInfo.AccountTransferPrice.Category,
                            CategoryName = accountTransferPriceInfo.AccountTransferPrice.Category.ToString(),
                            Rate = accountTransferPriceInfo.AccountTransferPrice.Rate,
                            Year = accountTransferPriceInfo.AccountTransferPrice.Year,
                            Period = accountTransferPriceInfo.AccountTransferPrice.Period,
                            SolutionId = accountTransferPriceInfo.AccountTransferPrice.SolutionId,
                            SolutionName = "",
                            AccountName = accountTransferPriceInfo.CustAccount != null ? accountTransferPriceInfo.CustAccount.AccountName : string.Empty,
                            Active = true,

                        });
                }

                return AccountTransferPrice.ToArray();
            });
        }

        #endregion

        #region GeneralTransferPrice operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public GeneralTransferPrice UpdateGeneralTransferPrice(GeneralTransferPrice generalTransferPrice)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGeneralTransferPriceRepository generalTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IGeneralTransferPriceRepository>();

                GeneralTransferPrice updatedEntity = null;

                if (generalTransferPrice.GeneralTransferPriceId == 0)
                {
                    generalTransferPrice.Year = GetSetup().Year;
                    updatedEntity = generalTransferPriceRepository.Add(generalTransferPrice);
                }
                else
                    updatedEntity = generalTransferPriceRepository.Update(generalTransferPrice);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteGeneralTransferPrice(int generalTransferPriceId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGeneralTransferPriceRepository generalTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IGeneralTransferPriceRepository>();

                generalTransferPriceRepository.Remove(generalTransferPriceId);
            });
        }

        public GeneralTransferPrice GetGeneralTransferPrice(int generalTransferPriceId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGeneralTransferPriceRepository generalTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IGeneralTransferPriceRepository>();

                GeneralTransferPrice generalTransferPriceEntity = generalTransferPriceRepository.Get(generalTransferPriceId);
                if (generalTransferPriceEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("General Transfer Price with ID of {0} is not in database", generalTransferPriceId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return generalTransferPriceEntity;
            });
        }

        public GeneralTransferPriceData[] GetAllGeneralTransferPrices()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGeneralTransferPriceRepository generalTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IGeneralTransferPriceRepository>();

                // ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
                //var setUp = setupRepository.Get().FirstOrDefault();

                List<GeneralTransferPriceData> GeneralTransferPrice = new List<GeneralTransferPriceData>();
                IEnumerable<GeneralTransferPrice> generalTransferPriceInfos = generalTransferPriceRepository.Get().ToArray();

                foreach (var generalTransferPriceInfo in generalTransferPriceInfos)
                {
                    GeneralTransferPrice.Add(
                        new GeneralTransferPriceData
                        {
                            GeneralTransferPriceId = generalTransferPriceInfo.EntityId,
                            Category = generalTransferPriceInfo.Category,
                            CategoryName = generalTransferPriceInfo.Category.ToString(),
                            CurrencyType = generalTransferPriceInfo.CurrencyType,
                            CurrencyTypeName = generalTransferPriceInfo.CurrencyType.ToString(),
                            Rate = generalTransferPriceInfo.Rate,
                            Year = generalTransferPriceInfo.Year,
                            Period = generalTransferPriceInfo.Period,
                            DefinitionCode = generalTransferPriceInfo.DefinitionCode,
                            MISCode = generalTransferPriceInfo.MISCode,
                            CompanyCode = generalTransferPriceInfo.CompanyCode,
                            Active = true,

                        });
                }

                return GeneralTransferPrice.ToArray();
            });
        }

        public void DeleteGTPSelectedIds(string selectedIds)
        {
            var connectionString = GetDataConnection();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("MultipleDeletion", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@IdLists",
                    Value = selectedIds,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@pageOwner",
                    Value = "GTP"
                });
                cmd.CommandTimeout = 0;

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }

        }

        #endregion

        #region CustAccount operations

        [OperationBehavior(TransactionScopeRequired = true)]


        public CustAccount[] GetAllCustAccounts()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICustAccountRepository CustAccountRepository = _DataRepositoryFactory.GetDataRepository<ICustAccountRepository>();

                IEnumerable<CustAccount> CustAccounts = CustAccountRepository.Get().ToArray();

                return CustAccounts.ToArray();
            });
        }

        public CustAccount[] GetCustAccounts(string searchType, string searchValue, int number)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICustAccountRepository CustAccountRepository = _DataRepositoryFactory.GetDataRepository<ICustAccountRepository>();
                List<CustAccount> CustAccounts = CustAccountRepository.GetCustomerAccountBySearch(searchType, searchValue, number);


                return CustAccounts.ToArray();
            });
        }


        #endregion

        #region BSExemption operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public BSExemption UpdateBSExemption(BSExemption bsExemption)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSExemptionRepository bsExemptionRepository = _DataRepositoryFactory.GetDataRepository<IBSExemptionRepository>();

                BSExemption updatedEntity = null;

                if (bsExemption.BSExemptionId == 0)
                {

                    updatedEntity = bsExemptionRepository.Add(bsExemption);
                }
                else
                    updatedEntity = bsExemptionRepository.Update(bsExemption);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBSExemption(int bsExemptionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSExemptionRepository bsExemptionRepository = _DataRepositoryFactory.GetDataRepository<IBSExemptionRepository>();

                bsExemptionRepository.Remove(bsExemptionId);
            });
        }

        public BSExemption GetBSExemption(int bsExemptionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSExemptionRepository bsExemptionRepository = _DataRepositoryFactory.GetDataRepository<IBSExemptionRepository>();

                BSExemption bsExemptionEntity = bsExemptionRepository.Get(bsExemptionId);
                if (bsExemptionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BSExemption with ID of {0} is not in database", bsExemptionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return bsExemptionEntity;
            });
        }

        public BSExemption[] GetAllBSExemptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSExemptionRepository bsExemptionRepository = _DataRepositoryFactory.GetDataRepository<IBSExemptionRepository>();


                IEnumerable<BSExemption> bsExemption = bsExemptionRepository.Get().ToArray();

                return bsExemption.ToArray();
            });
        }

        #endregion

        #region MemoAccountMap operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MemoAccountMap UpdateMemoAccountMap(MemoAccountMap memoAccountMap)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoAccountMapRepository memoAccountMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoAccountMapRepository>();

                MemoAccountMap updatedEntity = null;

                if (memoAccountMap.MemoAccountMapId == 0)
                {
                    updatedEntity = memoAccountMapRepository.Add(memoAccountMap);
                }
                else
                    updatedEntity = memoAccountMapRepository.Update(memoAccountMap);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMemoAccountMap(int memoAccountMapId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoAccountMapRepository memoAccountMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoAccountMapRepository>();

                memoAccountMapRepository.Remove(memoAccountMapId);
            });
        }

        public MemoAccountMap GetMemoAccountMap(int memoAccountMapId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoAccountMapRepository memoAccountMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoAccountMapRepository>();

                MemoAccountMap memoAccountMapEntity = memoAccountMapRepository.Get(memoAccountMapId);
                if (memoAccountMapEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MemoAccountMap with ID of {0} is not in database", memoAccountMapId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return memoAccountMapEntity;
            });
        }

        public MemoAccountMapData[] GetAllMemoAccountMaps()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoAccountMapRepository memoAccountMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoAccountMapRepository>();

                //var setUp = GetSetup();

                //IEnumerable<MemoAccountMap> memoAccountMap = memoAccountMapRepository.Get().Where (c=>c.Year == setUp.Year).ToArray();

                List<MemoAccountMapData> memoAccountMap = new List<MemoAccountMapData>();
                IEnumerable<MemoAccountMapInfo> memoAccountMapInfos = memoAccountMapRepository.GetMemoAccountMaps().ToArray();

                foreach (var memoAccountMapInfo in memoAccountMapInfos)
                {
                    memoAccountMap.Add(
                        new MemoAccountMapData
                        {
                            MemoAccountMapId = memoAccountMapInfo.MemoAccountMap.EntityId,
                            AccountNo = memoAccountMapInfo.MemoAccountMap.AccountNo,
                            Code = memoAccountMapInfo.MemoAccountMap.Code,
                            Name = memoAccountMapInfo.MemoUnits != null ? memoAccountMapInfo.MemoUnits.Name : string.Empty,
                            AccountName = memoAccountMapInfo.CustAccount != null ? memoAccountMapInfo.CustAccount.AccountName : string.Empty,
                            //  bsCaptionInfo.Parent != null ? bsCaptionInfo.Parent.EntityId : 0,
                            Active = memoAccountMapInfo.MemoAccountMap.Active
                        });
                }



                return memoAccountMap.ToArray();
            });
        }
        #endregion

        #region MemoGLMap operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MemoGLMap UpdateMemoGLMap(MemoGLMap memoGLMap)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoGLMapRepository memoGLMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoGLMapRepository>();

                MemoGLMap updatedEntity = null;

                if (memoGLMap.MemoGLMapId == 0)
                {

                    updatedEntity = memoGLMapRepository.Add(memoGLMap);
                }
                else
                    updatedEntity = memoGLMapRepository.Update(memoGLMap);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMemoGLMap(int memoGLMapId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoGLMapRepository memoGLMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoGLMapRepository>();

                memoGLMapRepository.Remove(memoGLMapId);
            });
        }

        public MemoGLMap GetMemoGLMap(int memoGLMapId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoGLMapRepository memoGLMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoGLMapRepository>();

                MemoGLMap memoGLMapEntity = memoGLMapRepository.Get(memoGLMapId);
                if (memoGLMapEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MemoGLMap with ID of {0} is not in database", memoGLMapId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return memoGLMapEntity;
            });
        }


        public MemoGLMapData[] GetAllMemoGLMaps()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoGLMapRepository memoGLMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoGLMapRepository>();

                List<MemoGLMapData> memoGLMap = new List<MemoGLMapData>();
                IEnumerable<MemoGLMapInfo> memoGLMapInfos = memoGLMapRepository.GetMemoGLMaps().ToArray();

                foreach (var memoGLMapInfo in memoGLMapInfos)
                {
                    memoGLMap.Add(
                        new MemoGLMapData
                        {
                            MemoGLMapId = memoGLMapInfo.MemoGLMap.EntityId,
                            Code = memoGLMapInfo.MemoGLMap.Code,
                            Name = memoGLMapInfo.MemoUnits.Name,
                            GLCode = memoGLMapInfo.GLDefinition != null ? memoGLMapInfo.GLDefinition.GL_Code : string.Empty,
                            GLDescription = memoGLMapInfo.GLDefinition != null ? memoGLMapInfo.GLDefinition.Description : string.Empty,
                            Active = memoGLMapInfo.MemoGLMap.Active
                        });
                }

                //memoGLMapInfo.GLDefinition != null ? memoGLMapInfo.GLDefinition.GL_Code : string.Empty,
                //memoGLMapInfo.GLDefinition != null ? memoGLMapInfo.GLDefinition.Description : string.Empty,

                return memoGLMap.ToArray();
            });
        }

        #endregion

        #region MemoProductMap operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MemoProductMap UpdateMemoProductMap(MemoProductMap memoProductMap)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoProductMapRepository memoProductMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoProductMapRepository>();

                MemoProductMap updatedEntity = null;

                if (memoProductMap.MemoProductMapId == 0)
                {
                    updatedEntity = memoProductMapRepository.Add(memoProductMap);
                }
                else
                    updatedEntity = memoProductMapRepository.Update(memoProductMap);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMemoProductMap(int memoProductMapId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoProductMapRepository memoProductMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoProductMapRepository>();

                memoProductMapRepository.Remove(memoProductMapId);
            });
        }

        public MemoProductMap GetMemoProductMap(int memoProductMapId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoProductMapRepository memoProductMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoProductMapRepository>();

                MemoProductMap memoProductMapEntity = memoProductMapRepository.Get(memoProductMapId);
                if (memoProductMapEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MemoProductMap with ID of {0} is not in database", memoProductMapId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return memoProductMapEntity;
            });
        }

        public MemoProductMapData[] GetAllMemoProductMaps()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoProductMapRepository memoProductMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoProductMapRepository>();

                List<MemoProductMapData> memoProductMap = new List<MemoProductMapData>();
                IEnumerable<MemoProductMapInfo> memoProductMapInfos = memoProductMapRepository.GetMemoProductMaps().ToArray();

                foreach (var memoProductMapInfo in memoProductMapInfos)
                {
                    memoProductMap.Add(
                        new MemoProductMapData
                        {
                            MemoProductMapId = memoProductMapInfo.MemoProductMap.EntityId,
                            ProductCode = memoProductMapInfo.MemoProductMap.ProductCode,
                            ProductName = memoProductMapInfo.Product != null ? memoProductMapInfo.Product.Name : string.Empty,
                            Code = memoProductMapInfo.MemoProductMap.Code,
                            UnitName = memoProductMapInfo.MemoUnits.Name,
                            Active = memoProductMapInfo.MemoProductMap.Active
                        });
                }


                return memoProductMap.ToArray();
            });
        }
        #endregion

        #region MemoUnits operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MemoUnits UpdateMemoUnits(MemoUnits memoUnit)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoUnitsRepository memoUnitRepository = _DataRepositoryFactory.GetDataRepository<IMemoUnitsRepository>();

                MemoUnits updatedEntity = null;

                if (memoUnit.MemoUnitsId == 0)
                {

                    updatedEntity = memoUnitRepository.Add(memoUnit);
                }
                else
                    updatedEntity = memoUnitRepository.Update(memoUnit);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMemoUnits(int memoUnitId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoUnitsRepository memoUnitRepository = _DataRepositoryFactory.GetDataRepository<IMemoUnitsRepository>();

                memoUnitRepository.Remove(memoUnitId);
            });
        }

        public MemoUnits GetMemoUnits(int memoUnitId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoUnitsRepository memoUnitRepository = _DataRepositoryFactory.GetDataRepository<IMemoUnitsRepository>();

                MemoUnits memoUnitEntity = memoUnitRepository.Get(memoUnitId);
                if (memoUnitEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MemoUnits with ID of {0} is not in database", memoUnitId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return memoUnitEntity;
            });
        }

        public MemoUnits[] GetAllMemoUnits()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoUnitsRepository memoUnitRepository = _DataRepositoryFactory.GetDataRepository<IMemoUnitsRepository>();


                IEnumerable<MemoUnits> memoUnit = memoUnitRepository.Get().ToArray();

                return memoUnit.ToArray();
            });
        }

        #endregion

        #region CaptionMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CaptionMapping UpdateCaptionMapping(CaptionMapping captionMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICaptionMappingRepository captionMappingRepository = _DataRepositoryFactory.GetDataRepository<ICaptionMappingRepository>();

                CaptionMapping updatedEntity = null;

                if (captionMapping.CaptionMappingId == 0)
                {

                    updatedEntity = captionMappingRepository.Add(captionMapping);
                }
                else
                    updatedEntity = captionMappingRepository.Update(captionMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCaptionMapping(int captionMappingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICaptionMappingRepository captionMappingRepository = _DataRepositoryFactory.GetDataRepository<ICaptionMappingRepository>();

                captionMappingRepository.Remove(captionMappingId);
            });
        }

        public CaptionMapping GetCaptionMapping(int captionMappingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICaptionMappingRepository captionMappingRepository = _DataRepositoryFactory.GetDataRepository<ICaptionMappingRepository>();

                CaptionMapping captionMappingEntity = captionMappingRepository.Get(captionMappingId);
                if (captionMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BSExemption with ID of {0} is not in database", captionMappingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return captionMappingEntity;
            });
        }

        public CaptionMapping[] GetAllCaptionMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICaptionMappingRepository captionMappingRepository = _DataRepositoryFactory.GetDataRepository<ICaptionMappingRepository>();


                IEnumerable<CaptionMapping> captionMapping = captionMappingRepository.Get().ToArray();

                return captionMapping.ToArray();
            });
        }

        #endregion

        #region RatioCaptionMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public RatioCaptionMapping UpdateRatioCaptionMapping(RatioCaptionMapping ratioCaptionMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatioCaptionMappingRepository ratioCaptionMappingRepository = _DataRepositoryFactory.GetDataRepository<IRatioCaptionMappingRepository>();

                RatioCaptionMapping updatedEntity = null;

                if (ratioCaptionMapping.RatioCaptionMappingId == 0)
                {

                    updatedEntity = ratioCaptionMappingRepository.Add(ratioCaptionMapping);
                }
                else
                    updatedEntity = ratioCaptionMappingRepository.Update(ratioCaptionMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteRatioCaptionMapping(int ratioCaptionMappingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatioCaptionMappingRepository ratioCaptionMappingRepository = _DataRepositoryFactory.GetDataRepository<IRatioCaptionMappingRepository>();

                ratioCaptionMappingRepository.Remove(ratioCaptionMappingId);
            });
        }

        public RatioCaptionMapping GetRatioCaptionMapping(int ratioCaptionMappingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatioCaptionMappingRepository ratioCaptionMappingRepository = _DataRepositoryFactory.GetDataRepository<IRatioCaptionMappingRepository>();

                RatioCaptionMapping ratioCaptionMappingEntity = ratioCaptionMappingRepository.Get(ratioCaptionMappingId);
                if (ratioCaptionMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("RatioCaptionMapping with ID of {0} is not in database", ratioCaptionMappingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ratioCaptionMappingEntity;
            });
        }

        public RatioCaptionMapping[] GetAllRatioCaptionMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatioCaptionMappingRepository ratioCaptionMappingRepository = _DataRepositoryFactory.GetDataRepository<IRatioCaptionMappingRepository>();


                IEnumerable<RatioCaptionMapping> ratioCaptionMapping = ratioCaptionMappingRepository.Get().ToArray();

                return ratioCaptionMapping.ToArray();
            });
        }

        #endregion

        #region Ratios operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Ratios UpdateRatios(Ratios ratios)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatiosRepository ratiosRepository = _DataRepositoryFactory.GetDataRepository<IRatiosRepository>();

                Ratios updatedEntity = null;

                if (ratios.RatiosId == 0)
                {

                    updatedEntity = ratiosRepository.Add(ratios);
                }
                else
                    updatedEntity = ratiosRepository.Update(ratios);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteRatios(int ratiosId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatiosRepository ratiosRepository = _DataRepositoryFactory.GetDataRepository<IRatiosRepository>();

                ratiosRepository.Remove(ratiosId);
            });
        }

        public Ratios GetRatios(int ratiosId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatiosRepository ratiosRepository = _DataRepositoryFactory.GetDataRepository<IRatiosRepository>();

                Ratios ratiosEntity = ratiosRepository.Get(ratiosId);
                if (ratiosEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Ratios with ID of {0} is not in database", ratiosId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ratiosEntity;
            });
        }

        public Ratios[] GetAllRatios()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatiosRepository ratiosRepository = _DataRepositoryFactory.GetDataRepository<IRatiosRepository>();


                IEnumerable<Ratios> ratios = ratiosRepository.Get().ToArray();

                return ratios.ToArray();
            });
        }

        #endregion

        #region AbcRatio operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public AbcRatio UpdateAbcRatio(AbcRatio abcRatio)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAbcRatioRepository abcRatioRepository = _DataRepositoryFactory.GetDataRepository<IAbcRatioRepository>();

                AbcRatio updatedEntity = null;

                if (abcRatio.AbcRatioId == 0)
                {

                    updatedEntity = abcRatioRepository.Add(abcRatio);
                }
                else
                    updatedEntity = abcRatioRepository.Update(abcRatio);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteAbcRatio(int abcRatioId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAbcRatioRepository abcRatioRepository = _DataRepositoryFactory.GetDataRepository<IAbcRatioRepository>();

                abcRatioRepository.Remove(abcRatioId);
            });
        }

        public AbcRatio GetAbcRatio(int abcRatioId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAbcRatioRepository abcRatioRepository = _DataRepositoryFactory.GetDataRepository<IAbcRatioRepository>();

                AbcRatio abcRatioEntity = abcRatioRepository.Get(abcRatioId);
                if (abcRatioEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("AbcRatio with ID of {0} is not in database", abcRatioId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return abcRatioEntity;
            });
        }

        public AbcRatio[] GetAllAbcRatio()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAbcRatioRepository abcRatioRepository = _DataRepositoryFactory.GetDataRepository<IAbcRatioRepository>();


                IEnumerable<AbcRatio> abcRatio = abcRatioRepository.Get().ToArray();

                return abcRatio.ToArray();
            });
        }

        #endregion

        #region Sbu operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Sbu UpdateSbu(Sbu sbu)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISbuRepository sbuRepository = _DataRepositoryFactory.GetDataRepository<ISbuRepository>();

                Sbu updatedEntity = null;

                if (sbu.SbuId == 0)
                {

                    updatedEntity = sbuRepository.Add(sbu);
                }
                else
                    updatedEntity = sbuRepository.Update(sbu);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSbu(int sbuId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISbuRepository sbuRepository = _DataRepositoryFactory.GetDataRepository<ISbuRepository>();

                sbuRepository.Remove(sbuId);
            });
        }

        public Sbu GetSbu(int sbuId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISbuRepository sbuRepository = _DataRepositoryFactory.GetDataRepository<ISbuRepository>();

                Sbu sbuEntity = sbuRepository.Get(sbuId);
                if (sbuEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Sbu with ID of {0} is not in database", sbuId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return sbuEntity;
            });
        }

        public Sbu[] GetAllSbu()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISbuRepository sbuRepository = _DataRepositoryFactory.GetDataRepository<ISbuRepository>();


                IEnumerable<Sbu> sbu = sbuRepository.Get().ToArray();

                return sbu.ToArray();
            });
        }

        #endregion

        #region SbuType operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SbuType UpdateSbuType(SbuType sbuType)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISbuTypeRepository sbuTypeRepository = _DataRepositoryFactory.GetDataRepository<ISbuTypeRepository>();

                SbuType updatedEntity = null;

                if (sbuType.SbuTypeId == 0)
                {

                    updatedEntity = sbuTypeRepository.Add(sbuType);
                }
                else
                    updatedEntity = sbuTypeRepository.Update(sbuType);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSbuType(int sbuTypeId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISbuTypeRepository sbuTypeRepository = _DataRepositoryFactory.GetDataRepository<ISbuTypeRepository>();

                sbuTypeRepository.Remove(sbuTypeId);
            });
        }

        public SbuType GetSbuType(int sbuTypeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISbuTypeRepository sbuTypeRepository = _DataRepositoryFactory.GetDataRepository<ISbuTypeRepository>();

                SbuType sbuTypeEntity = sbuTypeRepository.Get(sbuTypeId);
                if (sbuTypeEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SbuType with ID of {0} is not in database", sbuTypeId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return sbuTypeEntity;
            });
        }

        public SbuType[] GetAllSbuType()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISbuTypeRepository sbuTypeRepository = _DataRepositoryFactory.GetDataRepository<ISbuTypeRepository>();


                IEnumerable<SbuType> sbuType = sbuTypeRepository.Get().ToArray();

                return sbuType.ToArray();
            });
        }

        #endregion

        #region Servicese operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Servicese UpdateServices(Servicese services)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IServiceseRepository servicesRepository = _DataRepositoryFactory.GetDataRepository<IServiceseRepository>();

                Servicese updatedEntity = null;

                if (services.ServicesId == 0)
                {

                    updatedEntity = servicesRepository.Add(services);
                }
                else
                    updatedEntity = servicesRepository.Update(services);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteServices(int servicesId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IServiceseRepository servicesRepository = _DataRepositoryFactory.GetDataRepository<IServiceseRepository>();

                servicesRepository.Remove(servicesId);
            });
        }

        public Servicese GetServices(int servicesId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IServiceseRepository servicesRepository = _DataRepositoryFactory.GetDataRepository<IServiceseRepository>();

                Servicese servicesEntity = servicesRepository.Get(servicesId);
                if (servicesEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Services with ID of {0} is not in database", servicesId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return servicesEntity;
            });
        }

        public Servicese[] GetAllServices()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IServiceseRepository servicesRepository = _DataRepositoryFactory.GetDataRepository<IServiceseRepository>();


                IEnumerable<Servicese> services = servicesRepository.Get().ToArray();

                return services.ToArray();
            });
        }

        #endregion

        #region MessagingSubscription operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MessagingSubscription UpdateMessagingSubscription(MessagingSubscription messagingSubscription)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMessagingSubscriptionRepository messagingSubscriptionRepository = _DataRepositoryFactory.GetDataRepository<IMessagingSubscriptionRepository>();

                MessagingSubscription updatedEntity = null;

                if (messagingSubscription.MessagingSubscriptionId == 0)
                {
                    //messagingSubscription.Year = GetSetup().Year;
                    updatedEntity = messagingSubscriptionRepository.Add(messagingSubscription);
                    MessagingSubscriptionTriggeredBy(messagingSubscription);
                }
                else
                    updatedEntity = messagingSubscriptionRepository.Update(messagingSubscription);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMessagingSubscription(int messagingSubscriptionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMessagingSubscriptionRepository messagingSubscriptionRepository = _DataRepositoryFactory.GetDataRepository<IMessagingSubscriptionRepository>();

                messagingSubscriptionRepository.Remove(messagingSubscriptionId);
            });
        }

        public MessagingSubscription GetMessagingSubscription(int messagingSubscriptionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMessagingSubscriptionRepository messagingSubscriptionRepository = _DataRepositoryFactory.GetDataRepository<IMessagingSubscriptionRepository>();

                MessagingSubscription messagingSubscriptionEntity = messagingSubscriptionRepository.Get(messagingSubscriptionId);
                if (messagingSubscriptionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MessagingSubscription with ID of {0} is not in database", messagingSubscriptionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return messagingSubscriptionEntity;
            });
        }


        //public MessagingSubscription GetMessagingSubscriptionByRecipients(string recipients)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IMessagingSubscriptionRepository messagingSubscriptionRepository = _DataRepositoryFactory.GetDataRepository<IMessagingSubscriptionRepository>();

        //        MessagingSubscription messagingSubscriptionEntity = messagingSubscriptionRepository.GetByRecipients(recipients);
        //        if (messagingSubscriptionEntity == null)
        //        {
        //            NotFoundException ex = new NotFoundException(string.Format("MessagingSubscription with ID of {0} is not in database", recipients));
        //            throw new FaultException<NotFoundException>(ex, ex.Message);
        //        }

        //        return messagingSubscriptionEntity;
        //    });
        //}



        public Revenue[] GetMessagingSubscriptionByRecipients(string recipients)
        {
            var connectionString = GetDataConnection();

            var revenues = new List<Revenue>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("MPR_GetEmailRecipients", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Levels",
                    Value = recipients,
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var revenue = new Revenue();

                    if (reader["RevenueId"] != DBNull.Value)
                        revenue.RevenueId = int.Parse(reader["RevenueId"].ToString());

                    if (reader["TeamCode"] != DBNull.Value)
                        revenue.TeamCode = reader["TeamCode"].ToString();

                    if (reader["Narrative"] != DBNull.Value)
                        revenue.Narrative = reader["Narrative"].ToString();

                    if (reader["GLCode"] != DBNull.Value)
                        revenue.GLCode = reader["GLCode"].ToString();

                    if (reader["BranchCode"] != DBNull.Value)
                        revenue.BranchCode = reader["BranchCode"].ToString();

                    if (reader["Active"] != DBNull.Value)
                        revenue.Active = bool.Parse(reader["Active"].ToString());

                    revenues.Add(revenue);
                }

                con.Close();
            }

            return revenues.ToArray();
        }

        public DateTime[] GetRecipents()
        {
            //var connectionString = IFRSContext.GetDataConnection();
            var connectionString = GetDataConnection();

            List<DateTime> refno;
            var recipentList = new List<DateTime>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("mpr_GetReportDates", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var myRecipent = new RecipentsModel();
                        if (reader["Rundate"] != DBNull.Value)
                            myRecipent.Rundate = DateTime.Parse(reader["Rundate"].ToString());
                        recipentList.Add(myRecipent.Rundate);
                    }
                    reader.Close();
                    con.Close();
                }

                con.Close();
            }
            return recipentList.ToArray();
        }

        public string[] GetReports()
        {
            //var connectionString = IFRSContext.GetDataConnection();
            var connectionString = GetDataConnection();

            List<string> refno;
            var reportsList = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("mpr_get_email_reports", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var myReport = new ReportModel();
                        if (reader["Description"] != DBNull.Value)
                            myReport.Description = reader["Description"].ToString();
                        reportsList.Add(myReport.Description);
                    }
                    reader.Close();
                    con.Close();
                }

                con.Close();
            }
            return reportsList.ToArray();
        }



        public void MessagingSubscriptionTriggeredBy(MessagingSubscription messagingSubscription)
        {
            var connectionString = GetDataConnection();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("sp_start_job", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@eMessage",
                    Value = messagingSubscription.Recipents,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@eMessage",
                    Value = messagingSubscription.eMessage
                });

                cmd.CommandTimeout = 0;

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }

        }

        #endregion

        #region Staffs operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Staffs UpdateStaffs(Staffs staffs)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IStaffsRepository staffRepository = _DataRepositoryFactory.GetDataRepository<IStaffsRepository>();

                Staffs updatedEntity = null;

                if (staffs.StaffId == 0)
                    updatedEntity = staffRepository.Add(staffs);
                else
                    updatedEntity = staffRepository.Update(staffs);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteStaffs(int staffId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IStaffsRepository staffRepository = _DataRepositoryFactory.GetDataRepository<IStaffsRepository>();

                staffRepository.Remove(staffId);
            });
        }

        public Staffs GetStaffs(int staffId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IStaffsRepository staffRepository = _DataRepositoryFactory.GetDataRepository<IStaffsRepository>();

                Staffs staffEntity = staffRepository.Get(staffId);
                if (staffEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Staffs with ID of {0} is not in database", staffId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return staffEntity;
            });
        }

        public Staffs[] GetAllStaffs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IStaffsRepository staffRepository = _DataRepositoryFactory.GetDataRepository<IStaffsRepository>();

                IEnumerable<Staffs> staffs = staffRepository.Get().Where(c => c.Active);

                return staffs.ToArray();
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

        public string GetDataConnection()
        {
            string connectionString = "";

            if (!string.IsNullOrEmpty(DataConnector.CompanyCode))
            {
                IDatabaseRepository databaseRepository = _DataRepositoryFactory.GetDataRepository<IDatabaseRepository>();
                var companydb = databaseRepository.Get().Where(c => c.CompanyCode == DataConnector.CompanyCode).FirstOrDefault();

                if (companydb == null)
                    throw new Exception("Unable to load company database.");

                connectionString = string.Format("Data Source= {0};Initial Catalog={1};User ={2};Password={3};Integrated Security={4}", companydb.ServerName, companydb.DatabaseName, companydb.UserName, companydb.Password, companydb.IntegratedSecurity);
            }

            return connectionString;
        }

        #endregion

    }
}
