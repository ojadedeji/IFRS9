using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;
using Fintrak.Shared.Budget.Framework.Enums;

namespace Fintrak.Data.Budget
{
    [Export(typeof(IOperationReviewRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OperationReviewRepository : DataRepositoryBase<OperationReview>, IOperationReviewRepository
    {

        protected override OperationReview AddEntity(BudgetContext entityContext, OperationReview entity)
        {
            return entityContext.Set<OperationReview>().Add(entity);
        }

        protected override OperationReview UpdateEntity(BudgetContext entityContext, OperationReview entity)
        {
            return (from e in entityContext.Set<OperationReview>() 
                    where e.OperationReviewId == entity.OperationReviewId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OperationReview> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<OperationReview>()
                   select e;
        }

        protected override OperationReview GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OperationReview>()
                         where e.OperationReviewId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<OperationReview> GetOperationReviews(string operationCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.OperationReviewSet
                            where a.OperationCode == operationCode
                                select a;

                return query.ToFullyLoaded();
            }
        }
      
    }
}
