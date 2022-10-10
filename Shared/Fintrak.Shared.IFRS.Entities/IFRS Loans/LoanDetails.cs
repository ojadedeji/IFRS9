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
    public partial class LoanDetails : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int LoanDetailId { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }

 
        [DataMember]
        public string ProductCategory { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string ProductType { get; set; }

        [DataMember]
        public string RelatedParty { get; set; }

        [DataMember]
        public string Sector { get; set; }

        [DataMember]
        public DateTime? BookingDate { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public Decimal? ExchangeRate { get; set; }

        [DataMember]
        public Decimal? Amount { get; set; }

        [DataMember]
        public Decimal? RelatedAccountBal { get; set; }

        [DataMember]
        public Decimal? Rate { get; set; }

        [DataMember]
        public DateTime? ValueDate { get; set; }

        [DataMember]
        public Decimal? PrincipalOutstandingBal { get; set; }

        [DataMember]
        public Decimal? PastDueAmount { get; set; }

        [DataMember]
        public Decimal? Interest_Receiv_Pay_UnEarn { get; set; }

        [DataMember]
        public Decimal? InterestInSuspense { get; set; }

        [DataMember]
        public Decimal? PeriodicRepaymentAmount { get; set; }

      
        [DataMember]
        public DateTime? FirstRepaymentDate { get; set; }

        [DataMember]
        public Decimal? PeriodicPrincipalInstallment { get; set; }

        [DataMember]
        public Decimal? PeriodicInterestRepayment { get; set; }

      
        [DataMember]
        public DateTime? MaturityDate { get; set; }

        [DataMember]
        public string MISCode { get; set; }

        [DataMember]
        public Int32? TenorDays { get; set; }

        [DataMember]
        public Int32? InterestRepayFreq { get; set; }

        [DataMember]
        public Int32? PrincipalRepayFreq { get; set; }

        [DataMember]
        public string InterestRepayFreqInWords { get; set; }

        [DataMember]
        public string PrincipalRepayFreqInWords { get; set; }

        [DataMember]
        public Decimal? ProvisionConstituted { get; set; }

        [DataMember]
        public Decimal? CollateralHaircut { get; set; }

        [DataMember]
        public string CollateralType { get; set; }

        [DataMember]
        public double? CollateralValue { get; set; }

        [DataMember]
        public Decimal? CollateralRecoverableAmt { get; set; }

        [DataMember]
        public Decimal? ODLimit { get; set; }

        [DataMember]
        public DateTime? LastCrDate { get; set; }

        [DataMember]
        public DateTime? LastDrDate { get; set; }

        [DataMember]
        public Int32? Credit_Days { get; set; }

        [DataMember]
        public Int32? Debit_Days { get; set; }


        [DataMember]
        public Int32? ActiveDays { get; set; }

        [DataMember]
        public Int32? MonthToMaturity { get; set; }

        [DataMember]
        public int? MonthinThePeriod { get; set; }

        [DataMember]
        public Int32? TenorMonth { get; set; }

        [DataMember]
        public int? Year { get; set; }

        [DataMember]
        public int? Period { get; set; }

        [DataMember]
        public bool? LD { get; set; }


        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public string FinacleClassification { get; set; }

        [DataMember]
        public string SubClassification { get; set; }

        [DataMember]
        public string Segment { get; set; }

        [DataMember]
        public string SubSegment { get; set; }

        public int EntityId
        {
            get
            {
                return LoanDetailId;
            }
        }
    }
}
