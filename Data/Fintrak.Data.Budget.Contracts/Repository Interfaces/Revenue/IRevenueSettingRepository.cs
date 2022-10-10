using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Budget.Contracts
{
    public interface IRevenueSettingRepository : IDataRepository<RevenueSetting>
    {
        IEnumerable<RevenueSetting> GetRevenueSettings(string year, string reviewCode);
    }
}
