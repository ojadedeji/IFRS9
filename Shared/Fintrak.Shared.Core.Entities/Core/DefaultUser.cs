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

namespace Fintrak.Shared.Core.Entities
{
    public partial class DefaultUser : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int DefaultUserId { get; set; }

        [DataMember]
        [Required]
        public string LoginID { get; set; }

        [DataMember]
        [Required]
        public int SolutionId { get; set; }
      
        public int EntityId
        {
            get
            {
                return DefaultUserId;
            }
        }
    }
}
