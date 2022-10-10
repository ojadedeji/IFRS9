using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Core.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.Basic.Entities
{
    public partial class OpexBasisMapping : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int OpexBasisMappingId { get; set; }

        [DataMember]
        [Required]
        public string GLCode { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        [Required]
        public string Caption { get; set; }

        [DataMember]
        public string LineCaption { get; set; }

        [DataMember]
        public bool Active { get; set; }
        
        public int EntityId
        {
            get
            {
                return OpexBasisMappingId;
            }
        }

    }
}
