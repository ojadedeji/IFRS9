using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class BSCaptionInfo
    {
        public BSCaption BSCaption { get; set; }
        public BSCaption Parent { get; set; }
        public PLCaption PLCaption { get; set; }
    }
}