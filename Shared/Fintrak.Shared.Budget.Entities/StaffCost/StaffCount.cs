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
    public partial class StaffCount : TransactionEntityBase<int>, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int StaffCountId { get; set; }

        [DataMember]
        [Required]
        public string DefintionCode { get; set; }

        [DataMember]
        [Required]
        public string MisCode { get; set; }

        [DataMember]
        [Required]
        public string GradeCode { get; set; }

        [DataMember]
        [Required]
        public string ClassificationCode { get; set; }

        [DataMember]
        [Required]
        public string CurrencyCode { get; set; }

        [DataMember]
        [Required]
        public TransactionTypeEnum TransactionType { get; set; }

        [DataMember]
        [Required]
        public CenterTypeEnum CenterType { get; set; }

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
                return StaffCountId;
            }
        }
    }
}
