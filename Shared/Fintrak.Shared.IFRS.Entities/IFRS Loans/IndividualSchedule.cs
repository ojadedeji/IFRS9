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
    public partial class IndividualSchedule : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public decimal IRR { get; set; }

        [DataMember]
        public double AmountPrinEnd { get; set; }

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public DateTime ValueDate { get; set; }

        [DataMember]
        public DateTime FirstRepaymentdate { get; set; }

        [DataMember]
        public double PastDueAmount { get; set; }

        [DataMember]
        [Required]
        public DateTime MaturityDate { get; set; }

        [DataMember]
        [Required]
        public DateTime RunDate { get; set; }

        [DataMember]
        public bool Processed { get; set; }
      
        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}
