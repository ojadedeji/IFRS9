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
    [Export(typeof(IRestructureInfoRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RestructureInfoRepository : DataRepositoryBase<RestructureInfo>, IRestructureInfoRepository
    {
        protected override RestructureInfo AddEntity(IFRSContext entityContext, RestructureInfo entity)
        {
            return entityContext.Set<RestructureInfo>().Add(entity);
        }

        protected override RestructureInfo UpdateEntity(IFRSContext entityContext, RestructureInfo entity)
        {
            return (from e in entityContext.Set<RestructureInfo>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<RestructureInfo> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<RestructureInfo>()
                   select e;
        }

        protected override RestructureInfo GetEntity(IFRSContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<RestructureInfo>()
                         where e.ID == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<RestructureInfo> GetRecordByRefNo(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<RestructureInfo>()
                             where e.Refno == searchParam
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<RestructureInfo> GetRestructureInfos(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<RestructureInfo>()
                                 select new
                                 {

                                     RefNo = e.Refno,
                                     Amount = e.Outstandingbal,
                                     ValueDate = e.ValueDate,
                                     MaturityDate = e.MaturityDate,
                                     PrincipalFirstPMTDate = e.PrincFirstPmtDate,
                                     InterestFirstPMTDate = e.InterestFirstPmtDate,
                                     Ratee = e.Rate,
                                     PrincipalPMTFreq = e.Repayfreq,
                                     InterestPMTFreq = e.InterestRepayfreq,
                                     RepaymentNo = e.NoRepayments

                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<RestructureInfo>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<RestructureInfo>().Take(defaultCount)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
    

