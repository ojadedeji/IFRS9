using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class MPRGLMappingInfo
    {
        public MPRGLMapping MPRGLMapping { get; set; }
        public PLCaption PLCaption { get; set; }
        public GLDefinition GLDefinition { get; set; }
    }
}

