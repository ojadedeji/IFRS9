using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Budget.Contracts
{
    public interface IStaffCostRepository : IDataRepository<StaffCost>
    {
        IEnumerable<StaffCostInfo> GetStaffCosts(string year, string reviewCode, string definitionCode, string misCode, CenterTypeEnum center);
    }
}
