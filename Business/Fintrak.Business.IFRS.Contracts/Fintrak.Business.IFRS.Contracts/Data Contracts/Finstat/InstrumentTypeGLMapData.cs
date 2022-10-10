using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class InstrumentTypeGLMapData : DataContractBase
    {

        [DataMember]
        public int InstrumentTypeGLMapId { get; set; }
        
        [DataMember]
        public int InstrumentTypeId { get; set; }

        [DataMember]
        public string InstrumentTypeName { get; set; }

        [DataMember]
        public string InstrumentName { get; set; }

        [DataMember]
        public int GLTypeId { get; set; }

        [DataMember]
        public string GLTypeName { get; set; }

        [DataMember]
        public int GLId { get; set; }

        [DataMember]
        public string GLCode { get; set; }

        [DataMember]
        public string GLName { get; set; }

        [DataMember]
        public int CompanyId { get; set; }
        
        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
