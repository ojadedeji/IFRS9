using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.SqlClient;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IBorrowingPeriodicScheduleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BorrowingPeriodicScheduleRepository : DataRepositoryBase<BorrowingPeriodicSchedule>, IBorrowingPeriodicScheduleRepository
    {

        protected override BorrowingPeriodicSchedule AddEntity(IFRSContext entityContext, BorrowingPeriodicSchedule entity)
        {
            return entityContext.Set<BorrowingPeriodicSchedule>().Add(entity);
        }

        protected override BorrowingPeriodicSchedule UpdateEntity(IFRSContext entityContext, BorrowingPeriodicSchedule entity)
        {
            return (from e in entityContext.Set<BorrowingPeriodicSchedule>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BorrowingPeriodicSchedule> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<BorrowingPeriodicSchedule>()
                   select e;
        }

        protected override BorrowingPeriodicSchedule GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BorrowingPeriodicSchedule>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<string> GetDistinctBorrowingPeriodicScheduleRefNos()
        {
            IFRSContext entityContext = new IFRSContext();

            var query = (entityContext.BorrowingPeriodicScheduleSet.Select<BorrowingPeriodicSchedule, string>(r => r.RefNo)).Distinct();

            return query.ToFullyLoaded();
        }

        public IEnumerable<BorrowingPeriodicSchedule> GetBorrowingPeriodicScheduleRefNos(string borrowingPeriodicScheduleRefNo)
        {
            IFRSContext entityContext = new IFRSContext();

            var query = entityContext.BorrowingPeriodicScheduleSet.AsQueryable().Where(r => r.RefNo == borrowingPeriodicScheduleRefNo);

            return query.ToFullyLoaded();
        }

        public List<BorrowingPeriodicSchedule> GetDistinctRefNo()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            var connectionString = IFRSContext.GetDataConnection();

            var borrowingPeriodicSchedules = new List<BorrowingPeriodicSchedule>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("Get_Distinct_ifrs_borrowing_periodic_schedule", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var borrowingPeriodicSchedule = new BorrowingPeriodicSchedule();

                    if (reader["RefNo"] != DBNull.Value)
                        borrowingPeriodicSchedule.RefNo = reader["RefNo"].ToString();

                    borrowingPeriodicSchedules.Add(borrowingPeriodicSchedule);
                }

                con.Close();
            }

            return borrowingPeriodicSchedules;
        }
        public BorrowingPeriodicSchedule[] GetBorrowingPeriodicSchedulebyRefNo(string refNo, string path)
        {
            IFRSContext entityContext = new IFRSContext();

            if (refNo.Contains("ExportData "))
            {
                refNo = refNo.Replace("ExportData ", "");
                var query = (from e in entityContext.Set<BorrowingPeriodicSchedule>()
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

                return new List<BorrowingPeriodicSchedule>().Take(0).ToArray();
            }
            else
            {
                var query = (from e in entityContext.Set<BorrowingPeriodicSchedule>()
                             where (refNo.Contains(e.RefNo))
                             orderby e.RefNo, e.Date_Pmt
                             select e);

                return query.ToArray();
            }
        }
    }
}
