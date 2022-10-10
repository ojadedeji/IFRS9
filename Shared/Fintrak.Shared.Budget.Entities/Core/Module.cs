using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Fintrak.Shared.Budget.Entities
{
    public partial class Module : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ModuleId { get; set; }

        [DataMember]
        [Required]
        public string Code { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        public int EntityId
        {
            get
            {
                return ModuleId;
            }
        }
    }
}
