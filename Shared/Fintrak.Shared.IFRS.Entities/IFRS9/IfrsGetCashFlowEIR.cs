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
    public partial class IfrsGetCashFlowEIR : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public string Refno { get; set; }

        [DataMember]
        public DateTime DATE { get; set; }

        [DataMember]
        public int DaysInMonth { get; set; }

        [DataMember]
        public double PrincipalReypayment { get; set; }

        [DataMember]
        public double InterestPayment { get; set; }

        [DataMember]
        public double AmountDue { get; set; }

        [DataMember]
        public int CummulativeDate { get; set; }

        [DataMember]
        public int DaysInYear { get; set; }

        [DataMember]
        public double YearsInDecimal { get; set; }

        [DataMember]
        public double DiscountFactor { get; set; }

        [DataMember]
        public double RevisedCASHFLOW { get; set; }

        [DataMember]
        public double PVCashflow { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
