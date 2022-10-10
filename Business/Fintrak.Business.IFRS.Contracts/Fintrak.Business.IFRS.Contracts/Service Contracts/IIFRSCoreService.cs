using System;
using System.Linq;
using System.ServiceModel;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Business.IFRS.Contracts
{
    [ServiceContract]
    public interface IIFRSCoreService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();

       #region IFRSRegistry

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IFRSRegistry UpdateIFRSRegistry(IFRSRegistry ifrsRegistry);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIFRSRegistry(int ifrsRegistryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IFRSRegistry GetIFRSRegistry(int ifrsRegistryId);

        [OperationContract]
        IFRSRegistryData[] GetAllIFRSRegistries(int flag);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        string[] GetDistinctRefNotes();

        #endregion IFRSRegistry

       #region DerivedCaption

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        DerivedCaption UpdateDerivedCaption(DerivedCaption derivedCaption);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteDerivedCaption(int derivedCaptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        DerivedCaption GetDerivedCaption(int derivedCaptionId);

        [OperationContract]
        DerivedCaption[] GetAllDerivedCaptions();


        #endregion DerivedCaption

       #region QualitativeNote


        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteQualitativeNote(int qualitativeNoteId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        QualitativeNote GetQualitativeNote(int qualitativeNoteId);

        [OperationContract]
        QualitativeNote[] GetAllQualitativeNotes();

        [OperationContract]
        void UpdateQualitativeNote(string refNote, string topNote, string bottomNote, DateTime runDate, int reportType);

        #endregion QualitativeNote

       #region InterimQualitativeNote


        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteInterimQualitativeNote(int qualitativeNoteId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        InterimQualitativeNote GetInterimQualitativeNote(int qualitativeNoteId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        InterimQualitativeNote[] GetInterimQualitativeNoteByType(int rType);

        [OperationContract]
        InterimQualitativeNote[] GetAllInterimQualitativeNotes();

        [OperationContract]
        void UpdateInterimQualitativeNote(string report, string topNote, string bottomNote, DateTime runDate, int reportType);

        [OperationContract]
        string[] GetReportNamesbyType(int rType);

        #endregion InterimQualitativeNote

       #region IFRSReportPackViewer

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IFRSReport[] GetAllRunDates();

        [OperationContract]
        IFRSReportPack[] GetAllIFRSReportPacks();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        string ReturnReportUrl(string reportName, DateTime runDate);

        #endregion IFRSReportPackViewer

       #region IFRSRevacctRegistry

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IFRSRevacctRegistry UpdateIFRSRevacctRegistry(IFRSRevacctRegistry iFRSRevacctRegistry);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIFRSRevacctRegistry(int iFRSRevacctRegistryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IFRSRevacctRegistry GetIFRSRevacctRegistry(int iFRSRevacctRegistryId);

        [OperationContract]
        IFRSRevacctRegistryData[] GetAllIFRSRevacctRegistries();

        //[OperationContract]
        //[FaultContract(typeof(NotFoundException))]
        //string[] GetDistinctRefNotes();

        #endregion IFRSRevacctRegistry
    }
}
