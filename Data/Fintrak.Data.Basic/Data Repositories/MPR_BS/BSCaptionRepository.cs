using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IBSCaptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BSCaptionRepository : DataRepositoryBase<BSCaption>, IBSCaptionRepository
    {

        protected override BSCaption AddEntity(BasicContext entityContext, BSCaption entity)
        {
            return entityContext.Set<BSCaption>().Add(entity);
        }

        protected override BSCaption UpdateEntity(BasicContext entityContext, BSCaption entity)
        {
            return (from e in entityContext.Set<BSCaption>() 
                    where e.CaptionId == entity.CaptionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BSCaption> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<BSCaption>()
                   select e;
        }

        protected override BSCaption GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BSCaption>()
                         where e.CaptionId == id
                         select e);

            var results = query.FirstOrDefault() ;

            return results;
        }

        public IEnumerable<BSCaptionInfo> GetBSCaptions()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.BSCaptionSet
                            join b in entityContext.BSCaptionSet on a.CaptionId equals b.ParentId into parents
                            from pt in parents.DefaultIfEmpty()
                            join c in entityContext.PLCaptionSet on a.PLCaption equals c.Code into cparents
                            from pc in cparents.DefaultIfEmpty()
                            select new BSCaptionInfo()
                            {
                                BSCaption = a,
                                Parent = pt,
                                PLCaption = pc
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
