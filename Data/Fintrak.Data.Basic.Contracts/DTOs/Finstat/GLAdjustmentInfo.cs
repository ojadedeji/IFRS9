using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class GLAdjustmentInfo
    {
        public GLAdjustment GLAdjustment { get; set; }
        public GLMapping GLMapping { get; set; }
        public Currency Currency { get; set; }
        public Branch Branch { get; set; }
    }
}