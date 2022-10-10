using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class GLReclassificationInfo
    {
        public GLReclassification GLReclassification { get; set; }
        public PLCaption PLCaption { get; set; }
    }
}