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
    public partial class ReportStatus : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int StatusId { get; set; }

        [DataMember]
        [Required]
        public int SolutionId { get; set; }
        
        [DataMember]
        [Required]
        public bool Closed { get; set; }
      
        public int EntityId
        {
            get
            {
                return StatusId;
            }
        }
    }
}
