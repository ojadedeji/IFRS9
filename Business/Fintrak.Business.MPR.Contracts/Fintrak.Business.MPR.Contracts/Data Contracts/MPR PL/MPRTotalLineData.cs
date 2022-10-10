using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class MPRTotalLineData : DataContractBase
    {
        [DataMember]
        public int TotalLineIdId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public int? ParentId { get; set; }

        [DataMember]
        public string ParentName { get; set; }

        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
