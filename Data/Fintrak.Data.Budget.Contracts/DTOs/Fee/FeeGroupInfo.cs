using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class FeeGroupInfo
    {
        public FeeGroup FeeGroup { get; set; }
        public FeeGroup Parent { get; set; }
    }
}