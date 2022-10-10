using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;
using Fintrak.Shared.Basic.Framework;

namespace Fintrak.Data.Basic
{
    [Export(typeof(ISetUpRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SetUpRepository : DataRepositoryBase<SetUp>, ISetUpRepository
    {

        protected override SetUp AddEntity(BasicContext entityContext, SetUp entity)
        {
            return entityContext.Set<SetUp>().Add(entity);
        }

        protected override SetUp UpdateEntity(BasicContext entityContext, SetUp entity)
        {
            return (from e in entityContext.Set<SetUp>() 
                    where e.SetupId == entity.SetupId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SetUp> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<SetUp>()
                   select e;
        }

        protected override SetUp GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SetUp>()
                         where e.SetupId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
