using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsDescriptionLedgerRepository : IDataRepository<IfrsDescriptionLedger>
    {
        IEnumerable<IfrsDescriptionLedger> GetIfrsDescriptionLedgerBySearch(string searchParam, string path);
        IEnumerable<IfrsDescriptionLedger> GetIfrsDescriptionLedgers(int defaultCount);
        IEnumerable<IfrsDescriptionLedger> ExportIfrsDescriptionLedger(int defaultCount, string path);
    }
}
