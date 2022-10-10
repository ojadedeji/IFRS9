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
    [Export(typeof(IProductVolumeBasedRateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProductVolumeBasedRateRepository : DataRepositoryBase<ProductVolumeBasedRate>, IProductVolumeBasedRateRepository
    {

        protected override ProductVolumeBasedRate AddEntity(BudgetContext entityContext, ProductVolumeBasedRate entity)
        {
            return entityContext.Set<ProductVolumeBasedRate>().Add(entity);
        }

        protected override ProductVolumeBasedRate UpdateEntity(BudgetContext entityContext, ProductVolumeBasedRate entity)
        {
            return (from e in entityContext.Set<ProductVolumeBasedRate>() 
                    where e.ProductVolumeBasedRateId == entity.ProductVolumeBasedRateId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ProductVolumeBasedRate> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<ProductVolumeBasedRate>()
                   select e;
        }

        protected override ProductVolumeBasedRate GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ProductVolumeBasedRate>()
                         where e.ProductVolumeBasedRateId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProductVolumeBasedRateInfo> GetProductVolumeBasedRates(string year, string reviewCode, string definitionCode, string misCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.ProductVolumeBasedRateSet
                           
                            join d in entityContext.TeamSet on a.MisCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (a.Year == dpt.Year && a.ReviewCode == dpt.ReviewCode && a.DefintionCode == dpt.DefinitionCode)).DefaultIfEmpty()
                            join e in entityContext.TeamDefinitionSet on dp.DefinitionCode equals e.Code into eparents
                            from ep in eparents.Where(ept => (dp.Year == ept.Year && dp.ReviewCode == ept.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode && a.DefintionCode == definitionCode && a.MisCode == misCode  

                            select new ProductVolumeBasedRateInfo()
                            {
                                ProductVolumeBasedRate = a,
                                Team = dp,
                                TeamDefinition = ep
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<ProductVolumeBasedRateInfo> GetProductVolumeBasedRates(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.ProductVolumeBasedRateSet
                           
                            join d in entityContext.TeamSet on a.MisCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (a.Year == dpt.Year && a.ReviewCode == dpt.ReviewCode && a.DefintionCode == dpt.DefinitionCode)).DefaultIfEmpty()
                            join e in entityContext.TeamDefinitionSet on dp.DefinitionCode equals e.Code into eparents
                            from ep in eparents.Where(ept => (dp.Year == ept.Year && dp.ReviewCode == ept.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode 

                            select new ProductVolumeBasedRateInfo()
                            {
                                ProductVolumeBasedRate = a,
                                Team = dp,
                                TeamDefinition = ep
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
