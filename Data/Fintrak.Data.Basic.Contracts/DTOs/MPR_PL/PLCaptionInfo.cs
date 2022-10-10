using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class PLCaptionInfo
    {
        public PLCaption PLCaption { get; set; }
        public PLCaption Parent { get; set; }

    }
}