using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Fintrak.Shared.AuditService;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Data;
using systemContract = Fintrak.Data.SystemCore.Contracts;
using systemCore = Fintrak.Data.SystemCore;
using Fintrak.Data.SystemCore.Contracts;
using DecryptOperation;

namespace Fintrak.Data.IFRS
{
    public class IFRSContext : DbContext
    {
        const string SOLUTION_NAME = "FIN_IFRS";

        AuditManager _auditManager;

        public IFRSContext()
            : base(GetDataConnection())
        {
            System.Data.Entity.Database.SetInitializer<IFRSContext>(null);

            _auditManager = new AuditManager(GetDataConnection());
        }

        public IFRSContext(string connectionString)
            : base(connectionString)
        {
            System.Data.Entity.Database.SetInitializer<IFRSContext>(null);
            _auditManager = new AuditManager(connectionString);
        }

        //IFRS 
        public DbSet<DerivedCaption> DerivedCaptionSet { get; set; }
        public DbSet<GLMapping> GLMappingSet { get; set; }
        public DbSet<GLMappingMgt> GLMappingMgtSet { get; set; }
        public DbSet<InstrumentType> InstrumentTypeSet { get; set; }
        public DbSet<GLType> GLTypeSet { get; set; }
        public DbSet<InstrumentTypeGLMap> InstrumentTypeGLMapSet { get; set; }
        public DbSet<AutoPostingTemplate> AutoPostingTemplateSet { get; set; }
        public DbSet<TrialBalanceGap> TrialBalanceGapSet { get; set; }
        public DbSet<GLAdjustment> GLAdjustmentSet { get; set; }
        public DbSet<PostingDetail> PostingDetailSet { get; set; }
        public DbSet<PostingDetailContracts> PostingDetailContractsSet { get; set; }
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
        public DbSet<LoanPry> LoanPryDataSet { get; set; }
        public DbSet<RawLoanDetails> LoanDetailsSet { get; set; }
        public DbSet<IndividualImpairment> IndividualImpairmentSet { get; set; }
        public DbSet<IndividualSchedule> IndividualScheduleSet { get; set; }
        public DbSet<IntegralFee> IntegralFeeSet { get; set; }
        public DbSet<LoanIRRData> LoanIRRDataSet { get; set; }
        public DbSet<IfrsCustomer> IfrsCustomerSet { get; set; }
        public DbSet<IfrsCustomerAccount> IfrsCustomerAccountSet { get; set; }
        public DbSet<LoanPryMoratorium> LoanPryMoratoriumDataSet { get; set; }
        public DbSet<Borrowings> BorrowingsDataSet { get; set; }
        public DbSet<BorrowingPeriodicSchedule> BorrowingPeriodicScheduleSet { get; set; }
        public DbSet<BorrowingSchedule> BorrowingScheduleSet { get; set; }

        public DbSet<ChartOfAccount> ChartOfAccountSet { get; set; }
        public DbSet<Currency> CurrencySet { get; set; }
        public DbSet<FiscalYear> FiscalYearSet { get; set; }
        public DbSet<Branch> BranchSet { get; set; }
        public DbSet<Product> ProductSet { get; set; }
        public DbSet<FiscalPeriod> FiscalPeriodSet { get; set; }
        public DbSet<FinancialType> FinancialTypeSet { get; set; }
        public DbSet<GLDefinition> GLDefinitionSet { get; set; }
        public DbSet<RevenueGLMapping> RevenueGLMappingSet { get; set; }

        public DbSet<QualitativeNote> QualitativeNoteSet { get; set; }

        public DbSet<IFRSReportPack> IFRSReportPackSet { get; set; }
        public DbSet<IFRSRevacctRegistry> IFRSRevacctRegistrySet { get; set; }
        public DbSet<Placement> PlacementSet { get; set; }
        public DbSet<BondSummary> BondSummarySet { get; set; }
        public DbSet<LedgerDetailSummary> LedgerDetailSummarySet { get; set; }
        public DbSet<DepositRepaymentSchedule> DepositRepaymentScheduleSet { get; set; }
        public DbSet<LiabilityRepaymentSchedule> LiabilityRepaymentScheduleSet { get; set; }
        public DbSet<LoanCommitment> LoanCommitmentSet { get; set; }
        public DbSet<ChangesInEquity> ChangesInEquitySet { get; set; }

        public DbSet<ifrsexceptionreport> ifrsexceptionreportSet { get; set; }

        //IFRS9

        public DbSet<ExternalRating> ExternalRatingSet { get; set; }
        public DbSet<HistoricalSectorRating> HistoricalSectorRatingSet { get; set; }
        public DbSet<InternalRatingBased> InternalRatingBasedSet { get; set; }
        public DbSet<MacroEconomic> MacroEconomicSet { get; set; }
        public DbSet<RatingMapping> RatingMappingSet { get; set; }
        public DbSet<Transition> TransitionSet { get; set; }
        public DbSet<Sector> SectorSet { get; set; }
        public DbSet<HistoricalClassification> HistoricalClassificationSet { get; set; }
        public DbSet<MacroEconomicHistorical> MacroEconomicHistoricalSet { get; set; }
        public DbSet<NotchDifference> NotchDifferenceSet { get; set; }
        public DbSet<SectorialRegressedPD> SectorialRegressedPDSet { get; set; }
        public DbSet<SectorialRegressedLGD> SectorialRegressedLGDSet { get; set; }
        //public DbSet<ComputedForcastedPDLGD> ComputedForcastedPDLGDSet { get; set; }
        public DbSet<HistoricalSectorialPD> HistoricalSectorialPDSet { get; set; }
        public DbSet<MacroEconomicVariable> MacroEconomicVariableSet { get; set; }
        public DbSet<SectorVariableMapping> SectorVariableMappingSet { get; set; }

        public DbSet<PitFormular> PitFormularSet { get; set; }

        public DbSet<PortfolioExposure> PortfolioExposureSet { get; set; }

        public DbSet<SectorialExposure> SectorialExposureSet { get; set; }

        public DbSet<PiTPD> PiTPDSet { get; set; }
        public DbSet<EclCalculationModel> EclCalculationModelSet { get; set; }

        public DbSet<LoanBucketDistribution> LoanBucketDistributionSet { get; set; }

        public DbSet<MacroeconomicVDisplay> MacroeconomicVDisplaySet { get; set; }

        public DbSet<LifeTimePDClassification> LifeTimePDClassificationSet { get; set; }


        public DbSet<FairValuationModel> FairValuationModelSet { get; set; }

        public DbSet<SummaryReport> SummaryReportSet { get; set; }

        public DbSet<IfrsStocksPrimaryData> IfrsStocksPrimaryDataSet { get; set; }

        public DbSet<IfrsStocksMapping> IfrsStocksMappingSet { get; set; }

        public DbSet<IfrsEquityUnqouted> IfrsEquityUnqoutedSet { get; set; }

        public DbSet<Reconciliation> ReconciliationSet { get; set; }

        public DbSet<ForecastedMacroeconimcsSensitivity> ForecastedMacroeconimcsSensitivitySet { get; set; }

        public DbSet<BucketExposure> BucketExposureSet { get; set; }

        public DbSet<ForecastedMacroeconimcsScenario> ForecastedMacroeconimcsScenarioSet { get; set; }

        public DbSet<LoanSpreadSensitivity> LoanSpreadSensitivitySet { get; set; }

        public DbSet<LoanSpreadScenario> LoanSpreadScenarioSet { get; set; }

        public DbSet<UnquotedEquityFairvalueResult> UnquotedEquityFairvalueResultSet { get; set; }
        public DbSet<PiTPDComparism> PiTPDComparismSet { get; set; }

        public DbSet<MarkovMatrix> MarkovMatrixSet { get; set; }

        public DbSet<CCFModelling> CCFModellingSet { get; set; }

        public DbSet<HistoricalSectorialLGD> HistoricalSectorialLGDSet { get; set; }

        public DbSet<ECLComparism> ECLComparismSet { get; set; }

        public DbSet<ForeignEADExchangeRate> ForeignEADExchangeRateSet { get; set; }
        public DbSet<OffBalanceSheetExposure> OffBalanceSheetExposureSet { get; set; }

        //Begin Victor IFRS9 DataSet Segment
        public DbSet<LocalBondSpread> LocalBondSpreadSet { get; set; }
        public DbSet<MarginalPDDistribution> MarginalPDDistributionSet { get; set; }
        public DbSet<BondMarginalPDDistribution> BondMarginalPDDistributionSet { get; set; }
        public DbSet<MarginalPDDistributionPlacement> MarginalPDDistributionPlacementSet { get; set; }
        public DbSet<LgdComputationResult> LgdComputationResultSet { get; set; }
        public DbSet<LgdComputationResultPlacement> LgdComputationResultPlacementSet { get; set; }
        public DbSet<PlacementComputationResult> PlacementComputationResultSet { get; set; }
        public DbSet<EclComputationResult> FinalEclOutputSet { get; set; }
        public DbSet<BondEclComputationResult> BondEclComputationResultSet { get; set; }
        public DbSet<PlacementEclComputationResult> PlacementEclComputationResultSet { get; set; }
        public DbSet<LcBgEclComputationResult> LcBgEclComputationResultSet { get; set; }
        public DbSet<EuroBondSpread> EuroBondSpreadSet { get; set; }
        public DbSet<TBillEclComputationResult> TBillEclComputationResultSet { get; set; }
        //End Victor IFRS9 DataSet Segment

