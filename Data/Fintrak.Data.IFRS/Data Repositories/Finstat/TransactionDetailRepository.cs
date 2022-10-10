using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ITransactionDetailRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TransactionDetailRepository : DataRepositoryBase<TransactionDetail>, ITransactionDetailRepository
    {

        protected override TransactionDetail AddEntity(IFRSContext entityContext, TransactionDetail entity)
        {
            return entityContext.Set<TransactionDetail>().Add(entity);
        }

        protected override TransactionDetail UpdateEntity(IFRSContext entityContext, TransactionDetail entity)
        {
            return (from e in entityContext.Set<TransactionDetail>() 
                    where e.TransactionDetailId == entity.TransactionDetailId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TransactionDetail> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<TransactionDetail>()
                   select e;
        }

        protected override TransactionDetail GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TransactionDetail>()
                         where e.TransactionDetailId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
