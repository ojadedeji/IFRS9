using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IConsolidatedLoansRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ConsolidatedLoansRepository : DataRepositoryBase<ConsolidatedLoans>, IConsolidatedLoansRepository
    {
        protected override ConsolidatedLoans AddEntity(IFRSContext entityContext, ConsolidatedLoans entity)
        {
            return entityContext.Set<ConsolidatedLoans>().Add(entity);
        }

        protected override ConsolidatedLoans UpdateEntity(IFRSContext entityContext, ConsolidatedLoans entity)
        {
            return (from e in entityContext.Set<ConsolidatedLoans>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ConsolidatedLoans> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<ConsolidatedLoans>()
                   select e;
        }

        protected override ConsolidatedLoans GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ConsolidatedLoans>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



        /////////////////////////////////////////// Methods from IRepo..

        public IEnumerable<ConsolidatedLoans> GetConsolidatedLoansBySearch(string searchParam) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<ConsolidatedLoans>()
                             where e.AcctNo == searchParam  orderby e.RunDate //, e.datepmt                             
                             select e);
                return query.ToArray();
            }
        }

        public IEnumerable<ConsolidatedLoans> GetConsolidatedLoanss(int defaultCount) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<ConsolidatedLoans>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e);
                return query.ToArray();
            }
        }


    }
}