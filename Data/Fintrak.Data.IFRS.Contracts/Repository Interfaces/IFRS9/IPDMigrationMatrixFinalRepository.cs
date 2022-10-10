using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IPDMigrationMatrixFinalRepository : IDataRepository<PDMigrationMatrixFinal>
    {
        IEnumerable<PDMigrationMatrixFinal> GetAvailablePDMigrationMatrixFinal(int defaultCount, string path);
        IEnumerable<PDMigrationMatrixFinal> GetPDMigrationMatrixFinalBySearch(string searchParam, string path);
        IEnumerable<string> GetDistinctRatings();
        IEnumerable<PDMigrationMatrixFinal> GetPDMigrationMatrixFinalByRating(string ratingVal);


    }
}
