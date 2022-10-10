using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Business.IFRS.Contracts
{
    [ServiceContract]
    public interface IExtractedDataService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();
     
        #region IFRSBonds

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IFRSBonds UpdateIFRSBonds(IFRSBonds IFRSBonds);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIFRSBonds(int bondId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IFRSBonds GetIFRSBonds(int bondId);

        [OperationContract]
        IFRSBonds[] GetAllIFRSBonds();

        [OperationContract]
        IFRSBonds[] GetBondsByClassification(string classification);

        [OperationContract]
        IFRSBonds[] GetbondsByMaturityDate(DateTime matureDate);

        [OperationContract]
        void UpdatebondsByMaturityDate(DateTime matureDate, decimal cmprice);
    

        #endregion

        #region IFRSTbills

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IFRSTbills UpdateIFRSTbills(IFRSTbills IFRSTbills);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIFRSTbills(int tbillId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IFRSTbills GetIFRSTbills(int tbillId);

        [OperationContract]
        IFRSTbills[] GetAllIFRSTbills();

        [OperationContract]
        IFRSTbills[] GetTbillsByClassification(string classification, int type);
        [OperationContract]
        IFRSTbills[] GetTbillsByMaturityDate(DateTime matureDate, int type);

        [OperationContract]
        void UpdateTbillsByMaturityDate(DateTime matureDate, decimal cmprice);

        [OperationContract]
        IFRSTbills[] GetListByType(int Type);

        #endregion        

        #region LoanPry

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        LoanPry UpdateLoanPry(LoanPry loanPry);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteLoanPry(int pryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LoanPry GetLoanPry(int pryId);

        [OperationContract]
        LoanPry[] GetAllLoanPry();

        [OperationContract]
        LoanPry[] GetLoanPryByScheduleType(string schType);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LoanPry[] GetPryLoanBySearch(string searchParam);

        [OperationContract]
        LoanPry[] GetPryLoans(int defaultCount, string path);



        #endregion

        #region RawLoanDetails

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        RawLoanDetails UpdateRawLoanDetails(RawLoanDetails loanDetails);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRawLoanDetails(int loanDetailId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        RawLoanDetails GetRawLoanDetails(int loanDetailId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        RawLoanDetails[] GetAllRawLoanDetails(int defaultCount,string path);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        RawLoanDetails[] GetAllLoanDetailsBySearch(string searchParam);

        [OperationContract]
        void UpdateLoanClassNotch(string refNo, string rating, int stage, string notes);

        [OperationContract]
        void DeleteLoanDetailsNotch(string refNo);

        [OperationContract]
        CollateralRecov[] ComputeRecovAmt(string refNo, string collateralType, double collateralValue);

        [OperationContract]
        RawLoanDetails[] GetAllLoanDetails(int defaultCount, string path);

        #endregion

        #region IntegralFee

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IntegralFee UpdateIntegralFee(IntegralFee integralFee);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIntegralFee(int integralFeeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IntegralFee GetIntegralFee(int integralFeeId);

        [OperationContract]
        IntegralFee[] GetAllIntegralFee();

        #endregion

        #region IfrsCustomer

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IfrsCustomer UpdateIfrsCustomer(IfrsCustomer ifrsCustomer);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIfrsCustomer(int customerId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IfrsCustomer GetIfrsCustomer(int customerId);

        [OperationContract]
        IfrsCustomer[] GetAllIfrsCustomer();

        [OperationContract]
        IfrsCustomer[] GetIfrsCustomerByRating(string rating);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IfrsCustomer[] GetCustomerInfoBySearch(string searchParam);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IfrsCustomer[] GetCustomers(int defaultCount);
        #endregion

        #region IfrsCustomerAccount

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IfrsCustomerAccount UpdateIfrsCustomerAccount(IfrsCustomerAccount ifrsCustomerAccount);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIfrsCustomerAccount(int custAccountId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IfrsCustomerAccount GetIfrsCustomerAccount(int custAccountId);

        [OperationContract]
        IfrsCustomerAccount[] GetAllIfrsCustomerAccount();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        string[] GetDistinctSector();

        #endregion

        #region UnMappedProducts

        [OperationContract]
        UnMappedProduct[] GetAllUnMappedProducts();

        #endregion

        #region LoanPryMoratorium

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        LoanPryMoratorium UpdateLoanPryMoratorium(LoanPryMoratorium loanPryMoratorium);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteLoanPryMoratorium(int loanPryMoratoriumId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LoanPryMoratorium GetLoanPryMoratorium(int loanPryMoratoriumId);

        [OperationContract]
        LoanPryMoratorium[] GetAllLoanPryMoratorium();

        #endregion

        #region Borrowings

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Borrowings UpdateBorrowings(Borrowings borrowings);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBorrowings(int borrowingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Borrowings GetBorrowings(int borrowingId);

        [OperationContract]
        Borrowings[] GetAllBorrowings();

        
        #endregion

        #region OffBalanceSheetExposure

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OffBalanceSheetExposure UpdateOffBalanceSheetExposure(OffBalanceSheetExposure offBalanceSheetExposure);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOffBalanceSheetExposure(int obeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OffBalanceSheetExposure GetOffBalanceSheetExposure(int obeId);

        [OperationContract]
        OffBalanceSheetExposure[] GetAllOffBalanceSheetExposure();

        [OperationContract]
        OffBalanceSheetExposure[] GetOffBalanceSheetExposureByPortfolio(string portfolio);

        //[OperationContract]
        //OffBalanceSheetExposure[] GetTbillsByMaturityDate(DateTime matureDate);

        //[OperationContract]
        //void UpdateTbillsByMaturityDate(DateTime matureDate, decimal cmprice);

        #endregion

        #region Placement

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Placement UpdatePlacement(Placement placement);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePlacement(int Placement_Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Placement GetPlacement(int Placement_Id);

        [OperationContract]
        Placement[] GetAllPlacements();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Placement[] GetAvailablePlacement(int defaultCount, string path);
        
        #endregion

        #region LoanInterestRate

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        LoanInterestRate UpdateLoanInterestRate(LoanInterestRate loanInterestRate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteLoanInterestRate(int LoanInterestRate_Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LoanInterestRate GetLoanInterestRate(int LoanInterestRate_Id);

        [OperationContract]
        LoanInterestRate[] GetAllLoanInterestRates();

        //[OperationContract]
        //LoanInterestRate[] GetLoanInterestRateByRefNo(string RefNo);

        #endregion

        #region LoanCommitment

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        LoanCommitment UpdateLoanCommitment(LoanCommitment loanCommitment);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteLoanCommitment(int LoanCommitmentId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LoanCommitment GetLoanCommitment(int LoanCommitmentId);

        [OperationContract]
        LoanCommitment[] GetAllLoanCommitments();

        //[OperationContract]
        //LoanCommitment[] GetLoanCommitmentByRefNo(string RefNo);

        #endregion

        #region DepositRepaymentSchedule

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        DepositRepaymentSchedule UpdateDepositRepaymentSchedule(DepositRepaymentSchedule depositRepaymentSchedule);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteDepositRepaymentSchedule(int depositRepayId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        DepositRepaymentSchedule GetDepositRepaymentSchedule(int depositRepayId);

        [OperationContract]
        DepositRepaymentSchedule[] GetAllDepositRepaymentSchedule();

        [OperationContract]
        DepositRepaymentSchedule[] GetVarianceData();

        #endregion

        #region LiabilityRepaymentSchedule

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        LiabilityRepaymentSchedule UpdateLiabilityRepaymentSchedule(LiabilityRepaymentSchedule liabilityRepaymentSchedule);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteLiabilityRepaymentSchedule(int liabilityRepayId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LiabilityRepaymentSchedule GetLiabilityRepaymentSchedule(int liabilityRepayId);

        [OperationContract]
        LiabilityRepaymentSchedule[] GetAllLiabilityRepaymentSchedule();

        #endregion

        #region InputDetail

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        InputDetail UpdateInputDetail(InputDetail inputDetail);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteInputDetail(int InputDetailId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        InputDetail GetInputDetail(int InputDetailId);

        [OperationContract]
        InputDetail[] GetAllInputDetails();

        [OperationContract]
        EclWeightedAvg[] GetAllEclWeightedAvgs();

        [OperationContract]
        int InsertByRefno(string refNo);

        [OperationContract]
        void ComputeEcl();

        #endregion

        #region NseIndex

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        NseIndex UpdateNseIndex(NseIndex inputDetail);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteNseIndex(int NseIndexId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        NseIndex GetNseIndex(int NseIndexId);

        [OperationContract]
        NseIndex[] GetAllNseIndexs();

        [OperationContract]
        ProbabilityWeight[] GetAllProbabilityWeights();

        [OperationContract]
        void ComputeProbabilityWeight(double lOC);

        #endregion

        #region OBExposure

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OBExposure UpdateOBExposure(OBExposure OBExposure);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOBExposure(int obe_Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OBExposure[] GetOBExposureBySearch(int flag, string searchParam);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OBExposure GetOBExposurebyId(int obeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OBExposure[] GetOBExposure(int flag, int defaultCount, string path);


        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        string[] GetProductTypes();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        string[] GetsubTypes(string producType);

        #endregion

        #region OBExposureCCF

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OBExposureCCF UpdateOBExposureCCF(OBExposureCCF OBExposureCCF);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOBExposureCCF(int obe_Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OBExposureCCF[] GetOBExposureCCFBySearch(int flag, string searchParam);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OBExposureCCF GetOBExposureCCFbyId(int obeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OBExposureCCF[] GetOBExposureCCF(int flag, int defaultCount, string path);

        #endregion

        #region CollateralDetails

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CollateralDetails[] GetCollateralDetailsBySearch(string searchParam);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CollateralDetails[] GetCollateralDetails(int defaultCount, string path);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CollateralDetails GetCollateralDetailsById(int colDetsId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CollateralDetails UpdateCollateralDetails(CollateralDetails CollateralDetails);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCollateralDetails(int coldets_Id);

        #endregion

        #region HCClassification

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        HCClassification[] GetAllHCClassification();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        HCClassification GetHCClassificationById(int Id);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        HCClassification UpdateHCClassification(HCClassification HCClassification);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteHCClassification(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        HCClassification[] GetHCClassificationBySearch(string searchParam);

        #endregion

        #region FacClassConsolidated

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FacClassConsolidated[] GetFacClassConsolidatedBySearch(string searchParam);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FacClassConsolidated[] GetFacClassConsolidated(int defaultCount, string path);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FacClassConsolidated GetFacClassConsolidatedById(int colDetsId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FacClassConsolidated UpdateFacClassConsolidated(FacClassConsolidated FacClassConsolidated);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFacClassConsolidated(int coldets_Id);

        #endregion

        #region FacRating

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FacRating[] GetFacRatingBySearch(string searchParam);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FacRating[] GetFacRating(int defaultCount, string path);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FacRating GetFacRatingById(int facId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FacRating UpdateFacRating(FacRating FacRating);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFacRating(int fac_Id);

        #endregion

        #region FacStaging

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FacStaging[] GetFacStagingBySearch(string searchParam);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FacStaging[] GetFacStaging(int defaultCount, string path);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FacStaging GetFacStagingById(int facId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FacStaging UpdateFacStaging(FacStaging FacStaging);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFacStaging(int fac_Id);

        #endregion

        #region FacOBEStaging

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FacOBEStaging[] GetFacOBEStagingBySearch(string searchParam);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FacOBEStaging[] GetFacOBEStaging(int defaultCount, string path);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FacOBEStaging GetFacOBEStagingById(int facId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FacOBEStaging UpdateFacOBEStaging(FacOBEStaging FacOBEStaging);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFacOBEStaging(int fac_Id);

        #endregion


        #region Cashflow

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Cashflow UpdateCashflow(Cashflow loanPry);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCashflow(int pryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Cashflow GetCashflow(int pryId);

        [OperationContract]
        Cashflow[] GetAllCashflow();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Cashflow[] GetCashflowBySearch(string searchParam);

        [OperationContract]
        Cashflow[] GetCashflows(int defaultCount);

        #endregion

        
    }
}
