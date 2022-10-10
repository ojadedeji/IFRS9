using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsLifetimePDStagesRepository : IDataRepository<IfrsLifetimePDStages>
    {
        //IEnumerable<IfrsLifetimePDStages> GetIfrsLifetimePDStagesBySearch(string searchParam, string path);
        IEnumerable<IfrsLifetimePDStages> ExportIfrsLifetimePDStages(int defaultCount, string path);
    }
}
