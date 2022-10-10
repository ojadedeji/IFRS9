using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.Core.Contracts
{
    [DataContract]
    public class ExtractionRoleData : DataContractBase
    {
        [DataMember]
        public int ExtractionRoleId { get; set; }

        [DataMember]
        public int RoleId { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public int ExtractionId { get; set; }

        [DataMember]
        public string ExtractionName { get; set; }

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
