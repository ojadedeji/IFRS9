using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IMPRTotalLineMakeUpRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MPRTotalLineMakeUpRepository : DataRepositoryBase<MPRTotalLineMakeUp>, IMPRTotalLineMakeUpRepository
    {

        protected override MPRTotalLineMakeUp AddEntity(MPRContext entityContext, MPRTotalLineMakeUp entity)
        {
            return entityContext.Set<MPRTotalLineMakeUp>().Add(entity);
        }

        protected override MPRTotalLineMakeUp UpdateEntity(MPRContext entityContext, MPRTotalLineMakeUp entity)
        {
            return (from e in entityContext.Set<MPRTotalLineMakeUp>() 
                    where e.TotalLineMakeUpId == entity.TotalLineMakeUpId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MPRTotalLineMakeUp> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<MPRTotalLineMakeUp>()
                   select e;
        }

        protected override MPRTotalLineMakeUp GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MPRTotalLineMakeUp>()
                         where e.TotalLineMakeUpId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<MPRTotalLineMakeUpInfo> GetMPRTotalLineMakeUps()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.MPRTotalLineMakeUpSet
                            join c in entityContext.PLCaptionSet on a.CaptionCode equals c.Code
                            select new MPRTotalLineMakeUpInfo()
                            {
                                MPRTotalLineMakeUp = a,
                                PLCaption =c
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
