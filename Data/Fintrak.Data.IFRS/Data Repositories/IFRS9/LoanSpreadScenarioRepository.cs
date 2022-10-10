using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILoanSpreadScenarioRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoanSpreadScenarioRepository : DataRepositoryBase<LoanSpreadScenario>, ILoanSpreadScenarioRepository
    {
        protected override LoanSpreadScenario AddEntity(IFRSContext entityContext, LoanSpreadScenario entity)
        {
            return entityContext.Set<LoanSpreadScenario>().Add(entity);
        }

        protected override LoanSpreadScenario UpdateEntity(IFRSContext entityContext, LoanSpreadScenario entity)
        {
            return (from e in entityContext.Set<LoanSpreadScenario>()
                    where e.LoanSpreadScenarioId == entity.LoanSpreadScenarioId
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<LoanSpreadScenario> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LoanSpreadScenario>()
                   select e;
        }

        protected override LoanSpreadScenario GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LoanSpreadScenario>()
                         where e.LoanSpreadScenarioId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
        //public IEnumerable<LoanSpreadScenario> GetLoanAssessments(string bucket)
        //{
        //    using (IFRSContext entityContext = new IFRSContext())
        //    {
        //        var query = from a in entityContext.LoanSpreadScenarioSet
        //                    where a.Bucket == bucket
        //                    select a;

        //        return query.ToFullyLoaded();
        //    }
        //}
    }
}