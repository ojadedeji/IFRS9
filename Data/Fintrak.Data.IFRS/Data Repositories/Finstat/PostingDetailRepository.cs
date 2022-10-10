using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IPostingDetailRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PostingDetailRepository : DataRepositoryBase<PostingDetail>, IPostingDetailRepository
    {

        protected override PostingDetail AddEntity(IFRSContext entityContext, PostingDetail entity)
        {
            return entityContext.Set<PostingDetail>().Add(entity);
        }

        protected override PostingDetail UpdateEntity(IFRSContext entityContext, PostingDetail entity)
        {
            return (from e in entityContext.Set<PostingDetail>() 
                    where e.PostingDetailId == entity.PostingDetailId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<PostingDetail> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<PostingDetail>()
                   select e;
        }

        protected override PostingDetail GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PostingDetail>()
                         where e.PostingDetailId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<PostingDetail> GetEntitiesByType(ReportType reportType)
            {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from e in entityContext.Set<PostingDetail>()
                            where e.ReportType == reportType
                            select e;

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<PostingDetailInfo> GetPostingDetailByType(ReportType reportType)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.PostingDetailSet
                            join b in entityContext.PostingDetailSet on a.ReportType equals b.ReportType into parents
                            from pt in parents.DefaultIfEmpty()
                            where a.ReportType == reportType 
                            select new PostingDetailInfo()
                            {
                                PostingDetail = a,
                                Parent = pt
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
