using System;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;

namespace Fintrak.Data.IFRS.Contracts
{
    public class GLMappingMgtInfo
    {
        public GLMappingMgt GLMappingMgt { get; set; }
        public IFRSRegistry IFRSRegistry { get; set; }
    }
}