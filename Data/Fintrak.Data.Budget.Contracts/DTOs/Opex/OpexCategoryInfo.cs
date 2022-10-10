using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class OpexCategoryInfo
    {
        public OpexCategory OpexCategory { get; set; }
        public OpexCategory Parent { get; set; }
    }
}