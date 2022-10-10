using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.SystemCore.Contracts
{
    [DataContract]
    public class UserSessionData : DataContractBase
    {
        [DataMember]
        public int UserSessionId { get; set; }

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public int DatabaseId { get; set; }
        [DataMember]
        public string DatabaseName { get; set; }

        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        public bool CanExpire { get; set; }
    }
}
