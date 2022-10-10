using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Fintrak.Shared.MPR.Entities
{
    public partial class Staffs : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int StaffId { get; set; }

        [DataMember]
        [Required]
        public string StaffCode { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Phone { get; set; }

       
        public int EntityId
        {
            get
            {
                return StaffId;
            }
        }
    }
}
