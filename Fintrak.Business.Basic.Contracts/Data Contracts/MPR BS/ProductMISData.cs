using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Core.Framework;


namespace Fintrak.Business.Basic.Contracts
{
    [DataContract]
    public class ProductMISData : DataContractBase
    {
        [DataMember]
        public int ProductMISId { get; set; }

        [DataMember]
        public string CaptionCode { get; set; }

        [DataMember]
        public string CaptionName { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public AccountTypeEnum Category { get; set; }

        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        public CurrencyType CurrencyType { get; set; }

        [DataMember]
        public string CurrencyTypeName { get; set; }

        [DataMember]
        public string TeamDefinitionCode { get; set; }

        [DataMember]
        public string AccountOfficerDefinitionCode { get; set; }

        [DataMember]
        public int? AccountOfficerDefinitionId { get; set; }

        [DataMember]
        public string AccountOfficerCode { get; set; }

        [DataMember]
        public string AccountOfficerName { get; set; }

        [DataMember]
        public string TeamCode { get; set; }

        [DataMember]
        public string TeamName { get; set; }

       [DataMember]
        public bool Active { get; set; }
    }
}
