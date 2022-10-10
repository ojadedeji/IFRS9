using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IExpenseBasisRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExpenseBasisRepository : DataRepositoryBase<ExpenseBasis>, IExpenseBasisRepository
    {

        protected override ExpenseBasis AddEntity(MPRContext entityContext, ExpenseBasis entity)
        {
            return entityContext.Set<ExpenseBasis>().Add(entity);
        }

        protected override ExpenseBasis UpdateEntity(MPRContext entityContext, ExpenseBasis entity)
        {
            return (from e in entityContext.Set<ExpenseBasis>() 
                    where e.ExpenseBasisId == entity.ExpenseBasisId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ExpenseBasis> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<ExpenseBasis>()
                   select e;
        }

        protected override ExpenseBasis GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ExpenseBasis>()
                         where e.ExpenseBasisId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
