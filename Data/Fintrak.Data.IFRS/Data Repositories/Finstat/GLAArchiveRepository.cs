using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IGLAArchiveRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GLAArchiveRepository : DataRepositoryBase<GLAArchive>, IGLAArchiveRepository
    {
        protected override GLAArchive AddEntity(IFRSContext entityContext, GLAArchive entity)
        {
            return entityContext.Set<GLAArchive>().Add(entity);
        }

        protected override GLAArchive UpdateEntity(IFRSContext entityContext, GLAArchive entity)
        {
            return (from e in entityContext.Set<GLAArchive>()
                    where e.GLAdjustmentId == entity.GLAdjustmentId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<GLAArchive> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<GLAArchive>()
                   select e;
        }

        protected override GLAArchive GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<GLAArchive>()
                         where e.GLAdjustmentId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<GLAArchive> GetGLAArchiveByRundate(DateTime rundate)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<GLAArchive>()
                             where e.RunDate == rundate
                             select e);

                return query.ToArray();
            }
        }
        
       
    }
}