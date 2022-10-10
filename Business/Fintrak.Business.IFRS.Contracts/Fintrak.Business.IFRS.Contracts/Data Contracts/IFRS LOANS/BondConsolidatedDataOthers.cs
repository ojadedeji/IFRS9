using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class BondConsolidatedDataOthers : DataContractBase
    {
        [DataMember]
        public string RefNo { get; set; }

        [DataMember]
        public string PortfolioID { get; set; }

        [DataMember]
        public double? CurrentMarketYield { get; set; }

        [DataMember]
        public double? EIR { get; set; }

        [DataMember]
        public string Narration { get; set; }

        [DataMember]
        public DateTime? EffectiveDate { get; set; }

        [DataMember]
        public int FairValueBasis { get; set; }
        [DataMember]
        public DateTime? LastCouponDate { get; set; }
        [DataMember]
        public DateTime? NextCouponDate { get; set; }

        [DataMember]
        public double? AccruedInterest { get; set; }

        [DataMember]
        public double? FairValueGain { get; set; }
        [DataMember]
        public DateTime? MaturityDate { get; set; }

        [DataMember]
        public double? FaceValue { get; set; }

        [DataMember]
        public double? CleanPrice { get; set; }

        [DataMember]
        public double? PremiumDiscount { get; set; }

        [DataMember]
        public decimal CouponRate { get; set; }

        [DataMember]
        public double? Price { get; set; }

        [DataMember]
        public double? FairValue { get; set; }

        [DataMember]
        public double? AmortizedCost { get; set; }

        [DataMember]
        public string Classification { get; set; }

        [DataMember]
        public DateTime RunDate { get; set; }
    }
}
