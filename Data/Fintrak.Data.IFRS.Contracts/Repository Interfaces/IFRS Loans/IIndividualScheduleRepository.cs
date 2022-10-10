using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIndividualScheduleRepository : IDataRepository<IndividualSchedule>
    {
        //IEnumerable<IndividualScheduleInfo> GetIndividualSchedules();
        IEnumerable<IndividualSchedule> GetIndividualSchedules();
        IEnumerable<IndividualScheduleInfo> GetIndividualSchedules(string refNo);
        IEnumerable<string> GetDistinctRefNos();


    }
}
