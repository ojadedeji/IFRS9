using System;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;

namespace Fintrak.Data.IFRS.Contracts
{
    public class IndividualImpairmentInfo
    {
        public IndividualImpairment IndividualImpairment { get; set; }
        public RawLoanDetails LoanDetails { get; set; }
    }
}