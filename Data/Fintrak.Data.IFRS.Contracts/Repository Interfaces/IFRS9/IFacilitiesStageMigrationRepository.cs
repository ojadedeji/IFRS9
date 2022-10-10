using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IFacilitiesStageMigrationRepository : IDataRepository<FacilitiesStageMigration>
    {
        IEnumerable<FacilitiesStageMigration> GetAvailableFacilitiesStageMigration(int defaultCount, string path);
        IEnumerable<FacilitiesStageMigration> GetFacilitiesStageMigrationBySearch(string searchParam, string path);
        IEnumerable<string> GetDistinctProduct();
        IEnumerable<FacilitiesStageMigration> GetFacilitiesStageMigrationByProduct(string ProductVal);


    }
}
