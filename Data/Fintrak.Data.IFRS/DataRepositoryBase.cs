using System;
using System.Linq;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Data.IFRS
{
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, IFRSContext>
        where T : class, IIdentifiableEntity, new()
    {
    }
}
