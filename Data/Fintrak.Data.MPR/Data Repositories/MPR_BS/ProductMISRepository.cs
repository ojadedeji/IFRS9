using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IProductMISRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProductMISRepository : DataRepositoryBase<ProductMIS>, IProductMISRepository
    {

        protected override ProductMIS AddEntity(MPRContext entityContext, ProductMIS entity)
        {
            return entityContext.Set<ProductMIS>().Add(entity);
        }

        protected override ProductMIS UpdateEntity(MPRContext entityContext, ProductMIS entity)
        {
            return (from e in entityContext.Set<ProductMIS>()
                    where e.ProductMISId == entity.ProductMISId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ProductMIS> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<ProductMIS>()
                   select e;
        }

        protected override ProductMIS GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ProductMIS>()
                         where e.ProductMISId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProductMISInfo> GetProductMIS(string year)
        {
            MPRContext entityContext_Setup = new MPRContext();

            var _Setup = (from ty in entityContext_Setup.SetUpSet
                          select new MPRSetUp() { Period = ty.Period, Year = ty.Year }).FirstOrDefault();

            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.ProductMISSet
                            join b in entityContext.ProductSet on a.ProductCode equals b.Code 
                            join c in entityContext.TeamSet on a.TeamCode equals c.Code into teams
                            from t in teams.Where(c => (a.Year == c.Year)).DefaultIfEmpty() 
                            join d in entityContext.TeamSet on a.AccountOfficerCode equals d.Code into parents
                            from pt in parents.Where(c => (a.Year == c.Year)).DefaultIfEmpty()
                            join h in entityContext.BSCaptionSet on a.CaptionCode equals h.CaptionCode
                            join i in entityContext.TeamDefinitionSet on a.TeamDefinitionCode equals i.Code into iparents
                            from it in iparents.Where(c => (a.Year == c.Year)).DefaultIfEmpty()
                            join j in entityContext.TeamDefinitionSet on a.AccountOfficerDefinitionCode equals j.Code into jparents
                            from jt in jparents.Where(c => (a.Year == c.Year)).DefaultIfEmpty()
                            where a.Year == year && t.Period == _Setup.Period && pt.Period == _Setup.Period && it.Period == _Setup.Period && jt.Period == _Setup.Period && t.Year == year && pt.Year == year && it.Year == year && jt.Year == year
                            select new ProductMISInfo()
                            {
                                ProductMIS = a,
                                Product = b,
                                TeamDefinition = it,
                                Team = t,
                                AccountOfficerDefinition = jt,
                                AccountOfficer = pt,
                                Caption = h
                            };

                return query.ToFullyLoaded();
            }
        }

    }
}
