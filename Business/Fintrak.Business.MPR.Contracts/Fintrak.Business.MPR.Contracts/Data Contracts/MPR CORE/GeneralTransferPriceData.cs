using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class GeneralTransferPriceData : DataContractBase
    {
        [DataMember]
        public int GeneralTransferPriceId { get; set; }

        [DataMember]
        public AccountTypeEnum Category { get; set; }

        [DataMember]
        public String CategoryName { get; set; }

        [DataMember]
        public CurrencyType CurrencyType { get; set; }

        [DataMember]
        public String CurrencyTypeName { get; set; }

        [DataMember]
        public double Rate { get; set; }

        [DataMember]
        public string DefinitionCode { get; set; }

        [DataMember]
        public string DefinitionName { get; set; }

        [DataMember]
        public string MISCode { get; set; }

        [DataMember]
        public string MISName { get; set; }

        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public int Period { get; set; }

   
        [DataMember]
        public string CompanyCode { get; set; }


        [DataMember]
        public bool Active { get; set; }
    }
}

