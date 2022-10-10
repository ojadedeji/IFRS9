using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsOverdraftDataRepository : IDataRepository<IfrsOverdraftData>
    {
        //IEnumerable<IfrsOverdraftData> GetIfrsOverdraftDataBySource(string Source);
        IEnumerable<IfrsOverdraftData> GetRecordByRefNo(string RefNo);
        IEnumerable<IfrsOverdraftData> GetIfrsOverdraftData(int defaultCount, string path);
    }
}
