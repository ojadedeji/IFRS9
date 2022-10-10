using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;

namespace Fintrak.Data.Budget
{
    [Export(typeof(ITeamRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TeamRepository : DataRepositoryBase<Team>, ITeamRepository
    {

        protected override Team AddEntity(BudgetContext entityContext, Team entity)
        {
            return entityContext.Set<Team>().Add(entity);
        }

        protected override Team UpdateEntity(BudgetContext entityContext, Team entity)
        {
            return (from e in entityContext.Set<Team>() 
                    where e.TeamId == entity.TeamId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Team> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<Team>()
                   select e;
        }

        protected override Team GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Team>()
                         where e.TeamId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<TeamInfo> GetTeamInDefinition(string year, string reviewCode, string definitionCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.TeamSet
                            join b in entityContext.TeamDefinitionSet on a.DefinitionCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.TeamSet on a.ParentCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (a.Year == cpt.Year && a.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()
                            join d in entityContext.TeamDefinitionSet on cp.DefinitionCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (cp.Year == dpt.Year && cp.ReviewCode == dpt.ReviewCode)).DefaultIfEmpty()
                          
                            where a.Year == year && a.ReviewCode == reviewCode  && a.DefinitionCode == definitionCode 

                            select new TeamInfo()
                            {
                                Team = a,
                                TeamDefinition = bp,
                                Parent = cp,
                                ParentDefinition = dp
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<TeamInfo> GetTeamUnderDefinition(string year, string reviewCode, string definitionCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.TeamSet
                            join b in entityContext.TeamDefinitionSet on a.DefinitionCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.TeamSet on a.ParentCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (a.Year == cpt.Year && a.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()
                            join d in entityContext.TeamDefinitionSet on cp.DefinitionCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (cp.Year == dpt.Year && cp.ReviewCode == dpt.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode && cp.DefinitionCode == definitionCode

                            select new TeamInfo()
                            {
                                Team = a,
                                TeamDefinition = bp,
                                Parent = cp,
                                ParentDefinition = dp
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<TeamInfo> GetTeamUnderDefinition(string year, string reviewCode, string definitionCode,string misCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.TeamSet
                            join b in entityContext.TeamDefinitionSet on a.DefinitionCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.TeamSet on a.ParentCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (a.Year == cpt.Year && a.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()
                            join d in entityContext.TeamDefinitionSet on cp.DefinitionCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (cp.Year == dpt.Year && cp.ReviewCode == dpt.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode && cp.DefinitionCode == definitionCode && cp.Code == misCode 

                            select new TeamInfo()
                            {
                                Team = a,
                                TeamDefinition = bp,
                                Parent = cp,
                                ParentDefinition = dp
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<TeamInfo> GetTeams(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.TeamSet
                            join b in entityContext.TeamDefinitionSet on a.DefinitionCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.TeamSet on a.ParentCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (a.Year == cpt.Year && a.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()
                            join d in entityContext.TeamDefinitionSet on cp.DefinitionCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (cp.Year == dpt.Year && cp.ReviewCode == dpt.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode

                            select new TeamInfo()
                            {
                                Team = a,
                                TeamDefinition = bp,
                                Parent = cp,
                                ParentDefinition = dp
                            };

                return query.ToFullyLoaded();
            }
        }

      
    }
}
