using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIFRSTbillsRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IFRSTbillsRepository : DataRepositoryBase<IFRSTbills>, IIFRSTbillsRepository
    {
        protected override IFRSTbills AddEntity(IFRSContext entityContext, IFRSTbills entity)
        {
            return entityContext.Set<IFRSTbills>().Add(entity);
        }

        protected override IFRSTbills UpdateEntity(IFRSContext entityContext, IFRSTbills entity)
        {
            return (from e in entityContext.Set<IFRSTbills>()
                    where e.TbillId == entity.TbillId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IFRSTbills> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IFRSTbills>()
                   select e;
        }

        protected override IFRSTbills GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IFRSTbills>()
                         where e.TbillId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IFRSTbills> GetEntitiesByType(int Type)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IFRSTbills>()
                             where e.Flag == Type
                             select e);

                return query.ToArray();
            }
        }
    }
}