using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IGLReclassificationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GLReclassificationRepository : DataRepositoryBase<GLReclassification>, IGLReclassificationRepository
    {

        protected override GLReclassification AddEntity(MPRContext entityContext, GLReclassification entity)
        {
            return entityContext.Set<GLReclassification>().Add(entity);
        }

        protected override GLReclassification UpdateEntity(MPRContext entityContext, GLReclassification entity)
        {
            return (from e in entityContext.Set<GLReclassification>() 
                    where e.GLReclassificationId == entity.GLReclassificationId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<GLReclassification> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<GLReclassification>()
                   select e;
        }

        protected override GLReclassification GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<GLReclassification>()
                         where e.GLReclassificationId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<GLReclassificationInfo> GetGLReclassifications()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.GLReclassificationSet
                            join c in entityContext.PLCaptionSet on a.CaptionCode equals c.Code 
                            select new GLReclassificationInfo()
                            {
                                GLReclassification = a,
                                PLCaption = c
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
