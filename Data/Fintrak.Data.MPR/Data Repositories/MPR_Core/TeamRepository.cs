using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(ITeamRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TeamRepository : DataRepositoryBase<Team>, ITeamRepository
    {

        protected override Team AddEntity(MPRContext entityContext, Team entity)
        {
            return entityContext.Set<Team>().Add(entity);
        }

        protected override Team UpdateEntity(MPRContext entityContext, Team entity)
        {
            return (from e in entityContext.Set<Team>()
                    where e.TeamId == entity.TeamId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Team> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<Team>()
                   select e;
        }

        protected override Team GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Team>()
                         where e.TeamId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<TeamInfo> GetTeams()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.TeamSet
                            join b in entityContext.TeamSet on a.ParentCode equals b.Code into parents
                            from pt in parents.Where(c=>(a.Year==c.Year)).DefaultIfEmpty()
                            select new TeamInfo()
                            {
                                Team = a,
                                Parent = pt
                            };

                return query.ToFullyLoaded();
            }
        }


        public IEnumerable<TeamInfo> GetTeamsBySearch(string SearchValue)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.TeamSet
                            join b in entityContext.TeamSet on a.ParentCode equals b.Code into parents
                            from pt in parents.Where(c => (a.Year == c.Year)).DefaultIfEmpty()
                            where a.Code.Contains(SearchValue) || a.Name.Contains(SearchValue)
                            select new TeamInfo()
                            {
                                Team = a,
                                Parent = pt
                            };

                return query.ToFullyLoaded();
            }
        }

    }
}
