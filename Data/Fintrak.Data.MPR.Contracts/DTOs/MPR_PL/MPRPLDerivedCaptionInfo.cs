using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class MPRPLDerivedCaptionInfo
    {
        public MPRPLDerivedCaption MPRPLDerivedCaption { get; set; }
        public PLCaption CaptionCode { get; set; }
        public PLCaption DependencyCaptioncode { get; set; }
    }
}