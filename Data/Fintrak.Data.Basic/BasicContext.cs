using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Data;

using systemContract = Fintrak.Data.SystemCore.Contracts;
using systemCore = Fintrak.Data.SystemCore;
using Fintrak.Data.SystemCore.Contracts;
using DecryptOperation;

namespace Fintrak.Data.Basic
{
    public class BasicContext : DbContext
    {
        const string SOLUTION_NAME = "CORE";

        //public BasicContext()
        //    : base("FintrakDBConnection")
        //{
        //    System.Data.Entity.Database.SetInitializer<BasicContext>(null);
        //}

        public BasicContext()
            : base(GetDataConnection())
        {
            System.Data.Entity.Database.SetInitializer<BasicContext>(null);
        }

        //IFRS 
        public DbSet<DerivedCaption> DerivedCaptionSet { get; set; }
        public DbSet<GLMapping> GLMappingSet { get; set; }
        public DbSet<InstrumentType> InstrumentTypeSet { get; set; }
        public DbSet<GLType> GLTypeSet { get; set; }
        public DbSet<InstrumentTypeGLMap> InstrumentTypeGLMapSet { get; set; }
        public DbSet<AutoPostingTemplate> AutoPostingTemplateSet { get; set; }
        public DbSet<TrialBalanceGap> TrialBalanceGapSet { get; set; }
        public DbSet<GLAdjustment> GLAdjustmentSet { get; set; }
        public DbSet<PostingDetail> PostingDetailSet { get; set; }
        public DbSet<TrialBalance> TrialBalanceSet { get; set; }
        public DbSet<IFRSReport> IFRSReportSet { get; set; }
        public DbSet<TransactionDetail> TransactionDetailSet { get; set; }
        public DbSet<IFRSRegistry> IFRSRegistrySet { get; set; }
        public DbSet<LoanSetup> LoanSetupSet { get; set; }
        public DbSet<ScheduleType> ScheduleTypeSet { get; set; }
        public DbSet<IFRSProduct> IFRSProductSet { get; set; }
        public DbSet<CreditRiskRating> CreditRiskRatingSet { get; set; }
        public DbSet<CollateralCategory> CollateralCategorySet { get; set; }
        public DbSet<CollateralType> CollateralTypeSet { get; set; }
        public DbSet<CollateralRealizationPeriod> CollateralRealizationPeriodSet { get; set; }
        public DbSet<CollateralInformation> CollateralInformationSet { get; set; }
        public DbSet<WatchListedLoan> WatchListedLoanSet { get; set; }
        public DbSet<ImpairmentOverride> ImpairmentOverrideSet { get; set; }
        public DbSet<FairValueBasisMap> FairValueBasisMapSet { get; set; }
        public DbSet<FairValueBasisExemption> FairValueBasisExemptionSet { get; set; }
        public DbSet<BondComputation> BondComputationSet { get; set; }
        public DbSet<BondComputationResultZero> BondComputationResultZeroSet { get; set; }
        public DbSet<BondPeriodicSchedule> BondPeriodicScheduleSet { get; set; }
        public DbSet<EquityStockComputationResult> EquityStockComputationResultSet { get; set; }
        public DbSet<LoanPeriodicSchedule> LoanPeriodicScheduleSet { get; set; }
        public DbSet<LoanSchedule> LoanScheduleSet { get; set; }
        public DbSet<LoansImpairmentResult> LoansImpairmentResultSet { get; set; }
        public DbSet<TBillsComputationResult> TBillsComputationResultSet { get; set; }
        public DbSet<IFRSBudget> IFRSBudgetSet { get; set; }
        public DbSet<LoanPrimaryData> LoanPrimaryDataSet { get; set; }
        public DbSet<LoanDetails> LoanDetailsSet { get; set; }
        public DbSet<IndividualImpairment> IndividualImpairmentSet { get; set; }
        public DbSet<IndividualSchedule> IndividualScheduleSet { get; set; }
        public DbSet<IntegralFee> IntegralFeeSet { get; set; }
        public DbSet<LoanIRRData> LoanIRRDataSet { get; set; }

