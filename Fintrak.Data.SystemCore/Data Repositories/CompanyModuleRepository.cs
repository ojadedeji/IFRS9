using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.SystemCore.Entities;
using Fintrak.Shared.SystemCore.Framework;

namespace Fintrak.Data.SystemCore
{
    [Export(typeof(ICompanyModuleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CompanyModuleRepository : DataRepositoryBase<CompanyModule>, ICompanyModuleRepository
    {

        protected override CompanyModule AddEntity(SystemCoreContext entityContext, CompanyModule entity)
        {
            return entityContext.Set<CompanyModule>().Add(entity);
        }

        protected override CompanyModule UpdateEntity(SystemCoreContext entityContext, CompanyModule entity)
        {
            return (from e in entityContext.Set<CompanyModule>()
                    where e.CompanyModuleId == entity.CompanyModuleId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CompanyModule> GetEntities(SystemCoreContext entityContext)
        {
            //return from e in entityContext.Set<CompanyModule>()
            //       select e;
            var query = (from e in entityContext.Set<CompanyModule>()
                         select e).Take(0);
            var results = query;
            return results;
        }

        protected override CompanyModule GetEntity(SystemCoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CompanyModule>()
                         where e.CompanyModuleId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<CompanyModuleInfo> GetCompanyModules()
        {
            using (SystemCoreContext entityContext = new SystemCoreContext())
            {
                var query = from a in entityContext.CompanyModuleSet
                            join b in entityContext.CompanySet on a.CompanyCode equals b.Code
                            join c in entityContext.ModuleSet on a.ModuleName equals c.Name
                            select new CompanyModuleInfo()
                            {
                                 CompanyModule = a,
                                 Company = b,
                                 Module  = c
                            };

                return query.ToFullyLoaded();
            }
        }



        public IEnumerable<CompanyModuleInfo> GetCompanyModuleByCompanyCode(string companyCode)
        {
            using (SystemCoreContext entityContext = new SystemCoreContext())
            {
                var query = from a in entityContext.CompanyModuleSet
                            join b in entityContext.CompanySet on a.CompanyCode equals b.Code
                            join c in entityContext.ModuleSet on a.ModuleName equals c.Name
                            where a.CompanyCode == companyCode
                            select new CompanyModuleInfo()
                            {
                                CompanyModule = a,
                                Company = b,
                                Module = c
                            };

                return query.ToFullyLoaded();
            }
        }

    }
}
