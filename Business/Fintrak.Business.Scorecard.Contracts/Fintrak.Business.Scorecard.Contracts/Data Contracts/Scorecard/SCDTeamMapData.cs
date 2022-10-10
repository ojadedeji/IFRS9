using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Business.Scorecard.Contracts
{
    [DataContract]
    public class SCDTeamMapData : DataContractBase
    {
        [DataMember]
        public int TeamMapId { get; set; }

        [DataMember]
        public OfficeType Centre { get; set; }

        [DataMember]
        public string CentreName { get; set; }

        [DataMember]
        public string TeamDefinitionCode { get; set; }

        [DataMember]
        public string MISCode { get; set; }

        [DataMember]
        public string MISName { get; set; }

        [DataMember]
        public string ParentCode { get; set; }

        [DataMember]
        public string StaffCode { get; set; }

        [DataMember]
        public string StaffName { get; set; }

        [DataMember]
        public string Grade { get; set; }

        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public string TeamClassificationCode { get; set; }

        [DataMember]
        public string TeamClassificationName { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
