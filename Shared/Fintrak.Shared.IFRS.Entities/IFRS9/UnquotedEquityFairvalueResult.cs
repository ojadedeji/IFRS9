using Fintrak.Shared.IFRS.Framework;
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

namespace Fintrak.Shared.IFRS.Entities
{
    public partial class UnquotedEquityFairvalueResult : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int UnquotedEquityFairvalueResultId { get; set; }

        [DataMember]
        [Required]
        public string sector { get; set; }
        [DataMember]
     
        [Required]
        public string stock { get; set; }

        [DataMember]
        [Required]
        public double P_E { get; set; }

        [DataMember]
        [Required]
        public double P_CF { get; set; }

        [DataMember]
        public double P_BV { get; set; }

        [DataMember]
        public double P_S { get; set; }

        [Required]
        public double Estimated_Stock_value_PE { get; set; }

        [DataMember]
        public double Estimated_Stock_value_PCF { get; set; }

        [DataMember]
        public double Estimated_Stock_value_PBV { get; set; }

        [DataMember]
        public double Estimated_Stock_value_PS { get; set; }

        [DataMember]
        public double Estimated_Mean_Stock_value { get; set; }
        [DataMember]
        public double Standard_deviation { get; set; }

        [DataMember]
        public double Discount_loss { get; set; }

         [DataMember]
        public double fairvalue { get; set; }

        [DataMember]
        public bool Active { get; set; }
        public int EntityId
        {
            get
            {
                return UnquotedEquityFairvalueResultId;
            }
        }
    }
}
