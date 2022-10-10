using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public partial class LoanScheduleData : DataContractBase
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Sequence { get; set; }

        [DataMember]
        public string RefNo { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public DateTime PaymentDate { get; set; }

        [DataMember]
        public double OpeningBalance { get; set; }

        [DataMember]
        public double AmountPrincInit { get; set; }

        [DataMember]
        public double DailyInt { get; set; }

        [DataMember]
        public double DailyPrinc { get; set; }

        [DataMember]
        public double ClosingBalance { get; set; }

        [DataMember]
        public double AmountPrincEnd { get; set; }

        [DataMember]
        public double AccruedInterest { get; set; }

        [DataMember]
        public double AmortizedCost { get; set; }

        [DataMember]
        public decimal NorminalRate { get; set; } 

        public int AMSequence { get; set; }

        [DataMember]
        public string AMRefNo { get; set; }

        [DataMember]
        public DateTime AMDate { get; set; }

        [DataMember]
        public DateTime AMPaymentDate { get; set; }

        [DataMember]
        public double AMOpeningBalance { get; set; }

        [DataMember]
        public double AMAmountPrincInit { get; set; }

        [DataMember]
        public double AMDailyInt { get; set; }

        [DataMember]
        public double AMDailyPrinc { get; set; }

        [DataMember]
        public double? AMClosingBalance { get; set; }

        [DataMember]
        public double? AMAmountPrincEnd { get; set; }

        [DataMember]
        public double? AMAccruedInterest { get; set; }

        [DataMember]
        public double? AMAmortizedCost { get; set; }

        [DataMember]
        public double? BalloonAmt { get; set; }

        [DataMember]
        public double? DiscountPremium { get; set; }
    
        [DataMember]
        public double? UnearnedFee { get; set; }

        [DataMember]
        public double? EarnedFee { get; set; }

        [DataMember]
        public decimal EffectiveRate { get; set; }

        [DataMember]
        public int? NoOfPeriods { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }
    }
}
