using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IInvestmentMarginalPdRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class InvestmentMarginalPdRepository : DataRepositoryBase<InvestmentMarginalPd>, IInvestmentMarginalPdRepository
    {
        protected override InvestmentMarginalPd AddEntity(IFRSContext entityContext, InvestmentMarginalPd entity)
        {
            return entityContext.Set<InvestmentMarginalPd>().Add(entity);
        }

        protected override InvestmentMarginalPd UpdateEntity(IFRSContext entityContext, InvestmentMarginalPd entity)
        {
            return (from e in entityContext.Set<InvestmentMarginalPd>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<InvestmentMarginalPd> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<InvestmentMarginalPd>()
                   select e;
        }

        protected override InvestmentMarginalPd GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<InvestmentMarginalPd>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



        /////////////////////////////////////////// Methods from IRepo..

        public IEnumerable<InvestmentMarginalPd> GetInvestmentMarginalPdBySearch(string searchParam) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<InvestmentMarginalPd>()
                             where e.Refno == searchParam //orderby e.RefNo, e.datepmt                             
                             select e);
                return query.ToArray();
            }
        }

        public IEnumerable<InvestmentMarginalPd> GetInvestmentMarginalPds(int defaultCount) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<InvestmentMarginalPd>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e);
                return query.ToArray();
            }
        }


    }
}