using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.Basic.Contracts
{
    [DataContract]
    public class GLMappingData : DataContractBase
    {
        [DataMember]
        public int GLMappingId { get; set; }

        [DataMember]
        public string GLCode { get; set; }
        [DataMember]
        public string GLDescription { get; set; }

        [DataMember]
        public string GLParentCode { get; set; }
        [DataMember]
        public string CaptionCode { get; set; }

        [DataMember]
        public string MainCaption { get; set; }
        [DataMember]
        public string SubCaption { get; set; }
        [DataMember]
        public string SubCaption1 { get; set; }

        [DataMember]
        public string SubCaption2 { get; set; }

        public string SubCaption3 { get; set; }
        [DataMember]
        public string SubCaption4 { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }
        [DataMember]
        public int SubPosition { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
