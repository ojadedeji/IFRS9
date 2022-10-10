using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(ITeamRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TeamRepository : DataRepositoryBase<Team>, ITeamRepository
    {

        protected override Team AddEntity(BasicContext entityContext, Team entity)
        {
            return entityContext.Set<Team>().Add(entity);
        }

        protected override Team UpdateEntity(BasicContext entityContext, Team entity)
        {
            return (from e in entityContext.Set<Team>()
                    where e.TeamId == entity.TeamId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Team> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<Team>()
                   select e;
        }

        protected override Team GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Team>()
                         where e.TeamId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<TeamInfo> GetTeams()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.TeamSet
                            join b in entityContext.TeamSet on a.ParentCode equals b.Code into parents
                            from pt in parents.DefaultIfEmpty()
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
