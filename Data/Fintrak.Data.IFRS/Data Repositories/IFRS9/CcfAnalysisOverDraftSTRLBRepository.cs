using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ICcfAnalysisOverDraftSTRLBRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CcfAnalysisOverDraftSTRLBRepository : DataRepositoryBase<CcfAnalysisOverDraftSTRLB>, ICcfAnalysisOverDraftSTRLBRepository
    {
        protected override CcfAnalysisOverDraftSTRLB AddEntity(IFRSContext entityContext, CcfAnalysisOverDraftSTRLB entity)
        {
            return entityContext.Set<CcfAnalysisOverDraftSTRLB>().Add(entity);
        }

        protected override CcfAnalysisOverDraftSTRLB UpdateEntity(IFRSContext entityContext, CcfAnalysisOverDraftSTRLB entity)
        {
            return (from e in entityContext.Set<CcfAnalysisOverDraftSTRLB>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CcfAnalysisOverDraftSTRLB> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<CcfAnalysisOverDraftSTRLB>()
                   select e;
        }

        protected override CcfAnalysisOverDraftSTRLB GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CcfAnalysisOverDraftSTRLB>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



        /////////////////////////////////////////// Methods from IRepo..

        public IEnumerable<CcfAnalysisOverDraftSTRLB> GetCcfAnalysisOverDraftSTRLBBySearch(string searchParam) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<CcfAnalysisOverDraftSTRLB>()
                             where e.CCFType == searchParam               //orderby e.RefNo, e.datepmt                             
                             select e);
                return query.ToArray();
            }
        }

        public IEnumerable<CcfAnalysisOverDraftSTRLB> GetCcfAnalysisOverDraftSTRLBs(int defaultCount) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<CcfAnalysisOverDraftSTRLB>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e);
                return query.ToArray();
            }
        }


    }
}