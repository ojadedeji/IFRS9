using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IAccountMISRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountMISRepository : DataRepositoryBase<AccountMIS>, IAccountMISRepository
    {

        protected override AccountMIS AddEntity(MPRContext entityContext, AccountMIS entity)
        {
            return entityContext.Set<AccountMIS>().Add(entity);
        }

        protected override AccountMIS UpdateEntity(MPRContext entityContext, AccountMIS entity)
        {
            return (from e in entityContext.Set<AccountMIS>()
                    where e.AccountMISId == entity.AccountMISId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<AccountMIS> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<AccountMIS>()
                   select e;
        }

        protected override AccountMIS GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<AccountMIS>()
                         where e.AccountMISId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<AccountMISInfo> GetAccountMISs()
        {
            MPRContext entityContext_Setup = new MPRContext();

            var _Setup = (from ty in entityContext_Setup.SetUpSet
                         select new MPRSetUp() { Period = ty.Period, Year = ty.Year }).FirstOrDefault();

            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.AccountMISSet
                            join c in entityContext.TeamSet on a.TeamCode equals c.Code into teams
                            from t in teams.Where(c => (a.Year == c.Year)).DefaultIfEmpty()
                            join e in entityContext.TeamSet on a.AccountOfficerCode equals e.Code into parents
                            from pt in parents.Where(c => (a.Year == c.Year)).DefaultIfEmpty()
                            join b in entityContext.TeamDefinitionSet on a.TeamDefinitionCode equals b.Code into definitions
                            from df in definitions.Where(c => (a.Year == c.Year)).DefaultIfEmpty()
                            join d in entityContext.TeamDefinitionSet on a.AccountOfficerDefinitionCode equals d.Code into jparents
                            from jt in jparents.Where(c => (a.Year == c.Year)).DefaultIfEmpty()
                            join f in entityContext.CustAccountSet on a.AccountNo equals f.AccountNo into fparents
                            from ft in fparents.DefaultIfEmpty()
                            where t.Period == _Setup.Period && pt.Period == _Setup.Period && df.Period == _Setup.Period && jt.Period == _Setup.Period && t.Year == _Setup.Year && pt.Year == _Setup.Year && df.Year == _Setup.Year && jt.Year == _Setup.Year
                            select new AccountMISInfo()
                            {
                                AccountMIS = a,
                                TeamDefinition = df,
                                Team = t,
                                AccountOfficerDefinition = jt,
                                AccountOfficer = pt,
                                CustAccount = ft
                            };

                return query.ToFullyLoaded();
            }
        }

    }
}
