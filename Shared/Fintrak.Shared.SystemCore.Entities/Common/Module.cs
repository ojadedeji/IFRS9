using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Fintrak.Shared.SystemCore.Entities
{
    public partial class Module : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ModuleId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public string Alias { get; set; }

        [DataMember]
        [Required]
        public int SolutionId { get; set; }

        [DataMember]
        public bool CanUpdate { get; set; }

        public int EntityId
        {
            get
            {
                return ModuleId;
            }
        }
    }
}
