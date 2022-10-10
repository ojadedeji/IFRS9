using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsRepaymentScheduleRepository : IDataRepository<IfrsRepaymentSchedule>
    {
        IEnumerable<IfrsRepaymentSchedule> GetIfrsRepaymentScheduleBySearch(string searchParam, string path);
        IEnumerable<IfrsRepaymentSchedule> GetIfrsRepaymentSchedules(int defaultCount);
        IEnumerable<IfrsRepaymentSchedule> ExportIfrsRepaymentSchedule(int defaultCount, string path);
    }
}
