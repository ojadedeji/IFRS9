using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Budget.Contracts
{
    public interface ICapexEntryRepository : IDataRepository<CapexEntry>
    {
        IEnumerable<CapexEntryInfo> GetCapexEntries(string year, string reviewCode,string categoryCode,string definitionCode,string misCode);
        IEnumerable<CapexEntryInfo> GetCapexEntries(string year, string reviewCode);
    }
}
