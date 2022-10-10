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
    [Export(typeof(IProductGroupEntryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProductGroupEntryRepository : DataRepositoryBase<ProductGroupEntry>, IProductGroupEntryRepository
    {

        protected override ProductGroupEntry AddEntity(BudgetContext entityContext, ProductGroupEntry entity)
        {
            return entityContext.Set<ProductGroupEntry>().Add(entity);
        }

        protected override ProductGroupEntry UpdateEntity(BudgetContext entityContext, ProductGroupEntry entity)
        {
            return (from e in entityContext.Set<ProductGroupEntry>() 
                    where e.ProductGroupEntryId == entity.ProductGroupEntryId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ProductGroupEntry> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<ProductGroupEntry>()
                   select e;
        }

        protected override ProductGroupEntry GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ProductGroupEntry>()
                         where e.ProductGroupEntryId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProductGroupEntryInfo> GetProductGroupEntries(string year, string reviewCode, string definitionCode, string misCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.ProductGroupEntrySet
                            join b in entityContext.ProductGroupSet on a.GroupCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join d in entityContext.TeamSet on a.MisCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (a.Year == dpt.Year && a.ReviewCode == dpt.ReviewCode && a.DefintionCode == dpt.DefinitionCode)).DefaultIfEmpty()
                            join e in entityContext.TeamDefinitionSet on dp.DefinitionCode equals e.Code into eparents
                            from ep in eparents.Where(ept => (dp.Year == ept.Year && dp.ReviewCode == ept.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode && a.DefintionCode == definitionCode && a.MisCode == misCode  

                            select new ProductGroupEntryInfo()
                            {
                                ProductGroupEntry = a,
                                ProductGroup = bp,
                                Team = dp,
                                TeamDefinition = ep
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<ProductGroupEntryInfo> GetProductGroupEntries(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.ProductGroupEntrySet
                            join b in entityContext.ProductGroupSet on a.GroupCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()
                            join d in entityContext.TeamSet on a.MisCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (a.Year == dpt.Year && a.ReviewCode == dpt.ReviewCode && a.DefintionCode == dpt.DefinitionCode)).DefaultIfEmpty()
                            join e in entityContext.TeamDefinitionSet on dp.DefinitionCode equals e.Code into eparents
                            from ep in eparents.Where(ept => (dp.Year == ept.Year && dp.ReviewCode == ept.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode 

                            select new ProductGroupEntryInfo()
                            {
                                ProductGroupEntry = a,
                                ProductGroup = bp,
                                Team = dp,
                                TeamDefinition = ep
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
