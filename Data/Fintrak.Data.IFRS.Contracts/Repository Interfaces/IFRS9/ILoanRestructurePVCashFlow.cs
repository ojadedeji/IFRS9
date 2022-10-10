using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface ILoanRestructurePVCashFlowRepository : IDataRepository<LoanRestructurePVCashFlow>
    {
        IEnumerable<LoanRestructurePVCashFlow> GetAvailLoanRestructurePVCashFlows(int defaultCount, string path);
        IEnumerable<LoanRestructurePVCashFlow> GetLoanRestructurePVCashFlowsBySearch(string searchParam, string path);
        IEnumerable<string> GetDistinctLoanRestructurePVCashFlows();
        IEnumerable<LoanRestructurePVCashFlow> GetSubRefNo(string refno);

    }
}
