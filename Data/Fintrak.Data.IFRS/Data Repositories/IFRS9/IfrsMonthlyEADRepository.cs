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
    [Export(typeof(IIfrsMonthlyEADRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class   IfrsMonthlyEADRepository : DataRepositoryBase<IfrsMonthlyEAD>, IIfrsMonthlyEADRepository
    {
        protected override IfrsMonthlyEAD AddEntity(IFRSContext entityContext, IfrsMonthlyEAD entity)
        {
            return entityContext.Set<IfrsMonthlyEAD>().Add(entity);
        }

        protected override IfrsMonthlyEAD UpdateEntity(IFRSContext entityContext, IfrsMonthlyEAD entity)
        {
            return (from e in entityContext.Set<IfrsMonthlyEAD>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IfrsMonthlyEAD> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsMonthlyEAD>()
                   select e;
        }

        protected override IfrsMonthlyEAD GetEntity(IFRSContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<IfrsMonthlyEAD>()
                         where e.Id == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
        public IEnumerable<IfrsMonthlyEAD> GetAvailableIfrsMonthlyEAD(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsMonthlyEAD>()
                                 select new
                                 {
                                     e.RefNo,
                                     e.AccountNo,
                                     e.date_pmt,
                                     e.AmortizedCost,
                                     e.EAD,
                                     e.Producttype,
                                     e.SubType,
                                     e.OriginYear,
                                     e.InterestAccrued,
                                     e.currency,
                                     e.EIR,
                                     e.RunDate,
                                     e.TenorInDays,
                                     e.Stage
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsMonthlyEAD>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsMonthlyEAD>().Take(defaultCount)
                                 select e);

                    return query.ToArray();
                }
            }
        }
        public IEnumerable<IfrsMonthlyEAD> GetIfrsMonthlyEADBySearch(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsMonthlyEAD>()
                             where (e.AccountNo == searchParam || e.RefNo == searchParam)
                             select e);

                return query.ToArray();
            }
        }

    }
}
