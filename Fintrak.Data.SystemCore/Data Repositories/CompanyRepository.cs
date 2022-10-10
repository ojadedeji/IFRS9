using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.SystemCore.Entities;

namespace Fintrak.Data.SystemCore
{
    [Export(typeof(ICompanyRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CompanyRepository : DataRepositoryBase<Company>,  ICompanyRepository
    {
        protected override Company AddEntity(SystemCoreContext entityContext, Company entity)
        {
            return entityContext.Set<Company>().Add(entity);
        }

        protected override Company UpdateEntity(SystemCoreContext entityContext, Company entity)
        {
            return (from e in entityContext.Set<Company>()
                    where e.CompanyId == entity.CompanyId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Company> GetEntities(SystemCoreContext entityContext)
        {
            return from e in entityContext.Set<Company>()
                   select e;
        }

        protected override Company GetEntity(SystemCoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Company>()
                         where e.CompanyId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
