using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Budget.Framework;
using Fintrak.Shared.Budget.Framework.Enums;

namespace Fintrak.Business.Budget.Contracts
{
    [ServiceContract]
    public interface ICoreService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();

        #region BudgetingLevel

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BudgetingLevel UpdateBudgetingLevel(BudgetingLevel budgetingLevel);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBudgetingLevel(int budgetingLevelId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BudgetingLevel GetBudgetingLevel(int budgetingLevelId);

        [OperationContract]
        BudgetingLevelData[] GetBudgetingLevels(string year, string reviewCode);

        #endregion

        #region Currency

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Currency UpdateCurrency(Currency currency);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCurrency(int currencyId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Currency GetCurrency(int currencyId);

        [OperationContract]
        Currency[] GetAllCurrency();

        #endregion

        #region CurrencyRate

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CurrencyRate UpdateCurrencyRate(CurrencyRate currencyRate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCurrencyRate(int currencyRateId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CurrencyRate GetCurrencyRate(int currencyRateId);

        [OperationContract]
        CurrencyRate GetCurrencyRateByCode(string year, string reviewCode, RateTypeEnum rateType,string currencyCode);

        [OperationContract]
        CurrencyRate[] GetCurrencyRates(string year, string reviewCode,RateTypeEnum rateType);

        #endregion

        #region GeneralSetting

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        GeneralSetting UpdateGeneralSetting(GeneralSetting generalSetting);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGeneralSetting(int generalSettingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GeneralSetting GetGeneralSetting(int generalSettingId);

        [OperationContract]
        GeneralSetting GetFirstGeneralSetting();

        #endregion

        #region ModificationLevel

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ModificationLevel UpdateModificationLevel(ModificationLevel modificationLevel);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteModificationLevel(int modificationLevelId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ModificationLevel GetModificationLevel(int modificationLevelId);

        [OperationContract]
        ModificationLevelData[] GetAllModificationLevel();

        #endregion

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
        Module[] GetAllModule();

        #endregion

        #region Operation

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Operation UpdateOperation(Operation operation);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOperation(int operationId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Operation GetOperation(int operationId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Operation GetOperationByCode(string code);

        [OperationContract]
        Operation[] GetAllOperations();

        #endregion

        #region OperationReview

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OperationReview UpdateOperationReview(OperationReview operationReview);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOperationReview(int operationReviewId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OperationReview GetOperationReview(int operationReviewId);

        [OperationContract]
        OperationReview[] GetOperationReviews(string operationCode);

        //[OperationContract]
        //OperationReview[] GetAllOperationReviews();

        #endregion

        #region PolicyLevel

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PolicyLevel UpdatePolicyLevel(PolicyLevel policyLevel);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePolicyLevel(int policyLevelId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PolicyLevel GetPolicyLevel(int policyLevelId);

        [OperationContract]
        PolicyLevelData[] GetPolicyLevels(string year, string reviewCode);

        #endregion

        #region Policy

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Policy UpdatePolicy(Policy policy);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePolicy(int policyId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Policy GetPolicy(int policyId);

        [OperationContract]
        PolicyData[] GetAllPolicy();

        #endregion

        #region PrimaryLock

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PrimaryLock UpdatePrimaryLock(PrimaryLock primaryLock);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePrimaryLock(int primaryLockId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PrimaryLock GetPrimaryLock(int primaryLockId);

        [OperationContract]
        PrimaryLockData[] GetAllPrimaryLock();

        #endregion

        #region SecondaryLockLevel

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SecondaryLockLevel UpdateSecondaryLockLevel(SecondaryLockLevel secondaryLockLevel);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSecondaryLockLevel(int secondaryLockLevelId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SecondaryLockLevel GetSecondaryLockLevel(int secondaryLockLevelId);

        [OperationContract]
        SecondaryLockLevelData[] GetSecondaryLockLevels();

        #endregion

        #region SecondaryLock

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SecondaryLock UpdateSecondaryLock(SecondaryLock secondaryLock);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSecondaryLock(int secondaryLockId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SecondaryLock GetSecondaryLock(int secondaryLockId);

        [OperationContract]
        SecondaryLockData[] GetSecondaryLocks(string year, string reviewCode);

        #endregion
    }
}
