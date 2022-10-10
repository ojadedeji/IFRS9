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
    [Export(typeof(ITeamPayClassificationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TeamPayClassificationRepository : DataRepositoryBase<TeamPayClassification>, ITeamPayClassificationRepository
    {

        protected override TeamPayClassification AddEntity(BudgetContext entityContext, TeamPayClassification entity)
        {
            return entityContext.Set<TeamPayClassification>().Add(entity);
        }

        protected override TeamPayClassification UpdateEntity(BudgetContext entityContext, TeamPayClassification entity)
        {
            return (from e in entityContext.Set<TeamPayClassification>() 
                    where e.TeamPayClassificationId == entity.TeamPayClassificationId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TeamPayClassification> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<TeamPayClassification>()
                   select e;
        }

        protected override TeamPayClassification GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TeamPayClassification>()
                         where e.TeamPayClassificationId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<TeamPayClassificationInfo> GetTeamPayClassifications(string year, string reviewCode, CenterTypeEnum center, string definitionCode, string misCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.TeamPayClassificationSet
                            join b in entityContext.PayClassificationSet on a.ClassificationCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.TeamDefinitionSet on a.DefinitionCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (a.Year == cpt.Year && a.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()
                            join d in entityContext.TeamSet on a.MisCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (a.Year == dpt.Year && a.ReviewCode == dpt.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode && a.Center == center && a.DefinitionCode == definitionCode && a.MisCode == misCode 
                            select new TeamPayClassificationInfo()
                            {
                                TeamPayClassification = a,
                                PayClassification = bp,
                                TeamDefinition = cp,
                                Team = dp
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<TeamPayClassificationInfo> GetTeamPayClassifications(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.TeamPayClassificationSet
                            join b in entityContext.PayClassificationSet on a.ClassificationCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.TeamDefinitionSet on a.DefinitionCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (a.Year == cpt.Year && a.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()
                            join d in entityContext.TeamSet on a.MisCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (a.Year == dpt.Year && a.ReviewCode == dpt.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode 
                            select new TeamPayClassificationInfo()
                            {
                                TeamPayClassification = a,
                                PayClassification = bp,
                                TeamDefinition = cp,
                                Team = dp
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
