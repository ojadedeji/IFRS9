using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.Core.Contracts
{
    [DataContract]
    public class ClosedPeriodTemplateData : DataContractBase
    {
        [DataMember]
        public int ClosedPeriodTemplateId { get; set; }

        [DataMember]
        public int SolutionId { get; set; }

        [DataMember]
        public string SolutionName { get; set; }

        [DataMember]
        public string Action { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