        public DbSet<ComputedForcastedPDLGD> ComputedForcastedPDLGDSet { get; set; }
        public DbSet<ComputedForcastedPDLGDForeign> ComputedForcastedPDLGDForeignSet { get; set; }
        public DbSet<MacroEconomicsNPL> MacroEconomicsNPLSet { get; set; }
        public DbSet<MonthlyDiscountFactor> MonthlyDiscountFactorSet { get; set; }
        public DbSet<MonthlyDiscountFactorPlacement> MonthlyDiscountFactorPlacementSet { get; set; }
        public DbSet<MonthlyDiscountFactorBond> MonthlyDiscountFactorBondSet { get; set; }
        public DbSet<IfrsPdSeriesByRating> IfrsPdSeriesByRatingSet { get; set; }
        public DbSet<IfrsRetailPdSeries> IfrsRetailPdSeriesSet { get; set; }
        public DbSet<IfrsCorporateEcl> IfrsCorporateEclSet { get; set; }
        public DbSet<InvestmentOthersECL> InvestmentOthersECLSet { get; set; }
        public DbSet<IfrsLgdScenarioByInstrument> IfrsLgdScenarioByInstrumentSet { get; set; }
        public DbSet<IfrsSectorCCF> IfrsSectorCCFSet { get; set; }
        public DbSet<MacroVarRecoveryRates> MacroVarRecoveryRatesSet { get; set; }
        public DbSet<ProbabilityWeighted> ProbabilityWeightedSet { get; set; }
        public DbSet<MacrovariableEstimate> MacrovariableEstimateSet { get; set; }
        public DbSet<SectorMapping> SectorMappingSet { get; set; }
        public DbSet<SandPRating> SandPRatingSet { get; set; }
        public DbSet<LoanInterestRate> LoanInterestRateSet { get; set; }
        public DbSet<HistoricalDefaultedAccounts> HistoricalDefaultedAccountsSet { get; set; }
        public DbSet<OffBalancesheetECL> OffBalancesheetECLSet { get; set; }
        public DbSet<ImpairmentResultRetail> ImpairmentResultRetailSet { get; set; }
        public DbSet<ImpairmentResultOBE> ImpairmentResultOBESet { get; set; }
        public DbSet<ImpairmentInvestment> ImpairmentInvestmentSet { get; set; }
        public DbSet<ImpairmentCorporate> ImpairmentCorporateSet { get; set; }
        public DbSet<IfrsFinalRetailOutput> IfrsFinalRetailOutputSet { get; set; }
        public DbSet<IfrsCustomerPD> IfrsCustomerPDSet { get; set; }
        public DbSet<IfrsCorporatePdSeries> IfrsCorporatePdSeriesSet { get; set; }
        public DbSet<ECLInputRetail> ECLInputRetailSet { get; set; }
        public DbSet<StaffLoansComputationResult> StaffLoansComputationResultSet { get; set; }
        public DbSet<LoanCommitmentComputationResult> LoanCommitmentComputationResultSet { get; set; }
        public DbSet<InputDetail> InputDetailSet { get; set; }
        public DbSet<EclWeightedAvg> EclWeightedAvgSet { get; set; }
        public DbSet<NseIndex> NseIndexSet { get; set; }
        public DbSet<ProbabilityWeight> ProbabilityWeightSet { get; set; }
        public DbSet<MacroeconomicsVariableScenario> MacroeconomicsVariableScenarioSet { get; set; }
        public DbSet<LgdInputFactor> LgdInputFactorSet { get; set; }
        public DbSet<RegressionOutput> RegressionOutputSet { get; set; }
        public DbSet<RatioDetail> RatioDetailSet { get; set; }
        public DbSet<RatioCaption> RatioCaptionSet { get; set; }
        public DbSet<GLAArchive> GLAArchiveSet { get; set; }

        public DbSet<CollateralDetails> CollateralDetailsSet { get; set; }

        public DbSet<FacClassConsolidated> FacClassConsolidatedSet { get; set; }

        public DbSet<HCClassification> HCClassificationSet { get; set; }
        public DbSet<FacilityClassification> FacilityClassificationSet { get; set; }

        public DbSet<FacRating> FacRatingSet { get; set; }

        public DbSet<FacStaging> FacStagingSet { get; set; }
        public DbSet<FacilityStaging> FacilityStagingSet { get; set; }

        public DbSet<FacOBEStaging> FacOBEStagingSet { get; set; }

        public DbSet<OBExposureCCF> OBExposureCCFSet { get; set; }

        public DbSet<OBExposure> OBExposureSet { get; set; }

        public DbSet<Cashflow> CashflowDataSet { get; set; }

        public DbSet<SICRParameters> SICRParametersSet { get; set; }
        public DbSet<MarginalCCFStr> MarginalCCFStrDataSet { get; set; }
        public DbSet<LoanSignificantFlag> LoanSignificantFlagDataSet { get; set; }
        public DbSet<ObeEclComputation> ObeEclComputationDataSet { get; set; }
        public DbSet<LoansECLComputationResult> LoansECLComputationResultDataSet { get; set; }
        public DbSet<IfrsInvestment> IfrsInvestmentSet { get; set; }
        public DbSet<IfrsBondLGD> IfrsBondLGDSet { get; set; }

        public DbSet<MonthlyObeEadSTRLB> MonthlyObeEadSTRLB { get; set; }

        public DbSet<BondsECLComputationResult> BondsECLComputationResult { get; set; }

        public DbSet<MarginalPddSTRLB> MarginalPddSTRLBSet { get; set; }
        public DbSet<ODEclComputationResult> ODEclComputationResultSet { get; set; }
        public DbSet<MarginalPdObeDistr> MarginalPdObeDistrSet { get; set; }
        public DbSet<LGDComptResult> LGDComptResultSet { get; set; }
        public DbSet<ObeLGDComptResult> ObeLGDComptResultSet { get; set; }
        public DbSet<IfrsBondsMonthlyEAD> IfrsBondsMonthlyEADSet { get; set; }
        public DbSet<IfrsMonthlyEAD> IfrsMonthlyEADSet { get; set; }

        public DbSet<LoanClassificationSICRSignFlag> LoanClassificationSICRSignFlagSet { get; set; }
        public DbSet<LoanECLResult> LoanECLResult { get; set; }


        public DbSet<MarginalCCFPivotSTRLB> MarginalCCFPivotSTRLB { get; set; }
        public DbSet<CcfAnalysisOverDraftSTRLB> CcfAnalysisOverDraftSTRLB { get; set; }



        public DbSet<BondsECLResult> BondsECLResult { get; set; }
        public DbSet<ObeECLResult> ObeECLResult { get; set; }
        public DbSet<OdECLResult> OdECLResult { get; set; }

        public DbSet<CollateralGrowthRate> CollateralGrowthRate { get; set; }
        public DbSet<CummulativeDefaultAmt> CummulativeDefaultAmt { get; set; }
        public DbSet<CummulativeLifetimePd> CummulativeLifetimePd { get; set; }
        public DbSet<CollateralRecAmtStaging> CollateralRecAmtStaging { get; set; }
        public DbSet<HistoricalDefaultFreq> HistoricalDefaultFreq { get; set; }
        public DbSet<CummulativePD> CummulativePD { get; set; }
        //---------------//
        //Access Bank Plc//
        //---------------//
        public DbSet<Assumption> AssumptionSet { get; set; }
        public DbSet<SPCumulativePD> SPCumulativePDSet { get; set; }
        public DbSet<CollateralInput> CollateralInputSet { get; set; }


        public DbSet<PostingGLMapping> PostingGLMappingSet { get; set; }



        public DbSet<IfrsProjectedCummDefaultFrq> IfrsProjectedCummDefaultFrqSet { get; set; }
        public DbSet<IfrsMonthlyForwardPDMacroVar> IfrsMonthlyForwardPDMacroVarSet { get; set; }
        public DbSet<IfrsMarginalPDByScenerio> IfrsMarginalPDByScenerioSet { get; set; }

        public DbSet<InvestmentMarginalPd> InvestmentMarginalPd { get; set; }
        public DbSet<IfrsPdTermStructure> IfrsPdTermStructureSet { get; set; }

        public DbSet<ConsolidatedLoans> ConsolidatedLoans { get; set; }
        public DbSet<OverdraftMonthlyEAD> OverdraftMonthlyEADSet { get; set; }

        public DbSet<OverdraftLGDComputation> OverdraftLGDComputationSet { get; set; }



        /// ////////////////////////////
        public DbSet<IfrsLoansInfo> IfrsLoansInfoset { get; set; }
        public DbSet<IfrsLgdProjections> IfrsLgdProjectionsset { get; set; }
        public DbSet<IfrsPDProjection> IfrsPDProjectionset { get; set; }

        public DbSet<Calendar> CalendarSet { get; set; }

        public DbSet<InterimQualitativeNote> InterimQualitativeNoteSet { get; set; }

        public DbSet<CashFlowTB> CashFlowTBSet { get; set; }
        public DbSet<AmortizationOutput> AmortizationOutputSet { get; set; }

        public DbSet<SegmentPerformance> SegmentPerformanceSet { get; set; }


        public DbSet<IfrsLoanMissPayment> IfrsLoanMissPaymentSet { get; set; }
        public DbSet<MacroEconomicForeCast> MacroEconomicForeCastSet { get; set; }
        public DbSet<RegressionCofficient> RegressionCofficientSet { get; set; }
        public DbSet<IfrsOverdraftData> IfrsOverdraftDataset { get; set; }
        public DbSet<MacroNPL> MacroNPLset { get; set; }
        public DbSet<ProbabilisticModel> ProbabilisticModelset { get; set; }

        //////DBSet
        public DbSet<CashFlowRestructure> CashFlowRestructureSet { get; set; }
        public DbSet<ModificationGainorLoss> ModificationGainorLossSet { get; set; }
        public DbSet<RestructureInfo> RestructureInfoSet { get; set; }
        public DbSet<LoanRestructurePVCashFlow> LoanRestructurePVCashFlowSet { get; set; }


        public DbSet<CummulativeMatrix> CummulativeMatrixSet { get; set; }
        public DbSet<AdjustedMatrix> AdjustedMatrixSet { get; set; }
        public DbSet<CreditIndex> CreditIndexSet { get; set; }


        public DbSet<PDMigrationMatrixFinal> PDMigrationMatrixFinalSet { get; set; }
        public DbSet<FacilitiesStageMigration> FacilitiesStageMigrationSet { get; set; }

        public DbSet<Regressionweights> RegressionweightsSet { get; set; }

        public DbSet<CummulativePDD> CummulativePDDSet { get; set; }
        public DbSet<MarginalOutput> MarginalOutputSet { get; set; }

        public DbSet<HistoricalMacroVariableInput> HistoricalMacroVariableInputSet { get; set; }
        public DbSet<IfrsScalarUpload> IfrsScalarUploadSet { get; set; }

        public DbSet<Mevlisting> MevlistingSet { get; set; }
        
        //ABP
        public DbSet<IfrsHistoricalMEVAbp> IfrsHistoricalMEVAbpSet { get; set; }
        public DbSet<IfrsConfidenceIntervalAbp> IfrsConfidenceIntervalAbpSet { get; set; }
        public DbSet<IfrsMevForcastabp> IfrsMevForcastabpSet { get; set; }

