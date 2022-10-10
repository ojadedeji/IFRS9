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

    [Export(typeof(ILoanRestructurePVCashFlowRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoanRestructurePVCashFlowRepository : DataRepositoryBase<LoanRestructurePVCashFlow>, ILoanRestructurePVCashFlowRepository
    {
        protected override LoanRestructurePVCashFlow AddEntity(IFRSContext entityContext, LoanRestructurePVCashFlow entity)
        {
            return entityContext.Set<LoanRestructurePVCashFlow>().Add(entity);
        }

        protected override LoanRestructurePVCashFlow UpdateEntity(IFRSContext entityContext, LoanRestructurePVCashFlow entity)
        {
            return (from e in entityContext.Set<LoanRestructurePVCashFlow>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LoanRestructurePVCashFlow> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LoanRestructurePVCashFlow>()
                   select e;
        }

        protected override LoanRestructurePVCashFlow GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LoanRestructurePVCashFlow>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<LoanRestructurePVCashFlow> GetAvailLoanRestructurePVCashFlows(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<LoanRestructurePVCashFlow>()
                                 select new
                                 {
                                    e.RefNo,
                                    e.OpeningBalance,
                                     e.date_pmt,
                                     e.DNY,
                                    e.NoDays,
                                    e.TotalNoDays,
                                    e.YID,
                                    e.DiscountFactor,
                                    e.Amt_int_pay,
                                    e.Amt_prin_pay,
                                    e.Amt_pmt,
                                    e.CummulativeInterest,
                                    e.ClosingBalance,
                                    e.PVCashFlow

                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<LoanRestructurePVCashFlow>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<LoanRestructurePVCashFlow>().Take(defaultCount)
                                 select e);

                    return query.ToArray();
                }
            }
        }


        public IEnumerable<LoanRestructurePVCashFlow> GetLoanRestructurePVCashFlowsBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<LoanRestructurePVCashFlow>()
                                 where searchParam.Contains(e.RefNo)
                                 select new
                                 {
                                     e.RefNo,
                                     e.OpeningBalance,
                                     e.date_pmt,
                                     e.DNY,
                                     e.NoDays,
                                     e.TotalNoDays,
                                     e.YID,
                                     e.DiscountFactor,
                                     e.Amt_int_pay,
                                     e.Amt_prin_pay,
                                     e.Amt_pmt,
                                     e.CummulativeInterest,
                                     e.ClosingBalance,
                                     e.PVCashFlow
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var products = (from e in query select new { e.RefNo }).Distinct();
                        var count = products.Count();
                        var ExportHandler = new ExcelService(path);
                        var RefNo = count > 0 ? products.ToList().ElementAt(0).RefNo : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            RefNo = products.ToList().ElementAt(i).RefNo;
                            response = ExportHandler.Export(query.Where(e => e.RefNo == RefNo).ToList(), path + RefNo.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<LoanRestructurePVCashFlow>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<LoanRestructurePVCashFlow>()
                                 where e.RefNo == searchParam
                                 select e);
                    return query.ToArray();
                }
            }
        }


        public IEnumerable<LoanRestructurePVCashFlow> GetSubRefNo(string refno)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<LoanRestructurePVCashFlow>()
                             where e.RefNo == refno
                             select e);

                return query.ToArray();
            }
        }

        public IEnumerable<string> GetDistinctLoanRestructurePVCashFlows()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (entityContext.LoanRestructurePVCashFlowSet.Select(r => r.RefNo)).Distinct();

                return query.ToFullyLoaded();
            }
        }
    }
}