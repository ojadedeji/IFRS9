using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class PLCaptionInfo
    {
        public PLCaption PLCaption { get; set; }
        public PLCaption Parent { get; set; }

    }
}