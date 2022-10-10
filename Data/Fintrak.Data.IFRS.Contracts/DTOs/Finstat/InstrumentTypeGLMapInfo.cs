using System;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;

namespace Fintrak.Data.IFRS.Contracts
{
    public class InstrumentTypeGLMapInfo
    {
        public InstrumentTypeGLMap InstrumentTypeGLMap { get; set; }
        public InstrumentType InstrumentType { get; set; }
        public GLType GLType { get; set; }
        public GLMapping GLMapping { get; set; }
    }
}