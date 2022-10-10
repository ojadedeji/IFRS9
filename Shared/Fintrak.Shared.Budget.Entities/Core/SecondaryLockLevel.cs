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
    public partial class SecondaryLockLevel : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int SecondaryLockLevelId { get; set; }

        [DataMember]
        [Required]
        public string ModuleCode { get; set; }

        [DataMember]
        [Required]
        public string DefinitionCode { get; set; }

        public int EntityId
        {
            get
            {
                return SecondaryLockLevelId;
            }
        }
    }
}
