using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class MPRGLMappingInfo
    {
        public MPRGLMapping MPRGLMapping { get; set; }
        public PLCaption PLCaption { get; set; }
        public GLDefinition GLDefinition { get; set; }
    }
}

