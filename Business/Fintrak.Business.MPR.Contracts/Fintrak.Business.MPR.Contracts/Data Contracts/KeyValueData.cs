using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.SystemCore.Framework;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class KeyValueData : DataContractBase

    {

        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}