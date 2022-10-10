using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class IndividualScheduleInfo
    {
        public LoanPrimaryData LoanPrimaryData { get; set; }
        public IntegralFee IntegralFee { get; set; }
        public LoanIRRData LoanIRRData { get; set; }
    }
}