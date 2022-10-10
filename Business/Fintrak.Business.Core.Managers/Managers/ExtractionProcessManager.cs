using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration.Install;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Transactions;
using CoreTechs.Common.Text;
using DecryptOperation;
using Fintrak.Business.Core.Contracts;
using Fintrak.Data.Core.Contracts;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Data;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Common.Utils;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Core.Framework;
using Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;


namespace Fintrak.Business.Core.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class ExtractionProcessManager : ManagerBase, IExtractionProcessService
    {
        public ExtractionProcessManager()
        {

        }

        public ExtractionProcessManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        const string SOLUTION_NAME = "CORE";
        const string SOLUTION_ALIAS = "Core";
        const string MODULE_NAME = "FIN_EXTRACTION_PROCESS";
        const string MODULE_ALIAS = "Extraction & Process";


        const string GROUP_ADMINISTRATOR = "Administrator";
        const string GROUP_BUSINESS = "Business";
        const string GROUP_SUPER_BUSINESS = "Super Business";
        const string GROUP_USER = "User";

        string _additionalInfo = string.Empty;
        // string HOB = "Not Selected";

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
                        var superBusinessRole = roleRepository.Get().Where(c => c.Name == GROUP_SUPER_BUSINESS && c.SolutionId == solution.SolutionId).FirstOrDefault();
                        var businessRole = roleRepository.Get().Where(c => c.Name == GROUP_BUSINESS && c.SolutionId == solution.SolutionId).FirstOrDefault();

                        int menuIndex = 0;

                        //register menu
                        var root = new systemCoreEntities.Menu()
                        {
                            Name = "EXTRACTION_PROCESSING",
                            Alias = "Extraction & Processing",
                            Action = "",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "extraction_processing_image",
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
                            MenuId = root.EntityId,
                            RoleId = businessRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        var setupMenu = new systemCoreEntities.Menu()
                        {
                            Name = "SETUP",
                            Alias = "Setup",
                            Action = "",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "setup_image",
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

                        setupMenu = menuRepository.Add(setupMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = setupMenu.EntityId,
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
                            MenuId = setupMenu.EntityId,
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
                            MenuId = setupMenu.EntityId,
                            RoleId = businessRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        var actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "PACKAGE_SETUP",
                            Alias = "Package Setup",
                            Action = "PACKAGE_SETUP",
                            ActionUrl = "core-packagesetup-edit",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = setupMenu.EntityId,
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
                            Name = "CLOSED_PERIOD_TEMPLATE",
                            Alias = "Closed Period Templates",
                            Action = "CLOSED_PERIOD_TEMPLATE",
                            ActionUrl = "core-closedperiodtemplate-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = setupMenu.EntityId,
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
                            Name = "RUN_DATE",
                            Alias = "Run Date",
                            Action = "RUN_DATE",
                            ActionUrl = "core-solutionrundate-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = setupMenu.EntityId,
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
                            Name = "CLOSED_PERIOD",
                            Alias = @"Open\Closed Period",
                            Action = "CLOSED_PERIOD",
                            ActionUrl = "core-closedperiod-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = setupMenu.EntityId,
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

                        var extractionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "EXTRACTION",
                            Alias = "Extraction",
                            Action = "",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "extraction_image",
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

                        extractionMenu = menuRepository.Add(extractionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = extractionMenu.EntityId,
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
                            MenuId = extractionMenu.EntityId,
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
                            MenuId = extractionMenu.EntityId,
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
                            Name = "EXTRACTION_MANAGER",
                            Alias = "Extraction Manager",
                            Action = "EXTRACTION_MANAGER",
                            ActionUrl = "core-extraction-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = extractionMenu.EntityId,
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
                            Name = "RUN_EXTRACTION",
                            Alias = "Run Extraction",
                            Action = "RUN_EXTRACTION",
                            ActionUrl = "core-runextraction-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = extractionMenu.EntityId,
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

                        var processMenu = new systemCoreEntities.Menu()
                        {
                            Name = "PROCESS",
                            Alias = "Process",
                            Action = "",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "process_image",
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

                        processMenu = menuRepository.Add(processMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = processMenu.EntityId,
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
                            MenuId = processMenu.EntityId,
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
                            MenuId = processMenu.EntityId,
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
                            Name = "PROCESS_MANAGER",
                            Alias = "Process Manager",
                            Action = "PROCESS_MANAGER",
                            ActionUrl = "core-process-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = processMenu.EntityId,
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
                            Name = "RUN_PROCESS",
                            Alias = "Run Process",
                            Action = "RUN_PROCESS",
                            ActionUrl = "core-runprocess-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = processMenu.EntityId,
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

                        var uploadMenu = new systemCoreEntities.Menu()
                        {
                            Name = "UPLOAD",
                            Alias = "Upload",
                            Action = "",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "upload_image",
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

                        uploadMenu = menuRepository.Add(uploadMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = uploadMenu.EntityId,
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
                            MenuId = uploadMenu.EntityId,
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
                            MenuId = uploadMenu.EntityId,
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
                            Name = "UPLOAD_MANAGER",
                            Alias = "Upload Manager",
                            Action = "UPLOAD_MANAGER",
                            ActionUrl = "core-upload-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = uploadMenu.EntityId,
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
                            Name = "RUN_UPLOAD",
                            Alias = "Run Upload",
                            Action = "RUN_UPLOAD",
                            ActionUrl = "core-runupload-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = uploadMenu.EntityId,
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


                    }

                    ts.Complete();
                }

            });

        }


        #region Extraction operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Extraction UpdateExtraction(Extraction extraction)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionRepository extractionRepository = _DataRepositoryFactory.GetDataRepository<IExtractionRepository>();

                Extraction updatedEntity = null;

                if (extraction.ExtractionId == 0)
                    updatedEntity = extractionRepository.Add(extraction);
                else
                    updatedEntity = extractionRepository.Update(extraction);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteExtraction(int extractionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionRepository extractionRepository = _DataRepositoryFactory.GetDataRepository<IExtractionRepository>();

                extractionRepository.Remove(extractionId);
            });
        }

        public Extraction GetExtraction(int extractionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionRepository extractionRepository = _DataRepositoryFactory.GetDataRepository<IExtractionRepository>();

                Extraction extractionEntity = extractionRepository.Get(extractionId);
                if (extractionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Extraction with ID of {0} is not in database", extractionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return extractionEntity;
            });
        }

        public Extraction[] GetAllExtractions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionRepository extractionRepository = _DataRepositoryFactory.GetDataRepository<IExtractionRepository>();

                IEnumerable<Extraction> extractions = extractionRepository.Get().OrderBy(c => c.Position).ToArray();

                return extractions.ToArray();
            });
        }

        public ExtractionData[] GetExtractions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionRepository extractionRepository = _DataRepositoryFactory.GetDataRepository<IExtractionRepository>();

                var solutions = GetSolutions();
                var solutionIds = solutions.Where(c => c.Active).Select(c => c.EntityId).Distinct();

                List<ExtractionData> extractions = new List<ExtractionData>();
                IEnumerable<ExtractionInfo> extractionInfos = extractionRepository.GetExtractions().Where(c => solutionIds.Contains(c.Extraction.SolutionId)).OrderBy(c => c.Extraction.Position).ToArray();

                foreach (var extractionInfo in extractionInfos)
                {
                    extractions.Add(
                        new ExtractionData
                        {
                            ExtractionId = extractionInfo.Extraction.EntityId,
                            Title = extractionInfo.Extraction.Title,
                            RunType = extractionInfo.Extraction.RunType,
                            RunTypeName = extractionInfo.Extraction.RunType.ToString(),
                            PackageName = extractionInfo.Extraction.PackageName,
                            PackagePath = extractionInfo.Extraction.PackagePath,
                            ProcedureName = extractionInfo.Extraction.ProcedureName,
                            ScriptText = extractionInfo.Extraction.ScriptText,
                            NeedArchiveAction = extractionInfo.Extraction.NeedArchiveAction,
                            SolutionId = extractionInfo.Extraction.SolutionId,
                            Position = extractionInfo.Extraction.Position,
                            SolutionName = GetSolutionName(solutions, extractionInfo.Extraction.SolutionId),
                            Active = extractionInfo.Extraction.Active
                        });
                }

                return extractions.ToArray();
            });
        }

        public ExtractionData[] GetExtractionByLogin(string loginID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionRepository extractionRepository = _DataRepositoryFactory.GetDataRepository<IExtractionRepository>();
                IExtractionRoleRepository extractionRoleRepository = _DataRepositoryFactory.GetDataRepository<IExtractionRoleRepository>();
                IUserRoleRepository userRoleRepository = _DataRepositoryFactory.GetDataRepository<IUserRoleRepository>();

                var roleIds = userRoleRepository.GetUserRoleInfo().Where(c => c.UserSetup.LoginID == loginID).Select(c => c.Role.RoleId).Distinct();

                var extractionIds = extractionRoleRepository.GetExtractionRoles().Where(c => roleIds.Contains(c.ExtractionRole.RoleId)).Select(c => c.Extraction.ExtractionId).Distinct();

                var solutions = GetSolutions();
                var solutionIds = solutions.Where(c => c.Active).Select(c => c.EntityId).Distinct();

                List<ExtractionData> extractions = new List<ExtractionData>();
                IEnumerable<ExtractionInfo> extractionInfos = extractionRepository.GetExtractions().Where(c => extractionIds.Contains(c.Extraction.ExtractionId) && solutionIds.Contains(c.Extraction.SolutionId)).OrderBy(c => c.Extraction.Position).ToArray();

                foreach (var extractionInfo in extractionInfos)
                {
                    extractions.Add(
                        new ExtractionData
                        {
                            ExtractionId = extractionInfo.Extraction.EntityId,
                            Title = extractionInfo.Extraction.Title,
                            RunType = extractionInfo.Extraction.RunType,
                            RunTypeName = extractionInfo.Extraction.RunType.ToString(),
                            PackageName = extractionInfo.Extraction.PackageName,
                            PackagePath = extractionInfo.Extraction.PackagePath,
                            ProcedureName = extractionInfo.Extraction.ProcedureName,
                            ScriptText = extractionInfo.Extraction.ScriptText,
                            NeedArchiveAction = extractionInfo.Extraction.NeedArchiveAction,
                            SolutionId = extractionInfo.Extraction.SolutionId,
                            Position = extractionInfo.Extraction.Position,
                            SolutionName = GetSolutionName(solutions, extractionInfo.Extraction.SolutionId),
                            Active = extractionInfo.Extraction.Active
                        });
                }

                return extractions.ToArray();
            });
        }

        public ExtractionData[] GetExtractionBySolution(int solutionId, string loginID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionRepository extractionRepository = _DataRepositoryFactory.GetDataRepository<IExtractionRepository>();
                IExtractionRoleRepository extractionRoleRepository = _DataRepositoryFactory.GetDataRepository<IExtractionRoleRepository>();
                IUserRoleRepository userRoleRepository = _DataRepositoryFactory.GetDataRepository<IUserRoleRepository>();

                var roleIds = userRoleRepository.GetUserRoleInfo().Where(c => c.UserSetup.LoginID == loginID).Select(c => c.Role.RoleId).Distinct();

                //var processIds = processRoleRepository.GetProcessRoles().Where(c => roleIds.Contains(c.ProcessRole.RoleId)).Select(c => c.Process.ProcessId).Distinct();
                var extractionIds = extractionRoleRepository.GetExtractionRoles().Where(c => roleIds.Contains(c.ExtractionRole.RoleId)).Select(c => c.Extraction.ExtractionId).Distinct();

                //var modules = GetModules();
                //var moduleIds = modules.Where(c => c.SolutionId == solutionId && c.Active).Select(c => c.EntityId).Distinct();

                var solutions = GetSolutions();
                var solutionIds = solutions.Where(c => c.SolutionId == solutionId && c.Active).Select(c => c.EntityId).Distinct();

                List<ExtractionData> extractions = new List<ExtractionData>();
                IEnumerable<ExtractionInfo> extractionInfos = extractionRepository.GetExtractions().Where(c => extractionIds.Contains(c.Extraction.ExtractionId) && solutionIds.Contains(c.Extraction.SolutionId)).OrderBy(c => c.Extraction.Position).ToArray();

                foreach (var extractionInfo in extractionInfos)
                {
                    extractions.Add(
                        new ExtractionData
                        {
                            ExtractionId = extractionInfo.Extraction.EntityId,
                            Title = extractionInfo.Extraction.Title,
                            RunType = extractionInfo.Extraction.RunType,
                            RunTypeName = extractionInfo.Extraction.RunType.ToString(),
                            PackageName = extractionInfo.Extraction.PackageName,
                            PackagePath = extractionInfo.Extraction.PackagePath,
                            ProcedureName = extractionInfo.Extraction.ProcedureName,
                            ScriptText = extractionInfo.Extraction.ScriptText,
                            NeedArchiveAction = extractionInfo.Extraction.NeedArchiveAction,
                            SolutionId = extractionInfo.Extraction.SolutionId,
                            Position = extractionInfo.Extraction.Position,
                            SolutionName = GetSolutionName(solutions, extractionInfo.Extraction.SolutionId),
                            Active = extractionInfo.Extraction.Active
                        });
                }

                return extractions.ToArray();
            });
        }

        #endregion

        #region ExtractionRole operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ExtractionRole UpdateExtractionRole(ExtractionRole extractionRole)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionRoleRepository extractionRoleRepository = _DataRepositoryFactory.GetDataRepository<IExtractionRoleRepository>();

                ExtractionRole updatedEntity = null;

                if (extractionRole.ExtractionRoleId == 0)
                    updatedEntity = extractionRoleRepository.Add(extractionRole);
                else
                    updatedEntity = extractionRoleRepository.Update(extractionRole);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteExtractionRole(int extractionRoleId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionRoleRepository extractionRoleRepository = _DataRepositoryFactory.GetDataRepository<IExtractionRoleRepository>();

                extractionRoleRepository.Remove(extractionRoleId);
            });
        }

        public ExtractionRole GetExtractionRole(int extractionRoleId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionRoleRepository extractionRoleRepository = _DataRepositoryFactory.GetDataRepository<IExtractionRoleRepository>();

                ExtractionRole extractionRoleEntity = extractionRoleRepository.Get(extractionRoleId);
                if (extractionRoleEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ExtractionRole with ID of {0} is not in database", extractionRoleId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return extractionRoleEntity;
            });
        }

        public ExtractionRole[] GetAllExtractionRoles()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionRoleRepository extractionRoleRepository = _DataRepositoryFactory.GetDataRepository<IExtractionRoleRepository>();

                IEnumerable<ExtractionRole> extractionRoles = extractionRoleRepository.Get();

                return extractionRoles.ToArray();
            });
        }

        public ExtractionRoleData[] GetExtractionRoles()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionRoleRepository extractionRoleRepository = _DataRepositoryFactory.GetDataRepository<IExtractionRoleRepository>();

                var solutions = GetSolutions();
                var solutionIds = solutions.Where(c => c.Active).Select(c => c.EntityId).Distinct();

                var roles = GetRoles();

                List<ExtractionRoleData> extractionRoles = new List<ExtractionRoleData>();
                IEnumerable<ExtractionRoleInfo> extractionRoleInfos = extractionRoleRepository.GetExtractionRoles().Where(c => solutionIds.Contains(c.Extraction.SolutionId)).ToArray();

                foreach (var extractionRoleInfo in extractionRoleInfos)
                {
                    extractionRoles.Add(
                        new ExtractionRoleData
                        {
                            ExtractionRoleId = extractionRoleInfo.ExtractionRole.EntityId,
                            RoleId = extractionRoleInfo.ExtractionRole.RoleId,
                            RoleName = GetRoleName(roles, extractionRoleInfo.ExtractionRole.RoleId),
                            ExtractionId = extractionRoleInfo.Extraction.EntityId,
                            ExtractionName = extractionRoleInfo.Extraction.Title,
                            SolutionId = extractionRoleInfo.Extraction.SolutionId,
                            SolutionName = GetSolutionName(solutions, extractionRoleInfo.Extraction.SolutionId),
                            Active = extractionRoleInfo.ExtractionRole.Active
                        });
                }

                return extractionRoles.ToArray();
            });
        }

        public ExtractionRoleData[] GetExtractionRoleByExtraction(int extractionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionRoleRepository extractionRoleRepository = _DataRepositoryFactory.GetDataRepository<IExtractionRoleRepository>();

                ISolutionRepository solutionRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRepository>();

                var solutions = GetSolutions();
                var solutionIds = solutions.Where(c => c.Active).Select(c => c.EntityId).Distinct();

                var roles = GetRoles();

                List<ExtractionRoleData> extractionRoles = new List<ExtractionRoleData>();
                IEnumerable<ExtractionRoleInfo> extractionRoleInfos = extractionRoleRepository.GetExtractionRoleByExtraction(extractionId).Where(c => solutionIds.Contains(c.Extraction.SolutionId)).ToArray();

                foreach (var extractionRoleInfo in extractionRoleInfos)
                {
                    extractionRoles.Add(
                        new ExtractionRoleData
                        {
                            ExtractionRoleId = extractionRoleInfo.ExtractionRole.EntityId,
                            RoleId = extractionRoleInfo.ExtractionRole.RoleId,
                            RoleName = GetRoleName(roles, extractionRoleInfo.ExtractionRole.RoleId),
                            ExtractionId = extractionRoleInfo.Extraction.EntityId,
                            ExtractionName = extractionRoleInfo.Extraction.Title,
                            SolutionId = extractionRoleInfo.Extraction.SolutionId,
                            SolutionName = GetSolutionName(solutions, extractionRoleInfo.Extraction.SolutionId),
                            LongDescription = GetRoleName(roles, extractionRoleInfo.ExtractionRole.RoleId) + ' ' + GetSolutionName(solutions, extractionRoleInfo.Extraction.SolutionId),
                            Active = extractionRoleInfo.ExtractionRole.Active
                        });
                }

                return extractionRoles.ToArray();
            });
        }

        #endregion

        #region PackageSetup operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PackageSetup UpdatePackageSetup(PackageSetup packageSetup)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPackageSetupRepository packageSetupRepository = _DataRepositoryFactory.GetDataRepository<IPackageSetupRepository>();

                PackageSetup updatedEntity = null;

                if (packageSetup.PackageSetupId == 0)
                    updatedEntity = packageSetupRepository.Add(packageSetup);
                else
                    updatedEntity = packageSetupRepository.Update(packageSetup);

                return updatedEntity;
            });
        }

        public PackageSetup GetFirstPackageSetup()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPackageSetupRepository packageSetupRepository = _DataRepositoryFactory.GetDataRepository<IPackageSetupRepository>();

                PackageSetup packageSetup = packageSetupRepository.Get().FirstOrDefault();

                return packageSetup;
            });
        }

        #endregion

        #region Processes operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Processes UpdateProcess(Processes Processes)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessRepository processRepository = _DataRepositoryFactory.GetDataRepository<IProcessRepository>();

                Processes updatedEntity = null;

                if (Processes.ProcessId == 0)
                    updatedEntity = processRepository.Add(Processes);
                else
                    updatedEntity = processRepository.Update(Processes);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteProcess(int processId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessRepository processRepository = _DataRepositoryFactory.GetDataRepository<IProcessRepository>();

                processRepository.Remove(processId);
            });
        }

        public Processes GetProcess(int processId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessRepository processRepository = _DataRepositoryFactory.GetDataRepository<IProcessRepository>();

                Processes processEntity = processRepository.Get(processId);
                if (processEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Process with ID of {0} is not in database", processId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return processEntity;
            });
        }

        public Processes[] GetAllProcesses()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessRepository processRepository = _DataRepositoryFactory.GetDataRepository<IProcessRepository>();

                IEnumerable<Processes> processes = processRepository.Get().OrderBy(c => c.Position).ToArray();

                return processes.ToArray();
            });
        }

        public ProcessData[] GetProcesses()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessRepository processRepository = _DataRepositoryFactory.GetDataRepository<IProcessRepository>();

                var modules = GetModules();
                var moduleIds = modules.Where(c => c.Active).Select(c => c.EntityId).Distinct();

                List<ProcessData> processs = new List<ProcessData>();
                IEnumerable<ProcessInfo> processInfos = processRepository.GetProcesses().Where(c => moduleIds.Contains(c.Processes.ModuleId)).OrderBy(c => c.Processes.Position).ToArray();

                foreach (var processInfo in processInfos)
                {
                    processs.Add(
                        new ProcessData
                        {
                            ProcessId = processInfo.Processes.EntityId,
                            Title = processInfo.Processes.Title,
                            RunType = processInfo.Processes.RunType,
                            RunTypeName = processInfo.Processes.RunType.ToString(),
                            PackageName = processInfo.Processes.PackageName,
                            PackagePath = processInfo.Processes.PackagePath,
                            ModuleId = processInfo.Processes.ModuleId,
                            ModuleName = GetModuleName(modules, processInfo.Processes.ModuleId),
                            Position = processInfo.Processes.Position,
                            Active = processInfo.Processes.Active
                        });
                }

                return processs.OrderBy(c => c.Position).ToArray();
            });
        }

        public ProcessData[] GetProcessBySolution(int solutionId, string loginID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessRepository processRepository = _DataRepositoryFactory.GetDataRepository<IProcessRepository>();
                IProcessRoleRepository processRoleRepository = _DataRepositoryFactory.GetDataRepository<IProcessRoleRepository>();
                IUserRoleRepository userRoleRepository = _DataRepositoryFactory.GetDataRepository<IUserRoleRepository>();

                var roleIds = userRoleRepository.GetUserRoleInfo().Where(c => c.UserSetup.LoginID == loginID).Select(c => c.Role.RoleId).Distinct();

                var processIds = processRoleRepository.GetProcessRoles().Where(c => roleIds.Contains(c.ProcessRole.RoleId)).Select(c => c.Processes.ProcessId).Distinct();

                var modules = GetModules();
                var moduleIds = modules.Where(c => c.SolutionId == solutionId && c.Active).Select(c => c.EntityId).Distinct();

                List<ProcessData> processs = new List<ProcessData>();
                IEnumerable<ProcessInfo> processInfos = processRepository.GetProcesses().Where(c => moduleIds.Contains(c.Processes.ModuleId) && processIds.Contains(c.Processes.ProcessId)).OrderBy(c => c.Processes.Position).ToArray();

                foreach (var processInfo in processInfos)
                {
                    processs.Add(
                        new ProcessData
                        {
                            ProcessId = processInfo.Processes.EntityId,
                            Title = processInfo.Processes.Title,
                            RunType = processInfo.Processes.RunType,
                            RunTypeName = processInfo.Processes.RunType.ToString(),
                            PackageName = processInfo.Processes.PackageName,
                            PackagePath = processInfo.Processes.PackagePath,
                            ModuleId = processInfo.Processes.ModuleId,
                            ModuleName = GetModuleName(modules, processInfo.Processes.ModuleId),
                            Position = processInfo.Processes.Position,
                            Active = processInfo.Processes.Active
                        });
                }

                return processs.OrderBy(c => c.Position).ToArray();
            });
        }

        #endregion

        #region ProcessRole operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ProcessRole UpdateProcessRole(ProcessRole processRole)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessRoleRepository processRoleRepository = _DataRepositoryFactory.GetDataRepository<IProcessRoleRepository>();

                ProcessRole updatedEntity = null;

                if (processRole.ProcessRoleId == 0)
                    updatedEntity = processRoleRepository.Add(processRole);
                else
                    updatedEntity = processRoleRepository.Update(processRole);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteProcessRole(int processRoleId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessRoleRepository processRoleRepository = _DataRepositoryFactory.GetDataRepository<IProcessRoleRepository>();

                processRoleRepository.Remove(processRoleId);
            });
        }

        public ProcessRole GetProcessRole(int processRoleId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessRoleRepository processRoleRepository = _DataRepositoryFactory.GetDataRepository<IProcessRoleRepository>();

                ProcessRole processRoleEntity = processRoleRepository.Get(processRoleId);
                if (processRoleEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ProcessRole with ID of {0} is not in database", processRoleId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return processRoleEntity;
            });
        }

        public ProcessRole[] GetAllProcessRoles()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessRoleRepository processRoleRepository = _DataRepositoryFactory.GetDataRepository<IProcessRoleRepository>();

                IEnumerable<ProcessRole> processRoles = processRoleRepository.Get();

                return processRoles.ToArray();
            });
        }

        public ProcessRoleData[] GetProcessRoles()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessRoleRepository processRoleRepository = _DataRepositoryFactory.GetDataRepository<IProcessRoleRepository>();

                var modules = GetModules();
                var moduleIds = modules.Where(c => c.Active).Select(c => c.EntityId).Distinct();

                var roles = GetRoles();

                List<ProcessRoleData> processRoles = new List<ProcessRoleData>();
                IEnumerable<ProcessRoleInfo> processRoleInfos = processRoleRepository.GetProcessRoles().Where(c => moduleIds.Contains(c.Processes.ModuleId)).ToArray();

                foreach (var processRoleInfo in processRoleInfos)
                {
                    processRoles.Add(
                        new ProcessRoleData
                        {
                            ProcessRoleId = processRoleInfo.ProcessRole.EntityId,
                            RoleId = processRoleInfo.ProcessRole.RoleId,
                            RoleName = GetRoleName(roles, processRoleInfo.ProcessRole.RoleId),
                            ProcessId = processRoleInfo.Processes.EntityId,
                            ProcessName = processRoleInfo.Processes.Title,
                            ModuleId = processRoleInfo.Processes.ModuleId,
                            ModuleName = GetModuleName(modules, processRoleInfo.Processes.ModuleId),
                            Active = processRoleInfo.ProcessRole.Active
                        });
                }

                return processRoles.ToArray();
            });
        }

        public ProcessRoleData[] GetProcessRoleByProcess(int processId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessRoleRepository processRoleRepository = _DataRepositoryFactory.GetDataRepository<IProcessRoleRepository>();

                var modules = GetModules();
                var moduleIds = modules.Where(c => c.Active).Select(c => c.EntityId).Distinct();

                var roles = GetRoles();

                List<ProcessRoleData> processRoles = new List<ProcessRoleData>();
                IEnumerable<ProcessRoleInfo> processRoleInfos = processRoleRepository.GetProcessRoleByProcess(processId).Where(c => moduleIds.Contains(c.Processes.ModuleId)).ToArray();

                foreach (var processRoleInfo in processRoleInfos)
                {
                    processRoles.Add(
                        new ProcessRoleData
                        {
                            ProcessRoleId = processRoleInfo.ProcessRole.EntityId,
                            RoleId = processRoleInfo.ProcessRole.RoleId,
                            RoleName = GetRoleName(roles, processRoleInfo.ProcessRole.RoleId),
                            ProcessId = processRoleInfo.Processes.EntityId,
                            ProcessName = processRoleInfo.Processes.Title,
                            ModuleId = processRoleInfo.Processes.ModuleId,
                            ModuleName = GetModuleName(modules, processRoleInfo.Processes.ModuleId),
                            Active = processRoleInfo.ProcessRole.Active
                        });
                }

                return processRoles.ToArray();
            });
        }


        #endregion

        #region ExtractionTrigger operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ExtractionTrigger UpdateExtractionTrigger(ExtractionTrigger extractionTrigger)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionTriggerRepository extractionTriggerRepository = _DataRepositoryFactory.GetDataRepository<IExtractionTriggerRepository>();

                ExtractionTrigger updatedEntity = null;

                if (extractionTrigger.ExtractionTriggerId == 0)
                    updatedEntity = extractionTriggerRepository.Add(extractionTrigger);
                else
                    updatedEntity = extractionTriggerRepository.Update(extractionTrigger);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteExtractionTrigger(int extractionTriggerId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionTriggerRepository extractionTriggerRepository = _DataRepositoryFactory.GetDataRepository<IExtractionTriggerRepository>();

                extractionTriggerRepository.Remove(extractionTriggerId);
            });
        }

        public ExtractionTrigger GetExtractionTrigger(int extractionTriggerId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionTriggerRepository extractionTriggerRepository = _DataRepositoryFactory.GetDataRepository<IExtractionTriggerRepository>();

                ExtractionTrigger extractionTriggerEntity = extractionTriggerRepository.Get(extractionTriggerId);
                if (extractionTriggerEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ExtractionTrigger with ID of {0} is not in database", extractionTriggerId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return extractionTriggerEntity;
            });
        }

        public ExtractionTrigger[] GetAllExtractionTriggers()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionTriggerRepository extractionTriggerRepository = _DataRepositoryFactory.GetDataRepository<IExtractionTriggerRepository>();

                IEnumerable<ExtractionTrigger> extractionTriggers = extractionTriggerRepository.Get();

                return extractionTriggers.ToArray();
            });
        }

        public ExtractionTriggerData[] GetExtractionTriggers()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionTriggerRepository extractionTriggerRepository = _DataRepositoryFactory.GetDataRepository<IExtractionTriggerRepository>();

                List<ExtractionTriggerData> extractionTriggers = new List<ExtractionTriggerData>();
                IEnumerable<ExtractionTriggerInfo> extractionTriggerInfos = extractionTriggerRepository.GetExtractionTriggers().OrderBy(c => c.Extraction.Title).ToArray();

                foreach (var extractionTriggerInfo in extractionTriggerInfos)
                {
                    extractionTriggers.Add(
                        new ExtractionTriggerData
                        {
                            ExtractionId = extractionTriggerInfo.Extraction.EntityId,
                            ExtractionTitle = extractionTriggerInfo.Extraction.Title,
                            Code = extractionTriggerInfo.ExtractionTrigger.Code,
                            Status = extractionTriggerInfo.ExtractionTrigger.Status,
                            StatusName = extractionTriggerInfo.ExtractionTrigger.Status.ToString(),
                            Remark = extractionTriggerInfo.ExtractionTrigger.Remark,
                            UserName = extractionTriggerInfo.ExtractionTrigger.UserName,
                            StartDate = extractionTriggerInfo.ExtractionTrigger.StartDate,
                            EndDate = extractionTriggerInfo.ExtractionTrigger.EndDate,
                            RunTime = extractionTriggerInfo.ExtractionTrigger.RunTime,
                            NeedToArchive = extractionTriggerInfo.ExtractionTrigger.NeedToArchive,
                            Active = extractionTriggerInfo.ExtractionTrigger.Active
                        });
                }

                return extractionTriggers.ToArray();
            });
        }

        public ExtractionTriggerData[] GetExtractionTriggerByExtraction(int extractionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionTriggerRepository extractionTriggerRepository = _DataRepositoryFactory.GetDataRepository<IExtractionTriggerRepository>();


                List<ExtractionTriggerData> extractionTriggers = new List<ExtractionTriggerData>();
                IEnumerable<ExtractionTriggerInfo> extractionTriggerInfos = extractionTriggerRepository.GetExtractionTriggerByExtraction(extractionId).OrderByDescending(c => c.ExtractionTrigger.ExtractionTriggerId).ToArray();

                foreach (var extractionTriggerInfo in extractionTriggerInfos)
                {
                    extractionTriggers.Add(
                        new ExtractionTriggerData
                        {
                            ExtractionId = extractionTriggerInfo.Extraction.EntityId,
                            ExtractionTitle = extractionTriggerInfo.Extraction.Title,
                            Code = extractionTriggerInfo.ExtractionTrigger.Code,
                            Status = extractionTriggerInfo.ExtractionTrigger.Status,
                            StatusName = extractionTriggerInfo.ExtractionTrigger.Status.ToString(),
                            Remark = extractionTriggerInfo.ExtractionTrigger.Remark,
                            UserName = extractionTriggerInfo.ExtractionTrigger.UserName,
                            StartDate = extractionTriggerInfo.ExtractionTrigger.StartDate,
                            EndDate = extractionTriggerInfo.ExtractionTrigger.EndDate,
                            RunTime = extractionTriggerInfo.ExtractionTrigger.RunTime,
                            NeedToArchive = extractionTriggerInfo.ExtractionTrigger.NeedToArchive,
                            Active = extractionTriggerInfo.ExtractionTrigger.Active
                        });
                }

                return extractionTriggers.ToArray();
            });
        }

        public ExtractionTriggerData[] GetExtractionTriggerByJob(string jobCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionTriggerRepository extractionTriggerRepository = _DataRepositoryFactory.GetDataRepository<IExtractionTriggerRepository>();


                List<ExtractionTriggerData> extractionTriggers = new List<ExtractionTriggerData>();
                IEnumerable<ExtractionTriggerInfo> extractionTriggerInfos = extractionTriggerRepository.GetExtractionTriggerByJob(jobCode).OrderByDescending(c => c.ExtractionTrigger.ExtractionTriggerId).ToArray();

                foreach (var extractionTriggerInfo in extractionTriggerInfos)
                {
                    extractionTriggers.Add(
                        new ExtractionTriggerData
                        {
                            ExtractionId = extractionTriggerInfo.Extraction.EntityId,
                            ExtractionTitle = extractionTriggerInfo.Extraction.Title,
                            Code = extractionTriggerInfo.ExtractionTrigger.Code,
                            Status = extractionTriggerInfo.ExtractionTrigger.Status,
                            StatusName = extractionTriggerInfo.ExtractionTrigger.Status.ToString(),
                            Remark = extractionTriggerInfo.ExtractionTrigger.Remark,
                            UserName = extractionTriggerInfo.ExtractionTrigger.UserName,
                            StartDate = extractionTriggerInfo.ExtractionTrigger.StartDate,
                            EndDate = extractionTriggerInfo.ExtractionTrigger.EndDate,
                            RunTime = extractionTriggerInfo.ExtractionTrigger.RunTime,
                            NeedToArchive = extractionTriggerInfo.ExtractionTrigger.NeedToArchive,
                            Active = extractionTriggerInfo.ExtractionTrigger.Active
                        });
                }



                return extractionTriggers.ToArray();
            });
        }

        public ExtractionTriggerData[] GetExtractionTriggerByRunDate(DateTime startDate, DateTime endDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionTriggerRepository extractionTriggerRepository = _DataRepositoryFactory.GetDataRepository<IExtractionTriggerRepository>();


                List<ExtractionTriggerData> extractionTriggers = new List<ExtractionTriggerData>();
                IEnumerable<ExtractionTriggerInfo> extractionTriggerInfos = extractionTriggerRepository.GetExtractionTriggerByRunDate(startDate, endDate).Where(c => c.ExtractionTrigger.Status != PackageStatus.Cancel).OrderByDescending(c => c.ExtractionTrigger.ExtractionTriggerId).ToArray();

                foreach (var extractionTriggerInfo in extractionTriggerInfos)
                {
                    extractionTriggers.Add(
                        new ExtractionTriggerData
                        {
                            ExtractionId = extractionTriggerInfo.Extraction.EntityId,
                            ExtractionTitle = extractionTriggerInfo.Extraction.Title,
                            Code = extractionTriggerInfo.ExtractionTrigger.Code,
                            Status = extractionTriggerInfo.ExtractionTrigger.Status,
                            StatusName = extractionTriggerInfo.ExtractionTrigger.Status.ToString(),
                            Remark = extractionTriggerInfo.ExtractionTrigger.Remark,
                            UserName = extractionTriggerInfo.ExtractionTrigger.UserName,
                            StartDate = extractionTriggerInfo.ExtractionTrigger.StartDate,
                            EndDate = extractionTriggerInfo.ExtractionTrigger.EndDate,
                            RunTime = extractionTriggerInfo.ExtractionTrigger.RunTime,
                            NeedToArchive = extractionTriggerInfo.ExtractionTrigger.NeedToArchive,
                            Active = extractionTriggerInfo.ExtractionTrigger.Active
                        });
                }



                return extractionTriggers.ToArray();
            });
        }

        public ExtractionTriggerData[] GetExtractionTriggerByRunTime(DateTime runTime)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionTriggerRepository extractionTriggerRepository = _DataRepositoryFactory.GetDataRepository<IExtractionTriggerRepository>();


                List<ExtractionTriggerData> extractionTriggers = new List<ExtractionTriggerData>();
                IEnumerable<ExtractionTriggerInfo> extractionTriggerInfos = extractionTriggerRepository.GetExtractionTriggerByRunTime(runTime).OrderBy(c => c.Extraction.Title).ToArray();

                foreach (var extractionTriggerInfo in extractionTriggerInfos)
                {
                    extractionTriggers.Add(
                        new ExtractionTriggerData
                        {
                            ExtractionId = extractionTriggerInfo.Extraction.EntityId,
                            ExtractionTitle = extractionTriggerInfo.Extraction.Title,
                            Code = extractionTriggerInfo.ExtractionTrigger.Code,
                            Status = extractionTriggerInfo.ExtractionTrigger.Status,
                            StatusName = extractionTriggerInfo.ExtractionTrigger.Status.ToString(),
                            Remark = extractionTriggerInfo.ExtractionTrigger.Remark,
                            UserName = extractionTriggerInfo.ExtractionTrigger.UserName,
                            StartDate = extractionTriggerInfo.ExtractionTrigger.StartDate,
                            EndDate = extractionTriggerInfo.ExtractionTrigger.EndDate,
                            RunTime = extractionTriggerInfo.ExtractionTrigger.RunTime,
                            NeedToArchive = extractionTriggerInfo.ExtractionTrigger.NeedToArchive,
                            Active = extractionTriggerInfo.ExtractionTrigger.Active
                        });
                }

                return extractionTriggers.ToArray();
            });
        }

        [OperationBehavior(TransactionScopeRequired = false)]
        public ExtractionTriggerData[] RunExtraction(int jobId, int[] extractionIds, DateTime startDate, DateTime endDate, DateTime runTime)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IExtractionRepository extractionRepository = _DataRepositoryFactory.GetDataRepository<IExtractionRepository>();
                var extractions = extractionRepository.Get().Where(c => extractionIds.Contains(c.EntityId));

                foreach (var ext in extractions)
                {
                    var needToArchive = false;
                    if (string.IsNullOrEmpty(ext.NeedArchiveAction))
                    {
                        var result = SqlDataManager.RunProcedureWithMessage(GetDataConnection(), ext.NeedArchiveAction, new List<SqlParameter>().ToArray());

                        if (result == "true" || result == "false")
                            needToArchive = bool.Parse(result);
                    }

                    var trigger = new ExtractionTrigger()
                    {
                        ExtractionJobId = jobId,
                        ExtractionId = ext.EntityId,
                        Code = UniqueKeyGenerator.RNGCharacterMask(6, 8),
                        Status = PackageStatus.New,
                        StartDate = startDate,
                        EndDate = endDate,
                        RunTime = runTime,
                        Remark = "Not Started",
                        UserName = _LoginName,
                        NeedToArchive = needToArchive,
                        Active = true,
                        Deleted = false,
                        CreatedBy = "",
                        CreatedOn = DateTime.Now,
                        UpdatedBy = "",
                        UpdatedOn = DateTime.Now
                    };

                    UpdateExtractionTrigger(trigger);
                }

                var job = GetExtractionJob(jobId);

                return GetExtractionTriggerByJob(job.Code);
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public ExtractionTriggerData[] CancelExtractions(DateTime startDate, DateTime endDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IExtractionTriggerRepository extractionTriggerRepository = _DataRepositoryFactory.GetDataRepository<IExtractionTriggerRepository>();

                var triggers = extractionTriggerRepository.Get().Where(c => c.StartDate >= startDate && c.EndDate <= endDate && c.UserName == _LoginName);

                using (TransactionScope ts = new TransactionScope())
                {
                    foreach (var trigger in triggers)
                    {
                        if (trigger.Status != PackageStatus.Done || trigger.Status != PackageStatus.Cancel || trigger.Status == PackageStatus.Fail)
                        {
                            //var dbTrigger = extractionTriggerRepository.Get(trigger.ExtractionTriggerId);
                            trigger.Status = PackageStatus.Cancel;
                            trigger.Remark = "Extraction has been cancel.";
                            UpdateExtractionTrigger(trigger);
                        }
                    }

                    ts.Complete();
                }

                return GetExtractionTriggerByRunDate(startDate, endDate);
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public ExtractionTriggerData[] CancelExtractionByCode(string code, DateTime startDate, DateTime endDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IExtractionTriggerRepository extractionTriggerRepository = _DataRepositoryFactory.GetDataRepository<IExtractionTriggerRepository>();

                var triggers = extractionTriggerRepository.Get().Where(c => c.StartDate >= startDate && c.EndDate <= endDate && c.UserName == _LoginName && c.Code == code);

                using (TransactionScope ts = new TransactionScope())
                {
                    foreach (var trigger in triggers)
                    {
                        if (trigger.Status != PackageStatus.Done || trigger.Status != PackageStatus.Cancel || trigger.Status == PackageStatus.Fail)
                        {
                            //var dbTrigger = extractionTriggerRepository.Get(trigger.ExtractionTriggerId);
                            trigger.Status = PackageStatus.Cancel;
                            trigger.Remark = "Extraction has been cancel.";
                            UpdateExtractionTrigger(trigger);
                        }
                    }

                    ts.Complete();
                }

                return GetExtractionTriggerByRunDate(startDate, endDate);
            });
        }

        #endregion

        #region ProcessTrigger operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ProcessTrigger UpdateProcessTrigger(ProcessTrigger processTrigger)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessTriggerRepository processTriggerRepository = _DataRepositoryFactory.GetDataRepository<IProcessTriggerRepository>();

                ProcessTrigger updatedEntity = null;

                if (processTrigger.ProcessTriggerId == 0)
                    updatedEntity = processTriggerRepository.Add(processTrigger);
                else
                    updatedEntity = processTriggerRepository.Update(processTrigger);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteProcessTrigger(int processTriggerId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessTriggerRepository processTriggerRepository = _DataRepositoryFactory.GetDataRepository<IProcessTriggerRepository>();

                processTriggerRepository.Remove(processTriggerId);
            });
        }

        public ProcessTrigger GetProcessTrigger(int processTriggerId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessTriggerRepository processTriggerRepository = _DataRepositoryFactory.GetDataRepository<IProcessTriggerRepository>();

                ProcessTrigger processTriggerEntity = processTriggerRepository.Get(processTriggerId);
                if (processTriggerEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ProcessTrigger with ID of {0} is not in database", processTriggerId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return processTriggerEntity;
            });
        }

        public ProcessTrigger[] GetAllProcessTriggers()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessTriggerRepository processTriggerRepository = _DataRepositoryFactory.GetDataRepository<IProcessTriggerRepository>();

                IEnumerable<ProcessTrigger> processTriggers = processTriggerRepository.Get();

                return processTriggers.ToArray();
            });
        }

        public ProcessTriggerData[] GetProcessTriggers()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessTriggerRepository processTriggerRepository = _DataRepositoryFactory.GetDataRepository<IProcessTriggerRepository>();


                List<ProcessTriggerData> processTriggers = new List<ProcessTriggerData>();
                IEnumerable<ProcessTriggerInfo> processTriggerInfos = processTriggerRepository.GetProcessTriggers().OrderBy(c => c.Processes.Title).ToArray();

                foreach (var processTriggerInfo in processTriggerInfos)
                {
                    processTriggers.Add(
                        new ProcessTriggerData
                        {
                            ProcessId = processTriggerInfo.Processes.EntityId,
                            ProcessTitle = processTriggerInfo.Processes.Title,
                            Code = processTriggerInfo.ProcessTrigger.Code,
                            Status = processTriggerInfo.ProcessTrigger.Status,
                            StatusName = processTriggerInfo.ProcessTrigger.Status.ToString(),
                            StartDate = processTriggerInfo.ProcessTrigger.StartDate,
                            EndDate = processTriggerInfo.ProcessTrigger.EndDate,
                            Remark = processTriggerInfo.ProcessTrigger.Remark,
                            UserName = processTriggerInfo.ProcessTrigger.UserName,
                            RunTime = processTriggerInfo.ProcessTrigger.RunTime,
                            Active = processTriggerInfo.ProcessTrigger.Active
                        });
                }

                return processTriggers.ToArray();
            });
        }

        public ProcessTriggerData[] GetProcessTriggerByProcess(int processId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessTriggerRepository processTriggerRepository = _DataRepositoryFactory.GetDataRepository<IProcessTriggerRepository>();


                List<ProcessTriggerData> processTriggers = new List<ProcessTriggerData>();
                IEnumerable<ProcessTriggerInfo> processTriggerInfos = processTriggerRepository.GetProcessTriggerByProcess(processId).OrderBy(c => c.Processes.Title).ToArray();

                foreach (var processTriggerInfo in processTriggerInfos)
                {
                    processTriggers.Add(
                        new ProcessTriggerData
                        {
                            ProcessId = processTriggerInfo.Processes.EntityId,
                            ProcessTitle = processTriggerInfo.Processes.Title,
                            Code = processTriggerInfo.ProcessTrigger.Code,
                            Status = processTriggerInfo.ProcessTrigger.Status,
                            StatusName = processTriggerInfo.ProcessTrigger.Status.ToString(),
                            StartDate = processTriggerInfo.ProcessTrigger.StartDate,
                            EndDate = processTriggerInfo.ProcessTrigger.EndDate,
                            Remark = processTriggerInfo.ProcessTrigger.Remark,
                            UserName = processTriggerInfo.ProcessTrigger.UserName,
                            RunTime = processTriggerInfo.ProcessTrigger.RunTime,
                            Active = processTriggerInfo.ProcessTrigger.Active
                        });
                }

                return processTriggers.ToArray();
            });
        }

        public ProcessTriggerData[] GetProcessTriggerByRunDate()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessTriggerRepository processTriggerRepository = _DataRepositoryFactory.GetDataRepository<IProcessTriggerRepository>();


                List<ProcessTriggerData> processTriggers = new List<ProcessTriggerData>();
                IEnumerable<ProcessTriggerInfo> processTriggerInfos = processTriggerRepository.GetProcessTriggerByRunDate().Where(c => c.ProcessTrigger.Status != PackageStatus.Cancel).OrderByDescending(c => c.ProcessTrigger.ProcessTriggerId).ToArray();

                foreach (var processTriggerInfo in processTriggerInfos)
                {
                    processTriggers.Add(
                        new ProcessTriggerData
                        {
                            ProcessId = processTriggerInfo.Processes.EntityId,
                            ProcessTitle = processTriggerInfo.Processes.Title,
                            Code = processTriggerInfo.ProcessTrigger.Code,
                            Status = processTriggerInfo.ProcessTrigger.Status,
                            StatusName = processTriggerInfo.ProcessTrigger.Status.ToString(),
                            StartDate = processTriggerInfo.ProcessTrigger.StartDate,
                            EndDate = processTriggerInfo.ProcessTrigger.EndDate,
                            Remark = processTriggerInfo.ProcessTrigger.Remark,
                            UserName = processTriggerInfo.ProcessTrigger.UserName,
                            RunTime = processTriggerInfo.ProcessTrigger.RunTime,
                            Active = processTriggerInfo.ProcessTrigger.Active
                        });
                }

                return processTriggers.ToArray();
            });
        }

        public ProcessTriggerData[] GetProcessTriggerByRunTime(DateTime runTime)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessTriggerRepository processTriggerRepository = _DataRepositoryFactory.GetDataRepository<IProcessTriggerRepository>();


                List<ProcessTriggerData> processTriggers = new List<ProcessTriggerData>();
                IEnumerable<ProcessTriggerInfo> processTriggerInfos = processTriggerRepository.GetProcessTriggerByRunTime(runTime).OrderByDescending(c => c.ProcessTrigger.ProcessTriggerId).ToArray();

                foreach (var processTriggerInfo in processTriggerInfos)
                {
                    processTriggers.Add(
                        new ProcessTriggerData
                        {
                            ProcessId = processTriggerInfo.Processes.EntityId,
                            ProcessTitle = processTriggerInfo.Processes.Title,
                            Code = processTriggerInfo.ProcessTrigger.Code,
                            Status = processTriggerInfo.ProcessTrigger.Status,
                            StatusName = processTriggerInfo.ProcessTrigger.Status.ToString(),
                            StartDate = processTriggerInfo.ProcessTrigger.StartDate,
                            EndDate = processTriggerInfo.ProcessTrigger.EndDate,
                            Remark = processTriggerInfo.ProcessTrigger.Remark,
                            UserName = processTriggerInfo.ProcessTrigger.UserName,
                            RunTime = processTriggerInfo.ProcessTrigger.RunTime,
                            Active = processTriggerInfo.ProcessTrigger.Active
                        });
                }

                return processTriggers.ToArray();
            });
        }

        public ProcessTriggerData[] GetProcessTriggerByJob(string jobCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessTriggerRepository processTriggerRepository = _DataRepositoryFactory.GetDataRepository<IProcessTriggerRepository>();


                List<ProcessTriggerData> processTriggers = new List<ProcessTriggerData>();
                IEnumerable<ProcessTriggerInfo> processTriggerInfos = processTriggerRepository.GetProcessTriggerByJob(jobCode).OrderByDescending(c => c.ProcessTrigger.ProcessTriggerId).ToArray();

                foreach (var processTriggerInfo in processTriggerInfos)
                {
                    processTriggers.Add(
                        new ProcessTriggerData
                        {
                            ProcessId = processTriggerInfo.Processes.EntityId,
                            ProcessTitle = processTriggerInfo.Processes.Title,
                            Code = processTriggerInfo.ProcessTrigger.Code,
                            Status = processTriggerInfo.ProcessTrigger.Status,
                            StatusName = processTriggerInfo.ProcessTrigger.Status.ToString(),
                            Remark = processTriggerInfo.ProcessTrigger.Remark,
                            UserName = processTriggerInfo.ProcessTrigger.UserName,
                            StartDate = processTriggerInfo.ProcessTrigger.StartDate,
                            EndDate = processTriggerInfo.ProcessTrigger.EndDate,
                            RunTime = processTriggerInfo.ProcessTrigger.RunTime,
                            Active = processTriggerInfo.ProcessTrigger.Active
                        });
                }



                return processTriggers.ToArray();
            });
        }

        [OperationBehavior(TransactionScopeRequired = false)]
        public ProcessTriggerData[] RunProcess(int jobId, int[] processIds, DateTime runTime)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IProcessRepository processRepository = _DataRepositoryFactory.GetDataRepository<IProcessRepository>();
                IModuleRepository moduleRepository = _DataRepositoryFactory.GetDataRepository<IModuleRepository>();
                ISolutionRunDateRepository runDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();

                foreach (var id in processIds)
                {
                    var process = processRepository.Get(id);
                    var module = moduleRepository.Get(process.ModuleId);
                    var runDate = runDateRepository.Get().Where(c => c.SolutionId == module.SolutionId).FirstOrDefault();

                    var trigger = new ProcessTrigger()
                    {
                        ProcessJobId = jobId,
                        ProcessId = id,
                        Code = UniqueKeyGenerator.RNGCharacterMask(6, 8),
                        Status = PackageStatus.New,
                        StartDate = runDate.RunDate,//.FirstOfMonth(),
                        EndDate = runDate.RunDate,
                        RunTime = runTime,
                        Remark = "Not Started",
                        UserName = _LoginName,
                        Active = true,
                        Deleted = false,
                        CreatedBy = "",
                        CreatedOn = DateTime.Now,
                        UpdatedBy = "",
                        UpdatedOn = DateTime.Now
                    };

                    UpdateProcessTrigger(trigger);
                }

                var job = GetProcessJob(jobId);

                return GetProcessTriggerByJob(job.Code);
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public ProcessTriggerData[] CancelProcesses(DateTime startDate, DateTime endDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IProcessTriggerRepository processTriggerRepository = _DataRepositoryFactory.GetDataRepository<IProcessTriggerRepository>();

                var triggers = processTriggerRepository.Get().Where(c => c.StartDate >= startDate && c.EndDate <= endDate);

                using (TransactionScope ts = new TransactionScope())
                {
                    foreach (var trigger in triggers)
                    {
                        if (trigger.Status != PackageStatus.Done || trigger.Status != PackageStatus.Cancel || trigger.Status == PackageStatus.Fail)
                        {
                            //var dbTrigger = processTriggerRepository.Get(trigger.ProcessTriggerId);
                            trigger.Status = PackageStatus.Cancel;
                            trigger.Remark = "Process has been cancel.";
                            UpdateProcessTrigger(trigger);
                        }
                    }

                    ts.Complete();
                }

                return GetProcessTriggerByRunDate();
            });
        }

        public ProcessTriggerData[] CancelProcessByCode(string code, DateTime startDate, DateTime endDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IProcessTriggerRepository processTriggerRepository = _DataRepositoryFactory.GetDataRepository<IProcessTriggerRepository>();

                var triggers = processTriggerRepository.Get().Where(c => c.StartDate >= startDate && c.EndDate <= endDate && c.Code == code);

                using (TransactionScope ts = new TransactionScope())
                {
                    foreach (var trigger in triggers)
                    {
                        if (trigger.Status != PackageStatus.Done || trigger.Status != PackageStatus.Cancel || trigger.Status == PackageStatus.Fail)
                        {
                            //var dbTrigger = processTriggerRepository.Get(trigger.ProcessTriggerId);
                            trigger.Status = PackageStatus.Cancel;
                            trigger.Remark = "Process has been cancel.";
                            UpdateProcessTrigger(trigger);
                        }
                    }

                    ts.Complete();
                }

                return GetProcessTriggerByRunDate();
            });
        }

        #endregion

        #region SolutionRunDate operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SolutionRunDate UpdateSolutionRunDate(SolutionRunDate solutionRunDate)
        {

            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IClosedPeriodRepository closedPeriodRepository = _DataRepositoryFactory.GetDataRepository<IClosedPeriodRepository>();
                ISolutionRunDateRepository solutionRunDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();
                ISolutionRepository solutionRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRepository>();

                var closedPeriod = closedPeriodRepository.Get().Where(c => c.SolutionId == solutionRunDate.SolutionId && c.Status).FirstOrDefault();
                //Retrieve excemption solution id
                var solution = solutionRepository.Get().Where(c => c.Name == "FIN_IFRS");

                solutionRunDate.Active = true;

                if (solution != null)
                {
                    if (closedPeriod == null)
                    {
                        NotFoundException ex = new NotFoundException(string.Format("Active closed period not in database"));
                        throw new FaultException<NotFoundException>(ex, ex.Message);
                    }
                }

                SolutionRunDate updatedEntity = null;

                if (solution == null)
                {
                    if (solutionRunDate.RunDate.Date < closedPeriod.Date.FirstOfMonth().Date || solutionRunDate.RunDate.Date > closedPeriod.Date.LastOfMonth().Date)
                    {
                        NotFoundException ex = new NotFoundException(string.Format("Run date must be within active closed period."));
                        throw new FaultException<NotFoundException>(ex, ex.Message);
                    }
                }

                if (solutionRunDate.SolutionRunDateId == 0)
                    updatedEntity = solutionRunDateRepository.Add(solutionRunDate);

                else
                    if (solutionRunDate.SolutionId == 2)
                    {
                        AchiveRecord();
                        updatedEntity = solutionRunDateRepository.Update(solutionRunDate);
                    }
                    else
                    {
                        updatedEntity = solutionRunDateRepository.Update(solutionRunDate);
                    }


                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSolutionRunDate(int solutionRunDateId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISolutionRunDateRepository solutionRunDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();

                solutionRunDateRepository.Remove(solutionRunDateId);
            });
        }

        public SolutionRunDate GetSolutionRunDate(int solutionRunDateId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISolutionRunDateRepository solutionRunDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();

                SolutionRunDate solutionRunDateEntity = solutionRunDateRepository.Get(solutionRunDateId);
                if (solutionRunDateEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SolutionRunDate with ID of {0} is not in database", solutionRunDateId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return solutionRunDateEntity;
            });
        }

        public SolutionRunDate[] GetAllSolutionRunDates()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISolutionRunDateRepository solutionRunDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();

                IEnumerable<SolutionRunDate> solutionRunDates = solutionRunDateRepository.Get().ToArray();

                return solutionRunDates.ToArray();
            });
        }

        public SolutionRunDateData[] GetSolutionRunDates()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISolutionRunDateRepository solutionRunDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();

                ISolutionRepository solutionRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRepository>();

                var solutions = GetSolutions();
                var solutionIds = solutions.Where(c => c.Active).Select(c => c.EntityId).Distinct();

                List<SolutionRunDateData> solutionRunDates = new List<SolutionRunDateData>();
                IEnumerable<SolutionRunDateInfo> solutionRunDateInfos = solutionRunDateRepository.GetSolutionRunDates().Where(c => solutionIds.Contains(c.SolutionRunDate.SolutionId)).ToArray();

                foreach (var solutionRunDateInfo in solutionRunDateInfos)
                {
                    solutionRunDates.Add(
                        new SolutionRunDateData
                        {
                            SolutionRunDateId = solutionRunDateInfo.SolutionRunDate.EntityId,
                            RunDate = solutionRunDateInfo.SolutionRunDate.RunDate,
                            Month = solutionRunDateInfo.SolutionRunDate.RunDate.Month,
                            Year = solutionRunDateInfo.SolutionRunDate.RunDate.Year,
                            SolutionId = solutionRunDateInfo.SolutionRunDate.SolutionId,
                            SolutionName = GetSolutionName(solutions, solutionRunDateInfo.SolutionRunDate.SolutionId),
                            Active = solutionRunDateInfo.SolutionRunDate.Active
                        });
                }

                return solutionRunDates.ToArray();
            });
        }

        public SolutionRunDateData[] GetSolutionRunDateByLogin(string loginID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                systemCoreData.IUserRoleRepository userRoleRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IUserRoleRepository>();
                ISolutionRunDateRepository solutionRunDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();

                var solutionIds = userRoleRepository.GetUserRoleInfo().Where(c => c.UserSetup.LoginID == loginID).Select(c => c.Solution.SolutionId).Distinct();

                var solutions = GetSolutions();
                var solutionId2s = solutions.Where(c => c.Active).Select(c => c.EntityId).Distinct();

                List<SolutionRunDateData> solutionRunDates = new List<SolutionRunDateData>();

                IEnumerable<SolutionRunDateInfo> solutionRunDateInfos = solutionRunDateRepository.GetSolutionRunDates().Where(c => solutionIds.Contains(c.SolutionRunDate.SolutionId) && solutionId2s.Contains(c.SolutionRunDate.SolutionId)).ToArray();

                foreach (var solutionRunDateInfo in solutionRunDateInfos)
                {
                    solutionRunDates.Add(
                        new SolutionRunDateData
                        {
                            SolutionRunDateId = solutionRunDateInfo.SolutionRunDate.EntityId,
                            RunDate = solutionRunDateInfo.SolutionRunDate.RunDate,
                            Month = solutionRunDateInfo.SolutionRunDate.RunDate.Month,
                            Year = solutionRunDateInfo.SolutionRunDate.RunDate.Year,
                            SolutionId = solutionRunDateInfo.SolutionRunDate.SolutionId,
                            SolutionName = GetSolutionName(solutions, solutionRunDateInfo.SolutionRunDate.SolutionId),
                            Active = solutionRunDateInfo.SolutionRunDate.Active
                        });
                }

                return solutionRunDates.ToArray();
            });
        }


        //public SolutionRunDateData[] GetSolutionRunDateByLoginByDefault(string loginID)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        //systemCoreData.IUserRoleRepository userRoleRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IUserRoleRepository>();
        //        IDefaultUserRepository defaultuserRepository = _DataRepositoryFactory.GetDataRepository<IDefaultUserRepository>();
        //        ISolutionRunDateRepository solutionRunDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();

        //        var solutionIds = defaultuserRepository.GetDefaultUseres().Where(c => c.DefaultUser.LoginID == loginID).FirstOrDefault();

        //        var solutions = GetSolutions();
        //        var solutionId2s = solutions.Where(c => c.Active).Select(c => c.EntityId).Distinct();

        //        List<SolutionRunDateData> solutionRunDates = new List<SolutionRunDateData>();

        //        IEnumerable<SolutionRunDateInfo> solutionRunDateInfos = solutionRunDateRepository.GetSolutionRunDates().Where(c => c.SolutionRunDate.SolutionId == solutionIds.DefaultUser.SolutionId).ToArray();

        //        foreach (var solutionRunDateInfo in solutionRunDateInfos)
        //        {
        //            solutionRunDates.Add(
        //                new SolutionRunDateData
        //                {
        //                    SolutionRunDateId = solutionRunDateInfo.SolutionRunDate.EntityId,
        //                    RunDate = solutionRunDateInfo.SolutionRunDate.RunDate,
        //                    Month = solutionRunDateInfo.SolutionRunDate.RunDate.Month,
        //                    Year = solutionRunDateInfo.SolutionRunDate.RunDate.Year,
        //                    SolutionId = solutionRunDateInfo.SolutionRunDate.SolutionId,
        //                    SolutionName = GetSolutionName(solutions, solutionRunDateInfo.SolutionRunDate.SolutionId),
        //                    Active = solutionRunDateInfo.SolutionRunDate.Active
        //                });
        //        }

        //        return solutionRunDates.ToArray();
        //    });
        //}


        public string GetSolutionRunDateByLoginByDefault(string loginID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                //systemCoreData.IUserRoleRepository userRoleRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IUserRoleRepository>();
                IDefaultUserRepository defaultuserRepository = _DataRepositoryFactory.GetDataRepository<IDefaultUserRepository>();
                ISolutionRunDateRepository solutionRunDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();

                var solutionIds = defaultuserRepository.GetDefaultUseres().Where(c => c.DefaultUser.LoginID == loginID).FirstOrDefault();
                int solid = 0;
                if (solutionIds == null)
                {

                    solid = 1;

                }
                else
                {
                    solid = solutionIds.DefaultUser.SolutionId;
                }
                //var solutions = GetSolutions();
                //var solutionId2s = solutions.Where(c => c.Active).Select(c => c.EntityId).Distinct();

                //List<SolutionRunDateData> solutionRunDates = new List<SolutionRunDateData>();

                var solutionIdrun = solutionRunDateRepository.GetSolutionRunDates().Where(c => c.SolutionRunDate.SolutionId == solid).FirstOrDefault();
                object rundate;
                if (solutionIdrun == null)
                {
                    rundate = DateTime.Now;

                }
                else
                {
                    rundate = solutionIdrun.SolutionRunDate.RunDate;
                }
                //var rundate = solutionIdrun.SolutionRunDate.RunDate;
                //if (rundate == null)
                //{
                //    rundate = DateTime.Now;

                //}


                //if (rundate == null)
                //    rundate = DateTime.Now;
                //else
                //    rundate;

                ////return updatedEntity;


                //IEnumerable<SolutionRunDateInfo> solutionRunDateInfos = solutionRunDateRepository.GetSolutionRunDates().Where(c => c.SolutionRunDate.SolutionId == solutionIds.DefaultUser.SolutionId).ToArray();


                //foreach (var solutionRunDateInfo in solutionRunDateInfos)
                //{
                //    solutionRunDates.Add(
                //        new SolutionRunDateData
                //        {
                //            SolutionRunDateId = solutionRunDateInfo.SolutionRunDate.EntityId,
                //            RunDate = solutionRunDateInfo.SolutionRunDate.RunDate,
                //            Month = solutionRunDateInfo.SolutionRunDate.RunDate.Month,

                //            Year = solutionRunDateInfo.SolutionRunDate.RunDate.Year,
                //            SolutionId = solutionRunDateInfo.SolutionRunDate.SolutionId,
                //            SolutionName = GetSolutionName(solutions, solutionRunDateInfo.SolutionRunDate.SolutionId),
                //            Active = solutionRunDateInfo.SolutionRunDate.Active
                //        });
                //}


                return rundate.ToString();
            });

        }


        public void RestoreArchive(int solutionid, DateTime date)
        {

            var connectionString = GetDataConnection();

            int status = 0;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_restore_archived_data", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "SolutionId",
                    Value = solutionid,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "AchRunDate",
                    Value = date,
                });

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        public void AchiveRecord()
        {
            var connectionString = GetDataConnection();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ArchiveRecord", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public SolutionRunDate[] GetRunDate()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISolutionRunDateRepository solutionrundateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();
                IEnumerable<SolutionRunDate> solutionrundates = solutionrundateRepository.GetRunDate().ToArray();


                return solutionrundates.ToArray();
            });
        }

        #endregion

        #region ClosedPeriod operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ClosedPeriod UpdateClosedPeriod(ClosedPeriod closedPeriod)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IClosedPeriodRepository closedPeriodRepository = _DataRepositoryFactory.GetDataRepository<IClosedPeriodRepository>();

                ClosedPeriod updatedEntity = null;

                if (closedPeriod.ClosedPeriodId == 0)
                    updatedEntity = closedPeriodRepository.Add(closedPeriod);
                else
                    updatedEntity = closedPeriodRepository.Update(closedPeriod);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteClosedPeriod(int closedPeriodId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IClosedPeriodRepository closedPeriodRepository = _DataRepositoryFactory.GetDataRepository<IClosedPeriodRepository>();

                closedPeriodRepository.Remove(closedPeriodId);
            });
        }

        public ClosedPeriod GetClosedPeriod(int closedPeriodId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IClosedPeriodRepository closedPeriodRepository = _DataRepositoryFactory.GetDataRepository<IClosedPeriodRepository>();

                ClosedPeriod closedPeriodEntity = closedPeriodRepository.Get(closedPeriodId);
                if (closedPeriodEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ClosedPeriod with ID of {0} is not in database", closedPeriodId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return closedPeriodEntity;
            });
        }

        public ClosedPeriod[] GetAllClosedPeriods()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IClosedPeriodRepository closedPeriodRepository = _DataRepositoryFactory.GetDataRepository<IClosedPeriodRepository>();

                IEnumerable<ClosedPeriod> closedPeriods = closedPeriodRepository.Get().ToArray();

                return closedPeriods.ToArray();
            });
        }

        public ClosedPeriodData[] GetClosedPeriods()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IClosedPeriodRepository closedPeriodRepository = _DataRepositoryFactory.GetDataRepository<IClosedPeriodRepository>();

                var solutions = GetSolutions();
                var solutionIds = solutions.Where(c => c.Active).Select(c => c.EntityId).Distinct();

                List<ClosedPeriodData> closedPeriods = new List<ClosedPeriodData>();
              //  IEnumerable<ClosedPeriodInfo> closedPeriodInfos = closedPeriodRepository.GetClosedPeriods().Where(c => solutionIds.Contains(c.ClosedPeriod.SolutionId)).ToArray();
                IEnumerable<ClosedPeriodInfo> closedPeriodInfos = closedPeriodRepository.GetClosedPeriods().Where(c => solutionIds.Contains(c.ClosedPeriod.SolutionId)).OrderByDescending(c => c.ClosedPeriod.Status).ThenByDescending(c => c.ClosedPeriod.Date).ToArray();
                foreach (var closedPeriodInfo in closedPeriodInfos)
                {
                    closedPeriods.Add(
                        new ClosedPeriodData
                        {
                            ClosedPeriodId = closedPeriodInfo.ClosedPeriod.EntityId,
                            Date = closedPeriodInfo.ClosedPeriod.Date,
                            Month = closedPeriodInfo.ClosedPeriod.Date.Month,
                            Year = closedPeriodInfo.ClosedPeriod.Date.Year,
                            SolutionId = closedPeriodInfo.ClosedPeriod.SolutionId,
                            SolutionName = GetSolutionName(solutions, closedPeriodInfo.ClosedPeriod.SolutionId),
                            Status = closedPeriodInfo.ClosedPeriod.Status,
                            Active = closedPeriodInfo.ClosedPeriod.Active
                        });
                }

                return closedPeriods.ToArray();
            });
        }

        public ClosedPeriodData[] GetClosedPeriodByLogin(string loginID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUserRoleRepository userRoleRepository = _DataRepositoryFactory.GetDataRepository<IUserRoleRepository>();
                IClosedPeriodRepository closedPeriodRepository = _DataRepositoryFactory.GetDataRepository<IClosedPeriodRepository>();


                var solutionIds = userRoleRepository.GetUserRoleInfo().Where(c => c.UserSetup.LoginID == loginID).Select(c => c.Solution.SolutionId).Distinct();

                var solutions = GetSolutions();
                var solutionId2s = solutions.Where(c => c.Active).Select(c => c.EntityId).Distinct();

                List<ClosedPeriodData> closedPeriods = new List<ClosedPeriodData>();

                IEnumerable<ClosedPeriodInfo> closedPeriodInfos = closedPeriodRepository.GetClosedPeriods().Where(c => solutionIds.Contains(c.ClosedPeriod.SolutionId) && solutionId2s.Contains(c.ClosedPeriod.SolutionId)).OrderByDescending(c => c.ClosedPeriod.Status).ThenByDescending(c => c.ClosedPeriod.Date).ToArray();
                //IEnumerable<IFRSRegistryInfo> ifrsRegistryInfos = ifrsRegistryRepository.GetIFRSRegistrys(flag).OrderBy(c => c.IFRSRegistry.FinType).ThenBy(c => c.IFRSRegistry.Position).ToArray();

                foreach (var closedPeriodInfo in closedPeriodInfos)
                {
                    closedPeriods.Add(
                        new ClosedPeriodData
                        {
                            ClosedPeriodId = closedPeriodInfo.ClosedPeriod.EntityId,
                            Date = closedPeriodInfo.ClosedPeriod.Date,
                            Month = closedPeriodInfo.ClosedPeriod.Date.Month,
                            Year = closedPeriodInfo.ClosedPeriod.Date.Year,
                            SolutionId = closedPeriodInfo.ClosedPeriod.SolutionId,
                            SolutionName = GetSolutionName(solutions, closedPeriodInfo.ClosedPeriod.SolutionId),
                            Status = closedPeriodInfo.ClosedPeriod.Status,
                            Active = closedPeriodInfo.ClosedPeriod.Active,
                            Deleted = closedPeriodInfo.ClosedPeriod.Deleted 
                        });
                }

                return closedPeriods.ToArray();
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public ClosedPeriod ClosePeriod(ClosedPeriod closedPeriod)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IClosedPeriodRepository closedPeriodRepository = _DataRepositoryFactory.GetDataRepository<IClosedPeriodRepository>();
                IClosedPeriodTemplateRepository closedPeriodTemplateRepository = _DataRepositoryFactory.GetDataRepository<IClosedPeriodTemplateRepository>();

                var template = closedPeriodTemplateRepository.Get().Where(c => c.SolutionId == closedPeriod.SolutionId).FirstOrDefault();

                ClosedPeriod updatedEntity = null;

                using (TransactionScope ts = new TransactionScope())
                {
                   // updatedEntity = closedPeriodRepository.Update(closedPeriod);

                    //call closed period action
                    List<SqlParameter> parameters = new List<SqlParameter>();

                    parameters.Add(new SqlParameter
                    {
                        ParameterName = "RunDate",
                        Value = closedPeriod.Date
                    });

                    SqlDataManager.RunProcedure(GetDataConnection(), template.Action, parameters.ToArray());

                    ts.Complete();
                }

                return updatedEntity;
            });
        }

        #endregion

        #region ClosedPeriodTemplate operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ClosedPeriodTemplate UpdateClosedPeriodTemplate(ClosedPeriodTemplate closedPeriodTemplate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IClosedPeriodTemplateRepository closedPeriodTemplateRepository = _DataRepositoryFactory.GetDataRepository<IClosedPeriodTemplateRepository>();

                ClosedPeriodTemplate updatedEntity = null;

                if (closedPeriodTemplate.ClosedPeriodTemplateId == 0)
                    updatedEntity = closedPeriodTemplateRepository.Add(closedPeriodTemplate);
                else
                    updatedEntity = closedPeriodTemplateRepository.Update(closedPeriodTemplate);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteClosedPeriodTemplate(int closedPeriodTemplateId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IClosedPeriodTemplateRepository closedPeriodTemplateRepository = _DataRepositoryFactory.GetDataRepository<IClosedPeriodTemplateRepository>();

                closedPeriodTemplateRepository.Remove(closedPeriodTemplateId);
            });
        }

        public ClosedPeriodTemplate GetClosedPeriodTemplate(int closedPeriodTemplateId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IClosedPeriodTemplateRepository closedPeriodTemplateRepository = _DataRepositoryFactory.GetDataRepository<IClosedPeriodTemplateRepository>();

                ClosedPeriodTemplate closedPeriodTemplateEntity = closedPeriodTemplateRepository.Get(closedPeriodTemplateId);
                if (closedPeriodTemplateEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ClosedPeriodTemplate with ID of {0} is not in database", closedPeriodTemplateId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return closedPeriodTemplateEntity;
            });
        }

        public ClosedPeriodTemplate[] GetAllClosedPeriodTemplates()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IClosedPeriodTemplateRepository closedPeriodTemplateRepository = _DataRepositoryFactory.GetDataRepository<IClosedPeriodTemplateRepository>();

                IEnumerable<ClosedPeriodTemplate> closedPeriodTemplates = closedPeriodTemplateRepository.Get().ToArray();

                return closedPeriodTemplates.ToArray();
            });
        }

        public ClosedPeriodTemplateData[] GetClosedPeriodTemplates()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IClosedPeriodTemplateRepository closedPeriodTemplateRepository = _DataRepositoryFactory.GetDataRepository<IClosedPeriodTemplateRepository>();

                var solutions = GetSolutions();
                var solutionIds = solutions.Where(c => c.Active).Select(c => c.EntityId).Distinct();

                List<ClosedPeriodTemplateData> closedPeriodTemplates = new List<ClosedPeriodTemplateData>();
                IEnumerable<ClosedPeriodTemplateInfo> closedPeriodTemplateInfos = closedPeriodTemplateRepository.GetClosedPeriodTemplates().Where(c => solutionIds.Contains(c.ClosedPeriodTemplate.SolutionId)).ToArray();

                foreach (var closedPeriodTemplateInfo in closedPeriodTemplateInfos)
                {
                    closedPeriodTemplates.Add(
                        new ClosedPeriodTemplateData
                        {
                            ClosedPeriodTemplateId = closedPeriodTemplateInfo.ClosedPeriodTemplate.EntityId,
                            Action = closedPeriodTemplateInfo.ClosedPeriodTemplate.Action,
                            SolutionId = closedPeriodTemplateInfo.ClosedPeriodTemplate.SolutionId,
                            SolutionName = GetSolutionName(solutions, closedPeriodTemplateInfo.ClosedPeriodTemplate.SolutionId),
                            Active = closedPeriodTemplateInfo.ClosedPeriodTemplate.Active
                        });
                }

                return closedPeriodTemplates.ToArray();
            });
        }

        public ClosedPeriodTemplateData[] GetClosedPeriodTemplateByLogin(string loginID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUserRoleRepository userRoleRepository = _DataRepositoryFactory.GetDataRepository<IUserRoleRepository>();
                IClosedPeriodTemplateRepository closedPeriodTemplateRepository = _DataRepositoryFactory.GetDataRepository<IClosedPeriodTemplateRepository>();

                var solutionIds = userRoleRepository.GetUserRoleInfo().Where(c => c.UserSetup.LoginID == loginID).Select(c => c.Solution.SolutionId).Distinct();


                List<ClosedPeriodTemplateData> closedPeriodTemplates = new List<ClosedPeriodTemplateData>();

                var solutions = GetSolutions();
                var solutionId2s = solutions.Where(c => c.Active).Select(c => c.EntityId).Distinct();

                IEnumerable<ClosedPeriodTemplateInfo> closedPeriodTemplateInfos = closedPeriodTemplateRepository.GetClosedPeriodTemplates().Where(c => solutionIds.Contains(c.ClosedPeriodTemplate.SolutionId) && solutionId2s.Contains(c.ClosedPeriodTemplate.SolutionId)).ToArray();

                foreach (var closedPeriodTemplateInfo in closedPeriodTemplateInfos)
                {
                    closedPeriodTemplates.Add(
                        new ClosedPeriodTemplateData
                        {
                            ClosedPeriodTemplateId = closedPeriodTemplateInfo.ClosedPeriodTemplate.EntityId,
                            Action = closedPeriodTemplateInfo.ClosedPeriodTemplate.Action,
                            SolutionId = closedPeriodTemplateInfo.ClosedPeriodTemplate.SolutionId,
                            SolutionName = GetSolutionName(solutions, closedPeriodTemplateInfo.ClosedPeriodTemplate.SolutionId),
                            Active = closedPeriodTemplateInfo.ClosedPeriodTemplate.Active
                        });
                }

                return closedPeriodTemplates.ToArray();
            });
        }

        #endregion

        #region ExtractionJob operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ExtractionJob UpdateExtractionJob(ExtractionJob extractionJob)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionJobRepository extractionJobRepository = _DataRepositoryFactory.GetDataRepository<IExtractionJobRepository>();

                ExtractionJob updatedEntity = null;

                if (extractionJob.ExtractionJobId == 0)
                {
                    extractionJob.Code = UniqueKeyGenerator.RNGCharacterMask(6, 8);
                    extractionJob.Status = PackageStatus.New;
                    extractionJob.Active = true;
                    updatedEntity = extractionJobRepository.Add(extractionJob);
                }
                else
                    updatedEntity = extractionJobRepository.Update(extractionJob);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteExtractionJob(int extractionJobId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionJobRepository extractionJobRepository = _DataRepositoryFactory.GetDataRepository<IExtractionJobRepository>();

                extractionJobRepository.Remove(extractionJobId);
            });
        }

        public ExtractionJob GetExtractionJob(int extractionJobId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionJobRepository extractionJobRepository = _DataRepositoryFactory.GetDataRepository<IExtractionJobRepository>();

                ExtractionJob extractionJobEntity = extractionJobRepository.Get(extractionJobId);
                if (extractionJobEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ExtractionJob with ID of {0} is not in database", extractionJobId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return extractionJobEntity;
            });
        }

        public ExtractionJob[] GetCurrentExtractionJobs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionJobRepository extractionJobRepository = _DataRepositoryFactory.GetDataRepository<IExtractionJobRepository>();

                IEnumerable<ExtractionJob> extractionJobs = extractionJobRepository.Get().Where(c => c.Status == PackageStatus.New || c.Status == PackageStatus.Pending || c.Status == PackageStatus.Running).ToArray();

                return extractionJobs.ToArray();
            });
        }

        public ExtractionJob[] GetExtractionJobByDate(DateTime startDate, DateTime endDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExtractionJobRepository extractionJobRepository = _DataRepositoryFactory.GetDataRepository<IExtractionJobRepository>();

                IEnumerable<ExtractionJob> extractionJobs = extractionJobRepository.Get().Where(c => c.StartDate >= startDate && c.EndDate <= endDate).OrderByDescending(c => c.ExtractionJobId).ToArray();

                //IEnumerable<ExtractionJob> extractionJobs = extractionJobRepository.Get().Where(c => (c.StartDate >= startDate && c.EndDate <= endDate) && (c.Status == PackageStatus.New || c.Status == PackageStatus.Pending || c.Status == PackageStatus.Running || c.Status == PackageStatus.Done || c.Status == PackageStatus.Fail || c.Status == PackageStatus.Stop || c.Status == PackageStatus.Cancel)).ToArray();

                return extractionJobs.ToArray();
            });
        }

        [OperationBehavior(TransactionScopeRequired = false)]
        public ExtractionJob[] RunExtractionJob(int jobId, int[] extractionIds, DateTime startDate, DateTime endDate, DateTime runTime)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                foreach (var id in extractionIds)
                {
                    var trigger = new ExtractionTrigger()
                    {
                        ExtractionJobId = jobId,
                        ExtractionId = id,
                        Code = UniqueKeyGenerator.RNGCharacterMask(6, 8),
                        Status = PackageStatus.New,
                        StartDate = startDate,
                        EndDate = endDate,
                        RunTime = runTime,
                        Remark = "Not Started",
                        UserName = _LoginName,
                        Active = true,
                        Deleted = false,
                        CreatedBy = _LoginName,
                        CreatedOn = DateTime.Now,
                        UpdatedBy = _LoginName,
                        UpdatedOn = DateTime.Now
                    };

                    UpdateExtractionTrigger(trigger);
                }

                return GetExtractionJobByDate(startDate, endDate);
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public ExtractionJob[] CancelExtractionJobByCode(string jobCode, DateTime startDate, DateTime endDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IExtractionJobRepository extractionJobRepository = _DataRepositoryFactory.GetDataRepository<IExtractionJobRepository>();
                IExtractionTriggerRepository extractionTriggerJobRepository = _DataRepositoryFactory.GetDataRepository<IExtractionTriggerRepository>();

                ExtractionJob initiators = extractionJobRepository.Get().Where(c => c.Code == jobCode).FirstOrDefault();
                string initiatorName = initiators.UserName;

                if (initiatorName != _LoginName)
                {
                    NotFoundException ex = new NotFoundException(string.Format("You can NOT cancel Extraction not initiated by You"));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                var jobs = extractionJobRepository.Get().Where(c => c.UserName == _LoginName && c.Code == jobCode);

                using (TransactionScope ts = new TransactionScope())
                {
                    foreach (var job in jobs)
                    {
                        if (job.Status != PackageStatus.Done || job.Status != PackageStatus.Cancel || job.Status == PackageStatus.Fail)
                        {
                            //var dbJob = extractionJobRepository.Get(job.ExtractionJobId);
                            job.Status = PackageStatus.Cancel;
                            job.Remark = "Extraction has been cancel.";
                            UpdateExtractionJob(job);

                            var triggers = extractionTriggerJobRepository.GetExtractionTriggerByJob(job.Code);
                            foreach (var trigger in triggers)
                            {
                                if (trigger.ExtractionTrigger.Status != PackageStatus.New || trigger.ExtractionTrigger.Status != PackageStatus.Pending || trigger.ExtractionTrigger.Status != PackageStatus.Running)
                                {
                                    trigger.ExtractionTrigger.Status = PackageStatus.Cancel;
                                    trigger.ExtractionTrigger.Remark = "Extraction has been cancel.";
                                    UpdateExtractionTrigger(trigger.ExtractionTrigger);
                                }
                            }
                        }
                    }

                    ts.Complete();
                }

                return GetExtractionJobByDate(startDate, endDate);
            });
        }

        public void ClearExtractionHistory(int solutionId)
        {
            //ExecuteFaultHandledOperation(() =>
            //{
            //    var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
            //    AllowAccessToOperation(SOLUTION_NAME, groupNames);

            //    IExtractionJobRepository extractionJobRepository = _DataRepositoryFactory.GetDataRepository<IExtractionJobRepository>();

            //    extractionJobRepository.ClearExtractionHistory(solutionId);
            //});
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                try
                {
                    var connectionString = GetDataConnection();

                    using (var con = new SqlConnection(connectionString))
                    {
                        var cmd = new SqlCommand("spp_ifrs_getextractionservice_2delete", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add(new SqlParameter
                        {
                            ParameterName = "solutionId",
                            Value = solutionId,
                        });  

                        con.Open();
                        var myRefNo = new ReferenceNoModel();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {

                            if (reader["Code"] != DBNull.Value)
                                myRefNo.RefNo = reader["Code"].ToString();
                            ForceServiceDelete(myRefNo.RefNo, "spp_clear_extraction_history");
                        }
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        #endregion

        #region ProcessJob operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ProcessJob UpdateProcessJob(ProcessJob processJob)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessJobRepository processJobRepository = _DataRepositoryFactory.GetDataRepository<IProcessJobRepository>();

                ProcessJob updatedEntity = null;

                if (processJob.ProcessJobId == 0)
                {

                    updatedEntity = processJobRepository.Add(processJob);
                }
                else
                    updatedEntity = processJobRepository.Update(processJob);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteProcessJob(int processJobId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessJobRepository processJobRepository = _DataRepositoryFactory.GetDataRepository<IProcessJobRepository>();

                processJobRepository.Remove(processJobId);
            });
        }

        public ProcessJob GetProcessJob(int processJobId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessJobRepository processJobRepository = _DataRepositoryFactory.GetDataRepository<IProcessJobRepository>();

                ProcessJob processJobEntity = processJobRepository.Get(processJobId);
                if (processJobEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ProcessJob with ID of {0} is not in database", processJobId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return processJobEntity;
            });
        }

        public ProcessJob[] GetCurrentProcessJobs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessJobRepository processJobRepository = _DataRepositoryFactory.GetDataRepository<IProcessJobRepository>();

                IEnumerable<ProcessJob> processJobs = processJobRepository.Get().Where(c => c.Status == PackageStatus.New || c.Status == PackageStatus.Pending || c.Status == PackageStatus.Running).ToArray();

                return processJobs.ToArray();
            });
        }

        public ProcessJob[] GetProcessJobByRunDate()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProcessJobRepository processJobRepository = _DataRepositoryFactory.GetDataRepository<IProcessJobRepository>();

                //IEnumerable<ProcessJob> processJobs = processJobRepository.Get().Where(c => c.Status == PackageStatus.New || c.Status == PackageStatus.Pending || c.Status == PackageStatus.Running ).ToArray();

                IEnumerable<ProcessJob> processJobs = processJobRepository.Get().ToArray().OrderByDescending(c => c.ProcessJobId).ToArray();
                return processJobs.ToArray();
            });
        }

        [OperationBehavior(TransactionScopeRequired = false)]
        public ProcessJob[] RunProcessJob(int jobId, int[] processIds, DateTime runTime)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IProcessRepository processRepository = _DataRepositoryFactory.GetDataRepository<IProcessRepository>();
                IModuleRepository moduleRepository = _DataRepositoryFactory.GetDataRepository<IModuleRepository>();
                ISolutionRunDateRepository runDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();

                //var job = GetProcessJob(jobId);

                foreach (var id in processIds)
                {
                    //if (id == 11)
                    //{
                    //    HOB = "Selected";
                    //}
                    var process = processRepository.Get(id);
                    var module = moduleRepository.Get(process.ModuleId);
                    var runDate = runDateRepository.Get().FirstOrDefault();
                    //.Where(c => c.SolutionId == 2)
                    var trigger = new ProcessTrigger()
                    {
                        ProcessJobId = jobId,
                        ProcessId = id,
                        Code = UniqueKeyGenerator.RNGCharacterMask(6, 8),
                        Status = PackageStatus.New,
                        StartDate = runDate.RunDate.FirstOfMonth(),
                        EndDate = runDate.RunDate,
                        RunTime = runTime,
                        Remark = "Not Started",
                        UserName = _LoginName,
                        Active = true,
                        Deleted = false,
                        CreatedBy = _LoginName,
                        CreatedOn = DateTime.Now,
                        UpdatedBy = _LoginName,
                        UpdatedOn = DateTime.Now
                    };

                    UpdateProcessTrigger(trigger);
                }

                return GetCurrentProcessJobs();
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public ProcessJob[] CancelProcessJobByCode(string jobCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IProcessJobRepository processJobRepository = _DataRepositoryFactory.GetDataRepository<IProcessJobRepository>();
                IProcessTriggerRepository processTriggerJobRepository = _DataRepositoryFactory.GetDataRepository<IProcessTriggerRepository>();
                ProcessJob initiators = processJobRepository.Get().Where(c => c.Code == jobCode).FirstOrDefault();
                string initiatorName = initiators.UserName;

                if (initiatorName != _LoginName)
                {
                    NotFoundException ex = new NotFoundException(string.Format("You can NOT cancel Process not initiated by You"));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                var jobs = processJobRepository.Get().Where(c => c.UserName == _LoginName && c.Code == jobCode);
                using (TransactionScope ts = new TransactionScope())
                {
                    foreach (var job in jobs)
                    {
                        if (job.Status != PackageStatus.Done || job.Status != PackageStatus.Cancel || job.Status == PackageStatus.Fail)
                        {
                            //var dbJob = processJobRepository.Get(job.ProcessJobId);
                            job.Status = PackageStatus.Cancel;
                            job.Remark = "Process has been cancel.";
                            UpdateProcessJob(job);

                            var triggers = processTriggerJobRepository.GetProcessTriggerByJob(job.Code);
                            foreach (var trigger in triggers)
                            {
                                if (trigger.ProcessTrigger.Status != PackageStatus.New || trigger.ProcessTrigger.Status != PackageStatus.Pending || trigger.ProcessTrigger.Status != PackageStatus.Running)
                                {
                                    trigger.ProcessTrigger.Status = PackageStatus.Cancel;
                                    trigger.ProcessTrigger.Remark = "Process has been cancel.";
                                    UpdateProcessTrigger(trigger.ProcessTrigger);
                                }
                            }
                        }
                    }

                    ts.Complete();
                }

                return GetCurrentProcessJobs();
            });
        }
        [OperationBehavior(TransactionScopeRequired = true)]

        public void RestartService(string serviceName)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
               
                try
                {
                    string serviceServerName = GetServiceServerName();
                    ServiceController sc = new ServiceController(serviceName, serviceServerName);

                    if (sc.Status.Equals(ServiceControllerStatus.Stopped) || sc.Status.Equals(ServiceControllerStatus.StopPending) || sc.Status.Equals(ServiceControllerStatus.Paused))
                    {
                        sc.Start();
                    }
                    sc.Refresh();

                }

                catch (Exception ex)
                {
                    throw ex;
                }

            });
        }

        public string GetServiceStatus(string serviceName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                try
                {
                    string serviceServerName = GetServiceServerName();
                    var status2 = DoesServiceExist(serviceName, serviceServerName);
                     if (status2 == true)
                  { 
                    ServiceController sc = new ServiceController(serviceName, serviceServerName);
                    //  ServiceController sc = new ServiceController("MSSQL$SQLEXPRESS", "MISSERVER");
                    switch (sc.Status)
                    {
                        case ServiceControllerStatus.Running:
                            return "Running";
                        case ServiceControllerStatus.Stopped:
                            return "Stopped";
                        case ServiceControllerStatus.Paused:
                            return "Paused";
                        case ServiceControllerStatus.StopPending:
                            return "Stopping";
                        case ServiceControllerStatus.StartPending:
                            return "Starting";
                        default:
                            return "Status Changing";
                    }
                  }
                     else
                     {
                            return "Service Not Yet Installed";
                     }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public void ClearProcessHistory(int solutionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                try
                {
                    var connectionString = GetDataConnection();

                    using (var con = new SqlConnection(connectionString))
                    {
                        var cmd = new SqlCommand("spp_ifrs_getprocessservice_2delete", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add(new SqlParameter
                        {
                            ParameterName = "solutionId",
                            Value = solutionId,
                        });                      

                        con.Open();
                        var myRefNo = new ReferenceNoModel();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {

                            if (reader["Code"] != DBNull.Value)
                                myRefNo.RefNo = reader["Code"].ToString();
                            ForceServiceDelete(myRefNo.RefNo, "spp_clear_process_history");
                        }
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public void ForceServiceDelete(string serviceName,string sppName)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                try
                {
                    string serviceServerName = GetServiceServerName();
                   
                   var status = DoesServiceExist(serviceName, serviceServerName);
                     if (status == true)
                     {
                         ServiceController sc = new ServiceController(serviceName, serviceServerName);
                         if (sc.Status.Equals(ServiceControllerStatus.Running) || sc.Status.Equals(ServiceControllerStatus.StopPending) || sc.Status.Equals(ServiceControllerStatus.Paused))
                         {
                             sc.Stop();
                             sc.WaitForStatus(ServiceControllerStatus.Stopped);

                         }
                         sc.Refresh();
                         ServiceInstaller serviceInstallerObj = new ServiceInstaller();
                         InstallContext context = new InstallContext("<<log file path>>", null);
                         serviceInstallerObj.Context = context;
                         serviceInstallerObj.ServiceName = serviceName;
                         serviceInstallerObj.Uninstall(null);
                         
                     }
                     RemovefromDbase(serviceName, sppName);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }
   
        bool DoesServiceExist(string serviceName, string serviceServerName)
        {
            ServiceController[] services = ServiceController.GetServices(serviceServerName);
            var service = services.FirstOrDefault(s => s.ServiceName == serviceName);
            return service != null;
        }

        public void RemovefromDbase(string serviceName,string sppName)
        {

            var connectionString = GetDataConnection();

            int status = 0;

            using (var con = new SqlConnection(connectionString))
            {
               
                var cmd = new SqlCommand(sppName, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ServiceCode",
                    Value = serviceName,
                });               
                con.Open();
                status = cmd.ExecuteNonQuery();
                con.Close();
            }


        }

      
        #endregion

        #region Upload operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Upload UpdateUpload(Upload upload)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUploadRepository uploadRepository = _DataRepositoryFactory.GetDataRepository<IUploadRepository>();

                Upload updatedEntity = null;

                if (upload.UploadId == 0)
                    updatedEntity = uploadRepository.Add(upload);
                else
                    updatedEntity = uploadRepository.Update(upload);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteUpload(int uploadId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUploadRepository uploadRepository = _DataRepositoryFactory.GetDataRepository<IUploadRepository>();

                uploadRepository.Remove(uploadId);
            });
        }

        public Upload GetUpload(int uploadId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUploadRepository uploadRepository = _DataRepositoryFactory.GetDataRepository<IUploadRepository>();

                Upload uploadEntity = uploadRepository.Get(uploadId);
                if (uploadEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Upload with ID of {0} is not in database", uploadId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return uploadEntity;
            });
        }
        public Upload[] GetUploadBySolution(int solutionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUploadRepository uploadRepository = _DataRepositoryFactory.GetDataRepository<IUploadRepository>();

                IEnumerable<Upload> uploads = uploadRepository.Get().Where(c => c.SolutionId == solutionId).OrderBy(c => c.Title).ToArray();

                return uploads.ToArray();
            });
        }

        public Upload[] GetAllUploads()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUploadRepository uploadRepository = _DataRepositoryFactory.GetDataRepository<IUploadRepository>();

                IEnumerable<Upload> uploads = uploadRepository.Get().OrderBy(c => c.Position).ToArray();

                return uploads.ToArray();
            });
        }

        public UploadData[] GetUploads()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUploadRepository uploadRepository = _DataRepositoryFactory.GetDataRepository<IUploadRepository>();

                var solutions = GetSolutions();
                var solutionIds = solutions.Where(c => c.Active).Select(c => c.EntityId).Distinct();

                List<UploadData> uploads = new List<UploadData>();
                IEnumerable<UploadInfo> uploadInfos = uploadRepository.GetUploads().Where(c => solutionIds.Contains(c.Upload.SolutionId) && c.Upload.Active == true).OrderBy(c => c.Upload.Position).ToArray();

                foreach (var uploadInfo in uploadInfos)
                {
                    uploads.Add(
                        new UploadData
                        {
                            UploadId = uploadInfo.Upload.EntityId,
                            Title = uploadInfo.Upload.Title,
                            Code = uploadInfo.Upload.Code,
                            SolutionId = uploadInfo.Upload.SolutionId,
                            SolutionName = GetSolutionName(solutions, uploadInfo.Upload.SolutionId),
                            Action = uploadInfo.Upload.Action,
                            TruncateAction = uploadInfo.Upload.TruncateAction,
                            PostUploadAction = uploadInfo.Upload.PostUploadAction,
                            Verification = uploadInfo.Upload.Verification,
                            Position = uploadInfo.Upload.Position,
                            Template = uploadInfo.Upload.Template,
                            Active = uploadInfo.Upload.Active
                        });
                }

                return uploads.ToArray();
            });
        }

        public UploadResult[] UploadCSV(int uploadId, string csvText) //, bool truncate, bool postUploadAction
        {
            IUploadRepository uploadRepository = _DataRepositoryFactory.GetDataRepository<IUploadRepository>();
            List<UploadResult> results = new List<UploadResult>();

            TextReader reader = new StringReader(csvText);
            var table = new DataTable();
            using (var it = reader.ReadCsvWithHeader().GetEnumerator())
            {

                if (!it.MoveNext()) return null;

                foreach (var k in it.Current.Keys)
                    table.Columns.Add(k);

                do
                {
                    var row = table.NewRow();
                    foreach (var k in it.Current.Keys)
                        row[k] = it.Current[k];

                    table.Rows.Add(row);

                } while (it.MoveNext());
            }

            var upload = uploadRepository.Get(uploadId);
            var continueUpload = true;
            var bulkUpload = false;
            try
            {

                var truncate = false;
                if (string.IsNullOrEmpty(upload.TruncateAction))
                {
                    truncate = false;
                }
                else
                {
                    SqlDataManager.RunProcedure(GetDataConnection(), upload.TruncateAction, new List<SqlParameter>().ToArray());
                }

            }
            catch (Exception ex)
            {
                results.Add(new UploadResult()
                {
                    UploadId = uploadId,
                    UploadName = upload.Title,
                    Message = "Error encounter while trying to truncate table " + ". " + ex.Message,
                    Position = 0,
                    Pass = false
                });

                continueUpload = false;
            }

            if (continueUpload)
            {
                var rowCount = 0;
                var successCount = 0;
                var columnCount = 0;
                bulkUpload = upload.BulkUpload;

                //New Block of Code
               // List<SqlParameter> parameters = new List<SqlParameter>();
                rowCount += 1;
                columnCount = 0;
               
                if (bulkUpload==true)
                {
                    var connectionString = GetDataConnection();

                    using (var con = new SqlConnection(connectionString))
                    {
                        var cmd = new SqlCommand(upload.Action, con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TableVar", table);
                        cmd.Parameters.AddWithValue("@UserName", _LoginName);
                        cmd.CommandTimeout = 0;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        successCount = table.Rows.Count;
                        con.Close();
                    }
                }
                else
                {
                    //Upload Data
                    foreach (DataRow row in table.Rows)
                    {
                        List<SqlParameter> parameters = new List<SqlParameter>();
                        rowCount += 1;
                        columnCount = 0;

                        foreach (DataColumn column in table.Columns)
                        {
                            columnCount += 1;

                            parameters.Add(new SqlParameter
                            {
                                ParameterName = column.ColumnName,
                                Value = row[column.ColumnName]
                            });
                        }

                        parameters.Add(new SqlParameter
                        {
                            ParameterName = "UserName",
                            Value = _LoginName
                        });

                        try
                        {

                            uploadRepository.UploadData(upload.Action, parameters.ToArray());
                            successCount += 1;
                        }
                        catch (Exception ex)
                        {
                            results.Add(new UploadResult()
                            {
                                UploadId = uploadId,
                                UploadName = upload.Title,
                                Message = "Error encounter at position: " + rowCount.ToString() + ". " + ex.Message,
                                Position = rowCount,
                                Pass = false
                            });
                        }
                    }
                }
                 if (upload.Title.Contains("Adjustment"))
                {
                    _additionalInfo = AdjustmentInfo();
                }

                results.Add(new UploadResult()
                {
                    UploadId = uploadId,
                    UploadName = upload.Title,
                    Message = "Total number of record successfully uploaded is " + successCount.ToString(),
                    AdditionalInfo = _additionalInfo,
                    Position = rowCount,
                    Pass = true
                });

                var postAction = false;
                if (string.IsNullOrEmpty(upload.PostUploadAction))
                {
                    postAction = false;
                }

                else
                {
                    SqlDataManager.RunProcedure(GetDataConnection(), upload.PostUploadAction, new List<SqlParameter>().ToArray());
                }
            }


            //
            return results.ToArray();
        }

        private void SqlCommand(string p)
        {
            throw new NotImplementedException();
        }

        public UploadResult[] UploadCSVByCode(string uploadCode, string csvText)
        {
            IUploadRepository uploadRepository = _DataRepositoryFactory.GetDataRepository<IUploadRepository>();
            List<UploadResult> results = new List<UploadResult>();

            TextReader reader = new StringReader(csvText);
            var table = new DataTable();
            using (var it = reader.ReadCsvWithHeader().GetEnumerator())
            {

                if (!it.MoveNext()) return null;

                foreach (var k in it.Current.Keys)
                    table.Columns.Add(k);

                do
                {
                    var row = table.NewRow();
                    foreach (var k in it.Current.Keys)
                        row[k] = it.Current[k];

                    table.Rows.Add(row);

                } while (it.MoveNext());
            }

            var upload = uploadRepository.Get().Where(c => c.Code == uploadCode).FirstOrDefault();
            var rowCount = 0;
            var successCount = 0;
            var columnCount = 0;

            //Upload Data
            foreach (DataRow row in table.Rows)
            {
                //string actionName = string.Empty;
                List<SqlParameter> parameters = new List<SqlParameter>();

                rowCount += 1;
                columnCount = 0;

                //actionName = "exec " + upload.Action + " ";
                foreach (DataColumn column in table.Columns)
                {
                    columnCount += 1;

                    //if (columnCount == 1)
                    //    actionName += "@" + column.ColumnName;
                    //else
                    //    actionName += ",@" + column.ColumnName;

                    parameters.Add(new SqlParameter
                    {
                        ParameterName = column.ColumnName,
                        Value = row[column.ColumnName]
                    });
                }

                parameters.Add(new SqlParameter
                {
                    ParameterName = "UserName",
                    Value = _LoginName
                });

                try
                {
                    uploadRepository.UploadData(upload.Action, parameters.ToArray());
                    successCount += 1;
                }
                catch (Exception ex)
                {
                    results.Add(new UploadResult()
                    {
                        UploadId = upload.UploadId,
                        UploadName = upload.Title,
                        Message = "Error encounter at position: " + rowCount.ToString() + ". " + ex.Message,
                        Position = rowCount,
                        Pass = false
                    });
                }
            }

            results.Add(new UploadResult()
            {
                UploadId = upload.UploadId,
                UploadName = upload.Title,
                Message = "Total number of record successfully uploaded is " + successCount.ToString(),
                Position = rowCount,
                Pass = true
            });

            //
            return results.ToArray();
        }

        public UploadResult[] VerificationMsg(string sppVerify)
        {

            var connectionString = GetDataConnection();
            string vMessage = string.Empty;

            List<UploadResult> results = new List<UploadResult>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand(sppVerify, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["vMesage"] != DBNull.Value)
                        vMessage = reader["vMesage"].ToString();
                }
                con.Close();
            }

            results.Add(new UploadResult()
            {
                UploadId = 0,
                UploadName = "",
                Message = vMessage.ToString(),
                Position = 0,
                Pass = true
            });

            return results.ToArray();
        }
        #endregion

        #region UploadRole operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public UploadRole UpdateUploadRole(UploadRole uploadRole)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUploadRoleRepository uploadRoleRepository = _DataRepositoryFactory.GetDataRepository<IUploadRoleRepository>();

                UploadRole updatedEntity = null;

                if (uploadRole.UploadRoleId == 0)
                    updatedEntity = uploadRoleRepository.Add(uploadRole);
                else
                    updatedEntity = uploadRoleRepository.Update(uploadRole);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteUploadRole(int uploadRoleId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUploadRoleRepository uploadRoleRepository = _DataRepositoryFactory.GetDataRepository<IUploadRoleRepository>();

                uploadRoleRepository.Remove(uploadRoleId);
            });
        }

        public UploadRole GetUploadRole(int uploadRoleId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUploadRoleRepository uploadRoleRepository = _DataRepositoryFactory.GetDataRepository<IUploadRoleRepository>();

                UploadRole uploadRoleEntity = uploadRoleRepository.Get(uploadRoleId);
                if (uploadRoleEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("UploadRole with ID of {0} is not in database", uploadRoleId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return uploadRoleEntity;
            });
        }

        public UploadRole[] GetAllUploadRoles()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUploadRoleRepository uploadRoleRepository = _DataRepositoryFactory.GetDataRepository<IUploadRoleRepository>();

                IEnumerable<UploadRole> uploadRoles = uploadRoleRepository.Get();

                return uploadRoles.ToArray();
            });
        }

        public UploadRoleData[] GetUploadRoles()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUploadRoleRepository uploadRoleRepository = _DataRepositoryFactory.GetDataRepository<IUploadRoleRepository>();

                List<UploadRoleData> uploadRoles = new List<UploadRoleData>();
                IEnumerable<UploadRoleInfo> uploadRoleInfos = uploadRoleRepository.GetUploadRoles().ToArray();

                var roles = GetRoles();
                var solutions = GetSolutions();

                foreach (var uploadRoleInfo in uploadRoleInfos)
                {
                    uploadRoles.Add(
                        new UploadRoleData
                        {
                            UploadRoleId = uploadRoleInfo.UploadRole.EntityId,
                            RoleId = uploadRoleInfo.UploadRole.RoleId,
                            RoleName = GetRoleName(roles, uploadRoleInfo.UploadRole.RoleId),
                            UploadId = uploadRoleInfo.Upload.EntityId,
                            UploadName = uploadRoleInfo.Upload.Title,
                            SolutionId = uploadRoleInfo.Upload.SolutionId,
                            SolutionName = GetSolutionName(solutions, uploadRoleInfo.Upload.SolutionId),
                            Active = uploadRoleInfo.UploadRole.Active
                        });
                }

                return uploadRoles.ToArray();
            });
        }

        public UploadRoleData[] GetUploadRoleByUpload(int uploadId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUploadRoleRepository uploadRoleRepository = _DataRepositoryFactory.GetDataRepository<IUploadRoleRepository>();

                var roles = GetRoles();
                var solutions = GetSolutions();

                List<UploadRoleData> uploadRoles = new List<UploadRoleData>();
                IEnumerable<UploadRoleInfo> uploadRoleInfos = uploadRoleRepository.GetUploadRoleByUpload(uploadId).ToArray();

                foreach (var uploadRoleInfo in uploadRoleInfos)
                {
                    uploadRoles.Add(
                        new UploadRoleData
                        {
                            UploadRoleId = uploadRoleInfo.UploadRole.EntityId,
                            RoleId = uploadRoleInfo.UploadRole.RoleId,
                            RoleName = GetRoleName(roles, uploadRoleInfo.UploadRole.RoleId),
                            UploadId = uploadRoleInfo.Upload.EntityId,
                            UploadName = uploadRoleInfo.Upload.Title,
                            SolutionId = uploadRoleInfo.Upload.SolutionId,
                            SolutionName = GetSolutionName(solutions, uploadRoleInfo.Upload.SolutionId),
                            Active = uploadRoleInfo.UploadRole.Active
                        });
                }

                return uploadRoles.ToArray();
            });
        }

        #endregion

        #region CheckDataAvailability operations

        public CheckDataAvailability[] GetAllDataAvailability()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICheckDataAvailabilityRepository datacheckRepository = _DataRepositoryFactory.GetDataRepository<ICheckDataAvailabilityRepository>();

                IEnumerable<CheckDataAvailability> datacheck = datacheckRepository.Get().ToArray();

                return datacheck.ToArray();
            });
        }

        public void CheckDataAvailabilitybyRunDate(DateTime runDate)
        {

            var connectionString = GetDataConnection();

            int status = 0;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("ifrs_spp_check_datavailability", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "RunDate",
                    Value = runDate,
                });


                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        #endregion

        #region CheckifrsDataAvailability operations

        public CheckifrsDataAvailability[] GetAllifrsDataAvailability()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_SUPER_BUSINESS, GROUP_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICheckifrsDataAvailabilityRepository datacheckRepository = _DataRepositoryFactory.GetDataRepository<ICheckifrsDataAvailabilityRepository>();

                IEnumerable<CheckifrsDataAvailability> datacheck = datacheckRepository.Get().ToArray();

                return datacheck.ToArray();
            });
        }

        public void CheckifrsDataAvailabilitybyRunDate(DateTime runDate)
        {

            var connectionString = GetDataConnection();

            int status = 0;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("ifrs_spp_check_ifrs_datavailability", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "RunDate",
                    Value = runDate,
                });


                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        #endregion

        #region AdjustmentIfo operations

        public string AdjustmentInfo()
        {

            var connectionString = GetDataConnection();
            string vMessage = string.Empty;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("ifrs_spp_adjustment_info", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["Diff"] != DBNull.Value)
                        vMessage = reader["Diff"].ToString();
                }
                con.Close();
            }


            return vMessage.ToString();

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

        protected Module[] GetModules()
        {
            IModuleRepository moduleRepository = _DataRepositoryFactory.GetDataRepository<IModuleRepository>();

            var modules = moduleRepository.Get();

            return modules.ToArray();
        }

        protected Module[] GetModules(int solutionId)
        {
            IModuleRepository moduleRepository = _DataRepositoryFactory.GetDataRepository<IModuleRepository>();

            var modules = moduleRepository.Get().Where(c => c.SolutionId == solutionId);

            return modules.ToArray();
        }

        protected string GetModuleName(Module[] modules, int moduleId)
        {
            foreach (var module in modules)
            {
                if (module.ModuleId == moduleId)
                    return module.Alias;
            }

            return string.Empty;
        }

        protected Solution[] GetSolutions()
        {
            ISolutionRepository solutionRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRepository>();

            var solutions = solutionRepository.Get();

            return solutions.ToArray();
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

        protected Role[] GetRoles()
        {
            IRoleRepository roleRepository = _DataRepositoryFactory.GetDataRepository<IRoleRepository>();

            var roles = roleRepository.Get();

            return roles.ToArray();
        }

        protected string GetRoleName(Role[] roles, int roleId)
        {
            foreach (var role in roles)
            {
                if (role.RoleId == roleId)
                    return role.Name;
            }

            return string.Empty;
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

        public string GetServiceServerName()
        {
            string serviceServerName = "";
            if (!string.IsNullOrEmpty(DataConnector.CompanyCode))
            {
                IDatabaseRepository databaseRepository = _DataRepositoryFactory.GetDataRepository<IDatabaseRepository>();
                var companydb = databaseRepository.Get().Where(c => c.CompanyCode == DataConnector.CompanyCode).FirstOrDefault();

                if (companydb == null)
                    throw new Exception("Unable to load company database.");

                serviceServerName = companydb.ServiceServerName;
            }
            return serviceServerName;

        }

        #endregion

    }
}
