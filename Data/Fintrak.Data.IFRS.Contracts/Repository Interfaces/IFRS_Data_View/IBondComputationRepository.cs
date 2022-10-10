using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IBondComputationRepository : IDataRepository<BondComputation>
    {
        List<BondComputation> GetDistinctRefNo();
        BondComputation[] GetBondComputationResultbyRefNo(string refNo, DateTime? Date, string path);
    }
}
