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
    [Export(typeof(IMPRSetUpRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MPRSetUpRepository : DataRepositoryBase<MPRSetUp>, IMPRSetUpRepository
    {

        protected override MPRSetUp AddEntity(MPRContext entityContext, MPRSetUp entity)
        {
            return entityContext.Set<MPRSetUp>().Add(entity);
        }

        protected override MPRSetUp UpdateEntity(MPRContext entityContext, MPRSetUp entity)
        {
            return (from e in entityContext.Set<MPRSetUp>()
                    where e.SetupId == entity.SetupId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MPRSetUp> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<MPRSetUp>()
                   select e;
        }

        protected override MPRSetUp GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MPRSetUp>()
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
