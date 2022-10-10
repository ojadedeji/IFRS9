using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.SqlClient;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;
using Fintrak.Data.Core.Contracts;
using System.Data.Entity.Infrastructure;
using Fintrak.Shared.Common.Data;

namespace Fintrak.Data.Core
{
    [Export(typeof(IUploadRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UploadRepository : DataRepositoryBase<Upload>, IUploadRepository
    {
        protected override Upload AddEntity(CoreContext entityContext, Upload entity)
        {
            return entityContext.Set<Upload>().Add(entity);
        }

        protected override Upload UpdateEntity(CoreContext entityContext, Upload entity)
        {
            return (from e in entityContext.Set<Upload>()
                    where e.UploadId == entity.UploadId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Upload> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<Upload>()
                   select e;
        }

        protected override Upload GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Upload>()
                         where e.UploadId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<UploadInfo> GetUploads()
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.UploadSet
                           
                            select new UploadInfo()
                            {
                                Upload = a,
                               
                            };

                return query.ToFullyLoaded();
            }
        }

        public void UploadData(string actionName,SqlParameter [] parameters )
        {
            SqlDataManager.RunProcedure(CoreContext.GetDataConnection(), actionName, parameters);
        }
    }
}
