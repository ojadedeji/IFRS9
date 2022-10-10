using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;
using System.Configuration;
using System.Data.SqlClient;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IPLIncomeReportRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PLIncomeReportRepository : DataRepositoryBase<PLIncomeReport>, IPLIncomeReportRepository
    {

        protected override PLIncomeReport AddEntity(BasicContext entityContext, PLIncomeReport entity)
        {
            return entityContext.Set<PLIncomeReport>().Add(entity);
        }

        protected override PLIncomeReport UpdateEntity(BasicContext entityContext, PLIncomeReport entity)
        {
            return (from e in entityContext.Set<PLIncomeReport>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<PLIncomeReport> GetEntities(BasicContext entityContext)
        {
            //return from e in entityContext.Set<PLIncomeReport>()
            //       select e;
            var query = (from e in entityContext.Set<PLIncomeReport>()
                         select e).Take(5000);
            var results = query;
            return results;
        }

        protected override PLIncomeReport GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PLIncomeReport>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public List<PLIncomeReport> GetPLIncomeReportBySearch(string searchType, string searchValue, int number)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            var plIncomes = new List<PLIncomeReport>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_mpr_pl_income_report", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "SearchType",
                    Value = searchType,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "SearchValue",
                    Value = searchValue,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Number",
                    Value = number,
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var plIncome = new PLIncomeReport();

                    if (reader["id"] != DBNull.Value)
                        plIncome.Id = int.Parse(reader["id"].ToString());

                    if (reader["TransId"] != DBNull.Value)
                        plIncome.TransId = reader["TransId"].ToString();

                    if (reader["TransDate"] != DBNull.Value)
                        plIncome.TransDate = DateTime.Parse(reader["TransDate"].ToString());

                    if (reader["GLCode"] != DBNull.Value)
                        plIncome.GLCode = reader["GLCode"].ToString();

                    if (reader["Narrative"] != DBNull.Value)
                        plIncome.Narrative = reader["Narrative"].ToString();

                    if (reader["TeamCode"] != DBNull.Value)
                        plIncome.TeamCode = reader["TeamCode"].ToString();

                    if (reader["AccountOfficerCode"] != DBNull.Value)
                        plIncome.AccountOfficerCode = reader["AccountOfficerCode"].ToString();

                    if (reader["Caption"] != DBNull.Value)
                        plIncome.Caption = reader["Caption"].ToString();

                    if (reader["BranchCode"] != DBNull.Value)
                        plIncome.BranchCode = reader["BranchCode"].ToString();

                    if (reader["RelatedAccount"] != DBNull.Value)
                        plIncome.RelatedAccount = reader["RelatedAccount"].ToString();

                    if (reader["AccountTitle"] != DBNull.Value)
                        plIncome.AccountTitle = reader["AccountTitle"].ToString();

                    if (reader["CustCode"] != DBNull.Value)
                        plIncome.CustCode = reader["CustCode"].ToString();

                    if (reader["ProductCode"] != DBNull.Value)
                        plIncome.ProductCode = reader["ProductCode"].ToString();

                    if (reader["Period"] != DBNull.Value)
                        plIncome.Period = int.Parse(reader["Amount"].ToString());

                    if (reader["Year"] != DBNull.Value)
                        plIncome.Year = reader["Year"].ToString();

                    if (reader["EntryStatus"] != DBNull.Value)
                        plIncome.EntryStatus = reader["EntryStatus"].ToString();

                    if (reader["Amount"] != DBNull.Value)
                        plIncome.Amount = decimal.Parse(reader["Amount"].ToString());

                    if (reader["CompanyCode"] != DBNull.Value)
                        plIncome.CompanyCode = reader["CompanyCode"].ToString();

                    if (reader["RunDate"] != DBNull.Value)
                        plIncome.RunDate = DateTime.Parse(reader["RunDate"].ToString());

                    if (reader["martdate"] != DBNull.Value)
                        plIncome.martdate = DateTime.Parse(reader["martdate"].ToString());

                    if (reader["StaffID"] != DBNull.Value)
                        plIncome.StaffID = reader["StaffID"].ToString();


                    plIncomes.Add(plIncome);
                }

                con.Close();
            }

            return plIncomes;
        }
      
    }
}
