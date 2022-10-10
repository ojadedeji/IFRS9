using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class OpexAbcExemptionData : DataContractBase
    {
        [DataMember]
        public int OpexAbcExemptionId { get; set; }

        [DataMember]
        public string MisCode    { get; set; }

        [DataMember]
        public string MisName { get; set; }
        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
