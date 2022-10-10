using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IUnquotedEquityMKTRDiscRepository : IDataRepository<UnquotedEquityMKTRDisc>
    {
        IEnumerable<UnquotedEquityMKTRDisc> GetUnquotedEquityMKTRDiscBySearch(string searchParam, string path);
        IEnumerable<UnquotedEquityMKTRDisc> GetUnquotedEquityMKTRDiscs(int defaultCount);
        IEnumerable<UnquotedEquityMKTRDisc> ExportUnquotedEquityMKTRDisc(int defaultCount, string path);
    }
}
