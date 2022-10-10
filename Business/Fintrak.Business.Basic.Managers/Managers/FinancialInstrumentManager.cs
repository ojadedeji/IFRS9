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

using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;

namespace Fintrak.Business.Basic.Managers
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
                        var root = menuRepository.Get().Where(c => c.Alias == "Financial Instrument").FirstOrDefault();
                        //Role
                        var adminRole = roleRepository.Get().Where(c => c.Name == GROUP_ADMINISTRATOR && c.SolutionId == solution.SolutionId).FirstOrDefault();
                        var userRole = roleRepository.Get().Where(c => c.Name == GROUP_USER && c.SolutionId == solution.SolutionId).FirstOrDefault();


                        //register menu
                        var actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "IFRS_MARKET_YIELD",
                            Alias = "Market Yield/Price",
                            Action = "IFRS_MARKET_YIELD",
                            ActionUrl = "ifrsfi-marketyield-list",
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
                            Name = "FAIR_VALUE_BASIS_MAP",
                            Alias = "Fair Value Basis Map",
                            Action = "FAIR_VALUE_BASIS_MAP",
                            ActionUrl = "ifrsfi-fairvaluebasismap-list",
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
                            Name = "FAIR_VALUE_BASIS_EXEMPTION",
                            Alias = "Fair Value Basis Exemption",
                            Action = "FAIR_VALUE_BASIS_EXEMPTION",
                            ActionUrl = "ifrsfi-fairvaluebasisexemption-list",
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
