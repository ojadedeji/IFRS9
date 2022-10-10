using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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

using systemCoreFramework = Fintrak.Shared.SystemCore.Framework;
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;

namespace Fintrak.Business.Basic.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]  //GetIndividualSchedulebyRefNo
    public class IFRSLoanManager : ManagerBase, IIFRSLoanService
    {
        public IFRSLoanManager()
        {
        }

        public IFRSLoanManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        const string SOLUTION_NAME = "FIN_IFRS";
        const string SOLUTION_ALIAS = "IFRS";
        const string MODULE_NAME = "FIN_IFRS_LOAN";
        const string MODULE_ALIAS = "IFRS Loans";

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


                        int menuIndex = 0;
                        var root = menuRepository.Get().Where(c => c.Alias == "Loans").FirstOrDefault();
                        //Role
                        var adminRole = roleRepository.Get().Where(c => c.Name == GROUP_ADMINISTRATOR && c.SolutionId == solution.SolutionId).FirstOrDefault();
                        var userRole = roleRepository.Get().Where(c => c.Name == GROUP_USER && c.SolutionId == solution.SolutionId).FirstOrDefault();


                        //register menu
                        var actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "LOAN_SETUP",
                            Alias = "Loan SetUp",
                            Action = "LOAN_SETUP",
                            ActionUrl = "ifrsloan-setup-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "SCHEDULE_TYPE",
                            Alias = "Schedule Types",
                            Action = "SCHEDULE_TYPE",
                            ActionUrl = "ifrsloan-scheduletype-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "IFRS_PRODUCT",
                            Alias = "IFRS Products",
                            Action = "IFRS_PRODUCT",
                            ActionUrl = "ifrsloan-product-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "IFRS_CREDIT_RISK_RATING",
                            Alias = "Credit Risk Rating",
                            Action = "IFRS_CREDIT_RISK_RATING",
                            ActionUrl = "ifrsloan-creditriskrating-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "IFRS_COLLATERAL_CATEGORY",
                            Alias = "Collateral Categories",
                            Action = "IFRS_COLLATERAL_CATEGORY",
                            ActionUrl = "ifrsloan-collateralcategory-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "IFRS_COLLATERAL_INFORMATION",
                            Alias = "Collateral Information",
                            Action = "IFRS_COLLATERAL_INFORMATION",
                            ActionUrl = "ifrsloan-collateralinformation-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "IFRS_COLLATERAL_REALIZATION_PERIOD",
                            Alias = "Collateral Realization Period",
                            Action = "IFRS_COLLATERAL_REALIZATION_PERIOD",
                            ActionUrl = "ifrsloan-collateralrealizationperiod-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "IFRS_INDIVIDUAL_SCHEDULE",
                            Alias = "Individual Schedule",
                            Action = "IFRS_INDIVIDUAL_SCHEDULE",
                            ActionUrl = "ifrsloan-individualschedule-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "IFRS_WATCHLISTED_LOAN",
                            Alias = "Watchlisted Loans",
                            Action = "IFRS_WATCHLISTED_LOAN",
                            ActionUrl = "ifrsloan-watchlistedroot-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "IFRS_INDIVIDUAL_IMPAIRMENT",
                            Alias = "Individual Impairment",
                            Action = "IFRS_INDIVIDUAL_IMPAIRMENT",
                            ActionUrl = "ifrsloan-individualimpairment-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "IFRS_IMPAIRMENT_OVERRIDE",
                            Alias = "Impairment Overrides",
                            Action = "IFRS_IMPAIRMENT_OVERRIDE",
                            ActionUrl = "ifrsloan-impairmentoverride-list",
                            Image = null,
                            ImageUrl = "action_image",
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


        #region LoanSetup operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LoanSetup UpdateLoanSetup(LoanSetup loanSetup)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanSetupRepository loanSetupRepository = _DataRepositoryFactory.GetDataRepository<ILoanSetupRepository>();

                LoanSetup updatedEntity = null;

                if (loanSetup.LoanSetupId == 0)
                    updatedEntity = loanSetupRepository.Add(loanSetup);
                else
                    updatedEntity = loanSetupRepository.Update(loanSetup);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLoanSetup(int loanSetupId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanSetupRepository loanSetupRepository = _DataRepositoryFactory.GetDataRepository<ILoanSetupRepository>();

                loanSetupRepository.Remove(loanSetupId);
            });
        }

        public LoanSetup GetLoanSetup(int loanSetupId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanSetupRepository loanSetupRepository = _DataRepositoryFactory.GetDataRepository<ILoanSetupRepository>();

                LoanSetup loanSetupEntity = loanSetupRepository.Get(loanSetupId);
                if (loanSetupEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LoanSetup with ID of {0} is not in database", loanSetupId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return loanSetupEntity;
            });
        }

        //public LoanSetupData[] GetAllLoanSetups()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        ILoanSetupRepository loanSetupRepository = _DataRepositoryFactory.GetDataRepository<ILoanSetupRepository>();

        //        List<LoanSetupData> loanSetups = new List<LoanSetupData>();
        //        IEnumerable<LoanSetupData> loanSetups = loanSetupRepository.Get().ToArray();

        //        return loanSetups.ToArray();
        //    });
        //}
        public LoanSetupData[] GetAllLoanSetups()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanSetupRepository loanSetupRepository = _DataRepositoryFactory.GetDataRepository<ILoanSetupRepository>();


                List<LoanSetupData> loanSetups = new List<LoanSetupData>();
                IEnumerable<LoanSetup> loanSetupinfo = loanSetupRepository.Get().ToArray();

                foreach (var loanSetup in loanSetupinfo)
                {
                    loanSetups.Add(
                        new LoanSetupData
                        {
                            LoanSetupId = loanSetup.EntityId,
                            SignificantLoanMarkUp = loanSetup.SignificantLoanMarkUp,
                            RatingType = loanSetup.RatingType,
                            RatingTypeName = loanSetup.RatingType.ToString(),
                            EPDefault = loanSetup.EPDefault,
                            EPOption = loanSetup.EPOption,
                            CompanyCode = loanSetup.CompanyCode,
                            Active = loanSetup.Active
                        });
                }

                return loanSetups.ToArray();
            });
        }

        #endregion

        #region ScheduleType operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ScheduleType UpdateScheduleType(ScheduleType scheduleType)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScheduleTypeRepository scheduleTypeRepository = _DataRepositoryFactory.GetDataRepository<IScheduleTypeRepository>();

                ScheduleType updatedEntity = null;

                if (scheduleType.ScheduleTypeId == 0)
                    updatedEntity = scheduleTypeRepository.Add(scheduleType);
                else
                    updatedEntity = scheduleTypeRepository.Update(scheduleType);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteScheduleType(int scheduleTypeId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScheduleTypeRepository scheduleTypeRepository = _DataRepositoryFactory.GetDataRepository<IScheduleTypeRepository>();

                scheduleTypeRepository.Remove(scheduleTypeId);
            });
        }

        public ScheduleType GetScheduleType(int scheduleTypeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScheduleTypeRepository scheduleTypeRepository = _DataRepositoryFactory.GetDataRepository<IScheduleTypeRepository>();

                ScheduleType scheduleTypeEntity = scheduleTypeRepository.Get(scheduleTypeId);
                if (scheduleTypeEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ScheduleType with ID of {0} is not in database", scheduleTypeId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scheduleTypeEntity;
            });
        }

        public ScheduleType[] GetAllScheduleTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScheduleTypeRepository scheduleTypeRepository = _DataRepositoryFactory.GetDataRepository<IScheduleTypeRepository>();

                IEnumerable<ScheduleType> scheduleTypes = scheduleTypeRepository.Get().ToArray();

                return scheduleTypes.ToArray();
            });
        }
        #endregion

        #region IFRSProduct operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IFRSProduct UpdateIFRSProduct(IFRSProduct product)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSProductRepository productRepository = _DataRepositoryFactory.GetDataRepository<IIFRSProductRepository>();

                IFRSProduct updatedEntity = null;

                if (product.ProductId == 0)
                    updatedEntity = productRepository.Add(product);
                else
                    updatedEntity = productRepository.Update(product);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIFRSProduct(int productId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSProductRepository productRepository = _DataRepositoryFactory.GetDataRepository<IIFRSProductRepository>();

                productRepository.Remove(productId);
            });
        }

        public IFRSProduct GetIFRSProduct(int productId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSProductRepository productRepository = _DataRepositoryFactory.GetDataRepository<IIFRSProductRepository>();

                IFRSProduct productEntity = productRepository.Get(productId);
                if (productEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IFRSProduct with ID of {0} is not in database", productId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return productEntity;
            });
        }


        public IFRSProductData[] GetAllIFRSProducts()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSProductRepository productRepository = _DataRepositoryFactory.GetDataRepository<IIFRSProductRepository>();


                List<IFRSProductData> products = new List<IFRSProductData>();
                IEnumerable<IFRSProductInfo> productInfos = productRepository.GetIFRSProducts().ToArray();

                foreach (var productInfo in productInfos)
                {
                    products.Add(
                        new IFRSProductData
                        {
                            ProductId = productInfo.IFRSProduct.EntityId,
                            ProductCode = productInfo.IFRSProduct.ProductCode,
                            ProductName = productInfo.Product.Name,
                            MarketRate = productInfo.IFRSProduct.MarketRate,
                            PastDueRate = productInfo.IFRSProduct.PastDueRate,
                            ScheduleTypeCode = productInfo.IFRSProduct.ScheduleTypeCode,
                            ScheduleTypeName = productInfo.ScheduleType.Name,
                            Active = productInfo.IFRSProduct.Active
                        });
                }

                return products.ToArray();
            });
        }

        #endregion

        #region CreditRiskRating operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CreditRiskRating UpdateCreditRiskRating(CreditRiskRating creditRiskRating)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICreditRiskRatingRepository creditRiskRatingRepository = _DataRepositoryFactory.GetDataRepository<ICreditRiskRatingRepository>();

                CreditRiskRating updatedEntity = null;

                if (creditRiskRating.CreditRiskRatingId == 0)
                    updatedEntity = creditRiskRatingRepository.Add(creditRiskRating);
                else
                    updatedEntity = creditRiskRatingRepository.Update(creditRiskRating);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCreditRiskRating(int creditRiskRatingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICreditRiskRatingRepository creditRiskRatingRepository = _DataRepositoryFactory.GetDataRepository<ICreditRiskRatingRepository>();

                creditRiskRatingRepository.Remove(creditRiskRatingId);
            });
        }

        public CreditRiskRating GetCreditRiskRating(int creditRiskRatingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICreditRiskRatingRepository creditRiskRatingRepository = _DataRepositoryFactory.GetDataRepository<ICreditRiskRatingRepository>();

                CreditRiskRating creditRiskRatingEntity = creditRiskRatingRepository.Get(creditRiskRatingId);
                if (creditRiskRatingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CreditRiskRating with ID of {0} is not in database", creditRiskRatingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return creditRiskRatingEntity;
            });
        }

        public CreditRiskRating[] GetAllCreditRiskRatings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICreditRiskRatingRepository creditRiskRatingRepository = _DataRepositoryFactory.GetDataRepository<ICreditRiskRatingRepository>();

                IEnumerable<CreditRiskRating> creditRiskRatings = creditRiskRatingRepository.Get().ToArray();

                return creditRiskRatings.ToArray();
            });
        }

        #endregion

        #region CollateralCategory operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CollateralCategory UpdateCollateralCategory(CollateralCategory collateralCategory)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralCategoryRepository collateralCategoryRepository = _DataRepositoryFactory.GetDataRepository<ICollateralCategoryRepository>();

                CollateralCategory updatedEntity = null;

                if (collateralCategory.CollateralCategoryId == 0)
                    updatedEntity = collateralCategoryRepository.Add(collateralCategory);
                else
                    updatedEntity = collateralCategoryRepository.Update(collateralCategory);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCollateralCategory(int collateralCategoryId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralCategoryRepository collateralCategoryRepository = _DataRepositoryFactory.GetDataRepository<ICollateralCategoryRepository>();

                collateralCategoryRepository.Remove(collateralCategoryId);
            });
        }

        public CollateralCategory GetCollateralCategory(int collateralCategoryId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralCategoryRepository collateralCategoryRepository = _DataRepositoryFactory.GetDataRepository<ICollateralCategoryRepository>();

                CollateralCategory collateralCategoryEntity = collateralCategoryRepository.Get(collateralCategoryId);
                if (collateralCategoryEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CollateralCategory with ID of {0} is not in database", collateralCategoryId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return collateralCategoryEntity;
            });
        }

        public CollateralCategory[] GetAllCollateralCategorys()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralCategoryRepository collateralCategoryRepository = _DataRepositoryFactory.GetDataRepository<ICollateralCategoryRepository>();

                IEnumerable<CollateralCategory> collateralCategories = collateralCategoryRepository.Get().ToArray();

                return collateralCategories.ToArray();
            });
        }

        #endregion

        #region CollateralType operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CollateralType UpdateCollateralType(CollateralType collateralType)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralTypeRepository collateralTypeRepository = _DataRepositoryFactory.GetDataRepository<ICollateralTypeRepository>();

                CollateralType updatedEntity = null;

                if (collateralType.CollateralTypeId == 0)
                    updatedEntity = collateralTypeRepository.Add(collateralType);
                else
                    updatedEntity = collateralTypeRepository.Update(collateralType);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCollateralType(int collateralTypeId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralTypeRepository collateralTypeRepository = _DataRepositoryFactory.GetDataRepository<ICollateralTypeRepository>();

                collateralTypeRepository.Remove(collateralTypeId);
            });
        }

        public CollateralType GetCollateralType(int collateralTypeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralTypeRepository collateralTypeRepository = _DataRepositoryFactory.GetDataRepository<ICollateralTypeRepository>();

                CollateralType collateralTypeEntity = collateralTypeRepository.Get(collateralTypeId);
                if (collateralTypeEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CollateralType with ID of {0} is not in database", collateralTypeId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return collateralTypeEntity;
            });
        }

        public CollateralTypeData[] GetCollateralTypeByCategory(string categoryCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralTypeRepository collateralTypeRepository = _DataRepositoryFactory.GetDataRepository<ICollateralTypeRepository>();


                List<CollateralTypeData> collateralTypes = new List<CollateralTypeData>();
                IEnumerable<CollateralTypeInfo> collateralTypeInfos = collateralTypeRepository.GetCollateralTypes(categoryCode).ToArray();


                foreach (var collateralTypeInfo in collateralTypeInfos)
                {
                    collateralTypes.Add(
                        new CollateralTypeData
                        {
                            CollateralTypeId = collateralTypeInfo.CollateralType.EntityId,
                            Code = collateralTypeInfo.CollateralType.Code,
                            Name = collateralTypeInfo.CollateralType.Name,
                            CategoryCode = collateralTypeInfo.CollateralCategory.Code,
                            CategoryName = collateralTypeInfo.CollateralCategory.Name,
                            CompanyCode = collateralTypeInfo.CollateralType.CompanyCode,
                            Active = collateralTypeInfo.CollateralType.Active
                        });
                }

                return collateralTypes.ToArray();
            });
        }

        public CollateralTypeData[] GetAllCollateralTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralTypeRepository collateralTypeRepository = _DataRepositoryFactory.GetDataRepository<ICollateralTypeRepository>();


                List<CollateralTypeData> collateralTypes = new List<CollateralTypeData>();
                IEnumerable<CollateralTypeInfo> collateralTypeInfos = collateralTypeRepository.GetCollateralTypes().ToArray();

                foreach (var collateralTypeInfo in collateralTypeInfos)
                {
                    collateralTypes.Add(
                        new CollateralTypeData
                        {
                            CollateralTypeId = collateralTypeInfo.CollateralType.EntityId,
                            Code = collateralTypeInfo.CollateralType.Code,
                            Name = collateralTypeInfo.CollateralType.Name,
                            CategoryCode = collateralTypeInfo.CollateralCategory.Code,
                            CategoryName = collateralTypeInfo.CollateralCategory.Name,
                            CompanyCode = collateralTypeInfo.CollateralType.CompanyCode,
                            Active = collateralTypeInfo.CollateralType.Active
                        });
                }

                return collateralTypes.ToArray();
            });
        }

        #endregion

        #region CollateralRealizationPeriod operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CollateralRealizationPeriod UpdateCollateralRealizationPeriod(CollateralRealizationPeriod collateralRealizationPeriod)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralRealizationPeriodRepository collateralRealizationPeriodRepository = _DataRepositoryFactory.GetDataRepository<ICollateralRealizationPeriodRepository>();

                CollateralRealizationPeriod updatedEntity = null;

                if (collateralRealizationPeriod.CollateralRealizationPeriodId == 0)
                    updatedEntity = collateralRealizationPeriodRepository.Add(collateralRealizationPeriod);
                else
                    updatedEntity = collateralRealizationPeriodRepository.Update(collateralRealizationPeriod);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCollateralRealizationPeriod(int collateralRealizationPeriodId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralRealizationPeriodRepository collateralRealizationPeriodRepository = _DataRepositoryFactory.GetDataRepository<ICollateralRealizationPeriodRepository>();

                collateralRealizationPeriodRepository.Remove(collateralRealizationPeriodId);
            });
        }

        public CollateralRealizationPeriod GetCollateralRealizationPeriod(int collateralRealizationPeriodId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralRealizationPeriodRepository collateralRealizationPeriodRepository = _DataRepositoryFactory.GetDataRepository<ICollateralRealizationPeriodRepository>();

                CollateralRealizationPeriod collateralRealizationPeriodEntity = collateralRealizationPeriodRepository.Get(collateralRealizationPeriodId);
                if (collateralRealizationPeriodEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CollateralRealizationPeriod with ID of {0} is not in database", collateralRealizationPeriodId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return collateralRealizationPeriodEntity;
            });
        }


        public CollateralRealizationPeriodData[] GetAllCollateralRealizationPeriods()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralRealizationPeriodRepository collateralRealizationPeriodRepository = _DataRepositoryFactory.GetDataRepository<ICollateralRealizationPeriodRepository>();


                List<CollateralRealizationPeriodData> collateralRealizationPeriods = new List<CollateralRealizationPeriodData>();
                IEnumerable<CollateralRealizationPeriodInfo> collateralRealizationPeriodInfos = collateralRealizationPeriodRepository.GetCollateralRealizationPeriods().ToArray();

                foreach (var collateralRealizationPeriodInfo in collateralRealizationPeriodInfos)
                {
                    collateralRealizationPeriods.Add(
                        new CollateralRealizationPeriodData
                        {
                            CollateralRealizationPeriodId = collateralRealizationPeriodInfo.CollateralRealizationPeriod.EntityId,
                            TypeCode = collateralRealizationPeriodInfo.CollateralRealizationPeriod.TypeCode,
                            TypeName = collateralRealizationPeriodInfo.CollateralType.Name,
                            Duration = collateralRealizationPeriodInfo.CollateralRealizationPeriod.Duration,
                            CompanyCode = collateralRealizationPeriodInfo.CollateralRealizationPeriod.CompanyCode,
                            Active = collateralRealizationPeriodInfo.CollateralRealizationPeriod.Active
                        });
                }

                return collateralRealizationPeriods.ToArray();
            });
        }

        #endregion

        #region CollateralInformation operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CollateralInformation UpdateCollateralInformation(CollateralInformation collateralInformation)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralInformationRepository collateralInformationRepository = _DataRepositoryFactory.GetDataRepository<ICollateralInformationRepository>();

                CollateralInformation updatedEntity = null;

                if (collateralInformation.CollateralInformationId == 0)
                    updatedEntity = collateralInformationRepository.Add(collateralInformation);
                else
                    updatedEntity = collateralInformationRepository.Update(collateralInformation);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCollateralInformation(int collateralInformationId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralInformationRepository collateralInformationRepository = _DataRepositoryFactory.GetDataRepository<ICollateralInformationRepository>();

                collateralInformationRepository.Remove(collateralInformationId);
            });
        }

        public CollateralInformation GetCollateralInformation(int collateralInformationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralInformationRepository collateralInformationRepository = _DataRepositoryFactory.GetDataRepository<ICollateralInformationRepository>();

                CollateralInformation collateralInformationEntity = collateralInformationRepository.Get(collateralInformationId);
                if (collateralInformationEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CollateralInformation with ID of {0} is not in database", collateralInformationId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return collateralInformationEntity;
            });
        }

        //public CollateralInformation[] GetAllCollateralInformations()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        ICollateralInformationRepository collateralInformationRepository = _DataRepositoryFactory.GetDataRepository<ICollateralInformationRepository>();

        //        IEnumerable<CollateralInformation> collateralInformations = collateralInformationRepository.Get().ToArray();

        //        return collateralInformations.ToArray();
        //    });
        //}

        public CollateralInformationData[] GetAllCollateralInformations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralInformationRepository collateralInformationRepository = _DataRepositoryFactory.GetDataRepository<ICollateralInformationRepository>();


                List<CollateralInformationData> collaterals = new List<CollateralInformationData>();
                IEnumerable<CollateralDetailsInfo> collateralInfos = collateralInformationRepository.GetCollateralDetails().ToArray();

                foreach (var collateralInfo in collateralInfos)
                {
                    collaterals.Add(
                        new CollateralInformationData
                        {
                            CollateralInformationId = collateralInfo.CollateralInformation.EntityId,
                            RefNo = collateralInfo.CollateralInformation.RefNo,
                            AccountNo = collateralInfo.CollateralInformation.AccountNo,
                            Amount = collateralInfo.CollateralInformation.Amount,
                            Category = collateralInfo.CollateralCategory.Code,
                            CategoryName = collateralInfo.CollateralCategory.Name,
                            Type = collateralInfo.CollateralType.Code,
                            TypeName = collateralInfo.CollateralType.Name,
                            CustomerName = collateralInfo.CollateralInformation.CustomerName,
                            CompanyCode = collateralInfo.CollateralInformation.CompanyCode,
                            Active = collateralInfo.CollateralInformation.Active
                        });
                }

                return collaterals.ToArray();
            });
        }
        #endregion

        #region WatchListedLoan operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public WatchListedLoan UpdateWatchListedLoan(WatchListedLoan watchListedLoan)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IWatchListedLoanRepository watchListedLoanRepository = _DataRepositoryFactory.GetDataRepository<IWatchListedLoanRepository>();

                WatchListedLoan updatedEntity = null;

                if (watchListedLoan.WatchListedLoanId == 0)
                    updatedEntity = watchListedLoanRepository.Add(watchListedLoan);
                else
                    updatedEntity = watchListedLoanRepository.Update(watchListedLoan);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteWatchListedLoan(int watchListedLoanId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IWatchListedLoanRepository watchListedLoanRepository = _DataRepositoryFactory.GetDataRepository<IWatchListedLoanRepository>();

                watchListedLoanRepository.Remove(watchListedLoanId);
            });
        }

        public WatchListedLoan GetWatchListedLoan(int watchListedLoanId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IWatchListedLoanRepository watchListedLoanRepository = _DataRepositoryFactory.GetDataRepository<IWatchListedLoanRepository>();

                WatchListedLoan watchListedLoanEntity = watchListedLoanRepository.Get(watchListedLoanId);
                if (watchListedLoanEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("WatchListedLoan with ID of {0} is not in database", watchListedLoanId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return watchListedLoanEntity;
            });
        }

        public WatchListedLoan[] GetAllWatchListedLoans()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IWatchListedLoanRepository wListedLoanRepository = _DataRepositoryFactory.GetDataRepository<IWatchListedLoanRepository>();

                IEnumerable<WatchListedLoan> watchListedLoanRepository = wListedLoanRepository.Get().ToArray();

                return watchListedLoanRepository.ToArray();
            });
        }

        #endregion

        #region ImpairmentOverride operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ImpairmentOverride UpdateImpairmentOverride(ImpairmentOverride impairmentOverride)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IImpairmentOverrideRepository impairmentOverrideRepository = _DataRepositoryFactory.GetDataRepository<IImpairmentOverrideRepository>();

                ImpairmentOverride updatedEntity = null;

                if (impairmentOverride.ImpairmentOverrideId == 0)
                    updatedEntity = impairmentOverrideRepository.Add(impairmentOverride);
                else
                    updatedEntity = impairmentOverrideRepository.Update(impairmentOverride);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteImpairmentOverride(int impairmentOverrideId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IImpairmentOverrideRepository impairmentOverrideRepository = _DataRepositoryFactory.GetDataRepository<IImpairmentOverrideRepository>();

                impairmentOverrideRepository.Remove(impairmentOverrideId);
            });
        }

        public ImpairmentOverride GetImpairmentOverride(int impairmentOverrideId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IImpairmentOverrideRepository impairmentOverrideRepository = _DataRepositoryFactory.GetDataRepository<IImpairmentOverrideRepository>();

                ImpairmentOverride impairmentOverrideEntity = impairmentOverrideRepository.Get(impairmentOverrideId);
                if (impairmentOverrideEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ImpairmentOverride with ID of {0} is not in database", impairmentOverrideId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return impairmentOverrideEntity;
            });
        }

        public ImpairmentOverrideData[] GetAllImpairmentOverrides()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IImpairmentOverrideRepository impairmentOverrideRepository = _DataRepositoryFactory.GetDataRepository<IImpairmentOverrideRepository>();

                List<ImpairmentOverrideData> impairmentOverride = new List<ImpairmentOverrideData>();
                IEnumerable<ImpairmentOverride> impairmentOverrideInfo = impairmentOverrideRepository.Get().ToArray();
                foreach (var impOvd in impairmentOverrideInfo)
                {
                    impairmentOverride.Add(
                        new ImpairmentOverrideData
                        {
                            ImpairmentOverrideId = impOvd.EntityId,
                            RefNo = impOvd.RefNo,
                            Reason = impOvd.Reason,
                            AccountNo = impOvd.AccountNo,
                            Classification = impOvd.Classification,
                            ClassificationName = impOvd.Classification.ToString(),
                            CompanyCode = impOvd.CompanyCode,
                            Active = impOvd.Active
                        });
                }
                return impairmentOverride.ToArray();
            });
        }

        #endregion

        #region IndividualSchedule operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IndividualSchedule UpdateIndividualSchedule(IndividualSchedule individualSchedule)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIndividualScheduleRepository individualScheduleRepository = _DataRepositoryFactory.GetDataRepository<IIndividualScheduleRepository>();

                IndividualSchedule updatedEntity = null;

                if (individualSchedule.Id == 0)
                    updatedEntity = individualScheduleRepository.Add(individualSchedule);
                else
                    updatedEntity = individualScheduleRepository.Update(individualSchedule);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIndividualSchedule(int individualScheduleId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIndividualScheduleRepository individualScheduleRepository = _DataRepositoryFactory.GetDataRepository<IIndividualScheduleRepository>();

                individualScheduleRepository.Remove(individualScheduleId);
            });
        }

        public IndividualSchedule GetIndividualSchedule(int individualScheduleId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIndividualScheduleRepository individualScheduleRepository = _DataRepositoryFactory.GetDataRepository<IIndividualScheduleRepository>();

                IndividualSchedule individualScheduleEntity = individualScheduleRepository.Get(individualScheduleId);
                if (individualScheduleEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IndividualSchedule with ID of {0} is not in database", individualScheduleId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return individualScheduleEntity;
            });
        }

        public IndividualScheduleData[] GetAllIndividualSchedules()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIndividualScheduleRepository individualScheduleRepository = _DataRepositoryFactory.GetDataRepository<IIndividualScheduleRepository>();

                List<IndividualScheduleData> individualSchedule = new List<IndividualScheduleData>();
                IEnumerable<IndividualSchedule> individualScheduleInfo = individualScheduleRepository.Get().ToArray();
                foreach (var indSch in individualScheduleInfo)
                {
                    individualSchedule.Add(
                        new IndividualScheduleData
                        {
                            Id = indSch.EntityId,
                            RefNo = indSch.RefNo,
                            Amount = indSch.Amount,
                            AmountPrinEnd = indSch.Amount,
                            FeeAmount = indSch.FeeAmount,
                            IRR = indSch.IRR,
                            MaturityDate = indSch.MaturityDate,
                            Active = indSch.Active,
                            Processed = indSch.Processed,
                            RunDate = indSch.RunDate,
                            ValueDate = indSch.ValueDate
                        });
                }
                return individualSchedule.ToArray();
            });
        }


        public string[] GetDistinctRefNo()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIndividualScheduleRepository individualScheduleRepository = _DataRepositoryFactory.GetDataRepository<IIndividualScheduleRepository>();

                // List<string> listOfRefnos = new List<string>();
                IEnumerable<string> listOfRefnos = individualScheduleRepository.GetDistinctRefNos();
                //foreach (string refno in listOfString)
                //{
                //    listOfRefnos.Add(refno);
                //}

                return listOfRefnos.ToArray();
            });
        }

        public IndividualScheduleData[] GetIndividualSchedulebyRefNo(string refNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIndividualScheduleRepository individualScheduleRepository = _DataRepositoryFactory.GetDataRepository<IIndividualScheduleRepository>();

                List<IndividualScheduleData> individualSchedule = new List<IndividualScheduleData>();
                IEnumerable<IndividualScheduleInfo> individualScheduleInfo = individualScheduleRepository.GetIndividualSchedules(refNo).ToArray();
                foreach (var indSch in individualScheduleInfo)
                {
                    individualSchedule.Add(
                        new IndividualScheduleData
                        {
                            RefNo = indSch.LoanPrimaryData.RefNo,
                            AmountPrinEnd = indSch.LoanPrimaryData.Amount,
                            FeeAmount = indSch.IntegralFee.FeeAmount,
                            //IRR = indSch.LoanIRRData != null ? indSch.LoanIRRData.IRR : 0,
                            IRR = (decimal)(indSch.LoanIRRData != null ? indSch.LoanIRRData.IRR : 0),
                            MaturityDate = indSch.LoanPrimaryData.MaturityDate,
                            Active = true,
                            Amount = indSch.LoanPrimaryData.Amount,
                            Processed = false,
                            ValueDate = indSch.LoanPrimaryData.ValueDate
                        });
                }
                return individualSchedule.ToArray();
            });
        }
        #endregion

        #region IndividualImpairment operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IndividualImpairment UpdateIndividualImpairment(IndividualImpairment individualImpairment)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIndividualImpairmentRepository individualImpairmentRepository = _DataRepositoryFactory.GetDataRepository<IIndividualImpairmentRepository>();

                IndividualImpairment updatedEntity = null;

                if (individualImpairment.Id == 0)
                    updatedEntity = individualImpairmentRepository.Add(individualImpairment);
                else
                    updatedEntity = individualImpairmentRepository.Update(individualImpairment);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIndividualImpairment(int individualImpairmentId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIndividualImpairmentRepository individualImpairmentRepository = _DataRepositoryFactory.GetDataRepository<IIndividualImpairmentRepository>();

                individualImpairmentRepository.Remove(individualImpairmentId);
            });
        }

        public IndividualImpairment GetIndividualImpairment(int individualImpairmentId) 
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIndividualImpairmentRepository individualImpairmentRepository = _DataRepositoryFactory.GetDataRepository<IIndividualImpairmentRepository>();

                IndividualImpairment individualImpairmentEntity = individualImpairmentRepository.Get(individualImpairmentId);
                if (individualImpairmentEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("lndividualImpairment with ID of {0} is not in database", individualImpairmentId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return individualImpairmentEntity;
            });
        }

        public IndividualImpairmentData[] GetAlllndividualImpairments()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIndividualImpairmentRepository individualImpairmentRepository = _DataRepositoryFactory.GetDataRepository<IIndividualImpairmentRepository>();

                List<IndividualImpairmentData> individualImpairment = new List<IndividualImpairmentData>();
                IEnumerable<IndividualImpairment> individualImpairmentInfo = individualImpairmentRepository.GetIndividualImpairments().ToArray();
                foreach (var indImp in individualImpairmentInfo)
                {
                    individualImpairment.Add(
                        new IndividualImpairmentData
                        {
                            Id = indImp.EntityId,
                            RefNo = indImp.RefNo,
                            MaturityDate = indImp.MaturityDate,
                            AccountNo = indImp.AccountNo,
                            Processed = indImp.Processed,
                            ProductName = indImp.ProductName,
                            RunDate = indImp.RunDate,
                            ValueDate = indImp.ValueDate,
                            Active = indImp.Active
                        });
                }
                return individualImpairment.ToArray();
            });
        }


        public string[] GetAvailableReferenceNumbers()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIndividualImpairmentRepository individualImpairmentRepository = _DataRepositoryFactory.GetDataRepository<IIndividualImpairmentRepository>();

                // List<string> listOfRefnos = new List<string>();
                IEnumerable<string> listOfRefnos = individualImpairmentRepository.GetDistinctRefNos();
                //foreach (string refno in listOfString)
                //{
                //    listOfRefnos.Add(refno);
                //}

                return listOfRefnos.ToArray();
            });
        }


        public IndividualImpairmentData[] GetIndividualImpairments(string refNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIndividualImpairmentRepository individualImpairmentRepository = _DataRepositoryFactory.GetDataRepository<IIndividualImpairmentRepository>();

                List<IndividualImpairmentData> individualImpairment = new List<IndividualImpairmentData>();

                IEnumerable<LoanDetails> individualImpairmentInfo = individualImpairmentRepository.GetIndividualImpairments(refNo).ToArray();

                foreach (var indSch in individualImpairmentInfo)
                {
                    individualImpairment.Add(
                        new IndividualImpairmentData
                        {
                            RefNo = indSch.RefNo,
                            AccountNo = indSch.AccountNo,
                            ProductName = indSch.ProductName,
                            ValueDate = (DateTime)indSch.ValueDate,
                            MaturityDate = (DateTime)indSch.MaturityDate,
                            Active = indSch.Active
                        });
                }
                return individualImpairment.ToArray();
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




        #endregion

    }
}


  //List<IndividualScheduleData> individualSchedule = new List<IndividualScheduleData>();
  //IEnumerable<IndividualScheduleInfo> individualScheduleInfo = individualScheduleRepository.GetIndividualSchedules(refNo).ToArray();