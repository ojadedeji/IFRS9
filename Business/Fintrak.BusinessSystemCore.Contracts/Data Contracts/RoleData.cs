using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.SystemCore.Framework;

namespace Fintrak.Business.SystemCore.Contracts
{
    [DataContract]
    public class RoleData : DataContractBase
    {
        [DataMember]
        public int RoleId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public RoleType Type { get; set; }

        [DataMember]
        public string TypeName { get; set; }

        [DataMember]
        public int SolutionId { get; set; }

        [DataMember]
        public string SolutionName { get; set; }

        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        public string LongDescription { get; set; }

    }
}
