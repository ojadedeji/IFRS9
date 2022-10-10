using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class UnMappedProduct : DataContractBase
    {
        
        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public bool InGlobalProduct { get; set; }

        [DataMember]
        public bool InIFRSProduct { get; set; }

    }
}
