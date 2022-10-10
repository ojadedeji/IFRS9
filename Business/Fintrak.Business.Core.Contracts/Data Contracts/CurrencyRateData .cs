using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.Core.Contracts
{
    [DataContract]
    public class CurrencyRateData : DataContractBase
    {
        [DataMember]
        public int CurrencyRateId { get; set; }

        [DataMember]
        public int CurrencyId { get; set; }

        [DataMember]
        public string CurrencyName { get; set; }

        [DataMember]
        public int RateTypeId { get; set; }

        [DataMember]
        public string Frequency { get; set; }

        [DataMember]
        public string RateTypeName { get; set; }

        [DataMember]
        public double Rate { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public Nullable<DateTime> Rundate { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
