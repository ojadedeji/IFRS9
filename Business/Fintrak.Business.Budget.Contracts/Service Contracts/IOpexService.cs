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
    public interface IOpexService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();

        #region OpexCategory

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexCategory UpdateOpexCategory(OpexCategory opexCategory);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexCategory(int opexCategoryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexCategory GetOpexCategory(int opexCategoryId);

        [OperationContract]
        OpexCategoryData[] GetOpexCategories(string year, string reviewCode);

        #endregion

        #region OpexEntry

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexEntry UpdateOpexEntry(OpexEntry feeEntry);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexEntry(int feeEntryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexEntry GetOpexEntry(int feeEntryId);

        [OperationContract]
        OpexEntryData[] GetOpexEntries(string year, string reviewCode);

        #endregion

        #region OpexItem

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexItem UpdateOpexItem(OpexItem opexItem);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexItem(int opexItemId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexItem GetOpexItem(int opexItemId);

        [OperationContract]
        OpexItemData[] GetOpexItems(string year, string reviewCode);

        #endregion

        #region OpexVolumeBasedRate

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexVolumeBasedRate UpdateOpexVolumeBasedRate(OpexVolumeBasedRate opexVolumeBasedRate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexVolumeBasedRate(int opexVolumeBasedRateId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexVolumeBasedRate GetOpexVolumeBasedRate(int opexVolumeBasedRateId);

        [OperationContract]
        OpexVolumeBasedRateData[] GetOpexVolumeBasedRates(string year, string reviewCode);

        #endregion

        #region OpexVolumeBasedSetup

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexVolumeBasedSetup UpdateOpexVolumeBasedSetup(OpexVolumeBasedSetup opexVolumeBasedSetup);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexVolumeBasedSetup(int opexVolumeBasedSetupId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexVolumeBasedSetup GetOpexVolumeBasedSetup(int opexVolumeBasedSetupId);

        [OperationContract]
        OpexVolumeBasedSetupData[] GetOpexVolumeBasedSetups(string year, string reviewCode);

        #endregion
    }
}
