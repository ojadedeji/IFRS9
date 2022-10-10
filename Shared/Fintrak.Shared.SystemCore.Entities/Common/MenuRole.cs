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
    public partial class MenuRole : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int MenuRoleId { get; set; }

        [DataMember]
        [Required]
        public int MenuId { get; set; }

        [DataMember]
        [Required]
        public int RoleId { get; set; }

        public int EntityId
        {
            get
            {
                return MenuRoleId;
            }
        }
    }
}
