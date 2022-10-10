using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IUnquotedEquityPEPBRatioRepository : IDataRepository<UnquotedEquityPEPBRatio>
    {
        IEnumerable<UnquotedEquityPEPBRatio> GetUnquotedEquityPEPBRatioBySearch(string searchParam, string path);
        IEnumerable<UnquotedEquityPEPBRatio> GetUnquotedEquityPEPBRatios(int defaultCount);
        IEnumerable<UnquotedEquityPEPBRatio> ExportUnquotedEquityPEPBRatio(int defaultCount, string path);
    }
}
