using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IInvestmentOthersECLRepository : IDataRepository<InvestmentOthersECL>
    {
        IEnumerable<InvestmentOthersECL> GetInvestmentOthersECLByRefNo(string RefNo);
        IEnumerable<InvestmentOthersECL> GetAvailableInvestmentOthersECL(int defaultCount, string path);
    }
}
