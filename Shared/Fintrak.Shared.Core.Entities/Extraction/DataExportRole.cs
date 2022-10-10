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

namespace Fintrak.Shared.Core.Entities
{
    public partial class DataExportRole : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int DataExportRoleId { get; set; }

        [DataMember]
        [Required]
        public int RoleId { get; set; }

       
        [DataMember]
        [Required]
        public int DataExportId { get; set; }

        
        public int EntityId
        {
            get
            {
                return DataExportRoleId;
            }
        }
    }
}
