using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Transactions;
using Fintrak.Data.Budget.Contracts;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Business.Budget.Contracts;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Data;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.SystemCore.Entities;
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.SystemCore.Framework;

namespace Fintrak.Business.Budget.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]

    public class RevenueManager : ManagerBase, IRevenueService
    {
        public RevenueManager()
        {
        }

        public RevenueManager(IDataRepositoryFactory dataRepositoryFactory)
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
                    systemCoreEntities.Module module = moduleRepository.Get().Where(c => c.Name == RevenueModuleDefinition.MODULE_NAME).FirstOrDefault();

                    var register = false;
                    if (module == null)
                        register = true;
                    else
                        register = module.CanUpdate;

                    if (register)
                    {
                        //check if module category exit
                        Solution solution = solutionRepository.Get().Where(c => c.Name == RevenueModuleDefinition.SOLUTION_NAME).FirstOrDefault();
                        if (solution == null)
                        {
                            //register solution
                            solution = new Solution()
                            {
                                Name = RevenueModuleDefinition.SOLUTION_NAME,
                                Alias = RevenueModuleDefinition.SOLUTION_ALIAS,
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
                                Name = RevenueModuleDefinition.MODULE_NAME,
                                Alias = RevenueModuleDefinition.MODULE_ALIAS,
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

                        //foreach (var role in RevenueModuleDefinition.GetRoles())
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

                        foreach (var menu in RevenueModuleDefinition.GetMenus())
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

                        foreach (var menuRole in RevenueModuleDefinition.GetMenuRoles())
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

        #region Product Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Product UpdateProduct(Product product)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

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
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductRepository productRepository = _DataRepositoryFactory.GetDataRepository<IProductRepository>();

                productRepository.Remove(productId);
            });
        }

        public Product GetProduct(int productId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

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
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductRepository productRepository = _DataRepositoryFactory.GetDataRepository<IProductRepository>();

                IEnumerable<Product> products = productRepository.Get().ToArray();

                return products.ToArray();
            });
        }

        public ProductData[] GetProducts(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductRepository productRepository = _DataRepositoryFactory.GetDataRepository<IProductRepository>();

                List<ProductData> product = new List<ProductData>();
                IEnumerable<ProductInfo> productInfos = productRepository.GetProducts(year, reviewCode).ToArray();

                foreach (var productInfo in productInfos)
                {
                    product.Add(
                        new ProductData
                        {
                            ProductId = productInfo.Product.EntityId,
                            Year = productInfo.Product.Year,
                            Budgetable = productInfo.Product.Budgetable,
                            CurrencyCode = productInfo.Product.CurrencyCode,
                            CaptionCode = productInfo.RevenueCaption.Code,
                            CaptionName = productInfo.RevenueCaption.Name,
                            CategoryCode = productInfo.ProductCategory.Code,
                            CategoryName = productInfo.ProductCategory.Name,
                            ClassificationCode = productInfo.ProductClassification.Code,
                            ClassificationName = productInfo.ProductClassification.Name,
                            Code = productInfo.Product.Code,
                            GroupCode = productInfo.ProductGroup.Code,
                            GroupName = productInfo.ProductGroup.Name,
                            OtherCode = productInfo.Product.OtherCode,
                            Name = productInfo.Product.Name,
                            Position = productInfo.Product.Position,
                            ProductClass = productInfo.Product.ProductClass.ToString(),
                            Visibility = productInfo.Product.Visibility,
                            VolumeBase = productInfo.Product.VolumeBase,
                            ReviewCode = productInfo.Product.ReviewCode,
                            Active = productInfo.Product.Active

                        });
                }

                return product.ToArray();
            });
        }

        #endregion

        #region ProductGroup Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ProductGroup UpdateProductGroup(ProductGroup productGroup)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductGroupRepository productGroupRepository = _DataRepositoryFactory.GetDataRepository<IProductGroupRepository>();

                ProductGroup updatedEntity = null;

                if (productGroup.ProductGroupId == 0)
                    updatedEntity = productGroupRepository.Add(productGroup);
                else
                    updatedEntity = productGroupRepository.Update(productGroup);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteProductGroup(int productGroupId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductGroupRepository productGroupRepository = _DataRepositoryFactory.GetDataRepository<IProductGroupRepository>();

                productGroupRepository.Remove(productGroupId);
            });
        }

        public ProductGroup GetProductGroup(int productGroupId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductGroupRepository productGroupRepository = _DataRepositoryFactory.GetDataRepository<IProductGroupRepository>();

                ProductGroup productGroupEntity = productGroupRepository.Get(productGroupId);
                if (productGroupEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ProductGroup with ID of {0} is not in database", productGroupId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return productGroupEntity;
            });
        }

        public ProductGroup[] GetAllProductGroups()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductGroupRepository productGroupRepository = _DataRepositoryFactory.GetDataRepository<IProductGroupRepository>();

                IEnumerable<ProductGroup> productGroups = productGroupRepository.Get().ToArray();

                return productGroups.ToArray();
            });
        }

        public ProductGroupData[] GetProductGroups(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductGroupRepository productGroupRepository = _DataRepositoryFactory.GetDataRepository<IProductGroupRepository>();

                List<ProductGroupData> productGroup = new List<ProductGroupData>();
                IEnumerable<ProductGroupInfo> productGroupInfos = productGroupRepository.GetProductGroups(year, reviewCode).ToArray();

                foreach (var productGroupInfo in productGroupInfos)
                {
                    productGroup.Add(
                        new ProductGroupData
                        {
                            ProductGroupId = productGroupInfo.ProductGroup.EntityId,
                            Year = productGroupInfo.ProductGroup.Year,
                            ParentCode = productGroupInfo.Parent.Code,
                            Code = productGroupInfo.ProductGroup.Code,
                            Name = productGroupInfo.ProductGroup.Name,
                            ParentName = productGroupInfo.Parent.Name,
                            Position = productGroupInfo.ProductGroup.Position,
                            ReviewCode = productGroupInfo.ProductGroup.ReviewCode,
                            Active = productGroupInfo.ProductGroup.Active
                        });
                }

                return productGroup.ToArray();
            });
        }

        #endregion

        #region ProductGroupEntry Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ProductGroupEntry UpdateProductGroupEntry(ProductGroupEntry productGroupEntryEntry)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductGroupEntryRepository productGroupEntryEntryRepository = _DataRepositoryFactory.GetDataRepository<IProductGroupEntryRepository>();

                ProductGroupEntry updatedEntity = null;

                if (productGroupEntryEntry.ProductGroupEntryId == 0)
                    updatedEntity = productGroupEntryEntryRepository.Add(productGroupEntryEntry);
                else
                    updatedEntity = productGroupEntryEntryRepository.Update(productGroupEntryEntry);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteProductGroupEntry(int productGroupEntryEntryId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductGroupEntryRepository productGroupEntryEntryRepository = _DataRepositoryFactory.GetDataRepository<IProductGroupEntryRepository>();

                productGroupEntryEntryRepository.Remove(productGroupEntryEntryId);
            });
        }

        public ProductGroupEntry GetProductGroupEntry(int productGroupEntryEntryId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductGroupEntryRepository productGroupEntryEntryRepository = _DataRepositoryFactory.GetDataRepository<IProductGroupEntryRepository>();

                ProductGroupEntry productGroupEntryEntryEntity = productGroupEntryEntryRepository.Get(productGroupEntryEntryId);
                if (productGroupEntryEntryEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ProductGroupEntry with ID of {0} is not in database", productGroupEntryEntryId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return productGroupEntryEntryEntity;
            });
        }

        public ProductGroupEntry[] GetAllProductGroupEntrys()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductGroupEntryRepository productGroupEntryEntryRepository = _DataRepositoryFactory.GetDataRepository<IProductGroupEntryRepository>();

                IEnumerable<ProductGroupEntry> productGroupEntryEntrys = productGroupEntryEntryRepository.Get().ToArray();

                return productGroupEntryEntrys.ToArray();
            });
        }

        public ProductGroupEntryData[] GetProductGroupEntrys(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductGroupEntryRepository productGroupEntryRepository = _DataRepositoryFactory.GetDataRepository<IProductGroupEntryRepository>();

                List<ProductGroupEntryData> productGroupEntry = new List<ProductGroupEntryData>();
                IEnumerable<ProductGroupEntryInfo> productGroupEntryInfos = productGroupEntryRepository.GetProductGroupEntries(year, reviewCode).ToArray();

                foreach (var productGroupEntryInfo in productGroupEntryInfos)
                {
                    productGroupEntry.Add(
                        new ProductGroupEntryData
                        {
                            ProductGroupEntryId = productGroupEntryInfo.ProductGroupEntry.EntityId,
                            Year = productGroupEntryInfo.ProductGroupEntry.Year,
                            CurrencyCode = productGroupEntryInfo.ProductGroupEntry.CurrencyCode,
                            DefintionCode = productGroupEntryInfo.TeamDefinition.Code,
                            DefintionName = productGroupEntryInfo.TeamDefinition.Name,
                            GroupCode = productGroupEntryInfo.ProductGroup.Code,
                            GroupName = productGroupEntryInfo.ProductGroup.Name,
                            MisCode = productGroupEntryInfo.Team.Code,
                            MisName = productGroupEntryInfo.Team.Name,
                            CurrencyName = productGroupEntryInfo.Currency.Name,
                            ReviewCode = productGroupEntryInfo.ProductGroupEntry.ReviewCode,
                            Active = productGroupEntryInfo.ProductGroupEntry.Active
                        });
                }

                return productGroupEntry.ToArray();
            });
        }

        #endregion

        #region ProductInterestRate Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ProductInterestRate UpdateProductInterestRate(ProductInterestRate productInterestRate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductInterestRateRepository productInterestRateRepository = _DataRepositoryFactory.GetDataRepository<IProductInterestRateRepository>();

                ProductInterestRate updatedEntity = null;

                if (productInterestRate.ProductInterestRateId == 0)
                    updatedEntity = productInterestRateRepository.Add(productInterestRate);
                else
                    updatedEntity = productInterestRateRepository.Update(productInterestRate);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteProductInterestRate(int productInterestRateId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductInterestRateRepository productInterestRateRepository = _DataRepositoryFactory.GetDataRepository<IProductInterestRateRepository>();

                productInterestRateRepository.Remove(productInterestRateId);
            });
        }

        public ProductInterestRate GetProductInterestRate(int productInterestRateId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductInterestRateRepository productInterestRateRepository = _DataRepositoryFactory.GetDataRepository<IProductInterestRateRepository>();

                ProductInterestRate productInterestRateEntity = productInterestRateRepository.Get(productInterestRateId);
                if (productInterestRateEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ProductInterestRate with ID of {0} is not in database", productInterestRateId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return productInterestRateEntity;
            });
        }

        public ProductInterestRate[] GetAllProductInterestRates()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductInterestRateRepository productInterestRateRepository = _DataRepositoryFactory.GetDataRepository<IProductInterestRateRepository>();

                IEnumerable<ProductInterestRate> productInterestRates = productInterestRateRepository.Get().ToArray();

                return productInterestRates.ToArray();
            });
        }

        public ProductInterestRateData[] GetProductInterestRates(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductInterestRateRepository productInterestRateRepository = _DataRepositoryFactory.GetDataRepository<IProductInterestRateRepository>();

                List<ProductInterestRateData> productInterestRate = new List<ProductInterestRateData>();
                IEnumerable<ProductInterestRateInfo> productInterestRateInfos = productInterestRateRepository.GetProductInterestRates(year, reviewCode).ToArray();

                foreach (var productInterestRateInfo in productInterestRateInfos)
                {
                    productInterestRate.Add(
                        new ProductInterestRateData
                        {
                            ProductInterestRateId = productInterestRateInfo.ProductInterestRate.EntityId,
                            Year = productInterestRateInfo.ProductInterestRate.Year,
                            ProductCode = productInterestRateInfo.Product.Code,
                            ProductName = productInterestRateInfo.Product.Name,
                            DefintionCode = productInterestRateInfo.TeamDefinition.Code,
                            DefintionName = productInterestRateInfo.TeamDefinition.Name,
                            MisCode = productInterestRateInfo.Team.Code,
                            MisName = productInterestRateInfo.Team.Name,
                            ReviewCode = productInterestRateInfo.ProductInterestRate.ReviewCode,
                            Active = productInterestRateInfo.ProductInterestRate.Active
                        });
                }

                return productInterestRate.ToArray();
            });
        }

        #endregion

        #region ProductPoolRate Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ProductPoolRate UpdateProductPoolRate(ProductPoolRate productPoolRate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductPoolRateRepository productPoolRateRepository = _DataRepositoryFactory.GetDataRepository<IProductPoolRateRepository>();

                ProductPoolRate updatedEntity = null;

                if (productPoolRate.ProductPoolRateId == 0)
                    updatedEntity = productPoolRateRepository.Add(productPoolRate);
                else
                    updatedEntity = productPoolRateRepository.Update(productPoolRate);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteProductPoolRate(int productPoolRateId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductPoolRateRepository productPoolRateRepository = _DataRepositoryFactory.GetDataRepository<IProductPoolRateRepository>();

                productPoolRateRepository.Remove(productPoolRateId);
            });
        }

        public ProductPoolRate GetProductPoolRate(int productPoolRateId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductPoolRateRepository productPoolRateRepository = _DataRepositoryFactory.GetDataRepository<IProductPoolRateRepository>();

                ProductPoolRate productPoolRateEntity = productPoolRateRepository.Get(productPoolRateId);
                if (productPoolRateEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ProductPoolRate with ID of {0} is not in database", productPoolRateId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return productPoolRateEntity;
            });
        }

        public ProductPoolRate[] GetAllProductPoolRates()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductPoolRateRepository productPoolRateRepository = _DataRepositoryFactory.GetDataRepository<IProductPoolRateRepository>();

                IEnumerable<ProductPoolRate> productPoolRates = productPoolRateRepository.Get().ToArray();

                return productPoolRates.ToArray();
            });
        }

        public ProductPoolRateData[] GetProductPoolRates(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductPoolRateRepository productPoolRateRepository = _DataRepositoryFactory.GetDataRepository<IProductPoolRateRepository>();

                List<ProductPoolRateData> productPoolRate = new List<ProductPoolRateData>();
                IEnumerable<ProductPoolRateInfo> productPoolRateInfos = productPoolRateRepository.GetProductPoolRates(year, reviewCode).ToArray();

                foreach (var productPoolRateInfo in productPoolRateInfos)
                {
                    productPoolRate.Add(
                        new ProductPoolRateData
                        {
                            ProductPoolRateId = productPoolRateInfo.ProductPoolRate.EntityId,
                            Year = productPoolRateInfo.ProductPoolRate.Year,
                            ProductCode = productPoolRateInfo.Product.Code,
                            ProductName = productPoolRateInfo.Product.Name,
                            DefintionCode = productPoolRateInfo.TeamDefinition.Code,
                            DefintionName = productPoolRateInfo.TeamDefinition.Name,
                            MisCode = productPoolRateInfo.Team.Code,
                            MisName = productPoolRateInfo.Team.Name,
                            ReviewCode = productPoolRateInfo.ProductPoolRate.ReviewCode,
                            Active = productPoolRateInfo.ProductPoolRate.Active
                        });
                }

                return productPoolRate.ToArray();
            });
        }

        #endregion

        #region ProductSharedRatio Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ProductSharedRatio UpdateProductSharedRatio(ProductSharedRatio productSharedRatio)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductSharedRatioRepository productSharedRatioRepository = _DataRepositoryFactory.GetDataRepository<IProductSharedRatioRepository>();

                ProductSharedRatio updatedEntity = null;

                if (productSharedRatio.ProductSharedRatioId == 0)
                    updatedEntity = productSharedRatioRepository.Add(productSharedRatio);
                else
                    updatedEntity = productSharedRatioRepository.Update(productSharedRatio);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteProductSharedRatio(int productSharedRatioId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductSharedRatioRepository productSharedRatioRepository = _DataRepositoryFactory.GetDataRepository<IProductSharedRatioRepository>();

                productSharedRatioRepository.Remove(productSharedRatioId);
            });
        }

        public ProductSharedRatio GetProductSharedRatio(int productSharedRatioId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductSharedRatioRepository productSharedRatioRepository = _DataRepositoryFactory.GetDataRepository<IProductSharedRatioRepository>();

                ProductSharedRatio productSharedRatioEntity = productSharedRatioRepository.Get(productSharedRatioId);
                if (productSharedRatioEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ProductSharedRatio with ID of {0} is not in database", productSharedRatioId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return productSharedRatioEntity;
            });
        }

        public ProductSharedRatio[] GetAllProductSharedRatios()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductSharedRatioRepository productSharedRatioRepository = _DataRepositoryFactory.GetDataRepository<IProductSharedRatioRepository>();

                IEnumerable<ProductSharedRatio> productSharedRatios = productSharedRatioRepository.Get().ToArray();

                return productSharedRatios.ToArray();
            });
        }

        public ProductSharedRatioData[] GetProductSharedRatios(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductSharedRatioRepository productSharedRatioRepository = _DataRepositoryFactory.GetDataRepository<IProductSharedRatioRepository>();

                List<ProductSharedRatioData> productSharedRatio = new List<ProductSharedRatioData>();
                IEnumerable<ProductSharedRatioInfo> productSharedRatioInfos = productSharedRatioRepository.GetProductSharedRatios(year, reviewCode).ToArray();

                foreach (var productSharedRatioInfo in productSharedRatioInfos)
                {
                    productSharedRatio.Add(
                        new ProductSharedRatioData
                        {
                            ProductSharedRatioId = productSharedRatioInfo.ProductSharedRatio.EntityId,
                            Year = productSharedRatioInfo.ProductSharedRatio.Year,
                            ProductCode = productSharedRatioInfo.Product.Code,
                            ProductName = productSharedRatioInfo.Product.Name,
                            DefintionCode = productSharedRatioInfo.TeamDefinition.Code,
                            DefintionName = productSharedRatioInfo.TeamDefinition.Name,
                            MisCode = productSharedRatioInfo.Team.Code,
                            MisName = productSharedRatioInfo.Team.Name,
                            ReviewCode = productSharedRatioInfo.ProductSharedRatio.ReviewCode,
                            Active = productSharedRatioInfo.ProductSharedRatio.Active
                        });
                }

                return productSharedRatio.ToArray();
            });
        }

        #endregion

        #region ProductVolumeBasedRate Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ProductVolumeBasedRate UpdateProductVolumeBasedRate(ProductVolumeBasedRate productVolumeBasedRate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductVolumeBasedRateRepository productVolumeBasedRateRepository = _DataRepositoryFactory.GetDataRepository<IProductVolumeBasedRateRepository>();

                ProductVolumeBasedRate updatedEntity = null;

                if (productVolumeBasedRate.ProductVolumeBasedRateId == 0)
                    updatedEntity = productVolumeBasedRateRepository.Add(productVolumeBasedRate);
                else
                    updatedEntity = productVolumeBasedRateRepository.Update(productVolumeBasedRate);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteProductVolumeBasedRate(int productVolumeBasedRateId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductVolumeBasedRateRepository productVolumeBasedRateRepository = _DataRepositoryFactory.GetDataRepository<IProductVolumeBasedRateRepository>();

                productVolumeBasedRateRepository.Remove(productVolumeBasedRateId);
            });
        }

        public ProductVolumeBasedRate GetProductVolumeBasedRate(int productVolumeBasedRateId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductVolumeBasedRateRepository productVolumeBasedRateRepository = _DataRepositoryFactory.GetDataRepository<IProductVolumeBasedRateRepository>();

                ProductVolumeBasedRate productVolumeBasedRateEntity = productVolumeBasedRateRepository.Get(productVolumeBasedRateId);
                if (productVolumeBasedRateEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ProductVolumeBasedRate with ID of {0} is not in database", productVolumeBasedRateId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return productVolumeBasedRateEntity;
            });
        }

        public ProductVolumeBasedRate[] GetAllProductVolumeBasedRates()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductVolumeBasedRateRepository productVolumeBasedRateRepository = _DataRepositoryFactory.GetDataRepository<IProductVolumeBasedRateRepository>();

                IEnumerable<ProductVolumeBasedRate> productVolumeBasedRates = productVolumeBasedRateRepository.Get().ToArray();

                return productVolumeBasedRates.ToArray();
            });
        }

        public ProductVolumeBasedRateData[] GetProductVolumeBasedRates(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductVolumeBasedRateRepository productVolumeBasedRateRepository = _DataRepositoryFactory.GetDataRepository<IProductVolumeBasedRateRepository>();

                List<ProductVolumeBasedRateData> productVolumeBasedRate = new List<ProductVolumeBasedRateData>();
                IEnumerable<ProductVolumeBasedRateInfo> productVolumeBasedRateInfos = productVolumeBasedRateRepository.GetProductVolumeBasedRates(year, reviewCode).ToArray();

                foreach (var productVolumeBasedRateInfo in productVolumeBasedRateInfos)
                {
                    productVolumeBasedRate.Add(
                        new ProductVolumeBasedRateData
                        {
                            ProductVolumeBasedRateId = productVolumeBasedRateInfo.ProductVolumeBasedRate.EntityId,
                            Year = productVolumeBasedRateInfo.ProductVolumeBasedRate.Year,
                            ProductCode = productVolumeBasedRateInfo.Product.Code,
                            ProductName = productVolumeBasedRateInfo.Product.Name,
                            DefintionCode = productVolumeBasedRateInfo.TeamDefinition.Code,
                            DefintionName = productVolumeBasedRateInfo.TeamDefinition.Name,
                            MisCode = productVolumeBasedRateInfo.Team.Code,
                            MisName = productVolumeBasedRateInfo.Team.Name,
                            ReviewCode = productVolumeBasedRateInfo.ProductVolumeBasedRate.ReviewCode,
                            Active = productVolumeBasedRateInfo.ProductVolumeBasedRate.Active
                        });
                }

                return productVolumeBasedRate.ToArray();
            });
        }

        #endregion

        #region ProductVolumeBasedSetup Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ProductVolumeBasedSetup UpdateProductVolumeBasedSetup(ProductVolumeBasedSetup productVolumeBasedSetup)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductVolumeBasedSetupRepository productVolumeBasedSetupRepository = _DataRepositoryFactory.GetDataRepository<IProductVolumeBasedSetupRepository>();

                ProductVolumeBasedSetup updatedEntity = null;

                if (productVolumeBasedSetup.ProductVolumeBasedSetupId == 0)
                    updatedEntity = productVolumeBasedSetupRepository.Add(productVolumeBasedSetup);
                else
                    updatedEntity = productVolumeBasedSetupRepository.Update(productVolumeBasedSetup);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteProductVolumeBasedSetup(int productVolumeBasedSetupId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductVolumeBasedSetupRepository productVolumeBasedSetupRepository = _DataRepositoryFactory.GetDataRepository<IProductVolumeBasedSetupRepository>();

                productVolumeBasedSetupRepository.Remove(productVolumeBasedSetupId);
            });
        }

        public ProductVolumeBasedSetup GetProductVolumeBasedSetup(int productVolumeBasedSetupId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductVolumeBasedSetupRepository productVolumeBasedSetupRepository = _DataRepositoryFactory.GetDataRepository<IProductVolumeBasedSetupRepository>();

                ProductVolumeBasedSetup productVolumeBasedSetupEntity = productVolumeBasedSetupRepository.Get(productVolumeBasedSetupId);
                if (productVolumeBasedSetupEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ProductVolumeBasedSetup with ID of {0} is not in database", productVolumeBasedSetupId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return productVolumeBasedSetupEntity;
            });
        }

        public ProductVolumeBasedSetup[] GetAllProductVolumeBasedSetups()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductVolumeBasedSetupRepository productVolumeBasedSetupRepository = _DataRepositoryFactory.GetDataRepository<IProductVolumeBasedSetupRepository>();

                IEnumerable<ProductVolumeBasedSetup> productVolumeBasedSetups = productVolumeBasedSetupRepository.Get().ToArray();

                return productVolumeBasedSetups.ToArray();
            });
        }

        public ProductVolumeBasedSetupData[] GetProductVolumeBasedSetups(string year, string reviewCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IProductVolumeBasedSetupRepository productVolumeBasedSetupRepository = _DataRepositoryFactory.GetDataRepository<IProductVolumeBasedSetupRepository>();

                List<ProductVolumeBasedSetupData> productVolumeBasedSetup = new List<ProductVolumeBasedSetupData>();
                IEnumerable<ProductVolumeBasedSetupInfo> productVolumeBasedSetupInfos = productVolumeBasedSetupRepository.GetProductVolumeBasedSetups(year, reviewCode).ToArray();

                foreach (var productVolumeBasedSetupInfo in productVolumeBasedSetupInfos)
                {
                    productVolumeBasedSetup.Add(
                        new ProductVolumeBasedSetupData
                        {
                            ProductVolumeBasedSetupId = productVolumeBasedSetupInfo.ProductVolumeBasedSetup.EntityId,
                            Year = productVolumeBasedSetupInfo.ProductVolumeBasedSetup.Year,
                            ProductCode = productVolumeBasedSetupInfo.Product.Code,
                            ProductName = productVolumeBasedSetupInfo.Product.Name,
                            MakeUpCode = productVolumeBasedSetupInfo.ProductVolumeBasedSetup.MakeUpCode,
                            MakeUpName = productVolumeBasedSetupInfo.ProductVolumeBasedSetup.MakeUpCode,
                            ReviewCode = productVolumeBasedSetupInfo.ProductVolumeBasedSetup.ReviewCode,
                            Active = productVolumeBasedSetupInfo.ProductVolumeBasedSetup.Active
                        });
                }

                return productVolumeBasedSetup.ToArray();
            });
        }

        #endregion

        #region RevenueCaption Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public RevenueCaption UpdateRevenueCaption(RevenueCaption revenueCaption)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IRevenueCaptionRepository revenueCaptionRepository = _DataRepositoryFactory.GetDataRepository<IRevenueCaptionRepository>();

                RevenueCaption updatedEntity = null;

                if (revenueCaption.RevenueCaptionId == 0)
                    updatedEntity = revenueCaptionRepository.Add(revenueCaption);
                else
                    updatedEntity = revenueCaptionRepository.Update(revenueCaption);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteRevenueCaption(int revenueCaptionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IRevenueCaptionRepository revenueCaptionRepository = _DataRepositoryFactory.GetDataRepository<IRevenueCaptionRepository>();

                revenueCaptionRepository.Remove(revenueCaptionId);
            });
        }

        public RevenueCaption GetRevenueCaption(int revenueCaptionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IRevenueCaptionRepository revenueCaptionRepository = _DataRepositoryFactory.GetDataRepository<IRevenueCaptionRepository>();

                RevenueCaption revenueCaptionEntity = revenueCaptionRepository.Get(revenueCaptionId);
                if (revenueCaptionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("RevenueCaption with ID of {0} is not in database", revenueCaptionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return revenueCaptionEntity;
            });
        }

        public RevenueCaption[] GetAllRevenueCaptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IRevenueCaptionRepository revenueCaptionRepository = _DataRepositoryFactory.GetDataRepository<IRevenueCaptionRepository>();

                IEnumerable<RevenueCaption> revenueCaptions = revenueCaptionRepository.Get().ToArray();

                return revenueCaptions.ToArray();
            });
        }

        #endregion

        //#region RevenuePool Operations

        //[OperationBehavior(TransactionScopeRequired = true)]
        //public RevenuePool UpdateRevenuePool(RevenuePool revenuePool)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
        //        AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

        //        IRevenuePoolRepository revenuePoolRepository = _DataRepositoryFactory.GetDataRepository<IRevenuePoolRepository>();

        //        RevenuePool updatedEntity = null;

        //        if (revenuePool.RevenuePoolId == 0)
        //            updatedEntity = revenuePoolRepository.Add(revenuePool);
        //        else
        //            updatedEntity = revenuePoolRepository.Update(revenuePool);

        //        return updatedEntity;
        //    });
        //}

        //[OperationBehavior(TransactionScopeRequired = true)]
        //public void DeleteRevenuePool(int revenuePoolId)
        //{
        //    ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
        //        AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

        //        IRevenuePoolRepository revenuePoolRepository = _DataRepositoryFactory.GetDataRepository<IRevenuePoolRepository>();

        //        revenuePoolRepository.Remove(revenuePoolId);
        //    });
        //}

        //public RevenuePool GetRevenuePool(int revenuePoolId)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
        //        AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

        //        IRevenuePoolRepository revenuePoolRepository = _DataRepositoryFactory.GetDataRepository<IRevenuePoolRepository>();

        //        RevenuePool revenuePoolEntity = revenuePoolRepository.Get(revenuePoolId);
        //        if (revenuePoolEntity == null)
        //        {
        //            NotFoundException ex = new NotFoundException(string.Format("RevenuePool with ID of {0} is not in database", revenuePoolId));
        //            throw new FaultException<NotFoundException>(ex, ex.Message);
        //        }

        //        return revenuePoolEntity;
        //    });
        //}

        //public RevenuePool[] GetAllRevenuePools()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
        //        AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

        //        IRevenuePoolRepository revenuePoolRepository = _DataRepositoryFactory.GetDataRepository<IRevenuePoolRepository>();

        //        IEnumerable<RevenuePool> revenuePools = revenuePoolRepository.Get().ToArray();

        //        return revenuePools.ToArray();
        //    });
        //}

        //#endregion

        #region RevenueSetting Operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public RevenueSetting UpdateRevenueSetting(RevenueSetting revenueSetting)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IRevenueSettingRepository revenueSettingRepository = _DataRepositoryFactory.GetDataRepository<IRevenueSettingRepository>();

                RevenueSetting updatedEntity = null;

                if (revenueSetting.RevenueSettingId == 0)
                    updatedEntity = revenueSettingRepository.Add(revenueSetting);
                else
                    updatedEntity = revenueSettingRepository.Update(revenueSetting);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteRevenueSetting(int revenueSettingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IRevenueSettingRepository revenueSettingRepository = _DataRepositoryFactory.GetDataRepository<IRevenueSettingRepository>();

                revenueSettingRepository.Remove(revenueSettingId);
            });
        }

        public RevenueSetting GetRevenueSetting(int revenueSettingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IRevenueSettingRepository revenueSettingRepository = _DataRepositoryFactory.GetDataRepository<IRevenueSettingRepository>();

                RevenueSetting revenueSettingEntity = revenueSettingRepository.Get(revenueSettingId);
                if (revenueSettingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("RevenueSetting with ID of {0} is not in database", revenueSettingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return revenueSettingEntity;
            });
        }

        public RevenueSetting[] GetAllRevenueSettings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { BudgetModuleDefinition.GROUP_ADMINISTRATOR, BudgetModuleDefinition.GROUP_BUSINESS };
                AllowAccessToOperation(BudgetModuleDefinition.SOLUTION_NAME, groupNames);

                IRevenueSettingRepository revenueSettingRepository = _DataRepositoryFactory.GetDataRepository<IRevenueSettingRepository>();

                IEnumerable<RevenueSetting> revenueSettings = revenueSettingRepository.Get().ToArray();

                return revenueSettings.ToArray();
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
