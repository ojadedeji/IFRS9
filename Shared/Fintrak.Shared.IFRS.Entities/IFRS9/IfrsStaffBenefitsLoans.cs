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
    public partial class IfrsStaffBenefitsLoans : EntityBase, IIdentifiableEntity
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
        public int? TenorInMonths { get; set; }

        [DataMember]
        public int? PastPeriodInMonthsPreviousYearEnd { get; set; }

        [DataMember]
        public int? PastPeriodInMonthsPreviousYearEndPlusOneMonth { get; set; }

        [DataMember]
        public int? OutstandingPeriodInMonths { get; set; }

        [DataMember]
        public int? NoOfDays { get; set; }

        [DataMember]
        public int? NoOfMonths { get; set; }

        [DataMember]
        public double? ContractrualCashflow { get; set; }

        [DataMember]
        public double? FairValue { get; set; }

        [DataMember]
        public double? EmployeeBenefit { get; set; }

        [DataMember]
        public double? MonthlyAmortization { get; set; }

        [DataMember]
        public double? PastPeriodRepayments { get; set; }

        [DataMember]
        public double? PastPeriodAmortization { get; set; }

        [DataMember]
        public double? AmortisedPrepaidBenefit { get; set; }

        [DataMember]
        public double? EmployeeBenefitBalance { get; set; }

        [DataMember]
        public double? MarketRateInterestIncome { get; set; }

        [DataMember]
        public double? OffMarketRateInterestIncome { get; set; }

        [DataMember]
        public double? InterestDifferential { get; set; }

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
