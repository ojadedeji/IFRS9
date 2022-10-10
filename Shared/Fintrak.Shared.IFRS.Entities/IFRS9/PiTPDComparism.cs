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
    public partial class PiTPDComparism : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ComparismPDId { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        [Required]
        public string Grouping { get; set; }

        [DataMember]
        [Required]
        public string Description { get; set; }

        [DataMember]
        [Required]
        public double? BaseLinePD { get; set; }

        [DataMember]
        [Required]
        public double? StressedPiTPD { get; set; }
       
        [DataMember]
        [Required]
        public double? Movement { get; set; }
        
        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        [Required]
        public string Type { get; set; }
        public int EntityId
        {
            get
            {
                return ComparismPDId;
            }
        }
    }
}
