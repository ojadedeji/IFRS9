using System;
using System.Linq;
using System.ServiceModel;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.MPR.Framework;
using System.Collections.Generic;

namespace Fintrak.Business.MPR.Contracts
{
    [ServiceContract]
    public interface IMPRCoreService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();

        #region UserMIS

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        UserMIS UpdateUserMIS(UserMIS userMIS);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteUserMIS(int userMISId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        UserMIS GetUserMIS(int userMISId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        UserMIS GetUserMISByLoginID(string loginID);

        [OperationContract]
        UserMIS[] GetAllUserMISs();

        #endregion

        #region UserClassificationMap

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        UserClassificationMap UpdateUserClassificationMap(UserClassificationMap userClassificationMap);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteUserClassificationMap(int userClassificationMapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        UserClassificationMap GetUserClassificationMap(int userClassificationMapId);

        [OperationContract]
        UserClassificationMap[] GetAllUserClassificationMaps(string loginID);

        #endregion

        #region TeamDefinition

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TeamDefinition UpdateTeamDefinition(TeamDefinition teamDefinition);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTeamDefinition(int teamDefinitionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TeamDefinition GetTeamDefinition(int teamDefinitionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TeamDefinition GetTeamDefinitionByCode(string code);

        [OperationContract]
        IEnumerable<TeamDefinition> GetAllTeamDefinitions();

        #endregion

        #region TeamClassificationType

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TeamClassificationType UpdateTeamClassificationType(TeamClassificationType teamClassificationType);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTeamClassificationType(int teamClassificationTypeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TeamClassificationType GetTeamClassificationType(int teamClassificationTypeId);

        [OperationContract]
        TeamClassificationType[] GetAllTeamClassificationTypes();

        #endregion

        #region TeamClassification

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TeamClassification UpdateTeamClassification(TeamClassification teamClassification);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTeamClassification(int teamClassificationId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TeamClassification GetTeamClassification(int teamClassificationId);

        [OperationContract]
        TeamClassification[] GetAllTeamClassifications();

        [OperationContract]
        TeamClassification[] GetTeamClassifications(string typeCode);

        #endregion

        #region Team

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Team UpdateTeam(Team team);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTeam(int teamId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Team GetTeam(int teamId);

        [OperationContract]
        Team[] GetParentTeams(string definitionCode);

        [OperationContract]
        Team[] GetTeamByLevel(int level);

        [OperationContract]
        IEnumerable<Team> GetTeamByDefinition(string definitionCode);

        [OperationContract]
        TeamData[] GetTeams();

        [OperationContract]
        TeamData[] GetTeamsBySearch(string SearchValue);

        #endregion

        #region TeamClassificationMap

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TeamClassificationMap UpdateTeamClassificationMap(TeamClassificationMap teamClassificationMap);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTeamClassificationMap(int teamClassificationMapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TeamClassificationMap GetTeamClassificationMap(int teamClassificationMapId);

        [OperationContract]
        TeamClassificationMap[] GetAllTeamClassificationMaps(string misCode, string definitionCode);

        #endregion

        #region AccountOfficerDetail

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        AccountOfficerDetail UpdateAccountOfficerDetail(AccountOfficerDetail accountOfficerDetail);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteAccountOfficerDetail(int accountOfficerDetailId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        AccountOfficerDetail GetAccountOfficerDetail(int accountOfficerDetailId);

        [OperationContract]
        AccountOfficerDetail[] GetAllAccountOfficerDetails();

        #endregion

        #region BranchDefaultMIS

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BranchDefaultMIS UpdateBranchDefaultMIS(BranchDefaultMIS branchDefaultMIS);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBranchDefaultMIS(int branchDefaultMISId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BranchDefaultMIS GetBranchDefaultMIS(int branchDefaultMISId);

        [OperationContract]
        BranchDefaultMIS[] GetAllBranchDefaultMISs();

        #endregion

        #region AccountMIS

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        AccountMIS UpdateAccountMIS(AccountMIS accountMIS);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteAccountMIS(int accountMISId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        AccountMIS GetAccountMIS(int accountMISId);

        [OperationContract]
        AccountMISData[] GetAllAccountMISs();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSelectedIds(string selectedIds);

        #endregion

        #region ManagementTree

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ManagementTree UpdateManagementTree(ManagementTree managementTree);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteManagementTree(int managementTreeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ManagementTree GetManagementTree(int managementTreeId);

        [OperationContract]
        ManagementTreeData[] GetAllManagementTrees();

        #endregion

        #region MISReplacement

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MISReplacement UpdateMISReplacement(MISReplacement misReplacement);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMISReplacement(int misReplacementId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MISReplacement GetMISReplacement(int misReplacementId);

        [OperationContract]
        MISReplacement[] GetAllMISReplacements();

        #endregion

        #region SetUp

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SetUp UpdateMPRSetup(SetUp mprMPRSetup);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SetUp GetFirstMPRSetup();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MPRSetupData[] GetFirstMPRSetups();

        #endregion

        #region TransferPrice

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TransferPrice UpdateTransferPrice(TransferPrice transferPrice);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTransferPrice(int transferPriceId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TransferPrice GetTransferPrice(int transferPriceId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TransferPriceData[] GetAllTransferPrices();

        #endregion

        #region AccountTransferPrice

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        AccountTransferPrice UpdateAccountTransferPrice(AccountTransferPrice accountTransferPrice);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteAccountTransferPrice(int accountTransferPriceId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        AccountTransferPrice GetAccountTransferPrice(int accountTransferPriceId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        AccountTransferPriceData[] GetAllAccountTransferPrices();

        #endregion

        #region GeneralTransferPrice

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        GeneralTransferPrice UpdateGeneralTransferPrice(GeneralTransferPrice generalTransferPrice);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGeneralTransferPrice(int generalTransferPriceId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GeneralTransferPrice GetGeneralTransferPrice(int generalTransferPriceId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GeneralTransferPriceData[] GetAllGeneralTransferPrices();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGTPSelectedIds(string selectedIds);

        #endregion

        #region CustAccount

        [OperationContract]
        CustAccount[] GetAllCustAccounts();

        [OperationContract]
        CustAccount[] GetCustAccounts(string searchType, string searchValue, int number);

        #endregion

        #region BSExemption

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BSExemption UpdateBSExemption(BSExemption bsExemption);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBSExemption(int bsExemptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BSExemption GetBSExemption(int bsExemptionId);

        [OperationContract]
        BSExemption[] GetAllBSExemptions();

        #endregion

        #region MemoAccountMap

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MemoAccountMap UpdateMemoAccountMap(MemoAccountMap memoAccountMap);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMemoAccountMap(int memoAccountMapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MemoAccountMap GetMemoAccountMap(int memoAccountMapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MemoAccountMapData[] GetAllMemoAccountMaps();

        #endregion

        #region MemoGLMap

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MemoGLMap UpdateMemoGLMap(MemoGLMap memoGLMap);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMemoGLMap(int memoGLMapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MemoGLMap GetMemoGLMap(int memoGLMapId);

        [OperationContract]
        MemoGLMapData[] GetAllMemoGLMaps();

        #endregion

        #region MemoProductMap

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MemoProductMap UpdateMemoProductMap(MemoProductMap memoProductMap);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMemoProductMap(int memoProductMapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MemoProductMap GetMemoProductMap(int memoProductMapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MemoProductMapData[] GetAllMemoProductMaps();

        #endregion

        #region CaptionMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CaptionMapping UpdateCaptionMapping(CaptionMapping captionMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCaptionMapping(int captionMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CaptionMapping GetCaptionMapping(int captionMappingId);

        [OperationContract]
        CaptionMapping[] GetAllCaptionMappings();


        #endregion

        #region RatioCaptionMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        RatioCaptionMapping UpdateRatioCaptionMapping(RatioCaptionMapping ratioCaptionMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRatioCaptionMapping(int ratioCaptionMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        RatioCaptionMapping GetRatioCaptionMapping(int ratioCaptionMappingId);

        [OperationContract]
        RatioCaptionMapping[] GetAllRatioCaptionMappings();


        #endregion

        #region Ratios

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Ratios UpdateRatios(Ratios ratios);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRatios(int ratiosId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Ratios GetRatios(int ratiosId);

        [OperationContract]
        Ratios[] GetAllRatios();


        #endregion

        #region MemoUnits

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MemoUnits UpdateMemoUnits(MemoUnits memoUnit);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMemoUnits(int memoUnitId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MemoUnits GetMemoUnits(int memoUnitId);

        [OperationContract]
        MemoUnits[] GetAllMemoUnits();

        #endregion

        #region Staffss

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Staffs UpdateStaffs(Staffs staffs);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteStaffs(int staffId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Staffs GetStaffs(int staffId);

        [OperationContract]
        Staffs[] GetAllStaffs();

        #endregion Staffs

        #region MessagingSubscription

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MessagingSubscription UpdateMessagingSubscription(MessagingSubscription messagingSubscription);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMessagingSubscription(int messagingSubscriptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MessagingSubscription GetMessagingSubscription(int messagingSubscriptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Revenue[] GetMessagingSubscriptionByRecipients(string recipients);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        DateTime[] GetRecipents();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        string[] GetReports();


        #endregion

        #region AbcRatio

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        AbcRatio UpdateAbcRatio(AbcRatio abcRatio);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteAbcRatio(int abcRatioId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        AbcRatio GetAbcRatio(int abcRatioId);

        [OperationContract]
        AbcRatio[] GetAllAbcRatio();


        #endregion

        #region Sbu

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Sbu UpdateSbu(Sbu sbu);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSbu(int sbuId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Sbu GetSbu(int sbuId);

        [OperationContract]
        Sbu[] GetAllSbu();


        #endregion

        #region SbuType

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SbuType UpdateSbuType(SbuType sbuType);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSbuType(int sbuTypeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SbuType GetSbuType(int sbuTypeId);

        [OperationContract]
        SbuType[] GetAllSbuType();


        #endregion

        #region Servicese

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Servicese UpdateServices(Servicese service);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteServices(int servicesId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Servicese GetServices(int serviceId);

        [OperationContract]
        Servicese[] GetAllServices();


        #endregion
    }
}
