using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Data.IFRS.Contracts
{
    public interface IBorrowingPeriodicScheduleRepository : IDataRepository<BorrowingPeriodicSchedule>
    {
        IEnumerable<string> GetDistinctBorrowingPeriodicScheduleRefNos();
        List<BorrowingPeriodicSchedule> GetDistinctRefNo();
        BorrowingPeriodicSchedule[] GetBorrowingPeriodicSchedulebyRefNo(string refNo, string path);
    }
}
