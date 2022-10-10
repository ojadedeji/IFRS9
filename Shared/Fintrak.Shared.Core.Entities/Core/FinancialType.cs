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
    public partial class FinancialType : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int FinancialTypeId { get; set; }

        [DataMember]
        [Required]
        public string Code { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public int? ParentId { get; set; }
        

        public int EntityId
        {
            get
            {
                return FinancialTypeId;
            }
        }
    }
}
