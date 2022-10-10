using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Core.Contracts
{
    public interface IUploadRoleRepository : IDataRepository<UploadRole>
    {
        IEnumerable<UploadRoleInfo> GetUploadRoles();
        IEnumerable<UploadRoleInfo> GetUploadRoleByUpload(int uploadId);
    }
}
