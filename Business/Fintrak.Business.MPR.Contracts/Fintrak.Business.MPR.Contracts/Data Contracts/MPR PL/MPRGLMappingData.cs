using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class MPRGLMappingData : DataContractBase
    {
        [DataMember]
        public int MPRGLMappingId { get; set; }

        [DataMember]
        public string GLCode { get; set; }

        [DataMember]
        public string GLName { get; set; }

        [DataMember]
        public string CaptionCode { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public string CaptionName { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
