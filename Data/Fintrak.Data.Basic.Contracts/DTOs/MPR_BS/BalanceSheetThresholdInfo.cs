using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class BalanceSheetThresholdInfo
    {
        public BalanceSheetThreshold BalanceSheetThreshold { get; set; }
        public Product Product { get; set; }
        public BSCaption BSCaption { get; set; }

    }
}