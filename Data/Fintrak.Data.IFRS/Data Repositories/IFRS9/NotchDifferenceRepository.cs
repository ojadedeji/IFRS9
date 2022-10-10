using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(INotchDifferenceRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NotchDifferenceRepository : DataRepositoryBase<NotchDifference>, INotchDifferenceRepository
    {
        protected override NotchDifference AddEntity(IFRSContext entityContext, NotchDifference entity)
        {
            return entityContext.Set<NotchDifference>().Add(entity);
        }

        protected override NotchDifference UpdateEntity(IFRSContext entityContext, NotchDifference entity)
        {
            return (from e in entityContext.Set<NotchDifference>()
                    where e.NotchDifferenceId == entity.NotchDifferenceId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<NotchDifference> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<NotchDifference>()
                   select e;
        }

        protected override NotchDifference GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<NotchDifference>()
                         where e.NotchDifferenceId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}