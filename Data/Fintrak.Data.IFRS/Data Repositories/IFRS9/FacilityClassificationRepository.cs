using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IFacilityClassificationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FacilityClassificationRepository : DataRepositoryBase<FacilityClassification>, IFacilityClassificationRepository
    {
        protected override FacilityClassification AddEntity(IFRSContext entityContext, FacilityClassification entity)
        {
            return entityContext.Set<FacilityClassification>().Add(entity);
        }

        protected override FacilityClassification UpdateEntity(IFRSContext entityContext, FacilityClassification entity)
        {
            return (from e in entityContext.Set<FacilityClassification>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FacilityClassification> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<FacilityClassification>().Take(200)
                   select e;
        }

        protected override FacilityClassification GetEntity(IFRSContext entityContext, int facId)
        {
            var query = (from e in entityContext.Set<FacilityClassification>()
                         where e.Id == facId
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<FacilityClassification> GetFacilityClassificationBySearch(string type,string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<FacilityClassification>()
                             where (e.Refno == searchParam || e.AccountNo == searchParam) && e.FacilityType == type
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<FacilityClassification> GetFacilityClassification(int defaultCount,string type)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<FacilityClassification>()
                             where e.FacilityType == type
                             select e).Take(defaultCount);

                return query.ToArray();
            }
        }


    }
}