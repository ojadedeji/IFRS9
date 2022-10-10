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
    [Export(typeof(ITrialBalanceRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TrialBalanceRepository : DataRepositoryBase<TrialBalance>, ITrialBalanceRepository
    {

        protected override TrialBalance AddEntity(IFRSContext entityContext, TrialBalance entity)
        {
            return entityContext.Set<TrialBalance>().Add(entity);
        }

        protected override TrialBalance UpdateEntity(IFRSContext entityContext, TrialBalance entity)
        {
            return (from e in entityContext.Set<TrialBalance>() 
                    where e.TrialBalanceId == entity.TrialBalanceId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TrialBalance> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<TrialBalance>()
                   select e;
        }

        protected override TrialBalance GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TrialBalance>()
                         where e.TrialBalanceId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<TrialBalance> GetTrialBalances(DateTime runDate)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.TrialBalanceSet
                            where a.TransDate == runDate
                            select a;

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<TrialBalance> GetTrialBalancesByBranch(DateTime runDate, string branchCode)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.TrialBalanceSet
                            where a.TransDate == runDate && a.BranchCode == branchCode
                            select a;

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<TrialBalance> ExportTrialBalance(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<TrialBalance>()
                                 select new
                                 {
                                     e.BranchCode,
                                     e.GLCode,
                                     e.Description,
                                     e.GLSubHeadCode,
                                     e.Currency,
                                     e.ExchangeRate,
                                     e.Debit,
                                     e.Credit,
                                     e.LCY_Debit,
                                     e.LCY_Credit,
                                     e.Balance,
                                     e.LCY_Balance,
                                     e.TransDate,
                                     e.CompanyCode
                                 });

                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<TrialBalance>().Take(0).ToArray().OrderBy(c => c.TransDate).ThenBy(c => c.GLCode);

                    //var query = (from e in entityContext.Set<LoanECLResult>() //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                    //             select e);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<TrialBalance>().Take(defaultCount) //.OrderBy(c => c.RefNo).ThenBy(c => c.datepmt)
                                 select e);

                    return query.ToArray();
                }
            }
        }

    }
}
