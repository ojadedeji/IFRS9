using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IPiTPDComparismRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PiTPDComparismRepository : DataRepositoryBase<PiTPDComparism>, IPiTPDComparismRepository
    {
        protected override PiTPDComparism AddEntity(IFRSContext entityContext, PiTPDComparism entity)
        {
            return entityContext.Set<PiTPDComparism>().Add(entity);
        }

        protected override PiTPDComparism UpdateEntity(IFRSContext entityContext, PiTPDComparism entity)
        {
            return (from e in entityContext.Set<PiTPDComparism>()
                    where e.ComparismPDId == entity.ComparismPDId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<PiTPDComparism> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<PiTPDComparism>()
                   select e;
        }

        protected override PiTPDComparism GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PiTPDComparism>()
                         where e.ComparismPDId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}