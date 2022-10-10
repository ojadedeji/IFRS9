
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Common.Services.QueryService;

namespace Fintrak.Data.Core.Contracts
{
    public interface IProductRepository : IDataRepository<Product>
    {
        IEnumerable<Product> GetPaginatedEntities(QueryOptions queryOptions);
    }
}
