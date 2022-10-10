using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface ILoanScheduleRepository : IDataRepository<LoanSchedule>
    {
        IEnumerable<string> GetDistinctLoanScheduleRefNos();
        IEnumerable<MultiSelectDropDown> GetDistinctRefNo();
        IEnumerable<LoanSchedule> GetScheduleRange(string refNo, DateTime? rangeDate, string path);
    }
}
