using System;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;

namespace Fintrak.Data.IFRS.Contracts
{
    public class GLMappingInfo
    {
        public GLMapping GLMapping { get; set; }
        public IFRSRegistry IFRSRegistry { get; set; }
    }
}