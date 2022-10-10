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
    public partial class AutoPostingTemplate : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int AutoPostingTemplateId { get; set; }

        [DataMember]
        [Required]
        public string Title { get; set; }

        [DataMember]
        [Required]
        public string Action { get; set; }

        [DataMember]

        public string CompanyCode { get; set; }

        [DataMember]
       
        public bool Active { get; set; }

        
      
        public int EntityId
        {
            get
            {
                return AutoPostingTemplateId;
            }
        }
    }
}
