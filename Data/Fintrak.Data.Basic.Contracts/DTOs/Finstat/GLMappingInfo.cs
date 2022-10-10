using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class GLMappingInfo
    {
        public GLMapping GLMapping { get; set; }
        public IFRSRegistry IFRSRegistry { get; set; }
    }
}