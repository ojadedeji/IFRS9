using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IScheduleTypeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ScheduleTypeRepository : DataRepositoryBase<ScheduleType>, IScheduleTypeRepository
    {

        protected override ScheduleType AddEntity(IFRSContext entityContext, ScheduleType entity)
        {
            return entityContext.Set<ScheduleType>().Add(entity);
        }

        protected override ScheduleType UpdateEntity(IFRSContext entityContext, ScheduleType entity)
        {
            return (from e in entityContext.Set<ScheduleType>() 
                    where e.ScheduleTypeId == entity.ScheduleTypeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ScheduleType> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<ScheduleType>()
                   select e;
        }

        protected override ScheduleType GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ScheduleType>()
                         where e.ScheduleTypeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
 
    }
}
