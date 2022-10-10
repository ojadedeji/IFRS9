using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IPLCaptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PLCaptionRepository : DataRepositoryBase<PLCaption>, IPLCaptionRepository
    {

        protected override PLCaption AddEntity(BasicContext entityContext, PLCaption entity)
        {
            return entityContext.Set<PLCaption>().Add(entity);
        }

        protected override PLCaption UpdateEntity(BasicContext entityContext, PLCaption entity)
        {
            return (from e in entityContext.Set<PLCaption>() 
                    where e.PLCaptionId == entity.PLCaptionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<PLCaption> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<PLCaption>()
                   select e;
        }

        protected override PLCaption GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PLCaption>()
                         where e.PLCaptionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<PLCaptionInfo> GetPLCaptions()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.PLCaptionSet
                            join c in entityContext.PLCaptionSet on a.Code equals c.ParentCode into parents
                            from pt in parents.DefaultIfEmpty()
                            select new PLCaptionInfo()
                            {
                                PLCaption = a,
                                Parent = pt
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
