using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsScalarUploadRepository : IDataRepository<IfrsScalarUpload>
    {
        IEnumerable<IfrsScalarUpload> GetAvailableIfrsScalarUpload(int defaultCount, string path);
        IEnumerable<IfrsScalarUpload> GetIfrsScalarUploadBySearch(string searchParam, string path);
        IEnumerable<string> GetDistinctScalarType(); 
        IEnumerable<IfrsScalarUpload> GetIfrsScalarUploadByScalarType(string ScalarTypeVal);


    }
}
