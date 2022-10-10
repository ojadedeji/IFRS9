using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class InstrumentTypeInfo
    {
        public InstrumentType InstrumentType { get; set; }
        public InstrumentType Parent { get; set; }
    }
}