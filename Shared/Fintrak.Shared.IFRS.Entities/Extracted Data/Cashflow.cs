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
    public partial class Cashflow : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int CashflowId { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }

        [DataMember]
        public DateTime datepmt { get; set; }

        [DataMember]
        public double? amt_prin_pay { get; set; }

        [DataMember]
        public double? amt_int_pay { get; set; }

        [DataMember]
        public double? amt_fee_pay { get; set; }

        [DataMember]
        public bool Active { get; set; }


        public int EntityId {
            get {
                return CashflowId;
            }
        }

    }
}
