using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core.Contracts
{
    public class AuditTrailInfo
    {
        public AuditTrail AuditTrail { get; set; }

        public AuditActionObsolete AuditActionObsolete { get; set; }
    }
}