using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Core.Framework;
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
    public partial class OpexBusinessRule : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int OpexBusinessRuleId { get; set; }

        [DataMember]
        [Required]
        public string Source { get; set; }

        [DataMember]
        public string BasisCaption { get; set; }
        [DataMember]
        [Required]
        public double Ratio { get; set; }

        [DataMember]
        [Required]
        public string Template { get; set; }

        [DataMember]
        [Required]
        public string Target { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        [Required]
        public int Position { get; set; }

        [DataMember]
        [Required]
        public string Total { get; set; }

        [DataMember]
        [Required]
        public string Type { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        public int EntityId
        {
            get
            {
                return OpexBusinessRuleId;
            }
        }

    }
}
