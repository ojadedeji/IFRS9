using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface ICashFlowTBRepository : IDataRepository<CashFlowTB> {
        IEnumerable<CashFlowTB> GetCashFlowTBBySearch(string searchParam);
        IEnumerable<CashFlowTB> GetCashFlowTBs(int defaultCount, string path);    
    }
}
