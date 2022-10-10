using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.SystemCore.Framework;
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
    public partial class Role : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int RoleId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public RoleType Type { get; set; }

        [DataMember]
        [Required]
        public int SolutionId { get; set; }

        public int EntityId
        {
            get
            {
                return RoleId;
            }
        }
    }
}
