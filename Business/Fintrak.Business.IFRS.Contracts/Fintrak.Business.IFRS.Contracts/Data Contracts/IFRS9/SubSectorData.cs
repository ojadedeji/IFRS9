using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class SubSectorData : DataContractBase
    {
        [DataMember]
        public int SubSectorId { get; set; }

        [DataMember]
        public string SectorCode { get; set; }

        [DataMember]
        public string SectorName { get; set; }


        [DataMember]
        public string SubSectorCode { get; set; }

        [DataMember]
        public string SubSectorName { get; set; }

        [DataMember]
        public string Source { get; set; }
       

        [DataMember]
        public bool Active { get; set; }
    }
}
