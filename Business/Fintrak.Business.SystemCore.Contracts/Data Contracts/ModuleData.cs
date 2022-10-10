using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.SystemCore.Contracts
{
    [DataContract]
    public class ModuleData : DataContractBase
    {
        [DataMember]
        public int ModuleId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Alias { get; set; }

        [DataMember]
        public int SolutionId { get; set; }

        [DataMember]
        public string SolutionName { get; set; }

        [DataMember]
        public bool CanUpdate { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
