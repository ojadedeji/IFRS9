using System;
using System.Linq;
using Fintrak.Shared.SystemCore.Entities;

namespace Fintrak.Data.SystemCore.Contracts
{
    public class AuditTrailInfo
    {
        public AuditTrail AuditTrail { get; set; }

        public AuditActionObsolete AuditActionObsolete { get; set; }
    }
}