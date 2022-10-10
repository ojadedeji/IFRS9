using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.Basic.Contracts
{
    [DataContract]
    public class MPRSetupData : DataContractBase
    {
        [DataMember]
        public int MPRSetupId { get; set; }

        [DataMember]
        public string TeamDefinitionCode { get; set; }

        [DataMember]
        public string TeamDefinitionName { get; set; }

        [DataMember]
        public string TeamCode { get; set; }

        [DataMember]
        public string TeamName { get; set; }

        [DataMember]
        public int AccountLength { get; set; }

        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public Nullable<int> Period { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
