using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.Core.Contracts
{
    [DataContract]
    public class ProductTypeMappingData : DataContractBase
    {
        [DataMember]
        public int ProductTypeMappingId { get; set; }

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public int ProductTypeId { get; set; }

        [DataMember]
        public string ProductTypeName { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
