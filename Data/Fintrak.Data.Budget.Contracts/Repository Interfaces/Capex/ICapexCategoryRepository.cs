using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Budget.Contracts
{
    public interface ICapexCategoryRepository : IDataRepository<CapexCategory>
    {
        IEnumerable<CapexCategoryInfo> GetCapexCategories(string year, string reviewCode);

        IEnumerable<CapexCategoryInfo> GetAllCapexCategories();
    }
}
