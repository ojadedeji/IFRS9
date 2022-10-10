using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.SystemCore.Entities;

namespace Fintrak.Data.SystemCore
{
    [Export(typeof(IDatabaseRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DatabaseRepository : DataRepositoryBase<Database>, IDatabaseRepository
    {
        protected override Database AddEntity(SystemCoreContext entityContext, Database entity)
        {
            return entityContext.Set<Database>().Add(entity);
        }

        protected override Database UpdateEntity(SystemCoreContext entityContext, Database entity)
        {
            return (from e in entityContext.Set<Database>()
                    where e.DatabaseId == entity.DatabaseId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Database> GetEntities(SystemCoreContext entityContext)
        {
            return from e in entityContext.Set<Database>()
                   select e;
        }

        protected override Database GetEntity(SystemCoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Database>()
                         where e.DatabaseId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<DatabaseInfo> GetDatabases()
        {
            using (SystemCoreContext entityContext = new SystemCoreContext())
            {
                var query = from a in entityContext.DatabaseSet
                            join b in entityContext.SolutionSet on a.SolutionId equals b.SolutionId into parents
                            from pt in parents.DefaultIfEmpty()
                            select new DatabaseInfo()
                            {
                                Database = a,
                                Solution = pt
                            };

                return query.ToFullyLoaded();
            }
        }

    }
}





   //IEnumerable<DatabaseInfo> GetDatabases()