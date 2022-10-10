using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Core.Contracts
{
    public interface IExtractionRoleRepository : IDataRepository<ExtractionRole>
    {
        IEnumerable<ExtractionRoleInfo> GetExtractionRoles();
        IEnumerable<ExtractionRoleInfo> GetExtractionRoleByExtraction(int extractionId);
    }
}
