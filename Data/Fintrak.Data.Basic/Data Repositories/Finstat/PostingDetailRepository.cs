using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IPostingDetailRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PostingDetailRepository : DataRepositoryBase<PostingDetail>, IPostingDetailRepository
    {

        protected override PostingDetail AddEntity(BasicContext entityContext, PostingDetail entity)
        {
            return entityContext.Set<PostingDetail>().Add(entity);
        }

        protected override PostingDetail UpdateEntity(BasicContext entityContext, PostingDetail entity)
        {
            return (from e in entityContext.Set<PostingDetail>() 
                    where e.PostingDetailId == entity.PostingDetailId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<PostingDetail> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<PostingDetail>()
                   select e;
        }

        protected override PostingDetail GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PostingDetail>()
                         where e.PostingDetailId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
