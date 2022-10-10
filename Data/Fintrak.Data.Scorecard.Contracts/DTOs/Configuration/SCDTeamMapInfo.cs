using System;
using System.Linq;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Scorecard.Entities;

namespace Fintrak.Data.Scorecard.Contracts
{
    public class SCDTeamMapInfo
    {
        public SCDTeamMap SCDTeamMap { get; set; }     
        public Staff Staff { get; set; }
        public SCDTeamClassification TeamClassification { get; set; }
    }
}

