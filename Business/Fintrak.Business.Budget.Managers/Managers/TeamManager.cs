using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Transactions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Business.Budget.Contracts;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Data;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.SystemCore.Entities;
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.SystemCore.Framework;
using Fintrak.Data.Budget.Contracts;

namespace Fintrak.Business.Budget.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]

    public class TeamManager : ManagerBase, ITeamService
    {
        public TeamManager()
        {
        }

        public TeamManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        [OperationBehavior(TransactionScopeRequired = true)]
        public override void RegisterModule()
        {
            ExecuteFaultHandledOperation(() =>
            {
                ISolutionRepository solutionRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRepository>();
                systemCoreData.IModuleRepository moduleRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IModuleRepository>();
                IMenuRepository menuRepository = _DataRepositoryFactory.GetDataRepository<IMenuRepository>();
                IRoleRepository roleRepository = _DataRepositoryFactory.GetDataRepository<IRoleRepository>();
                IMenuRoleRepository menuRoleRepository = _DataRepositoryFactory.GetDataRepository<IMenuRoleRepository>();

                using (TransactionScope ts = new TransactionScope())
                {
                    //check if module has been installed
                    systemCoreEntities.Module module = moduleRepository.Get().Where(c => c.Name == FeeModuleDefinition.MODULE_NAME).FirstOrDefault();

                    var register = false;
                    if (module == null)
                        register = true;
                    else
                        register = module.CanUpdate;

                    if (register)
                    {
                        //check if module category exit
                        Solution solution = solutionRepository.Get().Where(c => c.Name == FeeModuleDefinition.SOLUTION_NAME).FirstOrDefault();
                        if (solution == null)
                        {
                            //register solution
                            solution = new Solution()
                            {
                                Name = FeeModuleDefinition.SOLUTION_NAME,
                                Alias = FeeModuleDefinition.SOLUTION_ALIAS,
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
                        if (module == null)
                        {
                            module = new systemCoreEntities.Module()
                            {
                                Name = FeeModuleDefinition.MODULE_NAME,
                                Alias = FeeModuleDefinition.MODULE_ALIAS,
                                SolutionId = solution.EntityId,
                                Active = true,
                                Deleted = false,
                                CreatedBy = "Auto",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "Auto",
                                UpdatedOn = DateTime.Now
                            };

                            module = moduleRepository.Add(module);
                        }

                        //Roles
                        var existingRoles = roleRepository.Get().Where(c => c.SolutionId == solution.SolutionId && c.Type == RoleType.Application).ToList();
                       
                        //Menus
                        var existingMenus = menuRepository.Get().Where(c => c.ModuleId == module.ModuleId).ToList();
                        var updatedMenus = new List<Menu>();

                        var menuIndex = 0;

                        foreach (var menu in FeeModuleDefinition.GetMenus())
                        {
                            menuIndex += 1;
                            Menu parentMenu = null;

                            int? parentId = null;
                            if (!string.IsNullOrEmpty(menu.Parent))
                            {
                                if (string.IsNullOrEmpty(menu.ParentModule))
                                {
                                    parentMenu = existingMenus.Where(c => c.Name == menu.Parent).FirstOrDefault();

                                    if (parentMenu == null)
                                        parentMenu = menuRepository.Get().Where(c => c.ModuleId == module.ModuleId && c.Name == menu.Parent).FirstOrDefault();  
                                }
                                else
                                {
                                    var parentModule = moduleRepository.Get().Where(c => c.Name == menu.ParentModule).FirstOrDefault();

                                    if (parentModule != null)
                                        parentMenu = menuRepository.Get().Where(c => c.ModuleId == parentModule.ModuleId && c.Name == menu.Parent).FirstOrDefault(); 
                                }

                                if (parentMenu != null)
                                    parentId = parentMenu.MenuId;
                            }

                            var localMenu = existingMenus.Where(c => c.Name == menu.Name).FirstOrDefault();

                            if (localMenu == null)
                            {
                                localMenu = new Menu() { Name = menu.Name, Alias = menu.Alias, Action = menu.Action, ActionUrl = menu.ActionUrl, ImageUrl = menu.ImageUrl, ModuleId = module.ModuleId, Position = menuIndex, ParentId = parentId, Active = true, Deleted = false, CreatedBy = "Auto", CreatedOn = DateTime.Now, UpdatedBy = "Auto", UpdatedOn = DateTime.Now };

                                localMenu = menuRepository.Add(localMenu);
                            }
                            else
                            {
                                localMenu.Alias = menu.Alias;
                                localMenu.Action = menu.Action;
                                localMenu.ActionUrl = menu.ActionUrl;
                                localMenu.ImageUrl = menu.ImageUrl;
                                localMenu.ModuleId = module.ModuleId;
                                localMenu.Position = menuIndex;
                                localMenu.ParentId = parentId;
                                localMenu.UpdatedOn = DateTime.Now;

                                localMenu = menuRepository.Update(localMenu);
                            }

                            updatedMenus.Add(localMenu);
                        }

                        //MenuRoles
                        var menuIds = updatedMenus.Select(c => c.MenuId).Distinct().ToArray();
                        var existingMenuRoles = menuRoleRepository.Get().Where(c => menuIds.Contains(c.MenuId)).ToList();

                        foreach (var menuRole in FeeModuleDefinition.GetMenuRoles())
                        {
                            var myMenu = updatedMenus.Where(c => c.Name == menuRole.MenuName).FirstOrDefault();
                            var myRole = existingRoles.Where(c => c.Name == menuRole.RoleName).FirstOrDefault();

                            var localMenuRole = existingMenuRoles.Where(c => c.MenuId == myMenu.MenuId && c.RoleId == myRole.RoleId).FirstOrDefault();

                            if (localMenuRole == null)
                            {
                                localMenuRole = new MenuRole() { MenuId = myMenu.MenuId, RoleId = myRole.RoleId, Active = true, Deleted = false, CreatedBy = "Auto", CreatedOn = DateTime.Now, UpdatedBy = "Auto", UpdatedOn = DateTime.Now };

                                menuRoleRepository.Add(localMenuRole);
                            }
                            else
                            {
                                localMenuRole.MenuId = myMenu.MenuId;
                                localMenuRole.RoleId = myRole.RoleId;
                                localMenuRole.UpdatedOn = DateTime.Now;

                                menuRoleRepository.Update(localMenuRole);
                            }


                        }
                    }

                    ts.Complete();
                }

            });

        }

        #region OfficerDetail Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public OfficerDetail UpdateOfficerDetail(OfficerDetail officerDetail)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOfficerDetailRepository officerDetailRepository = _DataRepositoryFactory.GetDataRepository<IOfficerDetailRepository>();

                OfficerDetail updatedEntity = null;

                if (officerDetail.OfficerDetailId == 0)
                    updatedEntity = officerDetailRepository.Add(officerDetail);
                else
                    updatedEntity = officerDetailRepository.Update(officerDetail);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOfficerDetail(int officerDetailId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOfficerDetailRepository officerDetailRepository = _DataRepositoryFactory.GetDataRepository<IOfficerDetailRepository>();

                officerDetailRepository.Remove(officerDetailId);
            });
        }

        public OfficerDetail GetOfficerDetail(int officerDetailId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOfficerDetailRepository officerDetailRepository = _DataRepositoryFactory.GetDataRepository<IOfficerDetailRepository>();

                OfficerDetail officerDetailEntity = officerDetailRepository.Get(officerDetailId);
                if (officerDetailEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OfficerDetail with ID of {0} is not in database", officerDetailId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return officerDetailEntity;
            });
        }

        public OfficerDetail[] GetAllOfficerDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOfficerDetailRepository officerDetailRepository = _DataRepositoryFactory.GetDataRepository<IOfficerDetailRepository>();

                IEnumerable<OfficerDetail> officerDetails = officerDetailRepository.Get().ToArray();

                return officerDetails.ToArray();
            });
        }

        #endregion

        #region TeamSetting Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public TeamSetting UpdateTeamSetting(TeamSetting teamSetting)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamSettingRepository teamSettingRepository = _DataRepositoryFactory.GetDataRepository<ITeamSettingRepository>();

                TeamSetting updatedEntity = null;

                if (teamSetting.TeamSettingId == 0)
                    updatedEntity = teamSettingRepository.Add(teamSetting);
                else
                    updatedEntity = teamSettingRepository.Update(teamSetting);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTeamSetting(int teamSettingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamSettingRepository teamSettingRepository = _DataRepositoryFactory.GetDataRepository<ITeamSettingRepository>();

                teamSettingRepository.Remove(teamSettingId);
            });
        }

        public TeamSetting GetTeamSetting(int teamSettingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamSettingRepository teamSettingRepository = _DataRepositoryFactory.GetDataRepository<ITeamSettingRepository>();

                TeamSetting teamSettingEntity = teamSettingRepository.Get(teamSettingId);
                if (teamSettingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("TeamSetting with ID of {0} is not in database", teamSettingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return teamSettingEntity;
            });
        }

        public TeamSetting[] GetAllTeamSettings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamSettingRepository teamSettingRepository = _DataRepositoryFactory.GetDataRepository<ITeamSettingRepository>();

                IEnumerable<TeamSetting> teamSettings = teamSettingRepository.Get().ToArray();

                return teamSettings.ToArray();
            });
        }

        #endregion

        #region TeamUser Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public TeamUser UpdateTeamUser(TeamUser teamUser)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamUserRepository teamUserRepository = _DataRepositoryFactory.GetDataRepository<ITeamUserRepository>();

                TeamUser updatedEntity = null;

                if (teamUser.TeamUserId == 0)
                    updatedEntity = teamUserRepository.Add(teamUser);
                else
                    updatedEntity = teamUserRepository.Update(teamUser);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTeamUser(int teamUserId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamUserRepository teamUserRepository = _DataRepositoryFactory.GetDataRepository<ITeamUserRepository>();

                teamUserRepository.Remove(teamUserId);
            });
        }

        public TeamUser GetTeamUser(int teamUserId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamUserRepository teamUserRepository = _DataRepositoryFactory.GetDataRepository<ITeamUserRepository>();

                TeamUser teamUserEntity = teamUserRepository.Get(teamUserId);
                if (teamUserEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("TeamUser with ID of {0} is not in database", teamUserId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return teamUserEntity;
            });
        }


        public TeamUser GetTeamUserByLoginID(string loginID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamUserRepository teamUserRepository = _DataRepositoryFactory.GetDataRepository<ITeamUserRepository>();

                TeamUser teamUsers = teamUserRepository.Get().Where(c => c.LoginID == loginID).FirstOrDefault();

                return teamUsers;
            });
        }

        public TeamUser[] GetAllTeamUsers()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamUserRepository teamUserRepository = _DataRepositoryFactory.GetDataRepository<ITeamUserRepository>();

                IEnumerable<TeamUser> teamUsers = teamUserRepository.Get().ToArray();

                return teamUsers.ToArray();
            });
        }

        #endregion

        #region TeamDefinition Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public TeamDefinition UpdateTeamDefinition(TeamDefinition teamDefinition)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

                TeamDefinition updatedEntity = null;

                if (teamDefinition.TeamDefinitionId == 0)
                    updatedEntity = teamDefinitionRepository.Add(teamDefinition);
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
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

                teamDefinitionRepository.Remove(teamDefinitionId);
            });
        }

        public TeamDefinition GetTeamDefinition(int teamDefinitionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

                TeamDefinition teamDefinitionEntity = teamDefinitionRepository.Get(teamDefinitionId);
                if (teamDefinitionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("TeamDefinition with ID of {0} is not in database", teamDefinitionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return teamDefinitionEntity;
            });
        }

        public TeamDefinition[] GetAllTeamDefinitions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

                IEnumerable<TeamDefinition> teamDefinition = teamDefinitionRepository.Get().ToArray();

                return teamDefinition.ToArray();
            });
        }

        public TeamDefinition[] GetTeamDefinitionByCode(string code)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

                IEnumerable<TeamDefinition> teamDefinitions = teamDefinitionRepository.Get().Where(c => c.Code == code).ToArray();

                return teamDefinitions.ToArray();
            });
        }
        #endregion

        #region TeamClassificationType Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public TeamClassificationType UpdateTeamClassificationType(TeamClassificationType teamClassificationType)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();

                TeamClassificationType updatedEntity = null;

                if (teamClassificationType.TeamClassificationTypeId == 0)
                    updatedEntity = teamClassificationTypeRepository.Add(teamClassificationType);
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
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();

                teamClassificationTypeRepository.Remove(teamClassificationTypeId);
            });
        }

        public TeamClassificationType GetTeamClassificationType(int teamClassificationTypeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

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
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();

                IEnumerable<TeamClassificationType> teamClassificationType = teamClassificationTypeRepository.Get().ToArray();

                return teamClassificationType.ToArray();
            });
        }

        #endregion

        #region TeamClassification Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public TeamClassification UpdateTeamClassification(TeamClassification teamClassification)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

                TeamClassification updatedEntity = null;

                if (teamClassification.TeamClassificationId == 0)
                    updatedEntity = teamClassificationRepository.Add(teamClassification);
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
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

                teamClassificationRepository.Remove(teamClassificationId);
            });
        }

        public TeamClassification GetTeamClassification(int teamClassificationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

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
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

                IEnumerable<TeamClassification> teamClassification = teamClassificationRepository.Get().ToArray();

                return teamClassification.ToArray();
            });
        }

        #endregion

        #region Team Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Team UpdateTeam(Team team)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

                Team updatedEntity = null;

                if (team.TeamId == 0)
                    updatedEntity = teamRepository.Add(team);
                else
                    updatedEntity = teamRepository.Update(team);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTeam(int teamId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

                teamRepository.Remove(teamId);
            });
        }

        public Team GetTeam(int teamId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

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

        public Team[] GetTeamByDefinition(string definitionCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

                IEnumerable<Team> teamEntity = teamRepository.Get().Where(c => c.DefinitionCode == definitionCode).ToArray();
                          
                return teamEntity.ToArray();
            });
        }

        public Team[] GetParentTeams(string definitionCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

                IEnumerable<Team> teamEntity = teamRepository.Get().Where(c => c.ParentCode == definitionCode).ToArray();

                return teamEntity.ToArray();
            });
        }

        public Team[] GetAllTeams()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

                IEnumerable<Team> teams = teamRepository.Get().ToArray();

                return teams.ToArray();
            });
        }

        public Team[] GetTeamByLevel(int level)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

                IEnumerable<Team> teams = teamRepository.Get().Where(c => c.TeamId == level).ToArray();

                return teams.ToArray();
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
