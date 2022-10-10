using System;
using System.Linq;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Scorecard.Entities;

namespace Fintrak.Data.Scorecard.Contracts
{
    public class SCDKPIEntryInfo
    {
        public SCDKPIEntry SCDKPIEntry { get; set; }
        public Staff Staff { get; set; }
        public SCDKPI SCDKPI { get; set; }

        public SCDTeamMap SCDTeamMap { get; set; }
    }
}

