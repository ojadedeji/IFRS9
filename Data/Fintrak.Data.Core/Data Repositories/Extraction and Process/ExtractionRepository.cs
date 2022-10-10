using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Core.Entities;
using Fintrak.Data.Core.Contracts;

namespace Fintrak.Data.Core
{
    [Export(typeof(IExtractionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExtractionRepository : DataRepositoryBase<Extraction>, IExtractionRepository
    {
        protected override Extraction AddEntity(CoreContext entityContext, Extraction entity)
        {
            return entityContext.Set<Extraction>().Add(entity);
        }

        protected override Extraction UpdateEntity(CoreContext entityContext, Extraction entity)
        {
            return (from e in entityContext.Set<Extraction>()
                    where e.ExtractionId == entity.ExtractionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Extraction> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<Extraction>()
                   select e;
        }

        protected override Extraction GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Extraction>()
                         where e.ExtractionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ExtractionInfo> GetExtractions()
        {
            using (CoreContext entityContext = new CoreContext())
            {
                var query = from a in entityContext.ExtractionSet
                          
                            select new ExtractionInfo()
                            {
                                Extraction = a
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
