using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.Scorecard.Contracts
{
    [DataContract]
    public class SCDKPIClassificationData : DataContractBase
    {
        [DataMember]
        public int ClassificationId { get; set; }

        [DataMember]
        public string KPICode { get; set; }

        [DataMember]
        public string KPIName { get; set; }

        [DataMember]
        public string TeamClassificationCode { get; set; }

        [DataMember]
        public string TeamClassificationName { get; set; }

        [DataMember]
        public string CategoryCode { get; set; }

        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
