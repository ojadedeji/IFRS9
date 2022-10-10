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
using entities = Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;
using data = Fintrak.Data.Budget.Contracts;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Budget.Framework.Enums;
using BudgetCoreEntities = Fintrak.Shared.Budget.Entities;
using BudgetCoreData = Fintrak.Data.Budget.Contracts;


namespace Fintrak.Business.Budget.Managers
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
                systemCoreData.IModuleRepository moduleRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IModuleRepository>();
                IMenuRepository menuRepository = _DataRepositoryFactory.GetDataRepository<IMenuRepository>();
                IRoleRepository roleRepository = _DataRepositoryFactory.GetDataRepository<IRoleRepository>();
                IMenuRoleRepository menuRoleRepository = _DataRepositoryFactory.GetDataRepository<IMenuRoleRepository>();

                using (TransactionScope ts = new TransactionScope())
                {
                    //check if module has been installed
                    systemCoreEntities.Module module = moduleRepository.Get().Where(c => c.Name == CoreModuleDefinition.MODULE_NAME).FirstOrDefault();

                    var register = false;
                    if (module == null)
                        register = true;
                    else
                        register = module.CanUpdate;

                    if (register)
                    {
                        //check if module category exit
                        Solution solution = solutionRepository.Get().Where(c => c.Name == CoreModuleDefinition.SOLUTION_NAME).FirstOrDefault();
                        if (solution == null)
                        {
                            //register solution
                            solution = new Solution()
                            {
                                Name = CoreModuleDefinition.SOLUTION_NAME,
                                Alias = CoreModuleDefinition.SOLUTION_ALIAS,
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
                                Name = CoreModuleDefinition.MODULE_NAME,
                                Alias = CoreModuleDefinition.MODULE_ALIAS,
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

                        foreach (var role in CoreModuleDefinition.GetRoles())
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

                        foreach (var menu in CoreModuleDefinition.GetMenus())
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

                        foreach (var menuRole in CoreModuleDefinition.GetMenuRoles())
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

        #region BudgetingLevel operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public BudgetingLevel UpdateBudgetingLevel(BudgetingLevel budgetingLevel)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR,BudgetModuleDefinition.GROUP_BUSINESS};
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IBudgetingLevelRepository budgetingLevelRepository = _DataRepositoryFactory.GetDataRepository<IBudgetingLevelRepository>();

                var setting = GetGeneralSetting();

                BudgetingLevel updatedEntity = null;

                if (budgetingLevel.BudgetingLevelId == 0)
                {
                    budgetingLevel.ReviewCode = setting.ReviewCode;
                    budgetingLevel.Year = setting.Year;
                    updatedEntity = budgetingLevelRepository.Add(budgetingLevel);
                }
                else
                    updatedEntity = budgetingLevelRepository.Update(budgetingLevel);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBudgetingLevel(int budgetingLevelId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IBudgetingLevelRepository budgetingLevelRepository = _DataRepositoryFactory.GetDataRepository<IBudgetingLevelRepository>();

                budgetingLevelRepository.Remove(budgetingLevelId);
            });
        }

        public BudgetingLevel GetBudgetingLevel(int budgetingLevelId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS,BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IBudgetingLevelRepository budgetingLevelRepository = _DataRepositoryFactory.GetDataRepository<IBudgetingLevelRepository>();

                BudgetingLevel budgetingLevelEntity = budgetingLevelRepository.Get(budgetingLevelId);
                if (budgetingLevelEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BudgetingLevel with ID of {0} is not in database", budgetingLevelId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return budgetingLevelEntity;
            });
        }

        public BudgetingLevelData[] GetBudgetingLevels(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IBudgetingLevelRepository budgetingLevelRepository = _DataRepositoryFactory.GetDataRepository<IBudgetingLevelRepository>();

                var setting = GetGeneralSetting();

                if (string.IsNullOrEmpty(year) || string.IsNullOrEmpty(reviewCode))
                {
                    year = setting.Year;
                    reviewCode = setting.ReviewCode;
                }

                List<BudgetingLevelData> budgetingLevel = new List<BudgetingLevelData>();
                IEnumerable<BudgetingLevelInfo> budgetingLevelInfos = budgetingLevelRepository.GetBudgetingLevels(year,reviewCode).OrderByDescending(c => c.Module.Name).ToArray();

                foreach (var budgetingLevelInfo in budgetingLevelInfos)
                {
                    budgetingLevel.Add(new BudgetingLevelData
                        {
                            BudgetingLevelId = budgetingLevelInfo.BudgetingLevel.EntityId,
                            ModuleCode = budgetingLevelInfo.BudgetingLevel.ModuleCode,
                            ModuleName = budgetingLevelInfo.Module != null ? budgetingLevelInfo.Module.Name : string.Empty,
                            DefinitionCode = budgetingLevelInfo.BudgetingLevel.DefinitionCode,
                            DefinitionName = budgetingLevelInfo.TeamDefinition != null ? budgetingLevelInfo.TeamDefinition.Name : string.Empty,
                            Center = budgetingLevelInfo.BudgetingLevel.Center,
                            CenterName = budgetingLevelInfo.BudgetingLevel.Center.ToString(),
                            ReviewCode = budgetingLevelInfo.BudgetingLevel.ReviewCode ,
                            Year = budgetingLevelInfo.BudgetingLevel.Year
                        });
                }

                return budgetingLevel.ToArray();
            });
        }


        #endregion

        #region Currency operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Currency UpdateCurrency(Currency currency)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICurrencyRepository currencyRepository = _DataRepositoryFactory.GetDataRepository<ICurrencyRepository>();

                Currency updatedEntity = null;

                if (currency.CurrencyId == 0)
                {
                    updatedEntity = currencyRepository.Add(currency);
                }
                else
                    updatedEntity = currencyRepository.Update(currency);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCurrency(int currencyId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICurrencyRepository currencyRepository = _DataRepositoryFactory.GetDataRepository<ICurrencyRepository>();

                currencyRepository.Remove(currencyId);
            });
        }

        public Currency GetCurrency(int currencyId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICurrencyRepository currencyRepository = _DataRepositoryFactory.GetDataRepository<ICurrencyRepository>();

                Currency currencyEntity = currencyRepository.Get(currencyId);
                if (currencyEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Currency with ID of {0} is not in database", currencyId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return currencyEntity;
            });
        }

        public Currency[] GetAllCurrency()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICurrencyRepository currencyRepository = _DataRepositoryFactory.GetDataRepository<ICurrencyRepository>();

                var setting = GetGeneralSetting();

                IEnumerable<Currency> currency = currencyRepository.Get().ToArray();

                return currency.ToArray();
            });
        }


        #endregion

        #region CurrencyRate operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CurrencyRate UpdateCurrencyRate(CurrencyRate currencyRate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICurrencyRateRepository currencyRateRepository = _DataRepositoryFactory.GetDataRepository<ICurrencyRateRepository>();

                var setting = GetGeneralSetting();

                CurrencyRate updatedEntity = null;

                if (currencyRate.CurrencyRateId == 0)
                {
                    currencyRate.ReviewCode = setting.ReviewCode;
                    currencyRate.Year = setting.Year;
                    updatedEntity = currencyRateRepository.Add(currencyRate);
                }
                else
                    updatedEntity = currencyRateRepository.Update(currencyRate);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCurrencyRate(int currencyRateId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICurrencyRateRepository currencyRateRepository = _DataRepositoryFactory.GetDataRepository<ICurrencyRateRepository>();

                currencyRateRepository.Remove(currencyRateId);
            });
        }

        public CurrencyRate GetCurrencyRate(int currencyRateId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICurrencyRateRepository currencyRateRepository = _DataRepositoryFactory.GetDataRepository<ICurrencyRateRepository>();

                CurrencyRate currencyRateEntity = currencyRateRepository.Get(currencyRateId);
                if (currencyRateEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CurrencyRate with ID of {0} is not in database", currencyRateId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return currencyRateEntity;
            });
        }

        public CurrencyRate GetCurrencyRateByCode(string year, string reviewCode, RateTypeEnum rateType, string currencyCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICurrencyRateRepository currencyRateRepository = _DataRepositoryFactory.GetDataRepository<ICurrencyRateRepository>();

                var setting = GetGeneralSetting();

                var currencyRate = currencyRateRepository.GetCurrencyRate(setting.Year, setting.ReviewCode, rateType, currencyCode);

                return currencyRate;
            });
        }

        public CurrencyRate[] GetCurrencyRates(string year, string reviewCode, RateTypeEnum rateType)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ICurrencyRateRepository currencyRateRepository = _DataRepositoryFactory.GetDataRepository<ICurrencyRateRepository>();

                var setting = GetGeneralSetting();

                IEnumerable<CurrencyRate> currencyRate = currencyRateRepository.GetCurrencyRates(setting.Year,setting.ReviewCode,rateType).ToArray();

                return currencyRate.ToArray();
            });
        }


        #endregion

        #region GeneralSetting operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public GeneralSetting UpdateGeneralSetting(GeneralSetting generalSetting)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IGeneralSettingRepository generalSettingRepository = _DataRepositoryFactory.GetDataRepository<IGeneralSettingRepository>();

                GeneralSetting updatedEntity = null;

                if (generalSetting.GeneralSettingId == 0)
                {
                    updatedEntity = generalSettingRepository.Add(generalSetting);
                }
                else
                    updatedEntity = generalSettingRepository.Update(generalSetting);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteGeneralSetting(int generalSettingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IGeneralSettingRepository generalSettingRepository = _DataRepositoryFactory.GetDataRepository<IGeneralSettingRepository>();

                generalSettingRepository.Remove(generalSettingId);
            });
        }

        public GeneralSetting GetGeneralSetting(int generalSettingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IGeneralSettingRepository generalSettingRepository = _DataRepositoryFactory.GetDataRepository<IGeneralSettingRepository>();

                GeneralSetting generalSettingEntity = generalSettingRepository.Get(generalSettingId);
                if (generalSettingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("GeneralSetting with ID of {0} is not in database", generalSettingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return generalSettingEntity;
            });
        }

        public GeneralSetting GetFirstGeneralSetting()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IGeneralSettingRepository generalSettingRepository = _DataRepositoryFactory.GetDataRepository<IGeneralSettingRepository>();

                GeneralSetting generalSetting = generalSettingRepository.Get().FirstOrDefault();

                if (generalSetting == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("GeneralSetting not in database"));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return generalSetting;
            });
        }


        #endregion

        #region ModificationLevel operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ModificationLevel UpdateModificationLevel(ModificationLevel modificationLevel)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IModificationLevelRepository modificationLevelRepository = _DataRepositoryFactory.GetDataRepository<IModificationLevelRepository>();

                ModificationLevel updatedEntity = null;

                if (modificationLevel.ModificationLevelId == 0)
                {
                    updatedEntity = modificationLevelRepository.Add(modificationLevel);
                }
                else
                    updatedEntity = modificationLevelRepository.Update(modificationLevel);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteModificationLevel(int modificationLevelId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IModificationLevelRepository modificationLevelRepository = _DataRepositoryFactory.GetDataRepository<IModificationLevelRepository>();

                modificationLevelRepository.Remove(modificationLevelId);
            });
        }

        public ModificationLevel GetModificationLevel(int modificationLevelId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IModificationLevelRepository modificationLevelRepository = _DataRepositoryFactory.GetDataRepository<IModificationLevelRepository>();

                ModificationLevel modificationLevelEntity = modificationLevelRepository.Get(modificationLevelId);
                if (modificationLevelEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ModificationLevel with ID of {0} is not in database", modificationLevelId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return modificationLevelEntity;
            });
        }

        public ModificationLevelData[] GetAllModificationLevel()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IModificationLevelRepository modificationLevelRepository = _DataRepositoryFactory.GetDataRepository<IModificationLevelRepository>();

                var setting = GetGeneralSetting();

                List<ModificationLevelData> modificationLevel = new List<ModificationLevelData>();
                IEnumerable<ModificationLevelInfo> modificationLevelInfos = modificationLevelRepository.GetModificationLevels(setting.Year,setting.ReviewCode).OrderByDescending(c => c.Module.Name).ToArray();

                foreach (var modificationLevelInfo in modificationLevelInfos)
                {
                    modificationLevel.Add(new ModificationLevelData
                    {
                        ModificationLevelId = modificationLevelInfo.ModificationLevel.EntityId,
                        ModuleCode = modificationLevelInfo.ModificationLevel.ModuleCode,
                        ModuleName = modificationLevelInfo.Module != null ? modificationLevelInfo.Module.Name : string.Empty,
                        DefinitionCode = modificationLevelInfo.ModificationLevel.DefinitionCode,
                        DefinitionName = modificationLevelInfo.TeamDefinition != null ? modificationLevelInfo.TeamDefinition.Name : string.Empty,
                        Status = modificationLevelInfo.ModificationLevel.Status,
                    });
                }

                return modificationLevel.ToArray();
            });
        }


        #endregion

        #region Module operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public BudgetCoreEntities.Module UpdateModule(BudgetCoreEntities.Module module)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                BudgetCoreData.IModuleRepository moduleRepository = _DataRepositoryFactory.GetDataRepository<BudgetCoreData.IModuleRepository>();

                BudgetCoreEntities.Module updatedEntity = null;

                if (module.ModuleId == 0)
                {
                    updatedEntity = moduleRepository.Add(module);
                }
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
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                BudgetCoreData.IModuleRepository moduleRepository = _DataRepositoryFactory.GetDataRepository<BudgetCoreData.IModuleRepository>();

                moduleRepository.Remove(moduleId);
            });
        }

        public BudgetCoreEntities.Module GetModule(int moduleId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                BudgetCoreData.IModuleRepository moduleRepository = _DataRepositoryFactory.GetDataRepository<BudgetCoreData.IModuleRepository>();

                BudgetCoreEntities.Module moduleEntity = moduleRepository.Get(moduleId);
                if (moduleEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Module with ID of {0} is not in database", moduleId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return moduleEntity;
            });
        }

        public BudgetCoreEntities.Module[] GetAllModule()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                BudgetCoreData.IModuleRepository moduleRepository = _DataRepositoryFactory.GetDataRepository<BudgetCoreData.IModuleRepository>();

                var setting = GetGeneralSetting();

                IEnumerable<BudgetCoreEntities.Module> module = moduleRepository.Get().ToArray();

                return module.ToArray();
            });
        }


        #endregion

        #region Operation operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Operation UpdateOperation(Operation operation)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOperationRepository operationRepository = _DataRepositoryFactory.GetDataRepository<IOperationRepository>();

                Operation updatedEntity = null;

                if (operation.OperationId == 0)
                {
                    updatedEntity = operationRepository.Add(operation);
                }
                else
                    updatedEntity = operationRepository.Update(operation);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOperation(int operationId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOperationRepository operationRepository = _DataRepositoryFactory.GetDataRepository<IOperationRepository>();

                operationRepository.Remove(operationId);
            });
        }

        public Operation GetOperation(int operationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOperationRepository operationRepository = _DataRepositoryFactory.GetDataRepository<IOperationRepository>();

                Operation operationEntity = operationRepository.Get(operationId);
                if (operationEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Operation with ID of {0} is not in database", operationId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return operationEntity;
            });
        }

        public Operation GetOperationByCode(string code)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOperationRepository operationRepository = _DataRepositoryFactory.GetDataRepository<IOperationRepository>();

                Operation operationEntity = operationRepository.Get().Where(c=>c.Code == code).FirstOrDefault();
                if (operationEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Operation with code of {0} is not in database", code));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return operationEntity;
            });
        }

        public Operation[] GetAllOperations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOperationRepository operationRepository = _DataRepositoryFactory.GetDataRepository<IOperationRepository>();

                IEnumerable<Operation> operation = operationRepository.Get().ToArray();

                return operation.ToArray();
            });
        }


        #endregion

        #region OperationReview operationReviews

        [OperationBehavior(TransactionScopeRequired = true)]
        public OperationReview UpdateOperationReview(OperationReview operationReview)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOperationReviewRepository operationReviewRepository = _DataRepositoryFactory.GetDataRepository<IOperationReviewRepository>();

                OperationReview updatedEntity = null;

                if (operationReview.OperationReviewId == 0)
                {
                    updatedEntity = operationReviewRepository.Add(operationReview);
                }
                else
                    updatedEntity = operationReviewRepository.Update(operationReview);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOperationReview(int operationReviewId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOperationReviewRepository operationReviewRepository = _DataRepositoryFactory.GetDataRepository<IOperationReviewRepository>();

                operationReviewRepository.Remove(operationReviewId);
            });
        }

        public OperationReview GetOperationReview(int operationReviewId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOperationReviewRepository operationReviewRepository = _DataRepositoryFactory.GetDataRepository<IOperationReviewRepository>();

                OperationReview operationReviewEntity = operationReviewRepository.Get(operationReviewId);
                if (operationReviewEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OperationReview with ID of {0} is not in database", operationReviewId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return operationReviewEntity;
            });
        }

        public OperationReview[] GetOperationReviews(string operationCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IOperationReviewRepository operationReviewRepository = _DataRepositoryFactory.GetDataRepository<IOperationReviewRepository>();

                IEnumerable<OperationReview> operationReview = operationReviewRepository.GetOperationReviews(operationCode).ToArray();

                return operationReview.ToArray();
            });
        }


        //public OperationReview[] GetAllOperationReviews()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
        //        AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

        //        IOperationReviewRepository operationRepository = _DataRepositoryFactory.GetDataRepository<IOperationReviewRepository>();

        //        var setting = GetGeneralSetting();

        //        IEnumerable<OperationReview> operation = operationRepository.Get().ToArray();

        //        return operation.ToArray();
        //    });
        //}

        #endregion

        #region PolicyLevel operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PolicyLevel UpdatePolicyLevel(PolicyLevel policyLevel)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IPolicyLevelRepository policyLevelRepository = _DataRepositoryFactory.GetDataRepository<IPolicyLevelRepository>();

                var setting = GetGeneralSetting();

                PolicyLevel updatedEntity = null;

                if (policyLevel.PolicyLevelId == 0)
                {
                    policyLevel.ReviewCode = setting.ReviewCode;
                    policyLevel.Year = setting.Year;
                    updatedEntity = policyLevelRepository.Add(policyLevel);
                }
                else
                    updatedEntity = policyLevelRepository.Update(policyLevel);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePolicyLevel(int policyLevelId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IPolicyLevelRepository policyLevelRepository = _DataRepositoryFactory.GetDataRepository<IPolicyLevelRepository>();

                policyLevelRepository.Remove(policyLevelId);
            });
        }

        public PolicyLevel GetPolicyLevel(int policyLevelId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IPolicyLevelRepository policyLevelRepository = _DataRepositoryFactory.GetDataRepository<IPolicyLevelRepository>();

                PolicyLevel policyLevelEntity = policyLevelRepository.Get(policyLevelId);
                if (policyLevelEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PolicyLevel with ID of {0} is not in database", policyLevelId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return policyLevelEntity;
            });
        }

        public PolicyLevelData[] GetPolicyLevels(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IPolicyLevelRepository policyLevelRepository = _DataRepositoryFactory.GetDataRepository<IPolicyLevelRepository>();

                var setting = GetGeneralSetting();

                if (string.IsNullOrEmpty(year) || string.IsNullOrEmpty(reviewCode))
                {
                    year = setting.Year;
                    reviewCode = setting.ReviewCode;
                }

                List<PolicyLevelData> policyLevel = new List<PolicyLevelData>();
                IEnumerable<PolicyLevelInfo> policyLevelInfos = policyLevelRepository.GetPolicyLevels(year,reviewCode).OrderByDescending(c => c.Module.Name).ToArray();

                foreach (var policyLevelInfo in policyLevelInfos)
                {
                    policyLevel.Add(new PolicyLevelData
                    {
                        PolicyLevelId = policyLevelInfo.PolicyLevel.EntityId,
                        PolicyCode = policyLevelInfo.PolicyLevel.PolicyCode,
                        PolicyName = policyLevelInfo.Policy != null ? policyLevelInfo.Policy.Name : string.Empty,
                        ModuleCode = policyLevelInfo.PolicyLevel.ModuleCode,
                        ModuleName = policyLevelInfo.Module != null ? policyLevelInfo.Module.Name : string.Empty,
                        DefinitionCode = policyLevelInfo.PolicyLevel.DefinitionCode,
                        DefinitionName = policyLevelInfo.TeamDefinition != null ? policyLevelInfo.TeamDefinition.Name : string.Empty,
                        Center = policyLevelInfo.PolicyLevel.Center,
                        CenterName = policyLevelInfo.PolicyLevel.Center.ToString(),
                        ReviewCode = policyLevelInfo.PolicyLevel.ReviewCode,
                        Year = policyLevelInfo.PolicyLevel.Year
                    });
                }

                return policyLevel.ToArray();
            });
        }


        #endregion

        #region Policy operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Policy UpdatePolicy(Policy policy)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IPolicyRepository policyRepository = _DataRepositoryFactory.GetDataRepository<IPolicyRepository>();

                Policy updatedEntity = null;

                if (policy.PolicyId == 0)
                {
                    updatedEntity = policyRepository.Add(policy);
                }
                else
                    updatedEntity = policyRepository.Update(policy);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePolicy(int policyId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IPolicyRepository policyRepository = _DataRepositoryFactory.GetDataRepository<IPolicyRepository>();

                policyRepository.Remove(policyId);
            });
        }

        public Policy GetPolicy(int policyId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IPolicyRepository policyRepository = _DataRepositoryFactory.GetDataRepository<IPolicyRepository>();

                Policy policyEntity = policyRepository.Get(policyId);
                if (policyEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Policy with ID of {0} is not in database", policyId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return policyEntity;
            });
        }

        public PolicyData[] GetAllPolicy()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IPolicyRepository policyRepository = _DataRepositoryFactory.GetDataRepository<IPolicyRepository>();

                List<PolicyData> policy = new List<PolicyData>();
                IEnumerable<PolicyInfo> policyInfos = policyRepository.GetPolicies().ToArray();

                foreach (var policyInfo in policyInfos)
                {
                    policy.Add(new PolicyData
                    {
                        PolicyId = policyInfo.Policy.EntityId,
                        Code = policyInfo.Policy.Code,
                        Name = policyInfo.Policy.Name,
                        ModuleCode = policyInfo.Policy.ModuleCode,
                        ModuleName = policyInfo.Module != null ? policyInfo.Module.Name : string.Empty,
                    });
                }

                return policy.ToArray();
            });
        }


        #endregion

        #region PrimaryLock operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PrimaryLock UpdatePrimaryLock(PrimaryLock primaryLock)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IPrimaryLockRepository primaryLockRepository = _DataRepositoryFactory.GetDataRepository<IPrimaryLockRepository>();

                PrimaryLock updatedEntity = null;

                if (primaryLock.PrimaryLockId == 0)
                {
                    updatedEntity = primaryLockRepository.Add(primaryLock);
                }
                else
                    updatedEntity = primaryLockRepository.Update(primaryLock);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePrimaryLock(int primaryLockId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IPrimaryLockRepository primaryLockRepository = _DataRepositoryFactory.GetDataRepository<IPrimaryLockRepository>();

                primaryLockRepository.Remove(primaryLockId);
            });
        }

        public PrimaryLock GetPrimaryLock(int primaryLockId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IPrimaryLockRepository primaryLockRepository = _DataRepositoryFactory.GetDataRepository<IPrimaryLockRepository>();

                PrimaryLock primaryLockEntity = primaryLockRepository.Get(primaryLockId);
                if (primaryLockEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PrimaryLock with ID of {0} is not in database", primaryLockId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return primaryLockEntity;
            });
        }

        public PrimaryLockData[] GetAllPrimaryLock()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IPrimaryLockRepository primaryLockRepository = _DataRepositoryFactory.GetDataRepository<IPrimaryLockRepository>();

                var setting = GetGeneralSetting();

                List<PrimaryLockData> primaryLock = new List<PrimaryLockData>();
                IEnumerable<PrimaryLockInfo> primaryLockInfos = primaryLockRepository.GetPrimaryLocks(setting.ReviewCode, setting.Year).ToArray();

                foreach (var primaryLockInfo in primaryLockInfos)
                {
                    primaryLock.Add(new PrimaryLockData
                    {
                        PrimaryLockId = primaryLockInfo.PrimaryLock.EntityId,
                        MisCode = primaryLockInfo.PrimaryLock.MisCode,
                        DefinitionCode = primaryLockInfo.PrimaryLock.DefinitionCode,
                        DefinitionName = primaryLockInfo.TeamDefinition != null ? primaryLockInfo.TeamDefinition.Name : string.Empty,
                        Note = primaryLockInfo.PrimaryLock.Note,
                        Lock = primaryLockInfo.PrimaryLock.Lock,
                        CanOverride = primaryLockInfo.PrimaryLock.CanOverride,
                        Year = primaryLockInfo.PrimaryLock.Year
                    });
                }

                return primaryLock.ToArray();
            });
        }


        #endregion

        #region SecondaryLockLevel operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SecondaryLockLevel UpdateSecondaryLockLevel(SecondaryLockLevel secondaryLockLevel)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ISecondaryLockLevelRepository secondaryLockLevelRepository = _DataRepositoryFactory.GetDataRepository<ISecondaryLockLevelRepository>();

                SecondaryLockLevel updatedEntity = null;

                if (secondaryLockLevel.SecondaryLockLevelId == 0)
                {
                    updatedEntity = secondaryLockLevelRepository.Add(secondaryLockLevel);
                }
                else
                    updatedEntity = secondaryLockLevelRepository.Update(secondaryLockLevel);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSecondaryLockLevel(int secondaryLockLevelId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ISecondaryLockLevelRepository secondaryLockLevelRepository = _DataRepositoryFactory.GetDataRepository<ISecondaryLockLevelRepository>();

                secondaryLockLevelRepository.Remove(secondaryLockLevelId);
            });
        }

        public SecondaryLockLevel GetSecondaryLockLevel(int secondaryLockLevelId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ISecondaryLockLevelRepository secondaryLockLevelRepository = _DataRepositoryFactory.GetDataRepository<ISecondaryLockLevelRepository>();

                SecondaryLockLevel secondaryLockLevelEntity = secondaryLockLevelRepository.Get(secondaryLockLevelId);
                if (secondaryLockLevelEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SecondaryLockLevel with ID of {0} is not in database", secondaryLockLevelId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return secondaryLockLevelEntity;
            });
        }

        public SecondaryLockLevelData[] GetSecondaryLockLevels()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ISecondaryLockLevelRepository secondaryLockLevelRepository = _DataRepositoryFactory.GetDataRepository<ISecondaryLockLevelRepository>();

                var setting = GetGeneralSetting();

                List<SecondaryLockLevelData> secondaryLockLevel = new List<SecondaryLockLevelData>();
                IEnumerable<SecondaryLockLevelInfo> secondaryLockLevelInfos = secondaryLockLevelRepository.GetSecondaryLockLevels(setting.ReviewCode, setting.Year).OrderByDescending(c => c.Module.Name).ToArray();

                foreach (var secondaryLockLevelInfo in secondaryLockLevelInfos)
                {
                    secondaryLockLevel.Add(new SecondaryLockLevelData
                    {
                        SecondaryLockLevelId = secondaryLockLevelInfo.SecondaryLockLevel.EntityId,
                        ModuleCode = secondaryLockLevelInfo.SecondaryLockLevel.ModuleCode,
                        ModuleName = secondaryLockLevelInfo.Module != null ? secondaryLockLevelInfo.Module.Name : string.Empty,
                        DefinitionCode = secondaryLockLevelInfo.SecondaryLockLevel.DefinitionCode,
                        DefinitionName = secondaryLockLevelInfo.TeamDefinition != null ? secondaryLockLevelInfo.TeamDefinition.Name : string.Empty,
                    });
                }

                return secondaryLockLevel.ToArray();
            });
        }


        #endregion

        #region SecondaryLock operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SecondaryLock UpdateSecondaryLock(SecondaryLock secondaryLock)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ISecondaryLockRepository secondaryLockRepository = _DataRepositoryFactory.GetDataRepository<ISecondaryLockRepository>();

                SecondaryLock updatedEntity = null;

                if (secondaryLock.SecondaryLockId == 0)
                {
                    updatedEntity = secondaryLockRepository.Add(secondaryLock);
                }
                else
                    updatedEntity = secondaryLockRepository.Update(secondaryLock);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSecondaryLock(int secondaryLockId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ISecondaryLockRepository secondaryLockRepository = _DataRepositoryFactory.GetDataRepository<ISecondaryLockRepository>();

                secondaryLockRepository.Remove(secondaryLockId);
            });
        }

        public SecondaryLock GetSecondaryLock(int secondaryLockId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ISecondaryLockRepository secondaryLockRepository = _DataRepositoryFactory.GetDataRepository<ISecondaryLockRepository>();

                SecondaryLock secondaryLockEntity = secondaryLockRepository.Get(secondaryLockId);
                if (secondaryLockEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SecondaryLock with ID of {0} is not in database", secondaryLockId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return secondaryLockEntity;
            });
        }

        public SecondaryLockData[] GetSecondaryLocks(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS, BudgetModuleDefinition.GROUP_READONLY, BudgetModuleDefinition.GROUP_USER };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                ISecondaryLockRepository secondaryLockRepository = _DataRepositoryFactory.GetDataRepository<ISecondaryLockRepository>();

                var setting = GetGeneralSetting();

                List<SecondaryLockData> secondaryLock = new List<SecondaryLockData>();
                IEnumerable<SecondaryLockInfo> secondaryLockInfos = secondaryLockRepository.GetSecondaryLocks(reviewCode, year).ToArray();

                foreach (var secondaryLockInfo in secondaryLockInfos)
                {
                    secondaryLock.Add(new SecondaryLockData
                    {
                        SecondaryLockId = secondaryLockInfo.SecondaryLock.EntityId,
                        MisCode = secondaryLockInfo.SecondaryLock.MisCode,
                        DefinitionCode = secondaryLockInfo.SecondaryLock.DefinitionCode,
                        DefinitionName = secondaryLockInfo.TeamDefinition != null ? secondaryLockInfo.TeamDefinition.Name : string.Empty,
                        ModuleCode = secondaryLockInfo.SecondaryLock.ModuleCode,
                       // DefinitionName = secondaryLockInfo.TeamDefinition != null ? secondaryLockInfo.TeamDefinition.Name : string.Empty,
                        Note = secondaryLockInfo.SecondaryLock.Note,
                        Year = secondaryLockInfo.SecondaryLock.Year
                    });
                }

                return secondaryLock.ToArray();
            });
        }


        #endregion

        #region Helper

        private GeneralSetting GetGeneralSetting()
        {
            IGeneralSettingRepository setupRepository = _DataRepositoryFactory.GetDataRepository<IGeneralSettingRepository>();

            var setup = setupRepository.Get().FirstOrDefault();
            if (setup == null)
            {
                NotFoundException ex = new NotFoundException(string.Format("Budget setup information is not in database"));
                throw new FaultException<NotFoundException>(ex, ex.Message);
            }

            return setup;
        }

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
