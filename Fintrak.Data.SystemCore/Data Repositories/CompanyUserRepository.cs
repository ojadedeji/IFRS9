using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.SystemCore.Entities;

namespace Fintrak.Data.SystemCore
{
    [Export(typeof(ICompanyUserRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CompanyUserRepository : DataRepositoryBase<CompanyUser>, ICompanyUserRepository
    {
        protected override CompanyUser AddEntity(SystemCoreContext entityContext, CompanyUser entity)
        {
            return entityContext.Set<CompanyUser>().Add(entity);
        }

        protected override CompanyUser UpdateEntity(SystemCoreContext entityContext, CompanyUser entity)
        {
            return (from e in entityContext.Set<CompanyUser>()
                    where e.CompanyUserId == entity.CompanyUserId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CompanyUser> GetEntities(SystemCoreContext entityContext)
        {
            return from e in entityContext.Set<CompanyUser>()
                   select e;
        }

        protected override CompanyUser GetEntity(SystemCoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CompanyUser>()
                         where e.CompanyUserId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<CompanyUser> GetCompanyUsers(string loginID, string companyCode)
        {
            using (SystemCoreContext entityContext = new SystemCoreContext())
            {
                var query = from a in entityContext.CompanyUserSet
                            join b in entityContext.UserSetupSet on a.UserId equals b.UserSetupId 
                            where b.LoginID == loginID && a.CompanyCode == companyCode
                                select a;

                            

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<CompanyUser> GetCompanyUsers(string loginID)
        {
            using (SystemCoreContext entityContext = new SystemCoreContext())
            {
                var query = from a in entityContext.CompanyUserSet
                            join b in entityContext.UserSetupSet on a.UserId equals b.UserSetupId 
                            where b.LoginID == loginID
                                select a;
                           

                return query.ToFullyLoaded();
            }
        }
    }
}

