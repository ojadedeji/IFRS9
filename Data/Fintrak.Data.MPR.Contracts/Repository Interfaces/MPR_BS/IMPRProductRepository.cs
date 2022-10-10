using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface IMPRProductRepository : IDataRepository<MPRProduct>
    {
        IEnumerable<MPRProductInfo> GetMPRProducts();
        IEnumerable<MPRProductInfo> GetMPRProducts(string productCode);
    }
}
