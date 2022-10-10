using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core.Contracts
{
    public class CurrencyRateInfo
    {
        public CurrencyRate CurrencyRate { get; set; }
        public Currency Currency { get; set; }
        public RateType RateType { get; set; }
    }
}