using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IMPRPLDerivedCaptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MPRPLDerivedCaptionRepository : DataRepositoryBase<MPRPLDerivedCaption>, IMPRPLDerivedCaptionRepository
    {

        protected override MPRPLDerivedCaption AddEntity(BasicContext entityContext, MPRPLDerivedCaption entity)
        {
            return entityContext.Set<MPRPLDerivedCaption>().Add(entity);
        }

        protected override MPRPLDerivedCaption UpdateEntity(BasicContext entityContext, MPRPLDerivedCaption entity)
        {
            return (from e in entityContext.Set<MPRPLDerivedCaption>()
                    where e.PLDerivedCaptionId == entity.PLDerivedCaptionId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MPRPLDerivedCaption> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<MPRPLDerivedCaption>()
                   select e;
        }

        protected override MPRPLDerivedCaption GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MPRPLDerivedCaption>()
                         where e.PLDerivedCaptionId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MPRPLDerivedCaptionInfo> GetMPRPLDerivedCaptions()
        {
            using (BasicContext entityContext = new BasicContext())
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
