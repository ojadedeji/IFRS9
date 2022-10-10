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
    [Export(typeof(IProductInterestRateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProductInterestRateRepository : DataRepositoryBase<ProductInterestRate>, IProductInterestRateRepository
    {

        protected override ProductInterestRate AddEntity(BudgetContext entityContext, ProductInterestRate entity)
        {
            return entityContext.Set<ProductInterestRate>().Add(entity);
        }

        protected override ProductInterestRate UpdateEntity(BudgetContext entityContext, ProductInterestRate entity)
        {
            return (from e in entityContext.Set<ProductInterestRate>() 
                    where e.ProductInterestRateId == entity.ProductInterestRateId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ProductInterestRate> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<ProductInterestRate>()
                   select e;
        }

        protected override ProductInterestRate GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ProductInterestRate>()
                         where e.ProductInterestRateId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProductInterestRateInfo> GetProductInterestRates(string year, string reviewCode, string definitionCode, string misCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.ProductInterestRateSet
                            join d in entityContext.TeamSet on a.MisCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (a.Year == dpt.Year && a.ReviewCode == dpt.ReviewCode && a.DefintionCode == dpt.DefinitionCode)).DefaultIfEmpty()
                            join e in entityContext.TeamDefinitionSet on dp.DefinitionCode equals e.Code into eparents
                            from ep in eparents.Where(ept => (dp.Year == ept.Year && dp.ReviewCode == ept.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode && a.DefintionCode == definitionCode && a.MisCode == misCode  

                            select new ProductInterestRateInfo()
                            {
                                ProductInterestRate = a,
                                Team = dp,
                                TeamDefinition = ep
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<ProductInterestRateInfo> GetProductInterestRates(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.ProductInterestRateSet
                            join d in entityContext.TeamSet on a.MisCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (a.Year == dpt.Year && a.ReviewCode == dpt.ReviewCode && a.DefintionCode == dpt.DefinitionCode)).DefaultIfEmpty()
                            join e in entityContext.TeamDefinitionSet on dp.DefinitionCode equals e.Code into eparents
                            from ep in eparents.Where(ept => (dp.Year == ept.Year && dp.ReviewCode == ept.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode 

                            select new ProductInterestRateInfo()
                            {
                                ProductInterestRate = a,
                                Team = dp,
                                TeamDefinition = ep
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
