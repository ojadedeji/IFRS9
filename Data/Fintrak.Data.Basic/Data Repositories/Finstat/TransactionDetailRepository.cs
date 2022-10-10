using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(ITransactionDetailRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TransactionDetailRepository : DataRepositoryBase<TransactionDetail>, ITransactionDetailRepository
    {

        protected override TransactionDetail AddEntity(BasicContext entityContext, TransactionDetail entity)
        {
            return entityContext.Set<TransactionDetail>().Add(entity);
        }

        protected override TransactionDetail UpdateEntity(BasicContext entityContext, TransactionDetail entity)
        {
            return (from e in entityContext.Set<TransactionDetail>() 
                    where e.TransactionDetailId == entity.TransactionDetailId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TransactionDetail> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<TransactionDetail>()
                   select e;
        }

        protected override TransactionDetail GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TransactionDetail>()
                         where e.TransactionDetailId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
