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
    [Export(typeof(IIfrsProjectedCummDefaultFrqRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class   IfrsProjectedCummDefaultFrqRepository : DataRepositoryBase<  IfrsProjectedCummDefaultFrq>, IIfrsProjectedCummDefaultFrqRepository
    {
        protected override   IfrsProjectedCummDefaultFrq AddEntity(IFRSContext entityContext,   IfrsProjectedCummDefaultFrq entity)
        {
            return entityContext.Set<  IfrsProjectedCummDefaultFrq>().Add(entity);
        }

        protected override   IfrsProjectedCummDefaultFrq UpdateEntity(IFRSContext entityContext,   IfrsProjectedCummDefaultFrq entity)
        {
            return (from e in entityContext.Set <IfrsProjectedCummDefaultFrq>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<  IfrsProjectedCummDefaultFrq> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsProjectedCummDefaultFrq>()
                   select e;
        }

        protected override   IfrsProjectedCummDefaultFrq GetEntity(IFRSContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<IfrsProjectedCummDefaultFrq>()
                         where e.Id == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<IfrsProjectedCummDefaultFrq> GetAllIfrsProjectedCummDefaultFrq(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsProjectedCummDefaultFrq>()
                                 select new
                                 {
                                     HC1 = e.ProductType,
                                     HC2 = e.sub_type,
                                     YOO = e.Origin_yr,
                                     OriginationYearBal = e.AdjBal,
                                     DefaulAmtYr0 = e.OutBalAfter_yr,
                                     DefaulAmtYr1 = e.OutBalAfter_yr1,
                                     DefaulAmtYr2 = e.OutBalAfter_yr2,
                                     DefaulAmtYr3 = e.OutBalAfter_yr3,
                                     DefaulAmtYr4 = e.OutBalAfter_yr4,
                                     DefaulAmtYr5 = e.OutBalAfter_yr5,
                                     DefaulAmtYr6 = e.OutBalAfter_yr6
                                 }).OrderBy(c => c.HC1).ThenBy(c => c.HC2).ThenBy(c => c.YOO);
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsProjectedCummDefaultFrq>().Take(defaultCount).ToArray();
                    //var query = (from e in entityContext.Set<IfrsProjectedCummDefaultFrq>() select e).OrderBy(c => c.ProductType).ThenBy(c => c.sub_type).ThenBy(c => c.Origin_yr);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsProjectedCummDefaultFrq>().Take(defaultCount) select e);

                    return query.ToArray();
                }
            }
        }


    }
}
