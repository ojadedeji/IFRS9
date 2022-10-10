using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IPostingGLMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PostingGLMappingRepository : DataRepositoryBase<PostingGLMapping>, IPostingGLMappingRepository
    {
        protected override PostingGLMapping AddEntity(IFRSContext entityContext, PostingGLMapping entity)
        {
            return entityContext.Set<PostingGLMapping>().Add(entity);
        }

        protected override PostingGLMapping UpdateEntity(IFRSContext entityContext, PostingGLMapping entity)
        {
            return (from e in entityContext.Set<PostingGLMapping>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<PostingGLMapping> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<PostingGLMapping>()
                   select e;
        }

        protected override PostingGLMapping GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PostingGLMapping>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}