using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class MISReplacementData : DataContractBase
    {
        [DataMember]
        public int MISReplacementId { get; set; }

        [DataMember]
        public string OldMISCode { get; set; }

        [DataMember]
        public int TeamDefinitionId { get; set; }

        [DataMember]
        public string TeamDefinitionName { get; set; }

        [DataMember]
        public int  TeamId { get; set; }

        [DataMember]
        public string TeamCode { get; set; }
        [DataMember]
        public string TeamName { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
