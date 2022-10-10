using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface IIncomeCentralVaultScheduleRepository : IDataRepository<IncomeCentralVaultSchedule>
    {
        IEnumerable<IncomeCentralVaultScheduleInfo> GetIncomeCentralVaultSchedule();    
    }
}
