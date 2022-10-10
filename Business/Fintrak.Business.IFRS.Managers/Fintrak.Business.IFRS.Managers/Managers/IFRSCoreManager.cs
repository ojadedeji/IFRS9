using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Transactions;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Business.IFRS.Contracts;
using Fintrak.Shared.Common;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Common.Utils;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Core.Framework;
using System.Configuration;
using System.Data.SqlClient;

using systemCoreFramework = Fintrak.Shared.SystemCore.Framework;
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.SystemCore.Entities;
using Fintrak.Shared.SystemCore.Framework;
using Fintrak.Shared.Common.Data;
using DecryptOperation;

namespace Fintrak.Business.IFRS.Managers
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


        [OperationBehavior(TransactionScopeRequired = true)]
        public override void RegisterModule()
        {
            ExecuteFaultHandledOperation(() =>
            {
                ISolutionRepository solutionRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRepository>();
                IModuleRepository moduleRepository = _DataRepositoryFactory.GetDataRepository<IModuleRepository>();
                IMenuRepository menuRepository = _DataRepositoryFactory.GetDataRepository<IMenuRepository>();
                IRoleRepository roleRepository = _DataRepositoryFactory.GetDataRepository<IRoleRepository>();
                IMenuRoleRepository menuRoleRepository = _DataRepositoryFactory.GetDataRepository<IMenuRoleRepository>();

                using (TransactionScope ts = new TransactionScope())
                {
                    //check if module has been installed
                    Module module = moduleRepository.Get().Where(c => c.Name == IFRSCoreModuleDefinition.MODULE_NAME).FirstOrDefault();

                    var register = false;
                    if (module == null)
                        register = true;
                    else
                        register = module.CanUpdate;

                    if (register)
                    {
                        //check if module category exit
                        Solution solution = solutionRepository.Get().Where(c => c.Name == IFRSCoreModuleDefinition.SOLUTION_NAME).FirstOrDefault();
                        if (solution == null)
                        {
                            //register solution
                            solution = new Solution()
                            {
                                Name = IFRSCoreModuleDefinition.SOLUTION_NAME,
                                Alias = IFRSCoreModuleDefinition.SOLUTION_ALIAS,
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
                            module = new Module()
                            {
                                Name = IFRSCoreModuleDefinition.MODULE_NAME,
                                Alias = IFRSCoreModuleDefinition.MODULE_ALIAS,
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

                        foreach (var role in IFRSCoreModuleDefinition.GetRoles())
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

                        foreach (var menu in IFRSCoreModuleDefinition.GetMenus())
                        {
                            menuIndex += 1;

                            int? parentId = null;
                            if (!string.IsNullOrEmpty(menu.Parent))
                            {
                                var parentMenu = existingMenus.Where(c => c.Name == menu.Parent).FirstOrDefault();

                                if (parentMenu == null)
                                    parentMenu = menuRepository.Get().Where(c => c.ModuleId == module.ModuleId && c.Name == menu.Parent).FirstOrDefault();

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

                        foreach (var menuRole in IFRSCoreModuleDefinition.GetMenuRoles())
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


        #region IFRSRegistry operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IFRSRegistry UpdateIFRSRegistry(IFRSRegistry ifrsRegistry)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { IFRSCoreModuleDefinition.GROUP_ADMINISTRATOR };
                AllowAccessToOperation(IFRSCoreModuleDefinition.SOLUTION_NAME, groupNames);

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
        public void DeleteIFRSRegistry(int ifrsRevenueId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { IFRSCoreModuleDefinition.GROUP_ADMINISTRATOR };
                AllowAccessToOperation(IFRSCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IIFRSRegistryRepository ifrsRegistryRepository = _DataRepositoryFactory.GetDataRepository<IIFRSRegistryRepository>();

                ifrsRegistryRepository.Remove(ifrsRevenueId);
            });
        }

        public IFRSRegistry GetIFRSRegistry(int ifrsRevenueId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { IFRSCoreModuleDefinition.GROUP_ADMINISTRATOR, IFRSCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(IFRSCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IIFRSRegistryRepository ifrsRegistryRepository = _DataRepositoryFactory.GetDataRepository<IIFRSRegistryRepository>();

                IFRSRegistry ifrsRegistryEntity = ifrsRegistryRepository.Get(ifrsRevenueId);
                if (ifrsRegistryEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IFRSRegistry with ID of {0} is not in database", ifrsRevenueId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifrsRegistryEntity;
            });
        }

        public IFRSRegistryData[] GetAllIFRSRegistries(int flag)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { IFRSCoreModuleDefinition.GROUP_ADMINISTRATOR, IFRSCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(IFRSCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IIFRSRegistryRepository ifrsRegistryRepository = _DataRepositoryFactory.GetDataRepository<IIFRSRegistryRepository>();


                List<IFRSRegistryData> ifrsRegistrys = new List<IFRSRegistryData>();
                IEnumerable<IFRSRegistryInfo> ifrsRegistryInfos = ifrsRegistryRepository.GetIFRSRegistrys(flag).OrderBy(c => c.IFRSRegistry.FinType).ThenBy(c => c.IFRSRegistry.Position).ToArray();

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
                            ParentName = ifrsRegistryInfo.Parent != null ? ifrsRegistryInfo.Parent.Caption : string.Empty,
                            IsTotalLine = ifrsRegistryInfo.IFRSRegistry.IsTotalLine,
                            Color = ifrsRegistryInfo.IFRSRegistry.Color,
                            Class = ifrsRegistryInfo.IFRSRegistry.Class,
                            Active = ifrsRegistryInfo.IFRSRegistry.Active,
                            Flag = ifrsRegistryInfo.IFRSRegistry.Flag,
                            UpdatedBy = ifrsRegistryInfo.IFRSRegistry.UpdatedBy
                        });
                }

                return ifrsRegistrys.ToArray();
            });
        }

        public string[] GetDistinctRefNotes()
        {
            //var connectionString = IFRSContext.GetDataConnection();
            var connectionString = GetDataConnection();

            List<string> refno;
            var refnoList = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("get_distinct_registry_refnote", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var refNote = new ReferenceNoModel();
                        if (reader["RefNo"] != DBNull.Value)
                            refNote.RefNo = reader["RefNo"].ToString();
                        refnoList.Add(refNote.RefNo);
                    }
                    reader.Close();
                    con.Close();
                }

                con.Close();
            }
            return refnoList.ToArray();
        }

        #endregion

        #region DerivedCaption operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public DerivedCaption UpdateDerivedCaption(DerivedCaption derivedCaption)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { IFRSCoreModuleDefinition.GROUP_ADMINISTRATOR };
                AllowAccessToOperation(IFRSCoreModuleDefinition.SOLUTION_NAME, groupNames);

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
                var groupNames = new List<string>() { IFRSCoreModuleDefinition.GROUP_ADMINISTRATOR };
                AllowAccessToOperation(IFRSCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IDerivedCaptionRepository derivedCaptionRepository = _DataRepositoryFactory.GetDataRepository<IDerivedCaptionRepository>();

                derivedCaptionRepository.Remove(derivedCaptionId);
            });
        }

        public DerivedCaption GetDerivedCaption(int derivedCaptionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { IFRSCoreModuleDefinition.GROUP_ADMINISTRATOR, IFRSCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(IFRSCoreModuleDefinition.SOLUTION_NAME, groupNames);

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
                var groupNames = new List<string>() { IFRSCoreModuleDefinition.GROUP_ADMINISTRATOR, IFRSCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(IFRSCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IDerivedCaptionRepository derivedCaptionRepository = _DataRepositoryFactory.GetDataRepository<IDerivedCaptionRepository>();

                IEnumerable<DerivedCaption> derivedCaptions = derivedCaptionRepository.Get().ToArray();

                return derivedCaptions.ToArray();
            });
        }


        #endregion

        #region QualitativeNote operations
        public void UpdateQualitativeNote(string refNote, string topNote, string bottomNote, DateTime runDate, int reportType)
        {

            var connectionString = GetDataConnection();

            int status = 0;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_update_qualitativenote", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "RefNote",
                    Value = refNote,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TopNote",
                    Value = topNote,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "BottomNote",
                    Value = bottomNote,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "RunDate",
                    Value = runDate,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ReportType",
                    Value = reportType,
                });

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteQualitativeNote(int qualitativeNoteId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { IFRSCoreModuleDefinition.GROUP_ADMINISTRATOR };
                AllowAccessToOperation(IFRSCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IQualitativeNoteRepository qualitativeNoteRepository = _DataRepositoryFactory.GetDataRepository<IQualitativeNoteRepository>();

                qualitativeNoteRepository.Remove(qualitativeNoteId);
            });
        }

        public QualitativeNote GetQualitativeNote(int qualitativeNoteId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { IFRSCoreModuleDefinition.GROUP_ADMINISTRATOR, IFRSCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(IFRSCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IQualitativeNoteRepository derivedCaptionRepository = _DataRepositoryFactory.GetDataRepository<IQualitativeNoteRepository>();

                QualitativeNote qualitativeNoteEntity = derivedCaptionRepository.Get(qualitativeNoteId);
                if (qualitativeNoteEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("QualitativeNote with ID of {0} is not in database", qualitativeNoteId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return qualitativeNoteEntity;
            });
        }

        public QualitativeNote[] GetAllQualitativeNotes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { IFRSCoreModuleDefinition.GROUP_ADMINISTRATOR, IFRSCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(IFRSCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IQualitativeNoteRepository qualitativeNoteRepository = _DataRepositoryFactory.GetDataRepository<IQualitativeNoteRepository>();

                IEnumerable<QualitativeNote> qualitativeNotes = qualitativeNoteRepository.Get().ToArray();

                return qualitativeNotes.ToArray();
            });
        }

        #endregion

        #region InterimQualitativeNote operations
        public void UpdateInterimQualitativeNote(string report, string topNote, string bottomNote, DateTime runDate, int ReportType)
        {

            var connectionString = GetDataConnection();
            string userName = _LoginName;
            int status = 0;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_update_interim_qualitativenote", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "report",
                    Value = report,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TopNote",
                    Value = topNote,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "BottomNote",
                    Value = bottomNote,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "RunDate",
                    Value = runDate,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ReportType",
                    Value = ReportType,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "userName",
                    Value = userName,
                });

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteInterimQualitativeNote(int qualitativeNoteId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { IFRSCoreModuleDefinition.GROUP_ADMINISTRATOR };
                AllowAccessToOperation(IFRSCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IInterimQualitativeNoteRepository qualitativeNoteRepository = _DataRepositoryFactory.GetDataRepository<IInterimQualitativeNoteRepository>();

                qualitativeNoteRepository.Remove(qualitativeNoteId);
            });
        }

        public InterimQualitativeNote GetInterimQualitativeNote(int qualitativeNoteId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { IFRSCoreModuleDefinition.GROUP_ADMINISTRATOR, IFRSCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(IFRSCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IInterimQualitativeNoteRepository derivedCaptionRepository = _DataRepositoryFactory.GetDataRepository<IInterimQualitativeNoteRepository>();

                InterimQualitativeNote qualitativeNoteEntity = derivedCaptionRepository.Get(qualitativeNoteId);
                if (qualitativeNoteEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("InterimQualitativeNote with ID of {0} is not in database", qualitativeNoteId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return qualitativeNoteEntity;
            });
        }

        public InterimQualitativeNote[] GetInterimQualitativeNoteByType(int rType)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { IFRSCoreModuleDefinition.GROUP_ADMINISTRATOR, IFRSCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(IFRSCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IInterimQualitativeNoteRepository derivedCaptionRepository = _DataRepositoryFactory.GetDataRepository<IInterimQualitativeNoteRepository>();

                InterimQualitativeNote[] qualitativeNoteEntity = derivedCaptionRepository.Get().Where(c => c.ReportType == rType).ToArray();
                if (qualitativeNoteEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("InterimQualitativeNote with ID of {0} is not in database", rType));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return qualitativeNoteEntity;
            });
        }

        public InterimQualitativeNote[] GetAllInterimQualitativeNotes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { IFRSCoreModuleDefinition.GROUP_ADMINISTRATOR, IFRSCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(IFRSCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IInterimQualitativeNoteRepository qualitativeNoteRepository = _DataRepositoryFactory.GetDataRepository<IInterimQualitativeNoteRepository>();

                IEnumerable<InterimQualitativeNote> qualitativeNotes = qualitativeNoteRepository.Get().ToArray();

                return qualitativeNotes.ToArray();
            });
        }

       
        public string[] GetReportNamesbyType(int rType)
        {
            //var connectionString = IFRSContext.GetDataConnection();
            var connectionString = GetDataConnection();

            // List<string> pTypes;
            var reportList = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_get_qualitative_reportname", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "rType",
                    Value = rType,
                });

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var reportname = new KeyValueModel();
                        if (reader["report"] != DBNull.Value)
                            reportname.Value = reader["report"].ToString();
                        reportList.Add(reportname.Value);
                    }
                    reader.Close();
                    con.Close();
                }

                con.Close();
            }
            return reportList.ToArray();
        }

        #endregion

        #region IFRSReportPackViewer operations


        public IFRSReport[] GetAllRunDates()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { IFRSCoreModuleDefinition.GROUP_ADMINISTRATOR, IFRSCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(IFRSCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IIFRSReportRepository reportRepository = _DataRepositoryFactory.GetDataRepository<IIFRSReportRepository>();
                IEnumerable<IFRSReport> ifrsreport = reportRepository.GetAllRunDate().ToArray();

                return ifrsreport.ToArray();
            });
        }

        public IFRSReportPack[] GetAllIFRSReportPacks()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { IFRSCoreModuleDefinition.GROUP_ADMINISTRATOR, IFRSCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(IFRSCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IIFRSReportPackRepository ifrsreportRepository = _DataRepositoryFactory.GetDataRepository<IIFRSReportPackRepository>();

                IEnumerable<IFRSReportPack> ifrsReportPack = ifrsreportRepository.Get().ToArray();

                return ifrsReportPack.ToArray();
            });
        }

        // 
        public string ReturnReportUrl(string reportName, DateTime runDate)
        {
            string reportUrl;
            var cCode = DataConnector.CompanyCode;
            string reportInitials = "http://pi360.fintrakonline.com/ReportServer_FINTRAKSQL/Pages/ReportViewer.aspx?%2fIFRS_PI360%2f";
            string secondinit = "&rs:Command=Render&CompanyCode=";
            string thirdinit = "&ReportDate=";
            string lastinit = "&BranchCode=All&rc:Parameters=False";

            reportUrl = string.Concat(reportInitials, reportName, secondinit, cCode, thirdinit, runDate, lastinit);

            return reportUrl;
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

        public string GetReportUrl()
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

        #region IFRSRevacctRegistry operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IFRSRevacctRegistry UpdateIFRSRevacctRegistry(IFRSRevacctRegistry IFRSRevacctRegistry)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { IFRSCoreModuleDefinition.GROUP_ADMINISTRATOR };
                AllowAccessToOperation(IFRSCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IIFRSRevacctRegistryRepository IFRSRevacctRegistryRepository = _DataRepositoryFactory.GetDataRepository<IIFRSRevacctRegistryRepository>();

                IFRSRevacctRegistry updatedEntity = null;

                if (IFRSRevacctRegistry.RevenueId == 0)
                    updatedEntity = IFRSRevacctRegistryRepository.Add(IFRSRevacctRegistry);
                else
                    updatedEntity = IFRSRevacctRegistryRepository.Update(IFRSRevacctRegistry);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIFRSRevacctRegistry(int IFRSRevacctRevenueId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { IFRSCoreModuleDefinition.GROUP_ADMINISTRATOR };
                AllowAccessToOperation(IFRSCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IIFRSRevacctRegistryRepository IFRSRevacctRegistryRepository = _DataRepositoryFactory.GetDataRepository<IIFRSRevacctRegistryRepository>();

                IFRSRevacctRegistryRepository.Remove(IFRSRevacctRevenueId);
            });
        }

        public IFRSRevacctRegistry GetIFRSRevacctRegistry(int IFRSRevacctRevenueId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { IFRSCoreModuleDefinition.GROUP_ADMINISTRATOR, IFRSCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(IFRSCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IIFRSRevacctRegistryRepository IFRSRevacctRegistryRepository = _DataRepositoryFactory.GetDataRepository<IIFRSRevacctRegistryRepository>();

                IFRSRevacctRegistry IFRSRevacctRegistryEntity = IFRSRevacctRegistryRepository.Get(IFRSRevacctRevenueId);
                if (IFRSRevacctRegistryEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IFRSRevacctRegistry with ID of {0} is not in database", IFRSRevacctRevenueId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return IFRSRevacctRegistryEntity;
            });
        }


        public IFRSRevacctRegistryData[] GetAllIFRSRevacctRegistries()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { IFRSCoreModuleDefinition.GROUP_ADMINISTRATOR, IFRSCoreModuleDefinition.GROUP_USER };
                AllowAccessToOperation(IFRSCoreModuleDefinition.SOLUTION_NAME, groupNames);

                IIFRSRevacctRegistryRepository ifrsRevacctRegistryRepository = _DataRepositoryFactory.GetDataRepository<IIFRSRevacctRegistryRepository>();


                List<IFRSRevacctRegistryData> ifrsRevacctRegistries = new List<IFRSRevacctRegistryData>();
                IEnumerable<IFRSRevacctRegistryInfo> ifrsRevacctRegistryInfos = ifrsRevacctRegistryRepository.GetIFRSRevacctRegistries().OrderBy(c => c.IFRSRevacctRegistry.Position).ToArray();

                foreach (var ifrsRevacctRegistryInfo in ifrsRevacctRegistryInfos)
                {
                    ifrsRevacctRegistries.Add(
                        new IFRSRevacctRegistryData
                        {
                            RevenueId = ifrsRevacctRegistryInfo.IFRSRevacctRegistry.EntityId,
                            Code = ifrsRevacctRegistryInfo.IFRSRevacctRegistry.Code,
                            Caption = ifrsRevacctRegistryInfo.IFRSRevacctRegistry.Caption,
                            Position = ifrsRevacctRegistryInfo.IFRSRevacctRegistry.Position,
                            RefNote = ifrsRevacctRegistryInfo.IFRSRevacctRegistry.RefNote,
                            FinType = ifrsRevacctRegistryInfo.IFRSRevacctRegistry.FinType,
                            FinSubType = ifrsRevacctRegistryInfo.IFRSRevacctRegistry.FinSubType,
                            ParentId = ifrsRevacctRegistryInfo.IFRSRevacctRegistry.ParentId,
                            ParentName = ifrsRevacctRegistryInfo.Parent != null ? ifrsRevacctRegistryInfo.Parent.Caption : string.Empty,
                            IsTotalLine = ifrsRevacctRegistryInfo.IFRSRevacctRegistry.IsTotalLine,
                            Color = ifrsRevacctRegistryInfo.IFRSRevacctRegistry.Color,
                            Class = ifrsRevacctRegistryInfo.IFRSRevacctRegistry.Class,
                            Active = ifrsRevacctRegistryInfo.IFRSRevacctRegistry.Active
                        });
                }

                return ifrsRevacctRegistries.ToArray();
            });
        }

        //public IFRSRevacctRegistry[] GetAllIFRSRevacctRegistries()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { IFRSCoreModuleDefinition.GROUP_ADMINISTRATOR, IFRSCoreModuleDefinition.GROUP_USER };
        //        AllowAccessToOperation(IFRSCoreModuleDefinition.SOLUTION_NAME, groupNames);

        //        IIFRSRevacctRegistryRepository iFRSRevacctRegistryRepository = _DataRepositoryFactory.GetDataRepository<IIFRSRevacctRegistryRepository>();

        //        IEnumerable<IFRSRevacctRegistry> iFRSRevacctRegistry = iFRSRevacctRegistryRepository.Get().ToArray();

        //        return iFRSRevacctRegistry.ToArray();
        //    });
        //}

        //public string[] GetDistinctRefNotes()
        //{
        //    //var connectionString = IFRSContext.GetDataConnection();
        //    var connectionString = GetDataConnection();

        //    List<string> refno;
        //    var refnoList = new List<string>();

        //    using (var con = new SqlConnection(connectionString))
        //    {
        //        var cmd = new SqlCommand("get_distinct_registry_refnote", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 0;


        //        con.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        {
        //            while (reader.Read())
        //            {
        //                var refNote = new ReferenceNoModel();
        //                if (reader["RefNo"] != DBNull.Value)
        //                    refNote.RefNo = reader["RefNo"].ToString();
        //                refnoList.Add(refNote.RefNo);
        //            }
        //            reader.Close();
        //            con.Close();
        //        }

        //        con.Close();
        //    }
        //    return refnoList.ToArray();
        //}

        #endregion
    }
}
