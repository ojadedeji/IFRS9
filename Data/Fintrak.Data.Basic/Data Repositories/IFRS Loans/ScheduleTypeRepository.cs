using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IScheduleTypeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ScheduleTypeRepository : DataRepositoryBase<ScheduleType>, IScheduleTypeRepository
    {

        protected override ScheduleType AddEntity(BasicContext entityContext, ScheduleType entity)
        {
            return entityContext.Set<ScheduleType>().Add(entity);
        }

        protected override ScheduleType UpdateEntity(BasicContext entityContext, ScheduleType entity)
        {
            return (from e in entityContext.Set<ScheduleType>() 
                    where e.ScheduleTypeId == entity.ScheduleTypeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ScheduleType> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<ScheduleType>()
                   select e;
        }

        protected override ScheduleType GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ScheduleType>()
                         where e.ScheduleTypeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
 
    }
}
