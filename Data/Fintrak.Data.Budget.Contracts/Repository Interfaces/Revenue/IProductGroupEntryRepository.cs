using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Budget.Contracts
{
    public interface IProductGroupEntryRepository : IDataRepository<ProductGroupEntry>
    {
        IEnumerable<ProductGroupEntryInfo> GetProductGroupEntries(string year, string reviewCode,string definitionCode,string misCode);
        IEnumerable<ProductGroupEntryInfo> GetProductGroupEntries(string year, string reviewCode);
    }
}
