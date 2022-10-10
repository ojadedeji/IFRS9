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
    [Export(typeof(IHistoricalClassificationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HistoricalClassificationRepository : DataRepositoryBase<HistoricalClassification>, IHistoricalClassificationRepository
    {
        protected override HistoricalClassification AddEntity(IFRSContext entityContext, HistoricalClassification entity)
        {
            return entityContext.Set<HistoricalClassification>().Add(entity);
        }

        protected override HistoricalClassification UpdateEntity(IFRSContext entityContext, HistoricalClassification entity)
        {
            return (from e in entityContext.Set<HistoricalClassification>()
                    where e.HistoricalClassificationId == entity.HistoricalClassificationId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<HistoricalClassification> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<HistoricalClassification>()
                   select e;
        }

        protected override HistoricalClassification GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<HistoricalClassification>()
                         where e.HistoricalClassificationId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<HistoricalClassification> GetAvailableHistoricalClassification(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<HistoricalClassification>()
                                 select new
                                 {
                                     e.CustomerNo,
                                     e.CustomerName,
                                     e.SectorIndustry,
                                     e.Classification,
                                     e.SubClassification,
                                     e.Collateral_Type,
                                     e.OutstandingBal,
                                     e.RecoverableAmt,
                                     e.Period,
                                     e.Year,
                                     e.CompanyCode
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<HistoricalClassification>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<HistoricalClassification>().Take(defaultCount)
                                 select e);

                    return query.ToArray();
                }
            }
        }

    }
}