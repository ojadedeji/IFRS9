using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.Basic.Contracts
{
    [DataContract]
    public class OpexManagementTreeData : DataContractBase
    {
        [DataMember]
        public int OpexMgtTreeId { get; set; }

        [DataMember]
        public string CostCentreMISCode { get; set; }

        [DataMember]
        public string TeamDefinitionCode { get; set; }

        [DataMember]
        public string TeamDefinitionName { get; set; }

        [DataMember]
        public string TeamCode { get; set; }

        [DataMember]
        public string TeamName { get; set; }

        [DataMember]
        public string AccountOfficerDefinitionCode { get; set; }

        [DataMember]
        public string AccountOfficerDefinitionName { get; set; }

        [DataMember]
        public string AccountOfficerCode { get; set; }

        [DataMember]
        public string AccountOfficerName { get; set; }

        [DataMember]
        public double Ratio { get; set; }


        [DataMember]
        public bool Active { get; set; }
    }
}
