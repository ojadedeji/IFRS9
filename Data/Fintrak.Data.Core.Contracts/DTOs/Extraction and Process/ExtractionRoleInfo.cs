using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core.Contracts
{
    public class ExtractionRoleInfo
    {
        public ExtractionRole ExtractionRole { get; set; }
        public Extraction Extraction { get; set; }
        ///
    }
}