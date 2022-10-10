using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core.Contracts
{
    public class UploadRoleInfo
    {
        public UploadRole UploadRole { get; set; }
        public Upload Upload { get; set; }
    }
}