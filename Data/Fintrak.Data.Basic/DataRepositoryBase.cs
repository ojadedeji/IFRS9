using System;
using System.Linq;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Data.Basic
{
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, BasicContext>
        where T : class, IIdentifiableEntity, new()
    {
    }
}
