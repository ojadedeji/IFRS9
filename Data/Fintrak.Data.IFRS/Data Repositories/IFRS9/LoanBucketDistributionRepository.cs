using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILoanBucketDistributionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoanBucketDistributionRepository : DataRepositoryBase<LoanBucketDistribution>, ILoanBucketDistributionRepository
    {
        protected override LoanBucketDistribution AddEntity(IFRSContext entityContext, LoanBucketDistribution entity)
        {
            return entityContext.Set<LoanBucketDistribution>().Add(entity);
        }

        protected override LoanBucketDistribution UpdateEntity(IFRSContext entityContext, LoanBucketDistribution entity)
        {
            return (from e in entityContext.Set<LoanBucketDistribution>()
                    where e.LoanBucketDistributionId == entity.LoanBucketDistributionId
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<LoanBucketDistribution> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LoanBucketDistribution>()
                   select e;
        }

        protected override LoanBucketDistribution GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LoanBucketDistribution>()
                         where e.LoanBucketDistributionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
        public IEnumerable<LoanBucketDistribution> GetLoanAssessments(string bucket)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.LoanBucketDistributionSet
                            where a.Bucket == bucket
                            select a;

                return query.ToFullyLoaded();
            }
        }
    }
}