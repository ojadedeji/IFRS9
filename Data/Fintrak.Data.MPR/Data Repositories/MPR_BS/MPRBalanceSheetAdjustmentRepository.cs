using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IMPRBalanceSheetAdjustmentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MPRBalanceSheetAdjustmentRepository : DataRepositoryBase<MPRBalanceSheetAdjustment>, IMPRBalanceSheetAdjustmentRepository
    {

        protected override MPRBalanceSheetAdjustment AddEntity(MPRContext entityContext, MPRBalanceSheetAdjustment entity)
        {
            return entityContext.Set<MPRBalanceSheetAdjustment>().Add(entity);
        }

        protected override MPRBalanceSheetAdjustment UpdateEntity(MPRContext entityContext, MPRBalanceSheetAdjustment entity)
        {
            return (from e in entityContext.Set<MPRBalanceSheetAdjustment>()
                    where e.BalancesheetAdjustmentId == entity.BalancesheetAdjustmentId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MPRBalanceSheetAdjustment> GetEntities(MPRContext entityContext)
        {
            //return from e in entityContext.Set<MPRBalanceSheetAdjustment>()
            //       select e;
            var query = (from e in entityContext.Set<MPRBalanceSheetAdjustment>()
                         select e).Take(5000);
            var results = query;
            return results;
        }

        protected override MPRBalanceSheetAdjustment GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MPRBalanceSheetAdjustment>()
                         where e.BalancesheetAdjustmentId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public List<MPRBalanceSheetAdjustment> GetBalanceSheetAdjustmentBySearch(string searchType, string searchValue, int number)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            var balSheetAdjs = new List<MPRBalanceSheetAdjustment>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_mpr_balancesheet_adjustment", con);
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
                    var balSheetAdj = new MPRBalanceSheetAdjustment();

                    if (reader["BalancesheetAdjustmentId"] != DBNull.Value)
                        balSheetAdj.BalancesheetAdjustmentId = int.Parse(reader["BalancesheetAdjustmentId"].ToString());

                    if (reader["AccountNo"] != DBNull.Value)
                        balSheetAdj.AccountNo = reader["AccountNo"].ToString();

                    if (reader["AccountName"] != DBNull.Value)
                        balSheetAdj.AccountName = reader["AccountName"].ToString();

                    if (reader["TeamCode"] != DBNull.Value)
                        balSheetAdj.TeamCode = reader["TeamCode"].ToString();

                    if (reader["AccountOfficerCode"] != DBNull.Value)
                        balSheetAdj.AccountOfficerCode = reader["AccountOfficerCode"].ToString();

                    if (reader["ProductCode"] != DBNull.Value)
                        balSheetAdj.ProductCode = reader["ProductCode"].ToString();

                    if (reader["Category"] != DBNull.Value)
                        balSheetAdj.Category = reader["Category"].ToString();

                    if (reader["CurrencyType"] != DBNull.Value)
                        balSheetAdj.CurrencyType = reader["CurrencyType"].ToString();

                    if (reader["ActualBal"] != DBNull.Value)
                        balSheetAdj.ActualBal = double.Parse(reader["ActualBal"].ToString());

                    if (reader["AverageBal"] != DBNull.Value)
                        balSheetAdj.AverageBal = double.Parse(reader["AverageBal"].ToString());

                    if (reader["Interest"] != DBNull.Value)
                        balSheetAdj.Interest = decimal.Parse(reader["Interest"].ToString());                 

                    if (reader["CompanyCode"] != DBNull.Value)
                        balSheetAdj.CompanyCode = reader["CompanyCode"].ToString();
                   
                    if (reader["Code"] != DBNull.Value)
                        balSheetAdj.Code = reader["Code"].ToString();

                    if (reader["RunDate"] != DBNull.Value)
                        balSheetAdj.Rundate = DateTime.Parse(reader["RunDate"].ToString());

                    balSheetAdjs.Add(balSheetAdj);
                }

                con.Close();
            }

            return balSheetAdjs;
        }

        public List<MPRBalanceSheetAdjustment> GetCodebyUsers(string username)
        {
            var connectionString = MPRContext.GetDataConnection();

            var mprbalancesheetadjustments = new List<MPRBalanceSheetAdjustment>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_getcurrentuser", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "username",
                    Value = username,
                });


                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var mprbalancesheetadjustment = new MPRBalanceSheetAdjustment();

                    if (reader["Code"] != DBNull.Value)
                        mprbalancesheetadjustment.Code = reader["Code"].ToString();

                    mprbalancesheetadjustments.Add(mprbalancesheetadjustment);
                }

                con.Close();
            }

            return mprbalancesheetadjustments;
        }


        public List<MPRBalanceSheetAdjustment> GetBalanceSheetAdjustmentByCode(string code, string userName)
        {
            var connectionString = MPRContext.GetDataConnection();

            var balSheetAdjs = new List<MPRBalanceSheetAdjustment>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_getmprbalancesheetadjustmentbyCode", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "code",
                    Value = code,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "username",
                    Value = userName,
                });


                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var balSheetAdj = new MPRBalanceSheetAdjustment();

                    if (reader["BalancesheetAdjustmentId"] != DBNull.Value)
                        balSheetAdj.BalancesheetAdjustmentId = int.Parse(reader["BalancesheetAdjustmentId"].ToString());

                    if (reader["AccountNo"] != DBNull.Value)
                        balSheetAdj.AccountNo = reader["AccountNo"].ToString();

                    if (reader["AccountName"] != DBNull.Value)
                        balSheetAdj.AccountName = reader["AccountName"].ToString();

                    if (reader["TeamCode"] != DBNull.Value)
                        balSheetAdj.TeamCode = reader["TeamCode"].ToString();

                    if (reader["AccountOfficerCode"] != DBNull.Value)
                        balSheetAdj.AccountOfficerCode = reader["AccountOfficerCode"].ToString();

                    if (reader["ProductCode"] != DBNull.Value)
                        balSheetAdj.ProductCode = reader["ProductCode"].ToString();

                    if (reader["Category"] != DBNull.Value)
                        balSheetAdj.Category = reader["Category"].ToString();

                    if (reader["CurrencyType"] != DBNull.Value)
                        balSheetAdj.CurrencyType = reader["CurrencyType"].ToString();

                    if (reader["ActualBal"] != DBNull.Value)
                        balSheetAdj.ActualBal = double.Parse(reader["ActualBal"].ToString());

                    if (reader["AverageBal"] != DBNull.Value)
                        balSheetAdj.AverageBal = double.Parse(reader["AverageBal"].ToString());

                    if (reader["Interest"] != DBNull.Value)
                        balSheetAdj.Interest = decimal.Parse(reader["Interest"].ToString());

                    if (reader["CompanyCode"] != DBNull.Value)
                        balSheetAdj.CompanyCode = reader["CompanyCode"].ToString();

                    if (reader["Code"] != DBNull.Value)
                        balSheetAdj.Code = reader["Code"].ToString();

                    if (reader["RunDate"] != DBNull.Value)
                        balSheetAdj.Rundate = DateTime.Parse(reader["RunDate"].ToString());

                    balSheetAdjs.Add(balSheetAdj);
                }

                con.Close();
            }

            return balSheetAdjs;
        }
      
    }
}
