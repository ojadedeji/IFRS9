using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsCorporateEclRepository : IDataRepository<IfrsCorporateEcl>
    {
        IEnumerable<IfrsCorporateEcl> GetEntityByRefNo(string refNo);
        IEnumerable<IfrsCorporateEcl> ExportIfrsCorporateEcl(int defaultCount, string path);
    }
}
