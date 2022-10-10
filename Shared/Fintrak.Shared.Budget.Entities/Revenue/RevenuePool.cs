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
    public partial class RevenuePool : TransactionEntityBase<decimal>, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int RevenuePoolId { get; set; }

        [DataMember]
        [Required]
        public string DefintionCode { get; set; }

        [DataMember]
        [Required]
        public string MisCode { get; set; }

        [DataMember]
        [Required]
        public string ProductCode { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        [Required]
        public string CurrencyCode { get; set; }

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
                return RevenuePoolId;
            }
        }
    }
}
