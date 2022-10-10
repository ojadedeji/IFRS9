using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IMonthlyObeEadSTRLBRepository : IDataRepository<MonthlyObeEadSTRLB>{
        IEnumerable<MonthlyObeEadSTRLB> GetMonthlyObeEadSTRLBBySearch(string searchParam);
        IEnumerable<MonthlyObeEadSTRLB> GetMonthlyObeEadSTRLBs(int defaultCount);
    }
}
