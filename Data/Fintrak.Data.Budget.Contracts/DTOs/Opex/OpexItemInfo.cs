using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class OpexItemInfo
    {
        public OpexItem OpexItem { get; set; }
        public OpexCategory OpexCategory { get; set; }
    }
}