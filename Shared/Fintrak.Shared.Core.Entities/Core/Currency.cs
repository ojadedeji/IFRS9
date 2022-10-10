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
    public partial class Currency : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int CurrencyId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        [Required]
        public string Symbol { get; set; }

        [DataMember]
        public double Rate { get; set; }

        [DataMember]
        [Required]
        public bool IsBase { get; set; }

        

      
        public int EntityId
        {
            get
            {
                return CurrencyId;
            }
        }
    }
}
