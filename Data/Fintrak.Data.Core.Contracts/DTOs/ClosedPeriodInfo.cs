using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core.Contracts
{
    public class ClosedPeriodInfo
    {
        public ClosedPeriod ClosedPeriod { get; set; }
        public Solution Solution { get; set; }
    }
}