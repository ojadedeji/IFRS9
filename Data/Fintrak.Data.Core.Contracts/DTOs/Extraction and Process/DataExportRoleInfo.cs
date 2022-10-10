using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core.Contracts
{
    public class DataExportRoleInfo
    {
        public DataExportRole DataExportRole { get; set; }
        public DataExport DataExport { get; set; }
    }
}