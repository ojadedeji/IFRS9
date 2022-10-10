using System;
using System.Linq;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Data.Scorecard
{
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, ScorecardContext>
        where T : class, IIdentifiableEntity, new()
    {
    }
}