        //MPR
        //public DbSet<UserMIS> UserMISSet { get; set; }
        //public DbSet<TeamDefinition> TeamDefinitionSet { get; set; }
        //public DbSet<TeamClassificationType> TeamClassificationTypeSet { get; set; }
        //public DbSet<TeamClassification> TeamClassificationSet { get; set; }
        //public DbSet<Team> TeamSet { get; set; }
        //public DbSet<TeamClassificationMap> TeamClassificationMapSet { get; set; }
        //public DbSet<AccountOfficerDetail> AccountOfficerDetailSet { get; set; }
        //public DbSet<BranchDefaultMIS> BranchDefaultMISSet { get; set; }
        //public DbSet<ManagementTree> ManagementTreeSet { get; set; }
        //public DbSet<AccountMIS> AccountMISSet { get; set; }
        //public DbSet<MISReplacement> MISReplacementSet { get; set; }
        //public DbSet<SetUp> SetUpSet { get; set; }
        //public DbSet<BSCaption> BSCaptionSet { get; set; }
        //public DbSet<MPRProduct> MPRProductSet { get; set; }
        //public DbSet<NonProductMap> NonProductMapSet { get; set; }
        //public DbSet<NonProductRate> NonProductRateSet { get; set; }
        //public DbSet<ProductMIS> ProductMISSet { get; set; }
        //public DbSet<BalanceSheetThreshold> BalanceSheetThresholdSet { get; set; }
        //public DbSet<AccountTransferPrice> AccountTransferPriceSet { get; set; }
        //public DbSet<TransferPrice> TransferPriceSet { get; set; }
        //public DbSet<GeneralTransferPrice> GeneralTransferPriceSet { get; set; }
        //public DbSet<BalanceSheetBudget> BalanceSheetBudgetSet { get; set; }
        //public DbSet<BalanceSheetBudgetOfficer> BalanceSheetBudgetOfficerSet { get; set; }
        //public DbSet<BSGLMapping> BSGLMappingSet { get; set; }
        //public DbSet<MPRBalanceSheetAdjustment> MPRBalanceSheetAdjustmentSet { get; set; }
        //public DbSet<MPRBalanceSheet> MPRBalanceSheetSet { get; set; }
        //public DbSet<CustAccount> CustAccountSet { get; set; }
        //public DbSet<MemoAccountMap> MemoAccountMapSet { get; set; }
        //public DbSet<MemoGLMap> MemoGLMapSet { get; set; }
        //public DbSet<MemoProductMap> MemoProductMapSet { get; set; }
        //public DbSet<MemoUnits> MemoUnitsSet { get; set; }
        //public DbSet<BSExemption> BSExemptionSet { get; set; }

        //MPR_PL
        //public DbSet<PLCaption> PLCaptionSet { get; set; }
        //public DbSet<MPRGLMapping> MPRGLMappingSet { get; set; }
        //public DbSet<GLReclassification> GLReclassificationSet { get; set; }
        //public DbSet<GLException> GLExceptionSet { get; set; }
        //public DbSet<MPRTotalLine> MPRTotalLineSet { get; set; }
        //public DbSet<MPRTotalLineMakeUp> MPRTotalLineMakeUpSet { get; set; }
        //public DbSet<GLMIS> GLMISSet { get; set; }
        //public DbSet<PLIncomeReport> PLIncomeReportSet { get; set; }
        //public DbSet<PLIncomeReportAdjustment> PLIncomeReportAdjustmentSet { get; set; }
        //public DbSet<RevenueBudget> RevenueBudgetSet { get; set; }
        //public DbSet<RevenueBudgetOfficer> RevenueBudgetOfficerSet { get; set; }
        //public DbSet<Revenue> RevenueSet { get; set; }
        //public DbSet<MPRPLDerivedCaption> MPRPLDerivedCaptionSet { get; set; }

        ////MPR_OPEX
        //public DbSet<CostCentre> CostCentreSet { get; set; }
        //public DbSet<CostCentreDefinition> CostCentreDefinitionSet { get; set; }
        //public DbSet<ExpenseBasis> ExpenseBasisSet { get; set; }
        //public DbSet<ExpenseMapping> ExpenseMappingSet { get; set; }
        //public DbSet<ExpenseProductMapping> ExpenseProductMappingSet { get; set; }
        //public DbSet<ExpenseGLMapping> ExpenseGLMappingSet { get; set; }
        //public DbSet<ExpenseRawBasis> ExpenseRawBasisSet { get; set; }
        //public DbSet<StaffCost> StaffCostSet { get; set; }
        //public DbSet<OpexManagementTree> OpexManagementTreeSet { get; set; }
        //public DbSet<ActivityBase> ActivityBaseSet { get; set; }
        //public DbSet<ActivityBaseRatio> ActivityBaseRatioSet { get; set; }
        //public DbSet<OpexMISReplacement> OpexMISReplacementSet { get; set; }
        //public DbSet<OpexBusinessRule> OpexBusinessRuleSet { get; set; }
        //public DbSet<OpexAbcExemption> OpexAbcExemptionSet { get; set; }
        //public DbSet<OpexRawExpense> OpexRawExpenseSet { get; set; }
        //public DbSet<OpexGLMapping> OpexGLMappingSet { get; set; }
        //public DbSet<OpexReport> OpexReportSet { get; set; }
        //public DbSet<OpexGLBasis> OpexGLBasisSet { get; set; }
        //public DbSet<OpexBasisMapping> OpexBasisMappingSet { get; set; }

        ////Score Card
        //public DbSet<SCDConfiguration> SCDConfigurationSet { get; set; }

