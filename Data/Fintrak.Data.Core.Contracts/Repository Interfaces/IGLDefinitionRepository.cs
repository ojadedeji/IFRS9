
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Core.Contracts
{
    public interface IGLDefinitionRepository : IDataRepository<GLDefinition>

    {
        //Currency GetCurrency();
    }
}
