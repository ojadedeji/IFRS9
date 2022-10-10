using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.IFRS.Entities
{
    public partial class BondComputation : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int Id { get; set; }

        [DataMember]
        [Browsable(false)]
        public int IdNew { get; set; }



        [DataMember]
        [Required]
        public string RefNo { get; set; }

        [DataMember]
        [Required]
        public int Day { get; set; }

        [DataMember]
        public DateTime PaymentDate { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public double OpeningBalance { get; set; }

        [DataMember]
        public double AmountPrincInit { get; set; }

        [DataMember]
        public double DailyCoupon { get; set; }

        [DataMember]
        public double DailyCouponLessFV { get; set; }

        [DataMember]
        public double DailyInt { get; set; }

        [DataMember]
        public double DailyPrinc { get; set; }

        [DataMember]
        public double? DailyPrincLessFV { get; set; }

        [DataMember]
        public double? AmortizedPremiumDisc { get; set; }

        [DataMember]
        public double ClosingBalance { get; set; }

        [DataMember]
        public double AmountPrincEnd { get; set; }

        [DataMember]
        public double AccruedInterest { get; set; }

        [DataMember]
        public double AmortizedCost { get; set; }

        [DataMember]
        public double? DiscountPremium { get; set; }


        [DataMember]
        public double? UnAmortized { get; set; }

        [DataMember]
        public double? Amortized { get; set; }

        [DataMember]
        public decimal CouponRate { get; set; }

        [DataMember]
        public decimal IRR { get; set; }

        [DataMember]
        public int NoOfPeriods { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }
      
        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}
