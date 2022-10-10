using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class OpexMISReplacementData : DataContractBase
    {
        [DataMember]
        public int OpexMISReplacementId { get; set; }

        [DataMember]
        public string OldMISCode { get; set; }

        [DataMember]
        public string MISCode { get; set; }

        [DataMember]
        public string MISName { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
