using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IAmortizationOutputRepository : IDataRepository<AmortizationOutput>
    {
        IEnumerable<AmortizationOutput> GetAmortizationOutputBySearch(string searchParam,string path);
        IEnumerable<AmortizationOutput> GetAmortizationOutputs(int defaultCount);
        IEnumerable<AmortizationOutput> ExportAmortizationOutput(int defaultCount, string path);
    }
}
