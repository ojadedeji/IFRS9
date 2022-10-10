using System;
using System.Linq;
using System.ServiceModel;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Business.Core.Contracts
{
    [ServiceContract]
    public interface IExtractionProcessService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();

       #region Extraction

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Extraction UpdateExtraction(Extraction extraction);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteExtraction(int extractionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Extraction GetExtraction(int extractionId);

        [OperationContract]
        Extraction[] GetAllExtractions();

        [OperationContract]
        ExtractionData[] GetExtractions();

        [OperationContract]
        ExtractionData[] GetExtractionByLogin(string loginID);

        [OperationContract]
        ExtractionData[] GetExtractionBySolution(int solutionId, string loginID);

        #endregion Extraction

       #region ExtractionRole

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ExtractionRole UpdateExtractionRole(ExtractionRole extractionRole);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteExtractionRole(int extractionRoleId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ExtractionRole GetExtractionRole(int extractionRoleId);

        [OperationContract]
        ExtractionRole[] GetAllExtractionRoles();

        [OperationContract]
        ExtractionRoleData[] GetExtractionRoles();

        [OperationContract]
        ExtractionRoleData[] GetExtractionRoleByExtraction(int extractionId);

        #endregion ExtractionRole

       #region PackageSetup

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PackageSetup UpdatePackageSetup(PackageSetup packageSetup);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PackageSetup GetFirstPackageSetup();

        #endregion PackageSetup

       #region Process

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Processes UpdateProcess(Processes process);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProcess(int processId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Processes GetProcess(int processId);

        [OperationContract]
        Processes[] GetAllProcesses();

        [OperationContract]
        ProcessData[] GetProcesses();

        [OperationContract]
        ProcessData[] GetProcessBySolution(int solutionId,string loginID);

        #endregion Process

       #region ProcessRole

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProcessRole UpdateProcessRole(ProcessRole processRole);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProcessRole(int processRoleId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProcessRole GetProcessRole(int processRoleId);

        [OperationContract]
        ProcessRole[] GetAllProcessRoles();

        [OperationContract]
        ProcessRoleData[] GetProcessRoles();

        [OperationContract]
        ProcessRoleData[] GetProcessRoleByProcess(int processId);


        #endregion ProcessRole

       #region ExtractionTrigger

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ExtractionTrigger UpdateExtractionTrigger(ExtractionTrigger extractionTrigger);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteExtractionTrigger(int extractionTriggerId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ExtractionTrigger GetExtractionTrigger(int extractionTriggerId);

        [OperationContract]
        ExtractionTrigger[] GetAllExtractionTriggers();

        [OperationContract]
        ExtractionTriggerData[] GetExtractionTriggers();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ExtractionTriggerData[] GetExtractionTriggerByExtraction(int extractionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ExtractionTriggerData[] GetExtractionTriggerByJob(string jobCode);

       [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ExtractionTriggerData[] GetExtractionTriggerByRunDate(DateTime startDate,DateTime endDate);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
       ExtractionTriggerData[] GetExtractionTriggerByRunTime(DateTime runTime);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ExtractionTriggerData[] RunExtraction(int jobId,int[] extractionIds, DateTime startDate, DateTime endDate, DateTime runTime);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ExtractionTriggerData[] CancelExtractions(DateTime startDate, DateTime endDate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ExtractionTriggerData[] CancelExtractionByCode(string code,DateTime startDate, DateTime endDate);

       #endregion ExtractionTrigger

       #region ProcessTrigger

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProcessTrigger UpdateProcessTrigger(ProcessTrigger processTrigger);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProcessTrigger(int processTriggerId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProcessTrigger GetProcessTrigger(int processTriggerId);

        [OperationContract]
        ProcessTrigger[] GetAllProcessTriggers();

        [OperationContract]
        ProcessTriggerData[] GetProcessTriggers();

        [OperationContract]
        ProcessTriggerData[] GetProcessTriggerByProcess(int processId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProcessTriggerData[] GetProcessTriggerByRunDate();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProcessTriggerData[] GetProcessTriggerByRunTime(DateTime runTime);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProcessTriggerData[] GetProcessTriggerByJob(string jobCode);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProcessTriggerData[] RunProcess(int jobId, int[] processIds, DateTime runTime);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProcessTriggerData[] CancelProcesses(DateTime startDate, DateTime endDate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProcessTriggerData[] CancelProcessByCode(string code,DateTime startDate, DateTime endDate);

        #endregion ProcessTrigger

       #region SolutionRunDate

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SolutionRunDate UpdateSolutionRunDate(SolutionRunDate solutionRunDate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSolutionRunDate(int solutionRunDateId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SolutionRunDate GetSolutionRunDate(int solutionRunDateId);

        [OperationContract]
        SolutionRunDate[] GetAllSolutionRunDates();

        [OperationContract]
        SolutionRunDateData[] GetSolutionRunDates();

        [OperationContract]
        SolutionRunDateData[] GetSolutionRunDateByLogin(string loginID);

        [OperationContract]
        string GetSolutionRunDateByLoginByDefault(string loginID);

        [OperationContract]
        SolutionRunDate[] GetRunDate();

        [OperationContract]
        void RestoreArchive(int solutionid, DateTime date);


        #endregion SolutionRunDate

       #region ClosedPeriod

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ClosedPeriod UpdateClosedPeriod(ClosedPeriod closedPeriod);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteClosedPeriod(int closedPeriodId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ClosedPeriod GetClosedPeriod(int closedPeriodId);

        [OperationContract]
        ClosedPeriod[] GetAllClosedPeriods();

        [OperationContract]
        ClosedPeriodData[] GetClosedPeriods();

        [OperationContract]
        ClosedPeriodData[] GetClosedPeriodByLogin(string loginID);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ClosedPeriod ClosePeriod(ClosedPeriod closedPeriod);

        #endregion ClosedPeriod

       #region ClosedPeriodTemplate

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ClosedPeriodTemplate UpdateClosedPeriodTemplate(ClosedPeriodTemplate closedPeriodTemplate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteClosedPeriodTemplate(int closedPeriodTemplateId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ClosedPeriodTemplate GetClosedPeriodTemplate(int closedPeriodTemplateId);

        [OperationContract]
        ClosedPeriodTemplate[] GetAllClosedPeriodTemplates();

        [OperationContract]
        ClosedPeriodTemplateData[] GetClosedPeriodTemplates();

        [OperationContract]
        ClosedPeriodTemplateData[] GetClosedPeriodTemplateByLogin(string loginID);

        #endregion ClosedPeriodTemplate

       #region ExtractionJob

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ExtractionJob UpdateExtractionJob(ExtractionJob extractionJob);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteExtractionJob(int extractionJobId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ExtractionJob GetExtractionJob(int extractionJobId);

        [OperationContract]
        ExtractionJob[] GetCurrentExtractionJobs();

        [OperationContract]
        ExtractionJob[] GetExtractionJobByDate(DateTime startDate,DateTime endDate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ExtractionJob[] RunExtractionJob(int jobId, int[] extractionIds, DateTime startDate, DateTime endDate, DateTime runTime);

        [OperationContract]
        ExtractionJob[] CancelExtractionJobByCode(string jobCode,DateTime startDate, DateTime endDate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void ClearExtractionHistory(int solutionId);

        #endregion ExtractionJob

       #region ProcessJob

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProcessJob UpdateProcessJob(ProcessJob processJob);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProcessJob(int processJobId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProcessJob GetProcessJob(int processJobId);

        [OperationContract]
        ProcessJob[] GetCurrentProcessJobs();

        [OperationContract]
        ProcessJob[] GetProcessJobByRunDate();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProcessJob[] RunProcessJob(int jobId, int[] processIds, DateTime runTime);

        [OperationContract]
        ProcessJob[] CancelProcessJobByCode(string jobCode);

        [OperationContract]
        void RestartService(string serviceName);

        [OperationContract]
        string GetServiceStatus(string serviceName);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void ClearProcessHistory(int solutionId);

        #endregion ProcessJob

       #region Upload

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Upload UpdateUpload(Upload upload);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteUpload(int uploadId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Upload GetUpload(int uploadId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Upload[] GetUploadBySolution(int solutionId);

        [OperationContract]
        Upload[] GetAllUploads();

        [OperationContract]
        UploadData[] GetUploads();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.NotAllowed)]
        UploadResult[] UploadCSV(int uploadId, string csvText);//, bool truncate, bool postUploadAction

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.NotAllowed)]
        UploadResult[] UploadCSVByCode(string uploadCode, string csvText);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.NotAllowed)]
        UploadResult[] VerificationMsg(string sppVerify);

  
        #endregion

       #region UploadRole

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        UploadRole UpdateUploadRole(UploadRole uploadRole);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteUploadRole(int uploadRoleId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        UploadRole GetUploadRole(int uploadRoleId);

        [OperationContract]
        UploadRole[] GetAllUploadRoles();

        [OperationContract]
        UploadRoleData[] GetUploadRoles();

        [OperationContract]
        UploadRoleData[] GetUploadRoleByUpload(int uploadId);

        #endregion

       #region CheckDataAvailability

        [OperationContract]
        CheckDataAvailability[] GetAllDataAvailability();

        [OperationContract]
        void CheckDataAvailabilitybyRunDate(DateTime runDate);

        #endregion

       #region CheckifrsDataAvailability

        [OperationContract]
        CheckifrsDataAvailability[] GetAllifrsDataAvailability();

        [OperationContract]
        void CheckifrsDataAvailabilitybyRunDate(DateTime runDate);

        #endregion
    }
}
