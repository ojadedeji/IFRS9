using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq;
using System.ServiceModel;
using System.Transactions;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Business.IFRS.Contracts;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Common.Utils;
using Fintrak.Shared.Core.Entities;

using Fintrak.Shared.SystemCore.Entities;
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.SystemCore.Framework;

namespace Fintrak.Business.IFRS.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class FinancialInstrumentManager : ManagerBase, IFinancialInstrumentService
    {
        public FinancialInstrumentManager()
        {
        }

        public FinancialInstrumentManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        const string SOLUTION_NAME = "FIN_IFRS";
        const string SOLUTION_ALIAS = "IFRS";
        const string MODULE_NAME = "FIN_FINANCIAL_INSTRUMENT";
        const string MODULE_ALIAS = "Financial Instruments";

        const string GROUP_ADMINISTRATOR = "Administrator";
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
                    systemCoreEntities.Module module = moduleRepository.Get().Where(c => c.Name == FinancialInstrumentModuleDefinition.MODULE_NAME).FirstOrDefault();

                    var register = false;
                    if (module == null)
                        register = true;
                    else
                        register = module.CanUpdate;
                    if (register)
                    {
                        //check if module category exit
                        Solution solution = solutionRepository.Get().Where(c => c.Name == FinancialInstrumentModuleDefinition.SOLUTION_NAME).FirstOrDefault();
                        if (solution == null)
                        {
                            //register solution
                            solution = new Solution()
                            {
                                Name = FinancialInstrumentModuleDefinition.SOLUTION_NAME,
                                Alias = FinancialInstrumentModuleDefinition.SOLUTION_ALIAS,
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
                                Name = FinancialInstrumentModuleDefinition.MODULE_NAME,
                                Alias = FinancialInstrumentModuleDefinition.MODULE_ALIAS,
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

                        foreach (var role in FinancialInstrumentModuleDefinition.GetRoles())
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

                        foreach (var menu in FinancialInstrumentModuleDefinition.GetMenus())
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

                        foreach (var menuRole in FinancialInstrumentModuleDefinition.GetMenuRoles())
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

        #region FairValueBasisMap operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public FairValueBasisMap UpdateFairValueBasisMap(FairValueBasisMap fairValueBasisMap)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFairValueBasisMapRepository fairValueBasisMapRepository = _DataRepositoryFactory.GetDataRepository<IFairValueBasisMapRepository>();

                FairValueBasisMap updatedEntity = null;

                if (fairValueBasisMap.FairValueBasisMapId == 0)
                    updatedEntity = fairValueBasisMapRepository.Add(fairValueBasisMap);
                else
                    updatedEntity = fairValueBasisMapRepository.Update(fairValueBasisMap);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteFairValueBasisMap(int fairValueBasisMapId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFairValueBasisMapRepository fairValueBasisMapRepository = _DataRepositoryFactory.GetDataRepository<IFairValueBasisMapRepository>();

                fairValueBasisMapRepository.Remove(fairValueBasisMapId);
            });
        }

        public FairValueBasisMap GetFairValueBasisMap(int fairValueBasisMapId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFairValueBasisMapRepository fairValueBasisMapRepository = _DataRepositoryFactory.GetDataRepository<IFairValueBasisMapRepository>();

                FairValueBasisMap fairValueBasisMapEntity = fairValueBasisMapRepository.Get(fairValueBasisMapId);
                if (fairValueBasisMapEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("FairValueBasisMap with ID of {0} is not in database", fairValueBasisMapId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return fairValueBasisMapEntity;
            });
        }

        //public FairValueBasisMap[] GetAllFairValueBasisMaps()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IFairValueBasisMapRepository fairValueBasisMapRepository = _DataRepositoryFactory.GetDataRepository<IFairValueBasFairValueBasisMapisMapRepository>();

        //        IEnumerable<FairValueBasisMap> fairValueBasisMaps = fairValueBasisMapRepository.Get().ToArray();

        //        return fairValueBasisMaps.ToArray();
        //    });
        //}

        public FairValueBasisMapData[] GetAllFairValueBasisMaps()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFairValueBasisMapRepository fairValueBasisMapRepository = _DataRepositoryFactory.GetDataRepository<IFairValueBasisMapRepository>();


                List<FairValueBasisMapData> fairValueBasisMaps = new List<FairValueBasisMapData>();
                IEnumerable<FairValueBasisMap> fairValueBasisMapInfo = fairValueBasisMapRepository.Get().ToArray();

                foreach (var fairValueBasisMap in fairValueBasisMapInfo)
                {
                    fairValueBasisMaps.Add(
                        new FairValueBasisMapData
                        {
                            FairValueBasisMapId = fairValueBasisMap.EntityId,
                            BasisLevel = fairValueBasisMap.BasisLevel,
                            Classification = fairValueBasisMap.Classification,
                            ClassificationName = fairValueBasisMap.Classification.ToString(),
                            InstrumentType = fairValueBasisMap.InstrumentType,
                            InstrumentTypeName = fairValueBasisMap.InstrumentType.ToString(),
                            CompanyCode = fairValueBasisMap.CompanyCode,
                            Active = fairValueBasisMap.Active
                        });
                }

                return fairValueBasisMaps.ToArray();
            });
        }     
        #endregion

        #region FairValueBasisExemption operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public FairValueBasisExemption UpdateFairValueBasisExemption(FairValueBasisExemption fairValueBasisExemption)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFairValueBasisExemptionRepository fairValueBasisExemptionRepository = _DataRepositoryFactory.GetDataRepository<IFairValueBasisExemptionRepository>();

                FairValueBasisExemption updatedEntity = null;

                if (fairValueBasisExemption.FairValueBasisExemptionId == 0)
                    updatedEntity = fairValueBasisExemptionRepository.Add(fairValueBasisExemption);
                else
                    updatedEntity = fairValueBasisExemptionRepository.Update(fairValueBasisExemption);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteFairValueBasisExemption(int fairValueBasisExemptionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFairValueBasisExemptionRepository fairValueBasisExemptionRepository = _DataRepositoryFactory.GetDataRepository<IFairValueBasisExemptionRepository>();

                fairValueBasisExemptionRepository.Remove(fairValueBasisExemptionId);
            });
        }

        public FairValueBasisExemption GetFairValueBasisExemption(int fairValueBasisExemptionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFairValueBasisExemptionRepository fairValueBasisExemptionRepository = _DataRepositoryFactory.GetDataRepository<IFairValueBasisExemptionRepository>();

                FairValueBasisExemption fairValueBasisExemptionEntity = fairValueBasisExemptionRepository.Get(fairValueBasisExemptionId);
                if (fairValueBasisExemptionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("FairValueBasisExemption with ID of {0} is not in database", fairValueBasisExemptionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return fairValueBasisExemptionEntity;
            });
        }

        public FairValueBasisExemptionData[] GetAllFairValueBasisExemptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFairValueBasisExemptionRepository fairValueBasisExemptionRepository = _DataRepositoryFactory.GetDataRepository<IFairValueBasisExemptionRepository>();

                List<FairValueBasisExemptionData> fairValueBasisExemptions = new List<FairValueBasisExemptionData>();
                IEnumerable<FairValueBasisExemption> fairValueBasisExemptionInfo = fairValueBasisExemptionRepository.Get().ToArray();

                foreach (var fairValueBasisExemption in fairValueBasisExemptionInfo)
                {
                    fairValueBasisExemptions.Add(
                        new FairValueBasisExemptionData
                        {
                            FairValueBasisExemptionId = fairValueBasisExemption.EntityId,
                            BasisLevel = fairValueBasisExemption.BasisLevel,
                            RefNo = fairValueBasisExemption.RefNo,
                            InstrumentType = fairValueBasisExemption.InstrumentType,
                            InstrumentTypeName = fairValueBasisExemption.InstrumentType.ToString(),
                            CompanyCode = fairValueBasisExemption.CompanyCode,
                            Active = fairValueBasisExemption.Active
                        });
                }

                return fairValueBasisExemptions.ToArray();
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
