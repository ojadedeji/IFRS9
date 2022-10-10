using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsGetCashFlowEIRRepository : IDataRepository<IfrsGetCashFlowEIR>
    {
        IEnumerable<IfrsGetCashFlowEIR> GetIfrsGetCashFlowEIRBySearch(string searchParam, string path);
        IEnumerable<IfrsGetCashFlowEIR> GetIfrsGetCashFlowEIRs(int defaultCount);
        IEnumerable<IfrsGetCashFlowEIR> ExportIfrsGetCashFlowEIR(int defaultCount, string path);
    }
}
