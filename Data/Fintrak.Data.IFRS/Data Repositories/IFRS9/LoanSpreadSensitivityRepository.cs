using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILoanSpreadSensitivityRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoanSpreadSensitivityRepository : DataRepositoryBase<LoanSpreadSensitivity>, ILoanSpreadSensitivityRepository
    {
        protected override LoanSpreadSensitivity AddEntity(IFRSContext entityContext, LoanSpreadSensitivity entity)
        {
            return entityContext.Set<LoanSpreadSensitivity>().Add(entity);
        }

        protected override LoanSpreadSensitivity UpdateEntity(IFRSContext entityContext, LoanSpreadSensitivity entity)
        {
            return (from e in entityContext.Set<LoanSpreadSensitivity>()
                    where e.LoanSpreadSensitivityId == entity.LoanSpreadSensitivityId
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<LoanSpreadSensitivity> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LoanSpreadSensitivity>()
                   select e;
        }

        protected override LoanSpreadSensitivity GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LoanSpreadSensitivity>()
                         where e.LoanSpreadSensitivityId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
        //public IEnumerable<loanSpreadSensitivity> GetLoanAssessments(string bucket)
        //{
        //    using (IFRSContext entityContext = new IFRSContext())
        //    {
        //        var query = from a in entityContext.loanSpreadSensitivitySet
        //                    where a.Bucket == bucket
        //                    select a;

        //        return query.ToFullyLoaded();
        //    }
        //}
    }
}