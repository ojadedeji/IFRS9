using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Shared.Common.Data
{
    [Export(typeof(IDataRepositoryFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        T IDataRepositoryFactory.GetDataRepository<T>()
        {
            return ObjectBase.Container.GetExportedValue<T>();
        }
    }
}
