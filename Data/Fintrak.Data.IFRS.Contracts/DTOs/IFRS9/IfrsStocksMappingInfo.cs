using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.IFRS.Entities;

namespace Fintrak.Data.IFRS.Contracts
{
    public class IfrsStocksMappingInfo
    {
        public IfrsStocksMapping IfrsStocksMapping { get; set; }
        public IfrsEquityUnqouted IfrsEquityUnqouted { get; set; }
        public IfrsStocksPrimaryData IfrsStocksPrimaryData { get; set; }
    }
}