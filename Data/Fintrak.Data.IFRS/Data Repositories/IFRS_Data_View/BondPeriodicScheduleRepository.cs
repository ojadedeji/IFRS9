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
    [Export(typeof(IBondPeriodicScheduleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BondPeriodicScheduleRepository : DataRepositoryBase<BondPeriodicSchedule>, IBondPeriodicScheduleRepository
    {

        protected override BondPeriodicSchedule AddEntity(IFRSContext entityContext, BondPeriodicSchedule entity)
        {
            return entityContext.Set<BondPeriodicSchedule>().Add(entity);
        }

        protected override BondPeriodicSchedule UpdateEntity(IFRSContext entityContext, BondPeriodicSchedule entity)
        {
            return (from e in entityContext.Set<BondPeriodicSchedule>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BondPeriodicSchedule> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<BondPeriodicSchedule>()
                   select e;
        }

        protected override BondPeriodicSchedule GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BondPeriodicSchedule>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<string> GetDistinctBondPeriodicScheduleRefNos()
        {
            IFRSContext entityContext = new IFRSContext();

            var query = (entityContext.BondPeriodicScheduleSet.Select<BondPeriodicSchedule, string>(r => r.RefNo)).Distinct();

            return query.ToFullyLoaded();
        }

        public IEnumerable<BondPeriodicSchedule> GetBondPeriodicScheduleRefNos(string refNo, string path)
        {
            IFRSContext entityContext = new IFRSContext();

            if (refNo.Contains("ExportData "))
            {
                refNo = refNo.Replace("ExportData ", "");
                var query = (from e in entityContext.Set<BondPeriodicSchedule>()
                             where (refNo.Contains(e.RefNo) || string.IsNullOrEmpty(refNo))
                             orderby e.RefNo, e.Date_Pmt
                             select e);

                if (refNo.Length >= 5 && refNo.Substring(0, 5) == "split")
                {
                    refNo = refNo.Substring(5, refNo.Length - 5);
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

                return new List<BondPeriodicSchedule>().Take(0).ToArray();
            }
            else
            {
                var query = (from e in entityContext.Set<BondPeriodicSchedule>()
                             where (refNo.Contains(e.RefNo))
                             orderby e.RefNo, e.Date_Pmt
                             select e);

                return query.ToArray();
            }
        }
    }
}
