using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IECLInputRetailRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ECLInputRetailRepository : DataRepositoryBase<ECLInputRetail>, IECLInputRetailRepository
    {
        protected override ECLInputRetail AddEntity(IFRSContext entityContext, ECLInputRetail entity)
        {
            return entityContext.Set<ECLInputRetail>().Add(entity);
        }

        protected override ECLInputRetail UpdateEntity(IFRSContext entityContext, ECLInputRetail entity)
        {
            return (from e in entityContext.Set<ECLInputRetail>()
                    where e.EclInputRetailId == entity.EclInputRetailId
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<ECLInputRetail> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<ECLInputRetail>()
                   select e;
        }

        protected override ECLInputRetail GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ECLInputRetail>()
                         where e.EclInputRetailId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
        public IEnumerable<ECLInputRetail> GetAllEclInputRetailsByRefno(string refNo)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<ECLInputRetail>()
                             where e.account_number == refNo
                             select e);

                return query.ToArray();
            }
        }

    }
}