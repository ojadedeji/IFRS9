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
    [Export(typeof(IProductSharedRatioRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProductSharedRatioRepository : DataRepositoryBase<ProductSharedRatio>, IProductSharedRatioRepository
    {

        protected override ProductSharedRatio AddEntity(BudgetContext entityContext, ProductSharedRatio entity)
        {
            return entityContext.Set<ProductSharedRatio>().Add(entity);
        }

        protected override ProductSharedRatio UpdateEntity(BudgetContext entityContext, ProductSharedRatio entity)
        {
            return (from e in entityContext.Set<ProductSharedRatio>() 
                    where e.ProductSharedRatioId == entity.ProductSharedRatioId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ProductSharedRatio> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<ProductSharedRatio>()
                   select e;
        }

        protected override ProductSharedRatio GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ProductSharedRatio>()
                         where e.ProductSharedRatioId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProductSharedRatioInfo> GetProductSharedRatios(string year, string reviewCode, string definitionCode, string misCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.ProductSharedRatioSet
                           
                            join d in entityContext.TeamSet on a.MisCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (a.Year == dpt.Year && a.ReviewCode == dpt.ReviewCode && a.DefintionCode == dpt.DefinitionCode)).DefaultIfEmpty()
                            join e in entityContext.TeamDefinitionSet on dp.DefinitionCode equals e.Code into eparents
                            from ep in eparents.Where(ept => (dp.Year == ept.Year && dp.ReviewCode == ept.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode && a.DefintionCode == definitionCode && a.MisCode == misCode  

                            select new ProductSharedRatioInfo()
                            {
                                ProductSharedRatio = a,
                                Team = dp,
                                TeamDefinition = ep
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<ProductSharedRatioInfo> GetProductSharedRatios(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.ProductSharedRatioSet
                           
                            join d in entityContext.TeamSet on a.MisCode equals d.Code into dparents
                            from dp in dparents.Where(dpt => (a.Year == dpt.Year && a.ReviewCode == dpt.ReviewCode && a.DefintionCode == dpt.DefinitionCode)).DefaultIfEmpty()
                            join e in entityContext.TeamDefinitionSet on dp.DefinitionCode equals e.Code into eparents
                            from ep in eparents.Where(ept => (dp.Year == ept.Year && dp.ReviewCode == ept.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode 

                            select new ProductSharedRatioInfo()
                            {
                                ProductSharedRatio = a,
                                Team = dp,
                                TeamDefinition = ep
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
