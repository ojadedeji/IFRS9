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
    public partial class DataExport : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int DataExportId { get; set; }

        [DataMember]
        [Required]
        public string Title { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        [Required]
        public int SolutionId { get; set; }

        [DataMember]
        [Required]
        public string Action { get; set; }

        [DataMember]
        [Required]
        public int Position { get; set; }

        [DataMember]
        [Required]
        public string Template { get; set; }

        [DataMember]
       public bool Active { get; set; }
        public int EntityId
        {
            get
            {
                return DataExportId;
            }
        }
    }
}
