using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IManagementTreeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ManagementTreeRepository : DataRepositoryBase<ManagementTree>, IManagementTreeRepository
    {

        protected override ManagementTree AddEntity(BasicContext entityContext, ManagementTree entity)
        {
            return entityContext.Set<ManagementTree>().Add(entity);
        }

        protected override ManagementTree UpdateEntity(BasicContext entityContext, ManagementTree entity)
        {
            return (from e in entityContext.Set<ManagementTree>() 
                    where e.ManagementTreeId == entity.ManagementTreeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ManagementTree> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<ManagementTree>()
                   select e;
        }

        protected override ManagementTree GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ManagementTree>()
                         where e.ManagementTreeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ManagementTreeInfo> GetManagementTrees()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.ManagementTreeSet
                            join c in entityContext.TeamSet on a.TeamCode equals c.Code  
                            join e in entityContext.TeamSet on a.AccountOfficerCode equals e.Code into parents
                            from pt in parents.DefaultIfEmpty()
                            join b in entityContext.TeamDefinitionSet on a.TeamDefinitionCode equals b.Code
                            join d in entityContext.TeamDefinitionSet on a.AccountOfficerDefinitionCode equals d.Code into jparents from jt in jparents.DefaultIfEmpty()

                            where c.Year == "2016" && pt.Year == "2016" && b.Year == "2016" && jt.Year == "2016"
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
