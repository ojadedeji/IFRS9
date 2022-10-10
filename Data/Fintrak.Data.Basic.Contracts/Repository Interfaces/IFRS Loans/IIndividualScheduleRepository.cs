using Fintrak.Shared.Basic.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Basic.Contracts
{
    public interface IIndividualScheduleRepository : IDataRepository<IndividualSchedule>
    {
        //IEnumerable<IndividualScheduleInfo> GetIndividualSchedules();
        IEnumerable<IndividualSchedule> GetIndividualSchedules();
        IEnumerable<IndividualScheduleInfo> GetIndividualSchedules(string refNo);
        IEnumerable<string> GetDistinctRefNos();


    }
}
