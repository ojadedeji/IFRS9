using System;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Budget.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.Budget.Contracts
{
    [DataContract]
    public class RevenueIncExpData : DataContractBase
    {
        [DataMember]
        public int RevenueIncExpId { get; set; }

        [DataMember]
        public string DefintionCode { get; set; }

        [DataMember]
        public string DefintionName { get; set; }

        [DataMember]
        public string MisCode { get; set; }

        [DataMember]
        public string MisName { get; set; }
      
        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public string CurrencyCode { get; set; }

        [DataMember]
        public string CurrencyName { get; set; }
     
        [DataMember]
        public string ReviewCode { get; set; }

        [DataMember]
        public string ReviewName { get; set; }

        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
