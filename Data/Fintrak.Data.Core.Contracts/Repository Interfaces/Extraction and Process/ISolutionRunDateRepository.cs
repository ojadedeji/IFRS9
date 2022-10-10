using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Core.Contracts
{
    public interface ISolutionRunDateRepository : IDataRepository<SolutionRunDate>
    {
        IEnumerable<SolutionRunDateInfo> GetSolutionRunDates();
        IEnumerable<SolutionRunDate> GetRunDate();

    }
}
