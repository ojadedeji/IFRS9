using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIndividualImpairmentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IndividualImpairmentRepository : DataRepositoryBase<IndividualImpairment>, IIndividualImpairmentRepository
    {

        protected override IndividualImpairment AddEntity(IFRSContext entityContext, IndividualImpairment entity)
        {
            return entityContext.Set<IndividualImpairment>().Add(entity);
        }

        protected override IndividualImpairment UpdateEntity(IFRSContext entityContext, IndividualImpairment entity)
        {
            return (from e in entityContext.Set<IndividualImpairment>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IndividualImpairment> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IndividualImpairment>()
                   select e;
        }

        protected override IndividualImpairment GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IndividualImpairment>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IndividualImpairment> GetIndividualImpairments()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = entityContext.IndividualImpairmentSet.AsQueryable().Where(r => r.Processed == false);

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<string> GetDistinctRefNos()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (entityContext.LoanDetailsSet.Select<RawLoanDetails, string>(r => r.RefNo)).Distinct();

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<RawLoanDetails> GetIndividualImpairments(string refNo)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = entityContext.LoanDetailsSet.AsQueryable().Where(r => r.RefNo == refNo);

                return query.ToFullyLoaded();
            }
        }


    }
}
