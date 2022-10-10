using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.SqlClient;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IBalanceSheetBudgetRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BalanceSheetBudgetRepository : DataRepositoryBase<BalanceSheetBudget>, IBalanceSheetBudgetRepository
    {

        protected override BalanceSheetBudget AddEntity(MPRContext entityContext, BalanceSheetBudget entity)
        {
            return entityContext.Set<BalanceSheetBudget>().Add(entity);
        }

        protected override BalanceSheetBudget UpdateEntity(MPRContext entityContext, BalanceSheetBudget entity)
        {
            return (from e in entityContext.Set<BalanceSheetBudget>()
                    where e.BudgetId == entity.BudgetId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BalanceSheetBudget> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<BalanceSheetBudget>()
                   select e;
        }

        protected override BalanceSheetBudget GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BalanceSheetBudget>()
                         where e.BudgetId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<BalanceSheetBudget> GetBalanceSheetBudgets(string year)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.BalanceSheetBudgetSet
                            where a.Year == year
                            select a;

                return query.ToFullyLoaded();
            }
        }


        public List<BalanceSheetBudget> GetBalanceSheetBySearch(string searchValue)
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
            var connectionString = MPRContext.GetDataConnection();

            var balSheets = new List<BalanceSheetBudget>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_mpr_balancesheetbudget", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "SearchValue",
                    Value = searchValue,
                });             

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var balSheet = new BalanceSheetBudget();

                    if (reader["BalancesheetBudgetId"] != DBNull.Value)
                        balSheet.BudgetId = int.Parse(reader["BalancesheetBudgetId"].ToString());

                    if (reader["CompanyCode"] != DBNull.Value)
                        balSheet.CompanyCode = reader["CompanyCode"].ToString();

                    if (reader["TeamCode"] != DBNull.Value)
                        balSheet.MisCode = reader["TeamCode"].ToString();

                    if (reader["Year"] != DBNull.Value)
                        balSheet.Year = reader["Year"].ToString();

                    if (reader["CaptionName"] != DBNull.Value)
                        balSheet.CaptionName = reader["CaptionName"].ToString();

                    if (reader["Mth1"] != DBNull.Value)
                        balSheet.Mth1 = decimal.Parse(reader["Mth1"].ToString());

                    if (reader["Mth2"] != DBNull.Value)
                        balSheet.Mth2 = decimal.Parse(reader["Mth2"].ToString());

                    if (reader["Mth3"] != DBNull.Value)
                        balSheet.Mth3 = decimal.Parse(reader["Mth3"].ToString());

                    if (reader["Mth4"] != DBNull.Value)
                        balSheet.Mth4 = decimal.Parse(reader["Mth4"].ToString());

                    if (reader["Mth5"] != DBNull.Value)
                        balSheet.Mth5 = decimal.Parse(reader["Mth5"].ToString());

                    if (reader["Mth6"] != DBNull.Value)
                        balSheet.Mth6 = decimal.Parse(reader["Mth6"].ToString());

                    if (reader["Mth7"] != DBNull.Value)
                        balSheet.Mth7 = decimal.Parse(reader["Mth7"].ToString());

                    if (reader["Mth8"] != DBNull.Value)
                        balSheet.Mth8 = decimal.Parse(reader["Mth8"].ToString());

                    if (reader["Mth9"] != DBNull.Value)
                        balSheet.Mth9 = decimal.Parse(reader["Mth9"].ToString());

                    if (reader["Mth10"] != DBNull.Value)
                        balSheet.Mth10 = decimal.Parse(reader["Mth10"].ToString());

                    if (reader["Mth11"] != DBNull.Value)
                        balSheet.Mth11 = decimal.Parse(reader["Mth11"].ToString());

                    if (reader["Mth12"] != DBNull.Value)
                        balSheet.Mth12 = decimal.Parse(reader["Mth12"].ToString());

                    balSheets.Add(balSheet);
                }

                con.Close();
            }

            return balSheets;
        }
      
    }
}
