using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IGLReclassificationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GLReclassificationRepository : DataRepositoryBase<GLReclassification>, IGLReclassificationRepository
    {

        protected override GLReclassification AddEntity(BasicContext entityContext, GLReclassification entity)
        {
            return entityContext.Set<GLReclassification>().Add(entity);
        }

        protected override GLReclassification UpdateEntity(BasicContext entityContext, GLReclassification entity)
        {
            return (from e in entityContext.Set<GLReclassification>() 
                    where e.GLReclassificationId == entity.GLReclassificationId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<GLReclassification> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<GLReclassification>()
                   select e;
        }

        protected override GLReclassification GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<GLReclassification>()
                         where e.GLReclassificationId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<GLReclassificationInfo> GetGLReclassifications()
        {
            using (BasicContext entityContext = new BasicContext())
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
