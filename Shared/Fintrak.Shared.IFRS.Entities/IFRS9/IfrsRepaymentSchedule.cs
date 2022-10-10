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
    public partial class IfrsRepaymentSchedule : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public string Refno { get; set; }

        [DataMember]
        public int num_pmt { get; set; }

        [DataMember]
        public DateTime PaymentDate { get; set; }

        [DataMember]
        public double BeginingBalance { get; set; }

        [DataMember]
        public double GrossInterest { get; set; }

        [DataMember]
        public double NetInterest { get; set; }

        [DataMember]
        public double Principal { get; set; }

        [DataMember]
        public double InterestPrincipal { get; set; }

        [DataMember]
        public double ResidualValue { get; set; }

        [DataMember]
        public double TotalBiAnnualPayment { get; set; }

        [DataMember]
        public double EndingBalance { get; set; }

        [DataMember]
        public double CummulativeInterest { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
