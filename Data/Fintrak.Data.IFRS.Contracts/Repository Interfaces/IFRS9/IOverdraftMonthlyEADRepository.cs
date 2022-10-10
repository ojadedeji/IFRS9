using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IOverdraftMonthlyEADRepository : IDataRepository<OverdraftMonthlyEAD>
    {
        IEnumerable<OverdraftMonthlyEAD> GetOverdraftMonthlyEADBySearch(string searchParam);
    }
}
