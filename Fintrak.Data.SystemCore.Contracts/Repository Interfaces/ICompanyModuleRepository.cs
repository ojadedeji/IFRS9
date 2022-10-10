
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.SystemCore.Entities;
using Fintrak.Shared.SystemCore.Framework;

namespace Fintrak.Data.SystemCore.Contracts
{
    public interface ICompanyModuleRepository : IDataRepository<CompanyModule>
    {
        IEnumerable<CompanyModuleInfo> GetCompanyModules();
        IEnumerable<CompanyModuleInfo> GetCompanyModuleByCompanyCode(string companyCode);
    }
}
