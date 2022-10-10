using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Data.IFRS.Contracts
{
    public interface IGLAdjustmentRepository : IDataRepository<GLAdjustment>
    {
        IEnumerable<GLAdjustmentInfo> GetGLAdjustments(AdjustmentType adjustmentType, ReportType reportType, DateTime runDate);
    }
}
