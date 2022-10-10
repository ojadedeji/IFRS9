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
        BondComputation[] GetBondComputationResultbyRefNo(string refNo, DateTime? Date, string path);

        [OperationContract]
        BondComputation[] GetAllBondComputations();

        [OperationContract]
        BondComputation[] GetRefNoBondComputation();
        [OperationContract]
        //[FaultContract(typeof(NotFoundException))]
        string[] GetDistinctRefNo();

        #endregion

        #region BondPeriodicSchedule

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BondPeriodicSchedule[] GetBondPeriodicScheduleDistinctRefNo();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IEnumerable<string> GetDistinctBondPeriodicScheduleRefNos();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BondPeriodicSchedule[] GetBondPeriodicSchedulebyRefNo(string refNo, string path);

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

        [OperationContract]
        BondComputationResultZero[] GetRefNoBondComputationResultZero();

        #endregion


        #region LoanDetailedInfo

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LoanPry[] GetPryLoanBySearch(string searchParam);


        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        RawLoanDetails[] GetAllLoanDetailsBySearch(string searchParam);


        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CollateralDetails[] GetCollateralDetailsBySearch(string searchParam);


        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LoanECLResult[] GetLoanECLResultBySearch(string searchParam);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LoansECLComputationResult[] GetLoansECLComputationResultBySearch(string searchParam, string path);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Cashflow[] GetCashflowBySearch(string searchParam);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IfrsMonthlyEAD[] GetIfrsMonthlyEADBySearch(string searchParam);


        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        string[] GetDistinctLoanDetailsRefNos(int count);

        #endregion



        #region LoanPeriodicSchedule

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LoanPeriodicSchedule[] GetLoanPeriodicScheduleDistinctRefNo();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LoanPeriodicSchedule[] GetLoanPeriodicSchedulebyRefNo(string refNo, string path);

        [OperationContract]
        LoanPeriodicSchedule[] GetAllLoanPeriodicSchedules();

        [OperationContract]
        LoanPeriodicSchedule[] GetRefNoLoanPeriodicSchedule();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        void DeleteLoanPeriodicSchedulebyRefNo(string refNo);

        [OperationContract]
        //[FaultContract(typeof(NotFoundException))]
        string[] GetLoanPeriodicRefNo();

        #endregion

        #region LoanSchedule

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        string[] GetDistinctLoanScheduleRefNos();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MultiSelectDropDown[] GetLoanScheduleDistinctRefNo();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LoanSchedule[] GetLoanSchedulebyRefNo(string refNo);

        [OperationContract]
        LoanSchedule[] GetAllLoanSchedules();

        //[OperationContract]
        //LoanSchedule[] GetRefNo();

        [OperationContract]
        LoanSchedule[] GetScheduleRange(string refNo, DateTime? rangeDate, string path);


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

        #region BondConsolidatedData

        [OperationContract]
        BondConsolidatedData[] GetAllBondConsolidatedData();

        #endregion

        #region LoanConsolidatedData

        [OperationContract]
        LoanConsolidatedData[] GetAllLoanConsolidatedData();

        #endregion

        #region LoanConsolidatedDataFSDH

        [OperationContract]
        LoanConsolidatedDataFSDH[] GetAllLoanConsolidatedDataFSDH();

        #endregion

        #region TbillConsolidatedData

        [OperationContract]
        TbillConsolidatedData[] GetAllTbillConsolidatedData();

        #endregion

        #region BondConsolidatedDataOthers

        [OperationContract]
        BondConsolidatedDataOthers[] GetAllBondConsolidatedDataOthers();

        #endregion

        #region BorrowingPeriodicSchedule

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BorrowingPeriodicSchedule[] GetBorrowingPeriodicScheduleDistinctRefNo();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IEnumerable<string> GetDistinctBorrowingScheduleRefNos();

        [OperationContract]
        //[FaultContract(typeof(NotFoundException))]
        string[] GetDistinctBorrowingPeriodicScheduleRefNos();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BorrowingPeriodicSchedule[] GetBorrowingPeriodicSchedulebyRefNo(string refNo, string path);

        [OperationContract]
        BorrowingPeriodicSchedule[] GetAllBorrowingPeriodicSchedules();

        [OperationContract]
        BorrowingPeriodicSchedule[] GetRefNoBorrowingPeriodicSchedule();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        void DeleteBorrowingPeriodicSchedulebyRefNo(string refNo);

        [OperationContract]
        //[FaultContract(typeof(NotFoundException))]
        string[] GetBorrowingPeriodicRefNo();

        #endregion

        #region BorrowingSchedule

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BorrowingSchedule[] GetBorrowingScheduleDistinctRefNo();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BorrowingSchedule[] GetBorrowingSchedulebyRefNo(string refNo, DateTime? Date, string path);

        [OperationContract]
        BorrowingSchedule[] GetAllBorrowingSchedules();



        #endregion
      

       
    }
}
