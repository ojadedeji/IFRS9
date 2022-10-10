using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Data.Entity;
using System.Transactions;
using Fintrak.Shared.SystemCore.Entities;
using coreEntities = Fintrak.Shared.SystemCore.Entities;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common;
using systemCore = Fintrak.Data.SystemCore;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.SystemCore.Framework;
using Fintrak.Business.SystemCore.Contracts;
using Fintrak.Data.SystemCore.Contracts;
using System.Data.SqlClient;
using Fintrak.Data.SystemCore;
using Fintrak.Shared.Common.Data;
using Fintrak.Data.SystemCore;


namespace Fintrak.Business.SystemCore.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class CoreManager : ManagerBase, ICoreService
    {
        public CoreManager()
        {
        }

        public CoreManager(IDataRepositoryFactory dataRepositoryFactory)
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
                IModuleRepository moduleRepository = _DataRepositoryFactory.GetDataRepository<IModuleRepository>();
                IMenuRepository menuRepository = _DataRepositoryFactory.GetDataRepository<IMenuRepository>();
                IRoleRepository roleRepository = _DataRepositoryFactory.GetDataRepository<IRoleRepository>();
                IMenuRoleRepository menuRoleRepository = _DataRepositoryFactory.GetDataRepository<IMenuRoleRepository>();

                using (TransactionScope ts = new TransactionScope())
                {
                    //check if module has been installed
                    Module module = moduleRepository.Get().Where(c => c.Name == SystemCoreModuleDefinition.MODULE_NAME).FirstOrDefault();

                    var register = false;
                    if (module == null)
                        register = true;
                    else
                        register = module.CanUpdate;

                    if (register)
                    {
                        //check if module category exit
                        Solution solution = solutionRepository.Get().Where(c => c.Name == SystemCoreModuleDefinition.SOLUTION_NAME).FirstOrDefault();
                        if (solution == null)
                        {
                            //register solution
                            solution = new Solution()
                            {
                                Name = SystemCoreModuleDefinition.SOLUTION_NAME,
                                Alias = SystemCoreModuleDefinition.SOLUTION_ALIAS,
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
                            module = new Module()
                            {
                                Name = SystemCoreModuleDefinition.MODULE_NAME,
                                Alias = SystemCoreModuleDefinition.MODULE_ALIAS,
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
                        var updatedRoles = new List<Role>();

                        foreach (var role in SystemCoreModuleDefinition.GetRoles())
                        {
                            var localRole = existingRoles.Where(c => c.Name == role.Name).FirstOrDefault();

                            if (localRole == null)
                            {
                                localRole = new Role() { Name = role.Name, Description = role.Description, SolutionId = solution.SolutionId, Type = RoleType.Application, Active = true, Deleted = false, CreatedBy = "Auto", CreatedOn = DateTime.Now, UpdatedBy = "Auto", UpdatedOn = DateTime.Now };

                                localRole = roleRepository.Add(localRole);
                            }
                            else
                            {
                                localRole.Description = role.Description;
                                localRole.UpdatedOn = DateTime.Now;
                                localRole = roleRepository.Update(localRole);
                            }



                            updatedRoles.Add(localRole);
                        }

                        //Menus
                        var existingMenus = menuRepository.Get().Where(c => c.ModuleId == module.ModuleId).ToList();
                        var updatedMenus = new List<Menu>();

                        var menuIndex = 0;

                        foreach (var menu in SystemCoreModuleDefinition.GetMenus())
                        {
                            menuIndex += 1;

                            int? parentId = null;
                            if (!string.IsNullOrEmpty(menu.Parent))
                            {
                                var parentMenu = existingMenus.Where(c => c.Name == menu.Parent).FirstOrDefault();

                                if (parentMenu == null)
                                    parentMenu = menuRepository.Get().Where(c => c.ModuleId == module.ModuleId && c.Name == menu.Parent).FirstOrDefault();

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

                        foreach (var menuRole in SystemCoreModuleDefinition.GetMenuRoles())
                        {
                            var myMenu = updatedMenus.Where(c => c.Name == menuRole.MenuName).FirstOrDefault();
                            var myRole = updatedRoles.Where(c => c.Name == menuRole.RoleName).FirstOrDefault();

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

        #region Solution operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Solution UpdateSolution(Solution solution)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ISolutionRepository solutionRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRepository>();

                Solution updatedEntity = null;

                if (solution.SolutionId == 0)
                    updatedEntity = solutionRepository.Add(solution);
                else
                    updatedEntity = solutionRepository.Update(solution);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSolution(int solutionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ISolutionRepository solutionRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRepository>();

                solutionRepository.Remove(solutionId);
            });
        }

        public Solution GetSolution(int solutionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ISolutionRepository solutionRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRepository>();

                Solution solutionEntity = solutionRepository.Get(solutionId);
                if (solutionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Solution with ID of {0} is not in database", solutionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return solutionEntity;
            });
        }

        public Solution[] GetAllSolutions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ISolutionRepository solutionRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRepository>();

                IEnumerable<Solution> solutions = solutionRepository.Get().Where(c => c.Active);

                return solutions.ToArray();
            });
        }

        #endregion

        #region Module operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Module UpdateModule(Module module)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IModuleRepository moduleRepository = _DataRepositoryFactory.GetDataRepository<IModuleRepository>();

                Module updatedEntity = null;

                if (module.ModuleId == 0)
                    updatedEntity = moduleRepository.Add(module);
                else
                    updatedEntity = moduleRepository.Update(module);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteModule(int moduleId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IModuleRepository moduleRepository = _DataRepositoryFactory.GetDataRepository<IModuleRepository>();

                moduleRepository.Remove(moduleId);
            });
        }

        public Module GetModule(int moduleId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IModuleRepository moduleRepository = _DataRepositoryFactory.GetDataRepository<IModuleRepository>();

                Module moduleEntity = moduleRepository.Get(moduleId);
                if (moduleEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Module with ID of {0} is not in database", moduleId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return moduleEntity;
            });
        }

        public Module[] GetAllModules()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IModuleRepository moduleRepository = _DataRepositoryFactory.GetDataRepository<IModuleRepository>();

                IEnumerable<Module> modules = moduleRepository.Get().Where(c => c.Active);

                return modules.ToArray();
            });
        }

        public ModuleData[] GetModules()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IModuleRepository moduleRepository = _DataRepositoryFactory.GetDataRepository<IModuleRepository>();

                List<ModuleData> modules = new List<ModuleData>();
                IEnumerable<ModuleInfo> moduleInfos = moduleRepository.GetModules().Where(c => c.Module.Active).OrderBy(c => c.Solution.Name).ToArray();

                foreach (var moduleInfo in moduleInfos)
                {
                    modules.Add(
                        new ModuleData
                        {
                            ModuleId = moduleInfo.Module.EntityId,
                            Name = moduleInfo.Module.Name,
                            Alias = moduleInfo.Module.Alias,
                            SolutionId = moduleInfo.Solution.EntityId,
                            SolutionName = moduleInfo.Solution.Alias,
                            CanUpdate = moduleInfo.Module.CanUpdate,
                            Active = moduleInfo.Module.Active
                        });
                }

                return modules.ToArray();
            });
        }

        #endregion

        #region Role operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Role UpdateRole(Role role)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IRoleRepository roleRepository = _DataRepositoryFactory.GetDataRepository<IRoleRepository>();

                Role updatedEntity = null;

                if (role.RoleId == 0)
                    updatedEntity = roleRepository.Add(role);
                else
                    updatedEntity = roleRepository.Update(role);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteRole(int roleId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IRoleRepository roleRepository = _DataRepositoryFactory.GetDataRepository<IRoleRepository>();

                roleRepository.Remove(roleId);
            });
        }

        public Role GetRole(int roleId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IRoleRepository roleRepository = _DataRepositoryFactory.GetDataRepository<IRoleRepository>();

                Role roleEntity = roleRepository.Get(roleId);
                if (roleEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Role with ID of {0} is not in database", roleId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return roleEntity;
            });
        }

        public RoleData[] GetAllRoles()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IRoleRepository roleRepository = _DataRepositoryFactory.GetDataRepository<IRoleRepository>();

                List<RoleData> roles = new List<RoleData>();
                IEnumerable<RoleInfo> roleInfos = roleRepository.GetRoles().Where(c => c.Solution.Active).OrderBy(c => c.Solution.Name).ToArray();

                foreach (var roleInfo in roleInfos)
                {
                    roles.Add(
                        new RoleData
                        {
                            RoleId = roleInfo.Role.EntityId,
                            Name = roleInfo.Role.Name,
                            Description = roleInfo.Role.Description,
                            Type = roleInfo.Role.Type,
                            TypeName = roleInfo.Role.Type.ToString(),
                            SolutionId = roleInfo.Solution.EntityId,
                            SolutionName = roleInfo.Solution.Alias,
                            Active = roleInfo.Role.Active,
                            LongDescription = string.Format("{0}-{1}", roleInfo.Role.Name, roleInfo.Solution.Alias)
                        });
                }

                return roles.ToArray();
            });
        }

        #endregion

        #region UserSetup operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public UserSetup UpdateUserSetup(UserSetup userSetup)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IUserSetupRepository userSetupRepository = _DataRepositoryFactory.GetDataRepository<IUserSetupRepository>();

                UserSetup updatedEntity = null;

                if (userSetup.UserSetupId == 0)
                    updatedEntity = userSetupRepository.Add(userSetup);
                else
                    updatedEntity = userSetupRepository.Update(userSetup);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public UserSetup UpdateUserSetupProfile(UserSetup userSetup)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IUserSetupRepository userSetupRepository = _DataRepositoryFactory.GetDataRepository<IUserSetupRepository>();

                UserSetup updatedEntity = null;

                updatedEntity = userSetupRepository.Update(userSetup);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteUserSetup(int userSetupId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IUserSetupRepository userSetupRepository = _DataRepositoryFactory.GetDataRepository<IUserSetupRepository>();

                userSetupRepository.Remove(userSetupId);
            });
        }

        public UserSetup GetUserSetup(int userSetupId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IUserSetupRepository userSetupRepository = _DataRepositoryFactory.GetDataRepository<IUserSetupRepository>();

                UserSetup userSetupEntity = userSetupRepository.Get(userSetupId);
                if (userSetupEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("UserSetup with ID of {0} is not in database", userSetupId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return userSetupEntity;
            });
        }

        public UserSetup GetUserSetupByLoginID(string loginID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                //var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                //AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IUserSetupRepository userSetupRepository = _DataRepositoryFactory.GetDataRepository<IUserSetupRepository>();

                UserSetup userSetups = userSetupRepository.Get().Where(c => c.LoginID == loginID).FirstOrDefault();

                return userSetups;
            });
        }

        public UserSetup[] GetAllUserSetups()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                //var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR };
                //AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IUserSetupRepository userSetupRepository = _DataRepositoryFactory.GetDataRepository<IUserSetupRepository>();

                IEnumerable<UserSetup> userSetups = userSetupRepository.Get();

                return userSetups.ToArray();
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void ConfirmDefaultUser()
        {
            ExecuteFaultHandledOperation(() =>
            {
                IUserSetupRepository userSetupRepository = _DataRepositoryFactory.GetDataRepository<IUserSetupRepository>();
                ISolutionRepository solutionRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRepository>();
                IRoleRepository roleRepository = _DataRepositoryFactory.GetDataRepository<IRoleRepository>();
                IUserRoleRepository userRoleRepository = _DataRepositoryFactory.GetDataRepository<IUserRoleRepository>();

                UserSetup userSetupEntity = userSetupRepository.GetByLoginID("fintrak");
                if (userSetupEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("UserSetup with login {0} is not in database", "fintrak"));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                UserSetup fintrakBusiness = userSetupRepository.GetByLoginID("fintrakbusiness");
                if (fintrakBusiness == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("UserSetup with login {0} is not in database", "fintrakbusiness"));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                UserSetup fintrakUser = userSetupRepository.GetByLoginID("fintrakuser");
                if (fintrakUser == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("UserSetup with login {0} is not in database", "fintrakuser"));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                Solution solution = solutionRepository.Get().Where(c => c.Name == "CORE").FirstOrDefault();
                if (solution == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Solution with name CORE is not in database"));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                Role role = roleRepository.Get().Where(c => c.Name == "Administrator" && c.SolutionId == solution.EntityId).FirstOrDefault();
                if (role == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Role for default user is not in database"));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                Role userRole2 = roleRepository.Get().Where(c => c.Name == "User" && c.SolutionId == solution.EntityId).FirstOrDefault();
                if (userRole2 == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Role for default user is not in database"));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                Role userRole3 = roleRepository.Get().Where(c => c.Name == "Business" && c.SolutionId == solution.EntityId).FirstOrDefault();
                if (userRole3 == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Role for default user is not in database"));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                var userRole = userRoleRepository.Get().Where(c => c.RoleId == role.RoleId && c.UserSetupId == userSetupEntity.UserSetupId).FirstOrDefault();

                //create role for manager
                if (userRole == null)
                {
                    userRole = new UserRole()
                    {
                        RoleId = role.RoleId,
                        UserSetupId = userSetupEntity.UserSetupId,
                        Active = true,
                        Deleted = false
                    };

                    userRoleRepository.Add(userRole);
                }

                userRole = userRoleRepository.Get().Where(c => c.RoleId == userRole2.RoleId && c.UserSetupId == fintrakUser.UserSetupId).FirstOrDefault();

                //create role for user
                if (userRole == null)
                {
                    userRole = new UserRole()
                    {
                        RoleId = userRole2.RoleId,
                        UserSetupId = fintrakUser.UserSetupId,
                        Active = true,
                        Deleted = false
                    };

                    userRoleRepository.Add(userRole);
                }

                userRole = userRoleRepository.Get().Where(c => c.RoleId == userRole3.RoleId && c.UserSetupId == fintrakBusiness.UserSetupId).FirstOrDefault();

                //create role for business
                if (userRole == null)
                {
                    userRole = new UserRole()
                    {
                        RoleId = userRole3.RoleId,
                        UserSetupId = fintrakBusiness.UserSetupId,
                        Active = true,
                        Deleted = false
                    };

                    userRoleRepository.Add(userRole);
                }

            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void AssignDefaultRole(UserSetup account)
        {
            ExecuteFaultHandledOperation(() =>
            {
                ISolutionRepository solutionRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRepository>();
                IRoleRepository roleRepository = _DataRepositoryFactory.GetDataRepository<IRoleRepository>();
                IUserRoleRepository userRoleRepository = _DataRepositoryFactory.GetDataRepository<IUserRoleRepository>();

                Solution solution = solutionRepository.Get().Where(c => c.Name == "CORE").FirstOrDefault();
                if (solution == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Solution with name CORE is not in database"));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                Role role = roleRepository.Get().Where(c => c.Name == "User" && c.SolutionId == solution.EntityId).FirstOrDefault();
                if (role == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Role for default user is not in database"));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                var userRole = userRoleRepository.Get().Where(c => c.RoleId == role.RoleId && c.UserSetupId == account.UserSetupId).FirstOrDefault();

                //create role for user
                if (userRole == null)
                {
                    userRole = new UserRole()
                    {
                        RoleId = role.RoleId,
                        UserSetupId = account.UserSetupId,
                        Active = true,
                        Deleted = false
                    };

                    userRoleRepository.Add(userRole);
                }
            });
        }

        #endregion

        #region UserRole operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public UserRole UpdateUserRole(UserRole userRole)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IUserRoleRepository userRoleRepository = _DataRepositoryFactory.GetDataRepository<IUserRoleRepository>();

                UserRole updatedEntity = null;

                if (userRole.UserRoleId == 0)
                    updatedEntity = userRoleRepository.Add(userRole);
                else
                    updatedEntity = userRoleRepository.Update(userRole);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteUserRole(int userRoleId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IUserRoleRepository userRoleRepository = _DataRepositoryFactory.GetDataRepository<IUserRoleRepository>();

                userRoleRepository.Remove(userRoleId);
            });
        }

        public UserRole GetUserRole(int userRoleId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IUserRoleRepository userRoleRepository = _DataRepositoryFactory.GetDataRepository<IUserRoleRepository>();

                UserRole userRoleEntity = userRoleRepository.Get(userRoleId);
                if (userRoleEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("UserRole with ID of {0} is not in database", userRoleId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return userRoleEntity;
            });
        }

        public UserRole[] GetAllUserRoles()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IUserRoleRepository userRoleRepository = _DataRepositoryFactory.GetDataRepository<IUserRoleRepository>();

                IEnumerable<UserRole> userRoles = userRoleRepository.Get();

                return userRoles.ToArray();
            });
        }

        public UserRoleData[] GetUserRoleByLoginID(string loginID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IUserRoleRepository userRoleRepository = _DataRepositoryFactory.GetDataRepository<IUserRoleRepository>();

                List<UserRoleData> userRoles = new List<UserRoleData>();
                IEnumerable<UserRoleInfo> userRoleInfos = userRoleRepository.GetUserRoleInfo().Where(c => c.UserSetup.LoginID == loginID && c.Role.Active).ToArray();

                foreach (var userRoleInfo in userRoleInfos)
                {
                    userRoles.Add(
                        new UserRoleData
                        {
                            UserRoleId = userRoleInfo.UserRole.EntityId,
                            UserSetupId = userRoleInfo.UserSetup.EntityId,
                            LoginID = userRoleInfo.UserSetup.LoginID,
                            RoleId = userRoleInfo.Role.EntityId,
                            RoleName = userRoleInfo.Role.Name,
                            Type = userRoleInfo.Role.Type,
                            SolutionId = userRoleInfo.Solution.EntityId,
                            SolutionName = userRoleInfo.Solution.Alias,
                            Active = userRoleInfo.UserRole.Active
                        });
                }

                return userRoles.ToArray();
            });
        }

        public UserRoleData[] GetUserRoles()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IUserRoleRepository userRoleRepository = _DataRepositoryFactory.GetDataRepository<IUserRoleRepository>();

                List<UserRoleData> userRoles = new List<UserRoleData>();
                IEnumerable<UserRoleInfo> userRoleInfos = userRoleRepository.GetUserRoleInfo().Where(c => c.Role.Active).ToArray();

                foreach (var userRoleInfo in userRoleInfos)
                {
                    userRoles.Add(
                        new UserRoleData
                        {
                            UserRoleId = userRoleInfo.UserRole.EntityId,
                            UserSetupId = userRoleInfo.UserSetup.EntityId,
                            LoginID = userRoleInfo.UserSetup.LoginID,
                            RoleId = userRoleInfo.Role.EntityId,
                            RoleName = userRoleInfo.Role.Name,
                            Type = userRoleInfo.Role.Type,
                            SolutionId = userRoleInfo.Solution.EntityId,
                            SolutionName = userRoleInfo.Solution.Alias,
                            Active = userRoleInfo.UserRole.Active
                        });
                }

                return userRoles.ToArray();
            });
        }

        #endregion

        #region Menu operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Menu UpdateMenu(Menu menu)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IMenuRepository menuRepository = _DataRepositoryFactory.GetDataRepository<IMenuRepository>();

                Menu updatedEntity = null;

                if (menu.MenuId == 0)
                    updatedEntity = menuRepository.Add(menu);
                else
                    updatedEntity = menuRepository.Update(menu);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMenu(int menuId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IMenuRepository menuRepository = _DataRepositoryFactory.GetDataRepository<IMenuRepository>();

                menuRepository.Remove(menuId);
            });
        }

        public Menu GetMenu(int menuId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IMenuRepository menuRepository = _DataRepositoryFactory.GetDataRepository<IMenuRepository>();

                Menu menuEntity = menuRepository.Get(menuId);
                if (menuEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Menu with ID of {0} is not in database", menuId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return menuEntity;
            });
        }

        public Menu[] GetAllMenus()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IMenuRepository menuRepository = _DataRepositoryFactory.GetDataRepository<IMenuRepository>();

                IEnumerable<Menu> menus = menuRepository.Get().Where(c => c.Active).OrderBy(c => c.ModuleId).OrderBy(c => c.Position);

                return menus.ToArray();
            });
        }

        public MenuData[] GetMenuByLoginID(string loginID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IMenuRepository menuRepository = _DataRepositoryFactory.GetDataRepository<IMenuRepository>();


                List<MenuData> menus = new List<MenuData>();
                IEnumerable<MenuInfo> menuInfos = menuRepository.GetMenuInfoByLoginID(loginID).Where(c => c.Menu.Active).OrderBy(c => c.Menu.ModuleId).OrderBy(c => c.Menu.Position).ToArray();

                foreach (var menuInfo in menuInfos)
                {
                    menus.Add(
                        new MenuData
                        {
                            MenuId = menuInfo.Menu.EntityId,
                            Name = menuInfo.Menu.Name,
                            Alias = menuInfo.Menu.Alias,
                            Action = menuInfo.Menu.Action,
                            ActionUrl = menuInfo.Menu.ActionUrl,
                            Image = menuInfo.Menu.Image,
                            ImageUrl = menuInfo.Menu.ImageUrl,
                            ModuleId = menuInfo.Module.EntityId,
                            ModuleName = menuInfo.Module.Alias,
                            ParentId = menuInfo.Parent != null ? menuInfo.Parent.EntityId : 0,
                            ParentName = menuInfo.Parent != null ? menuInfo.Parent.Alias : string.Empty,
                            Position = menuInfo.Menu.Position,
                            Active = menuInfo.Menu.Active
                        });
                }

                return menus.ToArray();
            });
        }

        public MenuData[] GetMenus()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IMenuRepository menuRepository = _DataRepositoryFactory.GetDataRepository<IMenuRepository>();

                List<MenuData> menus = new List<MenuData>();
                IEnumerable<MenuInfo> menuInfos = menuRepository.GetMenuInfo().Where(c => c.Menu.Active).OrderBy(c => c.Menu.ModuleId).OrderBy(c => c.Menu.Position).ToArray();

                foreach (var menuInfo in menuInfos)
                {
                    menus.Add(
                        new MenuData
                        {
                            MenuId = menuInfo.Menu.EntityId,
                            Name = menuInfo.Menu.Name,
                            Alias = menuInfo.Menu.Alias,
                            Action = menuInfo.Menu.Action,
                            ActionUrl = menuInfo.Menu.ActionUrl,
                            Image = menuInfo.Menu.Image,
                            ImageUrl = menuInfo.Menu.ImageUrl,
                            ModuleId = menuInfo.Module.EntityId,
                            ModuleName = menuInfo.Module.Alias,
                            ParentId = menuInfo.Parent != null ? menuInfo.Parent.EntityId : 0,
                            ParentName = menuInfo.Parent != null ? menuInfo.Parent.Alias : string.Empty,
                            Position = menuInfo.Menu.Position,
                            Active = menuInfo.Menu.Active
                        });
                }

                return menus.ToArray();
            });
        }

        #endregion

        #region MenuRole operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MenuRole UpdateMenuRole(MenuRole menuRole)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IMenuRoleRepository menuRoleRepository = _DataRepositoryFactory.GetDataRepository<IMenuRoleRepository>();

                MenuRole updatedEntity = null;

                if (menuRole.MenuRoleId == 0)
                    updatedEntity = menuRoleRepository.Add(menuRole);
                else
                    updatedEntity = menuRoleRepository.Update(menuRole);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMenuRole(int menuRoleId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IMenuRoleRepository menuRoleRepository = _DataRepositoryFactory.GetDataRepository<IMenuRoleRepository>();

                menuRoleRepository.Remove(menuRoleId);
            });
        }

        public MenuRole GetMenuRole(int menuRoleId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IMenuRoleRepository menuRoleRepository = _DataRepositoryFactory.GetDataRepository<IMenuRoleRepository>();

                MenuRole menuRoleEntity = menuRoleRepository.Get(menuRoleId);
                if (menuRoleEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MenuRole with ID of {0} is not in database", menuRoleId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return menuRoleEntity;
            });
        }

        public MenuRole[] GetAllMenuRoles()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IMenuRoleRepository menuRoleRepository = _DataRepositoryFactory.GetDataRepository<IMenuRoleRepository>();

                IEnumerable<MenuRole> menuRoles = menuRoleRepository.Get();

                return menuRoles.ToArray();
            });
        }

        public MenuRoleData[] GetMenuRoleByLoginID(string loginID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IMenuRoleRepository menuRoleRepository = _DataRepositoryFactory.GetDataRepository<IMenuRoleRepository>();

                List<MenuRoleData> menuRoles = new List<MenuRoleData>();
                IEnumerable<MenuRoleInfo> menuRoleInfos = menuRoleRepository.GetMenuRoleInfo(loginID).Where(c => c.Solution.Active).OrderBy(c => c.Menu.ModuleId).OrderBy(c => c.Menu.Position).ToArray();

                foreach (var menuRoleInfo in menuRoleInfos)
                {
                    menuRoles.Add(
                        new MenuRoleData
                        {
                            MenuRoleId = menuRoleInfo.MenuRole.EntityId,
                            MenuId = menuRoleInfo.Menu.EntityId,
                            MenuName = menuRoleInfo.Menu.Alias,
                            RoleId = menuRoleInfo.Role.EntityId,
                            RoleName = menuRoleInfo.Role.Name,
                            SolutionId = menuRoleInfo.Solution.EntityId,
                            SolutionName = menuRoleInfo.Solution.Alias,
                            Active = menuRoleInfo.MenuRole.Active
                        });
                }

                return menuRoles.ToArray();
            });
        }

        public MenuRoleData[] GetMenuRoles()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IMenuRoleRepository menuRoleRepository = _DataRepositoryFactory.GetDataRepository<IMenuRoleRepository>();

                List<MenuRoleData> menuRoles = new List<MenuRoleData>();
                IEnumerable<MenuRoleInfo> menuRoleInfos = menuRoleRepository.GetMenuRoleInfo().Where(c => c.Solution.Active).ToArray();

                foreach (var menuRoleInfo in menuRoleInfos)
                {
                    menuRoles.Add(
                        new MenuRoleData
                        {
                            MenuRoleId = menuRoleInfo.MenuRole.EntityId,
                            MenuId = menuRoleInfo.Menu.EntityId,
                            MenuName = menuRoleInfo.Menu.Alias,
                            RoleId = menuRoleInfo.Role.EntityId,
                            RoleName = menuRoleInfo.Role.Name,
                            SolutionId = menuRoleInfo.Solution.EntityId,
                            SolutionName = menuRoleInfo.Solution.Alias,
                            Active = menuRoleInfo.MenuRole.Active
                        });
                }

                return menuRoles.ToArray();
            });
        }

        #endregion

        #region AuditTrail operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public AuditTrail UpdateAuditTrail(AuditTrail auditTrail)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation("CORE", groupNames);

                IAuditTrailRepository auditTrailRepository = _DataRepositoryFactory.GetDataRepository<IAuditTrailRepository>();

                AuditTrail updatedEntity = null;

                if (auditTrail.AuditTrailId == 0)
                    updatedEntity = auditTrailRepository.Add(auditTrail);
                else
                    updatedEntity = auditTrailRepository.Update(auditTrail);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteAuditTrail(int auditTrailId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IAuditTrailRepository auditTrailRepository = _DataRepositoryFactory.GetDataRepository<IAuditTrailRepository>();

                auditTrailRepository.Remove(auditTrailId);
            });
        }

        public AuditTrail GetAuditTrail(int auditTrailId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IAuditTrailRepository auditTrailRepository = _DataRepositoryFactory.GetDataRepository<IAuditTrailRepository>();

                AuditTrail auditTrailEntity = auditTrailRepository.Get(auditTrailId);
                if (auditTrailEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("AuditTrail with ID of {0} is not in database", auditTrailId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return auditTrailEntity;
            });
        }

        public AuditTrail[] GetAllAuditTrails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IAuditTrailRepository auditTrailRepository = _DataRepositoryFactory.GetDataRepository<IAuditTrailRepository>();

                IEnumerable<AuditTrail> auditTrails = auditTrailRepository.Get();

                return auditTrails.ToArray();
            });
        }

        public AuditTrail[] GetAuditTrails(DateTime fromDate, DateTime toDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IAuditTrailRepository auditTrailRepository = _DataRepositoryFactory.GetDataRepository<IAuditTrailRepository>();

                IEnumerable<AuditTrail> auditTrails = auditTrailRepository.GetByDate(fromDate, toDate);

                return auditTrails.ToArray();
            });
        }

        public AuditTrail[] GetAuditTrailByTable(string tableName, DateTime fromDate, DateTime toDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IAuditTrailRepository auditTrailRepository = _DataRepositoryFactory.GetDataRepository<IAuditTrailRepository>();

                IEnumerable<AuditTrail> auditTrails = auditTrailRepository.GetByTable(tableName, fromDate, toDate);

                return auditTrails.ToArray();
            });
        }

        public AuditTrail[] GetAuditTrailByUser(string userName, DateTime fromDate, DateTime toDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IAuditTrailRepository auditTrailRepository = _DataRepositoryFactory.GetDataRepository<IAuditTrailRepository>();

                IEnumerable<AuditTrail> auditTrails = auditTrailRepository.GetByLoginID(userName, fromDate, toDate);

                return auditTrails.ToArray();
            });
        }

        public AuditTrail[] GetAuditTrailByAction(string action, DateTime fromDate, DateTime toDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IAuditTrailRepository auditTrailRepository = _DataRepositoryFactory.GetDataRepository<IAuditTrailRepository>();

                //IEnumerable<AuditTrail> auditTrails = auditTrailRepository.GetByAction(action, fromDate, toDate);
                List<AuditTrail> auditTrails = auditTrailRepository.GetByAction(action, fromDate, toDate);

                return auditTrails.ToArray();
            });
        }


        public AuditTrail[] GetAuditTrailByTab(AuditAction action)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IAuditTrailRepository auditTrailRepository = _DataRepositoryFactory.GetDataRepository<IAuditTrailRepository>();

                IEnumerable<AuditTrail> auditTrails = auditTrailRepository.GetAuditTrailByTab(action);

                return auditTrails.ToArray();
            });
        }

        #endregion

        #region Company operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Company UpdateCompany(Company company)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ICompanyRepository companyRepository = _DataRepositoryFactory.GetDataRepository<ICompanyRepository>();

                Company updatedEntity = null;

                if (company.CompanyId == 0)
                    updatedEntity = companyRepository.Add(company);
                else
                    updatedEntity = companyRepository.Update(company);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCompany(int companyId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ICompanyRepository companyRepository = _DataRepositoryFactory.GetDataRepository<ICompanyRepository>();

                companyRepository.Remove(companyId);
            });
        }

        public Company GetCompany(int companyId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ICompanyRepository companyRepository = _DataRepositoryFactory.GetDataRepository<ICompanyRepository>();

                Company companyEntity = companyRepository.Get(companyId);
                if (companyEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Company with ID of {0} is not in database", companyId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return companyEntity;
            });
        }

        public Company[] GetAllCompanies()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ICompanyRepository companyRepository = _DataRepositoryFactory.GetDataRepository<ICompanyRepository>();

                IEnumerable<Company> companies = companyRepository.Get();

                return companies.ToArray();
            });
        }

        #endregion

        #region General operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public General UpdateGeneral(General general)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IGeneralRepository generalRepository = _DataRepositoryFactory.GetDataRepository<IGeneralRepository>();

                General updatedEntity = null;

                if (general.GeneralId == 0)
                    updatedEntity = generalRepository.Add(general);
                else
                    updatedEntity = generalRepository.Update(general);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteGeneral(int generalId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IGeneralRepository generalRepository = _DataRepositoryFactory.GetDataRepository<IGeneralRepository>();

                generalRepository.Remove(generalId);
            });
        }

        public General GetGeneral(int generalId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IGeneralRepository generalRepository = _DataRepositoryFactory.GetDataRepository<IGeneralRepository>();

                General generalEntity = generalRepository.Get(generalId);
                if (generalEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("General with ID of {0} is not in database", generalId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return generalEntity;
            });
        }

        public General[] GetAllGenerals()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IGeneralRepository generalRepository = _DataRepositoryFactory.GetDataRepository<IGeneralRepository>();

                IEnumerable<General> generals = generalRepository.Get();

                return generals.ToArray();
            });
        }

        public General GetFirstGeneral()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ////var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR };
                ////AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IGeneralRepository generalRepository = _DataRepositoryFactory.GetDataRepository<IGeneralRepository>();

                General general = generalRepository.Get().FirstOrDefault();

                return general;
            });
        }

        #endregion

        #region Log Event

        public LogEvent[] GetLogByDateRangeAndType(DateTime start, DateTime end, string logLevel)
        {
            return LogDataManager.GetELMAHErrorLogByDateRangeAndType(start, end, logLevel).ToArray();
        }

        public LogEvent GetLogById(string id)
        {
            return LogDataManager.GetELMAHErrorLogById(id);
        }

        public void ClearLog(DateTime start, DateTime end, string[] logLevels)
        {
            LogDataManager.ClearELMAHErrorLog(start, end, logLevels);
        }

        #endregion

        #region Client operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public coreEntities.Client UpdateClient(coreEntities.Client client)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IClientRepository clientRepository = _DataRepositoryFactory.GetDataRepository<IClientRepository>();

                coreEntities.Client updatedEntity = null;

                if (client.ClientId == 0)
                    updatedEntity = clientRepository.Add(client);
                else
                    updatedEntity = clientRepository.Update(client);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteClient(int clientId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IClientRepository clientRepository = _DataRepositoryFactory.GetDataRepository<IClientRepository>();

                clientRepository.Remove(clientId);
            });
        }

        public coreEntities.Client GetClient(int clientId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IClientRepository clientRepository = _DataRepositoryFactory.GetDataRepository<IClientRepository>();

                coreEntities.Client clientEntity = clientRepository.Get(clientId);
                if (clientEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Client with ID of {0} is not in database", clientId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return clientEntity;
            });
        }

        public coreEntities.Client[] GetAllClients()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IClientRepository clientRepository = _DataRepositoryFactory.GetDataRepository<IClientRepository>();

                IEnumerable<coreEntities.Client> clients = clientRepository.Get();

                return clients.ToArray();
            });
        }

        #endregion

        #region Database operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Database UpdateDatabase(Database database)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IDatabaseRepository databaseRepository = _DataRepositoryFactory.GetDataRepository<IDatabaseRepository>();

                Database updatedEntity = null;

                if (database.DatabaseId == 0)
                    updatedEntity = databaseRepository.Add(database);
                else
                    updatedEntity = databaseRepository.Update(database);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteDatabase(int databaseId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IDatabaseRepository databaseRepository = _DataRepositoryFactory.GetDataRepository<IDatabaseRepository>();

                databaseRepository.Remove(databaseId);
            });
        }

        public Database GetDatabase(int databaseId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IDatabaseRepository databaseRepository = _DataRepositoryFactory.GetDataRepository<IDatabaseRepository>();

                Database databaseEntity = databaseRepository.Get(databaseId);
                if (databaseEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Database with ID of {0} is not in database", databaseId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return databaseEntity;
            });
        }

        public DatabaseData[] GetAllDatabases()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IDatabaseRepository databaseRepository = _DataRepositoryFactory.GetDataRepository<IDatabaseRepository>();
                List<DatabaseData> databases = new List<DatabaseData>();
                IEnumerable<DatabaseInfo> databaseInfos = databaseRepository.GetDatabases().ToArray();
                foreach (var databaseInfo in databaseInfos)
                {
                    databases.Add(
                        new DatabaseData
                        {
                            DatabaseId = databaseInfo.Database.EntityId,
                            Title = databaseInfo.Database.Title,
                            DatabaseName = databaseInfo.Database.DatabaseName,
                            CompanyCode = databaseInfo.Database.CompanyCode,
                            SolutionId = databaseInfo.Database.SolutionId != null ? databaseInfo.Database.SolutionId : 0,
                            SolutionName = databaseInfo.Solution.Alias,
                            IntegratedSecurity = databaseInfo.Database.IntegratedSecurity,
                            Password = databaseInfo.Database.Password,
                            ServerName = databaseInfo.Database.ServerName,
                            UserName = databaseInfo.Database.UserName,
                            ExtensionData = databaseInfo.Database.ExtensionData,
                            Active = databaseInfo.Database.Active
                        });
                }

                return databases.ToArray();
            });
        }

        #endregion

        #region CompanySecurity operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CompanySecurity UpdateCompanySecurity(CompanySecurity companySecurity)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ICompanySecurityRepository companySecurityRepository = _DataRepositoryFactory.GetDataRepository<ICompanySecurityRepository>();

                CompanySecurity updatedEntity = null;

                if (companySecurity.CompanySecurityId == 0)
                    updatedEntity = companySecurityRepository.Add(companySecurity);
                else
                    updatedEntity = companySecurityRepository.Update(companySecurity);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCompanySecurity(int companySecurityId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ICompanySecurityRepository companySecurityRepository = _DataRepositoryFactory.GetDataRepository<ICompanySecurityRepository>();

                companySecurityRepository.Remove(companySecurityId);
            });
        }

        public CompanySecurity GetCompanySecurity(int companySecurityId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ICompanySecurityRepository companySecurityRepository = _DataRepositoryFactory.GetDataRepository<ICompanySecurityRepository>();

                CompanySecurity companySecurityEntity = companySecurityRepository.Get(companySecurityId);
                if (companySecurityEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CompanySecurity with ID of {0} is not in companySecurity", companySecurityId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return companySecurityEntity;
            });
        }

        public CompanySecurity[] GetAllCompanySecuritys()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ICompanySecurityRepository companySecurityRepository = _DataRepositoryFactory.GetDataRepository<ICompanySecurityRepository>();

                IEnumerable<CompanySecurity> companySecuritys = companySecurityRepository.Get();

                return companySecuritys.ToArray();
            });
        }

        #endregion

        #region UserSession operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public UserSession UpdateUserSession(UserSession userSession)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IUserSessionRepository userSessionRepository = _DataRepositoryFactory.GetDataRepository<IUserSessionRepository>();

                UserSession updatedEntity = null;

                if (userSession.UserSessionId == 0)
                    updatedEntity = userSessionRepository.Add(userSession);
                else
                    updatedEntity = userSessionRepository.Update(userSession);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteUserSession(int userSessionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IUserSessionRepository userSessionRepository = _DataRepositoryFactory.GetDataRepository<IUserSessionRepository>();

                userSessionRepository.Remove(userSessionId);
            });
        }

        public UserSession GetUserSession(int userSessionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IUserSessionRepository userSessionRepository = _DataRepositoryFactory.GetDataRepository<IUserSessionRepository>();

                UserSession userSessionEntity = userSessionRepository.Get(userSessionId);
                if (userSessionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("UserSession with ID of {0} is not in database", userSessionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return userSessionEntity;
            });
        }

        public UserSession[] GetAllUserSessions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IUserSessionRepository userSessionRepository = _DataRepositoryFactory.GetDataRepository<IUserSessionRepository>();

                IEnumerable<UserSession> userSessions = userSessionRepository.Get();

                return userSessions.ToArray();
            });
        }

        public UserSessionData[] GetUserSessions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IUserSessionRepository userSessionRepository = _DataRepositoryFactory.GetDataRepository<IUserSessionRepository>();

                List<UserSessionData> userSessions = new List<UserSessionData>();

                IEnumerable<UserSessionInfo> userSessionInfos = userSessionRepository.GetUserSessions().ToArray();

                foreach (var userSessionInfo in userSessionInfos)
                {
                    userSessions.Add(
                        new UserSessionData
                        {
                            UserId = userSessionInfo.UserSession.UserId,
                            UserName = userSessionInfo.UserSetup.Name,
                            DatabaseId = userSessionInfo.UserSession.DatabaseId,
                            DatabaseName = userSessionInfo.Database.DatabaseName,
                            CanExpire = userSessionInfo.UserSession.CanExpire,
                            Active = userSessionInfo.UserSession.Active
                        }
                );
                }

                return userSessions.ToArray();
            });
        }

        public UserSessionData[] GetUserSessionByLoginID(int loginID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IUserSessionRepository userSessionRepository = _DataRepositoryFactory.GetDataRepository<IUserSessionRepository>();

                List<UserSessionData> userSessions = new List<UserSessionData>();

                IEnumerable<UserSessionInfo> userSessionInfos = userSessionRepository.GetUserSessions().ToArray();

                foreach (var userSessionInfo in userSessionInfos)
                {
                    userSessions.Add(
                        new UserSessionData
                        {
                            UserId = userSessionInfo.UserSession.UserId,
                            UserName = userSessionInfo.UserSetup.Name,
                            DatabaseId = userSessionInfo.UserSession.DatabaseId,
                            DatabaseName = userSessionInfo.Database.DatabaseName,
                            CanExpire = userSessionInfo.UserSession.CanExpire,
                            Active = userSessionInfo.UserSession.Active
                        }
                );
                }

                return userSessions.ToArray();
            });
        }

        #endregion

        #region CompanyUser operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CompanyUser UpdateCompanyUser(CompanyUser companyUser)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ICompanyUserRepository companyUserRepository = _DataRepositoryFactory.GetDataRepository<ICompanyUserRepository>();

                CompanyUser updatedEntity = null;

                if (companyUser.CompanyUserId == 0)
                    updatedEntity = companyUserRepository.Add(companyUser);
                else
                    updatedEntity = companyUserRepository.Update(companyUser);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCompanyUser(int companyUserId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ICompanyUserRepository companyUserRepository = _DataRepositoryFactory.GetDataRepository<ICompanyUserRepository>();

                companyUserRepository.Remove(companyUserId);
            });
        }

        public CompanyUser GetCompanyUser(int companyUserId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ICompanyUserRepository companyUserRepository = _DataRepositoryFactory.GetDataRepository<ICompanyUserRepository>();

                CompanyUser companyUserEntity = companyUserRepository.Get(companyUserId);
                if (companyUserEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CompanyUser with ID of {0} is not in companyUser", companyUserId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return companyUserEntity;
            });
        }

        public CompanyUser[] GetAllCompanyUsers()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ICompanyUserRepository companyUserRepository = _DataRepositoryFactory.GetDataRepository<ICompanyUserRepository>();

                IEnumerable<CompanyUser> companyUsers = companyUserRepository.Get();

                return companyUsers.ToArray();
            });
        }


        public CompanyUser GetCompanyUsers(string loginID, string companyCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                //var groupNames = new List<string>() {};
                //AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ICompanyUserRepository companyUserRepository = _DataRepositoryFactory.GetDataRepository<ICompanyUserRepository>();

                IEnumerable<CompanyUser> companyUsers = companyUserRepository.GetCompanyUsers(loginID, companyCode);

                return companyUsers.FirstOrDefault();
            });
        }

        public CompanyUser[] GetCompanyUserByLogin(string loginID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                //var groupNames = new List<string>() {};
                //AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ICompanyUserRepository companyUserRepository = _DataRepositoryFactory.GetDataRepository<ICompanyUserRepository>();

                IEnumerable<CompanyUser> companyUsers = companyUserRepository.GetCompanyUsers(loginID).ToArray();

                return companyUsers.ToArray();
            });
        }
        #endregion

        #region CompanyModule operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CompanyModule UpdateCompanyModule(CompanyModule companyModule)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ICompanyModuleRepository companyModuleRepository = _DataRepositoryFactory.GetDataRepository<ICompanyModuleRepository>();

                CompanyModule updatedEntity = null;

                if (companyModule.CompanyModuleId == 0)
                    updatedEntity = companyModuleRepository.Add(companyModule);
                else
                    updatedEntity = companyModuleRepository.Update(companyModule);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCompanyModule(int companyModuleId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ICompanyModuleRepository companyModuleRepository = _DataRepositoryFactory.GetDataRepository<ICompanyModuleRepository>();

                companyModuleRepository.Remove(companyModuleId);
            });
        }

        public CompanyModule GetCompanyModule(int companyModuleId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ICompanyModuleRepository companyModuleRepository = _DataRepositoryFactory.GetDataRepository<ICompanyModuleRepository>();

                CompanyModule companyModuleEntity = companyModuleRepository.Get(companyModuleId);
                if (companyModuleEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CompanyModule with ID of {0} is not in companyModule", companyModuleId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return companyModuleEntity;
            });
        }

        public CompanyModule[] GetAllCompanyModules()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ICompanyModuleRepository companyModuleRepository = _DataRepositoryFactory.GetDataRepository<ICompanyModuleRepository>();

                IEnumerable<CompanyModule> companyModules = companyModuleRepository.Get();

                return companyModules.ToArray();
            });
        }


        public CompanyModuleData[] GetCompanyModuleByCompanyCode(string companyCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                //var groupNames = new List<string>() {};
                //AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ICompanyModuleRepository companyModuleRepository = _DataRepositoryFactory.GetDataRepository<ICompanyModuleRepository>();

                List<CompanyModuleData> companyModules = new List<CompanyModuleData>();
                IEnumerable<CompanyModuleInfo> companyModuleDataInfos = companyModuleRepository.GetCompanyModuleByCompanyCode(companyCode).ToArray();

                foreach (var companyModuleDataInfo in companyModuleDataInfos)
                {
                    companyModules.Add(
                        new CompanyModuleData
                        {
                            CompanyModuleId = companyModuleDataInfo.CompanyModule.CompanyModuleId,
                            CompanyCode = companyModuleDataInfo.CompanyModule.CompanyCode,
                            CompanyName = companyModuleDataInfo.Company.Name,
                            ModuleName = companyModuleDataInfo.CompanyModule.ModuleName,
                            Active = companyModuleDataInfo.CompanyModule.Active,
                            Alias = companyModuleDataInfo.Module.Alias

                        });
                }

                return companyModules.ToArray();
            });
        }

        public CompanyModuleData[] GetCompanyModules()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                //var groupNames = new List<string>() {};
                //AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                ICompanyModuleRepository companyModuleRepository = _DataRepositoryFactory.GetDataRepository<ICompanyModuleRepository>();

                List<CompanyModuleData> companyModules = new List<CompanyModuleData>();

                IEnumerable<CompanyModuleInfo> companyModuleDataInfos = companyModuleRepository.GetCompanyModules().ToArray();
                foreach (var companyModuleDataInfo in companyModuleDataInfos)
                {
                    companyModules.Add(
                        new CompanyModuleData
                        {
                            CompanyModuleId = companyModuleDataInfo.CompanyModule.CompanyModuleId,
                            CompanyCode = companyModuleDataInfo.CompanyModule.CompanyCode,
                            CompanyName = companyModuleDataInfo.Company.Name,
                            ModuleName = companyModuleDataInfo.CompanyModule.ModuleName,
                            Active = companyModuleDataInfo.CompanyModule.Active,
                            Alias = companyModuleDataInfo.Module.Alias

                        });
                }

                return companyModules.ToArray();
            });
        }
        #endregion

        #region ErrorTracker operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ErrorTracker UpdateErrorTracker(ErrorTracker errorTracker)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IErrorTrackerRepository errorTrackerRepository = _DataRepositoryFactory.GetDataRepository<IErrorTrackerRepository>();

                ErrorTracker updatedEntity = null;

                if (errorTracker.ErrorTrackerId == 0)
                    updatedEntity = errorTrackerRepository.Add(errorTracker);
                else
                    updatedEntity = errorTrackerRepository.Update(errorTracker);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteErrorTracker(int errorTrackerId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IErrorTrackerRepository errorTrackerRepository = _DataRepositoryFactory.GetDataRepository<IErrorTrackerRepository>();

                errorTrackerRepository.Remove(errorTrackerId);
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteAllErrorTracker()
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IErrorTrackerRepository errorTrackerRepository = _DataRepositoryFactory.GetDataRepository<IErrorTrackerRepository>();


                IEnumerable<ErrorTracker> errorTrackers = errorTrackerRepository.Get();

                foreach (ErrorTracker errorTracker in errorTrackers)
                {
                    errorTrackerRepository.Remove(errorTracker.ErrorTrackerId);
                }


            });
        }

        public ErrorTracker GetErrorTracker(int errorTrackerId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS, SystemCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IErrorTrackerRepository errorTrackerRepository = _DataRepositoryFactory.GetDataRepository<IErrorTrackerRepository>();

                ErrorTracker errorTrackerEntity = errorTrackerRepository.Get(errorTrackerId);
                if (errorTrackerEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ErrorTracker with ID of {0} is not in errorTracker", errorTrackerId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return errorTrackerEntity;
            });
        }

        public ErrorTracker[] GetAllErrorTrackers()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { SystemCoreModuleDefinition.GROUP_ADMINISTRATOR, SystemCoreModuleDefinition.GROUP_SUPER_BUSINESS, SystemCoreModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(SystemCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IErrorTrackerRepository errorTrackerRepository = _DataRepositoryFactory.GetDataRepository<IErrorTrackerRepository>();

                IEnumerable<ErrorTracker> errorTrackers = errorTrackerRepository.Get();

                return errorTrackers.ToArray();
            });
        }

        #endregion

        #region Helper

        protected override bool AllowAccessToOperation(string solutionName, List<string> groupNames)
        {
            if (groupNames.Count == 0)
                return true;

            IUserRoleRepository accountRoleRepository = _DataRepositoryFactory.GetDataRepository<IUserRoleRepository>();
            var accountRoles = accountRoleRepository.GetUserRoleInfo(solutionName, _LoginName, groupNames);

            if (accountRoles == null || accountRoles.Count() <= 0)
            {
                AuthorizationValidationException ex = new AuthorizationValidationException(string.Format("Access denied for {0}.", _LoginName));
                throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
            }

            return true;
        }

        public bool IsNewSystem(string companyCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                bool isNew = false;

                ICompanyRepository companyRepository = _DataRepositoryFactory.GetDataRepository<ICompanyRepository>();
                IDatabaseRepository databaseRepository = _DataRepositoryFactory.GetDataRepository<IDatabaseRepository>();

                IEnumerable<Company> companies = companyRepository.Get();
                IEnumerable<Database> databases = databaseRepository.Get();

                if (companies.Count() == 0 || databases.Count() == 0)
                    isNew = true;
                else
                {
                    var company = companies.Where(c => c.Code == companyCode).FirstOrDefault();
                    if (company != null)
                        isNew = true;
                }

                return isNew;
            });
        }


        public bool IsFirstLogon(string loginId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                bool isFirst = false;

                IUserSetupRepository userSetupRepository = _DataRepositoryFactory.GetDataRepository<IUserSetupRepository>();

                UserSetup userSetups = userSetupRepository.Get().Where(c => c.LoginID == loginId).FirstOrDefault();

                int loginknt = userSetups.StaffID.FirstOrDefault();

                if (loginknt == 0)
                    isFirst = true;
                else
                {
                    isFirst = true;
                }

                return isFirst;
            });
        }

        //void RemoveAllErrorTracker()
        //{

        //    string connection = connew;

        //    int status = 0;

        //    using (var con = new SqlConnection(connection))
        //    {
        //        var cmd = new SqlCommand("Truncate Table cor_error_tracker", con);
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandTimeout = 0;
        //        con.Open();
        //        status = cmd.ExecuteNonQuery();
        //        con.Close();
        //    }


        //}


        #endregion

    }
}
