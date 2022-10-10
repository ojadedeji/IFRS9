using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Core.Framework;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Core.Contracts
{
    public interface IProcessTriggerRepository : IDataRepository<ProcessTrigger>
    {
        IEnumerable<ProcessTriggerInfo> GetProcessTriggers();
        IEnumerable<ProcessTriggerInfo> GetProcessTriggerByJob(string jobCode);
        IEnumerable<ProcessTriggerInfo> GetProcessTriggerByProcess(int processId);
        IEnumerable<ProcessTriggerInfo> GetProcessTriggerByRunDate();
        IEnumerable<ProcessTriggerInfo> GetProcessTriggerByRunTime(DateTime runTime);
    }
}
