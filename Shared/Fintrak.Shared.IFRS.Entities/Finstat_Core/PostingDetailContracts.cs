using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.IFRS.Framework;
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
    public partial class PostingDetailContracts : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public string Refno { get; set; }

        [DataMember]
        [Required]
        public DateTime date_pmt { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        [Required]
        public double PrincipalOustandingBal { get; set; }

        [DataMember]
        [Required]
        public double AmortizedCost { get; set; }

        [DataMember]
        public string GLCode { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
