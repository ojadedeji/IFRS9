using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Data.MPR
{
    [Export(typeof(ISetUpRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SetUpRepository : DataRepositoryBase<SetUp>, ISetUpRepository
    {

        protected override SetUp AddEntity(MPRContext entityContext, SetUp entity)
        {
            return entityContext.Set<SetUp>().Add(entity);
        }

        protected override SetUp UpdateEntity(MPRContext entityContext, SetUp entity)
        {
            return (from e in entityContext.Set<SetUp>()
                    where e.SetupId == entity.SetupId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SetUp> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<SetUp>()
                   select e;
        }

        protected override SetUp GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SetUp>()
                         where e.SetupId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MPRSetUpInfo> GetFirstMPRSetUps()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.SetUpSet
                            select new MPRSetUpInfo()
                            {
                                SetUp = a

                            };

                return query.ToFullyLoaded();
            }
        }

    }
}
