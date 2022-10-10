using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Core.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.Scorecard.Entities
{
    public partial class SCDTeamMap : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int TeamMapId { get; set; }

        [DataMember]
        [Required]
        public OfficeType Centre { get; set; }


        [DataMember]
        [Required]
        public string TeamDefinitionCode { get; set; }

        [DataMember]
        [Required]
        public string MISCode { get; set; }

        [DataMember]
        [Required]
        public string MISName { get; set; }

        [DataMember]
        public string StaffCode { get; set; }

        [DataMember]
        public string Grade { get; set; }

        [DataMember]
        public string ParentCode { get; set; }

        [DataMember]
        [Required]
        public int Period { get; set; }

        [DataMember]
        [Required]
        public string Year { get; set; }

        [DataMember]     
        public string TeamClassificationCode { get; set; }
      
        public int EntityId
        {
            get
            {
                return TeamMapId;
            }
        }

    }
}
