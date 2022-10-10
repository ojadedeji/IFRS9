using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Shared.Common.Contracts
{
    public interface IServiceFactory
    {
        T CreateClient<T>() where T : IServiceContract;
    }
}