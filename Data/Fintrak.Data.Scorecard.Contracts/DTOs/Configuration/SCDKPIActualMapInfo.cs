using System;
using System.Linq;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Scorecard.Entities;

namespace Fintrak.Data.Scorecard.Contracts
{
    public class SCDKPIActualMapInfo
    {
        public SCDKPIActualMap scdkpiactualmap { get; set; }
        public SCDKPI scdkpi { get; set; }


    }
}

