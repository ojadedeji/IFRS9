using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;

namespace Fintrak.Business.MPR.Contracts
{
    [ServiceContract]
    public interface IMPROPEXService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();


        #region CostCentreDefinition

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CostCentreDefinition UpdateCostCentreDefinition(CostCentreDefinition costCentreDefinition);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCostCentreDefinition(int ccDefinitionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CostCentreDefinition GetCostCentreDefinition(int ccDefinitionId);

        [OperationContract]
        CostCentreDefinition[] GetAllCostCentreDefinitions();


        #endregion

        #region CostCenter

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CostCentre UpdateCostCentre(CostCentre costCentre);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCostCentre(int costCentreId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CostCentre GetCostCentre(int costCentreId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CostCentreData[] GetAllCostCentres();

        [OperationContract]
        CostCentre[] GetParentCostCentres(string definitionCode);

        [OperationContract]
        CostCentre[] GetCostCentreByLevel(int level);

        [OperationContract]
        CostCentre[] GetCostCentreByDefinition(string definitionCode);

        #endregion

        #region ExpenseBasis

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ExpenseBasis UpdateExpenseBasis(ExpenseBasis expenseBasis);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteExpenseBasis(int expenseBasisId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ExpenseBasis GetExpenseBasis(int expenseBasisId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ExpenseBasis[] GetAllExpenseBasisInfo();


        #endregion

        #region ExpenseMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ExpenseMapping UpdateExpenseMapping(ExpenseMapping expenseMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteExpenseMapping(int expenseMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ExpenseMapping GetExpenseMapping(int expenseMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ExpenseMappingData[] GetAllExpenseMappings();


        #endregion

        #region ExpenseProductMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ExpenseProductMapping UpdateExpenseProductMapping(ExpenseProductMapping expenseProductMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteExpenseProductMapping(int expenseProductId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ExpenseProductMapping GetExpenseProductMapping(int expenseProductId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ExpenseProductMappingData[] GetAllExpenseProductMappings();


        #endregion

        #region ExpenseGLMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ExpenseGLMapping UpdateExpenseGLMapping(ExpenseGLMapping expenseGLMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteExpenseGLMapping(int expenseGLId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ExpenseGLMapping GetExpenseGLMapping(int expenseGLId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ExpenseGLMappingData[] GetAllExpenseGLMappings();


        #endregion

        #region ExpenseRawBasis

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ExpenseRawBasis UpdateExpenseRawBasis(ExpenseRawBasis expenseRawBasis);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteExpenseRawBasis(int expenseRawBasisId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ExpenseRawBasis GetExpenseRawBasis(int expenseRawBasisId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ExpenseRawBasisData[] GetAllExpenseRawBasisInfo();

        #endregion

        #region StaffCost

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        StaffCost UpdateStaffCost(StaffCost staffCost);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteStaffCost(int staffCostId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        StaffCost GetStaffCost(int staffCostId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        StaffCostData[] GetAllStaffCosts();


        #endregion

        #region OpexManagementTree

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexManagementTree UpdateOpexManagementTree(OpexManagementTree opexManagementTree);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexManagementTree(int opexMgtTreeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexManagementTree GetOpexManagementTree(int opexMgtTreeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexManagementTreeData[] GetAllOpexManagementTrees();


        #endregion

        #region ActivityBase

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ActivityBase UpdateActivityBase(ActivityBase activityBase);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteActivityBase(int activityBaseId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ActivityBase GetActivityBase(int activityBaseId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ActivityBase[] GetAllActivityBases();


        #endregion

        #region ActivityBaseRatio

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ActivityBaseRatio UpdateActivityBaseRatio(ActivityBaseRatio activityBaseRatio);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteActivityBaseRatio(int activityBaseRatioId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ActivityBaseRatio GetActivityBaseRatio(int activityBaseRatioId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ActivityBaseRatio[] GetAllActivityBaseRatios();


        #endregion

        #region OpexMISReplacement

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexMISReplacement UpdateOpexMISReplacement(OpexMISReplacement opexMISReplacement);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexMISReplacement(int opexMISReplacementId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexMISReplacement GetOpexMISReplacement(int opexMISReplacementId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexMISReplacementData[] GetAllOpexMISReplacements();


        #endregion

        #region OpexBusinessRule

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexBusinessRule UpdateOpexBusinessRule(OpexBusinessRule opexBusinessRule);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexBusinessRule(int opexBusinessRuleId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexBusinessRule GetOpexBusinessRule(int opexBusinessRuleId);

        [OperationContract]
        OpexBusinessRule[] GetAllOpexBusinessRules();
        #endregion

        #region OpexAbcExemption

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexAbcExemption UpdateOpexAbcExemption(OpexAbcExemption opexAbcExemption);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexAbcExemption(int opexAbcExemptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexAbcExemption GetOpexAbcExemption(int opexAbcExemptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexAbcExemptionData[] GetAllOpexAbcExemptions();

        //[OperationContract]
        //[FaultContract(typeof(NotFoundException))]
        //OpexAbcExemption[] GetAllOpexAbcExemptions();


        #endregion

        #region OpexRawExpense

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexRawExpense UpdateOpexRawExpense(OpexRawExpense opexRawExpense);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexRawExpense(int opexRawExpenseId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexRawExpense GetOpexRawExpense(int opexRawExpenseId);

        [OperationContract]
        OpexRawExpense[] GetAllOpexRawExpenses();


        #endregion

        #region OpexGLMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexGLMapping UpdateOpexGLMapping(OpexGLMapping opexGLMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexGLMapping(int opexGLMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexGLMapping GetOpexGLMapping(int opexGLMappingId);

        [OperationContract]
        OpexGLMapping[] GetAllOpexGLMappings();

        [OperationContract]
        KeyValueData[] GetUnMappedOpexGLs();


        #endregion

        #region OpexReport

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexReport UpdateOpexReport(OpexReport opexReport);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexReport(int opexReportId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexReport GetOpexReport(int opexReportId);

        [OperationContract]
        OpexReportData[] GetAllOpexReports();


        #endregion

        #region OpexGLBasis

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexGLBasis UpdateOpexGLBasis(OpexGLBasis opexGLBasis);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexGLBasis(int opexGLBasisId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexGLBasis GetOpexGLBasis(int opexGLBasisId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexGLBasis[] GetAllOpexGLBasiss();


        #endregion

        #region OpexBasisMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexBasisMapping UpdateOpexBasisMapping(OpexBasisMapping opexBasisMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexBasisMapping(int opexBasisMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexBasisMapping GetOpexBasisMapping(int opexBasisMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexBasisMapping[] GetAllOpexBasisMappings();


        #endregion

        #region CheckList

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CheckList UpdateCheckList(CheckList checkList);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCheckList(int checkListId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CheckList GetCheckList(int checkListId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CheckList[] GetAllCheckLists();

        [OperationContract]
        CheckListData[] RunCheckList();

        #endregion

        #region HoExemptionMISCode

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        HoExemptionMISCode UpdateHoExemptionMISCode(HoExemptionMISCode hoExemptionMISCode);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteHoExemptionMISCode(int id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        HoExemptionMISCode GetHoExemptionMISCode(int id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        HoExemptionMISCode[] GetAllHoExemptionMISCodes();

        #endregion

        #region FixedAssetSharingRatio

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FixedAssetSharingRatio UpdateFixedAssetSharingRatio(FixedAssetSharingRatio fixedAssetSharingRatio);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFixedAssetSharingRatio(int fixedAssetSharingRatioId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FixedAssetSharingRatio GetFixedAssetSharingRatio(int fixedAssetSharingRatioId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FixedAssetSharingRatio[] GetAllFixedAssetSharingRatios();


        #endregion

        #region IncomeCashCentreCode

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeCashCentreCode UpdateIncomeCashCentreCode(IncomeCashCentreCode incomeCashCentreCode);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeCashCentreCode(int incomeCashCentreCodeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeCashCentreCode GetIncomeCashCentreCode(int incomeCashCentreCodeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeCashCentreCode[] GetAllIncomeCashCentreCodes();


        #endregion

        #region IncomeCentralVaultAccounts

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeCentralVaultAccounts UpdateIncomeCentralVaultAccounts(IncomeCentralVaultAccounts incomeCentralVaultAccounts);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeCentralVaultAccounts(int incomeCentralVaultAccountsId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeCentralVaultAccounts GetIncomeCentralVaultAccounts(int incomeCentralVaultAccountsId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeCentralVaultAccounts[] GetAllIncomeCentralVaultAccounts();


        #endregion

        #region IncomeNEAGLSBU

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeNEAGLSBU UpdateIncomeNEAGLSBU(IncomeNEAGLSBU incomeNEAGLSBU);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeNEAGLSBU(int incomeNEAGLSBUId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeNEAGLSBU GetIncomeNEAGLSBU(int incomeNEAGLSBUId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeNEAGLSBU[] GetAllIncomeNEAGLSBUs();


        #endregion

        #region CategoryTransferPrice

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CategoryTransferPrice UpdateCategoryTransferPrice(CategoryTransferPrice categoryTransferPrice);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCategoryTransferPrice(int categoryTransferPriceId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CategoryTransferPrice GetCategoryTransferPrice(int categoryTransferPriceId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CategoryTransferPrice[] GetAllCategoryTransferPrices();


        #endregion

        #region LowCostRemap

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        LowCostRemap UpdateLowCostRemap(LowCostRemap lowCostRemap);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteLowCostRemap(int lowCostRemapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LowCostRemap GetLowCostRemap(int lowCostRemapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LowCostRemap[] GetAllLowCostRemaps();


        #endregion

        #region NEABranchSBUShares

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        NEABranchSBUShares UpdateNEABranchSBUShares(NEABranchSBUShares nEABranchSBUShares);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteNEABranchSBUShares(int nEABranchSBUSharesId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        NEABranchSBUShares GetNEABranchSBUShares(int nEABranchSBUSharesId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        NEABranchSBUShares[] GetAllNEABranchSBUShares();


        #endregion

        #region NEABranchSharingRatio

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        NEABranchSharingRatio UpdateNEABranchSharingRatio(NEABranchSharingRatio nEABranchSharingRatio);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteNEABranchSharingRatio(int nEABranchSharingRatioId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        NEABranchSharingRatio GetNEABranchSharingRatio(int nEABranchSharingRatioId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        NEABranchSharingRatio[] GetAllNEABranchSharingRatios();


        #endregion

        #region NEASharingRatio

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        NEASharingRatio UpdateNEASharingRatio(NEASharingRatio nEASharingRatio);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteNEASharingRatio(int nEASharingRatioId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        NEASharingRatio GetNEASharingRatio(int nEASharingRatioId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        NEASharingRatio[] GetAllNEASharingRatios();


        #endregion

        #region NEASharingRatioFcy

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        NEASharingRatioFcy UpdateNEASharingRatioFcy(NEASharingRatioFcy nEASharingRatioFcy);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteNEASharingRatioFcy(int nEASharingRatioFcyId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        NEASharingRatioFcy GetNEASharingRatioFcy(int nEASharingRatioFcyId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        NEASharingRatioFcy[] GetAllNEASharingRatioFcys();


        #endregion

        #region OpexBranchMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexBranchMapping UpdateOpexBranchMapping(OpexBranchMapping opexBranchMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexBranchMapping(int opexBranchMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexBranchMapping GetOpexBranchMapping(int opexBranchMappingId);

        [OperationContract]
        OpexBranchMapping[] GetAllOpexBranchMapping();


        #endregion

    }
}
