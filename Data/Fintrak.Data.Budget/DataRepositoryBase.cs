using System;
using System.Linq;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Data.Budget
{
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, BudgetContext>
        where T : class, IIdentifiableEntity, new()
    {
    }
}
