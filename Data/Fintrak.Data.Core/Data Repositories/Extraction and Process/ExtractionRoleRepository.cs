using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;
using Fintrak.Data.Core.Contracts;

namespace Fintrak.Data.Core
{
    [Export(typeof(IExtractionRoleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExtractionRoleRepository : DataRepositoryBase<ExtractionRole>, IExtractionRoleRepository
    {
        protected override ExtractionRole AddEntity(CoreContext entityContext, ExtractionRole entity)
        {
            return entityContext.Set<ExtractionRole>().Add(entity);
        }

        protected override ExtractionRole UpdateEntity(CoreContext entityContext, ExtractionRole entity)
        {
            return (from e in entityContext.Set<ExtractionRole>()
                    where e.ExtractionRoleId == entity.ExtractionRoleId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ExtractionRole> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<ExtractionRole>()
                   select e;
        }

        protected override ExtractionRole GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ExtractionRole>()
                         where e.ExtractionRoleId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ExtractionRoleInfo> GetExtractionRoles()
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ExtractionRoleSet
                            join b in entityContext.ExtractionSet on a.ExtractionId equals b.ExtractionId
                           
                            select new ExtractionRoleInfo()
                            {
                                ExtractionRole = a,
                                Extraction=b,
                                
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<ExtractionRoleInfo> GetExtractionRoleByExtraction(int extractionId)
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ExtractionRoleSet
                            join b in entityContext.ExtractionSet on a.ExtractionId equals b.ExtractionId
                          
                            where a.ExtractionId == extractionId 
                            select new ExtractionRoleInfo()
                            {
                                ExtractionRole = a,
                                Extraction = b,
                              
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
