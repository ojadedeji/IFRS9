using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.Common.Services;
using AutoMapper;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILoanScheduleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoanScheduleRepository : DataRepositoryBase<LoanSchedule>, ILoanScheduleRepository
    {

        private IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMissingTypeMaps = true).CreateMapper();

        protected override LoanSchedule AddEntity(IFRSContext entityContext, LoanSchedule entity)
        {
            return entityContext.Set<LoanSchedule>().Add(entity);
        }

        protected override LoanSchedule UpdateEntity(IFRSContext entityContext, LoanSchedule entity)
        {
            return (from e in entityContext.Set<LoanSchedule>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LoanSchedule> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LoanSchedule>()
                   select e;
        }

        protected override LoanSchedule GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LoanSchedule>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<string> GetDistinctLoanScheduleRefNos()
        {
            IFRSContext entityContext = new IFRSContext();

            var query = (entityContext.LoanScheduleSet.Select(r => r.RefNo)).Distinct();

            return query.ToFullyLoaded();
        }

        public IEnumerable<LoanSchedule> GetLoanScheduleRefNos(string loanScheduleRefNo)
        {
            IFRSContext entityContext = new IFRSContext();

            var query = entityContext.LoanScheduleSet.AsQueryable().Where(r => r.RefNo == loanScheduleRefNo);

            return query.ToFullyLoaded();
        }


        public IEnumerable<MultiSelectDropDown> GetDistinctRefNo()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            //var connectionString = IFRSContext.GetDataConnection();

            //var LoanSchedules = new List<LoanSchedule>();
            //using (var con = new SqlConnection(connectionString))
            //{
            //    var cmd = new SqlCommand("Get_Distinct_ifrs_loan_schedule", con);
            //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //    cmd.CommandTimeout = 0;


            //    con.Open();

            //    SqlDataReader reader = cmd.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        var LoanSchedule = new LoanSchedule();

            //        if (reader["RefNo"] != DBNull.Value)
            //            LoanSchedule.RefNo = reader["RefNo"].ToString();

            //        LoanSchedules.Add(LoanSchedule);
            //    }

            //    con.Close();
            //}

            IFRSContext entityContext = new IFRSContext();

            var query = entityContext.LoanScheduleSet.Select(r => r.RefNo).Distinct()
                   .Select(e => new
                   {
                       //Id = 0,
                       name = e,
                       icon = "",
                       maker = "",
                       ticked = false,
                       //Active = true,
                       //Deleted = false,
                       //CreatedBy = "auto",
                       //CreatedOn = new DateTime(),
                       //UpdatedBy = "auto",
                       //UpdatedOn = new DateTime(),
                       //RowVersion = "",
                       //ExtensionData = ""
                   });

            return query.Select(mapper.Map<MultiSelectDropDown>);
        }

        public IEnumerable<LoanSchedule> GetScheduleRange(string refNo, DateTime? rangeDate, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var minDate = rangeDate.HasValue ? rangeDate.Value.AddDays(-30) : rangeDate;
                var maxDate = rangeDate.HasValue ? rangeDate.Value.AddDays(30) : rangeDate;

                if (refNo.Contains("ExportData "))
                {
                    refNo = refNo.Replace("ExportData ", "");
                    var query = (from e in entityContext.Set<LoanSchedule>()
                                 where ((refNo.Contains(e.RefNo) || string.IsNullOrEmpty(refNo))
                                 && (e.PaymentDate >= minDate || (minDate == null))
                                 && (e.PaymentDate <= maxDate || (maxDate == null)))
                                 orderby e.RefNo, e.PaymentDate
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

                    return new List<LoanSchedule>().Take(0);
                }
                else
                {
                    var query = (from e in entityContext.Set<LoanSchedule>()
                                 where (refNo.Contains(e.RefNo) && (e.PaymentDate >= minDate && e.PaymentDate <= maxDate))
                                 orderby e.RefNo, e.PaymentDate
                                 select e);

                    return query.ToArray();
                }
            }
        }

    }
}
