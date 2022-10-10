using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(INseIndexRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NseIndexRepository : DataRepositoryBase<NseIndex>, INseIndexRepository
    {
        protected override NseIndex AddEntity(IFRSContext entityContext, NseIndex entity)
        {
            return entityContext.Set<NseIndex>().Add(entity);
        }

        protected override NseIndex UpdateEntity(IFRSContext entityContext, NseIndex entity)
        {
            return (from e in entityContext.Set<NseIndex>()
                    where e.NseIndexId == entity.NseIndexId
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<NseIndex> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<NseIndex>()
                   select e;
        }

        protected override NseIndex GetEntity(IFRSContext entityContext, int NseIndexId)
        {
            var query = (from e in entityContext.Set<NseIndex>()
                         where e.NseIndexId == NseIndexId
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProbabilityWeight> GetProbabilityWeights()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<ProbabilityWeight>()
                             select e);

                return query.ToArray();
            }
        }

        //public IEnumerable<NseIndex> GetNseIndexByRefNo(string RefNo)
        //{
        //    using(IFRSContext entityContext = new IFRSContext())
        //    {
        //    var query = (from e in entityContext.Set<NseIndex>()
        //                 where e.RefNo.Contains(RefNo)
        //                 select e);

        //    return query.ToArray();
        //    }
        //}

    }
}