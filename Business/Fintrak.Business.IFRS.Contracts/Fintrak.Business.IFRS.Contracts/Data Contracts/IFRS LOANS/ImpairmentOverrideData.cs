using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class ImpairmentOverrideData : DataContractBase
    {
        [DataMember]
        public int ImpairmentOverrideId { get; set; }

        [DataMember]
        public string RefNo { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public ImpairmentClassification Classification { get; set; }

        [DataMember]
        public string ClassificationName { get; set; }

        [DataMember]
        public string Reason { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
