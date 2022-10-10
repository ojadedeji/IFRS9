
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.SystemCore.Entities;
using Fintrak.Shared.SystemCore.Framework;

namespace Fintrak.Data.SystemCore.Contracts
{
    public interface IAuditTrailRepository : IDataRepository<AuditTrail>
    {
        IEnumerable<AuditTrail> GetByDate(DateTime fromDate, DateTime toDate);

        //IEnumerable<AuditTrail> GetByAction(AuditAction action, DateTime fromDate, DateTime toDate);

        IEnumerable<AuditTrail> GetByTable(string tableName, DateTime fromDate, DateTime toDate);

        IEnumerable<AuditTrail> GetByLoginID(string loginID, DateTime fromDate, DateTime toDate);

        IEnumerable<AuditTrail> GetAuditTrailByTab(AuditAction action);

        List<AuditTrail> GetByAction(string action, DateTime fromDate, DateTime toDate);

    }
}
