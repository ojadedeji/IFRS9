using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Fintrak.Shared.Budget.Entities
{
    public partial class Team : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int TeamId { get; set; }

        [DataMember]
        [Required]
        public string Code { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public string ParentCode { get; set; }

        [DataMember]
        [Required]
        public string DefinitionCode { get; set; }

        [DataMember]
        [Required]
        public string ClassificationCode { get; set; }

        [DataMember]
        [Required]
        public string ReviewCode { get; set; }

        [DataMember]
        public bool IsDefaultOfficer { get; set; }

        [DataMember]
        [Required]
        public string Year { get; set; }

        public int EntityId
        {
            get
            {
                return TeamId;
            }
        }
    }
}
