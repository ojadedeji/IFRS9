using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IHistoricalDefaultFreqRepository : IDataRepository<HistoricalDefaultFreq>{
        IEnumerable<HistoricalDefaultFreq> GetHistoricalDefaultFreqBySearch(string searchParam);
        IEnumerable<HistoricalDefaultFreq> GetHistoricalDefaultFreqs(int defaultCount, string path);
    }
}
