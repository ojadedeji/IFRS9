using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IRatioCaptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RatioCaptionRepository : DataRepositoryBase<RatioCaption>, IRatioCaptionRepository
    {
        protected override RatioCaption AddEntity(IFRSContext entityContext, RatioCaption entity)
        {
            return entityContext.Set<RatioCaption>().Add(entity);
        }

        protected override RatioCaption UpdateEntity(IFRSContext entityContext, RatioCaption entity)
        {
            return (from e in entityContext.Set<RatioCaption>()
                    where e.RatioCaptionID == entity.RatioCaptionID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<RatioCaption> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<RatioCaption>()
                   select e;
        }

        protected override RatioCaption GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<RatioCaption>()
                         where e.RatioCaptionID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}