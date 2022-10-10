using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.SystemCore.Entities;

namespace Fintrak.Business.IFRS.Contracts
{
    [ServiceContract]
    public interface IIFRSLoanService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();


        #region LoanSetup

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        LoanSetup UpdateLoanSetup(LoanSetup loanSetup);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteLoanSetup(int loanSetupId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LoanSetup GetLoanSetup(int loanSetupId);

        [OperationContract]
        LoanSetupData[] GetAllLoanSetups();

        #endregion

        #region ScheduleType

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ScheduleType UpdateScheduleType(ScheduleType scheduleType);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteScheduleType(int scheduleTypeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ScheduleType GetScheduleType(int scheduleTypeId);

        [OperationContract]
        ScheduleType[] GetAllScheduleTypes();

        #endregion

        #region IFRSProduct

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IFRSProduct UpdateIFRSProduct(IFRSProduct ifrsProduct);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIFRSProduct(int ifrsProductId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IFRSProduct GetIFRSProduct(int ifrsProductId);

        [OperationContract]
        IFRSProductData[] GetAllIFRSProducts();

        //[OperationContract]
        //[FaultContract(typeof(NotFoundException))]
        //ProductType GetLoanProducts(int productTypeId);

        #endregion

        #region CreditRiskRating

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CreditRiskRating UpdateCreditRiskRating(CreditRiskRating creditRiskRating);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCreditRiskRating(int creditRiskRatingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CreditRiskRating GetCreditRiskRating(int creditRiskRatingId);

        [OperationContract]
        CreditRiskRating[] GetAllCreditRiskRatings();

        #endregion

        #region CollateralCategory

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CollateralCategory UpdateCollateralCategory(CollateralCategory collateralCategory);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCollateralCategory(int collateralCategoryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CollateralCategory GetCollateralCategory(int collateralCategoryId);

        [OperationContract]
        CollateralCategory[] GetAllCollateralCategorys();

        #endregion

        #region CollateralType

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CollateralType UpdateCollateralType(CollateralType collateralType);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCollateralType(int collateralTypeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CollateralType GetCollateralType(int collateralTypeId);

        [OperationContract]
        CollateralTypeData[] GetAllCollateralTypes();

        [OperationContract]
        CollateralTypeData[] GetCollateralTypeByCategory(string categoryCode);

        #endregion

        #region CollateralreliazationPeriod

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CollateralRealizationPeriod UpdateCollateralRealizationPeriod(CollateralRealizationPeriod collateralRealizationPeriod);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCollateralRealizationPeriod(int collateralRealizationPeriodId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CollateralRealizationPeriod GetCollateralRealizationPeriod(int collateralRealizationPeriodId);

        [OperationContract]
        CollateralRealizationPeriodData[] GetAllCollateralRealizationPeriods();

        #endregion

        #region CollateralInformation

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CollateralInformation UpdateCollateralInformation(CollateralInformation collateralInformation);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCollateralInformation(int collateralInformationId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CollateralInformation GetCollateralInformation(int collateralInformationId);

        [OperationContract]
        CollateralInformationData[] GetAllCollateralInformations();

        #endregion

        #region WatchListedLoan

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        WatchListedLoan UpdateWatchListedLoan(WatchListedLoan watchListedLoan);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteWatchListedLoan(int watchListedLoanId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        WatchListedLoan GetWatchListedLoan(int watchListedLoanId);

        [OperationContract]
        WatchListedLoan[] GetAllWatchListedLoans();

        #endregion

        #region ImpairmentOverride

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ImpairmentOverride UpdateImpairmentOverride(ImpairmentOverride impairmentOverride);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteImpairmentOverride(int impairmentOverrideId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ImpairmentOverride GetImpairmentOverride(int impairmentOverrideId);

        [OperationContract]
        ImpairmentOverrideData[] GetAllImpairmentOverrides();

        #endregion

        #region IndividualSchedule

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IndividualSchedule UpdateIndividualSchedule(IndividualSchedule individualSchedule);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIndividualSchedule(int individualScheduleId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IndividualSchedule GetIndividualSchedule(int individualScheduleId);

        [OperationContract]
        IndividualScheduleData[] GetAllIndividualSchedules();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        string[] GetDistinctRefNo();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IndividualScheduleData[] GetIndividualSchedulebyRefNo(string refNo);

        #endregion

        #region lndividualImpairment

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IndividualImpairment UpdateIndividualImpairment(IndividualImpairment individualImpairment);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIndividualImpairment(int individualImpairmentId); 

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IndividualImpairment GetIndividualImpairment(int individualImpairmentId); 

        [OperationContract]
        IndividualImpairmentData[] GetAlllndividualImpairments();  

        [OperationContract]
        string[] GetAvailableReferenceNumbers();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IndividualImpairmentData[] GetIndividualImpairments(string refNo);

        #endregion

    }
}
