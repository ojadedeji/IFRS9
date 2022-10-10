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
    public partial class IfrsStaffBenefitsLoansReport : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public string RefNo { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public string StaffName { get; set; }

        [DataMember]
        public string LoanType { get; set; }

        [DataMember]
        public string producttype { get; set; }

        [DataMember]
        public double? LoanAmount { get; set; }

        [DataMember]
        public double? OutstandingPrincBal { get; set; }

        [DataMember]
        public decimal? InterestRate { get; set; }

        [DataMember]
        public DateTime? Startdate { get; set; }

        [DataMember]
        public DateTime? MaturityDate { get; set; }

        [DataMember]
        public decimal? MarketRate { get; set; }

        [DataMember]
        public double? FairValue { get; set; }

        [DataMember]
        public double? EmployeeBenefit { get; set; }

        [DataMember]
        public double? EmployeeBenefitBalance { get; set; }

        [DataMember]
        public double? IFRSAdjustedStaffLoanBalances { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
