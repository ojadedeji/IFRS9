using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIFRSBondsRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IFRSBondsRepository : DataRepositoryBase<IFRSBonds>, IIFRSBondsRepository
    {
        protected override IFRSBonds AddEntity(IFRSContext entityContext, IFRSBonds entity)
        {
            return entityContext.Set<IFRSBonds>().Add(entity);
        }

        protected override IFRSBonds UpdateEntity(IFRSContext entityContext, IFRSBonds entity)
        {
            return (from e in entityContext.Set<IFRSBonds>()
                    where e.BondId == entity.BondId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IFRSBonds> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IFRSBonds>()
                   select e;
        }

        protected override IFRSBonds GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IFRSBonds>()
                         where e.BondId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}