using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.Core.Contracts
{
    [DataContract]
    public class FinancialTypeData : DataContractBase
    {
        [DataMember]
        public int FinancialTypeId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }
       
        [DataMember]
        public int? ParentId { get; set; }

        [DataMember]
        public string ParentName { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
