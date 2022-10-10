using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Budget.Contracts
{
    public interface IOfficerDetailRepository : IDataRepository<OfficerDetail>
    {
        IEnumerable<OfficerDetailInfo> GetOfficerDetailInDefinition(string year, string reviewCode, string definitionCode);
        IEnumerable<OfficerDetailInfo> GetOfficerDetailUnderDefinition(string year, string reviewCode, string definitionCode);
        IEnumerable<OfficerDetailInfo> GetOfficerDetailUnderDefinition(string year, string reviewCode, string definitionCode, string misCode);
        IEnumerable<OfficerDetailInfo> GetOfficerDetails(string year, string reviewCode);
    }
}
