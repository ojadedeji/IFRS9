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
    public partial class LoanPry : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int PryId { get; set; }

        [DataMember]
        //[Required]
        public string RefNo { get; set; }

        [DataMember]
        [Required]
        public string AccountNo { get; set; }

        [DataMember]
        public string ProductCategory { get; set; }

        [DataMember]
        //[Required]
        public string ProductCode { get; set; }

        [DataMember]
        //[Required]
        public string ProductName { get; set; }

        [DataMember]
        public string ProductType { get; set; }

        [DataMember]
        [Required]
        public DateTime ValueDate { get; set; }

        [DataMember]
        public string Sector { get; set; }

        [DataMember]
        public string Currency { get; set; }
        
        [DataMember]
        [Required]
        public DateTime MaturityDate { get; set; }

        [DataMember]
        [Required]
        public Nullable<DateTime> FirstRepaymentdate { get; set; }

        [DataMember]
        [Required]
        public Nullable<DateTime> InterestFirstRepayDate { get; set; }

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public Nullable<double> PeriodicRepaymentAmount { get; set; }

        [DataMember]
        public decimal ExchangeRate { get; set; }

        [DataMember]
        public decimal Rate { get; set; }

        [DataMember]
        public int? Tenor { get; set; }

        [DataMember]
        public int InterestRepayFreq { get; set; }

        [DataMember]
        public int PrincipalRepayFreq { get; set; }

        [DataMember]
        public string Schedule_Type { get; set; }
        

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }



        //ACCESS START

        [DataMember]
        public string custid { get; set; }

        [DataMember]
        public Nullable<double> OutstandingBal { get; set; }

        [DataMember]
        public double? PastDue { get; set; }

        [DataMember]
        public string InitialCreditRating { get; set; }

        [DataMember]
        public string current_staging { get; set; }

        [DataMember]
        public string rating { get; set; }

        [DataMember]
        public string Classification { get; set; }

        [DataMember]
        public string CollateralType { get; set; }

        [DataMember]
        public Nullable<double> CollateralValue { get; set; }

        [DataMember]
        public string ForbearanceFlag { get; set; }

        [DataMember]
        public Nullable<double> ForcedSaleValue { get; set; }

        [DataMember]
        public Nullable<double> CashOMV { get; set; }

        [DataMember]
        public Nullable<double> CashFSV { get; set; }

        [DataMember]
        public Nullable<double> CostRecovery { get; set; }

        [DataMember]
        public int? MissedPaymentStage { get; set; }

        [DataMember]
        public int? ClassificationStage { get; set; }

        [DataMember]
        public int? ForbearanceStage { get; set; }

        [DataMember]
        public int? CreditStaging { get; set; }

        [DataMember]
        public int? ModelClassification { get; set; }

        [DataMember]
        public int? ClassificationOverride { get; set; }

        [DataMember]
        public int? ClassificationStageFinal { get; set; }

        //ACCESS END

        public int EntityId
        {
            get
            {
                return PryId;
            }
        }
    }
}
