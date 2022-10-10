using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IMPRPLDerivedCaptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MPRPLDerivedCaptionRepository : DataRepositoryBase<MPRPLDerivedCaption>, IMPRPLDerivedCaptionRepository
    {

        protected override MPRPLDerivedCaption AddEntity(MPRContext entityContext, MPRPLDerivedCaption entity)
        {
            return entityContext.Set<MPRPLDerivedCaption>().Add(entity);
        }

        protected override MPRPLDerivedCaption UpdateEntity(MPRContext entityContext, MPRPLDerivedCaption entity)
        {
            return (from e in entityContext.Set<MPRPLDerivedCaption>()
                    where e.PLDerivedCaptionId == entity.PLDerivedCaptionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MPRPLDerivedCaption> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<MPRPLDerivedCaption>()
                   select e;
        }

        protected override MPRPLDerivedCaption GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MPRPLDerivedCaption>()
                         where e.PLDerivedCaptionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MPRPLDerivedCaptionInfo> GetMPRPLDerivedCaptions()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.MPRPLDerivedCaptionSet
                            join c in entityContext.PLCaptionSet on a.CaptionCode equals c.Code
                            join d in entityContext.PLCaptionSet on a.DependencyCaptionCode equals d.Code
                            select new MPRPLDerivedCaptionInfo()
                            {
                                MPRPLDerivedCaption = a,
                                CaptionCode = c,
                                DependencyCaptioncode = d
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
