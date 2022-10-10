using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IStaffLoansComputationResultRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StaffLoansComputationResultRepository : DataRepositoryBase<StaffLoansComputationResult>, IStaffLoansComputationResultRepository
    {
        protected override StaffLoansComputationResult AddEntity(IFRSContext entityContext, StaffLoansComputationResult entity)
        {
            return entityContext.Set<StaffLoansComputationResult>().Add(entity);
        }

        protected override StaffLoansComputationResult UpdateEntity(IFRSContext entityContext, StaffLoansComputationResult entity)
        {
            return (from e in entityContext.Set<StaffLoansComputationResult>()
                    where e.StaffLoan_Id == entity.StaffLoan_Id
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<StaffLoansComputationResult> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<StaffLoansComputationResult>().Take(200)
                   select e;
        }

        protected override StaffLoansComputationResult GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<StaffLoansComputationResult>()
                         where e.StaffLoan_Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<StaffLoansComputationResult> GetEntityBySearchParam(string SearchParam)
        {

            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<StaffLoansComputationResult>()
                             where e.Name.Contains(SearchParam)
                             select e);

                return query.ToArray();
            }

        }

        public IEnumerable<StaffLoansComputationResult> GetStaffLoans(int defaultCount)
        {

            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<StaffLoansComputationResult>().Take(defaultCount)
                             select e);

                return query.ToArray();
            }

        }

    }
}