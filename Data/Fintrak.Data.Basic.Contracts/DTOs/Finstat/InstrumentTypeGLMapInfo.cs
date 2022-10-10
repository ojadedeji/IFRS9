using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class InstrumentTypeGLMapInfo
    {
        public InstrumentTypeGLMap InstrumentTypeGLMap { get; set; }
        public InstrumentType InstrumentType { get; set; }
        public GLType GLType { get; set; }
        public GLMapping GLMapping { get; set; }
    }
}