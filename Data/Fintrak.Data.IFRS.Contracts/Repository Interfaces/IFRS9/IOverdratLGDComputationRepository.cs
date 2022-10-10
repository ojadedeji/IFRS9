using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IOverdraftLGDComputationRepository : IDataRepository<OverdraftLGDComputation>
    {
        IEnumerable<OverdraftLGDComputation> GetRecordByRefNo(string RefNo);
        IEnumerable<OverdraftLGDComputation> GetOverdraftLGDComputations(int defaultCount, string path);
    }
}
