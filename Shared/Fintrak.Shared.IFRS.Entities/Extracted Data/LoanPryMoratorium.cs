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
    public partial class LoanPryMoratorium : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int LoanPryMoratoriumId { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }

        [DataMember]
        [Required]
        public string AccountNo { get; set; }

        [DataMember]
        public string ProductCategory { get; set; }

        [DataMember]
        [Required]
        public string ProductCode { get; set; }

        [DataMember]
        [Required]
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
        public DateTime FirstPrinRepaymentdate { get; set; }

        [DataMember]
        public DateTime FirstIntrestRepaymentdate { get; set; }

        [DataMember]
        public double Amount { get; set; }

       

        [DataMember]
        public bool FixedPrincipal { get; set; }

        [DataMember]
        public double FixedPrincipalAmount { get; set; }

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

        //[DataMember]
        //public string Schedule_Type { get; set; }
        

        [DataMember]
        public string CompanyCode { get; set; }


        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return LoanPryMoratoriumId;
            }
        }
    }
}
