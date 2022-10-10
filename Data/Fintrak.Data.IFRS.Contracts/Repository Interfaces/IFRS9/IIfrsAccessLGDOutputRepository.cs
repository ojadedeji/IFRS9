using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsAccessLGDOutputRepository : IDataRepository<IfrsAccessLGDOutput>
    {
        IEnumerable<IfrsAccessLGDOutput> GetIfrsAccessLGDOutputBySearch(string searchParam, string path);
        IEnumerable<IfrsAccessLGDOutput> GetIfrsAccessLGDOutputs(int defaultCount);
        IEnumerable<IfrsAccessLGDOutput> ExportIfrsAccessLGDOutput(int defaultCount, string path);
    }
}
