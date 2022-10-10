using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class FeeItemInfo
    {
        public FeeItem FeeItem { get; set; }
        public FeeGroup FeeGroup { get; set; }
        public FeeCaption FeeCaption { get; set; }
        public FeeCategory FeeCategory { get; set; }
    }
}