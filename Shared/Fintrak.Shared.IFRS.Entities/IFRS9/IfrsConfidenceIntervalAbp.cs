using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Fintrak.Shared.IFRS.Entities
{
    public partial class IfrsConfidenceIntervalAbp : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int IntervalId { get; set; }

        [DataMember]
        public double Ci_level { get; set; }

        [DataMember]
        public double Z_score { get; set; }

        public int EntityId
        {
            get
            {
                return IntervalId;
            }
        }

    }
}
