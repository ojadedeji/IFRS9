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
    [Export(typeof(IIfrsInvestmentECLSummaryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsInvestmentECLSummaryRepository : DataRepositoryBase<IfrsInvestmentECLSummary>, IIfrsInvestmentECLSummaryRepository
    {
        protected override IfrsInvestmentECLSummary AddEntity(IFRSContext entityContext, IfrsInvestmentECLSummary entity)
        {
            return entityContext.Set<IfrsInvestmentECLSummary>().Add(entity);
        }

        protected override IfrsInvestmentECLSummary UpdateEntity(IFRSContext entityContext, IfrsInvestmentECLSummary entity)
        {
            return (from e in entityContext.Set<IfrsInvestmentECLSummary>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IfrsInvestmentECLSummary> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsInvestmentECLSummary>()
                   select e;
        }

        protected override IfrsInvestmentECLSummary GetEntity(IFRSContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<IfrsInvestmentECLSummary>()
                         where e.ID == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsInvestmentECLSummary> GetRecordByRefno(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsInvestmentECLSummary>()
                             where e.Assetdescription == searchParam 
                             orderby e.Datepmt
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<IfrsInvestmentECLSummary> GetIfrsInvestmentECLSummarys(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsInvestmentECLSummary>()
                                 select new
                                 {
                                     ID = e.ID,
                                     Assetdescription = e.Assetdescription,
                                     AssetType = e.Assettype,
                                     EIR = e.EIR,
                                     ECL = e.ECL,
                                     Stage = e.Stage,
                                     Datepmt = e.Datepmt,
                                  


                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsInvestmentECLSummary>().Take(defaultCount).ToArray();

                    //var query = (from e in entityContext.Set<IfrsInvestmentECLSummary>() select e);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsInvestmentECLSummary>().Take(defaultCount) select e);

                    return query.ToArray();
                }
            }
        }
    }
}
