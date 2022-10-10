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
    public partial class ChangesInEquity : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ChangesInEquityId { get; set; } 


        [DataMember]
        [Required]
        public DateTime Rundate { get; set; }

        [DataMember]
        [Required]
        public string Caption { get; set; }

        [DataMember]
        [Required]
        public double ShareCapital { get; set; }

        [DataMember]
        [Required]
        public double SharePremium { get; set; }

        [DataMember]
        public double PropertyRevaluationReserve { get; set; }

        [DataMember]
        public double TranslationReserve { get; set; }

        [DataMember]
        public double FairValueReserve { get; set; }

        [DataMember]
        public double RegulatoryRiskReserve { get; set; }

        [DataMember]
        public double OtherRegulatoryReserves { get; set; }

        [DataMember]
        public double CapitalReserve { get; set; }

        [DataMember]
        public double ShareBonusReserve { get; set; }

        [DataMember]
        public double OtherReserves { get; set; }

        [DataMember]
        public double RetainedEarnings { get; set; }

        [DataMember]
        public double NonControllingInterest { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        public int EntityId
        {
            get
            {
                return ChangesInEquityId;
            }
        }
    }
}
