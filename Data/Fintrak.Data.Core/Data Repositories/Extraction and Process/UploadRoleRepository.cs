using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;
using Fintrak.Data.Core.Contracts;

namespace Fintrak.Data.Core
{
    [Export(typeof(IUploadRoleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UploadRoleRepository : DataRepositoryBase<UploadRole>, IUploadRoleRepository
    {
        protected override UploadRole AddEntity(CoreContext entityContext, UploadRole entity)
        {
            return entityContext.Set<UploadRole>().Add(entity);
        }

        protected override UploadRole UpdateEntity(CoreContext entityContext, UploadRole entity)
        {
            return (from e in entityContext.Set<UploadRole>()
                    where e.UploadRoleId == entity.UploadRoleId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<UploadRole> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<UploadRole>()
                   select e;
        }

        protected override UploadRole GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<UploadRole>()
                         where e.UploadRoleId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<UploadRoleInfo> GetUploadRoles()
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.UploadRoleSet
                            join b in entityContext.UploadSet on a.UploadId equals b.UploadId
                         
                            select new UploadRoleInfo()
                            {
                                UploadRole = a,
                                Upload=b,
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<UploadRoleInfo> GetUploadRoleByUpload(int uploadId)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.UploadRoleSet
                            join b in entityContext.UploadSet on a.UploadId equals b.UploadId
                           
                            where a.UploadId == uploadId 
                            select new UploadRoleInfo()
                            {
                                UploadRole = a,
                                Upload = b,

                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
