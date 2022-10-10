using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Business.IFRS.Contracts
{
    [ServiceContract]
    public interface IIFRSDataViewService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();
     
        #region BondComputationResult

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BondComputation[] GetBondComputationResultDistinctRefNo();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
       BondComputation[] GetBondComputationResultbyRefNo(string refNo);

        [OperationContract]
        BondComputation[] GetAllBondComputations();

        #endregion

        #region BondPeriodicSchedule

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BondPeriodicSchedule[] GetBondPeriodicScheduleDistinctRefNo();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BondPeriodicSchedule[] GetBondPeriodicSchedulebyRefNo(string refNo);

        [OperationContract]
        BondPeriodicSchedule[] GetAllBondPeriodicSchedules();

        #endregion

        #region BondComputationResultZero

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IEnumerable<string> GetBondComputationResultZeroDistinctRefNo();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BondComputationResultZero[] GetBondComputationResultZerobyRefNo(string refNo);

        [OperationContract]
        BondComputationResultZero[] GetBondComputationResultZeros();

        #endregion

        #region LoanPeriodicSchedule

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LoanPeriodicSchedule[] GetLoanPeriodicScheduleDistinctRefNo();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LoanPeriodicSchedule[] GetLoanPeriodicSchedulebyRefNo(string refNo);

        [OperationContract]
        LoanPeriodicSchedule[] GetAllLoanPeriodicSchedules();

        #endregion

        #region LoanSchedule

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LoanSchedule[] GetLoanScheduleDistinctRefNo();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LoanSchedule[] GetLoanSchedulebyRefNo(string refNo);

        [OperationContract]
        LoanSchedule[] GetAllLoanSchedules();

        #endregion

        #region LoansImpairmentResult

        [OperationContract]
        LoansImpairmentResult[] GetAllLoansImpairmentResults();

        #endregion

        #region TreasuryBills

        [OperationContract]
        TBillsComputationResult[] GetAllTBillsComputationResults();

        [OperationContract]
        TBillsComputationResult[] GetTBillsByClassification(string classification);
        #endregion

        #region EquityStocks

        [OperationContract]
        EquityStockComputationResult[] GetAllEquityStocks();

        [OperationContract]
        EquityStockComputationResult[] GetEquityStockByClassification(string classification);

        #endregion

       
    }
}
