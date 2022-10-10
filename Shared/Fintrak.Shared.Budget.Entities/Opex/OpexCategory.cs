using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Fintrak.Shared.Budget.Entities
{
    public partial class OpexCategory : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int OpexCategoryId { get; set; }

        [DataMember]
        [Required]
        public string Code { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public string ParentCode { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        [Required]
        public int Position { get; set; }

        [DataMember]
        [Required]
        public string ReviewCode { get; set; }

        [DataMember]
        [Required]
        public string Year { get; set; }

        public int EntityId
        {
            get
            {
                return OpexCategoryId;
            }
        }
    }
}
