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
    public partial class IfrsBenefitsStaffLoan : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public string Refno { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public DateTime Startdate { get; set; }

        [DataMember]
        public string ProductCategory { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public double PRINCIPALREPAYMENT { get; set; }

        [DataMember]
        public double LoanAmount { get; set; }

        [DataMember]
        public double Rate { get; set; }

        [DataMember]
        public int Tenor { get; set; }

        [DataMember]
        public DateTime MaturityDate { get; set; }

        [DataMember]
        public double TOTALOUTSTANDINGEXPOSURE { get; set; }

        [DataMember]
        public DateTime RUNDATE { get; set; }

        [DataMember]
        public double INTERESTRECEIVABLES { get; set; }

        [DataMember]
        public string CURRENCY { get; set; }

        [DataMember]
        public int EXCHANGERATE { get; set; }

        [DataMember]
        public string CUSTID { get; set; }

        [DataMember]
        public double CONTRACTUALCASHFLOW { get; set; }

        [DataMember]
        public double PRIMELENDINGRATE { get; set; }

        [DataMember]
        public double USINGCONTRACTRATE { get; set; }

        [DataMember]
        public string SIGNIFICANTTHRESHOLD { get; set; }

        [DataMember]
        public int PASTPERIOD { get; set; }

        [DataMember]
        public int OSPERIOD { get; set; }

        [DataMember]
        public double StaffLoansBenefitFairValueLoss { get; set; }

        [DataMember]
        public double FAIRVALUE { get; set; }

        [DataMember]
        public int DaysToMaturity { get; set; }

        [DataMember]
        public double BenefitPerDaytoMaturity { get; set; }

        [DataMember]
        public double PRINCIPALOUTSTANDINGBAL { get; set; }

        [DataMember]
        public int FacilityTenor { get; set; }

        [DataMember]
        public double PL_impact { get; set; }

        [DataMember]
        public double UNAMORTIZED { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
