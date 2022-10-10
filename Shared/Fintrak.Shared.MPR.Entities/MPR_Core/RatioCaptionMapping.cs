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
    public partial class RatioCaptionMapping : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int RatioCaptionMappingId { get; set; }

        [DataMember]
        [Required]
        public string RatioCaption { get; set; }

        [DataMember]
        [Required]
        public string ReportType { get; set; }


        [DataMember]
        [Required]
        public string ReportCaption { get; set; }


        public int EntityId
        {
            get
            {
                return RatioCaptionMappingId;
            }
        }
    }
}
