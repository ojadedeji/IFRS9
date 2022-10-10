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
    public class MPRTotalLineMakeUpData : DataContractBase
    {
        [DataMember]
        public int TotalLineMakeUpId { get; set; }

       [DataMember]
        public string TotalLine { get; set; }

        [DataMember]
        public string CaptionCode  { get; set; }

       [DataMember]
        public string CaptionName { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
