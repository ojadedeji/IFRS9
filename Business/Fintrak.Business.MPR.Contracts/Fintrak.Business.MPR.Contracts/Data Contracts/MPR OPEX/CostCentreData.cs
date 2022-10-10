using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class CostCentreData : DataContractBase
    {
        [DataMember]
        public int CostCentreId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string DefinitionCode { get; set; }

        [DataMember]
        public string DefinitionName { get; set; }

        [DataMember]
        public string Parent { get; set; }

        [DataMember]
        public string ParentName { get; set; }

        [DataMember]
        public string Year { get; set; }   

        [DataMember]
        public bool Active { get; set; }
    }
}
