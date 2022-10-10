using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.Basic.Contracts
{
    [DataContract]
    public class TeamClassificationMapData : DataContractBase
    {
        [DataMember]
        public int TeamClassificationMapId { get; set; }

        [DataMember]
        public int TeamClassificationTypeId { get; set; }

        [DataMember]
        public string TeamClassificationTypeName { get; set; }

        [DataMember]
        public int TeamClassificationId { get; set; }

        [DataMember]
        public string TeamClassificationCode { get; set; }

        [DataMember]
        public string TeamClassificationName { get; set; }

        [DataMember]
        public int TeamId { get; set; }

        [DataMember]
        public string TeamCode { get; set; }

        [DataMember]
        public string TeamName { get; set; }


        [DataMember]
        public bool Active { get; set; }
    }
}
