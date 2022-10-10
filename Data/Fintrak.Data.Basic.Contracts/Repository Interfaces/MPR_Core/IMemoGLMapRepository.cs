using Fintrak.Shared.Basic.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Basic.Contracts
{
    public interface IMemoGLMapRepository : IDataRepository<MemoGLMap>
    {
        IEnumerable<MemoGLMapInfo> GetMemoGLMaps();
    }
}
