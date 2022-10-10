using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IOffBalancesheetECLRepository : IDataRepository<OffBalancesheetECL>
    {
        IEnumerable<OffBalancesheetECL> GetEntityBySearchParam(string SearchParam);
        IEnumerable<OffBalancesheetECL> GetOffBalancesheetECLs(int defaultCount);
    }
}
