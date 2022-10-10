using System;
using System.Linq;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Data.SystemCore
{
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, SystemCoreContext>
        where T : class, IIdentifiableEntity, new()
    {
    }
}
