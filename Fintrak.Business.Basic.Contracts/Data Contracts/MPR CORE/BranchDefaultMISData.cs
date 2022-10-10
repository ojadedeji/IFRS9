using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.Basic.Contracts
{
    [DataContract]
    public class BranchDefaultMISData : DataContractBase
    {
        [DataMember]
        public int BranchDefaultMISId { get; set; }

        [DataMember]
        public int BranchId { get; set; }

        [DataMember]
        public string BranchName { get; set; }

        [DataMember]
        public int TeamDefinitionId { get; set; }

        [DataMember]
        public string TeamDefinitionName { get; set; }

        [DataMember]
        public int TeamId { get; set; }

        [DataMember]
        public string TeamCode { get; set; }

        [DataMember]
        public string TeamName { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
