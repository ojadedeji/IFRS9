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
    public partial class ExtractionRole : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ExtractionRoleId { get; set; }

        [DataMember]
        [Required]
        public int RoleId { get; set; }

       
        [DataMember]
        public int Extractionid { get; set; }

        
        public int EntityId
        {
            get
            {
                return ExtractionRoleId;
            }
        }
    }
}
