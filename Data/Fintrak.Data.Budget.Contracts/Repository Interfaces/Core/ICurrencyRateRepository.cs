using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Budget.Contracts
{
    public interface ICurrencyRateRepository : IDataRepository<CurrencyRate>
    {
        CurrencyRate GetCurrencyRate(string year, string reviewCode, RateTypeEnum rateType,string currencyCode);
        IEnumerable<CurrencyRate> GetCurrencyRates(string year,string reviewCode,RateTypeEnum rateType);
    }
}
