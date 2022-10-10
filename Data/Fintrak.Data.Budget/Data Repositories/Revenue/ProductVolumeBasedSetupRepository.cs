using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Budget.Entities;
using Fintrak.Data.Budget.Contracts;
using Fintrak.Shared.Budget.Framework.Enums;

namespace Fintrak.Data.Budget
{
    [Export(typeof(IProductVolumeBasedSetupRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProductVolumeBasedSetupRepository : DataRepositoryBase<ProductVolumeBasedSetup>, IProductVolumeBasedSetupRepository
    {

        protected override ProductVolumeBasedSetup AddEntity(BudgetContext entityContext, ProductVolumeBasedSetup entity)
        {
            return entityContext.Set<ProductVolumeBasedSetup>().Add(entity);
        }

        protected override ProductVolumeBasedSetup UpdateEntity(BudgetContext entityContext, ProductVolumeBasedSetup entity)
        {
            return (from e in entityContext.Set<ProductVolumeBasedSetup>() 
                    where e.ProductVolumeBasedSetupId == entity.ProductVolumeBasedSetupId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ProductVolumeBasedSetup> GetEntities(BudgetContext entityContext)
        {
            return from e in entityContext.Set<ProductVolumeBasedSetup>()
                   select e;
        }

        protected override ProductVolumeBasedSetup GetEntity(BudgetContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ProductVolumeBasedSetup>()
                         where e.ProductVolumeBasedSetupId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProductVolumeBasedSetupInfo> GetProductVolumeBasedSetups(string year, string reviewCode)
        {
            using (BudgetContext entityContext = new BudgetContext())
            {
                var query = from a in entityContext.ProductVolumeBasedSetupSet
                            join b in entityContext.ProductSet on a.ProductCode equals b.Code into bparents
                            from bp in bparents.Where(bpt => (a.Year == bpt.Year && a.ReviewCode == bpt.ReviewCode)).DefaultIfEmpty()

                            join c in entityContext.ProductSet on a.ProductCode equals c.Code into cparents
                            from cp in cparents.Where(cpt => (a.Year == cpt.Year && a.ReviewCode == cpt.ReviewCode)).DefaultIfEmpty()

                            where a.Year == year && a.ReviewCode == reviewCode
                            select new ProductVolumeBasedSetupInfo()
                            {
                                ProductVolumeBasedSetup = a,
                                Product  = bp,
                                MakeUp = cp
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
