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
using Fintrak.Data.Basic;

using systemCoreFramework = Fintrak.Shared.SystemCore.Framework;
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;

namespace Fintrak.Business.Basic.Managers
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

                    }

                    ts.Complete();
                }

            });

        }


        #region BondComputationResult operations

        [OperationBehavior(TransactionScopeRequired = true)]


        //public IEnumerable<BondComputation> GetBondComputationResultbyRefNo(string refNo)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //       // IBondComputationRepository bondComputationMapRepository = _DataRepositoryFactory.GetDataRepository<IBondComputationRepository>();

        //        IEnumerable<BondComputation> bondComputation = new  IEnumerable<BondComputation>();

        //        BondComputationRepository  bondComputationRepository = new BondComputationRepository();

        //        bondComputation = bondComputationRepository.GetBondPeriodicScheduleRefNos(refNo);


        //       // BondComputation bondComputationEntity = bondComputationMapRepository
        //        if (bondComputation == null)
        //        {
        //            NotFoundException ex = new NotFoundException(string.Format("BondComputationResult with ID of {0} is not in database", refNo));
        //            throw new FaultException<NotFoundException>(ex, ex.Message);
        //        }

        //        return bondComputation;
        //    });
        //}

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
        public BondComputation[] GetBondComputationResultbyRefNo(string refNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondComputationRepository bondComputationRepository = _DataRepositoryFactory.GetDataRepository<IBondComputationRepository>();

                BondComputation[] bondComputations = bondComputationRepository.Get().Where(c => c.RefNo == refNo).OrderBy(c => c.Day).ToArray();

                return bondComputations.ToArray();
            });
        }

        #endregion

        #region BondPeriodicSchedule operations

        [OperationBehavior(TransactionScopeRequired = true)]


        public BondPeriodicSchedule[] GetBondPeriodicSchedulebyRefNo(string refNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondPeriodicScheduleRepository bondPeriodicScheduleMapRepository = _DataRepositoryFactory.GetDataRepository<IBondPeriodicScheduleRepository>();

                BondPeriodicSchedule[] bondPeriodicScheduleEntity = bondPeriodicScheduleMapRepository.Get().Where(c => c.RefNo == refNo).OrderBy(c => c.Num_Pmt).ToArray();

                if (bondPeriodicScheduleEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BondPeriodicSchedule with ID of {0} is not in database", refNo));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return bondPeriodicScheduleEntity;
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


        public LoanPeriodicSchedule[] GetLoanPeriodicSchedulebyRefNo(string refNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanPeriodicScheduleRepository loanPeriodicScheduleMapRepository = _DataRepositoryFactory.GetDataRepository<ILoanPeriodicScheduleRepository>();

                LoanPeriodicSchedule[] loanPeriodicScheduleEntity = loanPeriodicScheduleMapRepository.Get().Where(c => c.RefNo == refNo).OrderBy(c => c.Num_Pmt).ToArray();
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


        public LoanSchedule[] GetLoanScheduleDistinctRefNo()
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
