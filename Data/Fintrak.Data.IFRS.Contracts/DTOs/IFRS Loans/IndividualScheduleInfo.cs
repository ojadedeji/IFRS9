using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.IFRS.Entities;

namespace Fintrak.Data.IFRS.Contracts
{
    public class IndividualScheduleInfo
    {
        public LoanPry LoanPryData { get; set; }
        public IntegralFee IntegralFee { get; set; }
        public LoanIRRData LoanIRRData { get; set; }
    }
}