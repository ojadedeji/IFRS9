using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class MPRTotalLineMakeUpInfo
    {
        public MPRTotalLineMakeUp MPRTotalLineMakeUp { get; set; }
        public PLCaption PLCaption { get; set; }

    }
}