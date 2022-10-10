using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IECLComparismRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ECLComparismRepository : DataRepositoryBase<ECLComparism>, IECLComparismRepository
    {
        protected override ECLComparism AddEntity(IFRSContext entityContext, ECLComparism entity)
        {
            return entityContext.Set<ECLComparism>().Add(entity);
        }

        protected override ECLComparism UpdateEntity(IFRSContext entityContext, ECLComparism entity)
        {
            return (from e in entityContext.Set<ECLComparism>()
                    where e.ECLComparismId == entity.ECLComparismId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ECLComparism> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<ECLComparism>()
                   select e;
        }

        protected override ECLComparism GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ECLComparism>()
                         where e.ECLComparismId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}