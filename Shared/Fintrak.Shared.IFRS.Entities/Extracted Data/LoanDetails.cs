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
    public partial class RawLoanDetails : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int LoanDetailId { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }

        [DataMember]
        //[Required]
        public string AccountNo { get; set; }

        [DataMember]
        public Nullable<double> Amount { get; set; }

        [DataMember]
        public Nullable<double> PastDueAmount { get; set; }

        [DataMember]
        public Nullable<double> ODLimit { get; set; }

        [DataMember]
        public Nullable<double> CollateralHaircut { get; set; }

        [DataMember]
        public Nullable<double> CollateralRecoverableAmt { get; set; }

        [DataMember]
        public string Segment { get; set; }

        [DataMember]
        public string CollateralType { get; set; }

        public Nullable<double> PrincipalOutstandingBal { get; set; }

        public Nullable<double> Interest_Receiv_Pay_UnEarn { get; set; }

        [DataMember]
        [Required]
        public string ProductCode { get; set; }

        [DataMember]
        //[Required]
        public string ProductName { get; set; }

        [DataMember]
        //[Required]
        public string ProductType { get; set; }

        [DataMember]
        //[Required]
        public string Sub_Type { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        [Required]
        public Nullable<DateTime> ValueDate { get; set; }

        [DataMember]
        [Required]
        public Nullable<DateTime> MaturityDate { get; set; }

        [DataMember]
        public Nullable<DateTime> FirstRepaymentdate { get; set; }

        [DataMember]
        public Nullable<DateTime> PrincipalFirstRepaymentDate { get; set; }

        [DataMember]
        public int? InterestRepayFreq { get; set; }

        [DataMember]
        public int? PrincipalRepayFreq { get; set; }

        [DataMember]
        public int? Stage { get; set; }

        [DataMember]
        public Nullable<decimal> ExchangeRate { get; set; }

        [DataMember]
        public Nullable<decimal> Rate { get; set; }

        [DataMember]
        public Nullable<DateTime> LastCrDate { get; set; }

        [DataMember]
        public Nullable<bool> LD { get; set; }

        [DataMember]
        public string Classification { get; set; }

        [DataMember]
        public Nullable<double> CollateralValue { get; set; }

        [DataMember]
        public string SubClassification { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public string Sector { get; set; }

        [DataMember]
        public string Custid { get; set; }

        [DataMember]
        public string ForbearanceFlag { get; set; }

        [DataMember]
        public int? MissedPayment { get; set; }

        [DataMember]
        public string InitialCreditRating { get; set; }

        [DataMember]
        public string InternalRating { get; set; }

        [DataMember]
        public int? MissedPayment_Stage { get; set; }

        [DataMember]
        public int? Classification_Stage { get; set; }

        [DataMember]
        public int? Forbearance_Stage { get; set; }

        [DataMember]
        public int? Classification_Stage_Final { get; set; }

        [DataMember]
        public int? PastDueDays { get; set; }

        [DataMember]
        public bool Active { get; set; }


        //ACCESS START

        [DataMember]
        public string CUSTOMER_NAME { get; set; }

        [DataMember]
        public Nullable<double> ForcedSaleValue { get; set; }

        [DataMember]
        public Nullable<double> CostRecovery { get; set; }

        [DataMember]
        public int? CreditStaging { get; set; }

        [DataMember]
        public int? ModelClassification { get; set; }

        [DataMember]
        public int? ClassificationOverride { get; set; }

        public int EntityId
        {
            get
            {
                return LoanDetailId;
            }
        }
    }
}
