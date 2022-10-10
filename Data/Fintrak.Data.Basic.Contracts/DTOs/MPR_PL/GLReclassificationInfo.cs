using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class GLReclassificationInfo
    {
        public GLReclassification GLReclassification { get; set; }
        public PLCaption PLCaption { get; set; }
    }
}