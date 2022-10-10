using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIfrsSectorCCFRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsSectorCCFRepository : DataRepositoryBase<IfrsSectorCCF>, IIfrsSectorCCFRepository
    {
        protected override IfrsSectorCCF AddEntity(IFRSContext entityContext, IfrsSectorCCF entity)
        {
            return entityContext.Set<IfrsSectorCCF>().Add(entity);
        }

        protected override IfrsSectorCCF UpdateEntity(IFRSContext entityContext, IfrsSectorCCF entity)
        {
            return (from e in entityContext.Set<IfrsSectorCCF>()
                    where e.SectorId == entity.SectorId
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsSectorCCF> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsSectorCCF>()
                   select e;
        }

        protected override IfrsSectorCCF GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsSectorCCF>()
                         where e.SectorId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}