using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Transactions;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Business.Budget.Contracts;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Data;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.SystemCore.Entities;
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.SystemCore.Framework;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;
using Fintrak.Shared.Common.Exceptions;

namespace Fintrak.Business.Budget.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]

    public class CapexManager : ManagerBase, ICapexService
    {
        public CapexManager()
        {
        }

        public CapexManager(IDataRepositoryFactory dataRepositoryFactory)
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
                    systemCoreEntities.Module module = moduleRepository.Get().Where(c => c.Name == CapexModuleDefinition.MODULE_NAME).FirstOrDefault();

                    var register = false;
                    if (module == null)
                        register = true;
                    else
                        register = module.CanUpdate;

                    if (register)
                    {
                        //check if module category exit
                        Solution solution = solutionRepository.Get().Where(c => c.Name == CapexModuleDefinition.SOLUTION_NAME).FirstOrDefault();
                        if (solution == null)
                        {
                            //register solution
                            solution = new Solution()
                            {
                                Name = CapexModuleDefinition.SOLUTION_NAME,
                                Alias = CapexModuleDefinition.SOLUTION_ALIAS,
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
                                Name = CapexModuleDefinition.MODULE_NAME,
                                Alias = CapexModuleDefinition.MODULE_ALIAS,
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

                        //foreach (var role in CapexModuleDefinition.GetRoles())
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

                        foreach (var menu in CapexModuleDefinition.GetMenus())
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

                        foreach (var menuRole in CapexModuleDefinition.GetMenuRoles())
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

        #region CapexCategory Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CapexCategory UpdateCapexCategory(CapexCategory capexCategory)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR,BudgetModuleDefinition.GROUP_BUSINESS};
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICapexCategoryRepository capexCategoryRepository = _DataRepositoryFactory.GetDataRepository<ICapexCategoryRepository>();

                CapexCategory updatedEntity = null;

                if (capexCategory.CapexCategoryId == 0)
                    updatedEntity = capexCategoryRepository.Add(capexCategory);
                else
                    updatedEntity = capexCategoryRepository.Update(capexCategory);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCapexCategory(int capexCategoryId)
        {
            ExecuteFaultHandledOperation(() =>
            {
               var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR,BudgetModuleDefinition.GROUP_BUSINESS};
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICapexCategoryRepository capexCategoryRepository = _DataRepositoryFactory.GetDataRepository<ICapexCategoryRepository>();

                capexCategoryRepository.Remove(capexCategoryId);
            });
        }

        public CapexCategory GetCapexCategory(int capexCategoryId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICapexCategoryRepository capexCategoryRepository = _DataRepositoryFactory.GetDataRepository<ICapexCategoryRepository>();

                CapexCategory capexCategoryEntity = capexCategoryRepository.Get(capexCategoryId);
                if (capexCategoryEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CapexCategory with ID of {0} is not in database", capexCategoryId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return capexCategoryEntity;
            });
        }

        public CapexCategory[] GetAllCapexCategorys()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR,BudgetModuleDefinition.GROUP_BUSINESS};
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICapexCategoryRepository capexCategoryRepository = _DataRepositoryFactory.GetDataRepository<ICapexCategoryRepository>();

                IEnumerable<CapexCategory> capexCategorys = capexCategoryRepository.Get().ToArray();

                return capexCategorys.ToArray();
            });
        }

        public CapexCategoryData[] GetAllCapexCategories(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICapexCategoryRepository capexCategoryRepository = _DataRepositoryFactory.GetDataRepository<ICapexCategoryRepository>();

                List<CapexCategoryData> capexCategory = new List<CapexCategoryData>();
                IEnumerable<CapexCategoryInfo> capexCategoryInfos = capexCategoryRepository.GetAllCapexCategories().ToArray();

                foreach (var capexCategoryInfo in capexCategoryInfos)
                {
                    capexCategory.Add(
                        new CapexCategoryData
                        {
                            CapexCategoryId = capexCategoryInfo.CapexCategory.EntityId,
                            Code=capexCategoryInfo.CapexCategory.Code,
                            Description=capexCategoryInfo.CapexCategory.Description,
                            Year=capexCategoryInfo.CapexCategory.Year,
                            ParentCode=capexCategoryInfo.Parent.Code,
                            Position=capexCategoryInfo.CapexCategory.Position,
                            ReviewCode=capexCategoryInfo.CapexCategory.ReviewCode,
                            Active=capexCategoryInfo.CapexCategory.Active,
                            Name = capexCategoryInfo.CapexCategory.Name,
                            ParentName = capexCategoryInfo.Parent != null ? capexCategoryInfo.Parent.Name : ""
                        });
                }

                return capexCategory.ToArray();
            });
        }

        #endregion

        #region  CapexCost Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CapexCost UpdateCapexCost( CapexCost  capexCost)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICapexCostRepository  capexCostRepository = _DataRepositoryFactory.GetDataRepository<ICapexCostRepository>();

                 CapexCost updatedEntity = null;

                if (capexCost. CapexCostId == 0)
                    updatedEntity =  capexCostRepository.Add(capexCost);
                else
                    updatedEntity =  capexCostRepository.Update(capexCost);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCapexCost(int  capexCostId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICapexCostRepository  capexCostRepository = _DataRepositoryFactory.GetDataRepository<ICapexCostRepository>();

                 capexCostRepository.Remove( capexCostId);
            });
        }

        public  CapexCost GetCapexCost(int  capexCostId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICapexCostRepository  capexCostRepository = _DataRepositoryFactory.GetDataRepository<ICapexCostRepository>();

                 CapexCost  capexCostEntity =  capexCostRepository.Get( capexCostId);
                if (capexCostEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format(" CapexCost with ID of {0} is not in database",  capexCostId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return  capexCostEntity;
            });
        }

        public  CapexCost[] GetAllCapexCosts()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICapexCostRepository  capexCostRepository = _DataRepositoryFactory.GetDataRepository<ICapexCostRepository>();

                IEnumerable<CapexCost>  capexCost =  capexCostRepository.Get().ToArray();

                return  capexCost.ToArray();
            });
        }
        public CapexCostData[] GetCapexCosts(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICapexCostRepository capexCategoryRepository = _DataRepositoryFactory.GetDataRepository<ICapexCostRepository>();

                List<CapexCostData> capexCategory = new List<CapexCostData>();
                IEnumerable<CapexCostInfo> capexCategoryInfos = capexCategoryRepository.GetCapexCosts(year, reviewCode).ToArray();

                foreach (var capexCategoryInfo in capexCategoryInfos)
                {
                    capexCategory.Add(
                        new CapexCostData
                        {
                            CapexCostId = capexCategoryInfo.CapexCost.EntityId,                           
                            Year = capexCategoryInfo.CapexCost.Year,
                            ReviewCode = capexCategoryInfo.CapexCost.ReviewCode,
                            Cost = capexCategoryInfo.CapexCost.Cost,
                            CenterType = capexCategoryInfo.CapexCost.CenterType,
                            CurrencyCode = capexCategoryInfo.CapexCost.CurrencyCode,
                            CurrencyName = capexCategoryInfo.Currency.Name,
                            DefintionCode = capexCategoryInfo.TeamDefinition.Code,
                            DefintionName = capexCategoryInfo.TeamDefinition.Name,
                            ItemCode = capexCategoryInfo.CapexItem.Code,
                            ItemName = capexCategoryInfo.CapexItem.Name,
                            MisCode = capexCategoryInfo.Team.Code,
                            MisName = capexCategoryInfo.Team.Name,
                            Active = capexCategoryInfo.CapexCost.Active                
                        });
                }

                return capexCategory.ToArray();
            });
        }

        #endregion

        #region  CapexEntry Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CapexEntry UpdateCapexEntry(CapexEntry capexCost)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICapexEntryRepository capexCostRepository = _DataRepositoryFactory.GetDataRepository<ICapexEntryRepository>();

                CapexEntry updatedEntity = null;

                if (capexCost.CapexEntryId == 0)
                    updatedEntity = capexCostRepository.Add(capexCost);
                else
                    updatedEntity = capexCostRepository.Update(capexCost);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCapexEntry(int capexCostId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICapexEntryRepository capexCostRepository = _DataRepositoryFactory.GetDataRepository<ICapexEntryRepository>();

                capexCostRepository.Remove(capexCostId);
            });
        }

        public CapexEntry GetCapexEntry(int capexCostId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICapexEntryRepository capexCostRepository = _DataRepositoryFactory.GetDataRepository<ICapexEntryRepository>();

                CapexEntry capexCostEntity = capexCostRepository.Get(capexCostId);
                if (capexCostEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format(" CapexEntry with ID of {0} is not in database", capexCostId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return capexCostEntity;
            });
        }

        public CapexEntry[] GetAllCapexEntrys()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICapexEntryRepository capexCostRepository = _DataRepositoryFactory.GetDataRepository<ICapexEntryRepository>();

                IEnumerable<CapexEntry> capexCost = capexCostRepository.Get().ToArray();

                return capexCost.ToArray();
            });
        }

        public CapexEntryData[] GetCapexEntries(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICapexEntryRepository capexEntryRepository = _DataRepositoryFactory.GetDataRepository<ICapexEntryRepository>();

                List<CapexEntryData> capexEntry = new List<CapexEntryData>();
                IEnumerable<CapexEntryInfo> capexEntryInfos = capexEntryRepository.GetCapexEntries(year, reviewCode).ToArray();

                foreach (var capexCategoryInfo in capexEntryInfos)
                {
                    capexEntry.Add(
                        new CapexEntryData
                        {
                            CapexEntryId = capexCategoryInfo.CapexEntry.EntityId,
                            Year = capexCategoryInfo.CapexEntry.Year,
                            ReviewCode = capexCategoryInfo.CapexEntry.ReviewCode,
                            CenterType = capexCategoryInfo.CapexEntry.CenterType,
                            CurrencyCode = capexCategoryInfo.CapexEntry.CurrencyCode,
                            CurrencyName = capexCategoryInfo.Currency.Name,
                            DefintionCode = capexCategoryInfo.TeamDefinition.Code,
                            DefintionName = capexCategoryInfo.TeamDefinition.Name,
                            ItemCode = capexCategoryInfo.CapexItem.Code,
                            ItemName = capexCategoryInfo.CapexItem.Name,
                            MisCode = capexCategoryInfo.Team.Code,
                            MisName = capexCategoryInfo.Team.Name,
                            Active = capexCategoryInfo.CapexEntry.Active
                        });
                }

                return capexEntry.ToArray();
            });
        }
        #endregion

        #region  CapexItem Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CapexItem UpdateCapexItem(CapexItem capexCost)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICapexItemRepository capexCostRepository = _DataRepositoryFactory.GetDataRepository<ICapexItemRepository>();

                CapexItem updatedEntity = null;

                if (capexCost.CapexItemId == 0)
                    updatedEntity = capexCostRepository.Add(capexCost);
                else
                    updatedEntity = capexCostRepository.Update(capexCost);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCapexItem(int capexCostId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICapexItemRepository capexCostRepository = _DataRepositoryFactory.GetDataRepository<ICapexItemRepository>();

                capexCostRepository.Remove(capexCostId);
            });
        }

        public CapexItem GetCapexItem(int capexCostId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICapexItemRepository capexCostRepository = _DataRepositoryFactory.GetDataRepository<ICapexItemRepository>();

                CapexItem capexCostEntity = capexCostRepository.Get(capexCostId);
                if (capexCostEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format(" CapexItem with ID of {0} is not in database", capexCostId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return capexCostEntity;
            });
        }

        public CapexItem[] GetAllCapexItems()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICapexItemRepository capexCostRepository = _DataRepositoryFactory.GetDataRepository<ICapexItemRepository>();

                IEnumerable<CapexItem> capexCost = capexCostRepository.Get().ToArray();

                return capexCost.ToArray();
            });
        }

        public CapexItemData[] GetCapexItems(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICapexItemRepository capexItemRepository = _DataRepositoryFactory.GetDataRepository<ICapexItemRepository>();

                List<CapexItemData> capexItem = new List<CapexItemData>();
                IEnumerable<CapexItemInfo> capexItemInfos = capexItemRepository.GetCapexItems(year, reviewCode).ToArray();

                foreach (var capexItemInfo in capexItemInfos)
                {
                    capexItem.Add(
                        new CapexItemData
                        {
                            CapexItemId = capexItemInfo.CapexItem.EntityId,
                            Year = capexItemInfo.CapexItem.Year,
                            ReviewCode = capexItemInfo.CapexItem.ReviewCode,
                            Budgetable=capexItemInfo.CapexItem.Budgetable,
                            CategoryCode = capexItemInfo.CapexItem.CategoryCode,
                            CategoryName = capexItemInfo.CapexCategory.Name,
                            Code = capexItemInfo.CapexItem.Code,
                            Cost = capexItemInfo.CapexItem.Cost,
                             Name = capexItemInfo.CapexItem.Name,
                             Position = capexItemInfo.CapexItem.Position,
                           //  ReviewName = capexItemInfo.CapexCategory.Code,
                            Active = capexItemInfo.CapexItem.Active
                        });
                }

                return capexItem.ToArray();
            });
        }

        #endregion

        #region  DepreciationRate Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public DepreciationRate UpdateDepreciationRate(DepreciationRate depreciationRate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IDepreciationRateRepository depreciationRateRepository = _DataRepositoryFactory.GetDataRepository<IDepreciationRateRepository>();

                DepreciationRate updatedEntity = null;

                if (depreciationRate.DepreciationRateId == 0)
                    updatedEntity = depreciationRateRepository.Add(depreciationRate);
                else
                    updatedEntity = depreciationRateRepository.Update(depreciationRate);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteDepreciationRate(int depreciationRateId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IDepreciationRateRepository depreciationRateRepository = _DataRepositoryFactory.GetDataRepository<IDepreciationRateRepository>();

                depreciationRateRepository.Remove(depreciationRateId);
            });
        }

        public DepreciationRate GetDepreciationRate(int depreciationRateId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IDepreciationRateRepository depreciationRateRepository = _DataRepositoryFactory.GetDataRepository<IDepreciationRateRepository>();

                DepreciationRate depreciationRateEntity = depreciationRateRepository.Get(depreciationRateId);
                if (depreciationRateEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format(" DepreciationRate with ID of {0} is not in database", depreciationRateId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return depreciationRateEntity;
            });
        }

        public DepreciationRate[] GetAllDepreciationRates()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IDepreciationRateRepository depreciationRateRepository = _DataRepositoryFactory.GetDataRepository<IDepreciationRateRepository>();

                IEnumerable<DepreciationRate> depreciationRate = depreciationRateRepository.Get().ToArray();

                return depreciationRate.ToArray();
            });
        }

        public DepreciationRateData[] GetDepreciationRates(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IDepreciationRateRepository depreciationRateRepository = _DataRepositoryFactory.GetDataRepository<IDepreciationRateRepository>();

                List<DepreciationRateData> depreciationRate = new List<DepreciationRateData>();
                IEnumerable<DepreciationRateInfo> depreciationRateInfos = depreciationRateRepository.GetDepreciationRates(year, reviewCode).ToArray();

                foreach (var depreciationRateInfo in depreciationRateInfos)
                {
                    depreciationRate.Add(
                        new DepreciationRateData
                        {
                            DepreciationRateId = depreciationRateInfo.DepreciationRate.EntityId,
                            Year = depreciationRateInfo.DepreciationRate.Year,
                            ReviewCode = depreciationRateInfo.DepreciationRate.ReviewCode,                             
                            CategoryCode = depreciationRateInfo.DepreciationRate.CategoryCode,
                            CategoryName = depreciationRateInfo.CapexCategory.Name,                                                 
                            Active = depreciationRateInfo.DepreciationRate.Active
                        });
                }

                return depreciationRate.ToArray();
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
