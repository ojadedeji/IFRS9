using System;
using System.Linq;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Scorecard.Entities;

namespace Fintrak.Data.Scorecard.Contracts
{
    public class SCDThresholdInfo
    {
        public SCDThreshold SCDThreshold { get; set; }
        public SCDTeamClassification SCDTeamClassification { get; set; }
        public SCDKPI SCDKPI { get; set; }
        public Staff Staff { get; set; }
    }
}

