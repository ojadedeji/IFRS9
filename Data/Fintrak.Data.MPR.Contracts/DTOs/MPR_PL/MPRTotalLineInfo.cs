using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class MPRTotalLineInfo
    {
        public MPRTotalLine MPRTotalLine { get; set; }
        public MPRTotalLine Parent { get; set; }
    }
}