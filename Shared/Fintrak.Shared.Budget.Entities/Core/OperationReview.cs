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
    public partial class OperationReview : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int OperationReviewId { get; set; }

        [DataMember]
        [Required]
        public string Code { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        [Required]
        public string OperationCode { get; set; }

        [DataMember]
        public OperationStatusEnum Status { get; set; }

        public int EntityId
        {
            get
            {
                return OperationReviewId;
            }
        }
    }
}
