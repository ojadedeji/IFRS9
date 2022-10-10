using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Core.Framework;
using Fintrak.Shared.Scorecard.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.Scorecard.Entities
{
    public partial class SCDKPI : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int KPIId { get; set; }

        [DataMember]
        [Required]
        public string Code { get; set; }


        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        [Required]
        public string Description { get; set; }


        [DataMember]
        [Required]
        public PeriodType PeriodType { get; set; }

        [DataMember]
        [Required]
        public KPIDirection Direction { get; set; }

        [DataMember]
        //[Required]
        public string CategoryCode { get; set; }

        [DataMember]
        [Required]
        public bool IsKPICalculated { get; set; }

        [DataMember]
        //[Required]
        public string Formula { get; set; }

        [DataMember]
        [Required]
        public AggregateMethods AggregateMethod { get; set; }

        [DataMember]
        [Required]
        public bool IsTargetCalculated { get; set; }

        [DataMember]
        public string ScoreFormula { get; set; }

      
        public int EntityId
        {
            get
            {
                return KPIId;
            }
        }

    }
}
