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
    [Export(typeof(ICapexCostRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CapexCostRepository : DataRepositoryBase<CapexCost>, ICapexCostRepository
    {

        protected override CapexCost AddEntity(BudgetContext entityContext, CapexCost entity)
        {
            return entityContext.Set<CapexCost>().Add(entity);
        }

        protected override CapexCost UpdateEntity(BudgetContext entityContext, CapexCost entity)
        {
            return (from e in entityContext.Set<CapexCost>() 
                    where e.CapexCostId == entity.CapexCostId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CapexCost> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<CapexCost>()
                   select e;
        }

        protected override CapexCost GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CapexCost>()
                         where e.CapexCostId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<CapexCostInfo> GetCapexCosts(string year, string reviewCode, string categoryCode, string definitionCode, string misCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.CapexCostSet
                            join b in entityContext.CapexItemSet on a.ItemCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.CapexCategorySet on bp.CategoryCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (bp.Year == cpt.Year && bp.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()
                            join d in entityContext.TeamSet on a.MisCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (a.Year == dpt.Year && a.ReviewCode == dpt.ReviewCode && a.DefintionCode == dpt.DefinitionCode)).DefaultIfEmpty()
                            join e in entityContext.TeamDefinitionSet on dp.DefinitionCode equals e.Code into eparents
                            from ep in eparents.Where(ept => (dp.Year == ept.Year && dp.ReviewCode == ept.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode && cp.Code == categoryCode && a.DefintionCode == definitionCode && a.MisCode == misCode  

                            select new CapexCostInfo()
                            {
                                CapexCost = a,
                                CapexItem = bp,
                                CapexCategory = cp,
                                Team = dp,
                                TeamDefinition = ep
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<CapexCostInfo> GetCapexCosts(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.CapexCostSet
                            join b in entityContext.CapexItemSet on a.ItemCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join c in entityContext.CapexCategorySet on bp.CategoryCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (bp.Year == cpt.Year && bp.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()
                            join d in entityContext.TeamSet on a.MisCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (a.Year == dpt.Year && a.ReviewCode == dpt.ReviewCode && a.DefintionCode == dpt.DefinitionCode)).DefaultIfEmpty()
                            join e in entityContext.TeamDefinitionSet on dp.DefinitionCode equals e.Code into eparents
                            from ep in eparents.Where(ept => (dp.Year == ept.Year && dp.ReviewCode == ept.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode 

                            select new CapexCostInfo()
                            {
                                CapexCost = a,
                                CapexItem = bp,
                                CapexCategory = cp,
                                Team = dp,
                                TeamDefinition = ep
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
