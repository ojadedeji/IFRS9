using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IProductMISRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProductMISRepository : DataRepositoryBase<ProductMIS>, IProductMISRepository
    {

        protected override ProductMIS AddEntity(BasicContext entityContext, ProductMIS entity)
        {
            return entityContext.Set<ProductMIS>().Add(entity);
        }

        protected override ProductMIS UpdateEntity(BasicContext entityContext, ProductMIS entity)
        {
            return (from e in entityContext.Set<ProductMIS>()
                    where e.ProductMISId == entity.ProductMISId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ProductMIS> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<ProductMIS>()
                   select e;
        }

        protected override ProductMIS GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ProductMIS>()
                         where e.ProductMISId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProductMISInfo> GetProductMIS()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.ProductMISSet
                            join b in entityContext.ProductSet on a.ProductCode equals b.Code
                            join c in entityContext.TeamSet on a.TeamCode equals c.Code 
                            join d in entityContext.TeamSet on a.AccountOfficerCode equals d.Code into parents
                            from pt in parents.DefaultIfEmpty()
                            join h in entityContext.BSCaptionSet on a.CaptionCode equals h.CaptionCode
                            join i in entityContext.TeamDefinitionSet on a.TeamDefinitionCode equals i.Code
                            join j in entityContext.TeamDefinitionSet on a.AccountOfficerDefinitionCode equals j.Code into jparents
                            from jt in jparents.DefaultIfEmpty()
                            select new ProductMISInfo()
                            {
                                ProductMIS = a,
                                Product = b,
                                TeamDefinition = i,
                                Team = c,
                                AccountOfficerDefinition = jt,
                                AccountOfficer = pt,
                                Caption = h
                            };

                return query.ToFullyLoaded();
            }
        }

    }
}
