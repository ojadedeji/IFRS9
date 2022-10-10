using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Business.Scorecard.Contracts
{
    [DataContract]
    public class SCDThresholdData : DataContractBase
    {
        [DataMember]
        public int ThresholdId { get; set; }

        [DataMember]
        public string KPICode { get; set; }

        [DataMember]
        public string KPIName { get; set; }

        [DataMember]
        public string TeamClassificationCode { get; set; }

        [DataMember]
        public string TeamClassificationName { get; set; }

        [DataMember]
        public string StaffCode { get; set; }

        [DataMember]
        public string StaffName { get; set; }


        [DataMember]
        public double Minimum { get; set; }

        [DataMember]
        public double Maximum { get; set; }

        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
