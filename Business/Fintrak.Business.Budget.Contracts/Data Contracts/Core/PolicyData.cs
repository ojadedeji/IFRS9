using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Budget.Framework.Enums;

namespace Fintrak.Business.Budget.Contracts
{
    [DataContract]
    public class PolicyData : DataContractBase
    {
        [DataMember]
        public int PolicyId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ModuleCode { get; set; }

        [DataMember]
        public string ModuleName { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
