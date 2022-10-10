using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsAccessttcDownTurnResultRepository : IDataRepository<IfrsAccessttcDownTurnResult>
    {
        IEnumerable<IfrsAccessttcDownTurnResult> GetIfrsAccessttcDownTurnResultBySearch(string searchParam, string path);
        IEnumerable<IfrsAccessttcDownTurnResult> GetIfrsAccessttcDownTurnResults(int defaultCount);
        IEnumerable<IfrsAccessttcDownTurnResult> ExportIfrsAccessttcDownTurnResult(int defaultCount, string path);
    }
}
