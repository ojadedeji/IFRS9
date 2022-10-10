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
    public partial class CashFlowRestructure: EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int  ID { get; set; }

        [DataMember]
        [Required]
        public double OpeningBalance { get; set; }

        [DataMember]
        [Required]
        public double amt_int_pay { get; set; }

        [DataMember]
        [Required]
        public double amt_prin_pay { get; set; }

        [DataMember]
        [Required]
        public double CummulativeInterest { get; set; }

        [DataMember]
        [Required]
        public double ClosingBalance { get; set; }        

        [DataMember]
        [Required]
        public string Refno { get; set; }

        [DataMember]
        [Required]
        public DateTime date_pmt { get; set; }

        [DataMember]
        [Required]
        public double amt_pmt { get; set; }


       [DataMember]
       [Required]
       public DateTime Rundate { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
