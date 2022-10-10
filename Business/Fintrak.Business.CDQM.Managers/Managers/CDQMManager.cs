using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq;
using System.ServiceModel;
using System.Transactions;
using Fintrak.Data.CDQM.Contracts;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Business.CDQM.Contracts;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.CDQM.Entities;
using Fintrak.Shared.Common.Utils;
using Fintrak.Shared.Core.Entities;

using roleType = Fintrak.Shared.SystemCore.Framework;
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;



namespace Fintrak.Business.CDQM.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class CDQMManager : ManagerBase, ICDQMService
    {
        public CDQMManager()
        {
        }

        public CDQMManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        const string SOLUTION_NAME = "FIN_CDQM";
        const string SOLUTION_ALIAS = "CDQM";
        const string MODULE_NAME = "FIN_CDQM";
        const string MODULE_ALIAS = "Customer Data Quality Management";

        const string GROUP_ADMINISTRATOR = "Administrator";
        const string GROUP_SUPER_USER = "Super User";
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
                        var adminRole = new systemCoreEntities.Role()
                        {
                            SolutionId = solution.SolutionId,
                            Name = GROUP_ADMINISTRATOR,
                            Description = "For CDQM solution unlimited users",
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now,
                            Type = roleType.RoleType.Application
                        };

                        roleRepository.Add(adminRole);

                        var superRole = new systemCoreEntities.Role()
                        {
                            SolutionId = solution.SolutionId,
                            Name =  GROUP_SUPER_USER,
                            Description = "For CDQM solution partial unlimited users",
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now,
                            Type = roleType.RoleType.Application
                        };

                        roleRepository.Add(superRole);

                        var userRole = new systemCoreEntities.Role()
                        {
                            SolutionId = solution.SolutionId,
                            Name = GROUP_USER,
                            Description = "For CDQM solution limited users",
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now,
                            Type = roleType.RoleType.Application
                        };

                        roleRepository.Add(userRole);

                        int menuIndex = 0;

                        //register menu
                        var root = new systemCoreEntities.Menu()
                        {
                            Name = "CDQM",
                            Alias = "CDQM",
                            Action = "",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "cdqm_image",
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
                            RoleId = superRole.EntityId,
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

                        var config = new systemCoreEntities.Menu()
                        {
                            Name = "CDQM_CONFIGURATION",
                            Alias = "Configuration",
                            Action = "CDQM_CONFIGURATION",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "cdqm_configuration_image",
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

                        menuRepository.Add(config);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = config.EntityId,
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
                            MenuId = config.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        var actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "CDQM_ADDRESS",
                            Alias = "Address",
                            Action = "CDQM_ADDRESS",
                            ActionUrl = "cdqm-address-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = config.EntityId,
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

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "CDQM_COUNTRY",
                            Alias = "Country",
                            Action = "CDQM_COUNTRY",
                            ActionUrl = "cdqm-country-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = config.EntityId,
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

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "CDQM_GENDER_GROUP",
                            Alias = "Gender Group",
                            Action = "CDQM_GENDER_GROUP",
                            ActionUrl = "cdqm-gendergroup-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = config.EntityId,
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

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "CDQM_MERCHANT",
                            Alias = "Merchant",
                            Action = "CDQM_MERCHANT",
                            ActionUrl = "cdqm-merchant-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = config.EntityId,
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

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "CDQM_TITLE",
                            Alias = "Title",
                            Action = "CDQM_TITLE",
                            ActionUrl = "cdqm-title-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = config.EntityId,
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

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "CDQM_ETL_CONFIGURATION",
                            Alias = "ETL Configuration",
                            Action = "CDQM_ETL_CONFIGURATION",
                            ActionUrl = "cdqm-etlconfiguration-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = config.EntityId,
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

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "CDQM_PRODUCT",
                            Alias = "Products",
                            Action = "CDQM_PRODUCT",
                            ActionUrl = "cdqm-product-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = config.EntityId,
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

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "CDQM_CUSTOMER_MIS",
                            Alias = "Customer MIS",
                            Action = "CDQM_CUSTOMER_MIS",
                            ActionUrl = "cdqm-customermis-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = config.EntityId,
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

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        var customer = new systemCoreEntities.Menu()
                        {
                            Name = "CDQM_CUSTOMERS",
                            Alias = "Customers",
                            Action = "CDQM_CUSTOMERS",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "cdqm_customer_image",
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

                        menuRepository.Add(customer);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = customer.EntityId,
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
                            MenuId = customer.EntityId,
                            RoleId = superRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = customer.EntityId,
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
                            Name = "CDQM_CHECK_CUSTOMER_DUPLICATE",
                            Alias = "Check Customer Duplicate",
                            Action = "CDQM_CHECK_CUSTOMER_DUPLICATE",
                            ActionUrl = "cdqm-customercheck-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = customer.EntityId,
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

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = superRole.EntityId,
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

                    }

                    ts.Complete();
                }

            });

        }

        #region CDQMAddress operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CDQMAddress UpdateCDQMAddress(CDQMAddress cdqmAddress)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMAddressRepository cdqmAddressRepository = _DataRepositoryFactory.GetDataRepository<ICDQMAddressRepository>();

                CDQMAddress updatedEntity = null;

                if (cdqmAddress.AddressId == 0)
                    updatedEntity = cdqmAddressRepository.Add(cdqmAddress);
                else
                    updatedEntity = cdqmAddressRepository.Update(cdqmAddress);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCDQMAddress(int addressId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMAddressRepository cdqmAddressRepository = _DataRepositoryFactory.GetDataRepository<ICDQMAddressRepository>();

                cdqmAddressRepository.Remove(addressId);
            });
        }

        public CDQMAddress GetCDQMAddress(int addressId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMAddressRepository cdqmAddressRepository = _DataRepositoryFactory.GetDataRepository<ICDQMAddressRepository>();

                CDQMAddress cdqmAddressEntity = cdqmAddressRepository.Get(addressId);
                if (cdqmAddressEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CDQMAddress with ID of {0} is not in database", addressId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return cdqmAddressEntity;
            });
        }

        public CDQMAddress[] GetAllCDQMAddresses()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMAddressRepository cdqmAddressRepository = _DataRepositoryFactory.GetDataRepository<ICDQMAddressRepository>();

                IEnumerable<CDQMAddress> cdqmAddresses = cdqmAddressRepository.Get().ToArray();

                return cdqmAddresses.ToArray();
            });
        }


        #endregion

        #region CDQMCountry operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CDQMCountry UpdateCDQMCountry(CDQMCountry cdqmCountry)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMCountryRepository cdqmCountryRepository = _DataRepositoryFactory.GetDataRepository<ICDQMCountryRepository>();

                CDQMCountry updatedEntity = null;

                if (cdqmCountry.CountryId == 0)
                    updatedEntity = cdqmCountryRepository.Add(cdqmCountry);
                else
                    updatedEntity = cdqmCountryRepository.Update(cdqmCountry);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCDQMCountry(int cdqmCountryId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMCountryRepository cdqmCountryRepository = _DataRepositoryFactory.GetDataRepository<ICDQMCountryRepository>();

                cdqmCountryRepository.Remove(cdqmCountryId);
            });
        }

        public CDQMCountry GetCDQMCountry(int cdqmCountryId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMCountryRepository cdqmCountryRepository = _DataRepositoryFactory.GetDataRepository<ICDQMCountryRepository>();

                CDQMCountry cdqmCountryEntity = cdqmCountryRepository.Get(cdqmCountryId);
                if (cdqmCountryEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CDQMCountry with ID of {0} is not in database", cdqmCountryId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return cdqmCountryEntity;
            });
        }

        public CDQMCountry[] GetAllCDQMCountries()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMCountryRepository cdqmCountryRepository = _DataRepositoryFactory.GetDataRepository<ICDQMCountryRepository>();

                IEnumerable<CDQMCountry> cdqmCountries = cdqmCountryRepository.Get().ToArray();

                return cdqmCountries.ToArray();
            });
        }


        #endregion

        #region CDQMCustomerMIS operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CDQMCustomerMIS UpdateCDQMCustomerMIS(CDQMCustomerMIS cdqmCustomerMIS)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMCustomerMISRepository cdqmCustomerMISRepository = _DataRepositoryFactory.GetDataRepository<ICDQMCustomerMISRepository>();

                CDQMCustomerMIS updatedEntity = null;

                if (cdqmCustomerMIS.CustomerMISId == 0)
                    updatedEntity = cdqmCustomerMISRepository.Add(cdqmCustomerMIS);
                else
                    updatedEntity = cdqmCustomerMISRepository.Update(cdqmCustomerMIS);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCDQMCustomerMIS(int customerMISId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMCustomerMISRepository cdqmCustomerMISRepository = _DataRepositoryFactory.GetDataRepository<ICDQMCustomerMISRepository>();

                cdqmCustomerMISRepository.Remove(customerMISId);
            });
        }

        public CDQMCustomerMIS GetCDQMCustomerMIS(int customerMISId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMCustomerMISRepository cdqmCustomerMISRepository = _DataRepositoryFactory.GetDataRepository<ICDQMCustomerMISRepository>();

                CDQMCustomerMIS cdqmCustomerMISEntity = cdqmCustomerMISRepository.Get(customerMISId);
                if (cdqmCustomerMISEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CDQMCustomerMIS with ID of {0} is not in database", customerMISId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return cdqmCustomerMISEntity;
            });
        }

        public CDQMCustomerMIS[] GetAllCDQMCustomerMIS()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMCustomerMISRepository cdqmCustomerMISRepository = _DataRepositoryFactory.GetDataRepository<ICDQMCustomerMISRepository>();

                IEnumerable<CDQMCustomerMIS> cdqmCustomerMIS = cdqmCustomerMISRepository.Get().ToArray();

                return cdqmCustomerMIS.ToArray();
            });
        }


        #endregion

        #region CDQMETLConfiguration operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CDQMETLConfiguration UpdateCDQMETLConfiguration(CDQMETLConfiguration cdqmETLConfiguration)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMETLConfigurationRepository cdqmETLConfigurationRepository = _DataRepositoryFactory.GetDataRepository<ICDQMETLConfigurationRepository>();

                CDQMETLConfiguration updatedEntity = null;

                if (cdqmETLConfiguration.ETLConfigurationId == 0)
                    updatedEntity = cdqmETLConfigurationRepository.Add(cdqmETLConfiguration);
                else
                    updatedEntity = cdqmETLConfigurationRepository.Update(cdqmETLConfiguration);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCDQMETLConfiguration(int etlConfigurationId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMETLConfigurationRepository cdqmETLConfigurationRepository = _DataRepositoryFactory.GetDataRepository<ICDQMETLConfigurationRepository>();

                cdqmETLConfigurationRepository.Remove(etlConfigurationId);
            });
        }

        public CDQMETLConfiguration GetCDQMETLConfiguration(int etlConfigurationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMETLConfigurationRepository cdqmETLConfigurationRepository = _DataRepositoryFactory.GetDataRepository<ICDQMETLConfigurationRepository>();

                CDQMETLConfiguration cdqmETLConfigurationEntity = cdqmETLConfigurationRepository.Get(etlConfigurationId);
                if (cdqmETLConfigurationEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CDQMETLConfiguration with ID of {0} is not in database", etlConfigurationId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return cdqmETLConfigurationEntity;
            });
        }

        public CDQMETLConfiguration[] GetAllCDQMETLConfigurations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMETLConfigurationRepository cdqmETLConfigurationRepository = _DataRepositoryFactory.GetDataRepository<ICDQMETLConfigurationRepository>();

                IEnumerable<CDQMETLConfiguration> cdqmETLConfigurations = cdqmETLConfigurationRepository.Get().ToArray();

                return cdqmETLConfigurations.ToArray();
            });
        }


        #endregion

        #region CDQMGenderGroup operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CDQMGenderGroup UpdateCDQMGenderGroup(CDQMGenderGroup cdqmGenderGroup)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMGenderGroupRepository cdqmGenderGroupRepository = _DataRepositoryFactory.GetDataRepository<ICDQMGenderGroupRepository>();

                CDQMGenderGroup updatedEntity = null;

                if (cdqmGenderGroup.GenderGroupId == 0)
                    updatedEntity = cdqmGenderGroupRepository.Add(cdqmGenderGroup);
                else
                    updatedEntity = cdqmGenderGroupRepository.Update(cdqmGenderGroup);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCDQMGenderGroup(int genderGroupId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMGenderGroupRepository cdqmGenderGroupRepository = _DataRepositoryFactory.GetDataRepository<ICDQMGenderGroupRepository>();

                cdqmGenderGroupRepository.Remove(genderGroupId);
            });
        }

        public CDQMGenderGroup GetCDQMGenderGroup(int genderGroupId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMGenderGroupRepository cdqmGenderGroupRepository = _DataRepositoryFactory.GetDataRepository<ICDQMGenderGroupRepository>();

                CDQMGenderGroup cdqmGenderGroupEntity = cdqmGenderGroupRepository.Get(genderGroupId);
                if (cdqmGenderGroupEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CDQMGenderGroup with ID of {0} is not in database", genderGroupId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return cdqmGenderGroupEntity;
            });
        }

        public CDQMGenderGroup[] GetAllCDQMGenderGroups()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMGenderGroupRepository cdqmGenderGroupRepository = _DataRepositoryFactory.GetDataRepository<ICDQMGenderGroupRepository>();

                IEnumerable<CDQMGenderGroup> cdqmGenderGroups = cdqmGenderGroupRepository.Get().ToArray();

                return cdqmGenderGroups.ToArray();
            });
        }


        #endregion

        #region CDQMMerchant operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CDQMMerchant UpdateCDQMMerchant(CDQMMerchant cdqmMerchant)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMMerchantRepository cdqmMerchantRepository = _DataRepositoryFactory.GetDataRepository<ICDQMMerchantRepository>();

                CDQMMerchant updatedEntity = null;

                if (cdqmMerchant.MerchantId == 0)
                    updatedEntity = cdqmMerchantRepository.Add(cdqmMerchant);
                else
                    updatedEntity = cdqmMerchantRepository.Update(cdqmMerchant);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCDQMMerchant(int merchantId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMMerchantRepository cdqmMerchantRepository = _DataRepositoryFactory.GetDataRepository<ICDQMMerchantRepository>();

                cdqmMerchantRepository.Remove(merchantId);
            });
        }

        public CDQMMerchant GetCDQMMerchant(int merchantId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMMerchantRepository cdqmMerchantRepository = _DataRepositoryFactory.GetDataRepository<ICDQMMerchantRepository>();

                CDQMMerchant cdqmMerchantEntity = cdqmMerchantRepository.Get(merchantId);
                if (cdqmMerchantEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CDQMMerchant with ID of {0} is not in database", merchantId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return cdqmMerchantEntity;
            });
        }

        public CDQMMerchant[] GetAllCDQMMerchants()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMMerchantRepository cdqmMerchantRepository = _DataRepositoryFactory.GetDataRepository<ICDQMMerchantRepository>();

                IEnumerable<CDQMMerchant> cdqmMerchantes = cdqmMerchantRepository.Get().ToArray();

                return cdqmMerchantes.ToArray();
            });
        }


        #endregion

        #region CDQMProduct operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CDQMProduct UpdateCDQMProduct(CDQMProduct cdqmProduct)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMProductRepository cdqmProductRepository = _DataRepositoryFactory.GetDataRepository<ICDQMProductRepository>();

                CDQMProduct updatedEntity = null;

                if (cdqmProduct.ProductId == 0)
                    updatedEntity = cdqmProductRepository.Add(cdqmProduct);
                else
                    updatedEntity = cdqmProductRepository.Update(cdqmProduct);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCDQMProduct(int productId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMProductRepository cdqmProductRepository = _DataRepositoryFactory.GetDataRepository<ICDQMProductRepository>();

                cdqmProductRepository.Remove(productId);
            });
        }

        public CDQMProduct GetCDQMProduct(int productId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMProductRepository cdqmProductRepository = _DataRepositoryFactory.GetDataRepository<ICDQMProductRepository>();

                CDQMProduct cdqmProductEntity = cdqmProductRepository.Get(productId);
                if (cdqmProductEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CDQMProduct with ID of {0} is not in database", productId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return cdqmProductEntity;
            });
        }

        public CDQMProduct[] GetAllCDQMProducts()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMProductRepository cdqmProductRepository = _DataRepositoryFactory.GetDataRepository<ICDQMProductRepository>();

                IEnumerable<CDQMProduct> cdqmProducts = cdqmProductRepository.Get().ToArray();

                return cdqmProducts.ToArray();
            });
        }


        #endregion

        #region CDQMTitle operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CDQMTitle UpdateCDQMTitle(CDQMTitle cdqmTitle)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMTitleRepository cdqmTitleRepository = _DataRepositoryFactory.GetDataRepository<ICDQMTitleRepository>();

                CDQMTitle updatedEntity = null;

                if (cdqmTitle.TitleId == 0)
                    updatedEntity = cdqmTitleRepository.Add(cdqmTitle);
                else
                    updatedEntity = cdqmTitleRepository.Update(cdqmTitle);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCDQMTitle(int titleId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMTitleRepository cdqmTitleRepository = _DataRepositoryFactory.GetDataRepository<ICDQMTitleRepository>();

                cdqmTitleRepository.Remove(titleId);
            });
        }

        public CDQMTitle GetCDQMTitle(int titleId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMTitleRepository cdqmTitleRepository = _DataRepositoryFactory.GetDataRepository<ICDQMTitleRepository>();

                CDQMTitle cdqmTitleEntity = cdqmTitleRepository.Get(titleId);
                if (cdqmTitleEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CDQMTitle with ID of {0} is not in database", titleId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return cdqmTitleEntity;
            });
        }

        public CDQMTitle[] GetAllCDQMTitles()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMTitleRepository cdqmTitleRepository = _DataRepositoryFactory.GetDataRepository<ICDQMTitleRepository>();

                IEnumerable<CDQMTitle> cdqmTitles = cdqmTitleRepository.Get().ToArray();

                return cdqmTitles.ToArray();
            });
        }


        #endregion

        #region CDQMCustomerDuplicate operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CDQMCustomerDuplicate UpdateCDQMCustomerDuplicate(CDQMCustomerDuplicate cdqmCustomerDuplicate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMCustomerDuplicateRepository cdqmCustomerDuplicateRepository = _DataRepositoryFactory.GetDataRepository<ICDQMCustomerDuplicateRepository>();

                CDQMCustomerDuplicate updatedEntity = null;

                if (cdqmCustomerDuplicate.CUST_DUPLICATES_ID == 0)
                    updatedEntity = cdqmCustomerDuplicateRepository.Add(cdqmCustomerDuplicate);
                else
                    updatedEntity = cdqmCustomerDuplicateRepository.Update(cdqmCustomerDuplicate);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCDQMCustomerDuplicate(int cod_CUST_ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMCustomerDuplicateRepository cdqmCustomerDuplicateRepository = _DataRepositoryFactory.GetDataRepository<ICDQMCustomerDuplicateRepository>();

                cdqmCustomerDuplicateRepository.Remove(cod_CUST_ID);
            });
        }

        public CDQMCustomerDuplicate GetCDQMCustomerDuplicate(int cod_CUST_ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMCustomerDuplicateRepository cdqmCustomerDuplicateRepository = _DataRepositoryFactory.GetDataRepository<ICDQMCustomerDuplicateRepository>();

                CDQMCustomerDuplicate cdqmCustomerDuplicateEntity = cdqmCustomerDuplicateRepository.Get(cod_CUST_ID);
                if (cdqmCustomerDuplicateEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CDQMCustomerDuplicate with ID of {0} is not in database", cod_CUST_ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return cdqmCustomerDuplicateEntity;
            });
        }

        public CDQMCustomerDuplicate[] GetAllCDQMCustomerDuplicates()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMCustomerDuplicateRepository cdqmCustomerDuplicateRepository = _DataRepositoryFactory.GetDataRepository<ICDQMCustomerDuplicateRepository>();

                IEnumerable<CDQMCustomerDuplicate> cdqmCustomerDuplicates = cdqmCustomerDuplicateRepository.Get().ToArray();

                return cdqmCustomerDuplicates.ToArray();
            });
        }

        public string[] GetCustomerGroupIDs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMCustomerDuplicateRepository cdqmCustomerDuplicateRepository = _DataRepositoryFactory.GetDataRepository<ICDQMCustomerDuplicateRepository>();

                List<string> ids = cdqmCustomerDuplicateRepository.GetDistinctGroupIDs();

                return ids.ToArray();
            });
        }


        #endregion

        #region CDQMCustomerPersistent operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CDQMCustomerPersistent UpdateCDQMCustomerPersistent(CDQMCustomerPersistent cdqmCustomerPersistent)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMCustomerPersistentRepository cdqmCustomerPersistentRepository = _DataRepositoryFactory.GetDataRepository<ICDQMCustomerPersistentRepository>();

                CDQMCustomerPersistent updatedEntity = null;

                if (cdqmCustomerPersistent.CUSTOMER_PERSISTENT_ID == 0)
                    updatedEntity = cdqmCustomerPersistentRepository.Add(cdqmCustomerPersistent);
                else
                    updatedEntity = cdqmCustomerPersistentRepository.Update(cdqmCustomerPersistent);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCDQMCustomerPersistent(int cod_CUST_ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMCustomerPersistentRepository cdqmCustomerPersistentRepository = _DataRepositoryFactory.GetDataRepository<ICDQMCustomerPersistentRepository>();

                cdqmCustomerPersistentRepository.Remove(cod_CUST_ID);
            });
        }

        public CDQMCustomerPersistent GetCDQMCustomerPersistent(int cod_CUST_ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMCustomerPersistentRepository cdqmCustomerPersistentRepository = _DataRepositoryFactory.GetDataRepository<ICDQMCustomerPersistentRepository>();

                CDQMCustomerPersistent cdqmCustomerPersistentEntity = cdqmCustomerPersistentRepository.Get(cod_CUST_ID);
                if (cdqmCustomerPersistentEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CDQMCustomerPersistent with ID of {0} is not in database", cod_CUST_ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return cdqmCustomerPersistentEntity;
            });
        }

        public CDQMCustomerPersistent[] GetAllCDQMCustomerPersistents()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMCustomerPersistentRepository cdqmCustomerPersistentRepository = _DataRepositoryFactory.GetDataRepository<ICDQMCustomerPersistentRepository>();

                IEnumerable<CDQMCustomerPersistent> cdqmCustomerPersistents = cdqmCustomerPersistentRepository.Get().ToArray();

                return cdqmCustomerPersistents.ToArray();
            });
        }

        public CDQMCustomerPersistent[] GetCustomerPersistents(string groupId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMCustomerPersistentRepository cdqmCustomerPersistentRepository = _DataRepositoryFactory.GetDataRepository<ICDQMCustomerPersistentRepository>();
                ICDQMCustomerDuplicateRepository cdqmCustomerDuplicateRepository = _DataRepositoryFactory.GetDataRepository<ICDQMCustomerDuplicateRepository>();

                var duplicates = cdqmCustomerDuplicateRepository.GetCustomerDuplicates(groupId);
                List<CDQMCustomerPersistent> cdqmCustomerPersistents = cdqmCustomerPersistentRepository.GetCustomerPersistentByGroupId(groupId);

                // List<CDQMCustomerPersistent> customerPersistents = new List<CDQMCustomerPersistent>();

                //foreach (var persistent in cdqmCustomerPersistents)
                //{
                //    var duplicate = duplicates.Where(c => c.COD_CUST_ID == persistent.COD_CUST_ID).FirstOrDefault();

                //    if (duplicate != null)
                //    {
                //        if (!duplicate.NotDuplicate)
                //            customerPersistents.Add(persistent);
                //    }
                //    else
                //        customerPersistents.Add(persistent);
                //}

                return cdqmCustomerPersistents.ToArray();
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public CDQMCustomerPersistent [] UpdateCustomer(CDQMCustomerPersistent cdqmCustomerPersistent)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR,GROUP_SUPER_USER, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICDQMCustomerDuplicateRepository cdqmCustomerDuplicateRepository = _DataRepositoryFactory.GetDataRepository<ICDQMCustomerDuplicateRepository>();

                var duplicate = cdqmCustomerDuplicateRepository.GetCustomerDuplicate(cdqmCustomerPersistent.COD_CUST_ID);
                duplicate.NotDuplicate = true;

                cdqmCustomerDuplicateRepository.Update(duplicate);

                var customers = GetCustomerPersistents(duplicate.COD_GROUP_ID);

                return customers;
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


        

        #endregion

    }
}
