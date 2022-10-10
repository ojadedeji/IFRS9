using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.Basic.Contracts
{
    [DataContract]
    public class IFRSProductData : DataContractBase
    {
        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string ScheduleTypeCode { get; set; }

        [DataMember]
        public string ScheduleTypeName { get; set; }

        [DataMember]
        public double MarketRate { get; set; }

        [DataMember]
        public double PastDueRate { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
