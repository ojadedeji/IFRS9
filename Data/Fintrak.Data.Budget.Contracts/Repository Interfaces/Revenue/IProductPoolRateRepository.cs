using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Budget.Contracts
{
    public interface IProductPoolRateRepository : IDataRepository<ProductPoolRate>
    {
        IEnumerable<ProductPoolRateInfo> GetProductPoolRates(string year, string reviewCode,string definitionCode,string misCode);
        IEnumerable<ProductPoolRateInfo> GetProductPoolRates(string year, string reviewCode);
    }
}
