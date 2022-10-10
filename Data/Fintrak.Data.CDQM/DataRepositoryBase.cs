using System;
using System.Linq;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Data.CDQM
{
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, CDQMContext>
        where T : class, IIdentifiableEntity, new()
    {
    }
}
