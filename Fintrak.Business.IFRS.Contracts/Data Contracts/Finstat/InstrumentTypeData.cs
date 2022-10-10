using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class InstrumentTypeData : DataContractBase
    {
        [DataMember]

        public int InstrumentTypeId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public IFRSInstrument Instrument { get; set; }

        [DataMember]
        public string InstrumentName { get; set; }

        [DataMember]
        public int? ParentId { get; set; }
        [DataMember]
        public string ParentName { get; set; }


        [DataMember]
        public bool Active { get; set; }
    }
}
