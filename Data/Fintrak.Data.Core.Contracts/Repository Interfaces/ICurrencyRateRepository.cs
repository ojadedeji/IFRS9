
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core.Contracts
{
    public interface ICurrencyRateRepository : IDataRepository<CurrencyRate>
    {
        IEnumerable<CurrencyRateInfo> GetCurrencyRateByCurrencyId(int currencyId);

        IEnumerable<CurrencyRateInfo> GetCurrencyRateByDate(DateTime runDate,string curSymbol);
        IEnumerable<CurrencyRateInfo> GetCurrencyRateInfo();
        IEnumerable<CurrencyRateInfo> GetCurrencyRateByRateTypeId(int currencyId, int rateTypeId);
        
    }
}
