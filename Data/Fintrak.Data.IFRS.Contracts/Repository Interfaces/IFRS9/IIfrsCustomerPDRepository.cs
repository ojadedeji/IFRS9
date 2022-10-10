using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsCustomerPDRepository : IDataRepository<IfrsCustomerPD>
    {
        IEnumerable<IfrsCustomerPD> GetEntityByRating(string rating);
        IEnumerable<IfrsCustomerPD> GetAvailableIfrsCustomerPD(int defaultCount, string path);
    }
}
