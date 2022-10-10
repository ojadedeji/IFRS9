using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.Scorecard.Contracts
{
    [DataContract]
    public class SCDKPIActualMapData : DataContractBase
    {
        [DataMember]
        public int MapId { get; set; }

        [DataMember]
        public string KPICode { get; set; }

        [DataMember]
        public string KPIName { get; set; }
      
        [DataMember]
        public string Formula { get; set; }

        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
