using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ISubSectorRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SubSectorRepository : DataRepositoryBase<SubSector>, ISubSectorRepository
    {
        protected override SubSector AddEntity(IFRSContext entityContext, SubSector entity)
        {
            return entityContext.Set<SubSector>().Add(entity);
        }

        protected override SubSector UpdateEntity(IFRSContext entityContext, SubSector entity)
        {
            return (from e in entityContext.Set<SubSector>()
                    where e.SubSectorId == entity.SubSectorId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SubSector> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<SubSector>()
                   select e;
        }

        protected override SubSector GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SubSector>()
                         where e.SubSectorId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<SubSector> GetSubSectorBySource(string Source)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<SubSector>()
                             where e.Source == Source
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<SubSectorInfo> GetSubSectors(string Source)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.SectorSet
                            join b in entityContext.SubSectorSet on a.Code equals b.SectorCode
                            where b.Source == Source
                            select new SubSectorInfo()
                            {
                                Sector = a,
                                SubSector = b
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<SubSectorInfo> GetSubSectorsBySectorCode(string Source ,string sectorCode)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.SectorSet
                            join b in entityContext.SubSectorSet on a.Code equals b.SectorCode
                            where  b.Source == Source && b.SectorCode == sectorCode
                            select new SubSectorInfo()
                            {
                                Sector = a,
                                SubSector = b
                            };

                return query.ToFullyLoaded();
            }
        }

    }
}