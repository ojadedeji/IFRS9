using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq;
using System.ServiceModel;
using System.Transactions;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Presentation.WebClient.Models;
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
using Fintrak.Data.IFRS;

using systemCoreFramework = Fintrak.Shared.SystemCore.Framework;
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.Common.Data;
using DecryptOperation;

namespace Fintrak.Business.IFRS.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]

    public class IFRSDataViewManager : ManagerBase, IIFRSDataViewService
    {
        public IFRSDataViewManager()
        {
        }

        public IFRSDataViewManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        const string SOLUTION_NAME = "FIN_IFRS";
        const string SOLUTION_ALIAS = "IFRS";
        const string MODULE_NAME = "FIN_IFRS_DATA_VIEW";
        const string MODULE_ALIAS = "IFRS Processed Data";

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
                        var root = menuRepository.Get().Where(c => c.Alias == "IFRS Processed Data").FirstOrDefault();
                        //Role
                        var adminRole = roleRepository.Get().Where(c => c.Name == GROUP_ADMINISTRATOR && c.SolutionId == solution.SolutionId).FirstOrDefault();
                        var userRole = roleRepository.Get().Where(c => c.Name == GROUP_USER && c.SolutionId == solution.SolutionId).FirstOrDefault();


                        //register menu
                        var actionMenu = new systemCoreEntities.Menu()
                         {
                             Name = "IFRS_BOND_PERIODIC_SCHEDULE",
                             Alias = "Bond Periodic Data",
                             Action = "IFRS_BOND_PERIODIC_SCHEDULE",
                             ActionUrl = "ifrs-bondperiodicschedule-list",
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
                           Name = "DAILY_BOND_AMORTIZATION",
                           Alias = "Daily Bond Amortization",
                           Action = "DAILY_BOND_AMORTIZATION",
                           ActionUrl = "ifrs-bondamortization-list",
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
                            Name = "ZERO_COUPON_BOND",
                            Alias = "Zero Coupon Bond",
                            Action = "ZERO_COUPON_BOND",
                            ActionUrl = "ifrs-zerocouponbond-list",
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
                            Name = "LOAN_PERIODIC_SCHEDULE",
                            Alias = "Loan Periodic Schedule",
                            Action = "LOAN_PERIODIC_SCHEDULE",
                            ActionUrl = "ifrs-loanperiodicschedule-list",
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
                            Name = "LOAN_DAILY_SCHEDULE",
                            Alias = "Loan Daily Schedule",
                            Action = "LOAN_DAILY_SCHEDULE",
                            ActionUrl = "ifrs-loandailyschedule-list",
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
                            Name = "LOAN_IMAPIRMENT_RESULT",
                            Alias = "Loan Impairment Result",
                            Action = "LOAN_IMAPIRMENT_RESULT",
                            ActionUrl = "ifrs-loanimpairmentresult-list",
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
                            Name = "TREASURY_BILL_COMPUTATION",
                            Alias = "Treasury Bills",
                            Action = "TREASURY_BILL_COMPUTATION",
                            ActionUrl = "ifrs-treasurybillscomputation-list",
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
                            Name = "EQUITY_STOCK_COMPUTATION",
                            Alias = "Equity Stock",
                            Action = "EQUITY_STOCK_COMPUTATION",
                            ActionUrl = "ifrs-equitystock-list",
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
                            Name = "BOND_CONSOLIDATED",
                            Alias = "Bond Consolidated",
                            Action = "BOND_CONSOLIDATED",
                            ActionUrl = "ifrs-bondconsolidateddata-list",
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
                            Name = "LOANS_CONSOLIDATED",
                            Alias = "Loans Consolidated",
                            Action = "LOANS_CONSOLIDATED",
                            ActionUrl = "ifrs-loanconsolidateddata-list",
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


        #region BondComputationResult operations

        [OperationBehavior(TransactionScopeRequired = true)]

        public BondComputation[] GetAllBondComputations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondComputationRepository bondComputationRepository = _DataRepositoryFactory.GetDataRepository<IBondComputationRepository>();

                IEnumerable<BondComputation> bondComputations = bondComputationRepository.Get().ToArray();

                return bondComputations.ToArray();
            });
        }


        public BondComputation[] GetBondComputationResultDistinctRefNo()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondComputationRepository bondComputationRepository = _DataRepositoryFactory.GetDataRepository<IBondComputationRepository>();

                IEnumerable<BondComputation> bondComputations = bondComputationRepository.Get().ToArray();

                //  var bComputation = bondComputations.Select(c => c.RefNo).Distinct();

                return bondComputations.ToArray();
            });
        }
        public BondComputation[] GetBondComputationResultbyRefNo(string refNo, DateTime? Date, string path)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondComputationRepository bondComputationRepository = _DataRepositoryFactory.GetDataRepository<IBondComputationRepository>();

                BondComputation[] bondComputations = bondComputationRepository.GetBondComputationResultbyRefNo(refNo, Date, path).ToArray();

                return bondComputations.ToArray();
            });
        }

        public BondComputation[] GetRefNoBondComputation()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondComputationRepository BondComputationRepository = _DataRepositoryFactory.GetDataRepository<IBondComputationRepository>();
                List<BondComputation> BondComputations = BondComputationRepository.GetDistinctRefNo();


                return BondComputations.ToArray();
            });
        }

        public string[] GetDistinctRefNo()
        {
            //var connectionString = IFRSContext.GetDataConnection();
            var connectionString = GetDataConnection();

            List<string> refno;
            var refnoList = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("Get_Distinct_ifrs_bond_computation_result", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var myRefNo = new ReferenceNoModel();
                        if (reader["RefNo"] != DBNull.Value)
                            myRefNo.RefNo = reader["RefNo"].ToString();
                        refnoList.Add(myRefNo.RefNo);
                    }
                    reader.Close();
                    con.Close();
                }

                con.Close();
            }
            return refnoList.ToArray();
        }

        #endregion

        #region BondPeriodicSchedule operations

        [OperationBehavior(TransactionScopeRequired = true)]


        public BondPeriodicSchedule[] GetBondPeriodicSchedulebyRefNo(string refNo, string path)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondPeriodicScheduleRepository bondPeriodicScheduleMapRepository = _DataRepositoryFactory.GetDataRepository<IBondPeriodicScheduleRepository>();

                //BondPeriodicSchedule[] bondPeriodicScheduleEntity = bondPeriodicScheduleMapRepository.Get().Where(c => c.RefNo == refNo).OrderBy(c => c.Num_Pmt).ToArray();
                BondPeriodicSchedule[] bondPeriodicScheduleEntity = bondPeriodicScheduleMapRepository.GetBondPeriodicScheduleRefNos(refNo, path).ToArray();


                if (bondPeriodicScheduleEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BondPeriodicSchedule with ID of {0} is not in database", refNo));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return bondPeriodicScheduleEntity;
            });
        }

        public IEnumerable<string> GetDistinctBondPeriodicScheduleRefNos()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondPeriodicScheduleRepository bondPeriodicScheduleRepository = _DataRepositoryFactory.GetDataRepository<IBondPeriodicScheduleRepository>();

                IEnumerable<string> bondPeriodicSchedules = bondPeriodicScheduleRepository.GetDistinctBondPeriodicScheduleRefNos().ToArray();

                return bondPeriodicSchedules.ToArray();
            });
        }

        public BondPeriodicSchedule[] GetAllBondPeriodicSchedules()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondPeriodicScheduleRepository bondPeriodicScheduleRepository = _DataRepositoryFactory.GetDataRepository<IBondPeriodicScheduleRepository>();

                IEnumerable<BondPeriodicSchedule> bondPeriodicSchedules = bondPeriodicScheduleRepository.Get().ToArray();

                return bondPeriodicSchedules.ToArray();
            });
        }


        public BondPeriodicSchedule[] GetBondPeriodicScheduleDistinctRefNo()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondPeriodicScheduleRepository bondPeriodicScheduleRepository = _DataRepositoryFactory.GetDataRepository<IBondPeriodicScheduleRepository>();

                IEnumerable<BondPeriodicSchedule> bondPeriodicSchedules = bondPeriodicScheduleRepository.Get().ToArray();

                return bondPeriodicSchedules.ToArray();
            });
        }

        #endregion

        #region BondComputationResultZero operations

        [OperationBehavior(TransactionScopeRequired = true)]


        public BondComputationResultZero[] GetBondComputationResultZerobyRefNo(string refNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondComputationResultZeroRepository bondComputationResultZeroMapRepository = _DataRepositoryFactory.GetDataRepository<IBondComputationResultZeroRepository>();

                BondComputationResultZero[] bondComputationResultZeroEntity = bondComputationResultZeroMapRepository.Get().Where(c => c.RefNo == refNo).OrderBy(c => c.Day).ToArray();
                if (bondComputationResultZeroEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BondComputationResultZero with ID of {0} is not in database", refNo));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return bondComputationResultZeroEntity;
            });
        }

        public BondComputationResultZero[] GetBondComputationResultZerobyRefNos(string refNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondComputationResultZeroRepository bondComputationResultZeroMapRepository = _DataRepositoryFactory.GetDataRepository<IBondComputationResultZeroRepository>();

                BondComputationResultZero[] bondComputationResultZeroEntity = bondComputationResultZeroMapRepository.Get().Where(c => c.RefNo == refNo).OrderBy(c => c.Day).ToArray();
                if (bondComputationResultZeroEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BondComputationResultZero with ID of {0} is not in database", refNo));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return bondComputationResultZeroEntity;
            });
        }

        public BondComputationResultZero[] GetBondComputationResultZeros()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondComputationResultZeroRepository bondComputationResultZeroRepository = _DataRepositoryFactory.GetDataRepository<IBondComputationResultZeroRepository>();

                IEnumerable<BondComputationResultZero> bondComputationResultZeros = bondComputationResultZeroRepository.Get().ToArray();

                return bondComputationResultZeros.ToArray();
            });
        }


        public IEnumerable<string> GetBondComputationResultZeroDistinctRefNo()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondComputationResultZeroRepository bondComputationResultZeroRepository = _DataRepositoryFactory.GetDataRepository<IBondComputationResultZeroRepository>();

                IEnumerable<string> bondComputationResultZeros = bondComputationResultZeroRepository.GetDistinctBondComputationResultZeroRefNos().ToArray();

                return bondComputationResultZeros.ToList();
            });
        }

        public BondComputationResultZero[] GetRefNoBondComputationResultZero()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondComputationResultZeroRepository BondComputationResultZeroRepository = _DataRepositoryFactory.GetDataRepository<IBondComputationResultZeroRepository>();
                List<BondComputationResultZero> BondComputationResultZeros = BondComputationResultZeroRepository.GetDistinctRefNo();


                return BondComputationResultZeros.ToArray();
            });
        }

        //public BondComputationResultZero[] GetBondComputationResultZeroDistinctRefNo()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IBondComputationResultZeroRepository bondComputationResultZeroRepository = _DataRepositoryFactory.GetDataRepository<IBondComputationResultZeroRepository>();

        //        IEnumerable<BondComputationResultZero> bondComputationResultZeros = bondComputationResultZeroRepository.Get().ToArray();

        //        return bondComputationResultZeros.ToArray();
        //    });
        //}

        #endregion

                        
        #region LoanPeriodicSchedule operations

        [OperationBehavior(TransactionScopeRequired = true)]


        public LoanPeriodicSchedule[] GetLoanPeriodicSchedulebyRefNo(string refNo, string path)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanPeriodicScheduleRepository loanPeriodicScheduleMapRepository = _DataRepositoryFactory.GetDataRepository<ILoanPeriodicScheduleRepository>();

                LoanPeriodicSchedule[] loanPeriodicScheduleEntity = loanPeriodicScheduleMapRepository.GetLoanPeriodicSchedulebyRefNo(refNo, path).ToArray();
                if (loanPeriodicScheduleEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LoanPeriodicSchedule with ID of {0} is not in database", refNo));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return loanPeriodicScheduleEntity;
            });
        }

        public LoanPeriodicSchedule[] GetAllLoanPeriodicSchedules()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanPeriodicScheduleRepository loanPeriodicScheduleRepository = _DataRepositoryFactory.GetDataRepository<ILoanPeriodicScheduleRepository>();

                IEnumerable<LoanPeriodicSchedule> loanPeriodicSchedules = loanPeriodicScheduleRepository.Get().ToArray();

                return loanPeriodicSchedules.ToArray();
            });
        }


        public LoanPeriodicSchedule[] GetLoanPeriodicScheduleDistinctRefNo()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanPeriodicScheduleRepository loanPeriodicScheduleRepository = _DataRepositoryFactory.GetDataRepository<ILoanPeriodicScheduleRepository>();

                IEnumerable<LoanPeriodicSchedule> loanPeriodicSchedules = loanPeriodicScheduleRepository.Get().ToArray();

                return loanPeriodicSchedules.ToArray();
            });
        }

        public LoanPeriodicSchedule[] GetRefNoLoanPeriodicSchedule()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanPeriodicScheduleRepository loanPeriodicScheduleRepository = _DataRepositoryFactory.GetDataRepository<ILoanPeriodicScheduleRepository>();
                List<LoanPeriodicSchedule> loanPeriodicSchedules = loanPeriodicScheduleRepository.GetDistinctRefNo();


                return loanPeriodicSchedules.ToArray();
            });
        }
        
        public void DeleteLoanPeriodicSchedulebyRefNo(string refNo)
        {

            var connectionString = GetDataConnection();

            int status = 0;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_delete_loan_periodic_data", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "refno",
                    Value = refNo,
                });

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        public  string[] GetLoanPeriodicRefNo()
        {

            var connectionString = GetDataConnection();

            List<string> refno;
            var refnoList = new List<string>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("select distinct RefNo from ifrs_loan_periodic_schedule order by refno", con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 0;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var myRefNo = new ReferenceNoModel();
                        if (reader["RefNo"] != DBNull.Value)
                            myRefNo.RefNo = reader["RefNo"].ToString();
                        refnoList.Add(myRefNo.RefNo);
                    }
                    reader.Close();
                    con.Close();
                }
            }
            return refnoList.ToArray();
        }

        #endregion

        #region LoanDetailedInfo

        public LoanPry[] GetPryLoanBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanPryRepository loandetailsRepository = _DataRepositoryFactory.GetDataRepository<ILoanPryRepository>();

                IEnumerable<LoanPry> loandetails = loandetailsRepository.GetPryLoanBySearch(searchParam);

                return loandetails.ToArray();
            });
        }


        public RawLoanDetails[] GetAllLoanDetailsBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRawLoanDetailsRepository loandetailsRepository = _DataRepositoryFactory.GetDataRepository<IRawLoanDetailsRepository>();

                IEnumerable<RawLoanDetails> loandetails = loandetailsRepository.GetLoanDetailsBySearch(searchParam);

                return loandetails.ToArray();
            });
        }

        public CollateralDetails[] GetCollateralDetailsBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICollateralDetailsRepository CollateralDetailsRepository = _DataRepositoryFactory.GetDataRepository<ICollateralDetailsRepository>();

                IEnumerable<CollateralDetails> CollateralDetails = CollateralDetailsRepository.GetCollateralDetailsBySearch(searchParam);

                return CollateralDetails.ToArray();
            });
        }


        public LoanECLResult[] GetLoanECLResultBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILoanECLResultRepository loaneclresultRepository = _DataRepositoryFactory.GetDataRepository<ILoanECLResultRepository>();
                IEnumerable<LoanECLResult> loaneclresults = loaneclresultRepository.GetLoanECLResultBySearch(searchParam);
                return loaneclresults.ToArray();
            });
        }


        public LoansECLComputationResult[] GetLoansECLComputationResultBySearch(string searchParam, string path)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILoansECLComputationResultRepository loanseclcomputationresultRepository = _DataRepositoryFactory.GetDataRepository<ILoansECLComputationResultRepository>();
                IEnumerable<LoansECLComputationResult> loanseclcomputationresults = loanseclcomputationresultRepository.GetLoansECLComputationResultBySearch(searchParam, path);
                return loanseclcomputationresults.ToArray();
            });
        }


        public Cashflow[] GetCashflowBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ICashflowRepository cashflowRepository = _DataRepositoryFactory.GetDataRepository<ICashflowRepository>();
                IEnumerable<Cashflow> cashflows = cashflowRepository.GetCashflowBySearch(searchParam);
                return cashflows.ToArray();
            });
        }

        public IfrsMonthlyEAD[] GetIfrsMonthlyEADBySearch(string searchParam)
        {
            return ExecuteFaultHandledOperation(() => {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IIfrsMonthlyEADRepository ifrsmonthlyeadRepository = _DataRepositoryFactory.GetDataRepository<IIfrsMonthlyEADRepository>();
                IEnumerable<IfrsMonthlyEAD> ifrsmonthlyeads = ifrsmonthlyeadRepository.GetIfrsMonthlyEADBySearch(searchParam);
                return ifrsmonthlyeads.ToArray();
            });
        }

        public string[] GetDistinctLoanDetailsRefNos(int count)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRawLoanDetailsRepository loanDetailsRepository = _DataRepositoryFactory.GetDataRepository<IRawLoanDetailsRepository>();

                IEnumerable<string> loanDetails = loanDetailsRepository.GetDistinctLoanDetailsRefNos(count).ToArray();

                return loanDetails.ToArray();
            });
        }

        #endregion


        #region LoanSchedule operations

        [OperationBehavior(TransactionScopeRequired = true)]


        public LoanSchedule[] GetLoanSchedulebyRefNo(string refNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanScheduleRepository loanScheduleMapRepository = _DataRepositoryFactory.GetDataRepository<ILoanScheduleRepository>();

                LoanSchedule[] loanScheduleEntity = loanScheduleMapRepository.Get().Where(c => c.RefNo == refNo).OrderBy(c => c.AMSequence).ToArray();
                if (loanScheduleEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LoanSchedule with ID of {0} is not in database", refNo));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return loanScheduleEntity;
            });
        }

        public LoanSchedule[] GetAllLoanSchedules()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanScheduleRepository loanScheduleRepository = _DataRepositoryFactory.GetDataRepository<ILoanScheduleRepository>();

                IEnumerable<LoanSchedule> loanSchedules = loanScheduleRepository.Get().ToArray();

                return loanSchedules.ToArray();
            });
        }

        public string[] GetDistinctLoanScheduleRefNos()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanScheduleRepository loanScheduleRepository = _DataRepositoryFactory.GetDataRepository<ILoanScheduleRepository>();

                IEnumerable<string> loanSchedules = loanScheduleRepository.GetDistinctLoanScheduleRefNos().ToArray();

                return loanSchedules.ToArray();
            });
        }

        public MultiSelectDropDown[] GetLoanScheduleDistinctRefNo()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanScheduleRepository loanScheduleRepository = _DataRepositoryFactory.GetDataRepository<ILoanScheduleRepository>();

                IEnumerable<MultiSelectDropDown> loanSchedules = loanScheduleRepository.GetDistinctRefNo().ToArray();

                return loanSchedules.ToArray();
            });
        }

        //public LoanSchedule[] GetRefNo()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        ILoanScheduleRepository loanScheduleRepository = _DataRepositoryFactory.GetDataRepository<ILoanScheduleRepository>();
        //        List<LoanSchedule> loanSchedules = loanScheduleRepository.GetDistinctRefNo();


        //        return loanSchedules.ToArray();
        //    });
        //}

        //string[]  GetLoanPeriodicRefNo()
        //{

        //    var connectionString = GetDataConnection();

        //    List<string> refno ;
        //    var refnoList = new List<string>();
        //    using (var con = new SqlConnection(connectionString))
        //    {
        //        var cmd = new SqlCommand("select distinct RefNo from ifrs_loan_periodic_schedule order by refno", con);
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandTimeout = 0;

        //        con.Open();

        //        SqlDataReader reader = cmd.ExecuteReader();
        //        {
        //            while (reader.Read())
        //            {
        //                var myRefNo = new ReferenceNoModel();
        //                if (reader["RefNo"] != DBNull.Value)
        //                    myRefNo.RefNo = reader["RefNo"].ToString();
        //                refnoList.Add(myRefNo.RefNo);
        //            }
        //            reader.Close();
        //            con.Close();
        //        }
        //    }
        //    return refnoList.ToArray();
        //}

        public LoanSchedule[] GetScheduleRange(string refNo, DateTime? rangeDate, string path)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanScheduleRepository loanScheduleRepository = _DataRepositoryFactory.GetDataRepository<ILoanScheduleRepository>();

                IEnumerable<LoanSchedule> loanSchedules = loanScheduleRepository.GetScheduleRange(refNo, rangeDate, path);


                return loanSchedules.ToArray();
            });
        }

        //public LoanScheduleData[] GetScheduleRange(string refNo, DateTime rangeDate)
        //{

        //    var connectionString = GetDataConnection();

        //    //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        //    var loanSchedules = new List<LoanScheduleData>();
        //    using (var con = new SqlConnection(connectionString))
        //    {
        //        var cmd = new SqlCommand("spp_fetchScheduleRange", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 0;
        //        cmd.Parameters.Add(new SqlParameter
        //        { ParameterName = "refNo",
        //            Value = refNo
        //        });
        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "dateParam",
        //            Value = rangeDate
        //        });


        //        con.Open();

        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            var loanSchedule = new LoanScheduleData();

        //            if (reader["Id"] != DBNull.Value)
        //                loanSchedule.Id = int.Parse(reader["Id"].ToString());

        //            if (reader["Sequence"] != DBNull.Value)
        //                loanSchedule.Sequence = int.Parse(reader["Sequence"].ToString());

        //            if (reader["RefNo"] != DBNull.Value)

        //                loanSchedule.RefNo = reader["RefNo"].ToString();

        //            if (reader["Date"] != DBNull.Value)
        //                loanSchedule.Date = DateTime.Parse(reader["Date"].ToString());

        //            if (reader["PaymentDate"] != DBNull.Value)
        //                loanSchedule.PaymentDate = DateTime.Parse(reader["PaymentDate"].ToString());

        //            if (reader["OpeningBalance"] != DBNull.Value)
        //                loanSchedule.OpeningBalance = double.Parse(reader["OpeningBalance"].ToString());

        //            if (reader["AmountPrincInit"] != DBNull.Value)
        //                loanSchedule.AmountPrincInit = double.Parse(reader["AmountPrincInit"].ToString());

        //            if (reader["DailyInt"] != DBNull.Value)
        //                loanSchedule.DailyInt = double.Parse(reader["DailyInt"].ToString());

        //            if (reader["DailyPrinc"] != DBNull.Value)
        //                loanSchedule.DailyPrinc = double.Parse(reader["DailyPrinc"].ToString());

        //            if (reader["ClosingBalance"] != DBNull.Value)
        //                loanSchedule.ClosingBalance = double.Parse(reader["ClosingBalance"].ToString());

        //            if (reader["AmountPrincEnd"] != DBNull.Value)
        //                loanSchedule.AmountPrincEnd = double.Parse(reader["AmountPrincEnd"].ToString());

        //            if (reader["AccruedInterest"] != DBNull.Value)
        //                loanSchedule.AccruedInterest = double.Parse(reader["AccruedInterest"].ToString());

        //            if (reader["AmortizedCost"] != DBNull.Value)
        //                loanSchedule.AmortizedCost = double.Parse(reader["AmortizedCost"].ToString());

        //            if (reader["NorminalRate"] != DBNull.Value)
        //                loanSchedule.NorminalRate = decimal.Parse(reader["NorminalRate"].ToString());

        //            if (reader["AMSequence"] != DBNull.Value)
        //                loanSchedule.AMSequence = int.Parse(reader["AMSequence"].ToString());

        //            if (reader["AMRefNo"] != DBNull.Value)
        //                loanSchedule.AMRefNo = reader["AMRefNo"].ToString();

        //            if (reader["AMDate"] != DBNull.Value)
        //                loanSchedule.AMDate = DateTime.Parse(reader["AMDate"].ToString());

        //            if (reader["AMPaymentDate"] != DBNull.Value)
        //                loanSchedule.AMPaymentDate = DateTime.Parse(reader["AMPaymentDate"].ToString());

        //            if (reader["AMOpeningBalance"] != DBNull.Value)
        //                loanSchedule.AMOpeningBalance = double.Parse(reader["AMOpeningBalance"].ToString());

        //            if (reader["AMAmountPrincInit"] != DBNull.Value)
        //                loanSchedule.AMAmountPrincInit = double.Parse(reader["AMAmountPrincInit"].ToString());

        //            if (reader["AMDailyInt"] != DBNull.Value)
        //                loanSchedule.AMDailyInt = double.Parse(reader["AMDailyInt"].ToString());

        //            if (reader["AMDailyPrinc"] != DBNull.Value)
        //                loanSchedule.AMDailyPrinc = double.Parse(reader["AMDailyPrinc"].ToString());

        //            if (reader["AMClosingBalance"] != DBNull.Value)
        //                loanSchedule.AMClosingBalance = double.Parse(reader["AMClosingBalance"].ToString());

        //            if (reader["AMAmountPrincEnd"] != DBNull.Value)
        //                loanSchedule.AMAmountPrincEnd = double.Parse(reader["AMAmountPrincEnd"].ToString());

        //            if (reader["AMAccruedInterest"] != DBNull.Value)
        //                loanSchedule.AMAccruedInterest = double.Parse(reader["AMAccruedInterest"].ToString());

        //            if (reader["AMAmortizedCost"] != DBNull.Value)
        //                loanSchedule.AMAmortizedCost = double.Parse(reader["AMAmortizedCost"].ToString());

        //            if (reader["BalloonAmt"] != DBNull.Value)
        //                loanSchedule.BalloonAmt = double.Parse(reader["BalloonAmt"].ToString());

        //            if (reader["DiscountPremium"] != DBNull.Value)
        //                loanSchedule.DiscountPremium = double.Parse(reader["DiscountPremium"].ToString());

        //            if (reader["UnearnedFee"] != DBNull.Value)
        //                loanSchedule.UnearnedFee = double.Parse(reader["UnearnedFee"].ToString());

        //            if (reader["EarnedFee"] != DBNull.Value)
        //                loanSchedule.EarnedFee = double.Parse(reader["EarnedFee"].ToString());

        //            if (reader["EffectiveRate"] != DBNull.Value)
        //                loanSchedule.EffectiveRate = decimal.Parse(reader["EffectiveRate"].ToString());

        //            if (reader["NoOfPeriods"] != DBNull.Value)
        //                loanSchedule.NoOfPeriods = int.Parse(reader["NoOfPeriods"].ToString());

        //            loanSchedules.Add(loanSchedule);
        //        }

        //        con.Close();
        //    }

        //    return loanSchedules.ToArray();
        //}

        #endregion

        #region LoansImpairmentResult operations

        [OperationBehavior(TransactionScopeRequired = true)]


        public LoansImpairmentResult[] GetAllLoansImpairmentResults()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoansImpairmentResultRepository loansImpairmentResultRepository = _DataRepositoryFactory.GetDataRepository<ILoansImpairmentResultRepository>();

                IEnumerable<LoansImpairmentResult> loansImpairmentResults = loansImpairmentResultRepository.Get().ToArray();

                return loansImpairmentResults.ToArray();
            });
        }


        #endregion

        #region TBillsComputationResult operations

        [OperationBehavior(TransactionScopeRequired = true)]


        public TBillsComputationResult[] GetAllTBillsComputationResults()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITBillsComputationResultRepository tbillsComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ITBillsComputationResultRepository>();

                IEnumerable<TBillsComputationResult> tbillsComputationResults = tbillsComputationResultRepository.Get().ToArray();

                return tbillsComputationResults.ToArray();
            });
        }

        public TBillsComputationResult[] GetTBillsByClassification(string classification)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITBillsComputationResultRepository tbillsComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ITBillsComputationResultRepository>();

                IEnumerable<TBillsComputationResult> tbillsComputationResults = tbillsComputationResultRepository.Get().Where(c => c.Classification == classification).ToArray();

                return tbillsComputationResults.ToArray();
            });
        }

        #endregion

        #region EquityStockComputationResult operations

        [OperationBehavior(TransactionScopeRequired = true)]


        public EquityStockComputationResult[] GetAllEquityStocks()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEquityStockComputationResultRepository equityComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IEquityStockComputationResultRepository>();

                IEnumerable<EquityStockComputationResult> equityComputationResults = equityComputationResultRepository.Get().ToArray();

                return equityComputationResults.ToArray();
            });
        }

        public EquityStockComputationResult[] GetEquityStockByClassification(string classification)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEquityStockComputationResultRepository equityComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IEquityStockComputationResultRepository>();

                IEnumerable<EquityStockComputationResult> equityComputationResults = equityComputationResultRepository.Get().Where(c => c.Classification == classification).ToArray();

                return equityComputationResults.ToArray();
            });
        }

        #endregion

        #region BondConsolidatedData operations

        public BondConsolidatedData[] GetAllBondConsolidatedData()
        {

            var connectionString = GetDataConnection();

            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            var bondConsolidatedDatas = new List<BondConsolidatedData>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_BondConsolidated_Get", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var bondConsolidatedData = new BondConsolidatedData();

                    if (reader["RefNo"] != DBNull.Value)
                        bondConsolidatedData.RefNo = reader["RefNo"].ToString();

                    if (reader["PortfolioID"] != DBNull.Value)
                        bondConsolidatedData.PortfolioID = reader["PortfolioID"].ToString();

                    if (reader["Narration"] != DBNull.Value)
                        bondConsolidatedData.Narration = reader["Narration"].ToString();

                    if (reader["ValueDate"] != DBNull.Value)
                        bondConsolidatedData.ValueDate = DateTime.Parse(reader["ValueDate"].ToString());

                    if (reader["IssueDate"] != DBNull.Value)
                        bondConsolidatedData.IssueDate = DateTime.Parse(reader["IssueDate"].ToString());

                    if (reader["MaturityDate"] != DBNull.Value)
                        bondConsolidatedData.MaturityDate = DateTime.Parse(reader["MaturityDate"].ToString());

                    if (reader["FirstCouponDate"] != DBNull.Value)
                        bondConsolidatedData.FirstCouponDate = DateTime.Parse(reader["FirstCouponDate"].ToString());

                    if (reader["FaceValue"] != DBNull.Value)
                        bondConsolidatedData.FaceValue = double.Parse(reader["FaceValue"].ToString());

                    if (reader["CleanPrice"] != DBNull.Value)
                        bondConsolidatedData.CleanPrice = double.Parse(reader["CleanPrice"].ToString());

                    if (reader["PremiumDiscount"] != DBNull.Value)
                        bondConsolidatedData.PremiumDiscount = double.Parse(reader["PremiumDiscount"].ToString());

                    if (reader["CouponRate"] != DBNull.Value)
                        bondConsolidatedData.CouponRate = decimal.Parse(reader["CouponRate"].ToString());

                    if (reader["OpeningBalance"] != DBNull.Value)
                        bondConsolidatedData.OpeningBalance = double.Parse(reader["OpeningBalance"].ToString());

                    if (reader["DailyCoupon"] != DBNull.Value)
                        bondConsolidatedData.DailyCoupon = double.Parse(reader["DailyCoupon"].ToString());

                    if (reader["DailyInt"] != DBNull.Value)
                        bondConsolidatedData.DailyInt = double.Parse(reader["DailyInt"].ToString());

                    if (reader["DailyPrinc"] != DBNull.Value)
                        bondConsolidatedData.DailyPrinc = double.Parse(reader["DailyPrinc"].ToString());

                    if (reader["UnAmortized"] != DBNull.Value)
                        bondConsolidatedData.UnAmortized = double.Parse(reader["UnAmortized"].ToString());

                    if (reader["Amortized"] != DBNull.Value)
                        bondConsolidatedData.Amortized = double.Parse(reader["Amortized"].ToString());

                    if (reader["ClosingBalance"] != DBNull.Value)
                        bondConsolidatedData.ClosingBalance = double.Parse(reader["ClosingBalance"].ToString());

                    if (reader["AmortizedCost"] != DBNull.Value)
                        bondConsolidatedData.AmortizedCost = double.Parse(reader["AmortizedCost"].ToString());

                    if (reader["IRR"] != DBNull.Value)
                        bondConsolidatedData.IRR = decimal.Parse(reader["IRR"].ToString());

                    if (reader["Classification"] != DBNull.Value)
                        bondConsolidatedData.Classification = reader["Classification"].ToString();

                    if (reader["Date"] != DBNull.Value)
                        bondConsolidatedData.Date = DateTime.Parse(reader["Date"].ToString());

                    bondConsolidatedDatas.Add(bondConsolidatedData);
                }

                con.Close();
            }

            return bondConsolidatedDatas.ToArray();
        }



        #endregion

        #region LoanConsolidatedData operations

        public LoanConsolidatedData[] GetAllLoanConsolidatedData()
        {

            var connectionString = GetDataConnection();

            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            var loanConsolidatedDatas = new List<LoanConsolidatedData>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_LoansConsolidated_Get", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var loanConsolidatedData = new LoanConsolidatedData();

                    if (reader["RefNo"] != DBNull.Value)
                        loanConsolidatedData.RefNo = reader["RefNo"].ToString();

                    if (reader["AccountNo"] != DBNull.Value)
                        loanConsolidatedData.AccountNo = reader["AccountNo"].ToString();

                    if (reader["ProductName"] != DBNull.Value)
                        loanConsolidatedData.ProductName = reader["ProductName"].ToString();

                    if (reader["Valuedate"] != DBNull.Value)
                        loanConsolidatedData.Valuedate = DateTime.Parse(reader["Valuedate"].ToString());

                    if (reader["FirstRepaymentDate"] != DBNull.Value)
                        loanConsolidatedData.FirstRepaymentDate = DateTime.Parse(reader["FirstRepaymentDate"].ToString());

                    if (reader["MaturityDate"] != DBNull.Value)
                        loanConsolidatedData.MaturityDate = DateTime.Parse(reader["MaturityDate"].ToString());

                    if (reader["Amount"] != DBNull.Value)
                        loanConsolidatedData.Amount = double.Parse(reader["Amount"].ToString());

                    if (reader["Tenor"] != DBNull.Value)
                        loanConsolidatedData.Tenor = int.Parse(reader["Tenor"].ToString());

                    if (reader["Feeamount"] != DBNull.Value)
                        loanConsolidatedData.Feeamount = double.Parse(reader["Feeamount"].ToString());

                    if (reader["Rate"] != DBNull.Value)
                        loanConsolidatedData.Rate = decimal.Parse(reader["Rate"].ToString());

                    if (reader["EarnedFee"] != DBNull.Value)
                        loanConsolidatedData.EarnedFee = double.Parse(reader["EarnedFee"].ToString());

                    if (reader["Unearnedfee"] != DBNull.Value)
                        loanConsolidatedData.Unearnedfee = double.Parse(reader["Unearnedfee"].ToString());

                    if (reader["AmortizedCost"] != DBNull.Value)
                        loanConsolidatedData.AmortizedCost = double.Parse(reader["AmortizedCost"].ToString());

                    if (reader["PrincipalRepayFreq"] != DBNull.Value)
                        loanConsolidatedData.PrincipalRepayFreq = double.Parse(reader["PrincipalRepayFreq"].ToString());

                    if (reader["LD"] != DBNull.Value)
                        loanConsolidatedData.LD = bool.Parse(reader["LD"].ToString());
                   
                    loanConsolidatedDatas.Add(loanConsolidatedData);
                }

                con.Close();
            }

            return loanConsolidatedDatas.ToArray();
        }



        #endregion

        #region LoanConsolidatedDataFSDH operations

        public LoanConsolidatedDataFSDH[] GetAllLoanConsolidatedDataFSDH()
        {

            var connectionString = GetDataConnection();

            var loanConsolidatedDatas = new List<LoanConsolidatedDataFSDH>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_LoansConsolidated_Get", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var loanConsolidatedData = new LoanConsolidatedDataFSDH();

                    if (reader["Refno"] != DBNull.Value)
                        loanConsolidatedData.Refno = reader["Refno"].ToString();

                    if (reader["AccountNo"] != DBNull.Value)
                        loanConsolidatedData.AccountNo = reader["AccountNo"].ToString();

                    if (reader["ProductName"] != DBNull.Value)
                        loanConsolidatedData.ProductName = reader["ProductName"].ToString();

                    if (reader["LastIntrPMTDate"] != DBNull.Value)
                        loanConsolidatedData.LastIntrPMTDate = DateTime.Parse(reader["LastIntrPMTDate"].ToString());

                    if (reader["AccruedIntr"] != DBNull.Value)
                        loanConsolidatedData.AccruedIntr = double.Parse(reader["AccruedIntr"].ToString());

                    if (reader["BookValue"] != DBNull.Value)
                        loanConsolidatedData.BookValue = double.Parse(reader["BookValue"].ToString());

                    if (reader["Rate"] != DBNull.Value)
                        loanConsolidatedData.Rate = decimal.Parse(reader["Rate"].ToString());

                    if (reader["AmortizedCost"] != DBNull.Value)
                        loanConsolidatedData.AmortizedCost = double.Parse(reader["AmortizedCost"].ToString());

                    if (reader["AmortDiff"] != DBNull.Value)
                        loanConsolidatedData.AmortDiff = double.Parse(reader["AmortDiff"].ToString());

                    if (reader["Rundate"] != DBNull.Value)
                        loanConsolidatedData.Rundate = DateTime.Parse(reader["Rundate"].ToString());

                    if (reader["PrincipalOutstandingBal"] != DBNull.Value)
                        loanConsolidatedData.PrincipalOutstandingBal = double.Parse(reader["PrincipalOutstandingBal"].ToString());

                    loanConsolidatedDatas.Add(loanConsolidatedData);
                }

                con.Close();
            }

            return loanConsolidatedDatas.ToArray();
        }



        #endregion

        #region TbillConsolidatedData operations

        public TbillConsolidatedData[] GetAllTbillConsolidatedData()
        {

            var connectionString = GetDataConnection();

            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            var tbillConsolidatedDatas = new List<TbillConsolidatedData>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_TbillConsolidated_Get", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var tbillConsolidatedData = new TbillConsolidatedData();

                    if (reader["RefNo"] != DBNull.Value)
                        tbillConsolidatedData.RefNo = reader["RefNo"].ToString();

                    if (reader["Description"] != DBNull.Value)
                        tbillConsolidatedData.Description = reader["Description"].ToString();

                    if (reader["TotalTenor"] != DBNull.Value)
                        tbillConsolidatedData.TotalTenor = Convert.ToInt32(reader["TotalTenor"].ToString());

                    if (reader["UsedDays"] != DBNull.Value)
                        tbillConsolidatedData.UsedDays = Convert.ToInt32(reader["UsedDays"].ToString());

                    if (reader["DaysToMaturity"] != DBNull.Value)
                        tbillConsolidatedData.DaysToMaturity = Convert.ToInt32(reader["DaysToMaturity"].ToString());

                    if (reader["ValueDate"] != DBNull.Value)
                        tbillConsolidatedData.ValueDate = DateTime.Parse(reader["ValueDate"].ToString());

                    if (reader["MaturityDate"] != DBNull.Value)
                        tbillConsolidatedData.MaturityDate = DateTime.Parse(reader["MaturityDate"].ToString());

                    if (reader["FaceValue"] != DBNull.Value)
                        tbillConsolidatedData.FaceValue = double.Parse(reader["FaceValue"].ToString());

                    if (reader["CleanPrice"] != DBNull.Value)
                        tbillConsolidatedData.CleanPrice = double.Parse(reader["CleanPrice"].ToString());                   

                    if (reader["Rate"] != DBNull.Value)
                        tbillConsolidatedData.Rate = decimal.Parse(reader["Rate"].ToString());

                    if (reader["Price"] != DBNull.Value)
                        tbillConsolidatedData.Price = double.Parse(reader["Price"].ToString());

                    if (reader["CurrentMarketYield"] != DBNull.Value)
                        tbillConsolidatedData.CurrentMarketYield = decimal.Parse(reader["CurrentMarketYield"].ToString());

                    if (reader["ComputedMarketPrice"] != DBNull.Value)
                        tbillConsolidatedData.ComputedMarketPrice = double.Parse(reader["ComputedMarketPrice"].ToString());

                    if (reader["AmortizedCost"] != DBNull.Value)
                        tbillConsolidatedData.AmortizedCost = double.Parse(reader["AmortizedCost"].ToString());

                    if (reader["FairValue"] != DBNull.Value)
                        tbillConsolidatedData.FairValue = double.Parse(reader["FairValue"].ToString());

                    if (reader["FairValueGain"] != DBNull.Value)
                        tbillConsolidatedData.FairValueGain = double.Parse(reader["FairValueGain"].ToString());

                    if (reader["IntrestReceivable"] != DBNull.Value)
                        tbillConsolidatedData.IntrestReceivable = double.Parse(reader["IntrestReceivable"].ToString());

                    if (reader["AmortizedCost"] != DBNull.Value)
                        tbillConsolidatedData.AmortizedCost = double.Parse(reader["AmortizedCost"].ToString());

                    if (reader["EIR"] != DBNull.Value)
                        tbillConsolidatedData.EIR = decimal.Parse(reader["EIR"].ToString());

                    if (reader["Classification"] != DBNull.Value)
                        tbillConsolidatedData.Classification = reader["Classification"].ToString();

                    tbillConsolidatedDatas.Add(tbillConsolidatedData);
                }

                con.Close();
            }

            return tbillConsolidatedDatas.ToArray();
        }



        #endregion

        #region BondConsolidatedDataOthers operations

        public BondConsolidatedDataOthers[] GetAllBondConsolidatedDataOthers()
        {

            var connectionString = GetDataConnection();

            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            var bondConsolidatedDataothers = new List<BondConsolidatedDataOthers>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_BondConsolidatedOthers_Get", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var bondConsolidatedDataother = new BondConsolidatedDataOthers();

                    if (reader["RefNo"] != DBNull.Value)
                        bondConsolidatedDataother.RefNo = reader["RefNo"].ToString();

                    if (reader["PortfolioID"] != DBNull.Value)

                        bondConsolidatedDataother.PortfolioID = reader["PortfolioID"].ToString();

                    if (reader["CurrentMarketYield"] != DBNull.Value)
                        bondConsolidatedDataother.CurrentMarketYield = double.Parse(reader["CurrentMarketYield"].ToString());

                    if (reader["EIR"] != DBNull.Value)
                        bondConsolidatedDataother.EIR = double.Parse(reader["EIR"].ToString());

                    if (reader["LastCouponDate"] != DBNull.Value)
                        bondConsolidatedDataother.LastCouponDate = DateTime.Parse(reader["LastCouponDate"].ToString());

                    if (reader["NextCouponDate"] != DBNull.Value)
                        bondConsolidatedDataother.NextCouponDate = DateTime.Parse(reader["NextCouponDate"].ToString());


                    if (reader["AccruedInterest"] != DBNull.Value)
                        bondConsolidatedDataother.AccruedInterest = double.Parse(reader["AccruedInterest"].ToString());

                    if (reader["FairValueGain"] != DBNull.Value)
                        bondConsolidatedDataother.FairValueGain = double.Parse(reader["FairValueGain"].ToString());

                    if (reader["FairValueBasis"] != DBNull.Value)
                        bondConsolidatedDataother.FairValueBasis = int.Parse(reader["FairValueBasis"].ToString());

                    if (reader["Narration"] != DBNull.Value)
                        bondConsolidatedDataother.Narration = reader["Narration"].ToString();

                    if (reader["EffectiveDate"] != DBNull.Value)
                        bondConsolidatedDataother.EffectiveDate = DateTime.Parse(reader["EffectiveDate"].ToString());

                    if (reader["MaturityDate"] != DBNull.Value)
                        bondConsolidatedDataother.MaturityDate = DateTime.Parse(reader["MaturityDate"].ToString());

                    if (reader["FaceValue"] != DBNull.Value)
                        bondConsolidatedDataother.FaceValue = double.Parse(reader["FaceValue"].ToString());

                    if (reader["CleanPrice"] != DBNull.Value)
                        bondConsolidatedDataother.CleanPrice = double.Parse(reader["CleanPrice"].ToString());

                    if (reader["Price"] != DBNull.Value)
                        bondConsolidatedDataother.Price = double.Parse(reader["Price"].ToString());

                    if (reader["PremiumDiscount"] != DBNull.Value)
                        bondConsolidatedDataother.PremiumDiscount = double.Parse(reader["PremiumDiscount"].ToString());

                    if (reader["CouponRate"] != DBNull.Value)
                        bondConsolidatedDataother.CouponRate = decimal.Parse(reader["CouponRate"].ToString());

                    if (reader["AmortizedCost"] != DBNull.Value)
                        bondConsolidatedDataother.AmortizedCost = double.Parse(reader["AmortizedCost"].ToString());
                             
                    if (reader["Classification"] != DBNull.Value)
                        bondConsolidatedDataother.Classification = reader["Classification"].ToString();

                    if (reader["FairValue"] != DBNull.Value)
                        bondConsolidatedDataother.FairValue = double.Parse(reader["FairValue"].ToString());

                    if (reader["RunDate"] != DBNull.Value)
                        bondConsolidatedDataother.RunDate = DateTime.Parse(reader["RunDate"].ToString());

                    bondConsolidatedDataothers.Add(bondConsolidatedDataother);
                }

                con.Close();
            }

            return bondConsolidatedDataothers.ToArray();
        }



        #endregion

        #region BorrowingPeriodicSchedule operations

        [OperationBehavior(TransactionScopeRequired = true)]


        public BorrowingPeriodicSchedule[] GetBorrowingPeriodicSchedulebyRefNo(string refNo, string path)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBorrowingPeriodicScheduleRepository borrowingPeriodicScheduleMapRepository = _DataRepositoryFactory.GetDataRepository<IBorrowingPeriodicScheduleRepository>();

                BorrowingPeriodicSchedule[] borrowingPeriodicScheduleEntity = borrowingPeriodicScheduleMapRepository.GetBorrowingPeriodicSchedulebyRefNo(refNo, path).ToArray();
                if (borrowingPeriodicScheduleEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BorrowingPeriodicSchedule with ID of {0} is not in database", refNo));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return borrowingPeriodicScheduleEntity;
            });
        }

        public BorrowingPeriodicSchedule[] GetAllBorrowingPeriodicSchedules()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBorrowingPeriodicScheduleRepository borrowingPeriodicScheduleRepository = _DataRepositoryFactory.GetDataRepository<IBorrowingPeriodicScheduleRepository>();

                IEnumerable<BorrowingPeriodicSchedule> borrowingPeriodicSchedules = borrowingPeriodicScheduleRepository.Get().ToArray();

                return borrowingPeriodicSchedules.ToArray();
            });
        }


        public BorrowingPeriodicSchedule[] GetBorrowingPeriodicScheduleDistinctRefNo()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBorrowingPeriodicScheduleRepository borrowingPeriodicScheduleRepository = _DataRepositoryFactory.GetDataRepository<IBorrowingPeriodicScheduleRepository>();

                IEnumerable<BorrowingPeriodicSchedule> borrowingPeriodicSchedules = borrowingPeriodicScheduleRepository.Get().ToArray();

                return borrowingPeriodicSchedules.ToArray();
            });
        }

        public IEnumerable<string> GetDistinctBorrowingScheduleRefNos()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBorrowingScheduleRepository borrowingScheduleRepository = _DataRepositoryFactory.GetDataRepository<IBorrowingScheduleRepository>();

                IEnumerable<string> borrowingSchedules = borrowingScheduleRepository.GetDistinctBorrowingScheduleRefNos().ToArray();

                return borrowingSchedules.ToArray();
            });
        }


        public string[] GetDistinctBorrowingPeriodicScheduleRefNos()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBorrowingPeriodicScheduleRepository borrowingScheduleRepository = _DataRepositoryFactory.GetDataRepository<IBorrowingPeriodicScheduleRepository>();

                IEnumerable<string> borrowingSchedules = borrowingScheduleRepository.GetDistinctBorrowingPeriodicScheduleRefNos().ToArray();

                return borrowingSchedules.ToArray();
            });
        }

        public BorrowingPeriodicSchedule[] GetRefNoBorrowingPeriodicSchedule()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBorrowingPeriodicScheduleRepository borrowingPeriodicScheduleRepository = _DataRepositoryFactory.GetDataRepository<IBorrowingPeriodicScheduleRepository>();
                List<BorrowingPeriodicSchedule> borrowingPeriodicSchedules = borrowingPeriodicScheduleRepository.GetDistinctRefNo();


                return borrowingPeriodicSchedules.ToArray();
            });
        }

        public void DeleteBorrowingPeriodicSchedulebyRefNo(string refNo)
        {

            var connectionString = GetDataConnection();

            int status = 0;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_delete_borrowing_periodic_data", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "refno",
                    Value = refNo,
                });

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        public string[] GetBorrowingPeriodicRefNo()
        {

            var connectionString = GetDataConnection();

            List<string> refno;
            var refnoList = new List<string>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("select distinct RefNo from ifrs_borrowings_periodic_schedule order by refno", con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 0;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var myRefNo = new ReferenceNoModel();
                        if (reader["RefNo"] != DBNull.Value)
                            myRefNo.RefNo = reader["RefNo"].ToString();
                        refnoList.Add(myRefNo.RefNo);
                    }
                    reader.Close();
                    con.Close();
                }
            }
            return refnoList.ToArray();
        }

        #endregion

        #region BorrowingSchedule operations

        [OperationBehavior(TransactionScopeRequired = true)]


        public BorrowingSchedule[] GetBorrowingSchedulebyRefNo(string refNo, DateTime? Date, string path)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBorrowingScheduleRepository borrowingScheduleMapRepository = _DataRepositoryFactory.GetDataRepository<IBorrowingScheduleRepository>();

                BorrowingSchedule[] borrowingScheduleEntity = borrowingScheduleMapRepository.GetBorrowingSchedulebyRefNo(refNo, Date, path).ToArray();
                if (borrowingScheduleEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BorrowingSchedule with ID of {0} is not in database", refNo));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return borrowingScheduleEntity;
            });
        }

        public BorrowingSchedule[] GetAllBorrowingSchedules()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBorrowingScheduleRepository borrowingScheduleRepository = _DataRepositoryFactory.GetDataRepository<IBorrowingScheduleRepository>();

                IEnumerable<BorrowingSchedule> borrowingSchedules = borrowingScheduleRepository.Get().ToArray();

                return borrowingSchedules.ToArray();
            });
        }


        public BorrowingSchedule[] GetBorrowingScheduleDistinctRefNo()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBorrowingScheduleRepository borrowingScheduleRepository = _DataRepositoryFactory.GetDataRepository<IBorrowingScheduleRepository>();

                IEnumerable<BorrowingSchedule> borrowingSchedules = borrowingScheduleRepository.Get().ToArray();

                return borrowingSchedules.ToArray();
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

        #endregion

        //public string[] GetException()
        //{

        //    var connectionString = GetDataConnection();

        //    List<string> refno;
        //    var refnoList = new List<string>();
        //    using (var con = new SqlConnection(connectionString))
        //    {
        //        var cmd = new SqlCommand("select distinct ExceptionType from ifrs_exception_report", con);
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandTimeout = 0;

        //        con.Open();

        //        SqlDataReader reader = cmd.ExecuteReader();
        //        {
        //            while (reader.Read())
        //            {
        //                var myRefNo = new ReferenceNoModel();
        //                if (reader["RefNo"] != DBNull.Value)
        //                    myRefNo.RefNo = reader["RefNo"].ToString();
        //                refnoList.Add(myRefNo.RefNo);
        //            }
        //            reader.Close();
        //            con.Close();
        //        }
        //    }
        //    return refnoList.ToArray();
        //}


    }
}
