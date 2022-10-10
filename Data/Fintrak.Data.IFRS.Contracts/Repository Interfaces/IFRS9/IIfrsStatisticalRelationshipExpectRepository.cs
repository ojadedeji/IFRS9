using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsStatisticalRelationshipExpectRepository : IDataRepository<IfrsStatisticalRelationshipExpect>
    {
        IEnumerable<IfrsStatisticalRelationshipExpect> GetIfrsStatisticalRelationshipExpectBySearch(string searchParam, string path);
        IEnumerable<IfrsStatisticalRelationshipExpect> GetIfrsStatisticalRelationshipExpects(int defaultCount);
        IEnumerable<IfrsStatisticalRelationshipExpect> ExportIfrsStatisticalRelationshipExpect(int defaultCount, string path);
    }
}
