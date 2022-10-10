using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Fintrak.Data.Core.Contracts
{
    public interface IUploadRepository : IDataRepository<Upload>
    {
        IEnumerable<UploadInfo> GetUploads();
        void UploadData(string actionName, SqlParameter[] parameters);

    }
}
