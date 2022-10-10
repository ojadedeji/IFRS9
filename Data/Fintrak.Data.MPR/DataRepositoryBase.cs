using System;
using System.Linq;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Data.MPR
{
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, MPRContext>
        where T : class, IIdentifiableEntity, new()
    {
    }
}
