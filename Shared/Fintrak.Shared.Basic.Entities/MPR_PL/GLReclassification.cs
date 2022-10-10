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
    public partial class GLReclassification : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int GLReclassificationId { get; set; }

        [DataMember]
        [Required]
        public string GLAccount { get; set; }

        [DataMember]
        [Required]
        public string CaptionCode { get; set; }

        [DataMember]

        public string CompanyCode { get; set; }        
      
        public int EntityId
        {
            get
            {
                return GLReclassificationId;
            }
        }

    }
}
