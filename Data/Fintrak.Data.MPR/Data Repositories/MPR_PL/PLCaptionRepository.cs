using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IPLCaptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PLCaptionRepository : DataRepositoryBase<PLCaption>, IPLCaptionRepository
    {

        protected override PLCaption AddEntity(MPRContext entityContext, PLCaption entity)
        {
            return entityContext.Set<PLCaption>().Add(entity);
        }

        protected override PLCaption UpdateEntity(MPRContext entityContext, PLCaption entity)
        {
            return (from e in entityContext.Set<PLCaption>() 
                    where e.PLCaptionId == entity.PLCaptionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<PLCaption> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<PLCaption>()
                   select e;
        }

        protected override PLCaption GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PLCaption>()
                         where e.PLCaptionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<PLCaptionInfo> GetPLCaptions()
        {
            using (MPRContext entityContext = new MPRContext())
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
