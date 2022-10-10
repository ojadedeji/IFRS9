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
        GLMapping UpdateGLMapping(GLMapping glMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGLMapping(int glMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GLMapping GetGLMapping(int glMappingId);

        [OperationContract]
        GLMapping[] GetAllGLMappings();

        [OperationContract]
        GLMappingData[] GetGLMappings();

        [OperationContract]
        GLMappingData[] GetUnMappedGLs();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GLMappingData[] GetUnMappedGLbyGLCode(string glCode);

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

        [OperationContract]
        PostingDetail[] GetAllPostingDetails();

        #endregion

        #region GLAdjustment

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        GLAdjustment UpdateGLAdjustment(GLAdjustment glAdjustment);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void PostGLAdjustment(AdjustmentType adjustmentType);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGLAdjustment(int glAdjustmentId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGLAdjustmentByCode(AdjustmentType adjustmentType,string adjustmentCode);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void ReverseGLAdjustmentByCode(AdjustmentType adjustmentType,string adjustmentCode);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void PurgeGLAdjustment(AdjustmentType adjustmentType);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GLAdjustment GetGLAdjustment(int glAdjustmentId);

        [OperationContract]
        GLAdjustmentData[] GetGLAdjustmentByStatus(AdjustmentType adjustmentType,bool posted);

        [OperationContract]
        GLAdjustmentData[] GetGLAdjustments(AdjustmentType adjustmentType);

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
    }
}
