using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class CapexItemInfo
    {
        public CapexItem CapexItem { get; set; }
        public CapexCategory CapexCategory { get; set; }
    }
}