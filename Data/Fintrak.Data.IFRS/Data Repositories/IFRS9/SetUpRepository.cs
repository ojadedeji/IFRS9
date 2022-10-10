using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ISetUpRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SetUpRepository : DataRepositoryBase<SetUp>, ISetUpRepository
    {
        protected override SetUp AddEntity(IFRSContext entityContext, SetUp entity)
        {
            return entityContext.Set<SetUp>().Add(entity);
        }

        protected override SetUp UpdateEntity(IFRSContext entityContext, SetUp entity)
        {
            return (from e in entityContext.Set<SetUp>()
                    where e.SetUpId == entity.SetUpId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SetUp> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<SetUp>()
                   select e;
        }

        protected override SetUp GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SetUp>()
                         where e.SetUpId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}