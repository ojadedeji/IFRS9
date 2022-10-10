using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IHistoricalMacroVariableInputRepository : IDataRepository<HistoricalMacroVariableInput>
    {
        IEnumerable<HistoricalMacroVariableInput> GetAvailableHistoricalMacroVariableInput(int defaultCount, string path);
        IEnumerable<HistoricalMacroVariableInput> GetHistoricalMacroVariableInputBySearch(string searchParam, string path);
        IEnumerable<HistoricalMacroVariableInput> GetHistoricalMacroVariableInputByReportDate(DateTime ReportDateVal);


    }
}