        //DENNIS - IKENNA ACCESS UPDATE
        public DbSet<IfrsAccessEstimateRecoveryOutput> IfrsAccessEstimateRecoveryOutputSet { get; set; }
        public DbSet<IfrsAccessLGDDiscountFactorOutput> IfrsAccessLGDDiscountFactorOutputSet { get; set; }
        public DbSet<IfrsAccessLGDOutput> IfrsAccessLGDOutputSet { get; set; }
        public DbSet<IfrsAccessttcDownTurnResult> IfrsAccessttcDownTurnResultSet { get; set; }
        public DbSet<IfrsRecoveryOutputnewAccessModel> IfrsRecoveryOutputnewAccessModelSet { get; set; }
        public DbSet<Test> TestSet { get; set; }

        ///////////////Tosin Update
        public DbSet<IfrsCureRatesRecoveryRates> IfrsCureRatesRecoveryRatesset { get; set; }
        public DbSet<IfrsInvestmentECLSummary> IfrsInvestmentECLSummaryset { get; set; }
        public DbSet<IfrsDataValidator> IfrsDataValidatorset { get; set; }

        ///////////////UnquotedEquity
        public DbSet<UnquotedEquityInput> UnquotedEquityInputSet { get; set; }
        public DbSet<UnquotedEquityMedAVGPB> UnquotedEquityMedAVGPBSet { get; set; }
        public DbSet<UnquotedEquityMedAVGPE> UnquotedEquityMedAVGPESet { get; set; }
        public DbSet<UnquotedEquityPEPBRatio> UnquotedEquityPEPBRatioSet { get; set; }
        public DbSet<UnquotedEquitySummaryInput> UnquotedEquitySummaryInputSet { get; set; }
        public DbSet<UEQUnquotedEquitySummaryReport> UEQUnquotedEquitySummaryReportSet { get; set; }
        public DbSet<UnquotedEquityMarketabilityDiscount> UnquotedEquityMarketabilityDiscountSet { get; set; }
        public DbSet<UnquotedEquityMKTRDisc> UnquotedEquityMKTRDiscSet { get; set; }
        public DbSet<UnquotedEquityCountryRiskDisc> UnquotedEquityCountryRiskDiscSet { get; set; }
        public DbSet<IfrsBenefitsStaffLoan> IfrsBenefitsStaffLoanSet { get; set; }
        public DbSet<IfrsGetCashFlowEIR> IfrsGetCashFlowEIRSet { get; set; }
        public DbSet<IfrsDescriptionLedger> IfrsDescriptionLedgerSet { get; set; }
        public DbSet<IfrsRepaymentSchedule> IfrsRepaymentScheduleSet { get; set; }
        public DbSet<SubSector> SubSectorSet { get; set; }
        public DbSet<IfrsStaffBenefitsLoans> IfrsStaffBenefitsLoansSet { get; set; }
        public DbSet<IfrsStaffBenefitsReportSummary> IfrsStaffBenefitsReportSummarySet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();



            //MarginalCCFPivotSTRLB
            modelBuilder.Entity<MarginalCCFPivotSTRLB>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<MarginalCCFPivotSTRLB>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MarginalCCFPivotSTRLB>().ToTable("ifrs_MarginalCCF_Pivot_STRLB");

            //CcfAnalysisOverDraftSTRLB
            modelBuilder.Entity<CcfAnalysisOverDraftSTRLB>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<CcfAnalysisOverDraftSTRLB>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CcfAnalysisOverDraftSTRLB>().ToTable("ifrs_CCFAnalysis_OverDraft_STRLB");



            //IFRSContext

            //BondsECLComputationResult
            modelBuilder.Entity<BondsECLComputationResult>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<BondsECLComputationResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BondsECLComputationResult>().ToTable("ifrs_bonds_ecl_computation_result");


            //MonthlyObeEadSTRLB
            modelBuilder.Entity<MonthlyObeEadSTRLB>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<MonthlyObeEadSTRLB>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MonthlyObeEadSTRLB>().ToTable("ifrs_monthly_OBE_EAD_STRLB");

