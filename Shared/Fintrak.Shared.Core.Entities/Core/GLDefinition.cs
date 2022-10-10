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
using System.Xml.Serialization;

namespace Fintrak.Shared.Core.Entities
{
    public partial class GLDefinition : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int GLDefinitionId { get; set; }

        [DataMember]
        [Required]
        public string GL_Code { get; set; }

        [DataMember]
        [Required]
        public string Description { get; set; }

        
        public int EntityId
        {
            get
            {
                return GLDefinitionId;
            }
        }
    }
}
