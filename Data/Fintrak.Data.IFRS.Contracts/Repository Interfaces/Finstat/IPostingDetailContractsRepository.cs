using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Data.IFRS.Contracts
{
    public interface IPostingDetailContractsRepository : IDataRepository<PostingDetailContracts>
    {
        IEnumerable<PostingDetailContracts> GetEntitiesByFilter(string filter, string path, int count);
        IEnumerable<string> GetDistinctPostingFilters(int count);
    }
}
