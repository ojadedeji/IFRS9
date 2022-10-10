using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.IFRS.Entities;

namespace Fintrak.Data.IFRS.Contracts
{
    public class  GLAdjustmentInfo
    {
        public GLAdjustment GLAdjustment { get; set; }
        public GLMapping GLMapping { get; set; }
        public Currency Currency { get; set; }
        public Branch Branch { get; set; }
    }
}