using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IUnquotedEquityCountryRiskDiscRepository : IDataRepository<UnquotedEquityCountryRiskDisc>
    {
        IEnumerable<UnquotedEquityCountryRiskDisc> GetUnquotedEquityCountryRiskDiscBySearch(string searchParam, string path);
        IEnumerable<UnquotedEquityCountryRiskDisc> GetUnquotedEquityCountryRiskDiscs(int defaultCount);
        IEnumerable<UnquotedEquityCountryRiskDisc> ExportUnquotedEquityCountryRiskDisc(int defaultCount, string path);
    }
}
