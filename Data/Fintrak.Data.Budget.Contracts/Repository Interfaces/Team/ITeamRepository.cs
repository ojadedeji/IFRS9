using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Budget.Contracts
{
    public interface ITeamRepository : IDataRepository<Team>
    {
        IEnumerable<TeamInfo> GetTeamInDefinition(string year, string reviewCode,string definitionCode);
        IEnumerable<TeamInfo> GetTeamUnderDefinition(string year, string reviewCode, string definitionCode);
        IEnumerable<TeamInfo> GetTeamUnderDefinition(string year, string reviewCode, string definitionCode,string misCode);
        IEnumerable<TeamInfo> GetTeams(string year, string reviewCode);
    }
}
