using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IGLExceptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GLExceptionRepository : DataRepositoryBase<GLException>, IGLExceptionRepository
    {

        protected override GLException AddEntity(BasicContext entityContext, GLException entity)
        {
            return entityContext.Set<GLException>().Add(entity);
        }

        protected override GLException UpdateEntity(BasicContext entityContext, GLException entity)
        {
            return (from e in entityContext.Set<GLException>() 
                    where e.GLExceptionId == entity.GLExceptionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<GLException> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<GLException>()
                   select e;
        }

        protected override GLException GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<GLException>()
                         where e.GLExceptionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<GLExceptionInfo> GetGLExceptions()
        {
            using (BasicContext entityContext = new BasicContext())
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
