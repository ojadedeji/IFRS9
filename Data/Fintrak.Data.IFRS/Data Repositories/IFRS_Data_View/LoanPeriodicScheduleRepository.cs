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
    [Export(typeof(ILoanPeriodicScheduleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoanPeriodicScheduleRepository : DataRepositoryBase<LoanPeriodicSchedule>, ILoanPeriodicScheduleRepository
    {

        protected override LoanPeriodicSchedule AddEntity(IFRSContext entityContext, LoanPeriodicSchedule entity)
        {
            return entityContext.Set<LoanPeriodicSchedule>().Add(entity);
        }

        protected override LoanPeriodicSchedule UpdateEntity(IFRSContext entityContext, LoanPeriodicSchedule entity)
        {
            return (from e in entityContext.Set<LoanPeriodicSchedule>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LoanPeriodicSchedule> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LoanPeriodicSchedule>()
                   select e;
        }

        protected override LoanPeriodicSchedule GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LoanPeriodicSchedule>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
        public LoanPeriodicSchedule[] GetLoanPeriodicSchedulebyRefNo(string refNo, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (refNo.Contains("ExportData "))
                {
                    refNo = refNo.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<LoanPeriodicSchedule>()
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

                    return new List<LoanPeriodicSchedule>().Take(0).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<LoanPeriodicSchedule>()
                                 where (refNo.Contains(e.RefNo))
                                 orderby e.RefNo, e.Date_Pmt
                                 select e);

                    return query.ToArray();
                }
            }
        }

        public IEnumerable<string> GetDistinctLoanPeriodicScheduleRefNos()
        {
            IFRSContext entityContext = new IFRSContext();

            var query = (entityContext.LoanPeriodicScheduleSet.Select<LoanPeriodicSchedule, string>(r => r.RefNo)).Distinct();

            return query.ToFullyLoaded();
        }

        public IEnumerable<LoanPeriodicSchedule> GetLoanPeriodicScheduleRefNos(string loanPeriodicScheduleRefNo)
        {
            IFRSContext entityContext = new IFRSContext();

            var query = entityContext.LoanPeriodicScheduleSet.AsQueryable().Where(r => r.RefNo == loanPeriodicScheduleRefNo);

            return query.ToFullyLoaded();
        }

        public List<LoanPeriodicSchedule> GetDistinctRefNo()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            var connectionString = IFRSContext.GetDataConnection();

            var loanPeriodicSchedules = new List<LoanPeriodicSchedule>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("Get_Distinct_ifrs_loan_periodic_schedule", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var loanPeriodicSchedule = new LoanPeriodicSchedule();

                    if (reader["RefNo"] != DBNull.Value)
                        loanPeriodicSchedule.RefNo = reader["RefNo"].ToString();

                    loanPeriodicSchedules.Add(loanPeriodicSchedule);
                }

                con.Close();
            }

            return loanPeriodicSchedules;
        }

    }
}
