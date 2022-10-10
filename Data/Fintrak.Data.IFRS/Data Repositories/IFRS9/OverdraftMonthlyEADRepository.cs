using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IOverdraftMonthlyEADRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class   OverdraftMonthlyEADRepository : DataRepositoryBase<OverdraftMonthlyEAD>, IOverdraftMonthlyEADRepository
    {
        protected override OverdraftMonthlyEAD AddEntity(IFRSContext entityContext, OverdraftMonthlyEAD entity)
        {
            return entityContext.Set<OverdraftMonthlyEAD>().Add(entity);
        }

        protected override OverdraftMonthlyEAD UpdateEntity(IFRSContext entityContext, OverdraftMonthlyEAD entity)
        {
            return (from e in entityContext.Set<OverdraftMonthlyEAD>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OverdraftMonthlyEAD> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<OverdraftMonthlyEAD>()
                   select e;
        }

        protected override OverdraftMonthlyEAD GetEntity(IFRSContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<OverdraftMonthlyEAD>()
                         where e.Id == Id orderby e.RefNo,e.date_pmt
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<OverdraftMonthlyEAD> GetOverdraftMonthlyEADBySearch(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<OverdraftMonthlyEAD>()
                             where (e.AccountNo == searchParam || e.RefNo == searchParam)
                                                          select e);

                return query.ToArray();
            }
        }

    }
}
