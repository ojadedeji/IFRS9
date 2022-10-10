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
    public partial class CurrencyRate : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int CurrencyRateId { get; set; }

        [DataMember]
        [Required]
        public string CurrencyCode { get; set; }

        [DataMember]
        [Required]
        public RateTypeEnum RateType { get; set; }

        [DataMember]
        public double Month1 { get; set; }

        [DataMember]
        public double Month2 { get; set; }

        [DataMember]
        public double Month3 { get; set; }

        [DataMember]
        public double Month4 { get; set; }

        [DataMember]
        public double Month5 { get; set; }

        [DataMember]
        public double Month6 { get; set; }

        [DataMember]
        public double Month7 { get; set; }

        [DataMember]
        public double Month8 { get; set; }

        [DataMember]
        public double Month9 { get; set; }

        [DataMember]
        public double Month10 { get; set; }

        [DataMember]
        public double Month11 { get; set; }

        [DataMember]
        public double Month12 { get; set; }

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
                return CurrencyRateId;
            }
        }
    }
}
