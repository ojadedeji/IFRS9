using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Budget.Contracts
{
    public interface IPayStructureRepository : IDataRepository<PayStructure>
    {
        IEnumerable<PayStructureInfo> GetPayStructures(string year, string reviewCode,string classificationCode);
        IEnumerable<PayStructureInfo> GetPayStructures(string year, string reviewCode);
    }
}
