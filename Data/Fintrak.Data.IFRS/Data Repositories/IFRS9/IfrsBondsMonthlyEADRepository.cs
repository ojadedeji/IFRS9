using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIfrsBondsMonthlyEADRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsBondsMonthlyEADRepository : DataRepositoryBase<IfrsBondsMonthlyEAD>, IIfrsBondsMonthlyEADRepository
    {
        protected override IfrsBondsMonthlyEAD AddEntity(IFRSContext entityContext, IfrsBondsMonthlyEAD entity)
        {
            return entityContext.Set<IfrsBondsMonthlyEAD>().Add(entity);
        }

        protected override IfrsBondsMonthlyEAD UpdateEntity(IFRSContext entityContext, IfrsBondsMonthlyEAD entity)
        {
            return (from e in entityContext.Set<IfrsBondsMonthlyEAD>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IfrsBondsMonthlyEAD> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsBondsMonthlyEAD>()
                   select e;
        }

        protected override IfrsBondsMonthlyEAD GetEntity(IFRSContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<IfrsBondsMonthlyEAD>()
                         where e.Id == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsBondsMonthlyEAD> GetIfrsBondMonthlyEADBySearch(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsBondsMonthlyEAD>()
                             where (e.AccountNo == searchParam || e.RefNo == searchParam)
                             orderby e.date_pmt
                             select e);

                return query.ToArray();
            }
        }



    }
}
