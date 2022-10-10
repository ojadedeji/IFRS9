using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Shared.Common.Contracts
{
    public interface IDataRepositoryFactory
    {
        T GetDataRepository<T>() where T : IDataRepository;
    }
}
