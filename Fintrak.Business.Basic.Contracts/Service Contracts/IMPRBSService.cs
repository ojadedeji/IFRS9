using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;

namespace Fintrak.Business.Basic.Contracts
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
        BSCaptionData[] GetAllBSCaptions();


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
        MPRBalanceSheet[] GetAllBalanceSheets();

        [OperationContract]
        MPRBalanceSheet[] GetBalanceSheets(string searchType, string searchValue, int number);


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

    }
}
