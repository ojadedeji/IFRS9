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
using System.Xml.Serialization;

namespace Fintrak.Shared.Basic.Entities
{
    public partial class GLType : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int GLTypeId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        
        public string Description { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }
      
        public int EntityId
        {
            get
            {
                return GLTypeId;
            }
        }
    }
}