        public DbSet<ChartOfAccount> ChartOfAccountSet { get; set; }
        public DbSet<Currency> CurrencySet { get; set; }
        public DbSet<FiscalYear> FiscalYearSet { get; set; }
        public DbSet<Branch> BranchSet { get; set; }
        public DbSet<Product> ProductSet { get; set; }
        public DbSet<FiscalPeriod> FiscalPeriodSet { get; set; }
        public DbSet<FinancialType> FinancialTypeSet { get; set; }
        public DbSet<GLDefinition> GLDefinitionSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

            //Basic


            //IFRS
            //DerivedCaption
            modelBuilder.Entity<DerivedCaption>().HasKey<int>(e => e.DerivedCaptionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<DerivedCaption>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<DerivedCaption>().ToTable("ifrs_derivedcaption");

            //GLMapping
            modelBuilder.Entity<GLMapping>().HasKey<int>(e => e.GLMappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<GLMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<GLMapping>().ToTable("ifrs_glmapping");

            //InstrumentType
            modelBuilder.Entity<InstrumentType>().HasKey<int>(e => e.InstrumentTypeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<InstrumentType>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<InstrumentType>().ToTable("ifrs_instrumentType");

            //GLType
            modelBuilder.Entity<GLType>().HasKey<int>(e => e.GLTypeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<GLType>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<GLType>().ToTable("ifrs_gltype");

            //InstrumentTypeGLMap
            modelBuilder.Entity<InstrumentTypeGLMap>().HasKey<int>(e => e.InstrumentTypeGLMapId).Ignore(e => e.EntityId);
            modelBuilder.Entity<InstrumentTypeGLMap>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<InstrumentTypeGLMap>().ToTable("ifrs_instrumenttypeglmap");

            //AutoPostingTemplate
            modelBuilder.Entity<AutoPostingTemplate>().HasKey<int>(e => e.AutoPostingTemplateId).Ignore(e => e.EntityId);
            modelBuilder.Entity<AutoPostingTemplate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<AutoPostingTemplate>().ToTable("ifrs_autopostingtemplate");

            //TrialBalanceGap
            modelBuilder.Entity<TrialBalanceGap>().HasKey<int>(e => e.TrialBalanceGAPId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TrialBalanceGap>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TrialBalanceGap>().ToTable("ifrs_trialbalancegap");

            //GLAdjustment
            modelBuilder.Entity<GLAdjustment>().HasKey<int>(e => e.GLAdjustmentId).Ignore(e => e.EntityId);
            modelBuilder.Entity<GLAdjustment>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<GLAdjustment>().ToTable("ifrs_gladjustment");

            //PostingDetail
            modelBuilder.Entity<PostingDetail>().HasKey<int>(e => e.PostingDetailId).Ignore(e => e.EntityId);
            modelBuilder.Entity<PostingDetail>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PostingDetail>().ToTable("ifrs_postingdetail");

            //TrialBalance
            modelBuilder.Entity<TrialBalance>().HasKey<int>(e => e.TrialBalanceId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TrialBalance>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TrialBalance>().ToTable("ifrs_trialbalance");

            //IFRSReport
            modelBuilder.Entity<IFRSReport>().HasKey<int>(e => e.IFRSReportId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IFRSReport>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IFRSReport>().ToTable("ifrs_report");

            //TransactionDetail
            modelBuilder.Entity<TransactionDetail>().HasKey<int>(e => e.TransactionDetailId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TransactionDetail>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TransactionDetail>().ToTable("ifrs_transactiondetail");

            //IFRSRegistry
            modelBuilder.Entity<IFRSRegistry>().HasKey<int>(e => e.RegistryId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IFRSRegistry>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IFRSRegistry>().ToTable("ifrs_registry");

            //LoanSetup
            modelBuilder.Entity<LoanSetup>().HasKey<int>(e => e.LoanSetupId).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanSetup>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanSetup>().ToTable("ifrs_loan_setup");

            //ScheduleType
            modelBuilder.Entity<ScheduleType>().HasKey<int>(e => e.ScheduleTypeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ScheduleType>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ScheduleType>().ToTable("ifrs_schedule_type");

            //IFRSProduct
            modelBuilder.Entity<IFRSProduct>().HasKey<int>(e => e.ProductId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IFRSProduct>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IFRSProduct>().ToTable("ifrs_product");

            //CreditRiskRating
            modelBuilder.Entity<CreditRiskRating>().HasKey<int>(e => e.CreditRiskRatingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CreditRiskRating>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CreditRiskRating>().ToTable("ifrs_credit_risk_rating");

            // CollateralCategory
            modelBuilder.Entity<CollateralCategory>().HasKey<int>(e => e.CollateralCategoryId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CollateralCategory>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CollateralCategory>().ToTable("ifrs_collateral_category");

            //CollateralType
            modelBuilder.Entity<CollateralType>().HasKey<int>(e => e.CollateralTypeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CollateralType>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CollateralType>().ToTable("ifrs_collateral_type");

            //CollateralRealizationPeriod
            modelBuilder.Entity<CollateralRealizationPeriod>().HasKey<int>(e => e.CollateralRealizationPeriodId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CollateralRealizationPeriod>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CollateralRealizationPeriod>().ToTable("ifrs_collateral_realization_period");

            //CollateralInformation
            modelBuilder.Entity<CollateralInformation>().HasKey<int>(e => e.CollateralInformationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CollateralInformation>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CollateralInformation>().ToTable("ifrs_collateral_information");

            //WatchlistedLoan
            modelBuilder.Entity<WatchListedLoan>().HasKey<int>(e => e.WatchListedLoanId).Ignore(e => e.EntityId);
            modelBuilder.Entity<WatchListedLoan>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<WatchListedLoan>().ToTable("ifrs_watchlisted_loan");

            //ImpairmentOverride
            modelBuilder.Entity<ImpairmentOverride>().HasKey<int>(e => e.ImpairmentOverrideId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ImpairmentOverride>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ImpairmentOverride>().ToTable("ifrs_impairment_override");

            //FairValueBasisMap
            modelBuilder.Entity<FairValueBasisMap>().HasKey<int>(e => e.FairValueBasisMapId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FairValueBasisMap>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FairValueBasisMap>().ToTable("ifrs_fair_value_basis_map");

            //FairValueBasisExemption
            modelBuilder.Entity<FairValueBasisExemption>().HasKey<int>(e => e.FairValueBasisExemptionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FairValueBasisExemption>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FairValueBasisExemption>().ToTable("ifrs_fair_value_basis_exemption");

            //BondComputation
            modelBuilder.Entity<BondComputation>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<BondComputation>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BondComputation>().ToTable("ifrs_bond_computation_result");

            //BondComputationResultZero
            modelBuilder.Entity<BondComputationResultZero>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<BondComputationResultZero>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BondComputationResultZero>().ToTable("ifrs_bond_computation_result_zero");

            //BondPeriodicSchedule
            modelBuilder.Entity<BondPeriodicSchedule>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<BondPeriodicSchedule>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BondPeriodicSchedule>().ToTable("ifrs_bond_periodic_schedule");

            //EquityStockComputationResult
            modelBuilder.Entity<EquityStockComputationResult>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<EquityStockComputationResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<EquityStockComputationResult>().ToTable("ifrs_equity_stock_computation_result");

            //LoanPeriodicSchedule
            modelBuilder.Entity<LoanPeriodicSchedule>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanPeriodicSchedule>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanPeriodicSchedule>().ToTable("ifrs_loan_periodic_schedule");

            //LoanSchedule
            modelBuilder.Entity<LoanSchedule>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanSchedule>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanSchedule>().ToTable("ifrs_loan_schedule");

            //LoansImpairmentResult
            modelBuilder.Entity<LoansImpairmentResult>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoansImpairmentResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoansImpairmentResult>().ToTable("ifrs_loans_impairment_result");

            //TBillsComputationResult
            modelBuilder.Entity<TBillsComputationResult>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<TBillsComputationResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TBillsComputationResult>().ToTable("ifrs_tbills_computation_result");

            //IFRSBudget
            modelBuilder.Entity<IFRSBudget>().HasKey<int>(e => e.IFRSBudgetId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IFRSBudget>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IFRSBudget>().ToTable("ifrs_budget");

            //LoanPrimaryData
            modelBuilder.Entity<LoanPrimaryData>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanPrimaryData>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanPrimaryData>().ToTable("ifrs_loan_primary_data");

            //LoanDetails
            modelBuilder.Entity<LoanDetails>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanDetails>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanDetails>().ToTable("ifrs_loans_details");

            //IndividualImpairment
            modelBuilder.Entity<IndividualImpairment>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IndividualImpairment>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IndividualImpairment>().ToTable("ifrs_individual_impairment");

            //IndividualSchedule
            modelBuilder.Entity<IndividualSchedule>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IndividualSchedule>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IndividualSchedule>().ToTable("ifrs_individual_schedule");

            //IntegralFee
            modelBuilder.Entity<IntegralFee>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IntegralFee>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IntegralFee>().ToTable("ifrs_integral_fee");

            //LoanIRRData
            modelBuilder.Entity<LoanIRRData>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanIRRData>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanIRRData>().ToTable("ifrs_loan_irr_data");

            ////MPR
            ////TeamDefinition
            //modelBuilder.Entity<UserMIS>().HasKey<int>(e => e.UserMISId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<UserMIS>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<UserMIS>().ToTable("mpr_usermis");

            ////TeamDefinition
            //modelBuilder.Entity<TeamDefinition>().HasKey<int>(e => e.TeamDefinitionId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<TeamDefinition>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<TeamDefinition>().ToTable("mpr_team_definition");

            ////TeamClassificationType
            //modelBuilder.Entity<TeamClassificationType>().HasKey<int>(e => e.TeamClassificationTypeId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<TeamClassificationType>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<TeamClassificationType>().ToTable("mpr_team_classification_type");

            ////TeamClassification
            //modelBuilder.Entity<TeamClassification>().HasKey<int>(e => e.TeamClassificationId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<TeamClassification>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<TeamClassification>().ToTable("mpr_team_classification");

            ////Team
            //modelBuilder.Entity<Team>().HasKey<int>(e => e.TeamId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<Team>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<Team>().ToTable("mpr_team");

            ////TeamClassificationMap
            //modelBuilder.Entity<TeamClassificationMap>().HasKey<int>(e => e.TeamClassificationMapId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<TeamClassificationMap>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<TeamClassificationMap>().ToTable("mpr_team_classification_map");

            ////AccountOfficerDetail
            //modelBuilder.Entity<AccountOfficerDetail>().HasKey<int>(e => e.AccountOfficerDetailId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<AccountOfficerDetail>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<AccountOfficerDetail>().ToTable("mpr_accountofficer_detail");

            ////BranchDefaultMIS
            //modelBuilder.Entity<BranchDefaultMIS>().HasKey<int>(e => e.BranchDefaultMISId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<BranchDefaultMIS>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<BranchDefaultMIS>().ToTable("mpr_branchdefaultmis");

            ////ManagementTree
            //modelBuilder.Entity<ManagementTree>().HasKey<int>(e => e.ManagementTreeId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<ManagementTree>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<ManagementTree>().ToTable("mpr_managementtree");

            ////AccountMIS
            //modelBuilder.Entity<AccountMIS>().HasKey<int>(e => e.AccountMISId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<AccountMIS>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<AccountMIS>().ToTable("mpr_accountmis");

            ////MISReplacement
            //modelBuilder.Entity<MISReplacement>().HasKey<int>(e => e.MISReplacementId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<MISReplacement>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<MISReplacement>().ToTable("mpr_misreplacement");

            ////SetUp
            //modelBuilder.Entity<SetUp>().HasKey<int>(e => e.SetupId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<SetUp>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<SetUp>().ToTable("mpr_setup");

            ////BSCaption
            //modelBuilder.Entity<BSCaption>().HasKey<int>(e => e.CaptionId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<BSCaption>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<BSCaption>().ToTable("mpr_bs_caption");

            ////MPRProduct
            //modelBuilder.Entity<MPRProduct>().HasKey<int>(e => e.ProductId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<MPRProduct>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<MPRProduct>().ToTable("mpr_product");

            ////NonProductMap
            //modelBuilder.Entity<NonProductMap>().HasKey<int>(e => e.NonProductMapId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<NonProductMap>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<NonProductMap>().ToTable("mpr_non_product_map");

            ////NonProductRate
            //modelBuilder.Entity<NonProductRate>().HasKey<int>(e => e.NonProductRateId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<NonProductRate>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<NonProductRate>().ToTable("mpr_non_product_rate");

            ////ProductMIS
            //modelBuilder.Entity<ProductMIS>().HasKey<int>(e => e.ProductMISId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<ProductMIS>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<ProductMIS>().ToTable("mpr_productmis");

            ////BalanceSheetThreshold
            //modelBuilder.Entity<BalanceSheetThreshold>().HasKey<int>(e => e.BalanceSheetThresholdId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<BalanceSheetThreshold>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<BalanceSheetThreshold>().ToTable("mpr_balancesheet_threshold");

            ////AccountTransferPrice
            //modelBuilder.Entity<AccountTransferPrice>().HasKey<int>(e => e.AccountTransferPriceId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<AccountTransferPrice>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<AccountTransferPrice>().ToTable("mpr_account_transfer_price");

            ////TransferPrice
            //modelBuilder.Entity<TransferPrice>().HasKey<int>(e => e.TransferPriceId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<TransferPrice>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<TransferPrice>().ToTable("mpr_transfer_price");

            ////GeneralTransferPrice
            //modelBuilder.Entity<GeneralTransferPrice>().HasKey<int>(e => e.GeneralTransferPriceId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<GeneralTransferPrice>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<GeneralTransferPrice>().ToTable("mpr_general_transfer_price");

            ////BalanceSheetBudget
            //modelBuilder.Entity<BalanceSheetBudget>().HasKey<int>(e => e.BudgetId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<BalanceSheetBudget>().Property(c => c.BudgetId).HasColumnName("BalanceSheetBudgetId");
            //modelBuilder.Entity<BalanceSheetBudget>().Property(c => c.MisCode).HasColumnName("TeamCode");
            //modelBuilder.Entity<BalanceSheetBudget>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<BalanceSheetBudget>().ToTable("mpr_balancesheet_budget");

            ////BalanceSheetBudgetOfficer
            //modelBuilder.Entity<BalanceSheetBudgetOfficer>().HasKey<int>(e => e.BudgetId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<BalanceSheetBudgetOfficer>().Property(c => c.BudgetId).HasColumnName("BalanceSheetBudgetOffId");
            //modelBuilder.Entity<BalanceSheetBudgetOfficer>().Property(c => c.MisCode).HasColumnName("AccountOfficerCode");
            //modelBuilder.Entity<BalanceSheetBudgetOfficer>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<BalanceSheetBudgetOfficer>().ToTable("mpr_balancesheet_budget_officer");

            ////BSGLMapping
            //modelBuilder.Entity<BSGLMapping>().HasKey<int>(e => e.BSGLMappingId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<BSGLMapping>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<BSGLMapping>().ToTable("mpr_bs_gl_mapping");

            ////MPRBalanceSheetAdjustment
            //modelBuilder.Entity<MPRBalanceSheetAdjustment>().HasKey<int>(e => e.BalancesheetAdjustmentId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<MPRBalanceSheetAdjustment>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<MPRBalanceSheetAdjustment>().ToTable("mpr_balancesheet_adjustment");

            ////MPRBalanceSheet
            //modelBuilder.Entity<MPRBalanceSheet>().HasKey<int>(e => e.BalanceSheetId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<MPRBalanceSheet>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<MPRBalanceSheet>().ToTable("mpr_balancesheet");

            ////CustAccount
            //modelBuilder.Entity<CustAccount>().HasKey<int>(e => e.CustAccountId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<CustAccount>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<CustAccount>().ToTable("cor_cust_account");

            ////MemoAccountMap
            //modelBuilder.Entity<MemoAccountMap>().HasKey<int>(e => e.MemoAccountMapId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<MemoAccountMap>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<MemoAccountMap>().ToTable("mpr_memo_account_map");

            ////MemoGLMap
            //modelBuilder.Entity<MemoGLMap>().HasKey<int>(e => e.MemoGLMapId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<MemoGLMap>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<MemoGLMap>().ToTable("mpr_memo_gl_map");

            ////MemoProductMap
            //modelBuilder.Entity<MemoProductMap>().HasKey<int>(e => e.MemoProductMapId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<MemoProductMap>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<MemoProductMap>().ToTable("mpr_memo_product_map");

            ////MemoUnits
            //modelBuilder.Entity<MemoUnits>().HasKey<int>(e => e.MemoUnitsId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<MemoUnits>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<MemoUnits>().ToTable("mpr_memo_units");

            ////BSExemption
            //modelBuilder.Entity<BSExemption>().HasKey<int>(e => e.BSExemptionId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<BSExemption>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<BSExemption>().ToTable("mpr_bs_exemption");

            ////MPR_PL
            ////PLCaption
            //modelBuilder.Entity<PLCaption>().HasKey<int>(e => e.PLCaptionId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<PLCaption>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<PLCaption>().ToTable("mpr_pl_caption");

            ////MPRGLMapping
            //modelBuilder.Entity<MPRGLMapping>().HasKey<int>(e => e.MPRGLMappingId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<MPRGLMapping>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<MPRGLMapping>().ToTable("mpr_gl_mapping");

            ////GLReclassification
            //modelBuilder.Entity<GLReclassification>().HasKey<int>(e => e.GLReclassificationId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<GLReclassification>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<GLReclassification>().ToTable("mpr_gl_reclassification");

            ////GLException
            //modelBuilder.Entity<GLException>().HasKey<int>(e => e.GLExceptionId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<GLException>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<GLException>().ToTable("mpr_gl_exception");

            ////MPRTotalLine
            //modelBuilder.Entity<MPRTotalLine>().HasKey<int>(e => e.TotallineId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<MPRTotalLine>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<MPRTotalLine>().ToTable("mpr_totalline");

            ////MPRTotalLineMakeUp
            //modelBuilder.Entity<MPRTotalLineMakeUp>().HasKey<int>(e => e.TotalLineMakeUpId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<MPRTotalLineMakeUp>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<MPRTotalLineMakeUp>().ToTable("mpr_totalline_makeup");

            ////GLMIS
            //modelBuilder.Entity<GLMIS>().HasKey<int>(e => e.GlMisId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<GLMIS>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<GLMIS>().ToTable("mpr_glmis");

            ////PLIncomeReport
            //modelBuilder.Entity<PLIncomeReport>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            //modelBuilder.Entity<PLIncomeReport>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<PLIncomeReport>().ToTable("mpr_pl_income_report");

            ////PLIncomeReportAdjustment
            //modelBuilder.Entity<PLIncomeReportAdjustment>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            //modelBuilder.Entity<PLIncomeReportAdjustment>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<PLIncomeReportAdjustment>().ToTable("mpr_pl_income_report_adjustment");

            ////RevenueBudget
            //modelBuilder.Entity<RevenueBudget>().HasKey<int>(e => e.BudgetId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<RevenueBudget>().Property(c => c.BudgetId).HasColumnName("RevenueBudgetId");
            //modelBuilder.Entity<RevenueBudget>().Property(c => c.MisCode).HasColumnName("TeamCode");
            //modelBuilder.Entity<RevenueBudget>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<RevenueBudget>().ToTable("mpr_revenue_budget");

            ////RevenueBudgetOfficer
            //modelBuilder.Entity<RevenueBudgetOfficer>().HasKey<int>(e => e.BudgetId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<RevenueBudgetOfficer>().Property(c => c.BudgetId).HasColumnName("RevenueBudgetOffId");
            //modelBuilder.Entity<RevenueBudgetOfficer>().Property(c => c.MisCode).HasColumnName("AccountOfficerCode");
            //modelBuilder.Entity<RevenueBudgetOfficer>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<RevenueBudgetOfficer>().ToTable("mpr_revenue_budget_officer");

            ////Revenue
            //modelBuilder.Entity<Revenue>().HasKey<int>(e => e.RevenueId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<Revenue>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<Revenue>().ToTable("mpr_revenue");

            ////MPRPLDerivedCaption
            //modelBuilder.Entity<MPRPLDerivedCaption>().HasKey<int>(e => e.PLDerivedCaptionId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<MPRPLDerivedCaption>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<MPRPLDerivedCaption>().ToTable("mpr_pl_derived_caption");

            ////MPR_OPEX
            ////CostCentre
            //modelBuilder.Entity<CostCentre>().HasKey<int>(e => e.CostCentreId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<CostCentre>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<CostCentre>().ToTable("mpr_costcentre");

            ////CostCentreDefinition
            //modelBuilder.Entity<CostCentreDefinition>().HasKey<int>(e => e.CCDefinitionId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<CostCentreDefinition>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<CostCentreDefinition>().ToTable("mpr_costcentre_definition");

            ////ExpenseBasis
            //modelBuilder.Entity<ExpenseBasis>().HasKey<int>(e => e.ExpenseBasisId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<ExpenseBasis>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<ExpenseBasis>().ToTable("mpr_expense_basis");

            ////ExpenseMapping
            //modelBuilder.Entity<ExpenseMapping>().HasKey<int>(e => e.ExpenseMappingId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<ExpenseMapping>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<ExpenseMapping>().ToTable("mpr_expense_map");

            ////ExpenseProductMapping
            //modelBuilder.Entity<ExpenseProductMapping>().HasKey<int>(e => e.ExpenseProductId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<ExpenseProductMapping>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<ExpenseProductMapping>().ToTable("mpr_expense_product_mapping");

            ////ExpenseGLMapping
            //modelBuilder.Entity<ExpenseGLMapping>().HasKey<int>(e => e.ExpenseGLId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<ExpenseGLMapping>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<ExpenseGLMapping>().ToTable("mpr_expense_gl_mapping");

            ////ExpenseRawBasis
            //modelBuilder.Entity<ExpenseRawBasis>().HasKey<int>(e => e.ExpenseRawBasisId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<ExpenseRawBasis>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<ExpenseRawBasis>().ToTable("mpr_expense_raw_basis");

            ////StaffCost
            //modelBuilder.Entity<StaffCost>().HasKey<int>(e => e.StaffCostId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<StaffCost>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<StaffCost>().ToTable("mpr_staffcost");

            ////OpexManagementTree
            //modelBuilder.Entity<OpexManagementTree>().HasKey<int>(e => e.OpexMgtTreeId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<OpexManagementTree>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<OpexManagementTree>().ToTable("mpr_opex_management_tree");

            ////ActivityBase
            //modelBuilder.Entity<ActivityBase>().HasKey<int>(e => e.ActivityBaseId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<ActivityBase>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<ActivityBase>().ToTable("mpr_activity_base");

            ////ActivityBaseRatio
            //modelBuilder.Entity<ActivityBaseRatio>().HasKey<int>(e => e.ActivityBaseRatioId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<ActivityBaseRatio>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<ActivityBaseRatio>().ToTable("mpr_activity_base_ratio");

            ////OpexMISReplacement
            //modelBuilder.Entity<OpexMISReplacement>().HasKey<int>(e => e.OpexMISReplacementId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<OpexMISReplacement>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<OpexMISReplacement>().ToTable("mpr_opex_mis_replacement");

            ////OpexBusinessRule
            //modelBuilder.Entity<OpexBusinessRule>().HasKey<int>(e => e.OpexBusinessRuleId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<OpexBusinessRule>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<OpexBusinessRule>().ToTable("mpr_opex_business_rule");

            ////OpexAbcExemption
            //modelBuilder.Entity<OpexAbcExemption>().HasKey<int>(e => e.OpexAbcExemptionId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<OpexAbcExemption>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<OpexAbcExemption>().ToTable("mpr_opex_abc_exemption");

            ////OpexRawExpense
            //modelBuilder.Entity<OpexRawExpense>().HasKey<int>(e => e.OpexRawExpenseId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<OpexRawExpense>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<OpexRawExpense>().ToTable("mpr_opex_raw_expense");

            ////OpexGLMapping
            //modelBuilder.Entity<OpexGLMapping>().HasKey<int>(e => e.GLMappingId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<OpexGLMapping>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<OpexGLMapping>().ToTable("mpr_opex_gl_mapping");

            ////OpexRreport
            //modelBuilder.Entity<OpexReport>().HasKey<int>(e => e.ReportId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<OpexReport>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<OpexReport>().ToTable("mpr_opex_report");

            ////OpexGLBasis
            //modelBuilder.Entity<OpexGLBasis>().HasKey<int>(e => e.OpexGLBasisId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<OpexGLBasis>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<OpexGLBasis>().ToTable("mpr_opex_gl_basis");

            ////OpexBasisMapping
            //modelBuilder.Entity<OpexBasisMapping>().HasKey<int>(e => e.OpexBasisMappingId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<OpexBasisMapping>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<OpexBasisMapping>().ToTable("mpr_opex_basis_mapping");

            ////Score Card
            ////SCDConfiguration
            //modelBuilder.Entity<SCDConfiguration>().HasKey<int>(e => e.ConfigurationId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<SCDConfiguration>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<SCDConfiguration>().ToTable("scd_configuration");

            //ChartOfAccount
            modelBuilder.Entity<ChartOfAccount>().HasKey<int>(e => e.ChartOfAccountId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ChartOfAccount>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ChartOfAccount>().ToTable("cor_chartofacct");

            //Currency
            modelBuilder.Entity<Currency>().HasKey<int>(e => e.CurrencyId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Currency>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Currency>().ToTable("cor_currency");

            //FiscalYear
            modelBuilder.Entity<FiscalYear>().HasKey<int>(e => e.FiscalYearId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FiscalYear>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FiscalYear>().ToTable("cor_fiscalyear");

            //Branch
            modelBuilder.Entity<Branch>().HasKey<int>(e => e.BranchId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Branch>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Branch>().ToTable("cor_branch");

            //Product
            modelBuilder.Entity<Product>().HasKey<int>(e => e.ProductId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Product>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Product>().ToTable("cor_product");

            //FiscalPeriod
            modelBuilder.Entity<FiscalPeriod>().HasKey<int>(e => e.FiscalPeriodId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FiscalPeriod>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FiscalPeriod>().ToTable("cor_fiscalperiod");

            //FinancialType
            modelBuilder.Entity<FinancialType>().HasKey<int>(e => e.FinancialTypeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FinancialType>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FinancialType>().ToTable("cor_financial_type");

            //GLDefinition
            modelBuilder.Entity<GLDefinition>().HasKey<int>(e => e.GLDefinitionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<GLDefinition>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<GLDefinition>().ToTable("cor_gl_definition");

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
                            UpdateAuditProperty(entry);
                        }
                    }
                }

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

        void UpdateAuditProperty(DbEntityEntry entry)
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
            else
            {
                //entry is modified
                var model = (EntityBase)entry.Entity;
                model.UpdatedBy = DataConnector.LoginName;
                model.UpdatedOn = DateTime.Now;
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

                string companydbServerName;
                string companydbDatabaseName;
                string companydbUserName;
                string companydbPassword;

                if (companydb.Database.Title == "Encrypted")
                {
                    var key = Properties.Resources.Key;
                    companydbServerName = CryptoEngine.Decrypt(companydb.Database.ServerName, key);
                    companydbDatabaseName = CryptoEngine.Decrypt(companydb.Database.DatabaseName, key);
                    companydbUserName = CryptoEngine.Decrypt(companydb.Database.UserName, key);
                    companydbPassword = CryptoEngine.Decrypt(companydb.Database.Password, key);
                }
                else
                {
                    companydbServerName = companydb.Database.ServerName;
                    companydbDatabaseName = companydb.Database.DatabaseName;
                    companydbUserName = companydb.Database.UserName;
                    companydbPassword = companydb.Database.Password;
                }

                //connectionString="Data Source=10.0.0.18\FintrakSQL2014;Initial Catalog=FintrakDB;User =sa;Password=sqluser10$;Integrated Security=False"
                connectionString = string.Format("Data Source= {0};Initial Catalog={1};User ={2};Password={3};Integrated Security={4}", companydbServerName, companydbDatabaseName, companydbUserName, companydbPassword, companydb.Database.IntegratedSecurity);
            }

            return connectionString;
        }

    }
}
