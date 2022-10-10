using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Budget.Contracts
{
    public interface ITeamPayClassificationRepository : IDataRepository<TeamPayClassification>
    {
        IEnumerable<TeamPayClassificationInfo> GetTeamPayClassifications(string year, string reviewCode,CenterTypeEnum center,string definitionCode,string misCode);
        IEnumerable<TeamPayClassificationInfo> GetTeamPayClassifications(string year, string reviewCode);
    }
}
