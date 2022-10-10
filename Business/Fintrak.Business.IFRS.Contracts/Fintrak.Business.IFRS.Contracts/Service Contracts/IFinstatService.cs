using System;
using System.Linq;
using System.ServiceModel;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Business.IFRS.Contracts
{
    [ServiceContract]
    public interface IFinstatService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();

        #region AutoPostingTemplate

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        AutoPostingTemplate UpdateAutoPostingTemplate(AutoPostingTemplate autoPostingTemplate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteAutoPostingTemplate(int autoPostingTemplateId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        AutoPostingTemplate GetAutoPostingTemplate(int autoPostingTemplateId);

        [OperationContract]
        AutoPostingTemplate[] GetAllAutoPostingTemplates();

        #endregion

        #region GLMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        GLMapping UpdateGLMapping(GLMapping glMapping,int status);
        //commented
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGLMapping(int glMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GLMapping GetGLMapping(int glMappingId);

        [OperationContract]
        GLMapping[] GetAllGLMappings();

        [OperationContract]
        GLMappingData[] GetGLMappings(int flag, int defaultCount, string path);

        [OperationContract]
        GLMappingData[] GetGLMappingsBySearch(int flag, string searchParam);

        [OperationContract]
        GLMappingData[] GetDistinctGLMappings();

        [OperationContract]
        GLMappingData[] GetUnMappedGLs();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GLMappingData[] GetUnMappedGLbyGLCode(string glCode);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GLMapping[] GetSubSubCaption(string caption);

        [OperationContract]
        string[] GetSubCaptionPosition(string caption);

        [OperationContract]
        GLMappingData[] GetUnMappedGLSpecial(int flag);

        #endregion

        #region GLMappingMgt

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        GLMappingMgt UpdateGLMappingMgt(GLMappingMgt glMapping,int status);
        //commented
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGLMappingMgt(int glMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GLMappingMgt GetGLMappingMgt(int glMappingId);

        [OperationContract]
        GLMappingMgt[] GetAllGLMappingMgts();

        [OperationContract]
        GLMappingMgtData[] GetGLMappingMgts();

        [OperationContract]
        GLMappingMgtData[] GetUnMappedMgtGLs();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GLMappingMgtData[] GetUnMappedMgtGLbyGLCode(string glCode);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GLMappingMgt[] GetMgtSubSubCaption(string caption);

        #endregion

        #region GLType

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        GLType UpdateGLType(GLType glType);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGLType(int glTypeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GLType GetGLType(int glTypeId);

        [OperationContract]
        GLType[] GetAllGLTypes();

        #endregion

        #region InstrumentType

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        InstrumentType UpdateInstrumentType(InstrumentType instrumentType);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteInstrumentType(int instrumentTypeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        InstrumentType GetInstrumentType(int instrumentTypeId);

        [OperationContract]      
        InstrumentTypeData[] GetAllInstrumentTypes();

        #endregion

        #region InstrumentTypeGLMap

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        InstrumentTypeGLMap UpdateInstrumentTypeGLMap(InstrumentTypeGLMap instrumentTypeGLMap);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteInstrumentTypeGLMap(int instrumentTypeGLMapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        InstrumentTypeGLMap GetInstrumentTypeGLMap(int instrumentTypeGLMapId);

        [OperationContract]
        InstrumentTypeGLMapData[] GetAllInstrumentTypeGLMaps();

        #endregion

        #region TrialBalanceGap

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TrialBalanceGap UpdateTrialBalanceGap(TrialBalanceGap trialBalanceGap);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTrialBalanceGap(int trialBalanceGapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TrialBalanceGap GetTrialBalanceGap(int trialBalanceGapId);

        [OperationContract]
        TrialBalanceGap[] GetAllTrialBalanceGaps();

        [OperationContract]
        TrialBalanceGap[] GetTrialBalanceGapByGL(string glCode);

        [OperationContract]
        TrialBalanceGap[] GetGapTrialBalancesByBranch(string branchCode);

        #endregion

        #region TrialBalance

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TrialBalance UpdateTrialBalance(TrialBalance trialBalance);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTrialBalance(int trialBalanceId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TrialBalance GetTrialBalance(int trialBalanceId);

        [OperationContract]
        TrialBalance[] GetAllTrialBalances();

        [OperationContract]
        TrialBalance[] GetTrialBalanceByGL(string glCode);

        [OperationContract]
        TrialBalance[] GetTrialBalancesByBranch(string branchCode);

        [OperationContract]
        TrialBalance[] ExportTrialBalance(int defaultCount, string path);

        #endregion

        #region TrialBalanceConsolidated

        [OperationContract]
        TrialBalanceConsolidated[] GetAllTrialBalanceConsolidated(string CompanyCode);

        #endregion

        #region TrialBalanceConsolidatedIFRS

        [OperationContract]
        TrialBalanceConsolidatedIFRS[] GetAllTrialBalanceConsolidatedIFRS(string CompanyCode);

        #endregion
        
        #region PostingDetail

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PostingDetail UpdatePostingDetail(PostingDetail postingDetail);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePostingDetail(int postingDetailId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PostingDetail GetPostingDetail(int postingDetailId);

        //[OperationContract]
        //PostingDetail[] GetAllPostingDetails();

        [OperationContract]
        PostingDetailData[] GetPostingDetailsByType(int reportType);

        #endregion

        #region PostingDetailContracts

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PostingDetailContracts UpdatePostingDetailContracts(PostingDetailContracts postingDetail);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePostingDetailContracts(int postingDetailId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PostingDetailContracts GetPostingDetailContracts(int postingDetailId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PostingDetailContracts[] GetPostingDetailContractsByFilter(string filter, string path,int count);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        string[] GetDistinctPostingFilters(int count);

        #endregion

        #region GLAdjustment

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        GLAdjustment UpdateGLAdjustment(GLAdjustment glAdjustment);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void PostGLAdjustment(AdjustmentType adjustmentType, ReportType reportType);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGLAdjustment(int glAdjustmentId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGLAdjustmentByCode(AdjustmentType adjustmentType, string adjustmentCode, ReportType reportType);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void ReverseGLAdjustmentByCode(AdjustmentType adjustmentType, string adjustmentCode, ReportType reportType);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void PurgeGLAdjustment(AdjustmentType adjustmentType, ReportType reportType);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void PurgeUnpostedGLAdjustment(int adjustmentType);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void CallUpPrevAdjustment(int adjustmentType);
        
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GLAdjustment GetGLAdjustment(int glAdjustmentId);

        [OperationContract]
        GLAdjustmentData[] GetGLAdjustmentByStatus(AdjustmentType adjustmentType, bool posted, ReportType reportType);

        [OperationContract]
        GLAdjustmentData[] GetGLAdjustments(AdjustmentType adjustmentType, ReportType reportType);


        [OperationContract]
        GLAdjustmentDataMain[] GetGLAdjustmentsMain(int glAdjustmentId, AdjustmentType adjustmentType, ReportType reportType);

        [OperationContract]
        GLAdjustmentDataMain[] GetGLAdjustmentByStatusM(AdjustmentType adjustmentType,  ReportType reportType,bool posted);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        string[] GetUnMappedGlCodes();
        #endregion

        #region IFRSReport

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IFRSReport UpdateIFRSReport(IFRSReport ifrsReport);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIFRSReport(int ifrsReportId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IFRSReport GetIFRSReport(int ifrsReportId);

        [OperationContract]
        IFRSReport[] GetAllIFRSReports();

        #endregion

        #region TransactionDetail

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TransactionDetail UpdateTransactionDetail(TransactionDetail transactionDetail);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTransactionDetail(int transactionDetailId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TransactionDetail GetTransactionDetail(int transactionDetailId);

        [OperationContract]
        TransactionDetail[] GetAllTransactionDetails();

        #endregion

        #region IFRSBudget

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IFRSBudget UpdateIFRSBudget(IFRSBudget ifrsBudget);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIFRSBudget(int ifrsbudgetId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IFRSBudget GetIFRSBudget(int ifrsbudgetId);

        [OperationContract]
        IFRSBudget[] GetAllIFRSBudgets();

        #endregion

        #region RevenueGLMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        RevenueGLMapping UpdateRevenueGLMapping(RevenueGLMapping revenueGLMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRevenueGLMapping(int revenueGLMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        RevenueGLMapping GetRevenueGLMapping(int revenueGLMappingId);

        [OperationContract]
        RevenueGLMapping[] GetAllRevenueGLMappings();

        [OperationContract]
        KeyValueData[] GetUnMappedRevenueGLs();


        #endregion

        #region LedgerDetailSummary

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        LedgerDetailSummary UpdateLedgerDetailSummary(LedgerDetailSummary postingDetail);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteLedgerDetailSummary(int SummaryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LedgerDetailSummary GetLedgerDetailSummary(int SummaryId);

        [OperationContract]
        LedgerDetailSummary[] GetAllLedgerDetailSummarys();

        #endregion

        #region ChangesInEquity

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ChangesInEquity UpdateChangesInEquity(ChangesInEquity changesInEquity);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteChangesInEquity(int ChangesInEquityId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ChangesInEquity GetChangesInEquity(int ChangesInEquityId);

        [OperationContract]
        ChangesInEquity[] GetAllChangesInEquitys();

        #endregion

        #region RatioDetail

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        RatioDetail UpdateRatioDetail(RatioDetail ratiodetail);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRatioDetail(int RatioID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        RatioDetail GetRatioDetail(int RatioID);

        [OperationContract]
        RatioDetail[] GetAllRatioDetails();

        #endregion

        #region RatioCaption

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        RatioCaption UpdateRatioCaption(RatioCaption ratiocaption);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRatioCaption(int RatioCaptionID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        RatioCaption GetRatioCaption(int RatioCaptionID);

        [OperationContract]
        RatioCaption[] GetAllRatioCaptions();

        #endregion

        #region GLAArchive

        [OperationContract]
        GLAArchive[] GetGLAArchivesByRundate(DateTime rundate);

        #endregion

        #region Calendar

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Calendar UpdateCalendar(Calendar Calendar);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCalendar(int calId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Calendar GetCalendar(int calId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Calendar[] GetCalendarException(DateTime rundate);

        [OperationContract]
        Calendar[] GetAllCalendars();

        #endregion

    }
}
