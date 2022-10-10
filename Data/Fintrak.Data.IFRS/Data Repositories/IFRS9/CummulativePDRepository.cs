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
    [Export(typeof(ICummulativePDRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CummulativePDRepository : DataRepositoryBase<CummulativePD>, ICummulativePDRepository
    {
        protected override CummulativePD AddEntity(IFRSContext entityContext, CummulativePD entity)
        {
            return entityContext.Set<CummulativePD>().Add(entity);
        }

        protected override CummulativePD UpdateEntity(IFRSContext entityContext, CummulativePD entity)
        {
            return (from e in entityContext.Set<CummulativePD>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CummulativePD> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<CummulativePD>()
                   select e;
        }

        protected override CummulativePD GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CummulativePD>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }



        /////////////////////////////////////////// Methods from IRepo..

        public IEnumerable<CummulativePD> GetCummulativePDBySearch(string searchParam) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<CummulativePD>()
                             //where e.RefNo == searchParam               //orderby e.RefNo, e.datepmt                             
                             select e);
                return query.ToArray();
            }
        }

        public IEnumerable<CummulativePD> GetCummulativePDs(int defaultCount, string path) {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<CummulativePD>()
                                 select new
                                 {
                                     HC1 = e.ProductType,
                                     HC2 = e.Sub_type,
                                     YOO = e.Origin_Yr,
                                     OriginationYearBal = e.AdjBal,
                                     Year_0 = e.OutBalAfter_Yr,
                                     Year_1 = e.OutBalAfter_Yr1,
                                     Year_2 = e.OutBalAfter_Yr2,
                                     Year_3 = e.OutBalAfter_Yr3,
                                     Year_4 = e.OutBalAfter_Yr4,
                                     Year_5 = e.OutBalAfter_Yr5,
                                     Year_6 = e.OutBalAfter_Yr6,
                                     Year_7 = e.OutBalAfter_Yr7,
                                     Year_8 = e.OutBalAfter_Yr8,
                                     Year_9 = e.OutBalAfter_Yr9,
                                     Year_10 = e.OutBalAfter_Yr10,
                                     Year_11 = e.OutBalAfter_Yr11,
                                     Year_12 = e.OutBalAfter_Yr12,
                                     Year_13 = e.OutBalAfter_Yr13,
                                     Year_14 = e.OutBalAfter_Yr14,
                                     Year_15 = e.OutBalAfter_Yr15,
                                     Year_16 = e.OutBalAfter_Yr16,
                                     Year_17 = e.OutBalAfter_Yr17,
                                     Year_18 = e.OutBalAfter_Yr18,
                                     Year_19 = e.OutBalAfter_Yr19,
                                     Year_20 = e.OutBalAfter_Yr20,
                                     Year_21 = e.OutBalAfter_Yr21,
                                     Year_22 = e.OutBalAfter_Yr22,
                                     Year_23 = e.OutBalAfter_Yr23,
                                     Year_24 = e.OutBalAfter_Yr24,
                                     Year_25 = e.OutBalAfter_Yr25,
                                     Year_26 = e.OutBalAfter_Yr26,
                                     Year_27 = e.OutBalAfter_Yr27,
                                     Year_28 = e.OutBalAfter_Yr28,
                                     Year_29 = e.OutBalAfter_Yr29,
                                     Year_30 = e.OutBalAfter_Yr30,
                                     Year_31 = e.OutBalAfter_Yr31,
                                     Year_32 = e.OutBalAfter_Yr32,
                                     Year_33 = e.OutBalAfter_Yr33,
                                     Year_34 = e.OutBalAfter_Yr34,
                                     Year_35 = e.OutBalAfter_Yr35,
                                     Year_36 = e.OutBalAfter_Yr36,
                                     Year_37 = e.OutBalAfter_Yr37,
                                     Year_38 = e.OutBalAfter_Yr38,
                                     Year_39 = e.OutBalAfter_Yr39,
                                     Year_40 = e.OutBalAfter_Yr40
                                 }).OrderBy(c => c.HC1).ThenBy(c => c.HC2).ThenBy(c => c.YOO);
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<CummulativePD>().Take(defaultCount).ToArray();

                    //var query = (from e in entityContext.Set<CummulativePD>() select e).OrderBy(c => c.ProductType).ThenBy(c => c.Sub_type).ThenBy(c => c.Origin_Yr);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<CummulativePD>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);
                    return query.ToArray();
                }
            }
        }


    }
}