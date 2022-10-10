using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Transactions;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Business.Budget.Contracts;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Data;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.SystemCore.Entities;
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.SystemCore.Framework;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;

namespace Fintrak.Business.Budget.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]

    public class StaffExpenseManager : ManagerBase, IStaffExpenseService
    {
        public StaffExpenseManager()
        {
        }

        public StaffExpenseManager(IDataRepositoryFactory dataRepositoryFactory)
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
                systemCoreData.IModuleRepository moduleRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IModuleRepository>();
                IMenuRepository menuRepository = _DataRepositoryFactory.GetDataRepository<IMenuRepository>();
                IRoleRepository roleRepository = _DataRepositoryFactory.GetDataRepository<IRoleRepository>();
                IMenuRoleRepository menuRoleRepository = _DataRepositoryFactory.GetDataRepository<IMenuRoleRepository>();

                using (TransactionScope ts = new TransactionScope())
                {
                    //check if module has been installed
                    systemCoreEntities.Module module = moduleRepository.Get().Where(c => c.Name == StaffExpenseModuleDefinition.MODULE_NAME).FirstOrDefault();

                    var register = false;
                    if (module == null)
                        register = true;
                    else
                        register = module.CanUpdate;

                    if (register)
                    {
                        //check if module category exit
                        Solution solution = solutionRepository.Get().Where(c => c.Name == StaffExpenseModuleDefinition.SOLUTION_NAME).FirstOrDefault();
                        if (solution == null)
                        {
                            //register solution
                            solution = new Solution()
                            {
                                Name = StaffExpenseModuleDefinition.SOLUTION_NAME,
                                Alias = StaffExpenseModuleDefinition.SOLUTION_ALIAS,
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
                            module = new systemCoreEntities.Module()
                            {
                                Name = StaffExpenseModuleDefinition.MODULE_NAME,
                                Alias = StaffExpenseModuleDefinition.MODULE_ALIAS,
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
                        //var updatedRoles = new List<Role>();

                        //foreach (var role in StaffExpenseModuleDefinition.GetRoles())
                        //{
                        //    var localRole = existingRoles.Where(c => c.Name == role.Name).FirstOrDefault();

                        //    if (localRole == null)
                        //    {
                        //        localRole = new Role() { Name = role.Name, Description = role.Description, SolutionId = solution.SolutionId, Type = RoleType.Application, Active = true, Deleted = false, CreatedBy = "Auto", CreatedOn = DateTime.Now, UpdatedBy = "Auto", UpdatedOn = DateTime.Now };

                        //        localRole = roleRepository.Add(localRole);
                        //    }
                        //    else
                        //    {
                        //        localRole.Description = role.Description;
                        //        localRole.UpdatedOn = DateTime.Now;
                        //        localRole = roleRepository.Update(localRole);
                        //    }

                        //    updatedRoles.Add(localRole);
                        //}

                        //Menus
                        var existingMenus = menuRepository.Get().Where(c => c.ModuleId == module.ModuleId).ToList();
                        var updatedMenus = new List<Menu>();

                        var menuIndex = 0;

                        foreach (var menu in StaffExpenseModuleDefinition.GetMenus())
                        {
                            menuIndex += 1;
                            Menu parentMenu = null;

                            int? parentId = null;
                            if (!string.IsNullOrEmpty(menu.Parent))
                            {
                                if (string.IsNullOrEmpty(menu.ParentModule))
                                {
                                    parentMenu = existingMenus.Where(c => c.Name == menu.Parent).FirstOrDefault();

                                    if (parentMenu == null)
                                        parentMenu = menuRepository.Get().Where(c => c.ModuleId == module.ModuleId && c.Name == menu.Parent).FirstOrDefault();
                                }
                                else
                                {
                                    var parentModule = moduleRepository.Get().Where(c => c.Name == menu.ParentModule).FirstOrDefault();

                                    if (parentModule != null)
                                        parentMenu = menuRepository.Get().Where(c => c.ModuleId == parentModule.ModuleId && c.Name == menu.Parent).FirstOrDefault();
                                }

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

                        foreach (var menuRole in StaffExpenseModuleDefinition.GetMenuRoles())
                        {
                            var myMenu = updatedMenus.Where(c => c.Name == menuRole.MenuName).FirstOrDefault();
                            var myRole = existingRoles.Where(c => c.Name == menuRole.RoleName).FirstOrDefault();

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

        #region Grade Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Grade UpdateGrade(Grade grade)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IGradeRepository gradeRepository = _DataRepositoryFactory.GetDataRepository<IGradeRepository>();

                Grade updatedEntity = null;

                if (grade.GradeId == 0)
                    updatedEntity = gradeRepository.Add(grade);
                else
                    updatedEntity = gradeRepository.Update(grade);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteGrade(int gradeId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IGradeRepository gradeRepository = _DataRepositoryFactory.GetDataRepository<IGradeRepository>();

                gradeRepository.Remove(gradeId);
            });
        }

        public Grade GetGrade(int gradeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IGradeRepository gradeRepository = _DataRepositoryFactory.GetDataRepository<IGradeRepository>();

                Grade gradeEntity = gradeRepository.Get(gradeId);
                if (gradeEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Grade with ID of {0} is not in database", gradeId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return gradeEntity;
            });
        }

        public Grade[] GetAllGrades()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IGradeRepository gradeRepository = _DataRepositoryFactory.GetDataRepository<IGradeRepository>();

                IEnumerable<Grade> grades = gradeRepository.Get().ToArray();

                return grades.ToArray();
            });
        }

        #endregion

        #region PayClassification Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PayClassification UpdatePayClassification(PayClassification payClassification)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IPayClassificationRepository payClassificationRepository = _DataRepositoryFactory.GetDataRepository<IPayClassificationRepository>();

                PayClassification updatedEntity = null;

                if (payClassification.PayClassificationId == 0)
                    updatedEntity = payClassificationRepository.Add(payClassification);
                else
                    updatedEntity = payClassificationRepository.Update(payClassification);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePayClassification(int payClassificationId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IPayClassificationRepository payClassificationRepository = _DataRepositoryFactory.GetDataRepository<IPayClassificationRepository>();

                payClassificationRepository.Remove(payClassificationId);
            });
        }

        public PayClassification GetPayClassification(int payClassificationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IPayClassificationRepository payClassificationRepository = _DataRepositoryFactory.GetDataRepository<IPayClassificationRepository>();

                PayClassification payClassificationEntity = payClassificationRepository.Get(payClassificationId);
                if (payClassificationEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PayClassification with ID of {0} is not in database", payClassificationId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return payClassificationEntity;
            });
        }

        public PayClassification[] GetAllPayClassifications()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IPayClassificationRepository payClassificationRepository = _DataRepositoryFactory.GetDataRepository<IPayClassificationRepository>();

                IEnumerable<PayClassification> payClassifications = payClassificationRepository.Get().ToArray();

                return payClassifications.ToArray();
            });
        }

        #endregion

        #region PayStructure Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PayStructure UpdatePayStructure(PayStructure payStructure)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IPayStructureRepository payStructureRepository = _DataRepositoryFactory.GetDataRepository<IPayStructureRepository>();

                PayStructure updatedEntity = null;

                if (payStructure.PayStructureId == 0)
                    updatedEntity = payStructureRepository.Add(payStructure);
                else
                    updatedEntity = payStructureRepository.Update(payStructure);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePayStructure(int payStructureId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IPayStructureRepository payStructureRepository = _DataRepositoryFactory.GetDataRepository<IPayStructureRepository>();

                payStructureRepository.Remove(payStructureId);
            });
        }

        public PayStructure GetPayStructure(int payStructureId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IPayStructureRepository payStructureRepository = _DataRepositoryFactory.GetDataRepository<IPayStructureRepository>();

                PayStructure payStructureEntity = payStructureRepository.Get(payStructureId);
                if (payStructureEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PayStructure with ID of {0} is not in database", payStructureId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return payStructureEntity;
            });
        }

        public PayStructure[] GetAllPayStructures()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IPayStructureRepository payStructureRepository = _DataRepositoryFactory.GetDataRepository<IPayStructureRepository>();

                IEnumerable<PayStructure> payStructures = payStructureRepository.Get().ToArray();

                return payStructures.ToArray();
            });
        }

        public PayStructureData[] GetPayStructures(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IPayStructureRepository payStructureRepository = _DataRepositoryFactory.GetDataRepository<IPayStructureRepository>();

                List<PayStructureData> payStructure = new List<PayStructureData>();
                IEnumerable<PayStructureInfo> payStructureInfos = payStructureRepository.GetPayStructures(year, reviewCode).ToArray();

                foreach (var payStructureInfo in payStructureInfos)
                {
                    payStructure.Add(
                        new PayStructureData
                        {
                            PayStructureId = payStructureInfo.PayStructure.EntityId,
                            Year = payStructureInfo.PayStructure.Year,
                            ClassificationCode = payStructureInfo.PayStructure.ClassificationCode,
                            ClassificationName = payStructureInfo.PayClassification.Name,
                            GradeCode = payStructureInfo.Grade.Code,
                            GradeName = payStructureInfo.Grade.Name,
                            GrossPay = payStructureInfo.PayStructure.GrossPay,
                            ReviewCode = payStructureInfo.PayStructure.ReviewCode,
                            ThirtheenMonth = payStructureInfo.PayStructure.ThirtheenMonth,
                            Active = payStructureInfo.PayStructure.Active
                        });
                }

                return payStructure.ToArray();
            });
        }

        #endregion

        #region StaffCost Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public StaffCost UpdateStaffCost(StaffCost staffCost)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IStaffCostRepository staffCostRepository = _DataRepositoryFactory.GetDataRepository<IStaffCostRepository>();

                StaffCost updatedEntity = null;

                if (staffCost.StaffCostId == 0)
                    updatedEntity = staffCostRepository.Add(staffCost);
                else
                    updatedEntity = staffCostRepository.Update(staffCost);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteStaffCost(int staffCostId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IStaffCostRepository staffCostRepository = _DataRepositoryFactory.GetDataRepository<IStaffCostRepository>();

                staffCostRepository.Remove(staffCostId);
            });
        }

        public StaffCost GetStaffCost(int staffCostId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IStaffCostRepository staffCostRepository = _DataRepositoryFactory.GetDataRepository<IStaffCostRepository>();

                StaffCost staffCostEntity = staffCostRepository.Get(staffCostId);
                if (staffCostEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("StaffCost with ID of {0} is not in database", staffCostId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return staffCostEntity;
            });
        }

        public StaffCost[] GetAllStaffCosts()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IStaffCostRepository staffCostRepository = _DataRepositoryFactory.GetDataRepository<IStaffCostRepository>();

                IEnumerable<StaffCost> staffCosts = staffCostRepository.Get().ToArray();

                return staffCosts.ToArray();
            });
        }

        public StaffCostData[] GetStaffCosts(string year, string reviewCode, string definitionCode, string misCode, CenterTypeEnum center)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IStaffCostRepository staffCostRepository = _DataRepositoryFactory.GetDataRepository<IStaffCostRepository>();

                List<StaffCostData> staffCost = new List<StaffCostData>();
                IEnumerable<StaffCostInfo> staffCostInfos = staffCostRepository.GetStaffCosts(year, reviewCode, definitionCode, misCode, center).ToArray();

                foreach (var staffCostInfo in staffCostInfos)
                {
                    staffCost.Add(
                        new StaffCostData
                        {
                            StaffCostId = staffCostInfo.StaffCost.EntityId,
                            Year = staffCostInfo.StaffCost.Year,
                            ClassificationCode = staffCostInfo.StaffCost.ClassificationCode,
                            ClassificationName = staffCostInfo.PayClassification.Name,
                            GradeCode = staffCostInfo.Grade.Code,
                            GradeName = staffCostInfo.Grade.Name,
                            CenterType = staffCostInfo.StaffCost.CenterType,
                            CurrencyCode = staffCostInfo.StaffCost.CurrencyCode,
                            DefintionCode = staffCostInfo.TeamDefinition.Code,
                            DefintionName = staffCostInfo.TeamDefinition.Name,
                            MisCode = staffCostInfo.Team.Code,
                            MisName = staffCostInfo.Team.Name,
                            TransactionType = staffCostInfo.StaffCost.TransactionType,
                            ReviewCode = staffCostInfo.StaffCost.ReviewCode,
                            Active = staffCostInfo.StaffCost.Active
                        });
                }

                return staffCost.ToArray();
            });
        }

        #endregion

        #region StaffCount Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public StaffCount UpdateStaffCount(StaffCount staffCount)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IStaffCountRepository staffCountRepository = _DataRepositoryFactory.GetDataRepository<IStaffCountRepository>();

                StaffCount updatedEntity = null;

                if (staffCount.StaffCountId == 0)
                    updatedEntity = staffCountRepository.Add(staffCount);
                else
                    updatedEntity = staffCountRepository.Update(staffCount);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteStaffCount(int staffCountId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IStaffCountRepository staffCountRepository = _DataRepositoryFactory.GetDataRepository<IStaffCountRepository>();

                staffCountRepository.Remove(staffCountId);
            });
        }

        public StaffCount GetStaffCount(int staffCountId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IStaffCountRepository staffCountRepository = _DataRepositoryFactory.GetDataRepository<IStaffCountRepository>();

                StaffCount staffCountEntity = staffCountRepository.Get(staffCountId);
                if (staffCountEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("StaffCount with ID of {0} is not in database", staffCountId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return staffCountEntity;
            });
        }

        public StaffCount[] GetAllStaffCounts()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IStaffCountRepository staffCountRepository = _DataRepositoryFactory.GetDataRepository<IStaffCountRepository>();

                IEnumerable<StaffCount> staffCounts = staffCountRepository.Get().ToArray();

                return staffCounts.ToArray();
            });
        }

        public StaffCountData[] GetStaffCounts(string year, string reviewCode, string definitionCode, string misCode, CenterTypeEnum center)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IStaffCountRepository staffCountRepository = _DataRepositoryFactory.GetDataRepository<IStaffCountRepository>();

                List<StaffCountData> staffCount = new List<StaffCountData>();
                IEnumerable<StaffCountInfo> staffCountInfos = staffCountRepository.GetStaffCounts(year, reviewCode, definitionCode, misCode, center).ToArray();

                foreach (var staffCountInfo in staffCountInfos)
                {
                    staffCount.Add(
                        new StaffCountData
                        {
                            StaffCountId = staffCountInfo.StaffCount.EntityId,
                            Year = staffCountInfo.StaffCount.Year,
                            ClassificationCode = staffCountInfo.StaffCount.ClassificationCode,
                            ClassificationName = staffCountInfo.PayClassification.Name,
                            GradeCode = staffCountInfo.Grade.Code,
                            GradeName = staffCountInfo.Grade.Name,
                            CenterType = staffCountInfo.StaffCount.CenterType,
                            CurrencyCode = staffCountInfo.StaffCount.CurrencyCode,
                            DefintionCode = staffCountInfo.TeamDefinition.Code,
                            DefintionName = staffCountInfo.TeamDefinition.Name,
                            MisCode = staffCountInfo.Team.Code,
                            MisName = staffCountInfo.Team.Name,
                            TransactionType = staffCountInfo.StaffCount.TransactionType,
                            ReviewCode = staffCountInfo.StaffCount.ReviewCode,
                            Active = staffCountInfo.StaffCount.Active
                        });
                }

                return staffCount.ToArray();
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
