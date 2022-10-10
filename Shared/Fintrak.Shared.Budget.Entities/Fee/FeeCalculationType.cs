using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Fintrak.Shared.Budget.Entities
{
    public partial class FeeCalculationType : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int FeeCalculationTypeId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public bool VolumeBase { get; set; }

        public int EntityId
        {
            get
            {
                return FeeCalculationTypeId;
            }
        }
    }
}
