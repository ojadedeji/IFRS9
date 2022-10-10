using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.MPR.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.MPR.Entities
{
    public partial class Ratios : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int RatiosId { get; set; }

        [DataMember]
        [Required]
        public string MainCaption { get; set; }

        [DataMember]
        [Required]
        public string Numerator { get; set; }

        [DataMember]
        [Required]
        public string Denominator { get; set; }

        [DataMember]
        [Required]
        public bool ProRatio { get; set; }

        [DataMember]
        [Required]
        public bool Bsin { get; set; }

        public int EntityId
        {
            get
            {
                return RatiosId;
            }
        }
    }
}
