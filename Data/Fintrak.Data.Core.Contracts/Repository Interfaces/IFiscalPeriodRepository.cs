
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core.Contracts
{
    public interface IFiscalPeriodRepository : IDataRepository<FiscalPeriod>
    {
        FiscalPeriodInfo GetOpenFiscalPeriodInfo();
        IEnumerable<FiscalPeriodInfo> GetFiscalPeriodInfo(int fiscalYearId);
    }
}
