using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Fintrak.Shared.Budget.Entities
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
        [Required]
        public string CurrencyCode { get; set; }

        [DataMember]
        [Required]
        public string CategoryCode { get; set; }

        [DataMember]
        [Required]
        public string GroupCode { get; set; }

        [DataMember]
        [Required]
        public string CaptionCode { get; set; }

        [DataMember]
        [Required]
        public ProductClassEnum ProductClass { get; set; }

        [DataMember]
        [Required]
        public string ClassificationCode { get; set; }

        [DataMember]
        public string OtherCode { get; set; }

        [DataMember]
        [Required]
        public int Position { get; set; }

        [DataMember]
        public bool Budgetable { get; set; }

        [DataMember]
        public bool VolumeBase { get; set; }

        [DataMember]
        public bool IsPoolRelated { get; set; }

        [DataMember]
        public bool Visibility { get; set; }

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
                return ProductId;
            }
        }
    }
}
