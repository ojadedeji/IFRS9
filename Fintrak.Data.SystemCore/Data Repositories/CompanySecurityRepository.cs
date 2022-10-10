using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.SystemCore.Entities;

namespace Fintrak.Data.SystemCore
{
    [Export(typeof(ICompanySecurityRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CompanySecurityRepository : DataRepositoryBase<CompanySecurity>, ICompanySecurityRepository
    {

        protected override CompanySecurity AddEntity(SystemCoreContext entityContext, CompanySecurity entity)
        {
            return entityContext.Set<CompanySecurity>().Add(entity);
        }

        protected override CompanySecurity UpdateEntity(SystemCoreContext entityContext, CompanySecurity entity)
        {
            return (from e in entityContext.Set<CompanySecurity>() 
                    where e.CompanySecurityId == entity.CompanySecurityId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CompanySecurity> GetEntities(SystemCoreContext entityContext)
        {
            return from e in entityContext.Set<CompanySecurity>()
                   select e;
        }

        protected override CompanySecurity GetEntity(SystemCoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CompanySecurity>()
                         where e.CompanySecurityId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
