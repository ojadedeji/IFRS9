using Fintrak.Shared.Basic.Framework;
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
    public partial class CollateralCategory : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int CollateralCategoryId { get; set; }

        [DataMember]
        [Required]
        public string Code { get; set; }

        [DataMember]
        
        public string Name { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }
      
        public int EntityId
        {
            get
            {
                return CollateralCategoryId;
            }
        }
    }
}
