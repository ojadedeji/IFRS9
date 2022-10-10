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
    [Export(typeof(IIfrsCureRatesRecoveryRatesRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsCureRatesRecoveryRatesRepository : DataRepositoryBase<IfrsCureRatesRecoveryRates>, IIfrsCureRatesRecoveryRatesRepository
    {
        protected override IfrsCureRatesRecoveryRates AddEntity(IFRSContext entityContext, IfrsCureRatesRecoveryRates entity)
        {
            return entityContext.Set<IfrsCureRatesRecoveryRates>().Add(entity);
        }

        protected override IfrsCureRatesRecoveryRates UpdateEntity(IFRSContext entityContext, IfrsCureRatesRecoveryRates entity)
        {
            return (from e in entityContext.Set<IfrsCureRatesRecoveryRates>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IfrsCureRatesRecoveryRates> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsCureRatesRecoveryRates>()
                   select e;
        }

        protected override IfrsCureRatesRecoveryRates GetEntity(IFRSContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<IfrsCureRatesRecoveryRates>()
                         where e.ID == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsCureRatesRecoveryRates> GetRecordByRefNo(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsCureRatesRecoveryRates>()
                             where e.ProductType == searchParam
                           
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<IfrsCureRatesRecoveryRates> GetIfrsCureRatesRecoveryRates (int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsCureRatesRecoveryRates>()
                                 select new
                                 {
                                     ID = e.ID,
                                     Producttype = e.ProductType,
                                     CureRate = e.CureRate,
                                     RecoveryRate = e.RecoveryRate,
                                     Rundate = e.RunDate
                           

                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsCureRatesRecoveryRates>().Take(defaultCount).ToArray();

                    //var query = (from e in entityContext.Set<IfrsCureRatesRecoveryRates>() select e);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsCureRatesRecoveryRates>().Take(defaultCount) select e);

                    return query.ToArray();
                }
            }
        }
    }
}
