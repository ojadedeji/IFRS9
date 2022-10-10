using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IMarginalOutputRepository : IDataRepository<MarginalOutput>
    {
        IEnumerable<MarginalOutput> GetAvailableMarginalOutput(int defaultCount, string path);
        IEnumerable<MarginalOutput> GetMarginalOutputBySearch(string searchParam, string path);
        IEnumerable<string> GetDistinctCreditRating();
        IEnumerable<MarginalOutput> GetMarginalOutputByCreditRating(string CreditRatingVal);


    }
}
