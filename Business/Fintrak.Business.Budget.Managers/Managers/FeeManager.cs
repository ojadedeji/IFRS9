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

    public class FeeManager : ManagerBase, IFeeService
    {
        public FeeManager()
        {
        }

        public FeeManager(IDataRepositoryFactory dataRepositoryFactory)
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
                        //var updatedRoles = new List<Role>();

                        //foreach (var role in FeeModuleDefinition.GetRoles())
                        //{
                        //    var localRole = existingRoles.Where(c => c.Name == role.Name).FirstOrDefault();

                        //    if (localRole == null)
                        //    {
                        //        localRole = new Role() { Name = role.Name, Description = role.Description, SolutionId = solution.SolutionId, Type = RoleType.Application, Active = true, Deleted = false, CreatedBy = "Auto", CreatedOn = DateTime.Now, UpdatedBy = "Auto", UpdatedOn = DateTime.Now };

                        //        localRole = roleRepository.Add(localRole);
                        //    }
                        //    else
                        //    {
                        //        localRole.Description = role.Description;
                        //        localRole.UpdatedOn = DateTime.Now;
                        //        localRole = roleRepository.Update(localRole);
                        //    }

                        //    updatedRoles.Add(localRole);
                        //}

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

        #region FeeCalculationType Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public FeeCalculationType UpdateFeeCalculationType(FeeCalculationType feeCalculationType)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeCalculationTypeRepository feeCalculationTypeRepository = _DataRepositoryFactory.GetDataRepository<IFeeCalculationTypeRepository>();

                FeeCalculationType updatedEntity = null;

                if (feeCalculationType.FeeCalculationTypeId == 0)
                    updatedEntity = feeCalculationTypeRepository.Add(feeCalculationType);
                else
                    updatedEntity = feeCalculationTypeRepository.Update(feeCalculationType);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteFeeCalculationType(int feeCalculationTypeId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeCalculationTypeRepository feeCalculationTypeRepository = _DataRepositoryFactory.GetDataRepository<IFeeCalculationTypeRepository>();

                feeCalculationTypeRepository.Remove(feeCalculationTypeId);
            });
        }

        public FeeCalculationType GetFeeCalculationType(int feeCalculationTypeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeCalculationTypeRepository feeCalculationTypeRepository = _DataRepositoryFactory.GetDataRepository<IFeeCalculationTypeRepository>();

                FeeCalculationType feeCalculationTypeEntity = feeCalculationTypeRepository.Get(feeCalculationTypeId);
                if (feeCalculationTypeEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("FeeCalculationType with ID of {0} is not in database", feeCalculationTypeId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return feeCalculationTypeEntity;
            });
        }

        public FeeCalculationType[] GetAllFeeCalculationTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeCalculationTypeRepository feeCalculationTypeRepository = _DataRepositoryFactory.GetDataRepository<IFeeCalculationTypeRepository>();

                IEnumerable<FeeCalculationType> feeCalculationTypes = feeCalculationTypeRepository.Get().ToArray();

                return feeCalculationTypes.ToArray();
            });
        }

      
        #endregion

        #region FeeCaption Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public FeeCaption UpdateFeeCaption(FeeCaption feeCaption)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeCaptionRepository feeCaptionRepository = _DataRepositoryFactory.GetDataRepository<IFeeCaptionRepository>();

                FeeCaption updatedEntity = null;

                if (feeCaption.FeeCaptionId == 0)
                    updatedEntity = feeCaptionRepository.Add(feeCaption);
                else
                    updatedEntity = feeCaptionRepository.Update(feeCaption);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteFeeCaption(int feeCaptionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeCaptionRepository feeCaptionRepository = _DataRepositoryFactory.GetDataRepository<IFeeCaptionRepository>();

                feeCaptionRepository.Remove(feeCaptionId);
            });
        }

        public FeeCaption GetFeeCaption(int feeCaptionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeCaptionRepository feeCaptionRepository = _DataRepositoryFactory.GetDataRepository<IFeeCaptionRepository>();

                FeeCaption feeCaptionEntity = feeCaptionRepository.Get(feeCaptionId);
                if (feeCaptionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("FeeCaption with ID of {0} is not in database", feeCaptionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return feeCaptionEntity;
            });
        }

        public FeeCaption[] GetAllFeeCaptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeCaptionRepository feeCaptionRepository = _DataRepositoryFactory.GetDataRepository<IFeeCaptionRepository>();

                IEnumerable<FeeCaption> feeCaptions = feeCaptionRepository.Get().ToArray();

                return feeCaptions.ToArray();
            });
        }


        #endregion

        #region FeeEntry Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public FeeEntry UpdateFeeEntry(FeeEntry feeEntry)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeEntryRepository feeEntryRepository = _DataRepositoryFactory.GetDataRepository<IFeeEntryRepository>();

                FeeEntry updatedEntity = null;

                if (feeEntry.FeeEntryId == 0)
                    updatedEntity = feeEntryRepository.Add(feeEntry);
                else
                    updatedEntity = feeEntryRepository.Update(feeEntry);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteFeeEntry(int feeEntryId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeEntryRepository feeEntryRepository = _DataRepositoryFactory.GetDataRepository<IFeeEntryRepository>();

                feeEntryRepository.Remove(feeEntryId);
            });
        }

        public FeeEntry GetFeeEntry(int feeEntryId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeEntryRepository feeEntryRepository = _DataRepositoryFactory.GetDataRepository<IFeeEntryRepository>();

                FeeEntry feeEntryEntity = feeEntryRepository.Get(feeEntryId);
                if (feeEntryEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("FeeEntry with ID of {0} is not in database", feeEntryId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return feeEntryEntity;
            });
        }

        public FeeEntry[] GetAllFeeEntrys()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeEntryRepository feeEntryRepository = _DataRepositoryFactory.GetDataRepository<IFeeEntryRepository>();

                IEnumerable<FeeEntry> feeEntrys = feeEntryRepository.Get().ToArray();

                return feeEntrys.ToArray();
            });
        }

        public FeeEntryData[] GetFeeEntry(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeEntryRepository feeEntryRepository = _DataRepositoryFactory.GetDataRepository<IFeeEntryRepository>();

                List<FeeEntryData> feeEntry = new List<FeeEntryData>();
                IEnumerable<FeeEntryInfo> feeEntryInfos = feeEntryRepository.GetFeeEntries(year, reviewCode).ToArray();

                foreach (var feeEntryInfo in feeEntryInfos)
                {
                    feeEntry.Add(
                        new FeeEntryData
                        {
                            FeeEntryId = feeEntryInfo.FeeEntry.EntityId,
                            Year = feeEntryInfo.FeeEntry.Year,
                            ReviewCode = feeEntryInfo.FeeEntry.ReviewCode,
                            AccountNo = feeEntryInfo.FeeEntry.AccountNo,
                            CurrencyCode = feeEntryInfo.FeeEntry.CurrencyCode,
                            CustomerName = feeEntryInfo.FeeEntry.CustomerName,
                            DefintionCode = feeEntryInfo.TeamDefinition.Code,
                            DefintionName = feeEntryInfo.TeamDefinition.Name,
                            ItemCode = feeEntryInfo.FeeItem.Code,
                            ItemName = feeEntryInfo.FeeItem.Name,
                            MisCode = feeEntryInfo.Team.Code,
                            MisName = feeEntryInfo.Team.Name,
                            Active = feeEntryInfo.FeeEntry.Active
                        });
                }

                return feeEntry.ToArray();
            });
        }

        #endregion

        #region FeeGroup Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public FeeGroup UpdateFeeGroup(FeeGroup feeGroup)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeGroupRepository feeGroupRepository = _DataRepositoryFactory.GetDataRepository<IFeeGroupRepository>();

                FeeGroup updatedEntity = null;

                if (feeGroup.FeeGroupId == 0)
                    updatedEntity = feeGroupRepository.Add(feeGroup);
                else
                    updatedEntity = feeGroupRepository.Update(feeGroup);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteFeeGroup(int feeGroupId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeGroupRepository feeGroupRepository = _DataRepositoryFactory.GetDataRepository<IFeeGroupRepository>();

                feeGroupRepository.Remove(feeGroupId);
            });
        }

        public FeeGroup GetFeeGroup(int feeGroupId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeGroupRepository feeGroupRepository = _DataRepositoryFactory.GetDataRepository<IFeeGroupRepository>();

                FeeGroup feeGroupEntity = feeGroupRepository.Get(feeGroupId);
                if (feeGroupEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("FeeGroup with ID of {0} is not in database", feeGroupId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return feeGroupEntity;
            });
        }

        public FeeGroup[] GetAllFeeGroups()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeGroupRepository feeGroupRepository = _DataRepositoryFactory.GetDataRepository<IFeeGroupRepository>();

                IEnumerable<FeeGroup> feeGroups = feeGroupRepository.Get().ToArray();

                return feeGroups.ToArray();
            });
        }


        public FeeGroupData[] GetFeeGroups(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeGroupRepository feeGroupRepository = _DataRepositoryFactory.GetDataRepository<IFeeGroupRepository>();

                List<FeeGroupData> feeGroup = new List<FeeGroupData>();
                IEnumerable<FeeGroupInfo> feeGroupInfos = feeGroupRepository.GetFeeGroups(year, reviewCode).ToArray();

                foreach (var feeGroupInfo in feeGroupInfos)
                {
                    feeGroup.Add(
                        new FeeGroupData
                        {
                            FeeGroupId = feeGroupInfo.FeeGroup.EntityId,
                            Year = feeGroupInfo.FeeGroup.Year,
                            Name = feeGroupInfo.FeeGroup.Name,
                            ParentCode = feeGroupInfo.Parent.Code,
                            ParentName = feeGroupInfo.Parent.Name,
                            Position = feeGroupInfo.FeeGroup.Position,
                            Code = feeGroupInfo.FeeGroup.Code,
                            ReviewCode = feeGroupInfo.FeeGroup.ReviewCode,
                            Active = feeGroupInfo.FeeGroup.Active
                        });
                }

                return feeGroup.ToArray();
            });
        }

        #endregion

        #region FeeGroupEntry Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public FeeGroupEntry UpdateFeeGroupEntry(FeeGroupEntry feeGroupEntry)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeGroupEntryRepository feeGroupEntryRepository = _DataRepositoryFactory.GetDataRepository<IFeeGroupEntryRepository>();

                FeeGroupEntry updatedEntity = null;

                if (feeGroupEntry.FeeGroupEntryId == 0)
                    updatedEntity = feeGroupEntryRepository.Add(feeGroupEntry);
                else
                    updatedEntity = feeGroupEntryRepository.Update(feeGroupEntry);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteFeeGroupEntry(int feeGroupEntryId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeGroupEntryRepository feeGroupEntryRepository = _DataRepositoryFactory.GetDataRepository<IFeeGroupEntryRepository>();

                feeGroupEntryRepository.Remove(feeGroupEntryId);
            });
        }

        public FeeGroupEntry GetFeeGroupEntry(int feeGroupEntryId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeGroupEntryRepository feeGroupEntryRepository = _DataRepositoryFactory.GetDataRepository<IFeeGroupEntryRepository>();

                FeeGroupEntry feeGroupEntryEntity = feeGroupEntryRepository.Get(feeGroupEntryId);
                if (feeGroupEntryEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("FeeGroupEntry with ID of {0} is not in database", feeGroupEntryId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return feeGroupEntryEntity;
            });
        }

        public FeeGroupEntryData[] GetFeeGroupEntrys(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeGroupEntryRepository feeGroupEntryRepository = _DataRepositoryFactory.GetDataRepository<IFeeGroupEntryRepository>();

                List<FeeGroupEntryData> feeGroupEntry = new List<FeeGroupEntryData>();
                IEnumerable<FeeGroupEntryInfo> feeGroupEntryInfos = feeGroupEntryRepository.GetFeeGroupEntries(year, reviewCode).ToArray();

                foreach (var feeGroupEntryInfo in feeGroupEntryInfos)
                {
                    feeGroupEntry.Add(
                        new FeeGroupEntryData
                        {
                            FeeGroupEntryId = feeGroupEntryInfo.FeeGroupEntry.EntityId,
                            Year = feeGroupEntryInfo.FeeGroupEntry.Year,
                            CurrencyCode = feeGroupEntryInfo.FeeGroupEntry.CurrencyCode,
                            DefintionCode = feeGroupEntryInfo.TeamDefinition.Code,
                            DefintionName = feeGroupEntryInfo.TeamDefinition.Name,
                            GroupCode = feeGroupEntryInfo.FeeGroup.Code,
                            GroupName = feeGroupEntryInfo.FeeGroup.Name,
                            MisCode = feeGroupEntryInfo.Team.Code,
                            MisName = feeGroupEntryInfo.Team.Name,
                            ReviewCode = feeGroupEntryInfo.FeeGroupEntry.ReviewCode,
                            Active = feeGroupEntryInfo.FeeGroupEntry.Active
                        });
                }

                return feeGroupEntry.ToArray();
            });
        }


        #endregion

        #region FeeItem Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public FeeItem UpdateFeeItem(FeeItem feeItem)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeItemRepository feeItemRepository = _DataRepositoryFactory.GetDataRepository<IFeeItemRepository>();

                FeeItem updatedEntity = null;

                if (feeItem.FeeItemId == 0)
                    updatedEntity = feeItemRepository.Add(feeItem);
                else
                    updatedEntity = feeItemRepository.Update(feeItem);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteFeeItem(int feeItemId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeItemRepository feeItemRepository = _DataRepositoryFactory.GetDataRepository<IFeeItemRepository>();

                feeItemRepository.Remove(feeItemId);
            });
        }

        public FeeItem GetFeeItem(int feeItemId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeItemRepository feeItemRepository = _DataRepositoryFactory.GetDataRepository<IFeeItemRepository>();

                FeeItem feeItemEntity = feeItemRepository.Get(feeItemId);
                if (feeItemEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("FeeItem with ID of {0} is not in database", feeItemId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                return feeItemEntity;
            });
        }

        public FeeItemData[] GetFeeItems(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeItemRepository feeItemRepository = _DataRepositoryFactory.GetDataRepository<IFeeItemRepository>();

                List<FeeItemData> feeItem = new List<FeeItemData>();
                IEnumerable<FeeItemInfo> feeItemInfos = feeItemRepository.GetFeeItems(year, reviewCode).ToArray();

                foreach (var feeItemInfo in feeItemInfos)
                {
                    feeItem.Add(
                        new FeeItemData
                        {
                            FeeItemId = feeItemInfo.FeeItem.EntityId,
                            Year = feeItemInfo.FeeItem.Year,
                            GroupCode = feeItemInfo.FeeGroup.Code,
                            GroupName = feeItemInfo.FeeGroup.Name,
                            Budgetable = feeItemInfo.FeeItem.Budgetable,
                            CalculationType = feeItemInfo.FeeItem.CalculationType,
                            CaptionCode = feeItemInfo.FeeItem.CaptionCode,
                            CaptionName = feeItemInfo.FeeCaption.Name,
                            CategoryCode = feeItemInfo.FeeCategory.Code,
                            CategoryName = feeItemInfo.FeeCategory.Name,
                            Code = feeItemInfo.FeeItem.Code,
                            Movement = feeItemInfo.FeeItem.Movement,
                            Name = feeItemInfo.FeeItem.Name,
                            Position = feeItemInfo.FeeItem.Position,
                            Unit = feeItemInfo.FeeItem.Unit,
                            Active = feeItemInfo.FeeItem.Active
                        });
                }

                return feeItem.ToArray();
            });
        }

        #endregion
      
        #region FeeMovement Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public FeeMovement UpdateFeeMovement(FeeMovement feeMovement)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeMovementRepository feeMovementRepository = _DataRepositoryFactory.GetDataRepository<IFeeMovementRepository>();

                FeeMovement updatedEntity = null;

                if (feeMovement.FeeMovementId == 0)
                    updatedEntity = feeMovementRepository.Add(feeMovement);
                else
                    updatedEntity = feeMovementRepository.Update(feeMovement);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteFeeMovement(int feeMovementId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeMovementRepository feeMovementRepository = _DataRepositoryFactory.GetDataRepository<IFeeMovementRepository>();

                feeMovementRepository.Remove(feeMovementId);
            });
        }

        public FeeMovement GetFeeMovement(int feeMovementId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeMovementRepository feeMovementRepository = _DataRepositoryFactory.GetDataRepository<IFeeMovementRepository>();

                FeeMovement feeMovementEntity = feeMovementRepository.Get(feeMovementId);
                if (feeMovementEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("FeeMovement with ID of {0} is not in database", feeMovementId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return feeMovementEntity;
            });
        }

        public FeeMovement[] GetAllFeeMovements()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeMovementRepository feeMovementRepository = _DataRepositoryFactory.GetDataRepository<IFeeMovementRepository>();

                IEnumerable<FeeMovement> feeMovements = feeMovementRepository.Get().ToArray();

                return feeMovements.ToArray();
            });
        }


        #endregion

        #region FeeSharedExemption Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public FeeSharedExemption UpdateFeeSharedExemption(FeeSharedExemption feeSharedExemption)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeSharedExemptionRepository feeSharedExemptionRepository = _DataRepositoryFactory.GetDataRepository<IFeeSharedExemptionRepository>();

                FeeSharedExemption updatedEntity = null;

                if (feeSharedExemption.FeeSharedExemptionId == 0)
                    updatedEntity = feeSharedExemptionRepository.Add(feeSharedExemption);
                else
                    updatedEntity = feeSharedExemptionRepository.Update(feeSharedExemption);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteFeeSharedExemption(int feeSharedExemptionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeSharedExemptionRepository feeSharedExemptionRepository = _DataRepositoryFactory.GetDataRepository<IFeeSharedExemptionRepository>();

                feeSharedExemptionRepository.Remove(feeSharedExemptionId);
            });
        }

        public FeeSharedExemption GetFeeSharedExemption(int feeSharedExemptionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeSharedExemptionRepository feeSharedExemptionRepository = _DataRepositoryFactory.GetDataRepository<IFeeSharedExemptionRepository>();

                FeeSharedExemption feeSharedExemptionEntity = feeSharedExemptionRepository.Get(feeSharedExemptionId);
                if (feeSharedExemptionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("FeeSharedExemption with ID of {0} is not in database", feeSharedExemptionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return feeSharedExemptionEntity;
            });
        }

        public FeeSharedExemptionData[] GetFeeSharedExemptions(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeSharedExemptionRepository feeItemRepository = _DataRepositoryFactory.GetDataRepository<IFeeSharedExemptionRepository>();

                List<FeeSharedExemptionData> feeItem = new List<FeeSharedExemptionData>();
                IEnumerable<FeeSharedExemptionInfo> feeItemInfos = feeItemRepository.GetFeeSharedExemptions(year, reviewCode).ToArray();

                foreach (var feeItemInfo in feeItemInfos)
                {
                    feeItem.Add(
                        new FeeSharedExemptionData
                        {
                            FeeSharedExemptionId = feeItemInfo.FeeSharedExemption.EntityId,
                            Year = feeItemInfo.FeeSharedExemption.Year,
                            ItemCode = feeItemInfo.FeeItem.Code,
                            ItemName = feeItemInfo.FeeItem.Name,
                            ReviewCode = feeItemInfo.FeeItem.ReviewCode,
                            Active = feeItemInfo.FeeSharedExemption.Active
                        });
                }

                return feeItem.ToArray();
            });
        }


        #endregion

        #region FeeSharedRatio Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public FeeSharedRatio UpdateFeeSharedRatio(FeeSharedRatio feeSharedRatio)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeSharedRatioRepository feeSharedRatioRepository = _DataRepositoryFactory.GetDataRepository<IFeeSharedRatioRepository>();

                FeeSharedRatio updatedEntity = null;

                if (feeSharedRatio.FeeSharedRatioId == 0)
                    updatedEntity = feeSharedRatioRepository.Add(feeSharedRatio);
                else
                    updatedEntity = feeSharedRatioRepository.Update(feeSharedRatio);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteFeeSharedRatio(int feeSharedRatioId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeSharedRatioRepository feeSharedRatioRepository = _DataRepositoryFactory.GetDataRepository<IFeeSharedRatioRepository>();

                feeSharedRatioRepository.Remove(feeSharedRatioId);
            });
        }

        public FeeSharedRatio GetFeeSharedRatio(int feeSharedRatioId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeSharedRatioRepository feeSharedRatioRepository = _DataRepositoryFactory.GetDataRepository<IFeeSharedRatioRepository>();

                FeeSharedRatio feeSharedRatioEntity = feeSharedRatioRepository.Get(feeSharedRatioId);
                if (feeSharedRatioEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("FeeSharedRatio with ID of {0} is not in database", feeSharedRatioId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return feeSharedRatioEntity;
            });
        }

        public FeeSharedRatioData[] GetFeeSharedRatios(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeSharedRatioRepository feeSharedRatioRepository = _DataRepositoryFactory.GetDataRepository<IFeeSharedRatioRepository>();

                List<FeeSharedRatioData> feeSharedRatio = new List<FeeSharedRatioData>();
                IEnumerable<FeeSharedRatioInfo> feeSharedRatioInfos = feeSharedRatioRepository.GetFeeSharedRatios(year, reviewCode).ToArray();

                foreach (var feeSharedRatioInfo in feeSharedRatioInfos)
                {
                    feeSharedRatio.Add(
                        new FeeSharedRatioData
                        {
                            FeeSharedRatioId = feeSharedRatioInfo.FeeSharedRatio.EntityId,
                            Year = feeSharedRatioInfo.FeeSharedRatio.Year,
                            ItemCode = feeSharedRatioInfo.FeeItem.Code,
                            DefintionCode=feeSharedRatioInfo.TeamDefinition.Code,
                            DefintionName=feeSharedRatioInfo.TeamDefinition.Name,
                            MisCode=feeSharedRatioInfo.Team.Code,
                            MisName=feeSharedRatioInfo.Team.Name,
                            ItemName = feeSharedRatioInfo.FeeItem.Name,
                            ReviewCode = feeSharedRatioInfo.FeeItem.ReviewCode,
                            Active = feeSharedRatioInfo.FeeSharedRatio.Active
                        });
                }

                return feeSharedRatio.ToArray();
            });
        }

        #endregion

        #region FeeVolumeBasedSetup Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public FeeVolumeBasedSetup UpdateFeeVolumeBasedSetup(FeeVolumeBasedSetup feeVolumeBasedSetup)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeVolumeBasedSetupRepository feeVolumeBasedSetupRepository = _DataRepositoryFactory.GetDataRepository<IFeeVolumeBasedSetupRepository>();

                FeeVolumeBasedSetup updatedEntity = null;

                if (feeVolumeBasedSetup.FeeVolumeBasedSetupId == 0)
                    updatedEntity = feeVolumeBasedSetupRepository.Add(feeVolumeBasedSetup);
                else
                    updatedEntity = feeVolumeBasedSetupRepository.Update(feeVolumeBasedSetup);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteFeeVolumeBasedSetup(int feeVolumeBasedSetupId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeVolumeBasedSetupRepository feeVolumeBasedSetupRepository = _DataRepositoryFactory.GetDataRepository<IFeeVolumeBasedSetupRepository>();

                feeVolumeBasedSetupRepository.Remove(feeVolumeBasedSetupId);
            });
        }

        public FeeVolumeBasedSetup GetFeeVolumeBasedSetup(int feeVolumeBasedSetupId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeVolumeBasedSetupRepository feeVolumeBasedSetupRepository = _DataRepositoryFactory.GetDataRepository<IFeeVolumeBasedSetupRepository>();

                FeeVolumeBasedSetup feeVolumeBasedSetupEntity = feeVolumeBasedSetupRepository.Get(feeVolumeBasedSetupId);
                if (feeVolumeBasedSetupEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("FeeVolumeBasedSetup with ID of {0} is not in database", feeVolumeBasedSetupId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return feeVolumeBasedSetupEntity;
            });
        }

        public FeeVolumeBasedSetupData[] GetFeeVolumeBasedSetups(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IFeeVolumeBasedSetupRepository feeVolumeBasedSetupRepository = _DataRepositoryFactory.GetDataRepository<IFeeVolumeBasedSetupRepository>();

                List<FeeVolumeBasedSetupData> feeVolumeBasedSetup = new List<FeeVolumeBasedSetupData>();
                IEnumerable<FeeVolumeBasedSetupInfo> feeVolumeBasedSetupInfos = feeVolumeBasedSetupRepository.GetFeeVolumeBasedSetups(year, reviewCode).ToArray();

                foreach (var feeVolumeBasedSetupInfo in feeVolumeBasedSetupInfos)
                {
                    feeVolumeBasedSetup.Add(
                        new FeeVolumeBasedSetupData
                        {
                            FeeVolumeBasedSetupId = feeVolumeBasedSetupInfo.FeeVolumeBasedSetup.EntityId,
                            Year = feeVolumeBasedSetupInfo.FeeVolumeBasedSetup.Year,
                            FeeCode=feeVolumeBasedSetupInfo.FeeVolumeBasedSetup.FeeCode,
                            FeeName=feeVolumeBasedSetupInfo.FeeItem.Name,
                            MakeUpCode=feeVolumeBasedSetupInfo.FeeVolumeBasedSetup.MakeUpCode,                                             
                            ReviewCode = feeVolumeBasedSetupInfo.FeeItem.ReviewCode,
                            Active = feeVolumeBasedSetupInfo.FeeVolumeBasedSetup.Active
                        });
                }

                return feeVolumeBasedSetup.ToArray();
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
