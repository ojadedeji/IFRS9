using System;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;

namespace Fintrak.Data.IFRS.Contracts
{
    public class CollateralRealizationPeriodInfo
    {
        public CollateralRealizationPeriod CollateralRealizationPeriod { get; set; }
        public CollateralType CollateralType { get; set; }
    }
}