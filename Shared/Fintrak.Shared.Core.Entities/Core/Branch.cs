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
    public partial class Branch : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int BranchId { get; set; }

        [DataMember]
        [Required]
        public string Code { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        [Required]
        public int CompanyId { get; set; }

        [DataMember]
        public string EMail { get; set; }

        [DataMember]
        public string Contact { get; set; }

        [DataMember]
        public string Phone { get; set; }

        public int EntityId
        {
            get
            {
                return BranchId;
            }
        }
    }
}
