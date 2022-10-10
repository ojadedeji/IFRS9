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
    public partial class ComputedForcastedPDLGDForeign : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ComputedPDId { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        [Required]
        public string Sector_Code { get; set; }

        [DataMember]
        [Required]
        public int Type { get; set; }

        [DataMember]
        [Required]
        public double? PD_LGD { get; set; }

        [DataMember]
        [Required]
        public double? AdversePD { get; set; }

        [DataMember]
        [Required]
        public double? OptimisticPD { get; set; }

        [DataMember]
        [Required]
        public double? PD { get; set; }

        [Required]
        public DateTime Rundate { get; set; } 

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return ComputedPDId;
            }
        }
    }
}
