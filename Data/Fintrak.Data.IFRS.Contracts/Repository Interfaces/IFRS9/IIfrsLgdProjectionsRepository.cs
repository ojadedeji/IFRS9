using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsLgdProjectionsRepository : IDataRepository<IfrsLgdProjections>
    {
        //IEnumerable<IfrsLgdProjections> GetIfrsLgdProjectionsBySource(string Source);
        IEnumerable<IfrsLgdProjections> GetRecordByRefNo(string RefNo);
        IEnumerable<IfrsLgdProjections> GetIfrsLgdProjections(int defaultCount, string path);
    }
}
