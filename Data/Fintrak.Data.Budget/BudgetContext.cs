using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Data;
using systemContract = Fintrak.Data.SystemCore.Contracts;
using systemCore = Fintrak.Data.SystemCore;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.AuditService;

namespace Fintrak.Data.Budget
{
    public class BudgetContext : DbContext
    {
        const string SOLUTION_NAME = "BUDGET";
        AuditManager _auditManager;

        public BudgetContext()
            : base(GetDataConnection())
        {
            System.Data.Entity.Database.SetInitializer<BudgetContext>(null);

            _auditManager = new AuditManager(GetDataConnection());
        }

        public BudgetContext(string connectionString)
            : base(connectionString)
        {
            System.Data.Entity.Database.SetInitializer<BudgetContext>(null);
            _auditManager = new AuditManager(connectionString);
        }

        //Core
        public DbSet<BudgetingLevel> BudgetingLevelSet { get; set; }
        public DbSet<Currency> CurrencySet { get; set; }
        public DbSet<CurrencyRate> CurrencyRateSet { get; set; }
        public DbSet<GeneralSetting> GeneralSettingSet { get; set; }
        public DbSet<ModificationLevel> ModificationLevelSet { get; set; }
        public DbSet<Module> ModuleSet { get; set; }
        public DbSet<Operation> OperationSet { get; set; }
        public DbSet<OperationReview> OperationReviewSet { get; set; }
        public DbSet<Policy> PolicySet { get; set; }
        public DbSet<PolicyLevel> PolicyLevelSet { get; set; }
        public DbSet<PrimaryLock> PrimaryLockSet { get; set; }
        public DbSet<SecondaryLock> SecondaryLockSet { get; set; }
        public DbSet<SecondaryLockLevel> SecondaryLockLevelSet { get; set; }

        //StaffCost
        public DbSet<Grade> GradeSet { get; set; }
        public DbSet<PayClassification> PayClassificationSet { get; set; }
        public DbSet<PayStructure> PayStructureSet { get; set; }
        public DbSet<StaffCost> StaffCostSet { get; set; }
        public DbSet<StaffCount> StaffCountSet { get; set; }
        public DbSet<TeamPayClassification> TeamPayClassificationSet { get; set; }

        //Capex
        public DbSet<CapexCategory> CapexCategorySet { get; set; }
        public DbSet<CapexCost> CapexCostSet { get; set; }
        public DbSet<CapexEntry> CapexEntrySet { get; set; }
        public DbSet<CapexItem> CapexItemSet { get; set; }
        public DbSet<DepreciationRate> DepreciationRateSet { get; set; }

        //Opex
        public DbSet<OpexCategory> OpexCategorySet { get; set; }
        public DbSet<OpexEntry> OpexEntrySet { get; set; }
        public DbSet<OpexItem> OpexItemSet { get; set; }
        public DbSet<OpexVolumeBasedRate> OpexVolumeBasedRateSet { get; set; }
        public DbSet<OpexVolumeBasedSetup> OpexVolumeBasedSetupSet { get; set; }

        //Team
        public DbSet<OfficerDetail> OfficerDetailSet { get; set; }
        public DbSet<Team> TeamSet { get; set; }
        public DbSet<TeamClassification> TeamClassificationSet { get; set; }
        public DbSet<TeamClassificationType> TeamClassificationTypeSet { get; set; }
        public DbSet<TeamDefinition> TeamDefinitionSet { get; set; }
        public DbSet<TeamSetting> TeamSettingSet { get; set; }
        public DbSet<TeamUser> TeamUserSet { get; set; }

        //Fee
        public DbSet<FeeCalculationType> FeeCalculationTypeSet { get; set; }
        public DbSet<FeeCaption> FeeCaptionSet { get; set; }
        public DbSet<FeeCategory> FeeCategorySet { get; set; }
        public DbSet<FeeEntry> FeeEntrySet { get; set; }
        public DbSet<FeeGroup> FeeGroupSet { get; set; }
        public DbSet<FeeGroupEntry> FeeGroupEntrySet { get; set; }
        public DbSet<FeeItem> FeeItemSet { get; set; }
        public DbSet<FeeMovement> FeeMovementSet { get; set; }
        public DbSet<FeeSharedExemption> FeeSharedExemptionSet { get; set; }
        public DbSet<FeeSharedRatio> FeeSharedRatioSet { get; set; }
        public DbSet<FeeVolumeBasedRate> FeeVolumeBasedRateSet { get; set; }
        public DbSet<FeeVolumeBasedSetup> FeeVolumeBasedSetupSet { get; set; }

