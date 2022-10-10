using System;
using System.Linq;
using System.ServiceModel;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Scorecard.Entities;

namespace Fintrak.Business.Scorecard.Contracts
{
    [ServiceContract]
    public interface IScorecardService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();

        #region SCDActual

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SCDActual UpdateSCDActual(SCDActual scdActual);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSCDActual(int actualId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SCDActual GetSCDActual(int actualId);

        [OperationContract]
        SCDActual[] GetAllSCDActuals();

        [OperationContract]
        SCDActual[] GetCaption();

        #endregion

        #region SCDCategory

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SCDCategory UpdateSCDCategory(SCDCategory scdCategory);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSCDCategory(int categoryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SCDCategory GetSCDCategory(int categoryId);

        [OperationContract]
        SCDCategoryData[] GetAllSCDCategorys();

        #endregion

        #region SCDKPIClassification

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SCDKPIClassification UpdateSCDKPIClassification(SCDKPIClassification scdClassification);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSCDKPIClassification(int classificationId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SCDKPIClassification GetSCDKPIClassification(int classificationId);

        [OperationContract]
        SCDKPIClassificationData[] GetAllSCDKPIClassifications();

        #endregion

        #region SCDConfiguration

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SCDConfiguration UpdateSCDConfiguration(SCDConfiguration scdConfiguration);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SCDConfiguration GetFirstSetup();

        #endregion

        #region SCDKPI

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SCDKPI UpdateSCDKPI(SCDKPI scdKPI);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSCDKPI(int kpiId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SCDKPI GetSCDKPI(int kpiId);

        [OperationContract]
        SCDKPIData[] GetAllSCDKPIs();

        #endregion

        #region SCDKPIActualMap

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SCDKPIActualMap UpdateSCDKPIActualMap(SCDKPIActualMap scdKPIActualMap);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSCDKPIActualMap(int mapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SCDKPIActualMap GetSCDKPIActualMap(int mapId);

        //[OperationContract]
        //SCDKPIActualMap[] GetAllSCDKPIActualMaps();

        [OperationContract]
        SCDKPIActualMapData[] GetAllSCDKPIActualMaps();

        #endregion

        #region SCDKPIEntry

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SCDKPIEntry UpdateSCDKPIEntry(SCDKPIEntry scdKPIEntry);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSCDKPIEntry(int entryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SCDKPIEntry GetSCDKPIEntry(int entryId);

        [OperationContract]
        SCDKPIEntryData[] GetAllSCDKPIEntrys();

        #endregion

        #region SCDKPITargetMap

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SCDKPITargetMap UpdateSCDKPITargetMap(SCDKPITargetMap scdKPITargetMap);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSCDKPITargetMap(int mapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SCDKPITargetMap GetSCDKPITargetMap(int mapId);

        //[OperationContract]
        //SCDKPITargetMap[] GetAllSCDKPITargetMaps();

        [OperationContract]
        SCDKPITargetMapData[] GetAllSCDKPITargetMaps();


        #endregion

        #region SCDParticipant

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SCDParticipant UpdateSCDParticipant(SCDParticipant scdParticipant);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSCDParticipant(int participantId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SCDParticipant GetSCDParticipant(int participantId);

        [OperationContract]
        SCDParticipantData[] GetAllSCDParticipants();

        #endregion

        #region SCDTarget

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SCDTarget UpdateSCDTarget(SCDTarget scdTarget);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSCDTarget(int targetId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SCDTarget GetSCDTarget(int targetId);

        [OperationContract]
        SCDTarget[] GetAllSCDTargets();

        [OperationContract]
        SCDTarget[] GetCaptions();



        #endregion

        #region SCDTeamMap

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SCDTeamMap UpdateSCDTeamMap(SCDTeamMap scdTeamMap);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSCDTeamMap(int teamMapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SCDTeamMap GetSCDTeamMap(int teamMapId);

        [OperationContract]
        SCDTeamMapData[] GetAllSCDTeamMaps();

        #endregion

        #region SCDThreshold

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SCDThreshold UpdateSCDThreshold(SCDThreshold scdThreshold);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSCDThreshold(int thresholdId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SCDThreshold GetSCDThreshold(int thresholdId);

        [OperationContract]
        SCDThresholdData[] GetAllSCDThresholds();

        #endregion

        #region SCDTeamClassification

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SCDTeamClassification UpdateSCDTeamClassification(SCDTeamClassification scdTeamClassification);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSCDTeamClassification(int teamClassificationId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SCDTeamClassification GetSCDTeamClassification(int teamClassificationId);

        [OperationContract]
        SCDTeamClassification[] GetAllSCDTeamClassifications();



        #endregion
    }
}
