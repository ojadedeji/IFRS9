using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IMarginalCCFPivotSTRLBRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MarginalCCFPivotSTRLBRepository : DataRepositoryBase<MarginalCCFPivotSTRLB>, IMarginalCCFPivotSTRLBRepository
    {
        protected override MarginalCCFPivotSTRLB AddEntity(IFRSContext entityContext, MarginalCCFPivotSTRLB entity)
        {
            return entityContext.Set<MarginalCCFPivotSTRLB>().Add(entity);
        }

        protected override MarginalCCFPivotSTRLB UpdateEntity(IFRSContext entityContext, MarginalCCFPivotSTRLB entity)
        {
            return (from e in entityContext.Set<MarginalCCFPivotSTRLB>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MarginalCCFPivotSTRLB> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MarginalCCFPivotSTRLB>()
                   select e;
        }

        protected override MarginalCCFPivotSTRLB GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MarginalCCFPivotSTRLB>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



        /////////////////////////////////////////// Methods from IRepo..

        public IEnumerable<MarginalCCFPivotSTRLB> GetMarginalCCFPivotSTRLBBySearch(string searchParam) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<MarginalCCFPivotSTRLB>()
                             //where e.RefNo == searchParam               //orderby e.RefNo, e.datepmt                             
                             select e);
                return query.ToArray();
            }
        }

        public IEnumerable<MarginalCCFPivotSTRLB> GetMarginalCCFPivotSTRLBs(int defaultCount) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<MarginalCCFPivotSTRLB>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e);
                return query.ToArray();
            }
        }


    }
}