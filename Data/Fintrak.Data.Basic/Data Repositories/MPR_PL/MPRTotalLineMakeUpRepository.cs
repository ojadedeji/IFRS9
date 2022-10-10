using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IMPRTotalLineMakeUpRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MPRTotalLineMakeUpRepository : DataRepositoryBase<MPRTotalLineMakeUp>, IMPRTotalLineMakeUpRepository
    {

        protected override MPRTotalLineMakeUp AddEntity(BasicContext entityContext, MPRTotalLineMakeUp entity)
        {
            return entityContext.Set<MPRTotalLineMakeUp>().Add(entity);
        }

        protected override MPRTotalLineMakeUp UpdateEntity(BasicContext entityContext, MPRTotalLineMakeUp entity)
        {
            return (from e in entityContext.Set<MPRTotalLineMakeUp>() 
                    where e.TotalLineMakeUpId == entity.TotalLineMakeUpId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MPRTotalLineMakeUp> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<MPRTotalLineMakeUp>()
                   select e;
        }

        protected override MPRTotalLineMakeUp GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MPRTotalLineMakeUp>()
                         where e.TotalLineMakeUpId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<MPRTotalLineMakeUpInfo> GetMPRTotalLineMakeUps()
        {
            using (BasicContext entityContext = new BasicContext())
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
