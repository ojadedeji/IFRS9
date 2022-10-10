using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Fintrak.Shared.Budget.Entities
{
    public partial class Currency : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int CurrencyId { get; set; }

        [DataMember]
        [Required]
        public string Code { get; set; }

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
