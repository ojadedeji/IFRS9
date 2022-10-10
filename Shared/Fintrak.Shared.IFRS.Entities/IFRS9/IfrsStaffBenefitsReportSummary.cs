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
    public partial class IfrsStaffBenefitsReportSummary : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public string GLCODE { get; set; }

        [DataMember]
        public string GLAccountName { get; set; }

        [DataMember]
        public double UnaditedPerGL { get; set; }

        [DataMember]
        public double PerListing { get; set; }

        [DataMember]
        public double AuditAdjustment { get; set; }

        [DataMember]
        public double Amortizedcost { get; set; }

        [DataMember]
        public double AmortisedCostPerComputationSchedule { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
