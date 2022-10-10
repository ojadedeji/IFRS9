using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IUnquotedEquitySummaryInputRepository : IDataRepository<UnquotedEquitySummaryInput>
    {
        IEnumerable<UnquotedEquitySummaryInput> GetUnquotedEquitySummaryInputBySearch(string searchParam, string path);
        IEnumerable<UnquotedEquitySummaryInput> GetUnquotedEquitySummaryInputs(int defaultCount);
        IEnumerable<UnquotedEquitySummaryInput> ExportUnquotedEquitySummaryInput(int defaultCount, string path);
    }
}
