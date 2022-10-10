using System;
using System.Linq;
using System.ServiceModel;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.CDQM.Entities;

namespace Fintrak.Business.CDQM.Contracts
{
    [ServiceContract]
    public interface ICDQMService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();


        #region CDQMAddress

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CDQMAddress UpdateCDQMAddress(CDQMAddress cdqmAddress);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCDQMAddress(int addressId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CDQMAddress GetCDQMAddress(int addressId);

        [OperationContract]
        CDQMAddress[] GetAllCDQMAddresses();

        #endregion

        #region CDQMCountry

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CDQMCountry UpdateCDQMCountry(CDQMCountry cdqmCountry);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCDQMCountry(int countryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CDQMCountry GetCDQMCountry(int countryId);

        [OperationContract]
        CDQMCountry[] GetAllCDQMCountries();

        #endregion

        #region CDQMCustomerMIS

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CDQMCustomerMIS UpdateCDQMCustomerMIS(CDQMCustomerMIS cdqmCustomerMIS);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCDQMCustomerMIS(int customerMISId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CDQMCustomerMIS GetCDQMCustomerMIS(int customerMISId);

        [OperationContract]
        CDQMCustomerMIS[] GetAllCDQMCustomerMIS();

        #endregion

        #region CDQMETLConfiguration

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CDQMETLConfiguration UpdateCDQMETLConfiguration(CDQMETLConfiguration cdqmETLConfiguration);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCDQMETLConfiguration(int etlConfigurationId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CDQMETLConfiguration GetCDQMETLConfiguration(int etlConfigurationId);

        [OperationContract]
        CDQMETLConfiguration[] GetAllCDQMETLConfigurations();

        #endregion

        #region CDQMGenderGroup

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CDQMGenderGroup UpdateCDQMGenderGroup(CDQMGenderGroup cdqmGenderGroup);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCDQMGenderGroup(int genderGroupId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CDQMGenderGroup GetCDQMGenderGroup(int genderGroupId);

        [OperationContract]
        CDQMGenderGroup[] GetAllCDQMGenderGroups();

        #endregion

        #region CDQMMerchant

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CDQMMerchant UpdateCDQMMerchant(CDQMMerchant cdqmMerchant);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCDQMMerchant(int merchantId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CDQMMerchant GetCDQMMerchant(int merchantId);

        [OperationContract]
        CDQMMerchant[] GetAllCDQMMerchants();

        #endregion

        #region CDQMProduct

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CDQMProduct UpdateCDQMProduct(CDQMProduct cdqmProduct);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCDQMProduct(int productId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CDQMProduct GetCDQMProduct(int productId);

        [OperationContract]
        CDQMProduct[] GetAllCDQMProducts();

        #endregion

        #region CDQMTitle

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CDQMTitle UpdateCDQMTitle(CDQMTitle cdqmTitle);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCDQMTitle(int titleId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CDQMTitle GetCDQMTitle(int titleId);

        [OperationContract]
        CDQMTitle[] GetAllCDQMTitles();

        #endregion

        #region CDQMCustomerDuplicate

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CDQMCustomerDuplicate UpdateCDQMCustomerDuplicate(CDQMCustomerDuplicate cdqmCustomerDuplicate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCDQMCustomerDuplicate(int cod_CUST_ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CDQMCustomerDuplicate GetCDQMCustomerDuplicate(int cod_CUST_ID);

        [OperationContract]
        CDQMCustomerDuplicate[] GetAllCDQMCustomerDuplicates();

        [OperationContract]
        string[] GetCustomerGroupIDs();

        #endregion

        #region CDQMCustomerPersistent

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CDQMCustomerPersistent UpdateCDQMCustomerPersistent(CDQMCustomerPersistent cdqmCustomerPersistent);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCDQMCustomerPersistent(int cod_CUST_ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CDQMCustomerPersistent GetCDQMCustomerPersistent(int cod_CUST_ID);

        [OperationContract]
        CDQMCustomerPersistent[] GetAllCDQMCustomerPersistents();

        [OperationContract]
        CDQMCustomerPersistent[] GetCustomerPersistents(string groupId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CDQMCustomerPersistent[] UpdateCustomer(CDQMCustomerPersistent cdqmCustomerPersistent);

        #endregion
    }
}
