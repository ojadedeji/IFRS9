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
    [Export(typeof(IProductPoolRateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProductPoolRateRepository : DataRepositoryBase<ProductPoolRate>, IProductPoolRateRepository
    {

        protected override ProductPoolRate AddEntity(BudgetContext entityContext, ProductPoolRate entity)
        {
            return entityContext.Set<ProductPoolRate>().Add(entity);
        }

        protected override ProductPoolRate UpdateEntity(BudgetContext entityContext, ProductPoolRate entity)
        {
            return (from e in entityContext.Set<ProductPoolRate>() 
                    where e.ProductPoolRateId == entity.ProductPoolRateId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ProductPoolRate> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<ProductPoolRate>()
                   select e;
        }

        protected override ProductPoolRate GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ProductPoolRate>()
                         where e.ProductPoolRateId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProductPoolRateInfo> GetProductPoolRates(string year, string reviewCode, string definitionCode, string misCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.ProductPoolRateSet
                            join d in entityContext.TeamSet on a.MisCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (a.Year == dpt.Year && a.ReviewCode == dpt.ReviewCode && a.DefintionCode == dpt.DefinitionCode)).DefaultIfEmpty()
                            join e in entityContext.TeamDefinitionSet on dp.DefinitionCode equals e.Code into eparents
                            from ep in eparents.Where(ept => (dp.Year == ept.Year && dp.ReviewCode == ept.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode && a.DefintionCode == definitionCode && a.MisCode == misCode  

                            select new ProductPoolRateInfo()
                            {
                                ProductPoolRate = a,
                                Team = dp,
                                TeamDefinition = ep
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<ProductPoolRateInfo> GetProductPoolRates(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.ProductPoolRateSet
                            join d in entityContext.TeamSet on a.MisCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (a.Year == dpt.Year && a.ReviewCode == dpt.ReviewCode && a.DefintionCode == dpt.DefinitionCode)).DefaultIfEmpty()
                            join e in entityContext.TeamDefinitionSet on dp.DefinitionCode equals e.Code into eparents
                            from ep in eparents.Where(ept => (dp.Year == ept.Year && dp.ReviewCode == ept.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode 

                            select new ProductPoolRateInfo()
                            {
                                ProductPoolRate = a,
                                Team = dp,
                                TeamDefinition = ep
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
