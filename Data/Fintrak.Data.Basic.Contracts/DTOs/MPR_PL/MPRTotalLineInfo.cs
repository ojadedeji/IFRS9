using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class MPRTotalLineInfo
    {
        public MPRTotalLine MPRTotalLine { get; set; }
        public MPRTotalLine Parent { get; set; }
    }
}