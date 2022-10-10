using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class IFRSRevacctRegistryData : DataContractBase
    {
        [DataMember]
        public int RevenueId { get; set; }
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Caption { get; set; }

        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public string RefNote { get; set; }

        [DataMember]
        public string FinType { get; set; }

        [DataMember]
        public string FinSubType { get; set; }

        [DataMember]
        public int? ParentId { get; set; }

        [DataMember]
        public string ParentName { get; set; }

        [DataMember]
        public bool IsTotalLine { get; set; }

        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public int Class { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
