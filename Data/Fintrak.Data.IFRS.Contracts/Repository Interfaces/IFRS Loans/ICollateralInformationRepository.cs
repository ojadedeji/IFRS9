using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Data.IFRS.Contracts
{
    public interface ICollateralInformationRepository : IDataRepository<CollateralInformation>
    {
        IEnumerable<CollateralDetailsInfo> GetCollateralDetails();
        IEnumerable<CollateralInformation> GetAvailableCollateralInformation(int defaultCount, string path);

    }
}
