using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class MPRPLDerivedCaptionInfo
    {
        public MPRPLDerivedCaption MPRPLDerivedCaption { get; set; }
        public PLCaption CaptionCode { get; set; }
        public PLCaption DependencyCaptioncode { get; set; }
    }
}