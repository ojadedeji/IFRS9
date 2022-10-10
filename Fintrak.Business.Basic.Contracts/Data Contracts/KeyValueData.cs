using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Business.Basic.Contracts
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