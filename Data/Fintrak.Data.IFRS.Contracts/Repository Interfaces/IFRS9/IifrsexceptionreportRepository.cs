using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IifrsexceptionreportRepository : IDataRepository<ifrsexceptionreport>
    {
        //IEnumerable<IfrsLoansInfo> GetIfrsLoansInfoBySource(string Source);
        IEnumerable<ifrsexceptionreport> GetRecordByRefNo(string searchParam);
        IEnumerable<ifrsexceptionreport> Getifrsexceptionreport(int defaultCount, string path);
        IEnumerable<ifrsexceptionreport> GetException();
    }
}
