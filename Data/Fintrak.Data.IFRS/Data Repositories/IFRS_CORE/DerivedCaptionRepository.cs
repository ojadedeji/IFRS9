using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IDerivedCaptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DerivedCaptionRepository : DataRepositoryBase<DerivedCaption>, IDerivedCaptionRepository
    {

        protected override DerivedCaption AddEntity(IFRSContext entityContext, DerivedCaption entity)
        {
            return entityContext.Set<DerivedCaption>().Add(entity);
        }

        protected override DerivedCaption UpdateEntity(IFRSContext entityContext, DerivedCaption entity)
        {
            return (from e in entityContext.Set<DerivedCaption>() 
                    where e.DerivedCaptionId == entity.DerivedCaptionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<DerivedCaption> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<DerivedCaption>()
                   select e;
        }

        protected override DerivedCaption GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<DerivedCaption>()
                         where e.DerivedCaptionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
 
    }
}
