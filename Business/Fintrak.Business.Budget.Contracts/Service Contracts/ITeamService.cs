using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Budget.Framework;

namespace Fintrak.Business.Budget.Contracts
{
    [ServiceContract]
    public interface ITeamService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();

        #region OfficerDetail

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OfficerDetail UpdateOfficerDetail(OfficerDetail officerDetail);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOfficerDetail(int officerDetailId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OfficerDetail GetOfficerDetail(int officerDetailId);

        [OperationContract]
        OfficerDetail[] GetAllOfficerDetails();

        #endregion

        #region TeamSetting

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TeamSetting UpdateTeamSetting(TeamSetting teamSetting);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTeamSetting(int teamSettingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TeamSetting GetTeamSetting(int teamSettingId);

        [OperationContract]
        TeamSetting[] GetAllTeamSettings();

        #endregion

        #region TeamUser

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TeamUser UpdateTeamUser(TeamUser teamUser);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTeamUser(int teamUserId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TeamUser GetTeamUser(int teamUserId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TeamUser GetTeamUserByLoginID(string loginID);

        [OperationContract]
        TeamUser[] GetAllTeamUsers();

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
        TeamDefinition[] GetTeamDefinitionByCode(string code);

        [OperationContract]
        TeamDefinition[] GetAllTeamDefinitions();

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
        Team[] GetTeamByDefinition(string definitionCode);

        //[OperationContract]
        //TeamData[] GetTeams();

        #endregion
    }
}
