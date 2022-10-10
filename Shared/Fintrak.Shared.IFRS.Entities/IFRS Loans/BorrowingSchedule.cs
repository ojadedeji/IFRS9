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
    public partial class BorrowingSchedule : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public int Sequence { get; set; }

        [DataMember]
        [Required]
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
        public double DailyPayment { get; set; }

        [DataMember]
        public double DailyIntExpense { get; set; }


        [DataMember]
        public double DailyPrinc { get; set; }

        [DataMember]
        public double ClosingBalance { get; set; }

        [DataMember]
        public double AmountPrincEnd { get; set; }


        [DataMember]
        public double AccruedInterestExpense { get; set; }

        [DataMember]
        public double AmortizedCost { get; set; }

        [DataMember]
        public decimal NorminalRate { get; set; } 

        public int AMSequence { get; set; }

        [DataMember]
        [Required]
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
        public double AMDailyPayment { get; set; }

        [DataMember]
        public double AMDailyIntExpense { get; set; }


        [DataMember]
        public double AMDailyPrinc { get; set; }

        [DataMember]
        public double? AMClosingBalance { get; set; }

        [DataMember]
        public double? AMAmountPrincEnd { get; set; }


        [DataMember]
        public double? AMAccruedInterestExpense { get; set; }

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
      
        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}
