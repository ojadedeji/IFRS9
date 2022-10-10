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
    [Export(typeof(IUnquotedEquityMarketabilityDiscountRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UnquotedEquityMarketabilityDiscountRepository : DataRepositoryBase<UnquotedEquityMarketabilityDiscount>, IUnquotedEquityMarketabilityDiscountRepository
    {
        protected override UnquotedEquityMarketabilityDiscount AddEntity(IFRSContext entityContext, UnquotedEquityMarketabilityDiscount entity)
        {
            return entityContext.Set<UnquotedEquityMarketabilityDiscount>().Add(entity);
        }

        protected override UnquotedEquityMarketabilityDiscount UpdateEntity(IFRSContext entityContext, UnquotedEquityMarketabilityDiscount entity)
        {
            return (from e in entityContext.Set<UnquotedEquityMarketabilityDiscount>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<UnquotedEquityMarketabilityDiscount> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<UnquotedEquityMarketabilityDiscount>()
                        select e;
            query = query.OrderBy(a => a.ID).GroupBy(e => e.Base).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override UnquotedEquityMarketabilityDiscount GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<UnquotedEquityMarketabilityDiscount>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        //public IEnumerable<UnquotedEquityMarketabilityDiscount> GetUnquotedEquityMarketabilityDiscountBySearch(string searchParam, string path)
        //{
        //    using (IFRSContext entityContext = new IFRSContext())
        //    {
        //        if (searchParam.Contains("ExportData "))
        //        {
        //            searchParam = searchParam.Replace("ExportData ", "");
        //            var query = (from e in entityContext.Set<UnquotedEquityMarketabilityDiscount>()
        //                         where searchParam.Contains(e.Base)
        //                         orderby e.Description
        //                         select new
        //                         {
        //                             e.Base,
        //                             e.Units,
        //                             e.BookValue,
        //                             e.MarketPrice,
        //                             e.ExchangeRate,
        //                             e.Rundate,
        //                             e.CompanyCode
        //                         });

        //            if (searchParam.Substring(0, 5) == "split")
        //            {
        //                searchParam = searchParam.Substring(5, searchParam.Length - 5);
        //                var accounts = (from e in query select new { e.Description }).Distinct();
        //                var count = accounts.Count();
        //                var ExportHandler = new ExcelService(path);
        //                var accountNo = count > 0 ? accounts.ToList().ElementAt(0).Description : "";
        //                string response = null;
        //                for (int i = 0; i < count; ++i)
        //                {
        //                    accountNo = accounts.ToList().ElementAt(i).Description;
        //                    response = ExportHandler.Export(query.Where(e => e.Description == accountNo).ToList(), path + accountNo.Replace("/", ""));
        //                }
        //            }
        //            else
        //            {
        //                var ExportHandler = new ExcelService(path);
        //                string response = ExportHandler.Export(query.ToList(), path);
        //            }

        //            return new List<UnquotedEquityMarketabilityDiscount>().Take(0).ToArray().OrderBy(c => c.Description).ThenBy(c => c.Description); ;
        //        }
        //        else
        //        {
        //            var query = (from e in entityContext.Set<UnquotedEquityMarketabilityDiscount>()
        //                         where e.Description == searchParam
        //                         //orderby e.RefNo, e.datepmt
        //                         select e);

        //            return query.ToArray();
        //        }
        //    }
        //}

        public IEnumerable<UnquotedEquityMarketabilityDiscount> GetUnquotedEquityMarketabilityDiscounts(int defaultCount)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<UnquotedEquityMarketabilityDiscount>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                             select e).Take(defaultCount);
                return query.ToArray();
            }
        }

        public IEnumerable<UnquotedEquityMarketabilityDiscount> ExportUnquotedEquityMarketabilityDiscount(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<UnquotedEquityMarketabilityDiscount>()
                                 select new
                                 {
                                     e.Base,
                                     e.Best,
                                     e.Worst
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<UnquotedEquityMarketabilityDiscount>().Take(0).ToArray().OrderBy(c => c.Base).ThenBy(c => c.Best);

                }
                else
                {
                    var query = (from e in entityContext.Set<UnquotedEquityMarketabilityDiscount>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }
    }
}
