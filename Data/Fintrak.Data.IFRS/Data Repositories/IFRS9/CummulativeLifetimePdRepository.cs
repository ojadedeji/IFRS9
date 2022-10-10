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
    [Export(typeof(ICummulativeLifetimePdRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CummulativeLifetimePdRepository : DataRepositoryBase<CummulativeLifetimePd>, ICummulativeLifetimePdRepository
    {
        protected override CummulativeLifetimePd AddEntity(IFRSContext entityContext, CummulativeLifetimePd entity)
        {
            return entityContext.Set<CummulativeLifetimePd>().Add(entity);
        }

        protected override CummulativeLifetimePd UpdateEntity(IFRSContext entityContext, CummulativeLifetimePd entity)
        {
            return (from e in entityContext.Set<CummulativeLifetimePd>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CummulativeLifetimePd> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<CummulativeLifetimePd>()
                   select e;
        }

        protected override CummulativeLifetimePd GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CummulativeLifetimePd>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



        /////////////////////////////////////////// Methods from IRepo..

        public IEnumerable<CummulativeLifetimePd> GetCummulativeLifetimePdBySearch(string searchParam) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<CummulativeLifetimePd>()
                            // where e.RefNo == searchParam               //orderby e.RefNo, e.datepmt                             
                             select e);
                return query.ToArray();
            }
        }

        public IEnumerable<CummulativeLifetimePd> GetCummulativeLifetimePds(int defaultCount, string path) {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<CummulativeLifetimePd>()
                                 select new
                                 {
                                     HC1 = e.ProductType,
                                     HC2 = e.sub_type,
                                     PD_Type = e.CummulativeLifeTimePD,
                                     PD_Yr0 = e.OutBalAfter_yr,
                                     PD_Yr1 = e.OutBalAfter_yr1,
                                     PD_Yr2 = e.OutBalAfter_yr2,
                                     PD_Yr3 = e.OutBalAfter_yr3,
                                     PD_Yr4 = e.OutBalAfter_yr4,
                                     PD_Yr5 = e.OutBalAfter_yr5,
                                     PD_Yr6 = e.OutBalAfter_yr6,
                                     PD_Yr7 = e.OutBalAfter_yr7,
                                     PD_Yr8 = e.OutBalAfter_yr8,
                                     PD_Yr9 = e.OutBalAfter_yr9,
                                     PD_Yr10 = e.OutBalAfter_yr10,
                                     PD_Yr11 = e.OutBalAfter_yr11,
                                     PD_Yr12 = e.OutBalAfter_yr12,
                                     PD_Yr13 = e.OutBalAfter_yr13,
                                     PD_Yr14 = e.OutBalAfter_yr14,
                                     PD_Yr15 = e.OutBalAfter_yr15,
                                     PD_Yr16 = e.OutBalAfter_yr16,
                                     PD_Yr17 = e.OutBalAfter_yr17,
                                     PD_Yr18 = e.OutBalAfter_yr18,
                                     PD_Yr19 = e.OutBalAfter_yr19,
                                     PD_Yr20 = e.OutBalAfter_yr20,
                                     PD_Yr21 = e.OutBalAfter_yr21,
                                     PD_Yr22 = e.OutBalAfter_yr22,
                                     PD_Yr23 = e.OutBalAfter_yr23,
                                     PD_Yr24 = e.OutBalAfter_yr24,
                                     PD_Yr25 = e.OutBalAfter_yr25,
                                     PD_Yr26 = e.OutBalAfter_yr26,
                                     PD_Yr27 = e.OutBalAfter_yr27,
                                     PD_Yr28 = e.OutBalAfter_yr28,
                                     PD_Yr29 = e.OutBalAfter_yr29,
                                     PD_Yr30 = e.OutBalAfter_yr30,
                                     PD_Yr31 = e.OutBalAfter_yr31,
                                     PD_Yr32 = e.OutBalAfter_yr32,
                                     PD_Yr33 = e.OutBalAfter_yr33,
                                     PD_Yr34 = e.OutBalAfter_yr34,
                                     PD_Yr35 = e.OutBalAfter_yr35,
                                     PD_Yr36 = e.OutBalAfter_yr36,
                                     PD_Yr37 = e.OutBalAfter_yr37,
                                     PD_Yr38 = e.OutBalAfter_yr38,
                                     PD_Yr39 = e.OutBalAfter_yr39,
                                     PD_Yr40 = e.OutBalAfter_yr40
                                 }).OrderBy(c => c.HC1).ThenBy(c => c.HC2).ThenBy(c => c.PD_Type);
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<CummulativeLifetimePd>().Take(defaultCount).ToArray();

                    //var query = (from e in entityContext.Set<CummulativeLifetimePd>() select e).OrderBy(c => c.ProductType).ThenBy(c => c.sub_type).ThenBy(c => c.CummulativeLifeTimePD);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<CummulativeLifetimePd>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);
                    return query.ToArray();
                }
            }
        }


    }
}