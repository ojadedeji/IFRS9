using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Budget.Framework.Enums;

namespace Fintrak.Business.Budget.Contracts
{
    [DataContract]
    public class SecondaryLockData : DataContractBase
    {
        [DataMember]
        public int SecondaryLockId { get; set; }

        [DataMember]
        public string ModuleCode { get; set; }

        [DataMember]
        public string ModuleName { get; set; }

        [DataMember]
        public string DefinitionCode { get; set; }

        [DataMember]
        public string DefinitionName { get; set; }

        [DataMember]
        public string MisCode { get; set; }

        [DataMember]
        public string Note { get; set; }
                
        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
