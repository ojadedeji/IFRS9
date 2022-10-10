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
    public partial class DepositRepaymentSchedule : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int DepositRepayId { get; set; }

        [DataMember]
        public double? INT_DUE { get; set; }
        [DataMember]
        public double? AmountDiff { get; set; }
        [DataMember]
        public double? INT_PAID { get; set; }

        [DataMember]
        public DateTime DUEDT { get; set; }

        [DataMember]
        public double? PRINCIPAL_PAID { get; set; }

        [DataMember]
        public double? PRINCIPAL_AMOUNT_DUE { get; set; }

        [DataMember]
        public string REFNO { get; set; }
       
        [DataMember]
        public DateTime RUNDATE { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return DepositRepayId;
            }
        }
    }
}
