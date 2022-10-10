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
    [Export(typeof(IHistoricalDefaultFreqRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HistoricalDefaultFreqRepository : DataRepositoryBase<HistoricalDefaultFreq>, IHistoricalDefaultFreqRepository
    {
        protected override HistoricalDefaultFreq AddEntity(IFRSContext entityContext, HistoricalDefaultFreq entity)
        {
            return entityContext.Set<HistoricalDefaultFreq>().Add(entity);
        }

        protected override HistoricalDefaultFreq UpdateEntity(IFRSContext entityContext, HistoricalDefaultFreq entity)
        {
            return (from e in entityContext.Set<HistoricalDefaultFreq>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<HistoricalDefaultFreq> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<HistoricalDefaultFreq>()
                   select e;
        }

        protected override HistoricalDefaultFreq GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<HistoricalDefaultFreq>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



        /////////////////////////////////////////// Methods from IRepo..

        public IEnumerable<HistoricalDefaultFreq> GetHistoricalDefaultFreqBySearch(string searchParam) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<HistoricalDefaultFreq>()
                             //where e.RefNo == searchParam               //orderby e.RefNo, e.datepmt                             
                             select e);
                return query.ToArray();
            }
        }

        public IEnumerable<HistoricalDefaultFreq> GetHistoricalDefaultFreqs(int defaultCount, string path) {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<HistoricalDefaultFreq>()
                                 select new
                                 {
                                     HC1 = e.ProductType,
                                     HC2 = e.Sub_type,
                                     YOO = e.Origin_Yr,
                                     OriginationYearBal = e.AdjBal,
                                     DeaultAmt_0 = e.OutBalAfter_Yr,
                                     DeaultAmt_1 = e.OutBalAfter_Yr1,
                                     DeaultAmt_2 = e.OutBalAfter_Yr2,
                                     DeaultAmt_3 = e.OutBalAfter_Yr3,
                                     DeaultAmt_4 = e.OutBalAfter_Yr4,
                                     DeaultAmt_5 = e.OutBalAfter_Yr5
                                 }).OrderBy(c => c.HC1).ThenBy(c => c.HC2).ThenBy(c => c.YOO);
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<HistoricalDefaultFreq>().Take(defaultCount).ToArray();
                    //var query = (from e in entityContext.Set<HistoricalDefaultFreq>() select e).OrderBy(c => c.ProductType).ThenBy(c => c.Sub_type).ThenBy(c => c.Origin_Yr);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<HistoricalDefaultFreq>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);
                    return query.ToArray();
                }
            }
        }


    }
}