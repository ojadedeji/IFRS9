using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ISectorRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SectorRepository : DataRepositoryBase<Sector>, ISectorRepository
    {
        protected override Sector AddEntity(IFRSContext entityContext, Sector entity)
        {
            return entityContext.Set<Sector>().Add(entity);
        }

        protected override Sector UpdateEntity(IFRSContext entityContext, Sector entity)
        {
            return (from e in entityContext.Set<Sector>()
                    where e.SectorId == entity.SectorId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Sector> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<Sector>()
                   select e;
        }

        protected override Sector GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Sector>()
                         where e.SectorId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<Sector> GetSectorBySource(string Source)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<Sector>()
                             where e.Source == Source
                             select e);

                return query.ToArray();
            }
        }
       
    }
}