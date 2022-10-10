using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.Core.Contracts
{
    [DataContract]
    public class DefaultUserData : DataContractBase
    {
        [DataMember]
        public int DefaultUserId { get; set; }

        [DataMember]
        public int SolutionId { get; set; }

        [DataMember]
        public string SolutionName { get; set; }

        [DataMember]
        public String LoginID { get; set; }

        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
