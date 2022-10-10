using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Fintrak.Shared.Budget.Entities
{
    public partial class PayStructure : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int PayStructureId { get; set; }

        [DataMember]
        [Required]
        public string GradeCode { get; set; }

        [DataMember]
        [Required]
        public string ClassificationCode { get; set; }

        [DataMember]
        [Required]
        public decimal GrossPay { get; set; }

        [DataMember]
        public decimal ThirtheenMonth { get; set; }

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
                return PayStructureId;
            }
        }
    }
}
