using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.Basic.Contracts
{
    [DataContract]
    public class AccountOfficerDetailData : DataContractBase
    {
        [DataMember]
        public int AccountOfficerDetailId { get; set; }

        [DataMember]
        public int AccountOfficerId { get; set; }

        [DataMember]
        public string AccountOfficerCode { get; set; }

        [DataMember]
        public string AccountOfficerName { get; set; }

        [DataMember]
        public string StaffID { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
