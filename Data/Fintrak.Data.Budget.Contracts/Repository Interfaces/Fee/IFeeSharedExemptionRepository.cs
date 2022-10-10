using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Budget.Contracts
{
    public interface IFeeSharedExemptionRepository : IDataRepository<FeeSharedExemption>
    {
        IEnumerable<FeeSharedExemptionInfo> GetFeeSharedExemptions(string year, string reviewCode);
    }
}
