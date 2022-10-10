using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IUnquotedEquityInputRepository : IDataRepository<UnquotedEquityInput>
    {
        IEnumerable<UnquotedEquityInput> GetUnquotedEquityInputBySearch(string searchParam, string path);
        IEnumerable<UnquotedEquityInput> GetUnquotedEquityInputs(int defaultCount);
        IEnumerable<UnquotedEquityInput> ExportUnquotedEquityInput(int defaultCount, string path);
    }
}
