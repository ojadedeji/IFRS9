using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IObeECLResultRepository : IDataRepository<ObeECLResult>{
        IEnumerable<ObeECLResult> GetObeECLResultBySearch(string searchParam);
        IEnumerable<ObeECLResult> GetObeECLResults(int defaultCount, string path);
    }
}
