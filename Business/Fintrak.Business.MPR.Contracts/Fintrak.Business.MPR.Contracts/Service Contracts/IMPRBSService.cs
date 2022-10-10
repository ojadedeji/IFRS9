using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;
using System.Collections.Generic;

namespace Fintrak.Business.MPR.Contracts
{
    [ServiceContract]
    public interface IMPRBSService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();


        #region BSCaption

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BSCaption UpdateBSCaption(BSCaption bsCaption);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBSCaption(int bsCaptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BSCaption GetBSCaption(int bsCaptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BSCaption[] GetBSCaptions();

        [OperationContract]
        BSCaptionData[] GetAllBSCaptions();

        [OperationContract]
        BSCaptionData[] GetAllMPRBSCaptions();

        [OperationContract]
        BSCaptionData[] GetAllBudgetBSCaptions();

        [OperationContract]
        BSCaptionData[] GetAllMPRBSCaptionsByCaptionName(string CaptionName);

        [OperationContract]
        BSCaptionData[] GetAllBudgetBSCaptionsByCaptionName(string CaptionName);


        #endregion

        #region MPRProduct

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MPRProduct UpdateMPRProduct(MPRProduct product);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMPRProduct(int productId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MPRProduct GetMPRProduct(int productId);

        [OperationContract]
        MPRProductData[] GetAllMPRProducts();

        [OperationContract]
        MPRProductData[] GetAllMPRProductsByProductCode(string productCode);

        [OperationContract]
        MPRProductData[] GetMPRProductByType(BalanceSheetType type);

        [OperationContract]
        MPRProductData[] GetMPRProductByNotional(bool notional);

        [OperationContract]
        KeyValueData[] GetUnMappedProducts();

        #endregion

        #region NonProductMap

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        NonProductMap UpdateNonProductMap(NonProductMap nonProductMap);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteNonProductMap(int nonProductMapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        NonProductMap GetNonProductMap(int nonProductMapId);

        [OperationContract]
        NonProductMapData[] GetAllNonProductMaps();

        #endregion

        #region NonProductRate

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        NonProductRate UpdateNonProductRate(NonProductRate nonProductRate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteNonProductRate(int nonProductRateId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        NonProductRate GetNonProductRate(int nonProductRateId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        NonProductRate[] GetAllNonProductRates();

        #endregion

        #region ProductMIS

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProductMIS UpdateProductMIS(ProductMIS productMIS);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProductMIS(int productMISId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProductMIS GetProductMIS(int productMISId);

        [OperationContract]
        ProductMISData[] GetAllProductMISs();

        #endregion

        #region BalanceSheetThreshold

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BalanceSheetThreshold UpdateBalanceSheetThreshold(BalanceSheetThreshold balanceSheetThreshold);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBalanceSheetThreshold(int balanceSheetThresholdId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BalanceSheetThreshold GetBalanceSheetThreshold(int balanceSheetThresholdId);

        [OperationContract]
        BalanceSheetThresholdData[] GetAllBalanceSheetThresholds();


        #endregion

        #region BalanceSheetAdjustment

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MPRBalanceSheetAdjustment UpdateBalanceSheetAdjustment(MPRBalanceSheetAdjustment balanceSheetAdjustment);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBalanceSheetAdjustment(int balanceSheetAdjustmentId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MPRBalanceSheetAdjustment GetBalanceSheetAdjustment(int balanceSheetAdjustmentId);

        [OperationContract]
        MPRBalanceSheetAdjustment[] GetAllBalanceSheetAdjustments();

        [OperationContract]
        MPRBalanceSheetAdjustment[] GetBalanceSheetAdjustments(string searchType, string searchValue, int number);

        [OperationContract]
        MPRBalanceSheetAdjustment[] GetCodebyUsers(string userName);

        [OperationContract]
        MPRBalanceSheetAdjustment[] GetBalanceSheetAdjustmentByCode(string code, string userName);


        [OperationContract]
        void DeleteMPRBalanceSheetAdjustment(string code, string userName);


        #endregion

        #region BalanceSheet

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MPRBalanceSheet UpdateBalanceSheet(MPRBalanceSheet balanceSheet);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBalanceSheet(int balanceSheetId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MPRBalanceSheet GetBalanceSheet(int balanceSheetId);

        [OperationContract]
        MPRBalanceSheet[] GetmprBalanceSheets(int number);

        [OperationContract]
        MPRBalanceSheet[] GetRunDate();

        [OperationContract]
        MPRBalanceSheet[] GetAllBalanceSheets(string searchType, string searchValue, int number, DateTime fromDate);

        //[OperationContract]
        //MPRBalanceSheet[] GetAllBalanceSheets(DateTime fromDate);

        [OperationContract]
        MPRBalanceSheet[] GetAllMPRBalanceSheets();



        #endregion

        #region BalanceSheetBudget

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BalanceSheetBudget UpdateBalanceSheetBudget(BalanceSheetBudget balanceSheetBudget);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBalanceSheetBudget(int balanceSheetBudgetId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BalanceSheetBudget GetBalanceSheetBudget(int balanceSheetBudgetId);

        [OperationContract]
        BalanceSheetBudget[] GetAllBalanceSheetBudgets(string year);

        [OperationContract]
        BalanceSheetBudget[] GetBalanceSheetBudgets(string searchValue);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBSBSelectedIds(string selectedIds);

        #endregion

        #region BalanceSheetBudgetOfficer

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BalanceSheetBudgetOfficer UpdateBalanceSheetBudgetOfficer(BalanceSheetBudgetOfficer balanceSheetBudgetOfficer);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBalanceSheetBudgetOfficer(int balanceSheetBudgetOffId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BalanceSheetBudgetOfficer GetBalanceSheetBudgetOfficer(int balanceSheetBudgetOffId);

        [OperationContract]
        BalanceSheetBudgetOfficer[] GetAllBalanceSheetBudgetOfficers(string year);

        [OperationContract]
        BalanceSheetBudgetOfficer[] GetBalanceSheetBudgetOfficers(string searchValue);



        #endregion

        #region BSGLMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BSGLMapping UpdateBSGLMapping(BSGLMapping bsGLMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBSGLMapping(int bsGLMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BSGLMapping GetBSGLMapping(int bsGLMappingId);

        [OperationContract]
        BSGLMappingData[] GetAllBSGLMappings();


        #endregion

        #region BSINOtherInformation

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BSINOtherInformation UpdateBSINOtherInformation(BSINOtherInformation bSINOtherInformation);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBSINOtherInformation(int bSINOtherInformationId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BSINOtherInformation GetBSINOtherInformation(int bSINOtherInformationId);

        [OperationContract]
        BSINOtherInformation[] GetAllBSINOtherInformations();

        [OperationContract]
        IEnumerable<BSCaption> GetAllBsPlCaptions();

        #endregion


        #region BSINOtherInformationTotalLine

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BSINOtherInformationTotalLine UpdateBSINOtherInformationTotalLine(BSINOtherInformationTotalLine bSINOtherInformationTotalLine);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBSINOtherInformationTotalLine(int bSINOtherInformationTotalLineId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BSINOtherInformationTotalLine GetBSINOtherInformationTotalLine(int bSINOtherInformationTotalLineId);

        [OperationContract]
        BSINOtherInformationTotalLine[] GetAllBSINOtherInformationTotalLines();

        [OperationContract]
        IEnumerable<BSCaption> GetAllBsPlOtherInfoCaptions();

        #endregion

    }
}
