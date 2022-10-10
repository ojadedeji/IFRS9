using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class MemoAccountMapData : DataContractBase
    {
        [DataMember]
        public int MemoAccountMapId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public string AccountName { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
