using System;
using System.Linq;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Data.Core
{
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, CoreContext>
        where T : class, IIdentifiableEntity, new()
    {
    }
}
