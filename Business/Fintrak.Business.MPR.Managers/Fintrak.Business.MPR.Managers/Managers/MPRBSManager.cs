using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
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
using Fintrak.Shared.MPR.Framework;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.Common.Utils;
using Fintrak.Shared.SystemCore.Entities;
using System.Data.SqlClient;
using Fintrak.Shared.Core.Framework;

using systemCoreFramework = Fintrak.Shared.SystemCore.Framework;
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;
using Fintrak.Data.Core.Contracts;

namespace Fintrak.Business.MPR.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class MPRBSManager : ManagerBase, IMPRBSService
    {
        public MPRBSManager()
        {
        }

        public MPRBSManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }
        /// <summary>
        /// </summary>
        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        const string SOLUTION_NAME = "FIN_MPR";
        const string SOLUTION_ALIAS = "MPR";
        const string MODULE_NAME = "FIN_MPR_BALANCE_SHEET";
        const string MODULE_ALIAS = "Balance Sheet";

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

                        var bs = new systemCoreEntities.Menu()
                        {
                            Name = "BALANCE_SHEET",
                            Alias = "Balance Sheet",
                            Action = "",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "balance_sheet_image",
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

                        bs = menuRepository.Add(bs);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = bs.EntityId,
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
                              Name = "BALANCESHEET_CAPTION",
                              Alias = "Captions",
                              Action = "BALANCESHEET_CAPTION",
                              ActionUrl = "mpr-bscaption-list",
                              Image = null,
                              ImageUrl = "action_image",
                              ModuleId = module.EntityId,
                              ParentId = bs.EntityId,
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
                            Name = "MPR_PRODUCT",
                            Alias = "Products",
                            Action = "MPR_PRODUCT",
                            ActionUrl = "mpr-mprproduct-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = bs.EntityId,
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
                            Name = "MPR_BSGLMAPPING",
                            Alias = "GL Mappings",
                            Action = "MPR_BSGLMAPPING",
                            ActionUrl = "mpr-bsglmapping-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = bs.EntityId,
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
                            Name = "NON_PRODUCT_MAPPING",
                            Alias = "Non Product Mapping",
                            Action = "NON_PRODUCT_MAPPING",
                            ActionUrl = "mpr-nonproductmap-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = bs.EntityId,
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
                            Name = "NON_PRODUCT_RATE",
                            Alias = "Non Product Rates",
                            Action = "NON_PRODUCT_RATE",
                            ActionUrl = "mpr-nonproductrate-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = bs.EntityId,
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
                            Name = "PRODUCT_MIS",
                            Alias = "Product MIS",
                            Action = "PRODUCT_MIS",
                            ActionUrl = "mpr-productmis-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = bs.EntityId,
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
                            Name = "BALANCESHEET_THRESHOLD",
                            Alias = "Balancesheet Thresholds",
                            Action = "BALANCESHEET_THRESHOLD",
                            ActionUrl = "mpr-balancesheetthreshold-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = bs.EntityId,
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
                            Name = "PRODUCT_TRANSFER_PRICING",
                            Alias = "Product Transfer Pricing",
                            Action = "PRODUCT_TRANSFER_PRICING",
                            ActionUrl = "mpr-transferpricing-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = bs.EntityId,
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
                            Name = "ACCOUNT_TRANSFER_PRICE",
                            Alias = "Account Transfer Price",
                            Action = "ACCOUNT_TRANSFER_PRICE",
                            ActionUrl = "mpr-accounttransferprice-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = bs.EntityId,
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
                            Name = "GENERAL_TRANSFER_PRICE",
                            Alias = "General Transfer Price",
                            Action = "GENERAL_TRANSFER_PRICE",
                            ActionUrl = "mpr-generaltransferprice-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = bs.EntityId,
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

                        //
                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "BALANCESHEET_REPORT",
                            Alias = "BalanceSheet Report",
                            Action = "BALANCESHEET_REPORT",
                            ActionUrl = "mpr-mprbalancesheet-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = bs.EntityId,
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
                            Name = "BALANCESHEET_ADJUSTMENT",
                            Alias = "Balance Sheet Adjustment",
                            Action = "BALANCESHEET_ADJUSTMENT",
                            ActionUrl = "mpr-mprbalancesheetadjustment-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = bs.EntityId,
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
                            Name = "UNMAPPED_PRODUCT",
                            Alias = "Un-Mapped Product",
                            Action = "UNMAPPED_PRODUCT",
                            ActionUrl = "mpr-unmappedproduct-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = bs.EntityId,
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

                        //

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "BALANCESHEET_BUDGET",
                            Alias = "BalanceSheet Budget",
                            Action = "BALANCESHEET_BUDGET",
                            ActionUrl = "mpr-balancesheetbudget-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = bs.EntityId,
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
                            Name = "BS_EXEMPTION",
                            Alias = "BS Exemption",
                            Action = "BS_EXEMPTION",
                            ActionUrl = "mpr-bsexemption-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = bs.EntityId,
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
                            Name = "MEMO_ACCOUNT_MAP",
                            Alias = "Memo Account Map",
                            Action = "MEMO_ACCOUNT_MAP",
                            ActionUrl = "mpr-memoaccountmap-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = bs.EntityId,
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
                            Name = "MEMO_PRODUCT_MAP",
                            Alias = "Memo Product Map",
                            Action = "MEMO_PRODUCT_MAP",
                            ActionUrl = "mpr-memoproductmap-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = bs.EntityId,
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

        #region BSCaption operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public BSCaption UpdateBSCaption(BSCaption bsCaption)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSCaptionRepository bsCaptionRepository = _DataRepositoryFactory.GetDataRepository<IBSCaptionRepository>();

                BSCaption updatedEntity = null;

                if (bsCaption.CaptionId == 0)
                    updatedEntity = bsCaptionRepository.Add(bsCaption);
                else
                    updatedEntity = bsCaptionRepository.Update(bsCaption);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBSCaption(int bsCaptionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSCaptionRepository bsCaptionRepository = _DataRepositoryFactory.GetDataRepository<IBSCaptionRepository>();

                bsCaptionRepository.Remove(bsCaptionId);
            });
        }

        public BSCaption GetBSCaption(int bsCaptionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSCaptionRepository bsCaptionRepository = _DataRepositoryFactory.GetDataRepository<IBSCaptionRepository>();

                BSCaption bsCaptionEntity = bsCaptionRepository.Get(bsCaptionId);
                if (bsCaptionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BSCaption with ID of {0} is not in database", bsCaptionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return bsCaptionEntity;
            });
        }

        public BSCaptionData[] GetAllBSCaptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSCaptionRepository bsCaptionRepository = _DataRepositoryFactory.GetDataRepository<IBSCaptionRepository>();


                List<BSCaptionData> bsCaption = new List<BSCaptionData>();
                IEnumerable<BSCaptionInfo> bsCaptionInfos = bsCaptionRepository.GetBSCaptions().OrderBy(c => c.BSCaption.CaptionName).ToArray();

                foreach (var bsCaptionInfo in bsCaptionInfos)
                {
                    bsCaption.Add(new BSCaptionData()
                    {
                        CaptionId = bsCaptionInfo.BSCaption.EntityId,
                        CaptionCode = bsCaptionInfo.BSCaption.CaptionCode,
                        CaptionName = bsCaptionInfo.BSCaption.CaptionName,
                        Position = bsCaptionInfo.BSCaption.Position,
                        Category = bsCaptionInfo.BSCaption.Category,
                        ModuleOwnerType = bsCaptionInfo.BSCaption.ModuleOwnerType,
                        ModuleName = bsCaptionInfo.BSCaption.ModuleOwnerType.ToString(),
                        CategoryName = bsCaptionInfo.BSCaption.Category.ToString(),
                        CurrencyType = bsCaptionInfo.BSCaption.CurrencyType,
                        CurrencyTypeName = bsCaptionInfo.BSCaption.CurrencyType.ToString(),
                        BalanceSheetType = bsCaptionInfo.BSCaption.BalanceSheetType,
                        BalanceSheetTypeName = bsCaptionInfo.BSCaption.BalanceSheetType.ToString(),
                        PLCaption = bsCaptionInfo.PLCaption != null ? bsCaptionInfo.PLCaption.Code : string.Empty,
                        //PLCaption = bsCaptionInfo.PLCaption.Code,
                        PLCaptionName = bsCaptionInfo.PLCaption != null ? bsCaptionInfo.PLCaption.Name : string.Empty,
                        Active = bsCaptionInfo.BSCaption.Active,
                        ParentId = bsCaptionInfo.Parent != null ? bsCaptionInfo.Parent.EntityId : 0,
                        //ParentName = ''
                    });
                }

                return bsCaption.ToArray();
            });
        }

        public BSCaptionData[] GetAllMPRBSCaptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSCaptionRepository bsCaptionRepository = _DataRepositoryFactory.GetDataRepository<IBSCaptionRepository>();




                List<BSCaptionData> bsCaption = new List<BSCaptionData>();
                IEnumerable<BSCaptionInfo> bsCaptionInfos = bsCaptionRepository.GetBSCaptions().Where(s => s.BSCaption.ModuleOwnerType == ModuleOwnerType.MPR).OrderBy(c => c.BSCaption.CaptionName).ToArray();

                foreach (var bsCaptionInfo in bsCaptionInfos)
                {
                    bsCaption.Add(new BSCaptionData()
                    {
                        CaptionId = bsCaptionInfo.BSCaption.EntityId,
                        CaptionCode = bsCaptionInfo.BSCaption.CaptionCode,
                        CaptionName = bsCaptionInfo.BSCaption.CaptionName,
                        Position = bsCaptionInfo.BSCaption.Position,
                        Category = bsCaptionInfo.BSCaption.Category,
                        ModuleOwnerType = bsCaptionInfo.BSCaption.ModuleOwnerType,
                        ModuleName = bsCaptionInfo.BSCaption.ModuleOwnerType.ToString(),
                        CategoryName = bsCaptionInfo.BSCaption.Category.ToString(),
                        CurrencyType = bsCaptionInfo.BSCaption.CurrencyType,
                        CurrencyTypeName = bsCaptionInfo.BSCaption.CurrencyType.ToString(),
                        BalanceSheetType = bsCaptionInfo.BSCaption.BalanceSheetType,
                        BalanceSheetTypeName = bsCaptionInfo.BSCaption.BalanceSheetType.ToString(),
                        PLCaption = bsCaptionInfo.PLCaption != null ? bsCaptionInfo.PLCaption.Code : string.Empty,
                        //PLCaption = bsCaptionInfo.PLCaption.Code,
                        PLCaptionName = bsCaptionInfo.PLCaption != null ? bsCaptionInfo.PLCaption.Name : string.Empty,
                        Active = bsCaptionInfo.BSCaption.Active,
                        ParentId = bsCaptionInfo.Parent != null ? bsCaptionInfo.Parent.EntityId : 0,
                        //ParentName = ''
                    });
                }

                return bsCaption.ToArray();
            });
        }

        public BSCaptionData[] GetAllBudgetBSCaptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSCaptionRepository bsCaptionRepository = _DataRepositoryFactory.GetDataRepository<IBSCaptionRepository>();


                List<BSCaptionData> bsCaption = new List<BSCaptionData>();
                IEnumerable<BSCaptionInfo> bsCaptionInfos = bsCaptionRepository.GetBSCaptions().Where(s => s.BSCaption.ModuleOwnerType == ModuleOwnerType.Budget).OrderBy(c => c.BSCaption.CaptionName).ToArray();

                foreach (var bsCaptionInfo in bsCaptionInfos)
                {
                    bsCaption.Add(new BSCaptionData()
                    {
                        CaptionId = bsCaptionInfo.BSCaption.EntityId,
                        CaptionCode = bsCaptionInfo.BSCaption.CaptionCode,
                        CaptionName = bsCaptionInfo.BSCaption.CaptionName,
                        Position = bsCaptionInfo.BSCaption.Position,
                        Category = bsCaptionInfo.BSCaption.Category,
                        ModuleOwnerType = bsCaptionInfo.BSCaption.ModuleOwnerType,
                        ModuleName = bsCaptionInfo.BSCaption.ModuleOwnerType.ToString(),
                        CategoryName = bsCaptionInfo.BSCaption.Category.ToString(),
                        CurrencyType = bsCaptionInfo.BSCaption.CurrencyType,
                        CurrencyTypeName = bsCaptionInfo.BSCaption.CurrencyType.ToString(),
                        BalanceSheetType = bsCaptionInfo.BSCaption.BalanceSheetType,
                        BalanceSheetTypeName = bsCaptionInfo.BSCaption.BalanceSheetType.ToString(),
                        PLCaption = bsCaptionInfo.PLCaption != null ? bsCaptionInfo.PLCaption.Code : string.Empty,
                        //PLCaption = bsCaptionInfo.PLCaption.Code,
                        PLCaptionName = bsCaptionInfo.PLCaption != null ? bsCaptionInfo.PLCaption.Name : string.Empty,
                        Active = bsCaptionInfo.BSCaption.Active,
                        ParentId = bsCaptionInfo.Parent != null ? bsCaptionInfo.Parent.EntityId : 0,
                        //ParentName = ''
                    });
                }

                return bsCaption.ToArray();
            });
        }

        public BSCaptionData[] GetAllMPRBSCaptionsByCaptionName(string CaptionName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSCaptionRepository bsCaptionRepository = _DataRepositoryFactory.GetDataRepository<IBSCaptionRepository>();




                List<BSCaptionData> bsCaption = new List<BSCaptionData>();
                IEnumerable<BSCaptionInfo> bsCaptionInfos = bsCaptionRepository.GetBSCaptions().Where(s => (s.BSCaption.ModuleOwnerType == ModuleOwnerType.MPR) && s.BSCaption.CaptionName == CaptionName).OrderBy(c => c.BSCaption.CaptionName).ToArray();

                foreach (var bsCaptionInfo in bsCaptionInfos)
                {
                    bsCaption.Add(new BSCaptionData()
                    {
                        CaptionId = bsCaptionInfo.BSCaption.EntityId,
                        CaptionCode = bsCaptionInfo.BSCaption.CaptionCode,
                        CaptionName = bsCaptionInfo.BSCaption.CaptionName,
                        Position = bsCaptionInfo.BSCaption.Position,
                        Category = bsCaptionInfo.BSCaption.Category,
                        ModuleOwnerType = bsCaptionInfo.BSCaption.ModuleOwnerType,
                        ModuleName = bsCaptionInfo.BSCaption.ModuleOwnerType.ToString(),
                        CategoryName = bsCaptionInfo.BSCaption.Category.ToString(),
                        CurrencyType = bsCaptionInfo.BSCaption.CurrencyType,
                        CurrencyTypeName = bsCaptionInfo.BSCaption.CurrencyType.ToString(),
                        BalanceSheetType = bsCaptionInfo.BSCaption.BalanceSheetType,
                        BalanceSheetTypeName = bsCaptionInfo.BSCaption.BalanceSheetType.ToString(),
                        PLCaption = bsCaptionInfo.PLCaption != null ? bsCaptionInfo.PLCaption.Code : string.Empty,
                        //PLCaption = bsCaptionInfo.PLCaption.Code,
                        PLCaptionName = bsCaptionInfo.PLCaption != null ? bsCaptionInfo.PLCaption.Name : string.Empty,
                        Active = bsCaptionInfo.BSCaption.Active,
                        ParentId = bsCaptionInfo.Parent != null ? bsCaptionInfo.Parent.EntityId : 0,
                        //ParentName = ''
                    });
                }

                return bsCaption.ToArray();
            });
        }

        public BSCaptionData[] GetAllBudgetBSCaptionsByCaptionName(string CaptionName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSCaptionRepository bsCaptionRepository = _DataRepositoryFactory.GetDataRepository<IBSCaptionRepository>();


                List<BSCaptionData> bsCaption = new List<BSCaptionData>();
                IEnumerable<BSCaptionInfo> bsCaptionInfos = bsCaptionRepository.GetBSCaptions().Where(s => (s.BSCaption.ModuleOwnerType == ModuleOwnerType.Budget) && s.BSCaption.CaptionName == CaptionName).OrderBy(c => c.BSCaption.CaptionName).ToArray();

                foreach (var bsCaptionInfo in bsCaptionInfos)
                {
                    bsCaption.Add(new BSCaptionData()
                    {
                        CaptionId = bsCaptionInfo.BSCaption.EntityId,
                        CaptionCode = bsCaptionInfo.BSCaption.CaptionCode,
                        CaptionName = bsCaptionInfo.BSCaption.CaptionName,
                        Position = bsCaptionInfo.BSCaption.Position,
                        Category = bsCaptionInfo.BSCaption.Category,
                        ModuleOwnerType = bsCaptionInfo.BSCaption.ModuleOwnerType,
                        ModuleName = bsCaptionInfo.BSCaption.ModuleOwnerType.ToString(),
                        CategoryName = bsCaptionInfo.BSCaption.Category.ToString(),
                        CurrencyType = bsCaptionInfo.BSCaption.CurrencyType,
                        CurrencyTypeName = bsCaptionInfo.BSCaption.CurrencyType.ToString(),
                        BalanceSheetType = bsCaptionInfo.BSCaption.BalanceSheetType,
                        BalanceSheetTypeName = bsCaptionInfo.BSCaption.BalanceSheetType.ToString(),
                        PLCaption = bsCaptionInfo.PLCaption != null ? bsCaptionInfo.PLCaption.Code : string.Empty,
                        //PLCaption = bsCaptionInfo.PLCaption.Code,
                        PLCaptionName = bsCaptionInfo.PLCaption != null ? bsCaptionInfo.PLCaption.Name : string.Empty,
                        Active = bsCaptionInfo.BSCaption.Active,
                        ParentId = bsCaptionInfo.Parent != null ? bsCaptionInfo.Parent.EntityId : 0,
                        //ParentName = ''
                    });
                }

                return bsCaption.ToArray();
            });
        }

        public BSCaption[] GetBSCaptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSCaptionRepository bsCaptionRepository = _DataRepositoryFactory.GetDataRepository<IBSCaptionRepository>();

                BSCaption[] bsCaptionEntity = bsCaptionRepository.Get().Where(c => c.ModuleOwnerType == ModuleOwnerType.MPR && c.Active == true).ToArray();

                if (bsCaptionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BSCaption Record Not Found In the Database"));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return bsCaptionEntity;
            });
        }

        #endregion

        #region MPRProduct operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MPRProduct UpdateMPRProduct(MPRProduct mprProduct)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRProductRepository mprProductRepository = _DataRepositoryFactory.GetDataRepository<IMPRProductRepository>();

                MPRProduct updatedEntity = null;

                if (mprProduct.ProductId == 0)
                    updatedEntity = mprProductRepository.Add(mprProduct);
                else
                    updatedEntity = mprProductRepository.Update(mprProduct);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMPRProduct(int productId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRProductRepository mprProductRepository = _DataRepositoryFactory.GetDataRepository<IMPRProductRepository>();

                mprProductRepository.Remove(productId);
            });
        }

        public MPRProduct GetMPRProduct(int productId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRProductRepository mprProductRepository = _DataRepositoryFactory.GetDataRepository<IMPRProductRepository>();

                MPRProduct mprProductEntity = mprProductRepository.Get(productId);
                if (mprProductEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MPRProduct with ID of {0} is not in database", productId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return mprProductEntity;
            });
        }

        public MPRProductData[] GetAllMPRProducts()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRProductRepository mprProductRepository = _DataRepositoryFactory.GetDataRepository<IMPRProductRepository>();


                List<MPRProductData> mprProduct = new List<MPRProductData>();
                IEnumerable<MPRProductInfo> mprProductInfos = mprProductRepository.GetMPRProducts().ToArray();

                foreach (var mprProductInfo in mprProductInfos)
                {
                    mprProduct.Add(
                        new MPRProductData
                        {
                            ProductId = mprProductInfo.MPRProduct.EntityId,
                            CaptionCode = mprProductInfo.BSCaption.CaptionCode,
                            CaptionName = mprProductInfo.BSCaption.CaptionName,
                            Category = mprProductInfo.BSCaption.Category,
                            CategoryName = mprProductInfo.BSCaption.Category.ToString(),
                            CurrencyType = mprProductInfo.BSCaption.CurrencyType,
                            CurrencyTypeName = mprProductInfo.BSCaption.CurrencyType.ToString(),
                            ModuleOwnerType = mprProductInfo.MPRProduct.ModuleOwnerType,
                            ModuleName = mprProductInfo.MPRProduct.ModuleOwnerType.ToString(),
                            ProductCode = mprProductInfo.MPRProduct.ProductCode,
                            ProductName = mprProductInfo.Product.Name,
                            LongDescription = mprProductInfo.Product.Name + " - " + mprProductInfo.BSCaption.CaptionName,
                            VolumeGL = mprProductInfo.MPRProduct.VolumeGL,
                            InterestGL = mprProductInfo.MPRProduct.InterestGL,
                            Budgetable = mprProductInfo.MPRProduct.Budgetable,
                            IsNotional = mprProductInfo.MPRProduct.IsNotional,
                            Rate = mprProductInfo.MPRProduct.Rate,
                            Active = mprProductInfo.MPRProduct.Active
                        });
                }

                return mprProduct.ToArray();
            });
        }

        public MPRProductData[] GetAllMPRProductsByProductCode(string productCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRProductRepository mprProductRepository = _DataRepositoryFactory.GetDataRepository<IMPRProductRepository>();


                List<MPRProductData> mprProduct = new List<MPRProductData>();
                IEnumerable<MPRProductInfo> mprProductInfos = mprProductRepository.GetMPRProducts(productCode).ToArray();

                foreach (var mprProductInfo in mprProductInfos)
                {
                    mprProduct.Add(
                        new MPRProductData
                        {
                            ProductId = mprProductInfo.MPRProduct.EntityId,
                            CaptionCode = mprProductInfo.BSCaption.CaptionCode,
                            CaptionName = mprProductInfo.BSCaption.CaptionName,
                            Category = mprProductInfo.BSCaption.Category,
                            CategoryName = mprProductInfo.BSCaption.Category.ToString(),
                            CurrencyType = mprProductInfo.BSCaption.CurrencyType,
                            CurrencyTypeName = mprProductInfo.BSCaption.CurrencyType.ToString(),
                            ModuleOwnerType = mprProductInfo.MPRProduct.ModuleOwnerType,
                            ModuleName = mprProductInfo.MPRProduct.ModuleOwnerType.ToString(),
                            ProductCode = mprProductInfo.MPRProduct.ProductCode,
                            ProductName = mprProductInfo.Product.Name,
                            LongDescription = mprProductInfo.Product.Name + " - " + mprProductInfo.BSCaption.CaptionName,
                            VolumeGL = mprProductInfo.MPRProduct.VolumeGL,
                            InterestGL = mprProductInfo.MPRProduct.InterestGL,
                            Budgetable = mprProductInfo.MPRProduct.Budgetable,
                            IsNotional = mprProductInfo.MPRProduct.IsNotional,
                            Rate = mprProductInfo.MPRProduct.Rate,
                            Active = mprProductInfo.MPRProduct.Active
                        });
                }

                return mprProduct.ToArray();
            });
        }

        public MPRProductData[] GetMPRProductByType(BalanceSheetType type)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRProductRepository mprProductRepository = _DataRepositoryFactory.GetDataRepository<IMPRProductRepository>();


                List<MPRProductData> mprProduct = new List<MPRProductData>();
                IEnumerable<MPRProductInfo> mprProductInfos = mprProductRepository.GetMPRProducts().Where(c => c.BSCaption.BalanceSheetType == type).ToArray();

                foreach (var mprProductInfo in mprProductInfos)
                {
                    mprProduct.Add(
                        new MPRProductData
                        {
                            ProductId = mprProductInfo.MPRProduct.EntityId,
                            CaptionCode = mprProductInfo.BSCaption.CaptionCode,
                            CaptionName = mprProductInfo.BSCaption.CaptionName,
                            Category = mprProductInfo.BSCaption.Category,
                            CategoryName = mprProductInfo.BSCaption.Category.ToString(),
                            CurrencyType = mprProductInfo.BSCaption.CurrencyType,
                            CurrencyTypeName = mprProductInfo.BSCaption.CurrencyType.ToString(),
                            ProductCode = mprProductInfo.MPRProduct.ProductCode,
                            ModuleOwnerType = mprProductInfo.MPRProduct.ModuleOwnerType,
                            ModuleName = mprProductInfo.MPRProduct.ModuleOwnerType.ToString(),
                            ProductName = mprProductInfo.Product.Name,
                            LongDescription = mprProductInfo.Product.Name + " - " + mprProductInfo.BSCaption.CaptionName,
                            VolumeGL = mprProductInfo.MPRProduct.VolumeGL,
                            InterestGL = mprProductInfo.MPRProduct.InterestGL,
                            Budgetable = mprProductInfo.MPRProduct.Budgetable,
                            IsNotional = mprProductInfo.MPRProduct.IsNotional,
                            Rate = mprProductInfo.MPRProduct.Rate,
                            Active = mprProductInfo.MPRProduct.Active
                        });
                }

                return mprProduct.ToArray();
            });
        }

        public MPRProductData[] GetMPRProductByNotional(bool notional)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRProductRepository mprProductRepository = _DataRepositoryFactory.GetDataRepository<IMPRProductRepository>();


                List<MPRProductData> mprProduct = new List<MPRProductData>();
                IEnumerable<MPRProductInfo> mprProductInfos = mprProductRepository.GetMPRProducts().Where(c => c.MPRProduct.IsNotional == notional).ToArray();

                foreach (var mprProductInfo in mprProductInfos)
                {
                    mprProduct.Add(
                        new MPRProductData
                        {
                            ProductId = mprProductInfo.MPRProduct.EntityId,
                            CaptionCode = mprProductInfo.BSCaption.CaptionCode,
                            CaptionName = mprProductInfo.BSCaption.CaptionName,
                            Category = mprProductInfo.BSCaption.Category,
                            CategoryName = mprProductInfo.BSCaption.Category.ToString(),
                            CurrencyType = mprProductInfo.BSCaption.CurrencyType,
                            CurrencyTypeName = mprProductInfo.BSCaption.CurrencyType.ToString(),
                            ProductCode = mprProductInfo.MPRProduct.ProductCode,
                            ModuleOwnerType = mprProductInfo.MPRProduct.ModuleOwnerType,
                            ModuleName = mprProductInfo.MPRProduct.ModuleOwnerType.ToString(),
                            ProductName = mprProductInfo.Product.Name,
                            LongDescription = mprProductInfo.Product.Name + " - " + mprProductInfo.BSCaption.CaptionName,
                            VolumeGL = mprProductInfo.MPRProduct.VolumeGL,
                            InterestGL = mprProductInfo.MPRProduct.InterestGL,
                            Budgetable = mprProductInfo.MPRProduct.Budgetable,
                            IsNotional = mprProductInfo.MPRProduct.IsNotional,
                            Rate = mprProductInfo.MPRProduct.Rate,
                            Active = mprProductInfo.MPRProduct.Active
                        });
                }

                return mprProduct.ToArray();
            });
        }

        public KeyValueData[] GetUnMappedProducts()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                var results = new List<KeyValueData>();

                var connectionString = GetDataConnection();

                using (var con = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand("spp_mpr_bs_getunmappedproduct", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    con.Open();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var data = new KeyValueData();

                        if (reader["ProductCode"] != DBNull.Value)
                            data.Key = reader["ProductCode"].ToString();

                        if (reader["Name"] != DBNull.Value)
                            data.Value = reader["Name"].ToString();

                        if (reader["CurrencyType"] != DBNull.Value)
                            data.Description = reader["CurrencyType"].ToString();

                        results.Add(data);
                    }

                    con.Close();
                }

                return results.ToArray();
            });
        }

        #endregion

        #region NonProductMap operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public NonProductMap UpdateNonProductMap(NonProductMap nonProductMap)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                INonProductMapRepository nonProductMapRepository = _DataRepositoryFactory.GetDataRepository<INonProductMapRepository>();

                NonProductMap updatedEntity = null;

                if (nonProductMap.NonProductMapId == 0)
                    updatedEntity = nonProductMapRepository.Add(nonProductMap);
                else
                    updatedEntity = nonProductMapRepository.Update(nonProductMap);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteNonProductMap(int nonProductMapId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                INonProductMapRepository nonProductMapRepository = _DataRepositoryFactory.GetDataRepository<INonProductMapRepository>();

                nonProductMapRepository.Remove(nonProductMapId);
            });
        }

        public NonProductMap GetNonProductMap(int nonProductMapId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                INonProductMapRepository nonProductMapRepository = _DataRepositoryFactory.GetDataRepository<INonProductMapRepository>();

                NonProductMap nonProductMapEntity = nonProductMapRepository.Get(nonProductMapId);
                if (nonProductMapEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("NonProductMap with ID of {0} is not in database", nonProductMapId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return nonProductMapEntity;
            });
        }

        public NonProductMapData[] GetAllNonProductMaps()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                INonProductMapRepository nonProductMapRepository = _DataRepositoryFactory.GetDataRepository<INonProductMapRepository>();


                List<NonProductMapData> nonProductMap = new List<NonProductMapData>();
                IEnumerable<NonProductMapInfo> nonProductMapInfos = nonProductMapRepository.GetNonProductMaps().ToArray();

                foreach (var nonProductMapInfo in nonProductMapInfos)
                {
                    nonProductMap.Add(
                        new NonProductMapData
                        {
                            NonProductMapId = nonProductMapInfo.NonProductMap.EntityId,
                            NonProductCode = nonProductMapInfo.NonProductMap.NonProductCode,
                            NonProductName = nonProductMapInfo.NonProduct != null ? nonProductMapInfo.NonProduct.Name : string.Empty,
                            CaptionCode = nonProductMapInfo.BSCaption.CaptionCode,
                            CaptionName = nonProductMapInfo.BSCaption != null ? nonProductMapInfo.BSCaption.CaptionName : string.Empty,
                            Category = nonProductMapInfo.BSCaption.Category,
                            CategoryName = nonProductMapInfo.BSCaption.Category.ToString(),
                            CurrencyType = nonProductMapInfo.BSCaption.CurrencyType,
                            CurrencyTypeName = nonProductMapInfo.BSCaption.CurrencyType.ToString(),
                            //ProductCode = nonProductMapInfo.NonProductMap.ProductCode,
                            //ProductName = nonProductMapInfo.Product.Name,
                            Active = nonProductMapInfo.NonProductMap.Active
                        });
                }

                return nonProductMap.ToArray();
            });
        }

        #endregion

        #region NonProductRate operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public NonProductRate UpdateNonProductRate(NonProductRate nonProductRate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                INonProductRateRepository nonProductRateRepository = _DataRepositoryFactory.GetDataRepository<INonProductRateRepository>();

                NonProductRate updatedEntity = null;

                if (nonProductRate.NonProductRateId == 0)
                {
                    nonProductRate.Year = GetSetup().Year;
                    updatedEntity = nonProductRateRepository.Add(nonProductRate);
                }
                else
                    updatedEntity = nonProductRateRepository.Update(nonProductRate);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteNonProductRate(int nonProductRateId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                INonProductRateRepository nonProductRateRepository = _DataRepositoryFactory.GetDataRepository<INonProductRateRepository>();

                nonProductRateRepository.Remove(nonProductRateId);
            });
        }

        public NonProductRate GetNonProductRate(int nonProductRateId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                INonProductRateRepository nonProductRateRepository = _DataRepositoryFactory.GetDataRepository<INonProductRateRepository>();

                NonProductRate nonProductRateEntity = nonProductRateRepository.Get(nonProductRateId);
                if (nonProductRateEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("NonProductRate with ID of {0} is not in database", nonProductRateId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return nonProductRateEntity;
            });
        }

        public NonProductRate[] GetAllNonProductRates()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                INonProductRateRepository nonProductRateRepository = _DataRepositoryFactory.GetDataRepository<INonProductRateRepository>();

                IEnumerable<NonProductRate> nonProductRates = nonProductRateRepository.Get().ToArray();

                return nonProductRates.ToArray();
            });
        }


        #endregion

        #region ProductMIS operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ProductMIS UpdateProductMIS(ProductMIS productMIS)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductMISRepository productMISRepository = _DataRepositoryFactory.GetDataRepository<IProductMISRepository>();

                ProductMIS updatedEntity = null;

                if (productMIS.ProductMISId == 0)
                {
                    productMIS.Year = GetSetup().Year;
                    updatedEntity = productMISRepository.Add(productMIS);
                }
                else
                    updatedEntity = productMISRepository.Update(productMIS);
                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteProductMIS(int productMISId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductMISRepository productMISRepository = _DataRepositoryFactory.GetDataRepository<IProductMISRepository>();

                productMISRepository.Remove(productMISId);
            });
        }

        public ProductMIS GetProductMIS(int productMISId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductMISRepository productMISRepository = _DataRepositoryFactory.GetDataRepository<IProductMISRepository>();

                ProductMIS productMISEntity = productMISRepository.Get(productMISId);
                if (productMISEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ProductMIS with ID of {0} is not in database", productMISId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return productMISEntity;
            });
        }

        public ProductMISData[] GetAllProductMISs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductMISRepository productMISRepository = _DataRepositoryFactory.GetDataRepository<IProductMISRepository>();

                var setup = GetSetup();

                List<ProductMISData> productMIS = new List<ProductMISData>();
                IEnumerable<ProductMISInfo> productMISInfos = productMISRepository.GetProductMIS(setup.Year).ToArray();

                foreach (var productMISInfo in productMISInfos)
                {
                    productMIS.Add(
                        new ProductMISData
                        {
                            ProductMISId = productMISInfo.ProductMIS.EntityId,
                            ProductCode = productMISInfo.ProductMIS.ProductCode,
                            ProductName = productMISInfo.Product.Name,
                            CaptionCode = productMISInfo.ProductMIS.CaptionCode,
                            CaptionName = productMISInfo.Caption.CaptionName,
                            Category = productMISInfo.Caption.Category,
                            CategoryName = productMISInfo.Caption.Category.ToString(),
                            CurrencyType = productMISInfo.Caption.CurrencyType,
                            CurrencyTypeName = productMISInfo.Caption.CurrencyType.ToString(),
                            AccountOfficerCode = productMISInfo.AccountOfficer != null ? productMISInfo.AccountOfficer.Code : string.Empty,
                            AccountOfficerName = productMISInfo.AccountOfficer != null ? productMISInfo.AccountOfficer.Name : string.Empty,
                            TeamCode = productMISInfo.Team.Code,
                            TeamName = productMISInfo.Team.Name,
                            TeamDefinitionCode = productMISInfo.TeamDefinition.Code,
                            AccountOfficerDefinitionCode = productMISInfo.AccountOfficerDefinition != null ? productMISInfo.AccountOfficerDefinition.Code : string.Empty,
                            Active = productMISInfo.ProductMIS.Active
                        });
                }

                return productMIS.ToArray();
            });
        }

        #endregion

        #region BalanceSheetThreshold operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public BalanceSheetThreshold UpdateBalanceSheetThreshold(BalanceSheetThreshold balanceSheetThreshold)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBalanceSheetThresholdRepository balanceSheetThresholdRepository = _DataRepositoryFactory.GetDataRepository<IBalanceSheetThresholdRepository>();

                BalanceSheetThreshold updatedEntity = null;

                if (balanceSheetThreshold.BalanceSheetThresholdId == 0)
                    updatedEntity = balanceSheetThresholdRepository.Add(balanceSheetThreshold);
                else
                    updatedEntity = balanceSheetThresholdRepository.Update(balanceSheetThreshold);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBalanceSheetThreshold(int balanceSheetThresholdId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBalanceSheetThresholdRepository balanceSheetThresholdRepository = _DataRepositoryFactory.GetDataRepository<IBalanceSheetThresholdRepository>();

                balanceSheetThresholdRepository.Remove(balanceSheetThresholdId);
            });
        }

        public BalanceSheetThreshold GetBalanceSheetThreshold(int balanceSheetThresholdId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBalanceSheetThresholdRepository balanceSheetThresholdRepository = _DataRepositoryFactory.GetDataRepository<IBalanceSheetThresholdRepository>();

                BalanceSheetThreshold balanceSheetThresholdEntity = balanceSheetThresholdRepository.Get(balanceSheetThresholdId);
                if (balanceSheetThresholdEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BalanceSheetThreshold with ID of {0} is not in database", balanceSheetThresholdId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return balanceSheetThresholdEntity;
            });
        }

        public BalanceSheetThresholdData[] GetAllBalanceSheetThresholds()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBalanceSheetThresholdRepository balanceSheetThresholdRepository = _DataRepositoryFactory.GetDataRepository<IBalanceSheetThresholdRepository>();


                List<BalanceSheetThresholdData> balanceSheetThreshold = new List<BalanceSheetThresholdData>();
                IEnumerable<BalanceSheetThresholdInfo> balanceSheetThresholdInfos = balanceSheetThresholdRepository.GetBalanceSheetThresholds().ToArray();

                foreach (var balanceSheetThresholdInfo in balanceSheetThresholdInfos)
                {
                    balanceSheetThreshold.Add(
                        new BalanceSheetThresholdData
                        {
                            BalanceSheetThresholdId = balanceSheetThresholdInfo.BalanceSheetThreshold.EntityId,
                            CaptionCode = balanceSheetThresholdInfo.BSCaption.CaptionCode,
                            CaptionName = balanceSheetThresholdInfo.BSCaption.CaptionName,
                            ProductCode = balanceSheetThresholdInfo.BalanceSheetThreshold.ProductCode,
                            ProductName = balanceSheetThresholdInfo.Product.Name,
                            Category = balanceSheetThresholdInfo.BSCaption.Category,
                            CategoryName = balanceSheetThresholdInfo.BSCaption.Category.ToString(),
                            CurrencyType = balanceSheetThresholdInfo.BSCaption.CurrencyType,
                            CurrencyTypeName = balanceSheetThresholdInfo.BSCaption.CurrencyType.ToString(),
                            Rate = balanceSheetThresholdInfo.BalanceSheetThreshold.Rate,
                            Active = balanceSheetThresholdInfo.BalanceSheetThreshold.Active
                        });
                }

                return balanceSheetThreshold.ToArray();
            });
        }

        #endregion

        #region MPRBalanceSheetAdjustment operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MPRBalanceSheetAdjustment UpdateBalanceSheetAdjustment(MPRBalanceSheetAdjustment mprBalanceSheetAdjustment)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRBalanceSheetAdjustmentRepository mprBalanceSheetAdjustmentRepository = _DataRepositoryFactory.GetDataRepository<IMPRBalanceSheetAdjustmentRepository>();

                MPRBalanceSheetAdjustment updatedEntity = null;

                if (mprBalanceSheetAdjustment.BalancesheetAdjustmentId == 0)
                    updatedEntity = mprBalanceSheetAdjustmentRepository.Add(mprBalanceSheetAdjustment);
                else
                    updatedEntity = mprBalanceSheetAdjustmentRepository.Update(mprBalanceSheetAdjustment);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBalanceSheetAdjustment(int mprBalanceSheetAdjustmentId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRBalanceSheetAdjustmentRepository mprBalanceSheetAdjustmentRepository = _DataRepositoryFactory.GetDataRepository<IMPRBalanceSheetAdjustmentRepository>();

                mprBalanceSheetAdjustmentRepository.Remove(mprBalanceSheetAdjustmentId);
            });
        }

        public MPRBalanceSheetAdjustment GetBalanceSheetAdjustment(int mprBalanceSheetAdjustmentId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRBalanceSheetAdjustmentRepository mprBalanceSheetAdjustmentRepository = _DataRepositoryFactory.GetDataRepository<IMPRBalanceSheetAdjustmentRepository>();

                MPRBalanceSheetAdjustment mprBalanceSheetAdjustmentEntity = mprBalanceSheetAdjustmentRepository.Get(mprBalanceSheetAdjustmentId);
                if (mprBalanceSheetAdjustmentEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MPRBalanceSheetAdjustment with ID of {0} is not in database", mprBalanceSheetAdjustmentId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return mprBalanceSheetAdjustmentEntity;
            });
        }

        public MPRBalanceSheetAdjustment[] GetAllBalanceSheetAdjustments()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRBalanceSheetAdjustmentRepository mprBalancesheetAdjustmentRepository = _DataRepositoryFactory.GetDataRepository<IMPRBalanceSheetAdjustmentRepository>();

                IEnumerable<MPRBalanceSheetAdjustment> mprBalancesheetAdjustments = mprBalancesheetAdjustmentRepository.Get().ToArray();

                return mprBalancesheetAdjustments.ToArray();
            });
        }

        public MPRBalanceSheetAdjustment[] GetBalanceSheetAdjustments(string searchType, string searchValue, int number)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRBalanceSheetAdjustmentRepository mprBalancesheetAdjustmentRepository = _DataRepositoryFactory.GetDataRepository<IMPRBalanceSheetAdjustmentRepository>();
                List<MPRBalanceSheetAdjustment> BalanceSheetAdjustments = mprBalancesheetAdjustmentRepository.GetBalanceSheetAdjustmentBySearch(searchType, searchValue, number);


                return BalanceSheetAdjustments.ToArray();
            });
        }

        public MPRBalanceSheetAdjustment[] GetCodebyUsers(string userName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRBalanceSheetAdjustmentRepository mprBalancesheetAdjustmentRepository = _DataRepositoryFactory.GetDataRepository<IMPRBalanceSheetAdjustmentRepository>();

                List<MPRBalanceSheetAdjustment> BalanceSheetAdjustments = mprBalancesheetAdjustmentRepository.GetCodebyUsers(userName);

                return BalanceSheetAdjustments.ToArray();
            });
        }

        public MPRBalanceSheetAdjustment[] GetBalanceSheetAdjustmentByCode(string code, string userName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRBalanceSheetAdjustmentRepository mprBalancesheetAdjustmentRepository = _DataRepositoryFactory.GetDataRepository<IMPRBalanceSheetAdjustmentRepository>();
                List<MPRBalanceSheetAdjustment> BalanceSheetAdjustments = mprBalancesheetAdjustmentRepository.GetBalanceSheetAdjustmentByCode(code, userName);


                return BalanceSheetAdjustments.ToArray();
            });
        }

        public void DeleteMPRBalanceSheetAdjustment(string code, string userName)
        {

            var connectionString = GetDataConnection();

            int status = 0;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_deletemprbalancesheetadjustmentbyCode", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "code",
                    Value = code,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "username",
                    Value = userName,
                });

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }


        #endregion

        #region MPRBalanceSheet operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MPRBalanceSheet UpdateBalanceSheet(MPRBalanceSheet mprBalanceSheet)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRBalanceSheetRepository mprBalanceSheetRepository = _DataRepositoryFactory.GetDataRepository<IMPRBalanceSheetRepository>();

                MPRBalanceSheet updatedEntity = null;

                if (mprBalanceSheet.BalanceSheetId == 0)
                    updatedEntity = mprBalanceSheetRepository.Add(mprBalanceSheet);
                else
                    updatedEntity = mprBalanceSheetRepository.Update(mprBalanceSheet);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBalanceSheet(int mprBalanceSheetId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRBalanceSheetRepository mprBalanceSheetRepository = _DataRepositoryFactory.GetDataRepository<IMPRBalanceSheetRepository>();

                mprBalanceSheetRepository.Remove(mprBalanceSheetId);
            });
        }

        public MPRBalanceSheet GetBalanceSheet(int mprBalanceSheetId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRBalanceSheetRepository mprBalanceSheetRepository = _DataRepositoryFactory.GetDataRepository<IMPRBalanceSheetRepository>();

                MPRBalanceSheet mprBalanceSheetEntity = mprBalanceSheetRepository.Get(mprBalanceSheetId);
                if (mprBalanceSheetEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MPRBalanceSheet with ID of {0} is not in database", mprBalanceSheetId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return mprBalanceSheetEntity;
            });
        }

        public MPRBalanceSheet[] GetmprBalanceSheets(int number)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISolutionRepository solutionRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRepository>();
                var solution = solutionRepository.Get().Where(c => c.Name == SOLUTION_NAME).FirstOrDefault();

                ISolutionRunDateRepository runDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();
                var runDate = runDateRepository.Get().Where(c => c.SolutionId == solution.SolutionId).FirstOrDefault();

                IMPRBalanceSheetRepository mprBalancesheetRepository = _DataRepositoryFactory.GetDataRepository<IMPRBalanceSheetRepository>();

                IEnumerable<MPRBalanceSheet> mprBalancesheets = mprBalancesheetRepository.GetBalanceSheets(runDate.RunDate, number).ToArray();

                return mprBalancesheets.ToArray();
            });
        }

        //public MPRBalanceSheet[] GetAllBalanceSheets(int number, DateTime fromDate, DateTime toDate)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        ISolutionRepository solutionRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRepository>();
        //        var solution = solutionRepository.Get().Where(c => c.Name == SOLUTION_NAME).FirstOrDefault();

        //        ISolutionRunDateRepository runDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();
        //        var runDate = runDateRepository.Get().Where(c => c.SolutionId == solution.SolutionId).FirstOrDefault();

        //        IMPRBalanceSheetRepository mprBalancesheetRepository = _DataRepositoryFactory.GetDataRepository<IMPRBalanceSheetRepository>();

        //        IEnumerable<MPRBalanceSheet> mprBalancesheets = mprBalancesheetRepository.GetAllBalanceSheets(runDate.RunDate, number, fromDate, toDate).ToArray();

        //        return mprBalancesheets.ToArray();
        //    });
        //}


        public MPRBalanceSheet[] GetRunDate()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRBalanceSheetRepository mprBalancesheetRepository = _DataRepositoryFactory.GetDataRepository<IMPRBalanceSheetRepository>();
                IEnumerable<MPRBalanceSheet> balanceSheets = mprBalancesheetRepository.GetRunDate();


                return balanceSheets.ToArray();
            });
        }
        public MPRBalanceSheet[] GetAllBalanceSheets(string searchType, string searchValue, int number, DateTime fromDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRBalanceSheetRepository mprBalancesheetRepository = _DataRepositoryFactory.GetDataRepository<IMPRBalanceSheetRepository>();
                IEnumerable<MPRBalanceSheet> balanceSheets = mprBalancesheetRepository.GetAllBalanceSheets(searchType, searchValue, number, fromDate);


                return balanceSheets.ToArray();
            });
        }

        public MPRBalanceSheet[] GetAllMPRBalanceSheets()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRBalanceSheetRepository balanceSheetRepository = _DataRepositoryFactory.GetDataRepository<IMPRBalanceSheetRepository>();

                IEnumerable<MPRBalanceSheet> balanceSheets = balanceSheetRepository.Get().ToArray();

                return balanceSheets.ToArray();
            });
        }


        #endregion

        #region BalanceSheetBudget operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public BalanceSheetBudget UpdateBalanceSheetBudget(BalanceSheetBudget balanceSheetBudget)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBalanceSheetBudgetRepository balanceSheetBudgetRepository = _DataRepositoryFactory.GetDataRepository<IBalanceSheetBudgetRepository>();

                BalanceSheetBudget updatedEntity = null;

                if (balanceSheetBudget.BudgetId == 0)
                    updatedEntity = balanceSheetBudgetRepository.Add(balanceSheetBudget);
                else
                    updatedEntity = balanceSheetBudgetRepository.Update(balanceSheetBudget);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBalanceSheetBudget(int balanceSheetBudgetId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBalanceSheetBudgetRepository balanceSheetBudgetRepository = _DataRepositoryFactory.GetDataRepository<IBalanceSheetBudgetRepository>();

                balanceSheetBudgetRepository.Remove(balanceSheetBudgetId);
            });
        }

        public BalanceSheetBudget GetBalanceSheetBudget(int balanceSheetBudgetId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBalanceSheetBudgetRepository balanceSheetBudgetRepository = _DataRepositoryFactory.GetDataRepository<IBalanceSheetBudgetRepository>();

                BalanceSheetBudget balanceSheetBudgetEntity = balanceSheetBudgetRepository.Get(balanceSheetBudgetId);
                if (balanceSheetBudgetEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BalanceSheetBudget with ID of {0} is not in database", balanceSheetBudgetId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return balanceSheetBudgetEntity;
            });
        }

        public BalanceSheetBudget[] GetAllBalanceSheetBudgets(string year)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBalanceSheetBudgetRepository mprBalancesheetBudgetRepository = _DataRepositoryFactory.GetDataRepository<IBalanceSheetBudgetRepository>();

                IEnumerable<BalanceSheetBudget> mprBalancesheetBudgets = mprBalancesheetBudgetRepository.GetBalanceSheetBudgets(year).ToArray();

                return mprBalancesheetBudgets.ToArray();
            });
        }

        public BalanceSheetBudget[] GetBalanceSheetBudgets(string searchValue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBalanceSheetBudgetRepository mprBalancesheetBudgetRepository = _DataRepositoryFactory.GetDataRepository<IBalanceSheetBudgetRepository>();
                List<BalanceSheetBudget> mprBalancesheetBudgets = mprBalancesheetBudgetRepository.GetBalanceSheetBySearch(searchValue);


                return mprBalancesheetBudgets.ToArray();
            });
        }

        public void DeleteBSBSelectedIds(string selectedIds)
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
                    Value = "BSBUDGET"
                });
                cmd.CommandTimeout = 0;

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }

        }

        #endregion

        #region BalanceSheetBudgetOfficer operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public BalanceSheetBudgetOfficer UpdateBalanceSheetBudgetOfficer(BalanceSheetBudgetOfficer balanceSheetBudgetOfficer)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBalanceSheetBudgetOfficerRepository balanceSheetBudgetOffRepository = _DataRepositoryFactory.GetDataRepository<IBalanceSheetBudgetOfficerRepository>();

                BalanceSheetBudgetOfficer updatedEntity = null;

                if (balanceSheetBudgetOfficer.BudgetId == 0)
                    updatedEntity = balanceSheetBudgetOffRepository.Add(balanceSheetBudgetOfficer);
                else
                    updatedEntity = balanceSheetBudgetOffRepository.Update(balanceSheetBudgetOfficer);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBalanceSheetBudgetOfficer(int balanceSheetBudgetOffId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBalanceSheetBudgetOfficerRepository balanceSheetBudgetOffRepository = _DataRepositoryFactory.GetDataRepository<IBalanceSheetBudgetOfficerRepository>();

                balanceSheetBudgetOffRepository.Remove(balanceSheetBudgetOffId);
            });
        }

        public BalanceSheetBudgetOfficer GetBalanceSheetBudgetOfficer(int balanceSheetBudgetOffId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBalanceSheetBudgetOfficerRepository balanceSheetBudgetOffRepository = _DataRepositoryFactory.GetDataRepository<IBalanceSheetBudgetOfficerRepository>();

                BalanceSheetBudgetOfficer balanceSheetBudgetOffEntity = balanceSheetBudgetOffRepository.Get(balanceSheetBudgetOffId);
                if (balanceSheetBudgetOffEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BalanceSheetBudgetOfficer with ID of {0} is not in database", balanceSheetBudgetOffId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return balanceSheetBudgetOffEntity;
            });
        }

        public BalanceSheetBudgetOfficer[] GetAllBalanceSheetBudgetOfficers(string year)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBalanceSheetBudgetOfficerRepository mprBalancesheetBudgetRepository = _DataRepositoryFactory.GetDataRepository<IBalanceSheetBudgetOfficerRepository>();

                IEnumerable<BalanceSheetBudgetOfficer> mprBalancesheetBudgetOfficers = mprBalancesheetBudgetRepository.GetBalanceSheetBudgetOfficers(year).ToArray();

                return mprBalancesheetBudgetOfficers.ToArray();
            });
        }

        public BalanceSheetBudgetOfficer[] GetBalanceSheetBudgetOfficers(string searchValue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBalanceSheetBudgetOfficerRepository mprBalancesheetBudgetRepository = _DataRepositoryFactory.GetDataRepository<IBalanceSheetBudgetOfficerRepository>();
                List<BalanceSheetBudgetOfficer> mprBalancesheetBudgetOfficers = mprBalancesheetBudgetRepository.GetBalanceSheetBySearch(searchValue);


                return mprBalancesheetBudgetOfficers.ToArray();
            });
        }


        #endregion

        #region BSGLMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public BSGLMapping UpdateBSGLMapping(BSGLMapping bsGLMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSGLMappingRepository bsGLMappingRepository = _DataRepositoryFactory.GetDataRepository<IBSGLMappingRepository>();

                BSGLMapping updatedEntity = null;

                if (bsGLMapping.BSGLMappingId == 0)
                    updatedEntity = bsGLMappingRepository.Add(bsGLMapping);
                else
                    updatedEntity = bsGLMappingRepository.Update(bsGLMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBSGLMapping(int bsGLMappingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSGLMappingRepository bsGLMappingRepository = _DataRepositoryFactory.GetDataRepository<IBSGLMappingRepository>();

                bsGLMappingRepository.Remove(bsGLMappingId);
            });
        }

        public BSGLMapping GetBSGLMapping(int bsGLMappingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSGLMappingRepository bsGLMappingRepository = _DataRepositoryFactory.GetDataRepository<IBSGLMappingRepository>();

                BSGLMapping bsGLMappingEntity = bsGLMappingRepository.Get(bsGLMappingId);
                if (bsGLMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BSGLMapping with ID of {0} is not in database", bsGLMappingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return bsGLMappingEntity;
            });
        }

        public BSGLMappingData[] GetAllBSGLMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSGLMappingRepository bsGLMappingRepository = _DataRepositoryFactory.GetDataRepository<IBSGLMappingRepository>();


                List<BSGLMappingData> bsGLMapping = new List<BSGLMappingData>();
                IEnumerable<BSGLMappingInfo> bsGLMappingInfos = bsGLMappingRepository.GetBSGLMappings().ToArray();

                foreach (var bsGLMappingInfo in bsGLMappingInfos)
                {
                    bsGLMapping.Add(new BSGLMappingData()
                    {

                        BSGLMappingId = bsGLMappingInfo.BSGLMapping.EntityId,
                        ProductCode = bsGLMappingInfo.BSGLMapping.ProductCode,
                        ProductName = bsGLMappingInfo.Product.Name,
                        GLCode = bsGLMappingInfo.BSGLMapping.GLCode,
                        GLName = bsGLMappingInfo.GLDefinition != null ? bsGLMappingInfo.GLDefinition.Description : string.Empty,
                        CompanyCode = bsGLMappingInfo.BSGLMapping.CompanyCode,
                        Active = bsGLMappingInfo.BSGLMapping.Active

                    });
                }

                return bsGLMapping.ToArray();
            });
        }


        #endregion

        #region BSINOtherInformation operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public BSINOtherInformation UpdateBSINOtherInformation(BSINOtherInformation bSINOtherInformation)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSINOtherInformationRepository bSINOtherInformationRepository = _DataRepositoryFactory.GetDataRepository<IBSINOtherInformationRepository>();

                BSINOtherInformation updatedEntity = null;

                if (bSINOtherInformation.BSINOtherInformationId == 0)
                    updatedEntity = bSINOtherInformationRepository.Add(bSINOtherInformation);
                else
                    updatedEntity = bSINOtherInformationRepository.Update(bSINOtherInformation);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBSINOtherInformation(int bSINOtherInformationId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSINOtherInformationRepository bSINOtherInformationRepository = _DataRepositoryFactory.GetDataRepository<IBSINOtherInformationRepository>();

                bSINOtherInformationRepository.Remove(bSINOtherInformationId);
            });
        }

        public BSINOtherInformation GetBSINOtherInformation(int bSINOtherInformationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSINOtherInformationRepository bSINOtherInformationRepository = _DataRepositoryFactory.GetDataRepository<IBSINOtherInformationRepository>();

                BSINOtherInformation bSINOtherInformationEntity = bSINOtherInformationRepository.Get(bSINOtherInformationId);
                if (bSINOtherInformationEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BSINOtherInformation with ID of {0} is not in database", bSINOtherInformationId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return bSINOtherInformationEntity;
            });
        }

        public BSINOtherInformation[] GetAllBSINOtherInformations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSINOtherInformationRepository bSINOtherInformationRepository = _DataRepositoryFactory.GetDataRepository<IBSINOtherInformationRepository>();

                IEnumerable<BSINOtherInformation> bSINOtherInformations = bSINOtherInformationRepository.Get().ToArray();

                return bSINOtherInformations.ToArray();
            });
        }


        public IEnumerable<BSCaption> GetAllBsPlCaptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSCaptionRepository bsCaptionRepository = _DataRepositoryFactory.GetDataRepository<IBSCaptionRepository>();
                IPLCaptionRepository plCaptionRepository = _DataRepositoryFactory.GetDataRepository<IPLCaptionRepository>();

                int? _ParentId = 0;

                var query = (from b in bsCaptionRepository.Get() select new { CaptionId = b.CaptionId, CaptionCode = b.CaptionCode, CaptionName = b.CaptionName, Category = b.Category, CurrencyType = CurrencyType.LCY.ToString(), BalanceSheetType = BalanceSheetType.OFF.ToString(), Position = b.Position, ParentId = b.ParentId, CompanyCode = b.CompanyCode, Active = b.Active, Deleted = b.Deleted, CreatedBy = b.CreatedBy, CreatedOn = b.CreatedOn, UpdatedBy = b.UpdatedBy, UpdatedOn = b.UpdatedOn, RowVersion = b.RowVersion, ModuleOwnerType = ModuleOwnerType.MPR.ToString() })
                .Concat(from a in plCaptionRepository.Get() select new { CaptionId = a.PLCaptionId, CaptionCode = a.Code, CaptionName = a.Name, Category = a.AccountType, CurrencyType = CurrencyType.LCY.ToString(), BalanceSheetType = BalanceSheetType.OFF.ToString(), Position = a.Position, ParentId = _ParentId, CompanyCode = a.CompanyCode, Active = a.Active, Deleted = a.Deleted, CreatedBy = a.CreatedBy, CreatedOn = a.CreatedOn, UpdatedBy = a.UpdatedBy, UpdatedOn = a.UpdatedOn, RowVersion = a.RowVersion, ModuleOwnerType = ModuleOwnerType.MPR.ToString() }).Where(u => u.ModuleOwnerType == ModuleOwnerType.MPR.ToString());


                var bsCaptionEntity = from r in query
                                      select new BSCaption()
                                      {
                                          CaptionId = r.CaptionId,
                                          CaptionCode = r.CaptionCode,
                                          CaptionName = r.CaptionName,
                                          Category = r.Category,
                                          CurrencyType = CurrencyType.LCY,
                                          BalanceSheetType = BalanceSheetType.OFF,
                                          Position = r.Position,
                                          ParentId = r.ParentId,
                                          ModuleOwnerType = ModuleOwnerType.MPR,
                                          CompanyCode = r.CompanyCode,
                                          Active = r.Active,
                                          Deleted = r.Deleted,
                                          CreatedBy = r.CreatedBy,
                                          CreatedOn = r.CreatedOn,
                                          UpdatedBy = r.UpdatedBy,
                                          UpdatedOn = r.UpdatedOn,
                                          RowVersion = r.RowVersion,
                                      };


                if (bsCaptionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Data not in database"));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                return bsCaptionEntity;


            });
        }


        #endregion

        #region BSINOtherInformationTotalLine operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public BSINOtherInformationTotalLine UpdateBSINOtherInformationTotalLine(BSINOtherInformationTotalLine bSINOtherInformationTotalLine)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSINOtherInformationTotalLineRepository bSINOtherInformationTotalLineRepository = _DataRepositoryFactory.GetDataRepository<IBSINOtherInformationTotalLineRepository>();

                BSINOtherInformationTotalLine updatedEntity = null;

                if (bSINOtherInformationTotalLine.BSINOtherInformationTotalLineId == 0)
                    updatedEntity = bSINOtherInformationTotalLineRepository.Add(bSINOtherInformationTotalLine);
                else
                    updatedEntity = bSINOtherInformationTotalLineRepository.Update(bSINOtherInformationTotalLine);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBSINOtherInformationTotalLine(int bSINOtherInformationTotalLineId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSINOtherInformationTotalLineRepository bSINOtherInformationTotalLineRepository = _DataRepositoryFactory.GetDataRepository<IBSINOtherInformationTotalLineRepository>();

                bSINOtherInformationTotalLineRepository.Remove(bSINOtherInformationTotalLineId);
            });
        }

        public BSINOtherInformationTotalLine GetBSINOtherInformationTotalLine(int bSINOtherInformationTotalLineId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSINOtherInformationTotalLineRepository bSINOtherInformationTotalLineRepository = _DataRepositoryFactory.GetDataRepository<IBSINOtherInformationTotalLineRepository>();

                BSINOtherInformationTotalLine bSINOtherInformationTotalLineEntity = bSINOtherInformationTotalLineRepository.Get(bSINOtherInformationTotalLineId);
                if (bSINOtherInformationTotalLineEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BSINOtherInformationTotalLine with ID of {0} is not in database", bSINOtherInformationTotalLineId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return bSINOtherInformationTotalLineEntity;
            });
        }

        public BSINOtherInformationTotalLine[] GetAllBSINOtherInformationTotalLines()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSINOtherInformationTotalLineRepository bSINOtherInformationTotalLineRepository = _DataRepositoryFactory.GetDataRepository<IBSINOtherInformationTotalLineRepository>();

                IEnumerable<BSINOtherInformationTotalLine> bSINOtherInformationTotalLines = bSINOtherInformationTotalLineRepository.Get().ToArray();

                return bSINOtherInformationTotalLines.ToArray();
            });
        }

        public IEnumerable<BSCaption> GetAllBsPlOtherInfoCaptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSCaptionRepository bsCaptionRepository = _DataRepositoryFactory.GetDataRepository<IBSCaptionRepository>();
                IPLCaptionRepository plCaptionRepository = _DataRepositoryFactory.GetDataRepository<IPLCaptionRepository>();
                IBSINOtherInformationRepository bsINOtherInformation = _DataRepositoryFactory.GetDataRepository<IBSINOtherInformationRepository>();

                int? _ParentId = 0;
                string _CaptionCode = "";

                var query = (from b in bsCaptionRepository.Get() select new { CaptionId = b.CaptionId, CaptionCode = b.CaptionCode, CaptionName = b.CaptionName, Category = b.Category, CurrencyType = CurrencyType.LCY.ToString(), BalanceSheetType = BalanceSheetType.OFF.ToString(), Position = b.Position, ParentId = b.ParentId, CompanyCode = b.CompanyCode, Active = b.Active, Deleted = b.Deleted, CreatedBy = b.CreatedBy, CreatedOn = b.CreatedOn, UpdatedBy = b.UpdatedBy, UpdatedOn = b.UpdatedOn, RowVersion = b.RowVersion, ModuleOwnerType = ModuleOwnerType.MPR.ToString() })
                     .Concat(from c in bsINOtherInformation.Get() select new { CaptionId = c.BSINOtherInformationId, CaptionCode = _CaptionCode, CaptionName = c.MainCaption, Category = AccountTypeEnum.View, CurrencyType = CurrencyType.LCY.ToString(), BalanceSheetType = BalanceSheetType.OFF.ToString(), Position = 0, ParentId = _ParentId, CompanyCode = _CaptionCode, Active = c.Active, Deleted = c.Deleted, CreatedBy = c.CreatedBy, CreatedOn = c.CreatedOn, UpdatedBy = c.UpdatedBy, UpdatedOn = c.UpdatedOn, RowVersion = c.RowVersion, ModuleOwnerType = ModuleOwnerType.MPR.ToString() })
                .Concat(from a in plCaptionRepository.Get() select new { CaptionId = a.PLCaptionId, CaptionCode = a.Code, CaptionName = a.Name, Category = a.AccountType, CurrencyType = CurrencyType.LCY.ToString(), BalanceSheetType = BalanceSheetType.OFF.ToString(), Position = a.Position, ParentId = _ParentId, CompanyCode = a.CompanyCode, Active = a.Active, Deleted = a.Deleted, CreatedBy = a.CreatedBy, CreatedOn = a.CreatedOn, UpdatedBy = a.UpdatedBy, UpdatedOn = a.UpdatedOn, RowVersion = a.RowVersion, ModuleOwnerType = ModuleOwnerType.MPR.ToString() }).Where(u => u.ModuleOwnerType == ModuleOwnerType.MPR.ToString());

                var bsCaptionEntity = from r in query
                                      select new BSCaption()
                                      {
                                          CaptionId = r.CaptionId,
                                          CaptionCode = r.CaptionCode,
                                          CaptionName = r.CaptionName,
                                          Category = r.Category,
                                          CurrencyType = CurrencyType.LCY,
                                          BalanceSheetType = BalanceSheetType.OFF,
                                          Position = r.Position,
                                          ParentId = r.ParentId,
                                          ModuleOwnerType = ModuleOwnerType.MPR,
                                          CompanyCode = r.CompanyCode,
                                          Active = r.Active,
                                          Deleted = r.Deleted,
                                          CreatedBy = r.CreatedBy,
                                          CreatedOn = r.CreatedOn,
                                          UpdatedBy = r.UpdatedBy,
                                          UpdatedOn = r.UpdatedOn,
                                          RowVersion = r.RowVersion,
                                      };


                if (bsCaptionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Data not in database"));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                return bsCaptionEntity;


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
