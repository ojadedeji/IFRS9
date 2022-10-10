using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IImpairmentResultRetailRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ImpairmentResultRetailRepository : DataRepositoryBase<ImpairmentResultRetail>, IImpairmentResultRetailRepository
    {
        protected override ImpairmentResultRetail AddEntity(IFRSContext entityContext, ImpairmentResultRetail entity)
        {
            return entityContext.Set<ImpairmentResultRetail>().Add(entity);
        }

        protected override ImpairmentResultRetail UpdateEntity(IFRSContext entityContext, ImpairmentResultRetail entity)
        {
            return (from e in entityContext.Set<ImpairmentResultRetail>()
                    where e.Impairment_Id == entity.Impairment_Id
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<ImpairmentResultRetail> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<ImpairmentResultRetail>().Take(200)
                   select e;
        }

        protected override ImpairmentResultRetail GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ImpairmentResultRetail>()
                         where e.Impairment_Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ImpairmentResultRetail> GetEntityBySearchParam(string SearchParam)
        {

            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<ImpairmentResultRetail>()
                             where e.CustomerName.Contains(SearchParam)
                             select e);

                return query.ToArray();
            }
        }

    }
}