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
    [Export(typeof(ICashFlowTBRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CashFlowTBRepository : DataRepositoryBase<CashFlowTB>, ICashFlowTBRepository
    {
        protected override CashFlowTB AddEntity(IFRSContext entityContext, CashFlowTB entity)
        {
            return entityContext.Set<CashFlowTB>().Add(entity);
        }

        protected override CashFlowTB UpdateEntity(IFRSContext entityContext, CashFlowTB entity)
        {
            return (from e in entityContext.Set<CashFlowTB>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CashFlowTB> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<CashFlowTB>()
                   select e;
        }

        protected override CashFlowTB GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CashFlowTB>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        ///////// ------- ////////////////////////////////// Methods from IRepo..

        public IEnumerable<CashFlowTB> GetCashFlowTBBySearch(string searchParam) {
            using (IFRSContext entityContext = new IFRSContext()) {
                var query = (from e in entityContext.Set<CashFlowTB>()
                             where e.Refno == searchParam  //orderby e.RunDate //, e.datepmt                             
                             select e);
                return query.ToArray();
            }
        }

        //public IEnumerable<CashFlowTB> GetCashFlowTBs(int defaultCount) {
        //    using (IFRSContext entityContext = new IFRSContext()) {
        //        var query = (from e in entityContext.Set<CashFlowTB>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
        //                     select e);
        //        return query.ToArray();
        //    }
        //}


        public IEnumerable<CashFlowTB> GetCashFlowTBBySearch(string searchParam, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (searchParam.Contains("ExportData "))
                {
                    searchParam = searchParam.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<CashFlowTB>()
                                 where searchParam.Contains(e.Refno) 
                                 //orderby e.RefNo, e.datepmt
                                 select new
                                 {
                                     RefNo = e.Refno,
                                     Component = e.Component,
                                     Start_date = e.Start_date,
                                     Due_Date = e.Due_Date,
                                     Amount_Due = e.Amount_Due,
                                     amount_settled = e.amount_settled,
                                     Over_due = e.Over_due,
                                     e.Rundate
                                 });

                    if (searchParam.Substring(0, 5) == "split")
                    {
                        searchParam = searchParam.Substring(5, searchParam.Length - 5);
                        var accounts = (from e in query select new { e.RefNo }).Distinct();
                        var count = accounts.Count();
                        var ExportHandler = new ExcelService(path);
                        var accountNo = count > 0 ? accounts.ToList().ElementAt(0).RefNo : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            accountNo = accounts.ToList().ElementAt(i).RefNo;
                            response = ExportHandler.Export(query.Where(e => e.RefNo == accountNo).ToList(), path + accountNo.Replace("/", ""));
                        }
                    }
                    else
                    {
                        var ExportHandler = new ExcelService(path);
                        string response = ExportHandler.Export(query.ToList(), path);
                    }

                    return new List<CashFlowTB>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<CashFlowTB>()
                                 where searchParam.Contains(e.Refno)
                                 //orderby e.RefNo, e.datepmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<CashFlowTB> GetCashFlowTBs(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<CashFlowTB>()
                                 select new
                                 {
                                     AccountNo = e.Refno,
                                     Component = e.Component,
                                     Start_date = e.Start_date,
                                     Due_Date = e.Due_Date,
                                     Amount_Due = e.Amount_Due,
                                     amount_settled = e.amount_settled,
                                     Over_due = e.Over_due,
                                     e.Rundate
                                 });
                    if (defaultCount == 0)
                    {
                        var ExportHandler = new ExcelService();
                        var response = ExportHandler.Export(query.ToList(), path);
                    }
                    else
                    {
                        var accounts = (from e in query select new { e.AccountNo }).Distinct();
                        var count = accounts.Count();
                        var ExportHandler = new ExcelService(path);
                        var accountNo = count > 0 ? accounts.ToList().ElementAt(0).AccountNo : "";
                        string response = null;
                        for (int i = 0; i < count; ++i)
                        {
                            accountNo = accounts.ToList().ElementAt(i).AccountNo;
                            response = ExportHandler.Export(query.Where(e => e.AccountNo == accountNo).ToList(), path + accountNo.Replace("/", ""));
                        }
                    }

                    return new List<CashFlowTB>().Take(0).ToArray();
                    //var query = (from e in entityContext.Set<LoansECLComputationResult>() select e);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<CashFlowTB>().Take(defaultCount) select e);

                    return query.ToArray();
                }
            }
        }


    }
}