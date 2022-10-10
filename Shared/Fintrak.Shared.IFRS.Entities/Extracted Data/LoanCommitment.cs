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
    public partial class LoanCommitment : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int LoanCommitmentId { get; set; }

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
        public double Amount { get; set; }

        [DataMember]
        public decimal ExchangeRate { get; set; }

        [DataMember]
        public decimal Rate { get; set; }

        [DataMember]
        public int? TenorDays { get; set; }

        [DataMember]
        public int? TenorMonth { get; set; }

        [DataMember]
        public string MISCode { get; set; }

        [DataMember]
        public Nullable<double> CollateralHaircut { get; set; }

        [DataMember]
        public Nullable<double> CollateralRecoverableAmt { get; set; }

        [DataMember]
        public Nullable<double> ODLimit { get; set; }

        [DataMember]
        public int? Period { get; set; }

        [DataMember]
        public int? Year { get; set; }

        [DataMember]
        public int? InterestRepayFreq { get; set; }

        [DataMember]
        public int? PrincipalRepayFreq { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public Nullable<double> PrincipalOutstandingBal { get; set; }

        [DataMember]
        public Nullable<double> PastDueAmount { get; set; }

        [DataMember]
        public string Rating { get; set; }

        [DataMember]
        public string Classification { get; set; }

        [DataMember]
        public string SubClassification { get; set; }

        [DataMember]
        public string CollateralType { get; set; }

        [DataMember]
        public Nullable<double> CollateralValue { get; set; }

        //[DataMember]
        //public string ForbearanceFlag { get; set; }

        [DataMember]
        public int? Stage { get; set; }

        [DataMember]
        public DateTime RunDate { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return LoanCommitmentId;
            }
        }
    }
}
