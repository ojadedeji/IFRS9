using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IImpairmentResultOBERepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ImpairmentResultOBERepository : DataRepositoryBase<ImpairmentResultOBE>, IImpairmentResultOBERepository
    {
        protected override ImpairmentResultOBE AddEntity(IFRSContext entityContext, ImpairmentResultOBE entity)
        {
            return entityContext.Set<ImpairmentResultOBE>().Add(entity);
        }

        protected override ImpairmentResultOBE UpdateEntity(IFRSContext entityContext, ImpairmentResultOBE entity)
        {
            return (from e in entityContext.Set<ImpairmentResultOBE>()
                    where e.Impairment_Id == entity.Impairment_Id
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<ImpairmentResultOBE> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<ImpairmentResultOBE>().Take(200)
                   select e;
        }

        protected override ImpairmentResultOBE GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ImpairmentResultOBE>()
                         where e.Impairment_Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ImpairmentResultOBE> GetEntityBySearchParam(string SearchParam)
        {

            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<ImpairmentResultOBE>()
                             where e.CustomerName.Contains(SearchParam)
                             select e);

                return query.ToArray();
            }

        }
    }
}