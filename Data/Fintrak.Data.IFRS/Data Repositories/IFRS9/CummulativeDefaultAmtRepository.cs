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
    [Export(typeof(ICummulativeDefaultAmtRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CummulativeDefaultAmtRepository : DataRepositoryBase<CummulativeDefaultAmt>, ICummulativeDefaultAmtRepository
    {
        protected override CummulativeDefaultAmt AddEntity(IFRSContext entityContext, CummulativeDefaultAmt entity)
        {
            return entityContext.Set<CummulativeDefaultAmt>().Add(entity);
        }

        protected override CummulativeDefaultAmt UpdateEntity(IFRSContext entityContext, CummulativeDefaultAmt entity)
        {
            return (from e in entityContext.Set<CummulativeDefaultAmt>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CummulativeDefaultAmt> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<CummulativeDefaultAmt>()
                   select e;
        }

        protected override CummulativeDefaultAmt GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CummulativeDefaultAmt>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



        /////////////////////////////////////////// Methods from IRepo..

        public IEnumerable<CummulativeDefaultAmt> GetCummulativeDefaultAmtBySearch(string searchParam) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<CummulativeDefaultAmt>()
                            // where e.RefNo == searchParam               //orderby e.RefNo, e.datepmt                             
                             select e);
                return query.ToArray();
            }
        }

        public IEnumerable<CummulativeDefaultAmt> GetCummulativeDefaultAmts(int defaultCount, string path) {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<CummulativeDefaultAmt>()
                                 select new
                                 {
                                     HC1 = e.ProductType,
                                     HC2 = e.sub_type,
                                     YOO = e.Origin_yr,
                                     OriginationYearBal = e.AdjBal,
                                     CumDefaulAmtYr0 = e.OutBalAfter_yr,
                                     CumDefaulAmtYr1 = e.OutBalAfter_yr1,
                                     CumDefaulAmtYr2 = e.OutBalAfter_yr2,
                                     CumDefaulAmtYr3 = e.OutBalAfter_yr3,
                                     CumDefaulAmtYr4 = e.OutBalAfter_yr4,
                                     CumDefaulAmtYr5 = e.OutBalAfter_yr5,
                                     CumDefaulAmtYr6 = e.OutBalAfter_yr6
                                 }).OrderBy(c => c.HC1).ThenBy(c => c.HC2).ThenBy(c => c.YOO);
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<CummulativeDefaultAmt>().Take(defaultCount).ToArray();

                    //var query = (from e in entityContext.Set<CummulativeDefaultAmt>() select e).OrderBy(c => c.ProductType).ThenBy(c => c.sub_type).ThenBy(c => c.Origin_yr);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<CummulativeDefaultAmt>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);
                    return query.ToArray();
                }
            }
        }


    }
}