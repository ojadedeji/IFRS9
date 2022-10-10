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
    public partial class BondSummary : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int BondId { get; set; }

        [DataMember]
        [Required]
        public string Refno { get; set; }

        [DataMember]
        [Required]
        public DateTime Valuedate { get; set; }

        [DataMember]
        public double Facevalue { get; set; }

        [DataMember]
        public double QtyTraded { get; set; }

        [DataMember]
        public DateTime MaturityDate { get; set; }

        [DataMember]
        public double Consideration { get; set; }

        [DataMember]
        public DateTime Rundate { get; set; }
      
        public int EntityId
        {
            get
            {
                return BondId;
            }
        }
    }
}
