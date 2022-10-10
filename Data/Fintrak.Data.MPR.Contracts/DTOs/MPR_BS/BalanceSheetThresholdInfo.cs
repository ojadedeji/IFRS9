using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class BalanceSheetThresholdInfo
    {
        public BalanceSheetThreshold BalanceSheetThreshold { get; set; }
        public Product Product { get; set; }
        public BSCaption BSCaption { get; set; }

    }
}