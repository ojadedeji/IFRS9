using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface IRevenueRepository : IDataRepository<Revenue>
    {
        IEnumerable<Revenue> GetRevenues(DateTime runDate, int number);
        List<Revenue> GetRevenueBySearch(string searchType, string searchValue, int number);
        List<Revenue> GetAllRevenueBySearch(string searchType, string searchValue, int number, DateTime runDate, DateTime toDate);
        IEnumerable<Revenue> GetRunDate();
    }
}
