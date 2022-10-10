using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IGLExceptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GLExceptionRepository : DataRepositoryBase<GLException>, IGLExceptionRepository
    {

        protected override GLException AddEntity(MPRContext entityContext, GLException entity)
        {
            return entityContext.Set<GLException>().Add(entity);
        }

        protected override GLException UpdateEntity(MPRContext entityContext, GLException entity)
        {
            return (from e in entityContext.Set<GLException>() 
                    where e.GLExceptionId == entity.GLExceptionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<GLException> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<GLException>()
                   select e;
        }

        protected override GLException GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<GLException>()
                         where e.GLExceptionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<GLExceptionInfo> GetGLExceptions()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.GLExceptionSet
                            select new GLExceptionInfo()
                            {
                                GLException = a
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
