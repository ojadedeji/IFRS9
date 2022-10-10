using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class BSGLMappingInfo
    {
        public BSGLMapping BSGLMapping { get; set; }
        public Product Product { get; set; }
        public GLDefinition GLDefinition { get; set; }
    }
}