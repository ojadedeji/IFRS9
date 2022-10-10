using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.Basic.Contracts
{
    [DataContract]
    public class MemoGLMapData : DataContractBase
    {
        [DataMember]
        public int MemoGLMapId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string GLCode { get; set; }

        [DataMember]
        public string GLDescription { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
