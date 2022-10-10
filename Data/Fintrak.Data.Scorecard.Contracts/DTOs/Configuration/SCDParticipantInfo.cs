using System;
using System.Linq;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Scorecard.Entities;

namespace Fintrak.Data.Scorecard.Contracts
{
    public class SCDParticipantInfo
    {
        public SCDParticipant SCDParticipant { get; set; }
        public SCDKPI SCDKPI { get; set; }
        public SCDTeamClassification Classification { get; set; }
        public Staff Staff { get; set; }
    }
}

