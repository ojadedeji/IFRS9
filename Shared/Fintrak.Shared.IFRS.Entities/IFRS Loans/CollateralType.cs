using Fintrak.Shared.IFRS.Framework;
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

namespace Fintrak.Shared.IFRS.Entities
{
    public partial class CollateralType : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int CollateralTypeId { get; set; }

        [DataMember]
        [Required]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string CategoryCode { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }
      
        public int EntityId
        {
            get
            {
                return CollateralTypeId;
            }
        }
    }
}
