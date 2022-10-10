using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Budget.Contracts
{
    public interface IOperationReviewRepository : IDataRepository<OperationReview>
    {
        IEnumerable<OperationReview> GetOperationReviews(string operationCode);
    }
}
