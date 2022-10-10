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
    public partial class BSINOtherInformation : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int BSINOtherInformationId { get; set; }

        [DataMember]
        [Required]
        public string Segment { get; set; }

        [DataMember]
        [Required]
        public string OtherCaption { get; set; }

        [DataMember]
        [Required]
        public string MainCaption { get; set; }

        [DataMember]
        public string SubCaption { get; set; }

        [DataMember]
        [Required]
        public string Currency { get; set; }

        [DataMember]
        [Required]
        public bool BSIN { get; set; }


        public int EntityId
        {
            get
            {
                return BSINOtherInformationId;
            }
        }

    }
}
