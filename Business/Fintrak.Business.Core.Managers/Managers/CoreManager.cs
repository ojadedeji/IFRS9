using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Transactions;
using Fintrak.Shared.Core.Entities;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Business.Core.Contracts;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Common.ServiceModel;
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;
using audit = Fintrak.Shared.AuditService;
using Fintrak.Shared.SystemCore.Entities;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.Common.Services.QueryService;
using Fintrak.Shared.Common.Data;
using System.Data.SqlClient;
using DecryptOperation;

namespace Fintrak.Business.Core.Managers
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

        const string SOLUTION_NAME = "CORE";
        const string SOL_NAME = "FIN_IFRS";
        const string SOLUTION_ALIAS = "Core";
        const string MODULE_NAME = "FIN_BUSINESS_CORE";
        const string MODULE_ALIAS = "Fintrak Business Core";
        const string GROUP_ADMINISTRATOR = "Administrator";
        const string GROUP_SUPER_BUSINESS = "Super Business";
        const string GROUP_BUSINESS = "Business";
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
                IProductTypeRepository productTypeRepository = _DataRepositoryFactory.GetDataRepository<IProductTypeRepository>();
                IRateTypeRepository rateTypeRepository = _DataRepositoryFactory.GetDataRepository<IRateTypeRepository>();
               
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
                        var superBusinessRole = roleRepository.Get().Where(c => c.Name == GROUP_SUPER_BUSINESS && c.SolutionId == solution.SolutionId).FirstOrDefault();
                        var businessRole = roleRepository.Get().Where(c => c.Name == GROUP_BUSINESS && c.SolutionId == solution.SolutionId).FirstOrDefault();
                        var userRole = roleRepository.Get().Where(c => c.Name == GROUP_USER && c.SolutionId == solution.SolutionId).FirstOrDefault();

                        var menuIndex = 0;

                        //register menu
                        var root = menuRepository.Get().Where(c => c.Alias == "Settings").FirstOrDefault();

                        var configurationMenu = menuRepository.Get().Where(c => c.Alias == "Configuration").FirstOrDefault();

                        var actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "STAFFS",
                            Alias = "Staffs",
                            Action = "STAFFS",
                            ActionUrl = "core-staff-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = configurationMenu.EntityId,
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
                            RoleId = superBusinessRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = businessRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "PAY_GRADES",
                            Alias = "Pay Grades",
                            Action = "PAY_GRADES",
                            ActionUrl = "core-paygrade-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = configurationMenu.EntityId,
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
                            RoleId = superBusinessRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = businessRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "PERIODS",
                            Alias = "Periods",
                            Action = "PERIODS",
                            ActionUrl = "core-fiscalyear-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = configurationMenu.EntityId,
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
                            RoleId = superBusinessRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = businessRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = userRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "PRODUCT_TYPES",
                            Alias = "Product Types",
                            Action = "PRODUCT_TYPES",
                            ActionUrl = "core-producttype-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = configurationMenu.EntityId,
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
                            RoleId = superBusinessRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = businessRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "PRODUCTS",
                            Alias = "Products",
                            Action = "PRODUCTS",
                            ActionUrl = "core-product-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = configurationMenu.EntityId,
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
                            RoleId = superBusinessRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = businessRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "FINANCIAL_TYPE",
                            Alias = "Financial Types",
                            Action = "FINANCIAL_TYPE",
                            ActionUrl = "core-financialtype-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = configurationMenu.EntityId,
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
                            RoleId = superBusinessRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = businessRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "CHART_OF_ACCOUNTS",
                            Alias = "Chart of Accounts",
                            Action = "CHART_OF_ACCOUNTS",
                            ActionUrl = "core-chartofaccount-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = configurationMenu.EntityId,
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
                            RoleId = superBusinessRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = businessRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "CURRENCY",
                            Alias = "Currency",
                            Action = "CURRENCY",
                            ActionUrl = "core-currency-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = configurationMenu.EntityId,
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
                            RoleId = superBusinessRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = businessRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "GL_DEFINITION",
                            Alias = "GL Master",
                            Action = "GL_DEFINITION",
                            ActionUrl = "core-gldefinition-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = configurationMenu.EntityId,
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
                            RoleId = superBusinessRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = businessRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });
                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "REPORT_VIEW_STATUS",
                            Alias = "Report View Status",
                            Action = "REPORT_VIEW_STATUS",
                            ActionUrl = "core-reportstatus-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = configurationMenu.EntityId,
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
                            RoleId = superBusinessRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = businessRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });
                        //ProductType
                        //var productType = new ProductType()
                        //{
                        //    Name = "Corporate"
                        //};

                        //productTypeRepository.Add(productType);

                        //productType = new ProductType()
                        //{
                        //    Name = "Retail"
                        //};

                        //productTypeRepository.Add(productType);

                        //productType = new ProductType()
                        //{
                        //    Name = "Staff"
                        //};

                        //productTypeRepository.Add(productType);

                        //productType = new ProductType()
                        //{
                        //    Name = "Loan"
                        //};

                        //productTypeRepository.Add(productType);

                        //productType = new ProductType()
                        //{
                        //    Name = "Individual"
                        //};

                        //productTypeRepository.Add(productType);

                        //productType = new ProductType()
                        //{
                        //    Name = "Public"
                        //};

                        //productTypeRepository.Add(productType);

                        ////RateType
                        //var rateType = new RateType()
                        //{
                        //    Name = "Actual"
                        //};

                        //rateTypeRepository.Add(rateType);

                        //rateType = new RateType()
                        //{
                        //    Name = "Budget"
                        //};

                        //rateTypeRepository.Add(rateType);

                        //rateType = new RateType()
                        //{
                        //    Name = "Forecast"
                        //};

                        //rateTypeRepository.Add(rateType);

                        //rateType = new RateType()
                        //{
                        //    Name = "Historical"
                        //};

                        //rateTypeRepository.Add(rateType);
                        
                    }

                    ts.Complete();
                }

            });

        }



        public UInt64 GetTotalRecordsCount(string tableName, string columnName, string searchParamS, Double? searchParamN)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                var connectionString = GetDataConnection();

                UInt64 totalRecords = 0;

                using (var con = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand("spp_Get_Count", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TableName",
                        Value = tableName,
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "ColumnName",
                        Value = columnName,
                    });
                    if (searchParamN == null)
                    {
                        cmd.Parameters.Add(new SqlParameter
                        {
                            ParameterName = "SearchParam",
                            Value = searchParamS,
                        });
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter
                        {
                            ParameterName = "SearchParam",
                            Value = searchParamN,
                        });
                    }

                    con.Open();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (reader["Count"] != DBNull.Value)
                            totalRecords = UInt64.Parse(reader["Count"].ToString());
                    }

                    con.Close();
                }
                return totalRecords;
            });
        }


        
        #region FiscalYear operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public FiscalYear UpdateFiscalYear(FiscalYear fiscalYear)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFiscalYearRepository fiscalYearRepository = _DataRepositoryFactory.GetDataRepository<IFiscalYearRepository>();

                FiscalYear updatedEntity = null;

                if (fiscalYear.FiscalYearId == 0)
                    updatedEntity = fiscalYearRepository.Add(fiscalYear);
                else
                    updatedEntity = fiscalYearRepository.Update(fiscalYear);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteFiscalYear(int fiscalYearId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFiscalYearRepository fiscalYearRepository = _DataRepositoryFactory.GetDataRepository<IFiscalYearRepository>();

                fiscalYearRepository.Remove(fiscalYearId);
            });
        }

        public FiscalYear GetFiscalYear(int fiscalYearId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFiscalYearRepository fiscalYearRepository = _DataRepositoryFactory.GetDataRepository<IFiscalYearRepository>();

                FiscalYear fiscalYearEntity = fiscalYearRepository.Get(fiscalYearId);
                if (fiscalYearEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("FiscalYear with ID of {0} is not in database", fiscalYearId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return fiscalYearEntity;
            });
        }

        public FiscalYear[] GetAllFiscalYears()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFiscalYearRepository fiscalYearRepository = _DataRepositoryFactory.GetDataRepository<IFiscalYearRepository>();

                IEnumerable<FiscalYear> fiscalYears = fiscalYearRepository.Get();

                return fiscalYears.ToArray();
            });
        }

        public FiscalYear GetOpenFiscalYear()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFiscalYearRepository fiscalYearRepository = _DataRepositoryFactory.GetDataRepository<IFiscalYearRepository>();

                FiscalYear fiscalYearEntity = fiscalYearRepository.GetOpenFiscalYear();

                return fiscalYearEntity;
            });
        }

        #endregion

        #region FiscalPeriod operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public FiscalPeriod UpdateFiscalPeriod(FiscalPeriod fiscalPeriod)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFiscalPeriodRepository fiscalPeriodRepository = _DataRepositoryFactory.GetDataRepository<IFiscalPeriodRepository>();

                FiscalPeriod updatedEntity = null;

                if (fiscalPeriod.FiscalPeriodId == 0)
                    updatedEntity = fiscalPeriodRepository.Add(fiscalPeriod);
                else
                    updatedEntity = fiscalPeriodRepository.Update(fiscalPeriod);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteFiscalPeriod(int fiscalPeriodId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFiscalPeriodRepository fiscalPeriodRepository = _DataRepositoryFactory.GetDataRepository<IFiscalPeriodRepository>();

                fiscalPeriodRepository.Remove(fiscalPeriodId);
            });
        }

        public FiscalPeriod GetFiscalPeriod(int fiscalPeriodId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFiscalPeriodRepository fiscalPeriodRepository = _DataRepositoryFactory.GetDataRepository<IFiscalPeriodRepository>();

                FiscalPeriod fiscalPeriodEntity = fiscalPeriodRepository.Get(fiscalPeriodId);
                if (fiscalPeriodEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("FiscalPeriod with ID of {0} is not in database", fiscalPeriodId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return fiscalPeriodEntity;
            });
        }

        public FiscalPeriod[] GetAllFiscalPeriods()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFiscalPeriodRepository fiscalPeriodRepository = _DataRepositoryFactory.GetDataRepository<IFiscalPeriodRepository>();

                IEnumerable<FiscalPeriod> fiscalPeriods = fiscalPeriodRepository.Get();

                return fiscalPeriods.ToArray();
            });
        }

        public FiscalPeriodData GetOpenFiscalPeriod()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFiscalPeriodRepository fiscalPeriodRepository = _DataRepositoryFactory.GetDataRepository<IFiscalPeriodRepository>();

                FiscalPeriodInfo fiscalPeriodInfo = fiscalPeriodRepository.GetOpenFiscalPeriodInfo();

                var fiscalPeriodData = new FiscalPeriodData
                        {
                            FiscalPeriodId = fiscalPeriodInfo.FiscalPeriod.EntityId,
                            Name = fiscalPeriodInfo.FiscalPeriod.Name,
                            StartDate = fiscalPeriodInfo.FiscalPeriod.StartDate,
                            EndDate = fiscalPeriodInfo.FiscalPeriod.EndDate,
                            Closed = fiscalPeriodInfo.FiscalPeriod.Closed,
                            FiscalYearId = fiscalPeriodInfo.FiscalYear.EntityId,
                            FiscalYearName = fiscalPeriodInfo.FiscalYear.Name,
                            Active = fiscalPeriodInfo.FiscalPeriod.Active
                        };

                return fiscalPeriodData;
            });
        }

        public FiscalPeriodData[] GetFiscalPeriodByYear(int fiscalYearId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFiscalPeriodRepository fiscalPeriodRepository = _DataRepositoryFactory.GetDataRepository<IFiscalPeriodRepository>();

                List<FiscalPeriodData> fiscalPeriods = new List<FiscalPeriodData>();
                IEnumerable<FiscalPeriodInfo> fiscalPeriodInfos = fiscalPeriodRepository.GetFiscalPeriodInfo(fiscalYearId).ToArray();

                foreach (var fiscalPeriodInfo in fiscalPeriodInfos)
                {
                    fiscalPeriods.Add(
                        new FiscalPeriodData
                        {
                            FiscalPeriodId = fiscalPeriodInfo.FiscalPeriod.EntityId,
                            Name = fiscalPeriodInfo.FiscalPeriod.Name,
                            StartDate = fiscalPeriodInfo.FiscalPeriod.StartDate,
                            EndDate = fiscalPeriodInfo.FiscalPeriod.EndDate,
                            Closed = fiscalPeriodInfo.FiscalPeriod.Closed,
                            Position = fiscalPeriodInfo.FiscalPeriod.StartDate.Month,
                            FiscalYearId = fiscalPeriodInfo.FiscalYear.EntityId,
                            FiscalYearName = fiscalPeriodInfo.FiscalYear.Name,
                            Active = fiscalPeriodInfo.FiscalPeriod.Active
                        });
                }

                return fiscalPeriods.ToArray();
            });
        }

        #endregion

        #region ProductType operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ProductType UpdateProductType(ProductType productType)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductTypeRepository productTypeRepository = _DataRepositoryFactory.GetDataRepository<IProductTypeRepository>();

                ProductType updatedEntity = null;

                if (productType.ProductTypeId == 0)
                    updatedEntity = productTypeRepository.Add(productType);
                else
                    updatedEntity = productTypeRepository.Update(productType);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteProductType(int productTypeId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductTypeRepository productTypeRepository = _DataRepositoryFactory.GetDataRepository<IProductTypeRepository>();

                productTypeRepository.Remove(productTypeId);
            });
        }

        public ProductType GetProductType(int productTypeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductTypeRepository productTypeRepository = _DataRepositoryFactory.GetDataRepository<IProductTypeRepository>();

                ProductType productTypeEntity = productTypeRepository.Get(productTypeId);
                if (productTypeEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ProductType with ID of {0} is not in database", productTypeId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return productTypeEntity;
            });
        }

        public ProductType[] GetAllProductTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductTypeRepository productTypeRepository = _DataRepositoryFactory.GetDataRepository<IProductTypeRepository>();

                IEnumerable<ProductType> productTypes = productTypeRepository.Get();

                return productTypes.ToArray();
            });
        }

        #endregion

        #region Product operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Product UpdateProduct(Product product)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductRepository productRepository = _DataRepositoryFactory.GetDataRepository<IProductRepository>();

                Product updatedEntity = null;

                if (product.ProductId == 0)
                    updatedEntity = productRepository.Add(product);
                else
                    updatedEntity = productRepository.Update(product);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteProduct(int productId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductRepository productRepository = _DataRepositoryFactory.GetDataRepository<IProductRepository>();

                productRepository.Remove(productId);
            });
        }

        public Product GetProduct(int productId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductRepository productRepository = _DataRepositoryFactory.GetDataRepository<IProductRepository>();

                Product productEntity = productRepository.Get(productId);
                if (productEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Product with ID of {0} is not in database", productId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return productEntity;
            });
        }

        public Product[] GetAllProducts()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductRepository productRepository = _DataRepositoryFactory.GetDataRepository<IProductRepository>();

                IEnumerable<Product> products = productRepository.Get().OrderBy(c => c.Name).ToArray();

                return products.ToArray();
            });
        }

        public Product[] GetAvailableProduct(QueryOptions queryOptions)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductRepository ProductRepository = _DataRepositoryFactory.GetDataRepository<IProductRepository>();

                IEnumerable<Product> Product = ProductRepository.GetPaginatedEntities(queryOptions);

                return Product.ToArray();
            });
        }

        public UInt64 GetTotalRecordsCountProduct(string tableName, string searchParam)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                var connectionString = GetDataConnection();

                UInt64 totalRecords = 0;

                using (var con = new SqlConnection(connectionString))
                {
                    var query = "Select Count(*) as Count from " + tableName
                                        + " where Code LIKE '%" + searchParam + "%'"
                                        + " or Name LIKE '%" + searchParam + "%'"
                                        + " or AssetGL LIKE '%" + searchParam + "%'"
                                        + " or LiabilityGL LIKE '%" + searchParam + "%'"
                                        + " or IncomeGL LIKE '%" + searchParam + "%'"
                                        + " or ExpenseGL LIKE '%" + searchParam + "%'";
                    var cmd = new SqlCommand(query, con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandTimeout = 0;

                    con.Open();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (reader["Count"] != DBNull.Value)
                            totalRecords = UInt64.Parse(reader["Count"].ToString());
                    }

                    con.Close();
                }
                return totalRecords;
            });
        }

        #endregion

        #region ProductTypeMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ProductTypeMapping UpdateProductTypeMapping(ProductTypeMapping productTypeMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductTypeMappingRepository productTypeMappingRepository = _DataRepositoryFactory.GetDataRepository<IProductTypeMappingRepository>();

                ProductTypeMapping updatedEntity = null;

                if (productTypeMapping.ProductTypeMappingId == 0)
                    updatedEntity = productTypeMappingRepository.Add(productTypeMapping);
                else
                    updatedEntity = productTypeMappingRepository.Update(productTypeMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteProductTypeMapping(int productTypeMappingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductTypeMappingRepository productTypeMappingRepository = _DataRepositoryFactory.GetDataRepository<IProductTypeMappingRepository>();

                productTypeMappingRepository.Remove(productTypeMappingId);
            });
        }

        public ProductTypeMapping GetProductTypeMapping(int productTypeMappingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductTypeMappingRepository productTypeMappingRepository = _DataRepositoryFactory.GetDataRepository<IProductTypeMappingRepository>();

                ProductTypeMapping productTypeMappingEntity = productTypeMappingRepository.Get(productTypeMappingId);
                if (productTypeMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ProductTypeMapping with ID of {0} is not in database", productTypeMappingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return productTypeMappingEntity;
            });
        }

        public ProductTypeMapping[] GetAllProductTypeMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductTypeMappingRepository productTypeMappingRepository = _DataRepositoryFactory.GetDataRepository<IProductTypeMappingRepository>();

                IEnumerable<ProductTypeMapping> productTypeMappings = productTypeMappingRepository.Get();

                return productTypeMappings.ToArray();
            });
        }

        public ProductTypeMappingData[] GetProductTypeMappingByProduct(string productCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductTypeMappingRepository productTypeMappingRepository = _DataRepositoryFactory.GetDataRepository<IProductTypeMappingRepository>();

                List<ProductTypeMappingData> productTypeMappings = new List<ProductTypeMappingData>();
                IEnumerable<ProductTypeMappingInfo> productTypeMappingInfos = productTypeMappingRepository.GetProductTypeMappingByProduct(productCode).OrderBy(c => c.ProductType.Name).ToArray();

                foreach (var productTypeMappingInfo in productTypeMappingInfos)
                {
                    productTypeMappings.Add(
                        new ProductTypeMappingData
                        {
                            ProductTypeMappingId = productTypeMappingInfo.ProductTypeMapping.EntityId,
                            ProductId = productTypeMappingInfo.Product.EntityId,
                            ProductCode = productTypeMappingInfo.Product.Code,
                            ProductName = productTypeMappingInfo.Product.Name,
                            ProductTypeId = productTypeMappingInfo.ProductType.EntityId,
                            ProductTypeName = productTypeMappingInfo.ProductType.Name,
                            Active = productTypeMappingInfo.ProductTypeMapping.Active
                        });
                }

                return productTypeMappings.ToArray();
            });
        }

        #endregion

        #region ChartOfAccount operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ChartOfAccount UpdateChartOfAccount(ChartOfAccount chartOfAccount)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IChartOfAccountRepository chartOfAccountRepository = _DataRepositoryFactory.GetDataRepository<IChartOfAccountRepository>();

                ChartOfAccount updatedEntity = null;

                if (chartOfAccount.ChartOfAccountId == 0)
                    updatedEntity = chartOfAccountRepository.Add(chartOfAccount);
                else
                    updatedEntity = chartOfAccountRepository.Update(chartOfAccount);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteChartOfAccount(int chartOfAccountId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IChartOfAccountRepository chartOfAccountRepository = _DataRepositoryFactory.GetDataRepository<IChartOfAccountRepository>();

                chartOfAccountRepository.Remove(chartOfAccountId);
            });
        }

        public ChartOfAccount GetChartOfAccount(int chartOfAccountId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IChartOfAccountRepository chartOfAccountRepository = _DataRepositoryFactory.GetDataRepository<IChartOfAccountRepository>();

                ChartOfAccount chartOfAccountEntity = chartOfAccountRepository.Get(chartOfAccountId);
                if (chartOfAccountEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ChartOfAccount with ID of {0} is not in database", chartOfAccountId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return chartOfAccountEntity;
            });
        }

        public ChartOfAccount[] GetAllChartOfAccounts()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IChartOfAccountRepository chartOfAccountRepository = _DataRepositoryFactory.GetDataRepository<IChartOfAccountRepository>();

                IEnumerable<ChartOfAccount> chartOfAccounts = chartOfAccountRepository.Get();

                return chartOfAccounts.ToArray();
            });
        }

        public ChartOfAccountData[] GetChartOfAccounts()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IChartOfAccountRepository chartOfAccountRepository = _DataRepositoryFactory.GetDataRepository<IChartOfAccountRepository>();

                List<ChartOfAccountData> chartOfAccounts = new List<ChartOfAccountData>();

                IEnumerable<ChartOfAccountInfo> chartOfAccountInfos = chartOfAccountRepository.GetChartOfAccountInfo().ToArray();

                foreach (var chartOfAccountInfo in chartOfAccountInfos)
                {
                    chartOfAccounts.Add(
                        new ChartOfAccountData
                        {
                            FinancialTypeId=chartOfAccountInfo.FinancialType.FinancialTypeId,
                            FinancialTypeName = chartOfAccountInfo.FinancialType != null ? chartOfAccountInfo.FinancialType.Name : "",
                            ChartOfAccountId = chartOfAccountInfo.ChartOfAccount.ChartOfAccountId,
                            AccountType = chartOfAccountInfo.ChartOfAccount.AccountType,
                            AccountTypeName = chartOfAccountInfo.ChartOfAccount.AccountType.ToString(),
                            AccountCode = chartOfAccountInfo.ChartOfAccount.AccountCode,
                            AccountName = chartOfAccountInfo.ChartOfAccount.AccountName,
                            ParentId = chartOfAccountInfo.ChartOfAccount.ParentId,
                            ParentName = chartOfAccountInfo.Parent != null ? chartOfAccountInfo.Parent.AccountName : "",
                            IfrsCaption = chartOfAccountInfo.ChartOfAccount.IFRS,
                            Position = chartOfAccountInfo.ChartOfAccount.Position,
                            Active = chartOfAccountInfo.ChartOfAccount.Active,
                            LongDescription = string.Format("{0} - {1}", chartOfAccountInfo.ChartOfAccount.AccountCode, chartOfAccountInfo.ChartOfAccount.AccountName
                            )
                        });
                }

                return chartOfAccounts.ToArray();
            });
        }

        #endregion

        #region Currency operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Currency UpdateCurrency(Currency currency)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICurrencyRepository currencyRepository = _DataRepositoryFactory.GetDataRepository<ICurrencyRepository>();

                Currency updatedEntity = null;

                if (currency.CurrencyId == 0)
                    updatedEntity = currencyRepository.Add(currency);
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
                var groupNames = new List<string>() { GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICurrencyRepository currencyRepository = _DataRepositoryFactory.GetDataRepository<ICurrencyRepository>();

                currencyRepository.Remove(currencyId);
            });
        }

        public Currency GetCurrency(int currencyId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

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
        public Currency[] GetBaseCurrency()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICurrencyRepository currencyRepository = _DataRepositoryFactory.GetDataRepository<ICurrencyRepository>();

                IEnumerable<Currency> currency = currencyRepository.Get().Where(c => c.IsBase);

                return currency.ToArray();
            });
        }


        public Currency[] GetAllCurrencies()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICurrencyRepository currencyRepository = _DataRepositoryFactory.GetDataRepository<ICurrencyRepository>();

                IEnumerable<Currency> currencies = currencyRepository.Get().OrderByDescending(c=>c.CurrencyId);

                return currencies.ToArray();
            });
        }

        #endregion

        #region RateType operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public RateType UpdateRateType(RateType rateType)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRateTypeRepository rateTypeRepository = _DataRepositoryFactory.GetDataRepository<IRateTypeRepository>();

                RateType updatedEntity = null;

                if (rateType.RateTypeId == 0)
                    updatedEntity = rateTypeRepository.Add(rateType);
                else
                    updatedEntity = rateTypeRepository.Update(rateType);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteRateType(int rateTypeId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRateTypeRepository rateTypeRepository = _DataRepositoryFactory.GetDataRepository<IRateTypeRepository>();

                rateTypeRepository.Remove(rateTypeId);
            });
        }

        public RateType GetRateType(int rateTypeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRateTypeRepository rateTypeRepository = _DataRepositoryFactory.GetDataRepository<IRateTypeRepository>();

                RateType rateTypeEntity = rateTypeRepository.Get(rateTypeId);
                if (rateTypeEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("RateType with ID of {0} is not in database", rateTypeId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return rateTypeEntity;
            });
        }

        public RateType[] GetAllRateTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRateTypeRepository rateTypeRepository = _DataRepositoryFactory.GetDataRepository<IRateTypeRepository>();

                IEnumerable<RateType> rateTypes = rateTypeRepository.Get();

                return rateTypes.ToArray();
            });
        }

        #endregion

        #region CurrencyRate operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CurrencyRate UpdateCurrencyRate(CurrencyRate currencyRate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICurrencyRateRepository currencyRateRepository = _DataRepositoryFactory.GetDataRepository<ICurrencyRateRepository>();

                CurrencyRate updatedEntity = null;

                if (currencyRate.CurrencyRateId == 0)
                    updatedEntity = currencyRateRepository.Add(currencyRate);
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
                var groupNames = new List<string>() { GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICurrencyRateRepository currencyRateRepository = _DataRepositoryFactory.GetDataRepository<ICurrencyRateRepository>();

                currencyRateRepository.Remove(currencyRateId);
            });
        }

        public CurrencyRate GetCurrencyRate(int currencyRateId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

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

        public CurrencyRate[] GetCurrencyRateByDate(string curSymbol)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICurrencyRateRepository currencyRateRepository = _DataRepositoryFactory.GetDataRepository<ICurrencyRateRepository>();
                ISolutionRunDateRepository runDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();

                var runDate = runDateRepository.GetSolutionRunDates().Where(c => c.SolutionRunDate.SolutionId == GetSolution(SOL_NAME).EntityId).FirstOrDefault();
                 List<CurrencyRate> currencyRates = new List<CurrencyRate>();

                IEnumerable<CurrencyRateInfo> currencyRateInfos = currencyRateRepository.GetCurrencyRateByDate(runDate.SolutionRunDate.RunDate, curSymbol).ToArray();
       
                foreach (var currencyRateInfo in currencyRateInfos)
                {
                    currencyRates.Add(
                        new CurrencyRate
                        {
                            CurrencyRateId = currencyRateInfo.CurrencyRate.EntityId,
                            CurrencyId = currencyRateInfo.Currency.EntityId,                          
                            Rate = currencyRateInfo.CurrencyRate.Rate,
                            Date = currencyRateInfo.CurrencyRate.Date,
                            Active = currencyRateInfo.CurrencyRate.Active
                        });
                }

                return currencyRates.ToArray();
            });
        }

        public CurrencyRateData[] GetCurrencyRates(int currencyId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICurrencyRateRepository currencyRateRepository = _DataRepositoryFactory.GetDataRepository<ICurrencyRateRepository>();

                List<CurrencyRateData> currencyRates = new List<CurrencyRateData>();

                IEnumerable<CurrencyRateInfo> currencyRateInfos = currencyRateRepository.GetCurrencyRateByCurrencyId(currencyId).ToArray();

                foreach (var currencyRateInfo in currencyRateInfos)
                {
                    currencyRates.Add(
                        new CurrencyRateData
                        {
                            CurrencyRateId = currencyRateInfo.CurrencyRate.EntityId,
                            CurrencyId = currencyRateInfo.Currency.EntityId,
                            CurrencyName = currencyRateInfo.Currency.Name,
                            RateTypeId = currencyRateInfo.RateType.EntityId,
                            Frequency = currencyRateInfo.CurrencyRate.Frequency,
                            RateTypeName = currencyRateInfo.RateType.Name,
                            Rate = currencyRateInfo.CurrencyRate.Rate,
                            Date = currencyRateInfo.CurrencyRate.Date,
                            Rundate = currencyRateInfo.CurrencyRate.Rundate,
                            Active = currencyRateInfo.CurrencyRate.Active
                        });
                }

                return currencyRates.ToArray();
            });
        }

        #endregion

        #region Branch operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Branch UpdateBranch(Branch branch)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBranchRepository branchRepository = _DataRepositoryFactory.GetDataRepository<IBranchRepository>();

                Branch updatedEntity = null;

                if (branch.BranchId == 0)
                    updatedEntity = branchRepository.Add(branch);
                else
                    updatedEntity = branchRepository.Update(branch);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBranch(int branchId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBranchRepository branchRepository = _DataRepositoryFactory.GetDataRepository<IBranchRepository>();

                branchRepository.Remove(branchId);
            });
        }

        public Branch GetBranch(int branchId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBranchRepository branchRepository = _DataRepositoryFactory.GetDataRepository<IBranchRepository>();

                Branch branchEntity = branchRepository.Get(branchId);
                if (branchEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Branch with ID of {0} is not in database", branchId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return branchEntity;
            });
        }
     
        public Branch[] GetAllBranches()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBranchRepository branchRepository = _DataRepositoryFactory.GetDataRepository<IBranchRepository>();

                IEnumerable<Branch> branches = branchRepository.Get();

                return branches.ToArray();
            });
        }

        #endregion

        #region FinancialType operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public FinancialType UpdateFinancialType(FinancialType financialType)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFinancialTypeRepository financialTypeRepository = _DataRepositoryFactory.GetDataRepository<IFinancialTypeRepository>();

                FinancialType updatedEntity = null;

                if (financialType.FinancialTypeId == 0)
                    updatedEntity = financialTypeRepository.Add(financialType);
                else
                    updatedEntity = financialTypeRepository.Update(financialType);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteFinancialType(int financialTypeId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFinancialTypeRepository financialTypeRepository = _DataRepositoryFactory.GetDataRepository<IFinancialTypeRepository>();

                financialTypeRepository.Remove(financialTypeId);
            });
        }

        public FinancialType GetFinancialType(int financialTypeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFinancialTypeRepository financialTypeRepository = _DataRepositoryFactory.GetDataRepository<IFinancialTypeRepository>();

                FinancialType financialTypeEntity = financialTypeRepository.Get(financialTypeId);
                if (financialTypeEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("FinancialType with ID of {0} is not in database", financialTypeId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return financialTypeEntity;
            });
        }

        public FinancialTypeData[] GetFinancialTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFinancialTypeRepository financialTypeRepository = _DataRepositoryFactory.GetDataRepository<IFinancialTypeRepository>();

                List<FinancialTypeData> financialTypes = new List<FinancialTypeData>();

                IEnumerable<FinancialTypeInfo> financialTypeInfos = financialTypeRepository.GetFinancialTypes().ToArray();

                foreach (var financialTypeInfo in financialTypeInfos)
                {
                    financialTypes.Add(
                        new FinancialTypeData
                        {
                            FinancialTypeId = financialTypeInfo.FinancialType.EntityId,
                            Code = financialTypeInfo.FinancialType.Code,
                            Name     = financialTypeInfo.FinancialType.Name,
                            ParentId = financialTypeInfo.FinancialType.ParentId,
                            ParentName = financialTypeInfo.Parent != null ? financialTypeInfo.Parent.Name: string.Empty,
                            Active = financialTypeInfo.FinancialType.Active
                        });
                }

                return financialTypes.ToArray();
            });
        }

        #endregion
   
        #region GLDefinition operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public GLDefinition UpdateGLDefinition(GLDefinition glDefinition)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLDefinitionRepository glDefinitionRepository = _DataRepositoryFactory.GetDataRepository<IGLDefinitionRepository>();

                GLDefinition updatedEntity = null;

                if (glDefinition.GLDefinitionId == 0)
                {

                    updatedEntity = glDefinitionRepository.Add(glDefinition);
                }
                else
                    updatedEntity = glDefinitionRepository.Update(glDefinition);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteGLDefinition(int glDefinitionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLDefinitionRepository glDefinitionRepository = _DataRepositoryFactory.GetDataRepository<IGLDefinitionRepository>();

                glDefinitionRepository.Remove(glDefinitionId);
            });
        }

        public GLDefinition GetGLDefinition(int glDefinitionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLDefinitionRepository glDefinitionRepository = _DataRepositoryFactory.GetDataRepository<IGLDefinitionRepository>();

                GLDefinition glDefinitionEntity = glDefinitionRepository.Get(glDefinitionId);
                if (glDefinitionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("GLDefinition with ID of {0} is not in database", glDefinitionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return glDefinitionEntity;
            });
        }

        public GLDefinition[] GetAllGLDefinitions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLDefinitionRepository glDefinitionRepository = _DataRepositoryFactory.GetDataRepository<IGLDefinitionRepository>();


                IEnumerable<GLDefinition> glDefinition = glDefinitionRepository.Get().ToArray();

                return glDefinition.ToArray();
            });
        }

        #endregion

        #region Staff operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Staff UpdateStaff(Staff staff)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IStaffRepository staffRepository = _DataRepositoryFactory.GetDataRepository<IStaffRepository>();

                Staff updatedEntity = null;

                if (staff.StaffId == 0)
                    updatedEntity = staffRepository.Add(staff);
                else
                    updatedEntity = staffRepository.Update(staff);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteStaff(int staffId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IStaffRepository staffRepository = _DataRepositoryFactory.GetDataRepository<IStaffRepository>();

                staffRepository.Remove(staffId);
            });
        }

        public Staff GetStaff(int staffId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IStaffRepository staffRepository = _DataRepositoryFactory.GetDataRepository<IStaffRepository>();

                Staff staffEntity = staffRepository.Get(staffId);
                if (staffEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Staff with ID of {0} is not in database", staffId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return staffEntity;
            });
        }

        public Staff[] GetAllStaffs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IStaffRepository staffRepository = _DataRepositoryFactory.GetDataRepository<IStaffRepository>();

                IEnumerable<Staff> staffs = staffRepository.Get().Where(c => c.Active);

                return staffs.ToArray();
            });
        }

        #endregion

        #region PayGrade operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PayGrade UpdatePayGrade(PayGrade payGrade)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPayGradeRepository payGradeRepository = _DataRepositoryFactory.GetDataRepository<IPayGradeRepository>();

                PayGrade updatedEntity = null;

                if (payGrade.PayGradeId == 0)
                    updatedEntity = payGradeRepository.Add(payGrade);
                else
                    updatedEntity = payGradeRepository.Update(payGrade);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePayGrade(int payGradeId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPayGradeRepository payGradeRepository = _DataRepositoryFactory.GetDataRepository<IPayGradeRepository>();

                payGradeRepository.Remove(payGradeId);
            });
        }

        public PayGrade GetPayGrade(int payGradeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPayGradeRepository payGradeRepository = _DataRepositoryFactory.GetDataRepository<IPayGradeRepository>();

                PayGrade payGradeEntity = payGradeRepository.Get(payGradeId);
                if (payGradeEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PayGrade with ID of {0} is not in database", payGradeId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return payGradeEntity;
            });
        }

        public PayGrade[] GetAllPayGrades()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPayGradeRepository payGradeRepository = _DataRepositoryFactory.GetDataRepository<IPayGradeRepository>();

                IEnumerable<PayGrade> payGrades = payGradeRepository.Get().Where(c => c.Active);

                return payGrades.ToArray();
            });
        }

        #endregion

        #region AuditTrail operations

        public audit.AuditTrail GetAuditTrail(int auditTrailId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                var auditManager = new audit.AuditManager();

                audit.AuditTrail auditTrailEntity = auditManager.Get(auditTrailId);
                if (auditTrailEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("AuditTrail with ID of {0} is not in database", auditTrailId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return auditTrailEntity;
            });
        }

        public audit.AuditTrail[] GetAllAuditTrails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                var auditManager = new audit.AuditManager();
                IEnumerable<audit.AuditTrail> auditTrails = auditManager.Get();

                return auditTrails.ToArray();
            });
        }

        public audit.AuditTrail[] GetAuditTrails(DateTime fromDate, DateTime toDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                var auditManager = new audit.AuditManager();
                IEnumerable<audit.AuditTrail> auditTrails = auditManager.GetByDate(fromDate, toDate);

                return auditTrails.ToArray();
            });
        }

        public audit.AuditTrail[] GetAuditTrailByTable(string tableName, DateTime fromDate, DateTime toDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                var auditManager = new audit.AuditManager();
                IEnumerable<audit.AuditTrail> auditTrails = auditManager.GetByTable(tableName, fromDate, toDate);

                return auditTrails.ToArray();
            });
        }

        public audit.AuditTrail[] GetAuditTrailByUser(string userName, DateTime fromDate, DateTime toDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                var auditManager = new audit.AuditManager();
                IEnumerable<audit.AuditTrail> auditTrails = auditManager.GetByLoginID(userName, fromDate, toDate);

                return auditTrails.ToArray();
            });
        }

        public audit.AuditTrail[] GetAuditTrailByAction(string action, DateTime fromDate, DateTime toDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                var auditManager = new audit.AuditManager();
                List<audit.AuditTrail> auditTrails = auditManager.GetByAction(action, fromDate, toDate);

                return auditTrails.ToArray();
            });
        }


        public audit.AuditTrail[] GetAuditTrailByTab(audit.AuditAction action)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                var auditManager = new audit.AuditManager();
                IEnumerable<audit.AuditTrail> auditTrails = auditManager.GetAuditTrailByTab(action);

                return auditTrails.ToArray();
            });
        }

        #endregion

        #region ReportStatus operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ReportStatus UpdateReportStatus(ReportStatus reportStatus)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IReportStatusRepository reportStatusRepository = _DataRepositoryFactory.GetDataRepository<IReportStatusRepository>();

                ReportStatus updatedEntity = null;

                if (reportStatus.StatusId == 0)
                    updatedEntity = reportStatusRepository.Add(reportStatus);
                else
                    updatedEntity = reportStatusRepository.Update(reportStatus);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteReportStatus(int statusId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IReportStatusRepository reportStatusRepository = _DataRepositoryFactory.GetDataRepository<IReportStatusRepository>();

                reportStatusRepository.Remove(statusId);
            });
        }

        public ReportStatus GetReportStatus(int statusId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IReportStatusRepository reportStatusRepository = _DataRepositoryFactory.GetDataRepository<IReportStatusRepository>();

                ReportStatus reportStatusEntity = reportStatusRepository.Get(statusId);
                if (reportStatusEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ReportStatus with ID of {0} is not in database", statusId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return reportStatusEntity;
            });
        }

        public ReportStatusData[] GetAllReportStatus()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IReportStatusRepository reportStatusRepository = _DataRepositoryFactory.GetDataRepository<IReportStatusRepository>();
                var solutions = GetSolutions();
                List<ReportStatusData> reportStatus = new List<ReportStatusData>();
                
                IEnumerable<ReportStatusInfo> reportstatusInfos = reportStatusRepository.GetAllReportStatus().ToArray();

                foreach (var reportstatusInfo in reportstatusInfos)
                {
                    reportStatus.Add(
                        new ReportStatusData
                        { 
                            StatusId=reportstatusInfo.ReportStatus.StatusId,
                            SolutionId = reportstatusInfo.ReportStatus.SolutionId,
                            SolutionName = GetSolutionName(solutions, reportstatusInfo.ReportStatus.SolutionId),
                            Closed = reportstatusInfo.ReportStatus.Closed,
                            Active = reportstatusInfo.ReportStatus.Active
                             
                        });
                }
                
                return reportStatus.ToArray();
            });
        }

        protected string GetSolutionName(Solution[] solutions, int solutionId)
        {
            foreach (var solution in solutions)
            {
                if (solution.SolutionId == solutionId)
                    return solution.Alias;
            }

            return string.Empty;
        }
        #endregion

        protected Solution[] GetSolutions()
        {
            ISolutionRepository solutionRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRepository>();

            var solutions = solutionRepository.Get();

            return solutions.ToArray();
        }


        #region DefaultUser operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public DefaultUser UpdateDefaultUser(DefaultUser defaultUser)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IDefaultUserRepository defaultUserRepository = _DataRepositoryFactory.GetDataRepository<IDefaultUserRepository>();

                DefaultUser updatedEntity = null;

                if (defaultUser.DefaultUserId == 0)
                    updatedEntity = defaultUserRepository.Add(defaultUser);
                else
                    updatedEntity = defaultUserRepository.Update(defaultUser);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteDefaultUser(int defaultUserId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IDefaultUserRepository defaultUserRepository = _DataRepositoryFactory.GetDataRepository<IDefaultUserRepository>();

                defaultUserRepository.Remove(defaultUserId);
            });
        }

        public DefaultUser GetDefaultUser(int defaultUserId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IDefaultUserRepository defaultUserRepository = _DataRepositoryFactory.GetDataRepository<IDefaultUserRepository>();

                DefaultUser defaultUserEntity = defaultUserRepository.Get(defaultUserId);
                if (defaultUserEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("DefaultUser with ID of {0} is not in database", defaultUserId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return defaultUserEntity;
            });
        }

        public DefaultUserData[] GetAllDefaultUsers()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IDefaultUserRepository defaultUserRepository = _DataRepositoryFactory.GetDataRepository<IDefaultUserRepository>();
                var solutions = GetSolutions();
                List<DefaultUserData> defaultUsers = new List<DefaultUserData>();

                IEnumerable<DefaultUserInfo> defaultusersInfos = defaultUserRepository.GetDefaultUseres().ToArray();

                foreach (var defaultusersInfo in defaultusersInfos)
                {
                    defaultUsers.Add(
                        new DefaultUserData
                        {
                            DefaultUserId = defaultusersInfo.DefaultUser.DefaultUserId,
                            SolutionId = defaultusersInfo.DefaultUser.SolutionId,
                            SolutionName = GetSolutionName(solutions, defaultusersInfo.DefaultUser.SolutionId),
                            LoginID = defaultusersInfo.DefaultUser.LoginID,
                            Active = defaultusersInfo.DefaultUser.Active

                        });
                }

                return defaultUsers.ToArray();
            });
        }


        //public ReportStatusData[] GetAllReportStatus()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_BUSINESS, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IReportStatusRepository reportStatusRepository = _DataRepositoryFactory.GetDataRepository<IReportStatusRepository>();
        //        var solutions = GetSolutions();
        //        List<ReportStatusData> reportStatus = new List<ReportStatusData>();

        //        IEnumerable<ReportStatusInfo> reportstatusInfos = reportStatusRepository.GetAllReportStatus().ToArray();

        //        foreach (var reportstatusInfo in reportstatusInfos)
        //        {
        //            reportStatus.Add(
        //                new ReportStatusData
        //                {
        //                    StatusId = reportstatusInfo.ReportStatus.StatusId,
        //                    SolutionId = reportstatusInfo.ReportStatus.SolutionId,
        //                    SolutionName = GetSolutionName(solutions, reportstatusInfo.ReportStatus.SolutionId),
        //                    Closed = reportstatusInfo.ReportStatus.Closed,
        //                    Active = reportstatusInfo.ReportStatus.Active

        //                });
        //        }

        //        return reportStatus.ToArray();
        //    });
        //}

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

        protected Solution GetSolution(string name)
        {
            ISolutionRepository solutionRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRepository>();

            var solution = solutionRepository.Get().Where(c => c.Name == name).FirstOrDefault();

            return solution;
        }


        #endregion

        public string GetDataConnection()
        {
            string connectionString = "";

            if (!string.IsNullOrEmpty(DataConnector.CompanyCode))
            {
                IDatabaseRepository databaseRepository = _DataRepositoryFactory.GetDataRepository<IDatabaseRepository>();
                var companydb = databaseRepository.Get().Where(c => c.CompanyCode == DataConnector.CompanyCode).FirstOrDefault();

                if (companydb == null)
                    throw new Exception("Unable to load company database.");

                string companydbServerName;
                string companydbDatabaseName;
                string companydbUserName;
                string companydbPassword;

                if (companydb.Title == "Encrypted")
                {
                    var key = Properties.Resources.Key;
                    companydbServerName = CryptoEngine.Decrypt(companydb.ServerName, key);
                    companydbDatabaseName = CryptoEngine.Decrypt(companydb.DatabaseName, key);
                    companydbUserName = CryptoEngine.Decrypt(companydb.UserName, key);
                    companydbPassword = CryptoEngine.Decrypt(companydb.Password, key);
                }
                else
                {
                    companydbServerName = companydb.ServerName;
                    companydbDatabaseName = companydb.DatabaseName;
                    companydbUserName = companydb.UserName;
                    companydbPassword = companydb.Password;
                }

                connectionString = string.Format("Data Source= {0};Initial Catalog={1};User ={2};Password={3};Integrated Security={4}", companydbServerName, companydbDatabaseName, companydbUserName, companydbPassword, companydb.IntegratedSecurity);
            }

            return connectionString;
        }
    }
}
