using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class AutoPostingTemplateData : DataContractBase
    {
        [DataMember]
        public int AutoPostingTemplateId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Action { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
