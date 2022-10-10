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
    [Export(typeof(IRevenueRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RevenueRepository : DataRepositoryBase<Revenue>, IRevenueRepository
    {

        protected override Revenue AddEntity(MPRContext entityContext, Revenue entity)
        {
            return entityContext.Set<Revenue>().Add(entity);
        }

        protected override Revenue UpdateEntity(MPRContext entityContext, Revenue entity)
        {
            return (from e in entityContext.Set<Revenue>()
                    where e.RevenueId == entity.RevenueId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Revenue> GetEntities(MPRContext entityContext)
        {
        //    return from e in entityContext.Set<Revenue>()
        //           select e;
            var query = (from e in entityContext.Set<Revenue>()
                         select e).Take(5000);
            var results = query;
            return results;
        }

        protected override Revenue GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Revenue>()
                         where e.RevenueId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<Revenue> GetRevenues(DateTime runDate, int number)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.RevenueSet
                            where a.RunDate == runDate.Date
                            select a;

                if (number == 0)
                    return query.ToFullyLoaded();

                return query.ToFullyLoaded().Take(number);
            }
        }


        public IEnumerable<Revenue> GetRunDate()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
            var connectionString = MPRContext.GetDataConnection();

            var revenues = new List<Revenue>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_mpr_getDistinctrevenueRundate", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
               

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var revenue = new Revenue();                  

                    if (reader["RunDate"] != DBNull.Value)
                        revenue.RunDate = DateTime.Parse(reader["RunDate"].ToString());

                    revenues.Add(revenue);
                }

                con.Close();
            }

            return revenues;
        }

        public List<Revenue> GetRevenueBySearch(string searchType, string searchValue, int number)
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
            var connectionString = MPRContext.GetDataConnection();

            var revenues = new List<Revenue>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_mpr_revenue", con);
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
                    var revenue = new Revenue();

                    if (reader["RevenueId"] != DBNull.Value)
                        revenue.RevenueId = int.Parse(reader["RevenueId"].ToString());

                    if (reader["TransId"] != DBNull.Value)
                        revenue.TransId = reader["TransId"].ToString();

                    if (reader["TransDate"] != DBNull.Value)
                        revenue.TransDate = DateTime.Parse(reader["TransDate"].ToString());

                    if (reader["GLCode"] != DBNull.Value)
                        revenue.GLCode = reader["GLCode"].ToString();

                    if (reader["Narrative"] != DBNull.Value)
                        revenue.Narrative = reader["Narrative"].ToString();

                    if (reader["TeamCode"] != DBNull.Value)
                        revenue.TeamCode = reader["TeamCode"].ToString();

                    if (reader["AccountOfficerCode"] != DBNull.Value)
                        revenue.AccountOfficerCode = reader["AccountOfficerCode"].ToString();

                    if (reader["Caption"] != DBNull.Value)
                        revenue.Caption = reader["Caption"].ToString();

                    if (reader["BranchCode"] != DBNull.Value)
                        revenue.BranchCode = reader["BranchCode"].ToString();

                    if (reader["RelatedAccount"] != DBNull.Value)
                        revenue.RelatedAccount = reader["RelatedAccount"].ToString();

                    if (reader["Amount_LCY"] != DBNull.Value)
                        revenue.Amount_LCY = decimal.Parse(reader["Amount_LCY"].ToString());

                    if (reader["AccountTitle"] != DBNull.Value)
                        revenue.AccountTitle = reader["AccountTitle"].ToString();

                    if (reader["Indicator"] != DBNull.Value)
                        revenue.Indicator = reader["Indicator"].ToString();

                    if (reader["CompanyCode"] != DBNull.Value)
                        revenue.CompanyCode = reader["CompanyCode"].ToString();

                    if (reader["RunDate"] != DBNull.Value)
                        revenue.RunDate = DateTime.Parse(reader["RunDate"].ToString());

                    revenues.Add(revenue);
                }

                con.Close();
            }

            return revenues;
        }


        public List<Revenue> GetAllRevenueBySearch(string searchType, string searchValue, int number, DateTime fromDate, DateTime toDate)
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
            var connectionString = MPRContext.GetDataConnection();

            var revenues = new List<Revenue>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_mpr_revenueAll", con);
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

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "FromDate",
                    Value = fromDate,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ToDate",
                    Value = toDate,
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var revenue = new Revenue();

                    if (reader["RevenueId"] != DBNull.Value)
                        revenue.RevenueId = int.Parse(reader["RevenueId"].ToString());

                    if (reader["TransId"] != DBNull.Value)
                        revenue.TransId = reader["TransId"].ToString();

                    if (reader["TransDate"] != DBNull.Value)
                        revenue.TransDate = DateTime.Parse(reader["TransDate"].ToString());

                    if (reader["GLCode"] != DBNull.Value)
                        revenue.GLCode = reader["GLCode"].ToString();

                    if (reader["Narrative"] != DBNull.Value)
                        revenue.Narrative = reader["Narrative"].ToString();

                    if (reader["TeamCode"] != DBNull.Value)
                        revenue.TeamCode = reader["TeamCode"].ToString();

                    if (reader["AccountOfficerCode"] != DBNull.Value)
                        revenue.AccountOfficerCode = reader["AccountOfficerCode"].ToString();

                    if (reader["Caption"] != DBNull.Value)
                        revenue.Caption = reader["Caption"].ToString();

                    if (reader["BranchCode"] != DBNull.Value)
                        revenue.BranchCode = reader["BranchCode"].ToString();

                    if (reader["RelatedAccount"] != DBNull.Value)
                        revenue.RelatedAccount = reader["RelatedAccount"].ToString();

                    if (reader["Amount_LCY"] != DBNull.Value)
                        revenue.Amount_LCY = decimal.Parse(reader["Amount_LCY"].ToString());

                    if (reader["AccountTitle"] != DBNull.Value)
                        revenue.AccountTitle = reader["AccountTitle"].ToString();

                    if (reader["Indicator"] != DBNull.Value)
                        revenue.Indicator = reader["Indicator"].ToString();
                
                    if (reader["CompanyCode"] != DBNull.Value)
                        revenue.CompanyCode = reader["CompanyCode"].ToString();

                    if (reader["RunDate"] != DBNull.Value)
                        revenue.RunDate = DateTime.Parse(reader["RunDate"].ToString());

                    revenues.Add(revenue);
                }

                con.Close();
            }

            return revenues;
        }
      
    }
}
