using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILifeTimePDClassificationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LifeTimePDClassificationRepository : DataRepositoryBase<LifeTimePDClassification>, ILifeTimePDClassificationRepository
    {
        protected override LifeTimePDClassification AddEntity(IFRSContext entityContext, LifeTimePDClassification entity)
        {
            return entityContext.Set<LifeTimePDClassification>().Add(entity);
        }

        protected override LifeTimePDClassification UpdateEntity(IFRSContext entityContext, LifeTimePDClassification entity)
        {
            return (from e in entityContext.Set<LifeTimePDClassification>()
                    where e.LifeTimePDClassificationId == entity.LifeTimePDClassificationId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LifeTimePDClassification> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LifeTimePDClassification>()
                   select e;
        }

        protected override LifeTimePDClassification GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LifeTimePDClassification>()
                         where e.LifeTimePDClassificationId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}