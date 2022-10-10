using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Budget.Contracts
{
    public interface IFeeVolumeBasedSetupRepository : IDataRepository<FeeVolumeBasedSetup>
    {
        IEnumerable<FeeVolumeBasedSetupInfo> GetFeeVolumeBasedSetups(string itemCode,string year, string reviewCode);
        IEnumerable<FeeVolumeBasedSetupInfo> GetFeeVolumeBasedSetups(string year, string reviewCode);
    }
}
