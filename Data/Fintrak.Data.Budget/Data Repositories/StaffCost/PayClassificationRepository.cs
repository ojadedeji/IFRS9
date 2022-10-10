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
    [Export(typeof(IPayClassificationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PayClassificationRepository : DataRepositoryBase<PayClassification>, IPayClassificationRepository
    {

        protected override PayClassification AddEntity(BudgetContext entityContext, PayClassification entity)
        {
            return entityContext.Set<PayClassification>().Add(entity);
        }

        protected override PayClassification UpdateEntity(BudgetContext entityContext, PayClassification entity)
        {
            return (from e in entityContext.Set<PayClassification>() 
                    where e.PayClassificationId == entity.PayClassificationId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<PayClassification> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<PayClassification>()
                   select e;
        }

        protected override PayClassification GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PayClassification>()
                         where e.PayClassificationId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<PayClassification> GetPayClassifications(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.PayClassificationSet
                            where a.Year == year && a.ReviewCode == reviewCode
                                select a;

                return query.ToFullyLoaded();
            }
        }
      
    }
}
