using Fintrak.Shared.Basic.Framework;
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

namespace Fintrak.Shared.Basic.Entities
{
    public partial class LoanPrimaryData : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int Id { get; set; }

   
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
        public string Sector { get; set; }

        [DataMember]
        public DateTime BookingDate { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public decimal ExchangeRate { get; set; }

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public decimal Rate { get; set; }

        [DataMember]
        public DateTime ValueDate { get; set; }

        [DataMember]
        public double PeriodicRepaymentAmount { get; set; }

        [DataMember]
        public DateTime FirstRepaymentDate { get; set; }

        [DataMember]
        public DateTime MaturityDate { get; set; }

        [DataMember]
        public Int32 Tenor { get; set; }

        [DataMember]
        public Int32 InterestRepayFreq { get; set; }

        [DataMember]
        public Int32 PrincipalRepayFreq { get; set; }

        [DataMember]
        public Int32 TenorMonth { get; set; }

        [DataMember]
        public bool LD { get; set; }

        [DataMember]
        public Int32 Schedule_Type { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

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
