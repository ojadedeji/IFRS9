using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IOpexManagementTreeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexManagementTreeRepository : DataRepositoryBase<OpexManagementTree>, IOpexManagementTreeRepository
    {

        protected override OpexManagementTree AddEntity(MPRContext entityContext, OpexManagementTree entity)
        {
            return entityContext.Set<OpexManagementTree>().Add(entity);
        }

        protected override OpexManagementTree UpdateEntity(MPRContext entityContext, OpexManagementTree entity)
        {
            return (from e in entityContext.Set<OpexManagementTree>()
                    where e.OpexMgtTreeId == entity.OpexMgtTreeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexManagementTree> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<OpexManagementTree>()
                   select e;
        }

        protected override OpexManagementTree GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexManagementTree>()
                         where e.OpexMgtTreeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<OpexManagementTreeInfo> GetOpexManagementTrees()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.OpexManagementTreeSet
                            join b in entityContext.CostCentreSet on a.CostCentreMISCode equals b.Code
                            join c in entityContext.TeamDefinitionSet on a.TeamDefinitionCode equals c.Code
                            join d in entityContext.TeamSet on a.TeamCode equals d.Code
                            join e in entityContext.TeamDefinitionSet on a.AccountOfficerDefinitionCode equals e.Code
                            join f in entityContext.TeamSet on a.AccountOfficerCode equals f.Code
                            join g in entityContext.SetUpSet on b.Year equals g.Year
                            where g.Year == c.Year && g.Year == d.Year && g.Year == b.Year && g.Year == e.Year && g.Year == f.Year
                            select new OpexManagementTreeInfo()
                            {
                                OpexManagementTree = a,
                                CostCentre = b,
                                TeamDefinition = c,
                                Team = d,
                                AccountOfficerDefinition = e,
                                AccountOfficerMis = f,
                                SetUp = g
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
