using System;
using System.Linq;
using System.ServiceModel;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.SystemCore.Entities;
using coreEntities = Fintrak.Shared.SystemCore.Entities;
using Fintrak.Shared.SystemCore.Framework;

namespace Fintrak.Business.SystemCore.Contracts
{
    [ServiceContract]
    public interface ICoreService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();

        #region Solution

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Solution UpdateSolution(Solution solution);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSolution(int solutionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Solution GetSolution(int solutionId);

        [OperationContract]
        Solution[] GetAllSolutions();

        #endregion Solution

        #region Module

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Module UpdateModule(Module module);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteModule(int moduleId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Module GetModule(int moduleId);

        [OperationContract]
        Module[] GetAllModules();

        [OperationContract]
        ModuleData[] GetModules();

        #endregion Module

        #region Role

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Role UpdateRole(Role role);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRole(int roleId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Role GetRole(int roleId);

        [OperationContract]
        RoleData[] GetAllRoles();

        #endregion Role

        #region UserSetup

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        UserSetup UpdateUserSetup(UserSetup userSetup);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        UserSetup UpdateUserSetupProfile(UserSetup userSetup);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteUserSetup(int userSetupId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        UserSetup GetUserSetup(int userSetupId);

        [OperationContract]
        UserSetup GetUserSetupByLoginID(string loginID);

        [OperationContract]
        UserSetup[] GetAllUserSetups();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void ConfirmDefaultUser();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void AssignDefaultRole(UserSetup account);

        #endregion UserSetup

        #region UserRole

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        UserRole UpdateUserRole(UserRole userRole);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteUserRole(int userRoleId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        UserRole GetUserRole(int userRoleId);

        [OperationContract]
        UserRole[] GetAllUserRoles();

        [OperationContract]
        UserRoleData[] GetUserRoleByLoginID(string loginID);

        [OperationContract]
        UserRoleData[] GetUserRoles();

        #endregion UserRole

        #region Menu

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Menu UpdateMenu(Menu menu);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMenu(int menuId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Menu GetMenu(int menuId);

        [OperationContract]
        Menu[] GetAllMenus();

        [OperationContract]
        MenuData[] GetMenuByLoginID(string loginID);

        [OperationContract]
        MenuData[] GetMenus();

        #endregion Menu

        #region MenuRole

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MenuRole UpdateMenuRole(MenuRole menuRole);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMenuRole(int menuRoleId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MenuRole GetMenuRole(int menuRoleId);

        [OperationContract]
        MenuRole[] GetAllMenuRoles();

        [OperationContract]
        MenuRoleData[] GetMenuRoles();

        [OperationContract]
        MenuRoleData[] GetMenuRoleByLoginID(string loginID);

        #endregion MenuRole

        #region AuditTrail

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        AuditTrail UpdateAuditTrail(AuditTrail auditTrail);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteAuditTrail(int auditTrailId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        AuditTrail GetAuditTrail(int auditTrailId);

        [OperationContract]
        AuditTrail[] GetAllAuditTrails();

        [OperationContract]
        AuditTrail[] GetAuditTrails(DateTime fromDate, DateTime toDate);

        [OperationContract]
        AuditTrail[] GetAuditTrailByTable(string tableName, DateTime fromDate, DateTime toDate);

        [OperationContract]
        AuditTrail[] GetAuditTrailByUser(string userName, DateTime fromDate, DateTime toDate);

        [OperationContract]
        AuditTrail[] GetAuditTrailByAction(string action, DateTime fromDate, DateTime toDate);

        [OperationContract]
        AuditTrail[] GetAuditTrailByTab(AuditAction action);


        #endregion AuditTrail

        #region Company

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Company UpdateCompany(Company company);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCompany(int companyId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Company GetCompany(int companyId);

        [OperationContract]
        Company[] GetAllCompanies();

        #endregion Company

        #region General

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        General UpdateGeneral(General general);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGeneral(int generalId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        General GetGeneral(int generalId);

        [OperationContract]
        General[] GetAllGenerals();

        [OperationContract]
        General GetFirstGeneral();


        #endregion GeneralcompanySecurity

        #region Log Event

        [OperationContract]
        LogEvent[] GetLogByDateRangeAndType(DateTime start, DateTime end, string logLevel);

        [OperationContract]
        LogEvent GetLogById(string id);

        [OperationContract]
        void ClearLog(DateTime start, DateTime end, string[] logLevels);

        #endregion

        #region Client

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        coreEntities.Client UpdateClient(coreEntities.Client client);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteClient(int clientId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        coreEntities.Client GetClient(int clientId);

        [OperationContract]
        coreEntities.Client[] GetAllClients();

        #endregion Client

        #region Database

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Database UpdateDatabase(Database database);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteDatabase(int databaseId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Database GetDatabase(int databaseId);

        [OperationContract]
        DatabaseData[] GetAllDatabases();

        #endregion Database

        #region CompanySecurity

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CompanySecurity UpdateCompanySecurity(CompanySecurity companySecurity);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCompanySecurity(int companySecurityId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CompanySecurity GetCompanySecurity(int companySecurityId);

        [OperationContract]
        CompanySecurity[] GetAllCompanySecuritys();

        #endregion CompanySecurity

        #region UserSession

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        UserSession UpdateUserSession(UserSession userSession);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteUserSession(int userSessionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        UserSession GetUserSession(int userSessionId);

        [OperationContract]
        UserSessionData[] GetUserSessionByLoginID(int loginID);


        #endregion

        #region CompanyUser

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CompanyUser UpdateCompanyUser(CompanyUser companyUser);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCompanyUser(int companyUserId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CompanyUser GetCompanyUser(int companyUserId);

        [OperationContract]
        CompanyUser[] GetAllCompanyUsers();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CompanyUser GetCompanyUsers(string loginID, string companyCode);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CompanyUser[] GetCompanyUserByLogin(string loginID);

        #endregion CompanyUser

        #region CompanyModule

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CompanyModule UpdateCompanyModule(CompanyModule companyModule);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCompanyModule(int companyModuleId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CompanyModule GetCompanyModule(int companyModuleId);

        [OperationContract]
        CompanyModule[] GetAllCompanyModules();

        [OperationContract]
        CompanyModuleData[] GetCompanyModuleByCompanyCode(string companyCode);

        [OperationContract]
        CompanyModuleData[] GetCompanyModules();


        #endregion

        #region ErrorTracker

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ErrorTracker UpdateErrorTracker(ErrorTracker errorTracker);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteErrorTracker(int errorTrackerId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteAllErrorTracker();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ErrorTracker GetErrorTracker(int errorTrackerId);

        [OperationContract]
        ErrorTracker[] GetAllErrorTrackers();


        #endregion

        [OperationContract]
        bool IsNewSystem(string companyCode);

        [OperationContract]
        bool IsFirstLogon(string loginId);
    }
}
