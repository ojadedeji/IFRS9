using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsLoansInfoRepository : IDataRepository<IfrsLoansInfo>
    {
        //IEnumerable<IfrsLoansInfo> GetIfrsLoansInfoBySource(string Source);

        IEnumerable<IfrsLoansInfo> GetRecordByRefNo(string Refno);
        IEnumerable<IfrsLoansInfo> GetIfrsLoansInfos(int defaultCount, string path);
    }
}
