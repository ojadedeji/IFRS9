using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.Core.Contracts
{
    [DataContract]
    public class ReportStatusData : DataContractBase
    {
        [DataMember]
        public int StatusId { get; set; }

        [DataMember]
        public int SolutionId { get; set; }

        [DataMember]
        public string SolutionName { get; set; }

        [DataMember]
        public bool Closed { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
