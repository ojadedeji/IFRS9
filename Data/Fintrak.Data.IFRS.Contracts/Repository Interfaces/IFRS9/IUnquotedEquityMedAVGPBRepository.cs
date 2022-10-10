using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IUnquotedEquityMedAVGPBRepository : IDataRepository<UnquotedEquityMedAVGPB>
    {
        IEnumerable<UnquotedEquityMedAVGPB> GetUnquotedEquityMedAVGPBBySearch(string searchParam, string path);
        IEnumerable<UnquotedEquityMedAVGPB> GetUnquotedEquityMedAVGPBs(int defaultCount);
        IEnumerable<UnquotedEquityMedAVGPB> ExportUnquotedEquityMedAVGPB(int defaultCount, string path);
    }
}
