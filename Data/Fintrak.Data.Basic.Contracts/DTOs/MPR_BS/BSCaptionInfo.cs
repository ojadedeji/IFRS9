using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class BSCaptionInfo
    {
        public BSCaption BSCaption { get; set; }
        public BSCaption Parent { get; set; }
        public PLCaption PLCaption { get; set; }
    }
}