            //IFRS
            //DerivedCaption
            modelBuilder.Entity<DerivedCaption>().HasKey<int>(e => e.DerivedCaptionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<DerivedCaption>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<DerivedCaption>().ToTable("ifrs_derivedcaption");

            //GLMapping
            modelBuilder.Entity<GLMapping>().HasKey<int>(e => e.GLMappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<GLMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<GLMapping>().ToTable("ifrs_glmapping");

            //GLMapping
            modelBuilder.Entity<GLMapping>().HasKey<int>(e => e.GLMappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<GLMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<GLMapping>().ToTable("ifrs_glmapping");

            //GLMappingMgt
            modelBuilder.Entity<GLMappingMgt>().HasKey<int>(e => e.GLMappingMgtId).Ignore(e => e.EntityId);
            modelBuilder.Entity<GLMappingMgt>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<GLMappingMgt>().ToTable("ifrs_glmapping_mgt");

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

            //PostingDetailContracts
            modelBuilder.Entity<PostingDetailContracts>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<PostingDetailContracts>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PostingDetailContracts>().ToTable("ifrs_AutoPosting_Summary");

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
            //modelBuilder.Entity<LoanPrimaryData>().HasKey<int>(e => e.PryId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<LoanPrimaryData>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<LoanPrimaryData>().ToTable("ifrs_loan_primary_data");

            //LoanDetails
            //modelBuilder.Entity<LoanDetails>().HasKey<int>(e => e.LoanDetailId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<LoanDetails>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<LoanDetails>().ToTable("ifrs_loans_details");

            //IndividualImpairment
            modelBuilder.Entity<IndividualImpairment>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IndividualImpairment>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IndividualImpairment>().ToTable("ifrs_individual_impairment");

            //IndividualSchedule
            modelBuilder.Entity<IndividualSchedule>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IndividualSchedule>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IndividualSchedule>().ToTable("ifrs_individual_schedule");

            //IntegralFee
            modelBuilder.Entity<IntegralFee>().HasKey<int>(e => e.IntegralFeeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IntegralFee>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IntegralFee>().ToTable("ifrs_integral_fee");

            //LoanIRRData
            modelBuilder.Entity<LoanIRRData>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanIRRData>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanIRRData>().ToTable("ifrs_loan_irr_data");

            //IfrsCustomer
            modelBuilder.Entity<IfrsCustomer>().HasKey<int>(e => e.CustomerId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsCustomer>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsCustomer>().ToTable("ifrs_customer");

            //IfrsCustomerAccount
            modelBuilder.Entity<IfrsCustomerAccount>().HasKey<int>(e => e.CustAccountId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsCustomerAccount>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsCustomerAccount>().ToTable("ifrs_customer_account");

            //BorrowingPeriodicSchedule
            modelBuilder.Entity<BorrowingPeriodicSchedule>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<BorrowingPeriodicSchedule>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BorrowingPeriodicSchedule>().ToTable("ifrs_borrowings_periodic_schedule");

            //BorrowingSchedule
            modelBuilder.Entity<BorrowingSchedule>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<BorrowingSchedule>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BorrowingSchedule>().ToTable("ifrs_borrowings_schedule");

            //BorrowingSchedule
            modelBuilder.Entity<Borrowings>().HasKey<int>(e => e.BorrowingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Borrowings>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Borrowings>().ToTable("ifrs_borrowings_primary_data");

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


            //ifrs_bonds
            modelBuilder.Entity<IFRSBonds>().HasKey<int>(e => e.BondId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IFRSBonds>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IFRSBonds>().ToTable("ifrs_bonds");

            //ifrs_tbills
            modelBuilder.Entity<IFRSTbills>().HasKey<int>(e => e.TbillId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IFRSTbills>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IFRSTbills>().ToTable("ifrs_tbills");

            //LoanPrimaryData
            modelBuilder.Entity<LoanPry>().HasKey<int>(e => e.PryId).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanPry>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanPry>().ToTable("ifrs_loan_primary_data");

            //LoanPrimaryMoratoriumData
            modelBuilder.Entity<LoanPryMoratorium>().HasKey<int>(e => e.LoanPryMoratoriumId).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanPryMoratorium>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanPryMoratorium>().ToTable("ifrs_loan_primary_moratorium");

            //RawLoanDetails
            modelBuilder.Entity<RawLoanDetails>().HasKey<int>(e => e.LoanDetailId).Ignore(e => e.EntityId);
            modelBuilder.Entity<RawLoanDetails>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<RawLoanDetails>().ToTable("ifrs_loans_details");

            //RevenueGLMapping
            modelBuilder.Entity<RevenueGLMapping>().HasKey<int>(e => e.GLMappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<RevenueGLMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<RevenueGLMapping>().ToTable("ifrs_revacct_glmapping");

            //QualitativeNote
            modelBuilder.Entity<QualitativeNote>().HasKey<int>(e => e.QualitativeNoteId).Ignore(e => e.EntityId);
            modelBuilder.Entity<QualitativeNote>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<QualitativeNote>().ToTable("ifrs_captions_qualitative");

            //IFRSReportPack
            modelBuilder.Entity<IFRSReportPack>().HasKey<int>(e => e.ReportPackId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IFRSReportPack>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IFRSReportPack>().ToTable("ifrs_report_pack");


            //IFRSReportPack
            modelBuilder.Entity<IFRSReportPack>().HasKey<int>(e => e.ReportPackId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IFRSReportPack>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IFRSReportPack>().ToTable("ifrs_report_pack");


            //IFRSRevacctRegistry
            modelBuilder.Entity<IFRSRevacctRegistry>().HasKey<int>(e => e.RevenueId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IFRSRevacctRegistry>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IFRSRevacctRegistry>().ToTable("ifrs_revacct_registry");


            //Placement
            modelBuilder.Entity<Placement>().HasKey<int>(e => e.Placement_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<Placement>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Placement>().ToTable("ifrs_placement");


            //ifrs_deposit_repayment_schedule
            modelBuilder.Entity<DepositRepaymentSchedule>().HasKey<int>(e => e.DepositRepayId).Ignore(e => e.EntityId);
            modelBuilder.Entity<DepositRepaymentSchedule>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<DepositRepaymentSchedule>().ToTable("ifrs_deposit_repayment_schedule");


            //ifrs_liability_repayment_schedule
            modelBuilder.Entity<LiabilityRepaymentSchedule>().HasKey<int>(e => e.LiabilityRepayId).Ignore(e => e.EntityId);
            modelBuilder.Entity<LiabilityRepaymentSchedule>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LiabilityRepaymentSchedule>().ToTable("ifrs_liability_repayment_schedule");


            //ifrs_Loan_Commitment
            modelBuilder.Entity<LoanCommitment>().HasKey<int>(e => e.LoanCommitmentId).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanCommitment>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanCommitment>().ToTable("ifrs_loan_commitments");


            //InputDetail
            modelBuilder.Entity<InputDetail>().HasKey<int>(e => e.InputDetailId).Ignore(e => e.EntityId);
            modelBuilder.Entity<InputDetail>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<InputDetail>().ToTable("ifrs_input_details");


            //EclWeightedAvg
            modelBuilder.Entity<EclWeightedAvg>().HasKey<int>(e => e.ECLWATempID).Ignore(e => e.EntityId);
            modelBuilder.Entity<EclWeightedAvg>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<EclWeightedAvg>().ToTable("ifrs_ecl_weighted_avg_temp");


            //NseIndex
            modelBuilder.Entity<NseIndex>().HasKey<int>(e => e.NseIndexId).Ignore(e => e.EntityId);
            modelBuilder.Entity<NseIndex>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<NseIndex>().ToTable("ifrs_NSEIndex");


            //ProbabilityWeight
            modelBuilder.Entity<ProbabilityWeight>().HasKey<int>(e => e.ProbabilityWeighId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ProbabilityWeight>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ProbabilityWeight>().ToTable("ifrs_ProbabilityWeight");


            //ChangesInEquity
            modelBuilder.Entity<ChangesInEquity>().HasKey<int>(e => e.ChangesInEquityId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ChangesInEquity>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ChangesInEquity>().ToTable("IFRS_ChangesInEquity_ClosingBalance");

            //RatioDetail
            modelBuilder.Entity<RatioDetail>().HasKey<int>(e => e.RatioID).Ignore(e => e.EntityId);
            modelBuilder.Entity<RatioDetail>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<RatioDetail>().ToTable("ifrs_ratio_details");

            //RatioCaption
            modelBuilder.Entity<RatioCaption>().HasKey<int>(e => e.RatioCaptionID).Ignore(e => e.EntityId);
            modelBuilder.Entity<RatioCaption>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<RatioCaption>().ToTable("ifrs_ratio_captions_grp");



            //*********************************************//
            //* * * * * * * * * * IFRS9 * * * * * * * * * *//
            //*********************************************//
            //ExternalRating
            modelBuilder.Entity<ExternalRating>().HasKey<int>(e => e.ExternalRatingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ExternalRating>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ExternalRating>().ToTable("ifrs_external_rating");

            //HistoricalSectorRating
            modelBuilder.Entity<HistoricalSectorRating>().HasKey<int>(e => e.HistoricalSectorRatingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<HistoricalSectorRating>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<HistoricalSectorRating>().ToTable("ifrs_sectorial_last_reporting_rating");

            //InternalRatingBased
            modelBuilder.Entity<InternalRatingBased>().HasKey<int>(e => e.InternalRatingBasedId).Ignore(e => e.EntityId);
            modelBuilder.Entity<InternalRatingBased>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<InternalRatingBased>().ToTable("ifrs_internal_rating_based");

            ////MacroEconomic
            //modelBuilder.Entity<MacroEconomic>().HasKey<int>(e => e.MacroEconomicId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<MacroEconomic>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<MacroEconomic>().ToTable("Ifrs_forecasted_macroeconimcs");

            //RatingMapping
            modelBuilder.Entity<RatingMapping>().HasKey<int>(e => e.RatingMappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<RatingMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<RatingMapping>().ToTable("ifrs_rating_mapping");

            //Transition
            modelBuilder.Entity<Transition>().HasKey<int>(e => e.TransitionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Transition>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Transition>().ToTable("ifrs_transition_matrix");

            //Sector
            modelBuilder.Entity<Sector>().HasKey<int>(e => e.SectorId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Sector>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Sector>().ToTable("ifrs_sector_listing");


            //HistoricalClassification
            modelBuilder.Entity<HistoricalClassification>().HasKey<int>(e => e.HistoricalClassificationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<HistoricalClassification>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<HistoricalClassification>().ToTable("ifrs_loan_classification_historical_data");

            //MacroEconomicHistorical
            modelBuilder.Entity<MacroEconomicHistorical>().HasKey<int>(e => e.MacroEconomicHistoricalId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MacroEconomicHistorical>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MacroEconomicHistorical>().ToTable("Ifrs_forecasted_macroeconimcs");

            //NotchDifference
            modelBuilder.Entity<NotchDifference>().HasKey<int>(e => e.NotchDifferenceId).Ignore(e => e.EntityId);
            modelBuilder.Entity<NotchDifference>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<NotchDifference>().ToTable("ifrs_notch_differences");


            //SetUp
            modelBuilder.Entity<SetUp>().HasKey<int>(e => e.SetUpId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SetUp>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SetUp>().ToTable("ifrs_setup_data_");

            //HistoricalSectorialPD
            modelBuilder.Entity<HistoricalSectorialPD>().HasKey<int>(e => e.HistoricalSectorialPDId).Ignore(e => e.EntityId);
            modelBuilder.Entity<HistoricalSectorialPD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<HistoricalSectorialPD>().ToTable("ifrs_historical_sectorial_pd");

            //ComputedForcastedPDLGD
            modelBuilder.Entity<ComputedForcastedPDLGD>().HasKey<int>(e => e.ComputedPDId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ComputedForcastedPDLGD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ComputedForcastedPDLGD>().ToTable("ifrs_computed_forcasted_pd_lgd");

            //SectorialRegressedLGD
            modelBuilder.Entity<SectorialRegressedLGD>().HasKey<int>(e => e.SectorialRegressedLGDId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SectorialRegressedLGD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SectorialRegressedLGD>().ToTable("ifrs_sectorial_regressed_lgd");

            //SectorialRegressedPD
            modelBuilder.Entity<SectorialRegressedPD>().HasKey<int>(e => e.SectorialRegressedPDId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SectorialRegressedPD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SectorialRegressedPD>().ToTable("ifrs_sectorial_regressed_pd");

            //MacroEconomicVariable
            modelBuilder.Entity<MacroEconomicVariable>().HasKey<int>(e => e.MacroEconomicVariableId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MacroEconomicVariable>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MacroEconomicVariable>().ToTable("ifrs_macroeconomic_variables");

            //SectorVariableMapping
            modelBuilder.Entity<SectorVariableMapping>().HasKey<int>(e => e.SectorVariableMappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SectorVariableMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SectorVariableMapping>().ToTable("ifrs_sector_variable_mapping");


            //PitFormular
            modelBuilder.Entity<PitFormular>().HasKey<int>(e => e.PitFormularId).Ignore(e => e.EntityId);
            modelBuilder.Entity<PitFormular>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PitFormular>().ToTable("ifrs_pit_formula");

            //PortfolioExposure
            modelBuilder.Entity<PortfolioExposure>().HasKey<int>(e => e.PortfolioId).Ignore(e => e.EntityId);
            modelBuilder.Entity<PortfolioExposure>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PortfolioExposure>().ToTable("ifrs_dashboard_portfolio_exposure");

            //PortfolioExposure
            modelBuilder.Entity<SectorialExposure>().HasKey<int>(e => e.SectorialExposureId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SectorialExposure>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SectorialExposure>().ToTable("ifrs_dashboard_sectorial_exposure");

            modelBuilder.Entity<EclCalculationModel>().HasKey<int>(e => e.EclModelId).Ignore(e => e.EntityId);
            modelBuilder.Entity<EclCalculationModel>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<EclCalculationModel>().ToTable("ifrs_ecl_models");

            //PiTPD
            modelBuilder.Entity<PiTPD>().HasKey<int>(e => e.PiTPDId).Ignore(e => e.EntityId);
            modelBuilder.Entity<PiTPD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PiTPD>().ToTable("ifrs_pitpds");

            //LoanBucketDistribution
            modelBuilder.Entity<LoanBucketDistribution>().HasKey<int>(e => e.LoanBucketDistributionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanBucketDistribution>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanBucketDistribution>().ToTable("ifrs_loan_classification_spread");

            //ifrs_macro_variable_display
            modelBuilder.Entity<MacroeconomicVDisplay>().HasKey<int>(e => e.MacroVariableDisplayId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MacroeconomicVDisplay>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MacroeconomicVDisplay>().ToTable("ifrs_macro_variable_display");

            //ifrs_macro_variable_display
            modelBuilder.Entity<LifeTimePDClassification>().HasKey<int>(e => e.LifeTimePDClassificationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<LifeTimePDClassification>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LifeTimePDClassification>().ToTable("ifrs_loan_classification_notch_diff");

            //ifrs_macro_variable_display
            modelBuilder.Entity<SummaryReport>().HasKey<int>(e => e.SummaryReportId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SummaryReport>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SummaryReport>().ToTable("ifrs_assessment_summary_report");

            //ifrs_macro_variable_display
            modelBuilder.Entity<FairValuationModel>().HasKey<int>(e => e.FairValueModelId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FairValuationModel>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FairValuationModel>().ToTable("ifrs_fairvaluation_models");


            //IfrsStocksPrimaryData
            modelBuilder.Entity<IfrsStocksPrimaryData>().HasKey<int>(e => e.IfrsStocksPrimaryDataId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsStocksPrimaryData>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsStocksPrimaryData>().ToTable("ifrs_stocks_primary_data");

            //IfrsStocksMapping
            modelBuilder.Entity<IfrsStocksMapping>().HasKey<int>(e => e.IfrsStocksMappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsStocksMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsStocksMapping>().ToTable("ifrs_stocks_mapping");

            //IfrsEquityUnqouted
            modelBuilder.Entity<IfrsEquityUnqouted>().HasKey<int>(e => e.IfrsEquityUnqoutedId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsEquityUnqouted>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsEquityUnqouted>().ToTable("ifrs_equity_unqouted");

            //Reconciliation
            modelBuilder.Entity<Reconciliation>().HasKey<int>(e => e.ReconciliationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Reconciliation>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Reconciliation>().ToTable("ifrs_reconciliation");

            //ForecastedMacroeconimcsSensitivity
            modelBuilder.Entity<ForecastedMacroeconimcsSensitivity>().HasKey<int>(e => e.ForecastedMacroeconimcsSensitivityId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ForecastedMacroeconimcsSensitivity>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ForecastedMacroeconimcsSensitivity>().ToTable("Ifrs_forecasted_macroeconimcs_Sensitivity");

            //BucketExposure
            modelBuilder.Entity<BucketExposure>().HasKey<int>(e => e.BucketExposureId).Ignore(e => e.EntityId);
            modelBuilder.Entity<BucketExposure>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BucketExposure>().ToTable("ifrs_dashboard_bucket_exposure");


            //ForecastedMacroeconimcsScenario
            modelBuilder.Entity<ForecastedMacroeconimcsScenario>().HasKey<int>(e => e.ForecastedMacroeconimcsScenarioId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ForecastedMacroeconimcsScenario>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ForecastedMacroeconimcsScenario>().ToTable("Ifrs_forecasted_macroeconimcs_Scenario");

            //LoanSpreadSensitivity
            modelBuilder.Entity<LoanSpreadSensitivity>().HasKey<int>(e => e.LoanSpreadSensitivityId).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanSpreadSensitivity>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanSpreadSensitivity>().ToTable("ifrs_loan_classification_spread_Sensitivity");


            //LoanSpreadScenario
            modelBuilder.Entity<LoanSpreadScenario>().HasKey<int>(e => e.LoanSpreadScenarioId).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanSpreadScenario>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanSpreadScenario>().ToTable("ifrs_loan_classification_spread_Scenario");

            //UnquotedEquityFairvalueResult
            modelBuilder.Entity<UnquotedEquityFairvalueResult>().HasKey<int>(e => e.UnquotedEquityFairvalueResultId).Ignore(e => e.EntityId);
            modelBuilder.Entity<UnquotedEquityFairvalueResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<UnquotedEquityFairvalueResult>().ToTable("ifrs_unquotedequity_fairvalue_result");

            //PiTPDComparism
            modelBuilder.Entity<PiTPDComparism>().HasKey<int>(e => e.ComparismPDId).Ignore(e => e.EntityId);
            modelBuilder.Entity<PiTPDComparism>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PiTPDComparism>().ToTable("ifrs_pitpd_comparism");


            //MarkovMatrix
            modelBuilder.Entity<MarkovMatrix>().HasKey<int>(e => e.MarkovMatrixId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MarkovMatrix>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MarkovMatrix>().ToTable("ifrs_pit_markov_matrix");

            //MarkovMatrix
            modelBuilder.Entity<CCFModelling>().HasKey<int>(e => e.CCFModellingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CCFModelling>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CCFModelling>().ToTable("ifrs_ccf_modelling");

            //MarkovMatrix
            modelBuilder.Entity<HistoricalSectorialLGD>().HasKey<int>(e => e.HistoricalSectorialLGDId).Ignore(e => e.EntityId);
            modelBuilder.Entity<HistoricalSectorialLGD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<HistoricalSectorialLGD>().ToTable("ifrs_historical_sectorial_lgd");


            //ECLComparism
            modelBuilder.Entity<ECLComparism>().HasKey<int>(e => e.ECLComparismId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ECLComparism>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ECLComparism>().ToTable("ifrs_ecl_comparism");

            //OffBalanceSheetExposure
            modelBuilder.Entity<OffBalanceSheetExposure>().HasKey<int>(e => e.ObeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<OffBalanceSheetExposure>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OffBalanceSheetExposure>().ToTable("ifrs_lc_bg");

            //ForeignEADExchangeRate
            modelBuilder.Entity<ForeignEADExchangeRate>().HasKey<int>(e => e.ForeignEADExchangeRateId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ForeignEADExchangeRate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ForeignEADExchangeRate>().ToTable("Ifrs_forecasted_macroeconimcs_InterestRate");

            //Begin Victor IFRS9 ORM Segment

            //LocalBondSpread
            modelBuilder.Entity<LocalBondSpread>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<LocalBondSpread>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LocalBondSpread>().ToTable("ifrs_Local_bond_spread");

            //MarginalPDDistribution
            modelBuilder.Entity<MarginalPDDistribution>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<MarginalPDDistribution>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MarginalPDDistribution>().ToTable("ifrs_MarginalPD_distribution");

            //BondMarginalPDDistribution
            modelBuilder.Entity<BondMarginalPDDistribution>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<BondMarginalPDDistribution>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BondMarginalPDDistribution>().ToTable("ifrs_Bond_MarginalPD_distribution");

            //MarginalPDDistributionPlacement
            modelBuilder.Entity<MarginalPDDistributionPlacement>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<MarginalPDDistributionPlacement>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MarginalPDDistributionPlacement>().ToTable("ifrs_MarginalPD_distribution_placement");

            //LgdComputationResult
            modelBuilder.Entity<LgdComputationResult>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<LgdComputationResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LgdComputationResult>().ToTable("ifrs_lgd_computation_result");

            //LgdComputationResultPlacement
            modelBuilder.Entity<LgdComputationResultPlacement>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<LgdComputationResultPlacement>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LgdComputationResultPlacement>().ToTable("ifrs_lgd_computation_result_placement");

            //PlacementComputationResult
            modelBuilder.Entity<PlacementComputationResult>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<PlacementComputationResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PlacementComputationResult>().ToTable("ifrs_placement_computation_result");

            //TrialBalanceGap
            modelBuilder.Entity<EclComputationResult>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<EclComputationResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<EclComputationResult>().ToTable("ifrs_ecl_computation_result");

            //FinalEclOutputBonds
            modelBuilder.Entity<BondEclComputationResult>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<BondEclComputationResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BondEclComputationResult>().ToTable("ifrs_bond_ecl_computation_result");

            //FinalEclOutputPlacement
            modelBuilder.Entity<PlacementEclComputationResult>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<PlacementEclComputationResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PlacementEclComputationResult>().ToTable("ifrs_placement_ecl_computation_result");

            //FinalEclOutputLcBg
            modelBuilder.Entity<LcBgEclComputationResult>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<LcBgEclComputationResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LcBgEclComputationResult>().ToTable("ifrs_lc_bg_ecl_computation_result");

            //FinalEclOutputTBills
            modelBuilder.Entity<TBillEclComputationResult>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<TBillEclComputationResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TBillEclComputationResult>().ToTable("ifrs_TBill_ecl_computation_result");

            //EuroBondSpread
            modelBuilder.Entity<EuroBondSpread>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<EuroBondSpread>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<EuroBondSpread>().ToTable("ifrs_Euro_bond_spread");

            //End Victor IFRS9 ORM Segment
            //ComputedForcastedPDLGD
            modelBuilder.Entity<ComputedForcastedPDLGD>().HasKey<int>(e => e.ComputedPDId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ComputedForcastedPDLGD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ComputedForcastedPDLGD>().ToTable("Ifrs_computed_forcasted_pd_lgd_local");

            //ComputedForcastedPDLGDForeign
            modelBuilder.Entity<ComputedForcastedPDLGDForeign>().HasKey<int>(e => e.ComputedPDId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ComputedForcastedPDLGDForeign>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ComputedForcastedPDLGDForeign>().ToTable("Ifrs_computed_forcasted_pd_lgd_foreign");


            //MacroEconomicsNPL
            modelBuilder.Entity<MacroEconomicsNPL>().HasKey<int>(e => e.macroeconomicnplId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MacroEconomicsNPL>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MacroEconomicsNPL>().ToTable("ifrs_MacroEconomics_NPL");


            //MonthlyDiscountFactorBond
            modelBuilder.Entity<MonthlyDiscountFactorBond>().HasKey<int>(e => e.MonthlyDiscountFactorBond_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<MonthlyDiscountFactorBond>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MonthlyDiscountFactorBond>().ToTable("ifrs_monthly_discount_factor_bond");


            //IfrsPdSeriesByRating
            modelBuilder.Entity<IfrsPdSeriesByRating>().HasKey<int>(e => e.Sno).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsPdSeriesByRating>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsPdSeriesByRating>().ToTable("ifrs_pdseries_by_rating");


            //IfrsCorporateEcl
            modelBuilder.Entity<IfrsCorporateEcl>().HasKey<int>(e => e.ecl_id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsCorporateEcl>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsCorporateEcl>().ToTable("ifrs_corporate_ecl");


            //InvestmentOthersECL
            modelBuilder.Entity<InvestmentOthersECL>().HasKey<int>(e => e.ecl_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<InvestmentOthersECL>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<InvestmentOthersECL>().ToTable("ifrs_investment_and_others_ecl");


            //IfrsRetailPdSeries
            modelBuilder.Entity<IfrsRetailPdSeries>().HasKey<int>(e => e.PdSeriesId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsRetailPdSeries>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsRetailPdSeries>().ToTable("ifrs_retail_pdseries");


            //IfrsLgdScenarioByInstrument
            modelBuilder.Entity<IfrsLgdScenarioByInstrument>().HasKey<int>(e => e.InstrumentId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsLgdScenarioByInstrument>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsLgdScenarioByInstrument>().ToTable("ifrs_lgd_scenario_by_instrument");


            //IfrsSectorCCF
            modelBuilder.Entity<IfrsSectorCCF>().HasKey<int>(e => e.SectorId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsSectorCCF>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsSectorCCF>().ToTable("ifrs_sector_ccf_data");


            //MacroVarRecoveryRates
            modelBuilder.Entity<MacroVarRecoveryRates>().HasKey<int>(e => e.RecoveryRatesId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MacroVarRecoveryRates>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MacroVarRecoveryRates>().ToTable("ifrs_macrovariable_recovery_rates");


            //MonthlyDiscountFactorPlacement
            modelBuilder.Entity<MonthlyDiscountFactorPlacement>().HasKey<int>(e => e.MonthlyDiscountFactorPlacement_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<MonthlyDiscountFactorPlacement>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MonthlyDiscountFactorPlacement>().ToTable("ifrs_monthly_discount_factor_placement");


            //MonthlyDiscountFactor
            modelBuilder.Entity<MonthlyDiscountFactor>().HasKey<int>(e => e.MonthlyDiscountFactor_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<MonthlyDiscountFactor>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MonthlyDiscountFactor>().ToTable("ifrs_monthly_discount_factor");

            //ProbabilityWeighted
            modelBuilder.Entity<ProbabilityWeighted>().HasKey<int>(e => e.ProbabilityWeighted_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<ProbabilityWeighted>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ProbabilityWeighted>().ToTable("ifrs_probability_Weighted");


            //MacrovariableEstimate
            modelBuilder.Entity<MacrovariableEstimate>().HasKey<int>(e => e.MacrovariableEstimate_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<MacrovariableEstimate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MacrovariableEstimate>().ToTable("ifrs_macrovariable_estimates");


            //SectorMapping
            modelBuilder.Entity<SectorMapping>().HasKey<int>(e => e.SectorMapping_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<SectorMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SectorMapping>().ToTable("ifrs_sector_mapping");


            //BondSummary
            modelBuilder.Entity<BondSummary>().HasKey<int>(e => e.BondId).Ignore(e => e.EntityId);
            modelBuilder.Entity<BondSummary>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BondSummary>().ToTable("ifrs_bond_summary");


            //LedgerDetailSummary
            modelBuilder.Entity<LedgerDetailSummary>().HasKey<int>(e => e.SummaryId).Ignore(e => e.EntityId);
            modelBuilder.Entity<LedgerDetailSummary>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LedgerDetailSummary>().ToTable("ifrs_ledger_detail_summary");

            //SandPRating
            modelBuilder.Entity<SandPRating>().HasKey<int>(e => e.SandPRating_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<SandPRating>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SandPRating>().ToTable("ifrs_SandPRating");

            //LoanInterestRate
            modelBuilder.Entity<LoanInterestRate>().HasKey<int>(e => e.LoanInterestRate_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanInterestRate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanInterestRate>().ToTable("ifrs_loan_interest_rate");

            //HistoricalDefaultedAccounts
            modelBuilder.Entity<HistoricalDefaultedAccounts>().HasKey<int>(e => e.DefaultedAccountId).Ignore(e => e.EntityId);
            modelBuilder.Entity<HistoricalDefaultedAccounts>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<HistoricalDefaultedAccounts>().ToTable("ifrs_historical_defaulted_accounts");

            //OffBalancesheetECL
            modelBuilder.Entity<OffBalancesheetECL>().HasKey<int>(e => e.obe_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<OffBalancesheetECL>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OffBalancesheetECL>().ToTable("ifrs_final_OBE_output");

            //ImpairmentResultOBE
            modelBuilder.Entity<ImpairmentResultOBE>().HasKey<int>(e => e.Impairment_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<ImpairmentResultOBE>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ImpairmentResultOBE>().ToTable("ifrs_impairmentresult_obe");

            //ImpairmentCorporate
            modelBuilder.Entity<ImpairmentCorporate>().HasKey<int>(e => e.Corporate_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<ImpairmentCorporate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ImpairmentCorporate>().ToTable("ifrs_corporate_impairment");

            //ImpairmentInvestment
            modelBuilder.Entity<ImpairmentInvestment>().HasKey<int>(e => e.Investment_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<ImpairmentInvestment>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ImpairmentInvestment>().ToTable("ifrs_investment_impairment");

            //ImpairmentResultRetail
            modelBuilder.Entity<ImpairmentResultRetail>().HasKey<int>(e => e.Impairment_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<ImpairmentResultRetail>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ImpairmentResultRetail>().ToTable("ifrs_impairmentresult_retail");

            //IfrsFinalRetailOutput
            modelBuilder.Entity<IfrsFinalRetailOutput>().HasKey<int>(e => e.FinalRetailId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsFinalRetailOutput>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsFinalRetailOutput>().ToTable("ifrs_final_retail_output");

            //IfrsCustomerPD
            modelBuilder.Entity<IfrsCustomerPD>().HasKey<int>(e => e.CustomerPDId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsCustomerPD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsCustomerPD>().ToTable("ifrs_CustomerPD");


            //IfrsCorporatePdSeries
            modelBuilder.Entity<IfrsCorporatePdSeries>().HasKey<int>(e => e.Sno).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsCorporatePdSeries>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsCorporatePdSeries>().ToTable("ifrs_corporate_PDseries");

            //ECLInputRetail
            modelBuilder.Entity<ECLInputRetail>().HasKey<int>(e => e.EclInputRetailId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ECLInputRetail>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ECLInputRetail>().ToTable("ifrs_ECLInput_Retail");

            //StaffLoansComputationResult
            modelBuilder.Entity<StaffLoansComputationResult>().HasKey<int>(e => e.StaffLoan_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<StaffLoansComputationResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<StaffLoansComputationResult>().ToTable("ifrs_staff_loans_computation_result");

            //LoanCommitmentComputationResult
            modelBuilder.Entity<LoanCommitmentComputationResult>().HasKey<int>(e => e.LoanCommitmentComputationResult_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanCommitmentComputationResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanCommitmentComputationResult>().ToTable("ifrs_ecl_computation_result_loancommit");

            //MacroeconomicsVariableScenario
            modelBuilder.Entity<MacroeconomicsVariableScenario>().HasKey<int>(e => e.MacroeconomicsId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MacroeconomicsVariableScenario>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MacroeconomicsVariableScenario>().ToTable("ifrs_NPL_Macroeconomics_Variable");

            //LgdInputFactor
            modelBuilder.Entity<LgdInputFactor>().HasKey<int>(e => e.LgdInputFactorId).Ignore(e => e.EntityId);
            modelBuilder.Entity<LgdInputFactor>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LgdInputFactor>().ToTable("ifrs_LGD_Input_Factor");

            //RegressionOutput
            modelBuilder.Entity<RegressionOutput>().HasKey<int>(e => e.RegressionOutputId).Ignore(e => e.EntityId);
            modelBuilder.Entity<RegressionOutput>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<RegressionOutput>().ToTable("ifrs_Regression_Outputs");


            //---------------//
            //Access Bank Plc//
            //---------------//

            //Assumption
            modelBuilder.Entity<Assumption>().HasKey<int>(e => e.InstrumentID).Ignore(e => e.EntityId);
            modelBuilder.Entity<Assumption>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Assumption>().ToTable("ifrs_assumptions");

            //CollateralInput
            modelBuilder.Entity<CollateralInput>().HasKey<int>(e => e.Collateral_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<CollateralInput>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CollateralInput>().ToTable("ifrs_collateral_input");

            //SPCumulativePD
            modelBuilder.Entity<SPCumulativePD>().HasKey<int>(e => e.SPCumulative_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<SPCumulativePD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SPCumulativePD>().ToTable("ifrs_SP_CumulativePD");

            //GLAArchive
            modelBuilder.Entity<GLAArchive>().HasKey<int>(e => e.GLAdjustmentId).Ignore(e => e.EntityId);
            modelBuilder.Entity<GLAArchive>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<GLAArchive>().ToTable("ifrs_gladjustment_Archive");

            ////ifrs_bondsABP
            //modelBuilder.Entity<IFRSBondsABP>().HasKey<int>(e => e.BondId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<IFRSBondsABP>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<IFRSBondsABP>().ToTable("ifrs_bonds");

            ////OffBalanceSheetExposureABP
            //modelBuilder.Entity<OffBalanceSheetExposureABP>().HasKey<int>(e => e.ObeId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<OffBalanceSheetExposureABP>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<OffBalanceSheetExposureABP>().ToTable("ifrs_lc_bg");

            //OBExposure
            modelBuilder.Entity<OBExposure>().HasKey<int>(e => e.obe_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<OBExposure>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OBExposure>().ToTable("ifrs_obe_Input");



            //OBExposureCCF
            modelBuilder.Entity<OBExposureCCF>().HasKey<int>(e => e.obe_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<OBExposureCCF>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OBExposureCCF>().ToTable("ifrs_obe_Input");
            //modelBuilder.Entity<OffBalanceSheetExposureABP>().ToTable("ifrs_lc_bg");

            //OBExposure
            modelBuilder.Entity<OBExposure>().HasKey<int>(e => e.obe_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<OBExposure>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OBExposure>().ToTable("ifrs_OBE_Primary_Data");

            //File_COLLATERAL_INFORMATION
            modelBuilder.Entity<CollateralDetails>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<CollateralDetails>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CollateralDetails>().ToTable("File_COLLATERAL_INFORMATION");

            //File_FAC_CLASS_LOAN_OD_OBE
            modelBuilder.Entity<FacClassConsolidated>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<FacClassConsolidated>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FacClassConsolidated>().ToTable("File_FAC_CLASS_LOAN_OD_OBE");

            //ifrs_homogeneous_classification
            modelBuilder.Entity<HCClassification>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<HCClassification>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<HCClassification>().ToTable("ifrs_homogeneous_classification");

            //FacilityClassification
            modelBuilder.Entity<FacilityClassification>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<FacilityClassification>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FacilityClassification>().ToTable("ifrs_loan_classification");

            //File_LOAN_OD_RATINGS
            modelBuilder.Entity<FacRating>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<FacRating>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FacRating>().ToTable("File_LOAN_OD_RATINGS");

            //File_BONDS_AND_GTEE_STAGING
            modelBuilder.Entity<FacOBEStaging>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<FacOBEStaging>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FacOBEStaging>().ToTable("File_BONDS_AND_GTEE_STAGING");

            //Cashflow
            modelBuilder.Entity<Cashflow>().HasKey<int>(e => e.CashflowId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Cashflow>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Cashflow>().ToTable("ifrs_cashflow");

            //File_LOAN_AND_OD_STAGING
            modelBuilder.Entity<FacStaging>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<FacStaging>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FacStaging>().ToTable("File_LOAN_AND_OD_STAGING");

            //FacilityStaging
            modelBuilder.Entity<FacilityStaging>().HasKey<int>(e => e.facId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FacilityStaging>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FacilityStaging>().ToTable("ifrs_facilities_staging");


            //SICRParameters
            modelBuilder.Entity<SICRParameters>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<SICRParameters>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SICRParameters>().ToTable("ifrs_loan_sicr");

            //MarginalCCFStr
            modelBuilder.Entity<MarginalCCFStr>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<MarginalCCFStr>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MarginalCCFStr>().ToTable("ifrs_MarginalCCF_STRLB");
            //LoansECLComputationResult
            modelBuilder.Entity<LoansECLComputationResult>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoansECLComputationResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoansECLComputationResult>().ToTable("ifrs_ecl_computation_result_stb");
            //ObeEclComputation
            modelBuilder.Entity<ObeEclComputation>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<ObeEclComputation>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ObeEclComputation>().ToTable("ifrs_OBE_ecl_computation_result");
            //LoanSignificantFlag
            modelBuilder.Entity<LoanSignificantFlag>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanSignificantFlag>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanSignificantFlag>().ToTable("IFRS_LoanClassification_SignificantFlag");

            ///////IFRS INVESTMENT
            modelBuilder.Entity<IfrsInvestment>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsInvestment>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsInvestment>().ToTable("Ifrs_Investment_Classification");

            /////IFRS BOND LGD
            modelBuilder.Entity<IfrsBondLGD>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsBondLGD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsBondLGD>().ToTable("ifrs_Bonds_lgd_computation_result");

            //MarginalPddSTRLB
            modelBuilder.Entity<MarginalPddSTRLB>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<MarginalPddSTRLB>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MarginalPddSTRLB>().ToTable("ifrs_MarginalPD_distribution_STRLB");

            //ODEclComputationResult
            modelBuilder.Entity<ODEclComputationResult>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<ODEclComputationResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ODEclComputationResult>().ToTable("ifrs_OD_ecl_computation_result");

            //MarginalPdObeDistr
            modelBuilder.Entity<MarginalPdObeDistr>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<MarginalPdObeDistr>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MarginalPdObeDistr>().ToTable("ifrs_MarginalPD_OBE_distribution_STRLB");

            //MarginalPdObeDistr
            modelBuilder.Entity<MarginalPdODDistr>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<MarginalPdODDistr>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MarginalPdODDistr>().ToTable("ifrs_MarginalPD_OD_distribution_STRLB");


            //LGDComptResult
            modelBuilder.Entity<LGDComptResult>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<LGDComptResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LGDComptResult>().ToTable("ifrs_lgd_computation_result_strlb");

            //ObeLGDComptResult
            modelBuilder.Entity<ObeLGDComptResult>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<ObeLGDComptResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ObeLGDComptResult>().ToTable("ifrs_OBE_lgd_computation_result");

            /////IFRS BONDS MONTHLY EAD
            modelBuilder.Entity<IfrsBondsMonthlyEAD>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsBondsMonthlyEAD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsBondsMonthlyEAD>().ToTable("ifrs_bonds_monthly_exposureatdefault");

            /////IFRS MONTHLY EAD
            modelBuilder.Entity<IfrsMonthlyEAD>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsMonthlyEAD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsMonthlyEAD>().ToTable("ifrs_monthly_exposureatdefaultWithPrePayment");

            /////LoanClassificationSICRSignFlag
            modelBuilder.Entity<LoanClassificationSICRSignFlag>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanClassificationSICRSignFlag>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanClassificationSICRSignFlag>().ToTable("ifrs_loanclassification_sicrbysignificantflag");

            //LoanECLResult
            modelBuilder.Entity<LoanECLResult>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanECLResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanECLResult>().ToTable("ifrs_Loan_ECL_Result");

            //ObeECLResult
            modelBuilder.Entity<ObeECLResult>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<ObeECLResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ObeECLResult>().ToTable("ifrs_OBE_ECL_Result");

            //BondsECLResult
            modelBuilder.Entity<BondsECLResult>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<BondsECLResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BondsECLResult>().ToTable("ifrs_Bonds_ECL_Result");

            //OdECLResult
            modelBuilder.Entity<OdECLResult>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<OdECLResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OdECLResult>().ToTable("ifrs_OD_ECL_Result");

            //CollateralGrowthRate
            modelBuilder.Entity<CollateralGrowthRate>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<CollateralGrowthRate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CollateralGrowthRate>().ToTable("ifrs_collateral_GrowthRate");

            //CummulativeDefaultAmt
            modelBuilder.Entity<CummulativeDefaultAmt>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<CummulativeDefaultAmt>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CummulativeDefaultAmt>().ToTable("ifrs_Cummulative_DefaultAmt");

            //CummulativeLifetimePd
            modelBuilder.Entity<CummulativeLifetimePd>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<CummulativeLifetimePd>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CummulativeLifetimePd>().ToTable("ifrs_CummulativeLifeTimePD");

            //CollateralRecAmtStaging
            modelBuilder.Entity<CollateralRecAmtStaging>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<CollateralRecAmtStaging>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CollateralRecAmtStaging>().ToTable("Ifrs_CollateralRecoverableAmount_Staging");

            //HistoricalDefaultFreq
            modelBuilder.Entity<HistoricalDefaultFreq>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<HistoricalDefaultFreq>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<HistoricalDefaultFreq>().ToTable("ifrs_HistoricalDefault_freq");

            //CummulativePD
            modelBuilder.Entity<CummulativePD>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<CummulativePD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CummulativePD>().ToTable("ifrs_CummulativePD");


            //InvestmentMarginalPd
            modelBuilder.Entity<InvestmentMarginalPd>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<InvestmentMarginalPd>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<InvestmentMarginalPd>().ToTable("Iifrs_Investment_MarginalPD_STRLB_Display");

            /////////////////////////////////////////////////////////////////////////

            //////// IFRSProjectedCumm
            modelBuilder.Entity<IfrsProjectedCummDefaultFrq>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsProjectedCummDefaultFrq>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsProjectedCummDefaultFrq>().ToTable("ifrs_ProjectedCummDefaultFrq_Forcasted_STRLB");

            ///////// IfrsMonthlyForwardPDMacroVar
            modelBuilder.Entity<IfrsMonthlyForwardPDMacroVar>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsMonthlyForwardPDMacroVar>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsMonthlyForwardPDMacroVar>().ToTable("ifrs_MonthlyForwardPDWithMacroVar_STRL_Display");

            ///////////////// IFRSMarginalPDByScenerio

            modelBuilder.Entity<IfrsMarginalPDByScenerio>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsMarginalPDByScenerio>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsMarginalPDByScenerio>().ToTable("ifrs_MarginalPDByScenerio_STRLB_Display");

            /// IfrsPdTermStructure

            modelBuilder.Entity<IfrsPdTermStructure>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsPdTermStructure>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsPdTermStructure>().ToTable("ifrs_PDTermStructure");

            //ConsolidatedLoans
            modelBuilder.Entity<ConsolidatedLoans>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<ConsolidatedLoans>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ConsolidatedLoans>().ToTable("ifrs_ConsolidatedLoans");

            //OverdraftMonthlyEAD
            modelBuilder.Entity<OverdraftMonthlyEAD>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<OverdraftMonthlyEAD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OverdraftMonthlyEAD>().ToTable("ifrs_OD_Monthly_exposureatdefault");


            /////////////Tosin
            ///////////////////////////////////IFRSLOANSINFO
            modelBuilder.Entity<IfrsLoansInfo>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsLoansInfo>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsLoansInfo>().ToTable("ifrs_loansInfo");

            ///////////////////////////////////IFRSLgdProjections
            modelBuilder.Entity<IfrsLgdProjections>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsLgdProjections>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsLgdProjections>().ToTable("ifrs_Lgd_Projections");


            ///////////////////////////////////IFRSPDProjection
            modelBuilder.Entity<IfrsPDProjection>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsPDProjection>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsPDProjection>().ToTable("ifrs_PD_Projection");


            //Overdraft LGD Computation
            modelBuilder.Entity<OverdraftLGDComputation>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<OverdraftLGDComputation>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OverdraftLGDComputation>().ToTable("ifrs_OD_lgd_computation_result");

            //Calendar
            modelBuilder.Entity<Calendar>().HasKey<int>(e => e.CalId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Calendar>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Calendar>().ToTable("ifrs_calendar");

            //Calendar
            modelBuilder.Entity<InterimQualitativeNote>().HasKey<int>(e => e.InterimQualitativeNoteId).Ignore(e => e.EntityId);
            modelBuilder.Entity<InterimQualitativeNote>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<InterimQualitativeNote>().ToTable("ifrs_report_qualitativenotes");







            //PostingGLMapping
            modelBuilder.Entity<PostingGLMapping>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<PostingGLMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PostingGLMapping>().ToTable("ifrs_PostingGLMapping");

            //ExceptionReport 
            modelBuilder.Entity<ifrsexceptionreport>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<ifrsexceptionreport>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ifrsexceptionreport>().ToTable("ifrs_exception_report");

            modelBuilder.Entity<CashFlowTB>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<CashFlowTB>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CashFlowTB>().ToTable("ifrs_loansrawcashflow");

            modelBuilder.Entity<AmortizationOutput>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<AmortizationOutput>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<AmortizationOutput>().ToTable("ifrs_amortization_output");

            modelBuilder.Entity<SegmentPerformance>().HasKey<int>(e => e.SegmentId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SegmentPerformance>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SegmentPerformance>().ToTable("ifrs_segment_Performance");





            //////////////////////////////////////////////////IFRS Loan Miss Payment
            modelBuilder.Entity<IfrsLoanMissPayment>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsLoanMissPayment>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsLoanMissPayment>().ToTable("ifrs_Loan_MissPayment");

            ///////////////////////////////////////////////////////////////MacroeconomicForecast
            modelBuilder.Entity<MacroEconomicForeCast>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<MacroEconomicForeCast>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MacroEconomicForeCast>().ToTable("ifrs_MacroEcononicVariables_ForcastPD");




            //////////////////////////////////////////////////Regression Cofficient
            modelBuilder.Entity<RegressionCofficient>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<RegressionCofficient>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<RegressionCofficient>().ToTable("ifrs_Regression_Coefficient");

            ///////////////////////////////////IfrsOverdraftData
            modelBuilder.Entity<IfrsOverdraftData>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsOverdraftData>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsOverdraftData>().ToTable("ifrs_overDraftData");

            ///////////////////////////////////MacroNPL
            modelBuilder.Entity<MacroNPL>().HasKey<int>(e => e.MacroID).Ignore(e => e.EntityId);
            modelBuilder.Entity<MacroNPL>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MacroNPL>().ToTable("Macro__NPL");

            ///////////////////////////////////ProbabilisticModel
            modelBuilder.Entity<ProbabilisticModel>().HasKey<int>(e => e.ProbId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ProbabilisticModel>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ProbabilisticModel>().ToTable("ifrs_probabilistic_model");

            //ModificationGainorLoss
            modelBuilder.Entity<ModificationGainorLoss>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<ModificationGainorLoss>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ModificationGainorLoss>().ToTable("ifrs_Modification_FairValueGainLoss");


            //CashFlowRestructure
            modelBuilder.Entity<CashFlowRestructure>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<CashFlowRestructure>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CashFlowRestructure>().ToTable("ifrs_Loan_CashFlow_Restructure");




            //RestructureInfo
            modelBuilder.Entity<RestructureInfo>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<RestructureInfo>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<RestructureInfo>().ToTable("ifrs_Loan_Restructure_Info");





            ///////////////////////////////////////////////// LoanRestructurePVCashFlow
            modelBuilder.Entity<LoanRestructurePVCashFlow>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanRestructurePVCashFlow>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanRestructurePVCashFlow>().ToTable("ifrs_Loan_Restructure_PVCashFlow_STRLB");

            ///////////////////////////////////////////////// CummulativeMatrix
            modelBuilder.Entity<CummulativeMatrix>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<CummulativeMatrix>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CummulativeMatrix>().ToTable("ifrs_cummulative_matrixes");

            ///////////////////////////////////////////////// AdjustedMatrix
            modelBuilder.Entity<AdjustedMatrix>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<AdjustedMatrix>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<AdjustedMatrix>().ToTable("ifrs_adjusted_matrix");


            ///////////////////////////////////////////////// CreditIndex
            modelBuilder.Entity<CreditIndex>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<CreditIndex>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CreditIndex>().ToTable("ifrs_credit_index");

            //////////////////////////////////////////////////PDMigrationMatrixFinal 
            modelBuilder.Entity<PDMigrationMatrixFinal>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<PDMigrationMatrixFinal>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PDMigrationMatrixFinal>().ToTable("ifrs_pdmigrationmatrixfinal");


            ///////////////////////////////////////////////// FacilitiesStageMigration
            modelBuilder.Entity<FacilitiesStageMigration>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<FacilitiesStageMigration>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FacilitiesStageMigration>().ToTable("ifrs_facilities_stage_migration");


            ///////////////////////////////////////////////// CummulativePDD
            modelBuilder.Entity<CummulativePDD>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<CummulativePDD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CummulativePDD>().ToTable("ifrs_CummilativePD");


            ///////////////////////////////////////////////// MarginalOutput
            modelBuilder.Entity<MarginalOutput>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<MarginalOutput>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MarginalOutput>().ToTable("ifrs_marginal_output");

            ///////////////////////////////////////////////// HistoricalMacroVariableInput
            modelBuilder.Entity<HistoricalMacroVariableInput>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<HistoricalMacroVariableInput>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<HistoricalMacroVariableInput>().ToTable("Historical_MacroVariable_Input");


            ///////////////////////////////////////////////// IfrsScalarUpload
            modelBuilder.Entity<IfrsScalarUpload>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsScalarUpload>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsScalarUpload>().ToTable("ifrs_scalar_upload");


            //////////////////////////////////////////////////Regressionweights 
            modelBuilder.Entity<Regressionweights>().HasKey<int>(e => e.weight_id).Ignore(e => e.EntityId);
            modelBuilder.Entity<Regressionweights>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Regressionweights>().ToTable("ifrs_regression_weights");



            //////////////////////////////////////////////////Mevlisting 
            modelBuilder.Entity<Mevlisting>().HasKey<int>(e => e.listing_id).Ignore(e => e.EntityId);
            modelBuilder.Entity<Mevlisting>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Mevlisting>().ToTable("ifrs_MEV_listing");

            ///////////////////////////////////////////////// IfrsHistoricalMEVAbp
            modelBuilder.Entity<IfrsHistoricalMEVAbp>().HasKey<int>(e => e.HistoricalMevId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsHistoricalMEVAbp>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsHistoricalMEVAbp>().ToTable("ifrs_historical_MEV");


            ///////////////////////////////////////////////// IfrsConfidenceIntervalAbp
            modelBuilder.Entity<IfrsConfidenceIntervalAbp>().HasKey<int>(e => e.IntervalId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsConfidenceIntervalAbp>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsConfidenceIntervalAbp>().ToTable("ifrs_confidence_interval");

            ///////////////////////////////////////////////// IfrsMevForcastabp
            modelBuilder.Entity<IfrsMevForcastabp>().HasKey<int>(e => e.Forecast_id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsMevForcastabp>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsMevForcastabp>().ToTable("ifrs_Mev_forcast");

            ///////////////////////////////////////////////// IfrsAccessEstimateRecoveryOutput
            modelBuilder.Entity<IfrsAccessEstimateRecoveryOutput>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsAccessEstimateRecoveryOutput>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsAccessEstimateRecoveryOutput>().ToTable("IFRS_Access_Estimate_Recovery_output");


            //////////////////////////////////////////////////IfrsAccessLGDDiscountFactorOutput 
            modelBuilder.Entity<IfrsAccessLGDDiscountFactorOutput>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsAccessLGDDiscountFactorOutput>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsAccessLGDDiscountFactorOutput>().ToTable("IFRS_Access_LGD_Discount_factor_output_dennis");


            //////////////////////////////////////////////////IfrsAccessLGDOutput 
            modelBuilder.Entity<IfrsAccessLGDOutput>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsAccessLGDOutput>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsAccessLGDOutput>().ToTable("IFRS_Access_LGD_OUTPUT_DENNIS");

            ///////////////////////////////////////////////// IfrsAccessttcDownTurnResult
            modelBuilder.Entity<IfrsAccessttcDownTurnResult>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsAccessttcDownTurnResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsAccessttcDownTurnResult>().ToTable("IFRS_Access_ttc_DownTurn_Result");


            ///////////////////////////////////////////////// IfrsRecoveryOutputnewAccessModel
            modelBuilder.Entity<IfrsRecoveryOutputnewAccessModel>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsRecoveryOutputnewAccessModel>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsRecoveryOutputnewAccessModel>().ToTable("IFRS_Recovery_Output_newAccess_MODEL_Dennis");

            ///////////////////////////////////////////////// Test
            modelBuilder.Entity<Test>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<Test>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Test>().ToTable("Test");

            ///////////////////////////TosinUpdate
            ///
            ///////////////////////////////////IfrsCureRatesRecoveryRates
            modelBuilder.Entity<IfrsCureRatesRecoveryRates>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsCureRatesRecoveryRates>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsCureRatesRecoveryRates>().ToTable("Ifrs_CureRates_RecoveryRates_Display");

            ///////////////////////////////////IFRSDataValidator
            modelBuilder.Entity<IfrsDataValidator>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsDataValidator>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsDataValidator>().ToTable("ifrs_DataValidator");

            ///////////////////////////////////IfrsInvestmentECLSummary
            modelBuilder.Entity<IfrsInvestmentECLSummary>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsInvestmentECLSummary>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsInvestmentECLSummary>().ToTable("Ifrs_Investment_ECL_Summary");

            ///////////////////////////UnquotedEquity
            modelBuilder.Entity<UnquotedEquityInput>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<UnquotedEquityInput>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<UnquotedEquityInput>().ToTable("IFRS_UnQuotedEquity_Input");

            modelBuilder.Entity<UnquotedEquityMedAVGPB>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<UnquotedEquityMedAVGPB>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<UnquotedEquityMedAVGPB>().ToTable("IFRS_UnquotedEquity_MedAVGPB");

            modelBuilder.Entity<UnquotedEquityMedAVGPE>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<UnquotedEquityMedAVGPE>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<UnquotedEquityMedAVGPE>().ToTable("IFRS_UnquotedEquity_MedAVGPE");

            modelBuilder.Entity<UnquotedEquityPEPBRatio>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<UnquotedEquityPEPBRatio>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<UnquotedEquityPEPBRatio>().ToTable("IFRS_UnQuotedEquity_PEPB_Ratio");

            modelBuilder.Entity<UnquotedEquitySummaryInput>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<UnquotedEquitySummaryInput>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<UnquotedEquitySummaryInput>().ToTable("IFRS_UnquotedEquity_Summary_Input");

            modelBuilder.Entity<UEQUnquotedEquitySummaryReport>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<UEQUnquotedEquitySummaryReport>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<UEQUnquotedEquitySummaryReport>().ToTable("UEQ_UnquotedEquity_SummaryReport");

            modelBuilder.Entity<UnquotedEquityMarketabilityDiscount>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<UnquotedEquityMarketabilityDiscount>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<UnquotedEquityMarketabilityDiscount>().ToTable("ifrs_UnQuotedEquity_Marketability_Discount");

            modelBuilder.Entity<UnquotedEquityCountryRiskDisc>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<UnquotedEquityCountryRiskDisc>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<UnquotedEquityCountryRiskDisc>().ToTable("IFRS_UnquotedEquity_CountryRiskDisc");

            modelBuilder.Entity<UnquotedEquityMKTRDisc>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<UnquotedEquityMKTRDisc>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<UnquotedEquityMKTRDisc>().ToTable("IFRS_UnquotedEquity_MKTRDisc");

            modelBuilder.Entity<IfrsBenefitsStaffLoan>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsBenefitsStaffLoan>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsBenefitsStaffLoan>().ToTable("ifrs_benefits_staff_loan");

            modelBuilder.Entity<IfrsGetCashFlowEIR>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsGetCashFlowEIR>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsGetCashFlowEIR>().ToTable("ifrs_modification_gain_or_loss");

            modelBuilder.Entity<IfrsDescriptionLedger>().HasKey<int>(e => e.id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsDescriptionLedger>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsDescriptionLedger>().ToTable("ifrs_description_ledger");

            modelBuilder.Entity<IfrsRepaymentSchedule>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsRepaymentSchedule>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsRepaymentSchedule>().ToTable("ifrs_repayment_schedule");

            modelBuilder.Entity<IfrsModicationSummary>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsModicationSummary>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsModicationSummary>().ToTable("ifrs_modication_summary");

            modelBuilder.Entity<IfrsMevRaltionship>().HasKey<int>(e => e.rel_id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsMevRaltionship>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsMevRaltionship>().ToTable("ifrs_mev_raltionship");

            modelBuilder.Entity<IfrsStatisticalRelationshipExpect>().HasKey<int>(e => e.sta_id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsStatisticalRelationshipExpect>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsStatisticalRelationshipExpect>().ToTable("ifrs_statistical_relationship_expect");

            modelBuilder.Entity<IfrsLifetimePDStages>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsLifetimePDStages>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsLifetimePDStages>().ToTable("ifrs_LifetimePD_Stages");

            modelBuilder.Entity<IfrsTransitionalMatrix>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsTransitionalMatrix>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsTransitionalMatrix>().ToTable("ifrs_transitional_Matrix");

            modelBuilder.Entity<SubSector>().HasKey<int>(e => e.SubSectorId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SubSector>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SubSector>().ToTable("ifrs_subsector_listing");

            modelBuilder.Entity<IfrsStaffBenefitsLoans>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsStaffBenefitsLoans>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsStaffBenefitsLoans>().ToTable("ifrs_staff_benefits_loans");

            modelBuilder.Entity<IfrsStaffBenefitsReportSummary>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsStaffBenefitsReportSummary>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsStaffBenefitsReportSummary>().ToTable("ifrs_staff_benefits_report_summary");

            modelBuilder.Entity<IfrsStaffBenefitsLoansReport>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsStaffBenefitsLoansReport>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsStaffBenefitsLoansReport>().ToTable("ifrs_staff_benefits_loans_report");
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

                connectionString = string.Format("Data Source= {0};Initial Catalog={1};User ={2};Password={3};Integrated Security={4}", companydbServerName, companydbDatabaseName, companydbUserName, companydbPassword, companydb.Database.IntegratedSecurity);
            }

            return connectionString;
        }

    }
}
