using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.Core.Contracts
{
    [DataContract]
    public class BranchData : DataContractBase
    {
        [DataMember]
        public int BranchId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public int CompanyId { get; set; }


        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Contact { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
