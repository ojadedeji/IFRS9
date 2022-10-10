using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class IncomeCentralVaultScheduleInfo
    {
        public IncomeCentralVaultSchedule IncomeCentralVaultSchedule { get; set; }
        public Branch Branch { get; set; }
    }
}