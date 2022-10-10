using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class TbillConsolidatedData : DataContractBase
    {
        [DataMember]
        public string RefNo { get; set; }
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime ValueDate { get; set; }

        [DataMember]
        public DateTime MaturityDate { get; set; }

        [DataMember]
        public int TotalTenor { get; set; }

        [DataMember]
        public int UsedDays { get; set; }

        [DataMember]
        public int DaysToMaturity { get; set; }

        

        [DataMember]
        public decimal Rate { get; set; }

        [DataMember]
        public double FaceValue { get; set; }

        [DataMember]
        public double CleanPrice { get; set; }

        [DataMember]
        public decimal CurrentMarketYield { get; set; }

        [DataMember]
        public double ComputedMarketPrice { get; set; }

        [DataMember]
        public decimal EIR { get; set; }

        [DataMember]
        public double Price { get; set; }

        [DataMember]
        public double AmortizedCost { get; set; }

        [DataMember]
        public double FairValue { get; set; }

        [DataMember]
        public double IntrestReceivable { get; set; }
        [DataMember]
        public double FairValueGain { get; set; }

        [DataMember]
        public string Classification { get; set; }
        [DataMember]
        public DateTime RunDate { get; set; }

    }
}
