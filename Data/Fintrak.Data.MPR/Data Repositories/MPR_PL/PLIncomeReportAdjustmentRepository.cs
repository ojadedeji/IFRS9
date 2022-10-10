using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;
using System.Configuration;
using System.Data.SqlClient;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IPLIncomeReportAdjustmentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PLIncomeReportAdjustmentRepository : DataRepositoryBase<PLIncomeReportAdjustment>, IPLIncomeReportAdjustmentRepository
    {

        protected override PLIncomeReportAdjustment AddEntity(MPRContext entityContext, PLIncomeReportAdjustment entity)
        {
            return entityContext.Set<PLIncomeReportAdjustment>().Add(entity);
        }

        protected override PLIncomeReportAdjustment UpdateEntity(MPRContext entityContext, PLIncomeReportAdjustment entity)
        {
            return (from e in entityContext.Set<PLIncomeReportAdjustment>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<PLIncomeReportAdjustment> GetEntities(MPRContext entityContext)
        {
            //return from e in entityContext.Set<PLIncomeReportAdjustment>()
            //       select e;
            var query = (from e in entityContext.Set<PLIncomeReportAdjustment>()
                         select e).Take(5000);
            var results = query;
            return results;
        }

        protected override PLIncomeReportAdjustment GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PLIncomeReportAdjustment>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public List<PLIncomeReportAdjustment> GetPLIncomeReportAdjustmentBySearch(string searchType, string searchValue, int number)
        {
            var connectionString = MPRContext.GetDataConnection();

            var plIncomeAdjs = new List<PLIncomeReportAdjustment>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_mpr_pl_income_report_adjustment", con);
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
                    var plIncomeAdj = new PLIncomeReportAdjustment();

                    if (reader["id"] != DBNull.Value)
                        plIncomeAdj.Id = int.Parse(reader["id"].ToString());

                    if (reader["GLCode"] != DBNull.Value)
                        plIncomeAdj.GLCode = reader["GLCode"].ToString();

                    if (reader["Narrative"] != DBNull.Value)
                        plIncomeAdj.Narrative = reader["Narrative"].ToString();

                    if (reader["TeamCode"] != DBNull.Value)
                        plIncomeAdj.TeamCode = reader["TeamCode"].ToString();

                    if (reader["AccountOfficerCode"] != DBNull.Value)
                        plIncomeAdj.AccountOfficerCode = reader["AccountOfficerCode"].ToString();

                    if (reader["Caption"] != DBNull.Value)
                        plIncomeAdj.Caption = reader["Caption"].ToString();

                    if (reader["BranchCode"] != DBNull.Value)
                        plIncomeAdj.BranchCode = reader["BranchCode"].ToString();

                    if (reader["RelatedAccount"] != DBNull.Value)
                        plIncomeAdj.RelatedAccount = reader["RelatedAccount"].ToString();

                    if (reader["Amount"] != DBNull.Value)
                        plIncomeAdj.Amount = decimal.Parse(reader["Amount"].ToString());                 

                    if (reader["CompanyCode"] != DBNull.Value)
                        plIncomeAdj.CompanyCode = reader["CompanyCode"].ToString();

                    if (reader["RunDate"] != DBNull.Value)
                        plIncomeAdj.RunDate = DateTime.Parse(reader["RunDate"].ToString());

                    plIncomeAdjs.Add(plIncomeAdj);
                }

                con.Close();
            }

            return plIncomeAdjs;
        }

        public List<PLIncomeReportAdjustment> GetCodebyUser(string username)
        {
            var connectionString = MPRContext.GetDataConnection();

            var mprbalancesheetadjustments = new List<PLIncomeReportAdjustment>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_getplcurrentuser", con);
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
                    var mprbalancesheetadjustment = new PLIncomeReportAdjustment();

                    if (reader["Code"] != DBNull.Value)
                        mprbalancesheetadjustment.Code = reader["Code"].ToString();

                    mprbalancesheetadjustments.Add(mprbalancesheetadjustment);
                }

                con.Close();
            }

            return mprbalancesheetadjustments;
        }

        public List<PLIncomeReportAdjustment> GetBalanceSheetAdjustmentByCode(string code, string userName)
        {
            var connectionString = MPRContext.GetDataConnection();

            var plIncomeAdjs = new List<PLIncomeReportAdjustment>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_getplIncomeReportAdjustmentbyCode", con);
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
                    var plIncomeAdj = new PLIncomeReportAdjustment();

                    if (reader["id"] != DBNull.Value)
                        plIncomeAdj.Id = int.Parse(reader["id"].ToString());

                    if (reader["GLCode"] != DBNull.Value)
                        plIncomeAdj.GLCode = reader["GLCode"].ToString();

                    if (reader["Narrative"] != DBNull.Value)
                        plIncomeAdj.Narrative = reader["Narrative"].ToString();

                    if (reader["TeamCode"] != DBNull.Value)
                        plIncomeAdj.TeamCode = reader["TeamCode"].ToString();

                    if (reader["AccountOfficerCode"] != DBNull.Value)
                        plIncomeAdj.AccountOfficerCode = reader["AccountOfficerCode"].ToString();

                    if (reader["Caption"] != DBNull.Value)
                        plIncomeAdj.Caption = reader["Caption"].ToString();

                    if (reader["BranchCode"] != DBNull.Value)
                        plIncomeAdj.BranchCode = reader["BranchCode"].ToString();

                    if (reader["RelatedAccount"] != DBNull.Value)
                        plIncomeAdj.RelatedAccount = reader["RelatedAccount"].ToString();

                    if (reader["Amount"] != DBNull.Value)
                        plIncomeAdj.Amount = decimal.Parse(reader["Amount"].ToString());

                    if (reader["CompanyCode"] != DBNull.Value)
                        plIncomeAdj.CompanyCode = reader["CompanyCode"].ToString();

                    if (reader["Code"] != DBNull.Value)
                        plIncomeAdj.Code = reader["Code"].ToString();

                    if (reader["RunDate"] != DBNull.Value)
                        plIncomeAdj.RunDate = DateTime.Parse(reader["RunDate"].ToString());

                    plIncomeAdjs.Add(plIncomeAdj);
                }

                con.Close();
            }

            return plIncomeAdjs;
        }
      
    }
}
