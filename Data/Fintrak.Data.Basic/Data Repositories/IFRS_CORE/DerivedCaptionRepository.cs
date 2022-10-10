using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IDerivedCaptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DerivedCaptionRepository : DataRepositoryBase<DerivedCaption>, IDerivedCaptionRepository
    {

        protected override DerivedCaption AddEntity(BasicContext entityContext, DerivedCaption entity)
        {
            return entityContext.Set<DerivedCaption>().Add(entity);
        }

        protected override DerivedCaption UpdateEntity(BasicContext entityContext, DerivedCaption entity)
        {
            return (from e in entityContext.Set<DerivedCaption>() 
                    where e.DerivedCaptionId == entity.DerivedCaptionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<DerivedCaption> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<DerivedCaption>()
                   select e;
        }

        protected override DerivedCaption GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<DerivedCaption>()
                         where e.DerivedCaptionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
 
    }
}
