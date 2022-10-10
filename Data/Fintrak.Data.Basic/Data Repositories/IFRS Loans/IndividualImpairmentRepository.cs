using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IIndividualImpairmentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IndividualImpairmentRepository : DataRepositoryBase<IndividualImpairment>, IIndividualImpairmentRepository
    {

        protected override IndividualImpairment AddEntity(BasicContext entityContext, IndividualImpairment entity)
        {
            return entityContext.Set<IndividualImpairment>().Add(entity);
        }

        protected override IndividualImpairment UpdateEntity(BasicContext entityContext, IndividualImpairment entity)
        {
            return (from e in entityContext.Set<IndividualImpairment>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IndividualImpairment> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<IndividualImpairment>()
                   select e;
        }

        protected override IndividualImpairment GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IndividualImpairment>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IndividualImpairment> GetIndividualImpairments()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = entityContext.IndividualImpairmentSet.AsQueryable().Where(r => r.Processed == false);

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<string> GetDistinctRefNos()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = (entityContext.LoanDetailsSet.Select<LoanDetails, string>(r => r.RefNo)).Distinct();

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<LoanDetails> GetIndividualImpairments(string refNo)
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = entityContext.LoanDetailsSet.AsQueryable().Where(r => r.RefNo == refNo);

                return query.ToFullyLoaded();
            }
        }


    }
}
