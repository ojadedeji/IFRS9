using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ITransitionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TransitionRepository : DataRepositoryBase<Transition>, ITransitionRepository
    {
        protected override Transition AddEntity(IFRSContext entityContext, Transition entity)
        {
            return entityContext.Set<Transition>().Add(entity);
        }

        protected override Transition UpdateEntity(IFRSContext entityContext, Transition entity)
        {
            return (from e in entityContext.Set<Transition>()
                    where e.TransitionId == entity.TransitionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Transition> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<Transition>()
                   select e;
        }

        protected override Transition GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Transition>()
                         where e.TransitionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}