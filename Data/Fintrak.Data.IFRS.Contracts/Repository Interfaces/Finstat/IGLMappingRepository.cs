using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Data.IFRS.Contracts
{
    public interface IGLMappingRepository : IDataRepository<GLMapping>
    {
        IEnumerable<GLMappingInfo> GetGLMappings(int flag,int defaultCount,string path);
        IEnumerable<GLMappingInfo> GetGLMappingsBySearch(int flag, string searchParam);
        List<GLMapping> GetSubSubCaption(string caption);
        IEnumerable<GLMappingInfo> GetDistinctGLMappings();
    }
}
