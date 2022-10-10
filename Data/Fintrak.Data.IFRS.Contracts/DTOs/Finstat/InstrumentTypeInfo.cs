using System;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;

namespace Fintrak.Data.IFRS.Contracts
{
    public class InstrumentTypeInfo
    {
        public InstrumentType InstrumentType { get; set; }
        public InstrumentType Parent { get; set; }
    }
}