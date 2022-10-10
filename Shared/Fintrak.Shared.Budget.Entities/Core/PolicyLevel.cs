using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Fintrak.Shared.Budget.Entities
{
    public partial class PolicyLevel : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int PolicyLevelId { get; set; }

        [DataMember]
        [Required]
        public string PolicyCode { get; set; }

        [DataMember]
        [Required]
        public string ModuleCode { get; set; }

        [DataMember]
        [Required]
        public string DefinitionCode { get; set; }

        [DataMember]
        [Required]
        public CenterTypeEnum Center { get; set; }

        [DataMember]
        [Required]
        public string ReviewCode { get; set; }

        [DataMember]
        [Required]
        public string Year { get; set; }

        public int EntityId
        {
            get
            {
                return PolicyLevelId;
            }
        }
    }
}
