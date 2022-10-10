using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface ICummulativePDDRepository : IDataRepository<CummulativePDD>
    {
        IEnumerable<CummulativePDD> GetAvailableCummulativePDD(int defaultCount, string path);
        IEnumerable<CummulativePDD> GetCummulativePDDBySearch(string searchParam, string path);
        IEnumerable<string> GetDistinctRatingAgency();
        IEnumerable<CummulativePDD> GetCummulativePDDByRatingAgency(string RatingAgencyVal);


    }
}
