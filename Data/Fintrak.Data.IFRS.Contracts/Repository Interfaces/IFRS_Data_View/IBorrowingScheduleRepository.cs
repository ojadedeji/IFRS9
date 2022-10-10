using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IBorrowingScheduleRepository : IDataRepository<BorrowingSchedule>
    {
        List<BorrowingSchedule> GetDistinctRefNo();
        IEnumerable<string> GetDistinctBorrowingScheduleRefNos();
        BorrowingSchedule[] GetBorrowingSchedulebyRefNo(string refNo, DateTime? Date, string path);
    }
}
