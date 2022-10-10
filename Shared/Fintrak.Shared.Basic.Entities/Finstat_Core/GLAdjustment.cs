using Fintrak.Shared.Basic.Framework;
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
using System.Xml.Serialization;

namespace Fintrak.Shared.Basic.Entities
{
    public partial class GLAdjustment : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int GLAdjustmentId { get; set; }

        [DataMember]
        public string AdjustmentCode { get; set; }

        [DataMember]
        [Required]
        public string GLCode { get; set; }

        [DataMember]
        public string Narration { get; set; }

        [DataMember]
        [Required]
        public Indicator Indicator { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        [Required]
        public string CompanyCode { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public DateTime RunDate { get; set; }

        [DataMember]
        public bool Posted { get; set; }

        [DataMember]
        public string  GroupCode { get; set; }

        [DataMember]
        public AdjustmentType AdjustmentType { get; set; }

        public int EntityId
        {
            get
            {
                return GLAdjustmentId;
            }
        }
    }
}
