using System;
using System.Linq;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Shared.AuditService
{
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, DataContext>
        where T : class, IIdentifiableEntity, new()
    {
    }
}
