using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class BondConsolidatedData : DataContractBase
    {
        [DataMember]
        public string RefNo { get; set; }

        [DataMember]
        public string PortfolioID { get; set; }

        [DataMember]
        public string Narration { get; set; }

        [DataMember]
        public DateTime ValueDate { get; set; }

        [DataMember]
        public DateTime IssueDate { get; set; }

        [DataMember]
        public DateTime MaturityDate { get; set; }

        [DataMember]
        public DateTime FirstCouponDate { get; set; }

        [DataMember]
        public double FaceValue { get; set; }

        [DataMember]
        public double CleanPrice { get; set; }

        [DataMember]
        public double PremiumDiscount { get; set; }

        [DataMember]
        public decimal CouponRate { get; set; }

        [DataMember]
        public double OpeningBalance { get; set; }

        [DataMember]
        public double DailyCoupon { get; set; }

        [DataMember]
        public double DailyInt { get; set; }

        [DataMember]
        public double DailyPrinc { get; set; }

        [DataMember]
        public double UnAmortized { get; set; }

        [DataMember]
        public double Amortized { get; set; }

        [DataMember]
        public double ClosingBalance { get; set; }

        [DataMember]
        public double AmortizedCost { get; set; }

        [DataMember]
        public decimal IRR { get; set; }

        [DataMember]
        public string Classification { get; set; }

        [DataMember]
        public DateTime Date { get; set; }
    }
}
