using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIfrsLoanMissPaymentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsLoanMissPaymentRepository : DataRepositoryBase<IfrsLoanMissPayment>, IIfrsLoanMissPaymentRepository
    {
        protected override IfrsLoanMissPayment AddEntity(IFRSContext entityContext, IfrsLoanMissPayment entity)
        {
            return entityContext.Set<IfrsLoanMissPayment>().Add(entity);
        }

        protected override IfrsLoanMissPayment UpdateEntity(IFRSContext entityContext, IfrsLoanMissPayment entity)
        {
            return (from e in entityContext.Set<IfrsLoanMissPayment>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IfrsLoanMissPayment> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsLoanMissPayment>()
                   select e;
        }

        protected override IfrsLoanMissPayment GetEntity(IFRSContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<IfrsLoanMissPayment>()
                         where e.ID == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        //public IEnumerable<IfrsLoanMissPayment> GetRecordByRefNo(string searchParam)
        //{
        //    using (IFRSContext entityContext = new IFRSContext())
        //    {
        //        var query = (from e in entityContext.Set<IfrsLoanMissPayment>()
        //                     where e.ID == searchParam
        //                     orderby e.date_pmt
        //                     select e);

        //        return query.ToArray();
        //    }
        //}

        //public IEnumerable<IfrsLoanMissPayment> GetIfrsLoanMissPayments(int defaultCount, string path)
        //{
        //    using (IFRSContext entityContext = new IFRSContext())
        //    {
        //        if (!string.IsNullOrEmpty(path))
        //        {
        //            var query = (from e in entityContext.Set<IfrsLoanMissPayment>()
        //                         select new
        //                         {
        //                             CustID = e.ID,
        //                             DefaultParam = e.DefaultParam,
        //                             DaysPastDue = e.DaysPastDue
                                    
        //                         });
        //            var ExportHandler = new ExcelService();
        //            var response = ExportHandler.Export(query.ToList(), path);

        //            return new List<IfrsLoanMissPayment>().Take(defaultCount).ToArray();

        //            //var query = (from e in entityContext.Set<IfrsLoanMissPayment>() select e);
        //            //var ExportHandler = new ExcelService();
        //            //var response = ExportHandler.Export(query.ToList(), path);

        //            //return query.Take(defaultCount).ToArray();
        //        }
        //        else
        //        {
        //            var query = (from e in entityContext.Set<IfrsLoanMissPayment>().Take(defaultCount) select e);

        //            return query.ToArray();
        //        }
        //    }
        //}
    }
}

