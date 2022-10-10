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
    public partial class IfrsMonthlyEAD : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int Id { get; set; }


        [DataMember]
        [Required]
        public string RefNo { get; set; }

        [DataMember]
        [Required]
        public string AccountNo { get; set; }

        
        [DataMember]
        [Required]
        public DateTime date_pmt { get; set; }

        [DataMember]
        [Required]
        public double AmortizedCost { get; set; }

        [DataMember]
        [Required]
        public double EAD { get; set; }

        [DataMember]
        [Required]
        public string Producttype { get; set; }

        [DataMember]
        [Required]
        public string SubType { get; set; }

        [DataMember]
        [Required]
        public int OriginYear { get; set; }

        [DataMember]
        [Required]
        public double InterestAccrued { get; set; }

        [DataMember]
        [Required]
        public string currency { get; set; }
        
        [DataMember]
        [Required]
        public double EIR { get; set; }

        [DataMember]
        [Required]
        public DateTime RunDate { get; set; }

        [DataMember]
        [Required]
        public int TenorInDays { get; set; }
        [DataMember]
        [Required]
        public int Stage { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}
