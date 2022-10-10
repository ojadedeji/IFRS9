using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IMarginalPdObeDistrRepository : IDataRepository<MarginalPdObeDistr>
    {
        IEnumerable<MarginalPdObeDistr> GetMarginalPdObeDistrBySearch(string searchParam);
        IEnumerable<MarginalPdObeDistr> GetMarginalPdObeDistrs(int defaultCount);
    }
}
