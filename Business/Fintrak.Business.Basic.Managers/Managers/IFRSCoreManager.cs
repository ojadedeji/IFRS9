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
using Fintrak.Shared.Core.Framework;

using systemCoreFramework = Fintrak.Shared.SystemCore.Framework;
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;

namespace Fintrak.Business.Basic.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class IFRSCoreManager : ManagerBase, IIFRSCoreService
    {
        public IFRSCoreManager()
        {
        }

        public IFRSCoreManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        const string SOLUTION_NAME = "FIN_IFRS";
        const string SOLUTION_ALIAS = "IFRS";
        const string MODULE_NAME = "FIN_IFRS_CORE";
        const string MODULE_ALIAS = "IFRS Core";

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
                IGLTypeRepository glTypeRepository = _DataRepositoryFactory.GetDataRepository<IGLTypeRepository>();
             
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
                        var adminRole = new systemCoreEntities.Role()
                        {
                            SolutionId = solution.SolutionId,
                            Name = GROUP_ADMINISTRATOR,
                            Description = "For IFRS solution unlimited users",
                            Type = systemCoreFramework.RoleType.Application,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        roleRepository.Add(adminRole);

                        var userRole = new systemCoreEntities.Role()
                        {
                            SolutionId = solution.SolutionId,
                            Name = GROUP_USER,
                            Description = "For IFRS solution limited users",
                            Type = systemCoreFramework.RoleType.Application,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        roleRepository.Add(userRole);

                        int menuIndex = 0;

                        //register menu
                        var root = new systemCoreEntities.Menu()
                        {
                            Name = "IFRS",
                            Alias = "IFRS",
                            Action = "",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "ifrs_image",
                            ModuleId = module.EntityId,
                            ParentId = null,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        root = menuRepository.Add(root);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = root.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = root.EntityId,
                            RoleId = userRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        var loan = new systemCoreEntities.Menu()
                        {
                            Name = "IFRS_LOAN",
                            Alias = "Loans",
                            Action = "IFRS_LOAN",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "ifrs_loan_image",
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

                        menuRepository.Add(loan);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = loan.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = loan.EntityId,
                            RoleId = userRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });


                        var finInstrument = new systemCoreEntities.Menu()
                        {
                            Name = "IFRS_FINANCIAL_INSTRUMENT",
                            Alias = "Financial Instrument",
                            Action = "IFRS_FINANCIAL_INSTRUMENT",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "ifrs_financialinstrument_image",
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

                        menuRepository.Add(finInstrument);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = finInstrument.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = finInstrument.EntityId,
                            RoleId = userRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        var dataview = new systemCoreEntities.Menu()
                        {
                            Name = "IFRS_DATA_VIEW",
                            Alias = "IFRS Processed Data",
                            Action = "IFRS_DATA_VIEW",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "ifrs_dataview_image",
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

                        menuRepository.Add(dataview);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = dataview.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = dataview.EntityId,
                            RoleId = userRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });


                        var finstat = new systemCoreEntities.Menu()
                        {
                            Name = "FINSTAT",
                            Alias = "Finstat",
                            Action = "FINSTAT",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "finstat_image",
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

                        menuRepository.Add(finstat);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = finstat.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = finstat.EntityId,
                            RoleId = userRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        var actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "IFRS_REGISTRY",
                            Alias = "Registries",
                            Action = "IFRS_REGISTRY",
                            ActionUrl = "finstat-registry-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = finstat.EntityId,
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
                            Name = "DERIVED_CAPTIONS",
                            Alias = "Derived Captions",
                            Action = "DERIVED_CAPTIONS",
                            ActionUrl = "finstat-derivedcaption-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = finstat.EntityId,
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


        #region IFRSRegistry operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IFRSRegistry UpdateIFRSRegistry(IFRSRegistry ifrsRegistry)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSRegistryRepository ifrsRegistryRepository = _DataRepositoryFactory.GetDataRepository<IIFRSRegistryRepository>();

                IFRSRegistry updatedEntity = null;

                if (ifrsRegistry.RegistryId == 0)
                    updatedEntity = ifrsRegistryRepository.Add(ifrsRegistry);
                else
                    updatedEntity = ifrsRegistryRepository.Update(ifrsRegistry);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIFRSRegistry(int ifrsRegistryId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSRegistryRepository ifrsRegistryRepository = _DataRepositoryFactory.GetDataRepository<IIFRSRegistryRepository>();

                ifrsRegistryRepository.Remove(ifrsRegistryId);
            });
        }

        public IFRSRegistry GetIFRSRegistry(int ifrsRegistryId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSRegistryRepository ifrsRegistryRepository = _DataRepositoryFactory.GetDataRepository<IIFRSRegistryRepository>();

                IFRSRegistry ifrsRegistryEntity = ifrsRegistryRepository.Get(ifrsRegistryId);
                if (ifrsRegistryEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IFRSRegistry with ID of {0} is not in database", ifrsRegistryId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifrsRegistryEntity;
            });
        }

        public IFRSRegistryData[] GetAllIFRSRegistries()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSRegistryRepository ifrsRegistryRepository = _DataRepositoryFactory.GetDataRepository<IIFRSRegistryRepository>();


                List<IFRSRegistryData> ifrsRegistrys = new List<IFRSRegistryData>();
                IEnumerable<IFRSRegistryInfo> ifrsRegistryInfos = ifrsRegistryRepository.GetIFRSRegistrys().OrderBy(c => c.IFRSRegistry.Position).ToArray();

                foreach (var ifrsRegistryInfo in ifrsRegistryInfos)
                {
                    ifrsRegistrys.Add(
                        new IFRSRegistryData
                        {
                            RegistryId = ifrsRegistryInfo.IFRSRegistry.EntityId,
                            Code = ifrsRegistryInfo.IFRSRegistry.Code,
                            Caption = ifrsRegistryInfo.IFRSRegistry.Caption,
                            Position = ifrsRegistryInfo.IFRSRegistry.Position,
                            RefNote = ifrsRegistryInfo.IFRSRegistry.RefNote,
                            FinType = ifrsRegistryInfo.IFRSRegistry.FinType,
                            FinSubType = ifrsRegistryInfo.IFRSRegistry.FinSubType,
                            ParentId = ifrsRegistryInfo.IFRSRegistry.ParentId,
                            ParentName = ifrsRegistryInfo.Parent != null ?   ifrsRegistryInfo.Parent.Caption : string.Empty,
                            IsTotalLine = ifrsRegistryInfo.IFRSRegistry.IsTotalLine,
                            Color = ifrsRegistryInfo.IFRSRegistry.Color,
                            Class = ifrsRegistryInfo.IFRSRegistry.Class,
                            Active = ifrsRegistryInfo.IFRSRegistry.Active
                        });
                }

                return ifrsRegistrys.ToArray();
            });
        }

                       
        #endregion

        #region DerivedCaption operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public DerivedCaption UpdateDerivedCaption(DerivedCaption derivedCaption)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IDerivedCaptionRepository derivedCaptionRepository = _DataRepositoryFactory.GetDataRepository<IDerivedCaptionRepository>();

                DerivedCaption updatedEntity = null;

                if (derivedCaption.DerivedCaptionId == 0)
                    updatedEntity = derivedCaptionRepository.Add(derivedCaption);
                else
                    updatedEntity = derivedCaptionRepository.Update(derivedCaption);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteDerivedCaption(int derivedCaptionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IDerivedCaptionRepository derivedCaptionRepository = _DataRepositoryFactory.GetDataRepository<IDerivedCaptionRepository>();

                derivedCaptionRepository.Remove(derivedCaptionId);
            });
        }

        public DerivedCaption GetDerivedCaption(int derivedCaptionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IDerivedCaptionRepository derivedCaptionRepository = _DataRepositoryFactory.GetDataRepository<IDerivedCaptionRepository>();

                DerivedCaption derivedCaptionEntity = derivedCaptionRepository.Get(derivedCaptionId);
                if (derivedCaptionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("DerivedCaption with ID of {0} is not in database", derivedCaptionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return derivedCaptionEntity;
            });
        }

        public DerivedCaption[] GetAllDerivedCaptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IDerivedCaptionRepository derivedCaptionRepository = _DataRepositoryFactory.GetDataRepository<IDerivedCaptionRepository>();

                IEnumerable<DerivedCaption> derivedCaptions = derivedCaptionRepository.Get().ToArray();

                return derivedCaptions.ToArray();
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
