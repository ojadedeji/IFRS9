using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IBorrowingsRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BorrowingsRepository : DataRepositoryBase<Borrowings>, IBorrowingsRepository
    {
        protected override Borrowings AddEntity(IFRSContext entityContext, Borrowings entity)
        {
            return entityContext.Set<Borrowings>().Add(entity);
        }

        protected override Borrowings UpdateEntity(IFRSContext entityContext, Borrowings entity)
        {
            return (from e in entityContext.Set<Borrowings>()
                    where e.BorrowingId == entity.BorrowingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Borrowings> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<Borrowings>()
                   select e;
        }

        protected override Borrowings GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Borrowings>()
                         where e.BorrowingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}