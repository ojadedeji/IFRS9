using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.SystemCore.Contracts
{
    [DataContract]
    public class CompanyModuleData : DataContractBase
    {
        [DataMember]
        public int CompanyModuleId { get; set; }

        [DataMember]
        public string ModuleName { get; set; }

        [DataMember]
        public string Alias { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
