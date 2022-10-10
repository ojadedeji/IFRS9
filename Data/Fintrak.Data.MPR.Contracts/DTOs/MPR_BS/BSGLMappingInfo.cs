using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class BSGLMappingInfo
    {
        public BSGLMapping BSGLMapping { get; set; }
        public Product Product { get; set; }
        public GLDefinition GLDefinition { get; set; }
    }
}