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
    public partial class RateType : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int RateTypeId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        

      
        public int EntityId
        {
            get
            {
                return RateTypeId;
            }
        }
    }
}
