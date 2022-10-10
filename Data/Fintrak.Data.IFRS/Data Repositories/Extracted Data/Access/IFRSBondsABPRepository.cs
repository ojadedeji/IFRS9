using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIFRSBondsABPRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IFRSBondsABPRepository : DataRepositoryBase<IFRSBondsABP>, IIFRSBondsABPRepository
    {
        protected override IFRSBondsABP AddEntity(IFRSContext entityContext, IFRSBondsABP entity)
        {
            return entityContext.Set<IFRSBondsABP>().Add(entity);
        }

        protected override IFRSBondsABP UpdateEntity(IFRSContext entityContext, IFRSBondsABP entity)
        {
            return (from e in entityContext.Set<IFRSBondsABP>()
                    where e.BondId == entity.BondId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IFRSBondsABP> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IFRSBondsABP>()
                   select e;
        }

        protected override IFRSBondsABP GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IFRSBondsABP>()
                         where e.BondId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}