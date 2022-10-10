using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IAccountMISRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountMISRepository : DataRepositoryBase<AccountMIS>, IAccountMISRepository
    {

        protected override AccountMIS AddEntity(BasicContext entityContext, AccountMIS entity)
        {
            return entityContext.Set<AccountMIS>().Add(entity);
        }

        protected override AccountMIS UpdateEntity(BasicContext entityContext, AccountMIS entity)
        {
            return (from e in entityContext.Set<AccountMIS>() 
                    where e.AccountMISId == entity.AccountMISId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<AccountMIS> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<AccountMIS>()
                   select e;
        }

        protected override AccountMIS GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<AccountMIS>()
                         where e.AccountMISId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<AccountMISInfo> GetAccountMISs()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.AccountMISSet
                            join c in entityContext.TeamSet on a.TeamCode equals c.Code
                            join e in entityContext.TeamSet on a.AccountOfficerCode equals e.Code into parents
                            from pt in parents.DefaultIfEmpty()
                            join b in entityContext.TeamDefinitionSet on a.TeamDefinitionCode equals b.Code
                            join d in entityContext.TeamDefinitionSet on a.AccountOfficerDefinitionCode equals d.Code into jparents
                            from jt in jparents.DefaultIfEmpty()
                            join f in entityContext.CustAccountSet on a.AccountNo equals f.AccountNo into fparents
                            from ft in fparents.DefaultIfEmpty()
                            select new AccountMISInfo()
                            {
                                AccountMIS = a,
                                TeamDefinition = b,
                                Team = c,
                                AccountOfficerDefinition = jt,
                                AccountOfficer = pt,
                                CustAccount = ft
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
