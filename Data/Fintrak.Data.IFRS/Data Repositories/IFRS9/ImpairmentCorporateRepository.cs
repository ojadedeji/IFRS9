using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IImpairmentCorporateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ImpairmentCorporateRepository : DataRepositoryBase<ImpairmentCorporate>, IImpairmentCorporateRepository
    {
        protected override ImpairmentCorporate AddEntity(IFRSContext entityContext, ImpairmentCorporate entity)
        {
            return entityContext.Set<ImpairmentCorporate>().Add(entity);
        }

        protected override ImpairmentCorporate UpdateEntity(IFRSContext entityContext, ImpairmentCorporate entity)
        {
            return (from e in entityContext.Set<ImpairmentCorporate>()
                    where e.Corporate_Id == entity.Corporate_Id
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<ImpairmentCorporate> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<ImpairmentCorporate>().Take(200)
                   select e;
        }

        protected override ImpairmentCorporate GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ImpairmentCorporate>()
                         where e.Corporate_Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}