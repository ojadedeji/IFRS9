using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Transactions;
using Fintrak.Data.Budget.Contracts;
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
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Business.Budget.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]

    public class OpexManager : ManagerBase, IOpexService
    {
        public OpexManager()
        {
        }

        public OpexManager(IDataRepositoryFactory dataRepositoryFactory)
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
                    systemCoreEntities.Module module = moduleRepository.Get().Where(c => c.Name == OpexModuleDefinition.MODULE_NAME).FirstOrDefault();

                    var register = false;
                    if (module == null)
                        register = true;
                    else
                        register = module.CanUpdate;

                    if (register)
                    {
                        //check if module category exit
                        Solution solution = solutionRepository.Get().Where(c => c.Name == OpexModuleDefinition.SOLUTION_NAME).FirstOrDefault();
                        if (solution == null)
                        {
                            //register solution
                            solution = new Solution()
                            {
                                Name = OpexModuleDefinition.SOLUTION_NAME,
                                Alias = OpexModuleDefinition.SOLUTION_ALIAS,
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
                                Name = OpexModuleDefinition.MODULE_NAME,
                                Alias = OpexModuleDefinition.MODULE_ALIAS,
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

                        //foreach (var role in OpexModuleDefinition.GetRoles())
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

                        foreach (var menu in OpexModuleDefinition.GetMenus())
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

                        foreach (var menuRole in OpexModuleDefinition.GetMenuRoles())
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

        #region OpexCategory Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public OpexCategory UpdateOpexCategory(OpexCategory opexCategory)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOpexCategoryRepository opexCategoryRepository = _DataRepositoryFactory.GetDataRepository<IOpexCategoryRepository>();

                OpexCategory updatedEntity = null;

                if (opexCategory.OpexCategoryId == 0)
                    updatedEntity = opexCategoryRepository.Add(opexCategory);
                else
                    updatedEntity = opexCategoryRepository.Update(opexCategory);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOpexCategory(int opexCategoryId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOpexCategoryRepository opexCategoryRepository = _DataRepositoryFactory.GetDataRepository<IOpexCategoryRepository>();

                opexCategoryRepository.Remove(opexCategoryId);
            });
        }

        public OpexCategory GetOpexCategory(int opexCategoryId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOpexCategoryRepository opexCategoryRepository = _DataRepositoryFactory.GetDataRepository<IOpexCategoryRepository>();

                OpexCategory opexCategoryEntity = opexCategoryRepository.Get(opexCategoryId);
                if (opexCategoryEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OpexCategory with ID of {0} is not in database", opexCategoryId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return opexCategoryEntity;
            });
        }

        public OpexCategory[] GetAllOpexCategorys()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOpexCategoryRepository opexCategoryRepository = _DataRepositoryFactory.GetDataRepository<IOpexCategoryRepository>();

                IEnumerable<OpexCategory> opexCategorys = opexCategoryRepository.Get().ToArray();

                return opexCategorys.ToArray();
            });
        }

        public OpexCategoryData[] GetOpexCategories(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOpexCategoryRepository opexCategoryRepository = _DataRepositoryFactory.GetDataRepository<IOpexCategoryRepository>();

                List<OpexCategoryData> opexCategory = new List<OpexCategoryData>();
                IEnumerable<OpexCategoryInfo> opexCategoryInfos = opexCategoryRepository.GetOpexCategories(year, reviewCode).ToArray();

                foreach (var opexCategoryInfo in opexCategoryInfos)
                {
                    opexCategory.Add(
                        new OpexCategoryData
                        {
                            OpexCategoryId = opexCategoryInfo.OpexCategory.EntityId,
                            Year = opexCategoryInfo.OpexCategory.Year,
                            Code = opexCategoryInfo.OpexCategory.Code,
                            Description = opexCategoryInfo.OpexCategory.Description,
                            Name = opexCategoryInfo.OpexCategory.Name,
                            ParentCode = opexCategoryInfo.Parent.Code,
                            ParentName = opexCategoryInfo.Parent.Name,
                            Position = opexCategoryInfo.OpexCategory.Position,
                            ReviewCode = opexCategoryInfo.OpexCategory.ReviewCode,
                            Active = opexCategoryInfo.OpexCategory.Active
                        });
                }

                return opexCategory.ToArray();
            });
        }

        #endregion

        #region OpexEntry Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public OpexEntry UpdateOpexEntry(OpexEntry opexEntry)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOpexEntryRepository opexEntryRepository = _DataRepositoryFactory.GetDataRepository<IOpexEntryRepository>();

                OpexEntry updatedEntity = null;

                if (opexEntry.OpexEntryId == 0)
                    updatedEntity = opexEntryRepository.Add(opexEntry);
                else
                    updatedEntity = opexEntryRepository.Update(opexEntry);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOpexEntry(int opexEntryId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOpexEntryRepository opexEntryRepository = _DataRepositoryFactory.GetDataRepository<IOpexEntryRepository>();

                opexEntryRepository.Remove(opexEntryId);
            });
        }

        public OpexEntry GetOpexEntry(int opexEntryId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOpexEntryRepository opexEntryRepository = _DataRepositoryFactory.GetDataRepository<IOpexEntryRepository>();

                OpexEntry opexEntryEntity = opexEntryRepository.Get(opexEntryId);
                if (opexEntryEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OpexEntry with ID of {0} is not in database", opexEntryId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return opexEntryEntity;
            });
        }

        public OpexEntryData[] GetOpexEntries(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOpexEntryRepository opexEntryRepository = _DataRepositoryFactory.GetDataRepository<IOpexEntryRepository>();

                List<OpexEntryData> opexEntry = new List<OpexEntryData>();
                IEnumerable<OpexEntryInfo> opexEntryInfos = opexEntryRepository.GetOpexEntries(year, reviewCode).ToArray();

                foreach (var opexEntryInfo in opexEntryInfos)
                {
                    opexEntry.Add(
                        new OpexEntryData
                        {
                            OpexEntryId = opexEntryInfo.OpexEntry.EntityId,
                            Year = opexEntryInfo.OpexEntry.Year,
                            CenterType = opexEntryInfo.OpexEntry.CenterType,
                            CurrencyCode = opexEntryInfo.OpexEntry.CurrencyCode,
                            DefintionCode = opexEntryInfo.TeamDefinition.Code,
                            DefintionName = opexEntryInfo.TeamDefinition.Name,
                            ItemCode = opexEntryInfo.OpexItem.Code,
                            ItemName = opexEntryInfo.OpexItem.Name,
                            MisCode = opexEntryInfo.Team.Code,
                            MisName = opexEntryInfo.Team.Name,
                            Note = opexEntryInfo.OpexEntry.Note,
                            ReviewCode = opexEntryInfo.OpexEntry.ReviewCode,
                            Active = opexEntryInfo.OpexEntry.Active

                        });
                }

                return opexEntry.ToArray();
            });
        }

        #endregion

        #region OpexItem Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public OpexItem UpdateOpexItem(OpexItem opexItem)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOpexItemRepository opexItemRepository = _DataRepositoryFactory.GetDataRepository<IOpexItemRepository>();

                OpexItem updatedEntity = null;

                if (opexItem.OpexItemId == 0)
                    updatedEntity = opexItemRepository.Add(opexItem);
                else
                    updatedEntity = opexItemRepository.Update(opexItem);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOpexItem(int opexItemId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOpexItemRepository opexItemRepository = _DataRepositoryFactory.GetDataRepository<IOpexItemRepository>();

                opexItemRepository.Remove(opexItemId);
            });
        }

        public OpexItem GetOpexItem(int opexItemId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOpexItemRepository opexItemRepository = _DataRepositoryFactory.GetDataRepository<IOpexItemRepository>();

                OpexItem opexItemEntity = opexItemRepository.Get(opexItemId);
                if (opexItemEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OpexItem with ID of {0} is not in database", opexItemId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return opexItemEntity;
            });
        }

        public OpexItemData[] GetOpexItems(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOpexItemRepository opexItemRepository = _DataRepositoryFactory.GetDataRepository<IOpexItemRepository>();

                List<OpexItemData> opexItem = new List<OpexItemData>();
                IEnumerable<OpexItemInfo> opexItemInfos = opexItemRepository.GetOpexItems(year, reviewCode).ToArray();

                foreach (var opexItemInfo in opexItemInfos)
                {
                    opexItem.Add(
                        new OpexItemData
                        {
                            OpexItemId = opexItemInfo.OpexItem.EntityId,
                            Year = opexItemInfo.OpexItem.Year,
                            Budgetable = opexItemInfo.OpexItem.Budgetable,
                            CategoryCode = opexItemInfo.OpexItem.CategoryCode,
                            CategoryName = opexItemInfo.OpexCategory.Name,
                            Code = opexItemInfo.OpexItem.Code,
                            GLCode = opexItemInfo.OpexItem.GLCode,
                            //  GLName = opexItemInfo.OpexItem.,
                            Name = opexItemInfo.OpexItem.Name,
                            Position = opexItemInfo.OpexItem.Position,
                            VolumeBase = opexItemInfo.OpexItem.VolumeBase,
                            ReviewCode = opexItemInfo.OpexItem.ReviewCode,
                            Active = opexItemInfo.OpexItem.Active
                        });
                }

                return opexItem.ToArray();
            });
        }

        #endregion

        #region OpexVolumeBasedRate Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public OpexVolumeBasedRate UpdateOpexVolumeBasedRate(OpexVolumeBasedRate opexVolumeBasedRate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOpexVolumeBasedRateRepository opexVolumeBasedRateRepository = _DataRepositoryFactory.GetDataRepository<IOpexVolumeBasedRateRepository>();

                OpexVolumeBasedRate updatedEntity = null;

                if (opexVolumeBasedRate.OpexVolumeBasedRateId == 0)
                    updatedEntity = opexVolumeBasedRateRepository.Add(opexVolumeBasedRate);
                else
                    updatedEntity = opexVolumeBasedRateRepository.Update(opexVolumeBasedRate);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOpexVolumeBasedRate(int opexVolumeBasedRateId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOpexVolumeBasedRateRepository opexVolumeBasedRateRepository = _DataRepositoryFactory.GetDataRepository<IOpexVolumeBasedRateRepository>();

                opexVolumeBasedRateRepository.Remove(opexVolumeBasedRateId);
            });
        }

        public OpexVolumeBasedRate GetOpexVolumeBasedRate(int opexVolumeBasedRateId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOpexVolumeBasedRateRepository opexVolumeBasedRateRepository = _DataRepositoryFactory.GetDataRepository<IOpexVolumeBasedRateRepository>();

                OpexVolumeBasedRate opexVolumeBasedRateEntity = opexVolumeBasedRateRepository.Get(opexVolumeBasedRateId);
                if (opexVolumeBasedRateEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OpexVolumeBasedRate with ID of {0} is not in database", opexVolumeBasedRateId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return opexVolumeBasedRateEntity;
            });
        }

        public OpexVolumeBasedRateData[] GetOpexVolumeBasedRates(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOpexVolumeBasedRateRepository opexVolumeBasedRateRepository = _DataRepositoryFactory.GetDataRepository<IOpexVolumeBasedRateRepository>();

                List<OpexVolumeBasedRateData> opexVolumeBasedRate = new List<OpexVolumeBasedRateData>();
                IEnumerable<OpexVolumeBasedRateInfo> opexVolumeBasedRateInfos = opexVolumeBasedRateRepository.GetOpexVolumeBasedRates(year, reviewCode).ToArray();

                foreach (var opexVolumeBasedRateInfo in opexVolumeBasedRateInfos)
                {
                    opexVolumeBasedRate.Add(
                        new OpexVolumeBasedRateData
                        {
                            OpexVolumeBasedRateId = opexVolumeBasedRateInfo.OpexVolumeBasedRate.EntityId,
                            Year = opexVolumeBasedRateInfo.OpexVolumeBasedRate.Year,
                            ReviewCode = opexVolumeBasedRateInfo.OpexVolumeBasedRate.ReviewCode,
                            CenterType = opexVolumeBasedRateInfo.OpexVolumeBasedRate.CenterType,
                            DefintionCode = opexVolumeBasedRateInfo.TeamDefinition.Code,
                            DefintionName = opexVolumeBasedRateInfo.TeamDefinition.Name,
                            ItemCode = opexVolumeBasedRateInfo.OpexItem.Code,
                            ItemName = opexVolumeBasedRateInfo.OpexItem.Name,
                            MisCode = opexVolumeBasedRateInfo.Team.Code,
                            MisName = opexVolumeBasedRateInfo.Team.Name,
                            Active = opexVolumeBasedRateInfo.OpexVolumeBasedRate.Active
                        });
                }

                return opexVolumeBasedRate.ToArray();
            });
        }

        #endregion

        #region OpexVolumeBasedSetup Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public OpexVolumeBasedSetup UpdateOpexVolumeBasedSetup(OpexVolumeBasedSetup opexVolumeBasedSetup)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOpexVolumeBasedSetupRepository opexVolumeBasedSetupRepository = _DataRepositoryFactory.GetDataRepository<IOpexVolumeBasedSetupRepository>();

                OpexVolumeBasedSetup updatedEntity = null;

                if (opexVolumeBasedSetup.OpexVolumeBasedSetupId == 0)
                    updatedEntity = opexVolumeBasedSetupRepository.Add(opexVolumeBasedSetup);
                else
                    updatedEntity = opexVolumeBasedSetupRepository.Update(opexVolumeBasedSetup);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOpexVolumeBasedSetup(int opexVolumeBasedSetupId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOpexVolumeBasedSetupRepository opexVolumeBasedSetupRepository = _DataRepositoryFactory.GetDataRepository<IOpexVolumeBasedSetupRepository>();

                opexVolumeBasedSetupRepository.Remove(opexVolumeBasedSetupId);
            });
        }

        public OpexVolumeBasedSetup GetOpexVolumeBasedSetup(int opexVolumeBasedSetupId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOpexVolumeBasedSetupRepository opexVolumeBasedSetupRepository = _DataRepositoryFactory.GetDataRepository<IOpexVolumeBasedSetupRepository>();

                OpexVolumeBasedSetup opexVolumeBasedSetupEntity = opexVolumeBasedSetupRepository.Get(opexVolumeBasedSetupId);
                if (opexVolumeBasedSetupEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OpexVolumeBasedSetup with ID of {0} is not in database", opexVolumeBasedSetupId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return opexVolumeBasedSetupEntity;
            });
        }

        public OpexVolumeBasedSetupData[] GetOpexVolumeBasedSetups(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOpexVolumeBasedSetupRepository opexVolumeBasedSetupRepository = _DataRepositoryFactory.GetDataRepository<IOpexVolumeBasedSetupRepository>();

                List<OpexVolumeBasedSetupData> opexVolumeBasedSetup = new List<OpexVolumeBasedSetupData>();
                IEnumerable<OpexVolumeBasedSetupInfo> opexVolumeBasedSetupInfos = opexVolumeBasedSetupRepository.GetOpexVolumeBasedSetups(year, reviewCode).ToArray();

                foreach (var opexVolumeBasedSetupInfo in opexVolumeBasedSetupInfos)
                {
                    opexVolumeBasedSetup.Add(
                        new OpexVolumeBasedSetupData
                        {
                            OpexVolumeBasedSetupId = opexVolumeBasedSetupInfo.OpexVolumeBasedSetup.EntityId,
                            Year = opexVolumeBasedSetupInfo.OpexVolumeBasedSetup.Year,
                            ReviewCode = opexVolumeBasedSetupInfo.OpexVolumeBasedSetup.ReviewCode,
                            OpexCode = opexVolumeBasedSetupInfo.OpexVolumeBasedSetup.OpexCode,
                          //  OpexName = opexVolumeBasedSetupInfo.OpexVolumeBasedSetup.,
                            ProductCode = opexVolumeBasedSetupInfo.OpexVolumeBasedSetup.ProductCode,
                            ProductName = opexVolumeBasedSetupInfo.Product.Name,                           
                            Active = opexVolumeBasedSetupInfo.OpexVolumeBasedSetup.Active
                        });
                }

                return opexVolumeBasedSetup.ToArray();
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
