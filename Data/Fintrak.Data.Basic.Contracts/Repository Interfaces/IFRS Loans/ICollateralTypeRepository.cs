using Fintrak.Shared.Basic.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Basic.Contracts
{
    public interface ICollateralTypeRepository : IDataRepository<CollateralType>
    {
        IEnumerable<CollateralTypeInfo> GetCollateralTypes();
        IEnumerable<CollateralTypeInfo> GetCollateralTypes(string categoryCode);
    }
}