        //Revenue
        public DbSet<Product> ProductSet { get; set; }
        public DbSet<ProductCategory> ProductCategorySet { get; set; }
        public DbSet<ProductClassification> ProductClassificationSet { get; set; }
        public DbSet<ProductGroup> ProductGroupSet { get; set; }
        public DbSet<ProductGroupEntry> ProductGroupEntrySet { get; set; }
        public DbSet<ProductInterestRate> ProductInterestRateSet { get; set; }
        public DbSet<ProductPoolRate> ProductPoolRateSet { get; set; }
        public DbSet<ProductSharedRatio> ProductSharedRatioSet { get; set; }
        public DbSet<ProductVolumeBasedRate> ProductVolumeBasedRateSet { get; set; }
        public DbSet<ProductVolumeBasedSetup> ProductVolumeBasedSetupSet { get; set; }
        public DbSet<RevenueCaption> RevenueCaptionSet { get; set; }
        public DbSet<RevenueIncExp> RevenueIncExpSet { get; set; }
        public DbSet<RevenuePool> RevenuePoolSet { get; set; }
        public DbSet<RevenueSetting> RevenueSettingSet { get; set; }
      
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

            //Core
            modelBuilder.Entity<Module>().HasKey<int>(e => e.ModuleId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Module>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Module>().ToTable("bud_module");

            modelBuilder.Entity<BudgetingLevel>().HasKey<int>(e => e.BudgetingLevelId).Ignore(e => e.EntityId);
            modelBuilder.Entity<BudgetingLevel>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BudgetingLevel>().ToTable("bud_budgeting_level");

            modelBuilder.Entity<Currency>().HasKey<int>(e => e.CurrencyId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Currency>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Currency>().ToTable("bud_currency");

            modelBuilder.Entity<CurrencyRate>().HasKey<int>(e => e.CurrencyRateId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CurrencyRate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CurrencyRate>().ToTable("bud_currency_rate");

            modelBuilder.Entity<GeneralSetting>().HasKey<int>(e => e.GeneralSettingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<GeneralSetting>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<GeneralSetting>().ToTable("bud_general_setting");

            modelBuilder.Entity<ModificationLevel>().HasKey<int>(e => e.ModificationLevelId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ModificationLevel>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ModificationLevel>().ToTable("bud_modification_level");

            modelBuilder.Entity<Operation>().HasKey<int>(e => e.OperationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Operation>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Operation>().ToTable("bud_operation");

            modelBuilder.Entity<OperationReview>().HasKey<int>(e => e.OperationReviewId).Ignore(e => e.EntityId);
            modelBuilder.Entity<OperationReview>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OperationReview>().ToTable("bud_operation_review");

            modelBuilder.Entity<Policy>().HasKey<int>(e => e.PolicyId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Policy>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Policy>().ToTable("bud_policy");

            modelBuilder.Entity<PolicyLevel>().HasKey<int>(e => e.PolicyLevelId).Ignore(e => e.EntityId);
            modelBuilder.Entity<PolicyLevel>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PolicyLevel>().ToTable("bud_policy_level");

            modelBuilder.Entity<PrimaryLock>().HasKey<int>(e => e.PrimaryLockId).Ignore(e => e.EntityId);
            modelBuilder.Entity<PrimaryLock>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PrimaryLock>().ToTable("bud_primary_lock");

            modelBuilder.Entity<SecondaryLockLevel>().HasKey<int>(e => e.SecondaryLockLevelId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SecondaryLockLevel>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SecondaryLockLevel>().ToTable("bud_secondary_lock_level");

            modelBuilder.Entity<SecondaryLock>().HasKey<int>(e => e.SecondaryLockId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SecondaryLock>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SecondaryLock>().ToTable("bud_secondary_lock");

            //Team
            modelBuilder.Entity<OfficerDetail>().HasKey<int>(e => e.OfficerDetailId).Ignore(e => e.EntityId);
            modelBuilder.Entity<OfficerDetail>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OfficerDetail>().ToTable("bud_officer_detail");

            modelBuilder.Entity<Team>().HasKey<int>(e => e.TeamId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Team>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Team>().ToTable("bud_team");

            modelBuilder.Entity<TeamClassification>().HasKey<int>(e => e.TeamClassificationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TeamClassification>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamClassification>().ToTable("bud_team_classification");

            modelBuilder.Entity<TeamClassificationType>().HasKey<int>(e => e.TeamClassificationTypeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TeamClassificationType>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamClassificationType>().ToTable("bud_team_classification_type");

            modelBuilder.Entity<TeamDefinition>().HasKey<int>(e => e.TeamDefinitionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TeamDefinition>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamDefinition>().ToTable("bud_team_definition");

            modelBuilder.Entity<TeamSetting>().HasKey<int>(e => e.TeamSettingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TeamSetting>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamSetting>().ToTable("bud_team_setting");

            modelBuilder.Entity<TeamUser>().HasKey<int>(e => e.TeamUserId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TeamUser>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamUser>().ToTable("bud_team_user");

            //Capex
            modelBuilder.Entity<CapexCategory>().HasKey<int>(e => e.CapexCategoryId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CapexCategory>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CapexCategory>().ToTable("bud_capex_category");

            modelBuilder.Entity<CapexCost>().HasKey<int>(e => e.CapexCostId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CapexCost>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CapexCost>().ToTable("bud_capex_cost");

            modelBuilder.Entity<CapexEntry>().HasKey<int>(e => e.CapexEntryId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CapexEntry>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CapexEntry>().ToTable("bud_capex_entry");

            modelBuilder.Entity<CapexItem>().HasKey<int>(e => e.CapexItemId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CapexItem>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CapexItem>().ToTable("bud_capex_item");

            modelBuilder.Entity<DepreciationRate>().HasKey<int>(e => e.DepreciationRateId).Ignore(e => e.EntityId);
            modelBuilder.Entity<DepreciationRate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<DepreciationRate>().ToTable("bud_depreciation_rate");

            //Fee
            modelBuilder.Entity<FeeCalculationType>().HasKey<int>(e => e.FeeCalculationTypeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FeeCalculationType>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FeeCalculationType>().ToTable("bud_fee_calculation_type");

            modelBuilder.Entity<FeeCaption>().HasKey<int>(e => e.FeeCaptionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FeeCaption>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FeeCaption>().ToTable("bud_fee_caption");

            modelBuilder.Entity<FeeCategory>().HasKey<int>(e => e.FeeCategoryId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FeeCategory>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FeeCategory>().ToTable("bud_fee_category");

            modelBuilder.Entity<FeeEntry>().HasKey<int>(e => e.FeeEntryId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FeeEntry>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FeeEntry>().ToTable("bud_fee_entry");

            modelBuilder.Entity<FeeGroup>().HasKey<int>(e => e.FeeGroupId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FeeGroup>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FeeGroup>().ToTable("bud_fee_group");

            modelBuilder.Entity<FeeGroupEntry>().HasKey<int>(e => e.FeeGroupEntryId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FeeGroupEntry>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FeeGroupEntry>().ToTable("bud_fee_group_entry");

            modelBuilder.Entity<FeeItem>().HasKey<int>(e => e.FeeItemId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FeeItem>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FeeItem>().ToTable("bud_fee_item");

            modelBuilder.Entity<FeeMovement>().HasKey<int>(e => e.FeeMovementId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FeeMovement>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FeeMovement>().ToTable("bud_fee_movement");

            modelBuilder.Entity<FeeSharedExemption>().HasKey<int>(e => e.FeeSharedExemptionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FeeSharedExemption>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FeeSharedExemption>().ToTable("bud_fee_shared_exemption");

            modelBuilder.Entity<FeeSharedRatio>().HasKey<int>(e => e.FeeSharedRatioId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FeeSharedRatio>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FeeSharedRatio>().ToTable("bud_fee_shared_ratio");

            modelBuilder.Entity<FeeVolumeBasedRate>().HasKey<int>(e => e.FeeVolumeBasedRateId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FeeVolumeBasedRate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FeeVolumeBasedRate>().ToTable("bud_fee_volume_based_rate");

            modelBuilder.Entity<FeeVolumeBasedSetup>().HasKey<int>(e => e.FeeVolumeBasedSetupId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FeeVolumeBasedSetup>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FeeVolumeBasedSetup>().ToTable("bud_fee_volume_based_setup");

            //Opex
            modelBuilder.Entity<OpexCategory>().HasKey<int>(e => e.OpexCategoryId).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexCategory>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexCategory>().ToTable("bud_opex_category");

            modelBuilder.Entity<OpexEntry>().HasKey<int>(e => e.OpexEntryId).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexEntry>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexEntry>().ToTable("bud_opex_entry");

            modelBuilder.Entity<OpexItem>().HasKey<int>(e => e.OpexItemId).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexItem>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexItem>().ToTable("bud_opex_item");

            modelBuilder.Entity<OpexVolumeBasedRate>().HasKey<int>(e => e.OpexVolumeBasedRateId).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexVolumeBasedRate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexVolumeBasedRate>().ToTable("bud_opex_volume_based_rate");

            modelBuilder.Entity<OpexVolumeBasedSetup>().HasKey<int>(e => e.OpexVolumeBasedSetupId).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexVolumeBasedSetup>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexVolumeBasedSetup>().ToTable("bud_opex_volume_based_setup");

            //StaffCost
            modelBuilder.Entity<Grade>().HasKey<int>(e => e.GradeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Grade>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Grade>().ToTable("bud_grade");

            modelBuilder.Entity<PayClassification>().HasKey<int>(e => e.PayClassificationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<PayClassification>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PayClassification>().ToTable("bud_pay_classification");

            modelBuilder.Entity<PayStructure>().HasKey<int>(e => e.PayStructureId).Ignore(e => e.EntityId);
            modelBuilder.Entity<PayStructure>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PayStructure>().ToTable("bud_pay_structure");

            modelBuilder.Entity<StaffCost>().HasKey<int>(e => e.StaffCostId).Ignore(e => e.EntityId);
            modelBuilder.Entity<StaffCost>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<StaffCost>().ToTable("bud_staff_cost");

            modelBuilder.Entity<StaffCount>().HasKey<int>(e => e.StaffCountId).Ignore(e => e.EntityId);
            modelBuilder.Entity<StaffCount>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<StaffCount>().ToTable("bud_staff_count");

            modelBuilder.Entity<TeamPayClassification>().HasKey<int>(e => e.TeamPayClassificationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TeamPayClassification>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamPayClassification>().ToTable("bud_team_pay_classification");

            //Revenue
            modelBuilder.Entity<Product>().HasKey<int>(e => e.ProductId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Product>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Product>().ToTable("bud_product");

            modelBuilder.Entity<ProductCategory>().HasKey<int>(e => e.ProductCategoryId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ProductCategory>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ProductCategory>().ToTable("bud_product_category");

            modelBuilder.Entity<ProductClassification>().HasKey<int>(e => e.ProductClassificationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ProductClassification>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ProductClassification>().ToTable("bud_product_classification");

            modelBuilder.Entity<ProductGroup>().HasKey<int>(e => e.ProductGroupId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ProductGroup>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ProductGroup>().ToTable("bud_product_group");

            modelBuilder.Entity<ProductGroupEntry>().HasKey<int>(e => e.ProductGroupEntryId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ProductGroupEntry>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ProductGroupEntry>().ToTable("bud_product_group_entry");

            modelBuilder.Entity<ProductInterestRate>().HasKey<int>(e => e.ProductInterestRateId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ProductInterestRate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ProductInterestRate>().ToTable("bud_product_interest_rate");

            modelBuilder.Entity<ProductPoolRate>().HasKey<int>(e => e.ProductPoolRateId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ProductPoolRate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ProductPoolRate>().ToTable("bud_product_pool_rate");

            modelBuilder.Entity<ProductSharedRatio>().HasKey<int>(e => e.ProductSharedRatioId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ProductSharedRatio>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ProductSharedRatio>().ToTable("bud_product_shared_ratio");

            modelBuilder.Entity<ProductVolumeBasedRate>().HasKey<int>(e => e.ProductVolumeBasedRateId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ProductVolumeBasedRate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ProductVolumeBasedRate>().ToTable("bud_product_volume_based_rate");

            modelBuilder.Entity<ProductVolumeBasedSetup>().HasKey<int>(e => e.ProductVolumeBasedSetupId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ProductVolumeBasedSetup>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ProductVolumeBasedSetup>().ToTable("bud_product_volume_based_setup");

            modelBuilder.Entity<RevenueCaption>().HasKey<int>(e => e.RevenueCaptionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<RevenueCaption>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<RevenueCaption>().ToTable("bud_revenue_caption");

            modelBuilder.Entity<RevenueIncExp>().HasKey<int>(e => e.RevenueIncExpId).Ignore(e => e.EntityId);
            modelBuilder.Entity<RevenueIncExp>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<RevenueIncExp>().ToTable("bud_revenue_incexp");

            modelBuilder.Entity<RevenuePool>().HasKey<int>(e => e.RevenuePoolId).Ignore(e => e.EntityId);
            modelBuilder.Entity<RevenuePool>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<RevenuePool>().ToTable("bud_revenue_pool");

            modelBuilder.Entity<RevenueSetting>().HasKey<int>(e => e.RevenueSettingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<RevenueSetting>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<RevenueSetting>().ToTable("bud_revenue_setting");
        }

        public override int SaveChanges()
        {
            try
            {
                if (ChangeTracker.HasChanges())
                {
                    var entries = this.ChangeTracker.Entries();

                    foreach (DbEntityEntry entry in entries)
                    {
                        if (entry.Entity != null)
                        {
                            if (entry.State == EntityState.Added)
                            {
                                //entry is Added 

                                var model = (EntityBase)entry.Entity;
                                model.CreatedBy = DataConnector.LoginName;
                                model.CreatedOn = DateTime.Now;
                                model.UpdatedBy = DataConnector.LoginName;
                                model.UpdatedOn = DateTime.Now;
                            }
                            else if (entry.State == EntityState.Deleted)
                            {
                                //entry in deleted

                            }
                            else
                            {
                                //entry is modified
                                var model = (EntityBase)entry.Entity;
                                model.UpdatedBy = DataConnector.LoginName;
                                model.UpdatedOn = DateTime.Now;
                            }

                            _auditManager.AddAudit(entry, DataConnector.LoginName);
                        }
                    }
                }

                _auditManager.Save();

                return base.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                var innerEx = e.InnerException;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;

                throw new Exception(innerEx.Message);
            }
            catch (DbEntityValidationException e)
            {
                var sb = new StringBuilder();

                foreach (var entry in e.EntityValidationErrors)
                {
                    foreach (var error in entry.ValidationErrors)
                    {
                        sb.AppendLine(string.Format("{0}-{1}-{2}", entry.Entry.Entity, error.PropertyName, error.ErrorMessage));
                    }
                }

                throw new Exception(sb.ToString());
            }
            catch (Exception e)
            {
                var innerEx = e.InnerException;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;

                throw new Exception(innerEx.Message);
            }

        }

        public static string GetDataConnection()
        {
            string connectionString = "";

            if (!string.IsNullOrEmpty(DataConnector.CompanyCode) && !string.IsNullOrEmpty(SOLUTION_NAME))
            {
                systemContract.IDatabaseRepository databaseRepository = new systemCore.DatabaseRepository();
                var companydbs = databaseRepository.GetDatabases().Where(c => c.Database.CompanyCode == DataConnector.CompanyCode && (c.Solution.Name == SOLUTION_NAME || c.Solution.Name == "CORE"));

                DatabaseInfo companydb = null;

                if (companydbs == null)
                    throw new Exception("Unable to load database.");
                else
                {
                    companydb = companydbs.Where(c => c.Solution.Name == SOLUTION_NAME).FirstOrDefault();

                    if (companydb == null)
                        companydb = companydbs.FirstOrDefault();
                }

                //connectionString="Data Source=10.0.0.18\FintrakSQL2014;Initial Catalog=FintrakDB;User =sa;Password=sqluser10$;Integrated Security=False"
                connectionString = string.Format("Data Source= {0};Initial Catalog={1};User ={2};Password={3};Integrated Security={4}", companydb.Database.ServerName, companydb.Database.DatabaseName, companydb.Database.UserName, companydb.Database.Password, companydb.Database.IntegratedSecurity);
            }

            return connectionString;
        }

    }
}
