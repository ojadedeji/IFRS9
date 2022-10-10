using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ICalendarRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CalendarRepository : DataRepositoryBase<Calendar>, ICalendarRepository
    {
        protected override Calendar AddEntity(IFRSContext entityContext, Calendar entity)
        {
            return entityContext.Set<Calendar>().Add(entity);
        }

        protected override Calendar UpdateEntity(IFRSContext entityContext, Calendar entity)
        {
            return (from e in entityContext.Set<Calendar>()
                    where e.CalId == entity.CalId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Calendar> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<Calendar>()
                   select e;
        }

        public IEnumerable<Calendar> GetCalendarException(DateTime rundate)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<Calendar>()
                             where e.ThisDate == rundate
                             select e);

                return query.ToArray();
            }
        }

        protected override Calendar GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Calendar>()
                         where e.CalId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}