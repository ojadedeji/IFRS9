using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IManagementTreeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ManagementTreeRepository : DataRepositoryBase<ManagementTree>, IManagementTreeRepository
    {

        protected override ManagementTree AddEntity(MPRContext entityContext, ManagementTree entity)
        {
            return entityContext.Set<ManagementTree>().Add(entity);
        }

        protected override ManagementTree UpdateEntity(MPRContext entityContext, ManagementTree entity)
        {
            return (from e in entityContext.Set<ManagementTree>()
                    where e.ManagementTreeId == entity.ManagementTreeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ManagementTree> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<ManagementTree>()
                   select e;
        }

        protected override ManagementTree GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ManagementTree>()
                         where e.ManagementTreeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ManagementTreeInfo> GetManagementTrees(string curYear)
        {

            MPRContext entityContext_Setup = new MPRContext();

            var _Setup = (from ty in entityContext_Setup.SetUpSet
                          select new MPRSetUp() { Period = ty.Period, Year = ty.Year }).FirstOrDefault();

            using (MPRContext entityContext = new MPRContext())
            {

                var query = from a in entityContext.ManagementTreeSet
                            join c in entityContext.TeamSet on a.TeamCode equals c.Code
                            join e in entityContext.TeamSet on a.AccountOfficerCode equals e.Code into parents
                            from pt in parents.DefaultIfEmpty()
                            join b in entityContext.TeamDefinitionSet on a.TeamDefinitionCode equals b.Code
                            join d in entityContext.TeamDefinitionSet on a.AccountOfficerDefinitionCode equals d.Code into jparents
                            from jt in jparents.DefaultIfEmpty()
                            where c.Year == curYear && pt.Year == curYear && b.Year == curYear && jt.Year == curYear && c.Period == _Setup.Period && pt.Period == _Setup.Period && b.Period == _Setup.Period && jt.Period == _Setup.Period
                            select new ManagementTreeInfo()
                            {
                                ManagementTree = a,
                                TeamDefinition = b,
                                Team = c,
                                AccountOfficerDefinition = jt,
                                AccountOfficer = pt
                            };

                return query.ToFullyLoaded();
            }
        }



    }
}
