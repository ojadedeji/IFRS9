using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class CapexCategoryInfo
    {
        public CapexCategory CapexCategory { get; set; }
        public CapexCategory Parent { get; set; }
    }
}