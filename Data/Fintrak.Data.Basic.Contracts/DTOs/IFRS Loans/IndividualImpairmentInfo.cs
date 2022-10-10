using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class IndividualImpairmentInfo
    {
        public IndividualImpairment IndividualImpairment { get; set; }
        public LoanDetails LoanDetails { get; set; }
    }
}