using System;
using System.Linq;
using System.ServiceModel;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Shared.Basic.Framework;

namespace Fintrak.Business.Basic.Contracts
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
        IFRSRegistryData[] GetAllIFRSRegistries();

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

    }
}
