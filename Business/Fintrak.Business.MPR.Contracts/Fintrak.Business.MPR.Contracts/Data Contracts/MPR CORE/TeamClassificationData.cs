using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class TeamClassificationData : DataContractBase
    {
        [DataMember]
        public int TeamClassificationId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int TeamClassificationTypeId { get; set; }

        [DataMember]
        public string TeamClassificationTypeName { get; set; }

        [DataMember]
        public string ParentCode { get; set; }

        [DataMember]
        public string ParentName { get; set; }

        [DataMember]
        public int Level { get; set; }

        [DataMember]
        public Nullable<int> Period { get; set; }

        [DataMember]
        public int CompanyId { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
