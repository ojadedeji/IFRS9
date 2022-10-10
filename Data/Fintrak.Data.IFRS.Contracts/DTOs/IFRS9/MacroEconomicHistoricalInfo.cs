using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.IFRS.Entities;

namespace Fintrak.Data.IFRS.Contracts
{
    public class MacroEconomicHistoricalInfo
    {
        public MacroEconomicHistorical MacroEconomicHistorical { get; set; }
        public Sector Sector { get; set; }
        public MacroEconomicVariable MacroEconomicVariable { get; set; }
    }
}