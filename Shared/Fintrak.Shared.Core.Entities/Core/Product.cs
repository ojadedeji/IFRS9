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

namespace Fintrak.Shared.Core.Entities
{
    public partial class Product : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ProductId { get; set; }

        [DataMember]
        [Required]
        public string Code { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public string AssetGL { get; set; }

        [DataMember]
        public string LiabilityGL { get; set; }

        [DataMember]
        public string IncomeGL { get; set; }

        [DataMember]
        public string ExpenseGL { get; set; }

        [DataMember]
        public bool IsSwitch { get; set; }

        public int EntityId
        {
            get
            {
                return ProductId;
            }
        }
    }
}
