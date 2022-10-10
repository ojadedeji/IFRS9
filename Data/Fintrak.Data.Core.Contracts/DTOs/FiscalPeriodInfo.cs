using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core.Contracts
{
    public class FiscalPeriodInfo
    {
        public FiscalPeriod FiscalPeriod { get; set; }
        public FiscalYear FiscalYear { get; set; }
    }
}