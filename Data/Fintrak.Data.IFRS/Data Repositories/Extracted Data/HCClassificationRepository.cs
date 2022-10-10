using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.Common.Services;
using Fintrak.Shared.IFRS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Fintrak.Data.IFRS
{
    [Export(typeof(IHCClassificationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HCClassificationRepository : DataRepositoryBase<HCClassification>, IHCClassificationRepository
    {
        protected override HCClassification AddEntity(IFRSContext entityContext, HCClassification entity)
        {
            return entityContext.Set<HCClassification>().Add(entity);
        }

        protected override HCClassification UpdateEntity(IFRSContext entityContext, HCClassification entity)
        {
            return (from e in entityContext.Set<HCClassification>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<HCClassification> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<HCClassification>()
                   select e;
        }

        protected override HCClassification GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<HCClassification>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<HCClassification> GetHCClassificationBySearch(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<HCClassification>()
                             where e.HC1.Contains(searchParam) || e.HC2.Contains(searchParam) || (searchParam.Contains(e.HC2) && searchParam.Contains(e.HC1))
                             select e);

                return query.ToArray();
            }
        }
    }
}
