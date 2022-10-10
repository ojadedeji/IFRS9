using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ISectorMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SectorMappingRepository : DataRepositoryBase<SectorMapping>, ISectorMappingRepository
    {
        protected override SectorMapping AddEntity(IFRSContext entityContext, SectorMapping entity)
        {
            return entityContext.Set<SectorMapping>().Add(entity);
        }

        protected override SectorMapping UpdateEntity(IFRSContext entityContext, SectorMapping entity)
        {
            return (from e in entityContext.Set<SectorMapping>()
                    where e.SectorMapping_Id == entity.SectorMapping_Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SectorMapping> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<SectorMapping>()
                   select e;
        }

        protected override SectorMapping GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SectorMapping>()
                         where e.SectorMapping_Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        //public IEnumerable<SectorMapping> GetSectorMappingBySource(string Source)
        //{
        //    using (IFRSContext entityContext = new IFRSContext())
        //    {
        //        var query = (from e in entityContext.Set<SectorMapping>()
        //                     where e.Source == Source
        //                     select e);

        //        return query.ToArray();
        //    }
        //}
       
    }
}