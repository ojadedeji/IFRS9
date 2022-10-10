using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Budget.Framework.Enums;

namespace Fintrak.Business.Budget.Contracts
{
    [DataContract]
    public class PrimaryLockData : DataContractBase
    {
        [DataMember]
        public int PrimaryLockId { get; set; }

        [DataMember]
        public string DefinitionCode { get; set; }

        [DataMember]
        public string DefinitionName { get; set; }

        [DataMember]
        public string MisCode { get; set; }

        [DataMember]
        public string Note { get; set; }

        [DataMember]
        public bool Lock { get; set; }

        [DataMember]
        public bool CanOverride { get; set; }
                
        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
