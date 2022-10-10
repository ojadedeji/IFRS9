using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Data.IFRS.Contracts
{
    public interface IBondPeriodicScheduleRepository : IDataRepository<BondPeriodicSchedule>
    {
        IEnumerable<string> GetDistinctBondPeriodicScheduleRefNos();
        IEnumerable<BondPeriodicSchedule> GetBondPeriodicScheduleRefNos(string refNo, string path);
    }
}
