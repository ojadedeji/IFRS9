using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Budget.Contracts
{
    public interface IProductInterestRateRepository : IDataRepository<ProductInterestRate>
    {
        IEnumerable<ProductInterestRateInfo> GetProductInterestRates(string year, string reviewCode, string definitionCode, string misCode);
        IEnumerable<ProductInterestRateInfo> GetProductInterestRates(string year, string reviewCode);
    }
}
