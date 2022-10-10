using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Fintrak.Shared.Budget.Entities
{
    public partial class TeamDefinition : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int TeamDefinitionId { get; set; }

        [DataMember]
        [Required]
        public string Code { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        [Required]
        public int Position { get; set; }

        [DataMember]
        public bool CanClassify { get; set; }

        [DataMember]
        public bool IsOfficer { get; set; }

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
                return TeamDefinitionId;
            }
        }
    }
}
