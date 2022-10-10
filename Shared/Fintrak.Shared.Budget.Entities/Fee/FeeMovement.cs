using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Fintrak.Shared.Budget.Entities
{
    public partial class FeeMovement : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int FeeMovementId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        public int EntityId
        {
            get
            {
                return FeeMovementId;
            }
        }
    }
}
