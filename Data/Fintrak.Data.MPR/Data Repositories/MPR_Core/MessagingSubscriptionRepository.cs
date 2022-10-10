using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IMessagingSubscriptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MessagingSubscriptionRepository : DataRepositoryBase<MessagingSubscription>, IMessagingSubscriptionRepository
    {

        protected override MessagingSubscription AddEntity(MPRContext entityContext, MessagingSubscription entity)
        {
            return entityContext.Set<MessagingSubscription>().Add(entity);
        }

        protected override MessagingSubscription UpdateEntity(MPRContext entityContext, MessagingSubscription entity )
        {
            return (from e in entityContext.Set<MessagingSubscription>()
                    where e.MessagingSubscriptionId == entity.MessagingSubscriptionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MessagingSubscription> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<MessagingSubscription>()
                   select e;
        }

        protected override MessagingSubscription GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MessagingSubscription>()
                         where e.MessagingSubscriptionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
