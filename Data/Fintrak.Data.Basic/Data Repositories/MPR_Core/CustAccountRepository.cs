using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;
using Fintrak.Shared.Basic.Framework;

namespace Fintrak.Data.Basic
{
    [Export(typeof(ICustAccountRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CustAccountRepository : DataRepositoryBase<CustAccount>, ICustAccountRepository
    {

        protected override CustAccount AddEntity(BasicContext entityContext, CustAccount entity)
        {
            return entityContext.Set<CustAccount>().Add(entity);
        }

        protected override CustAccount UpdateEntity(BasicContext entityContext, CustAccount entity)
        {
            return (from e in entityContext.Set<CustAccount>() 
                    where e.CustAccountId == entity.CustAccountId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CustAccount> GetEntities(BasicContext entityContext)
        {
            //return from e in entityContext.Set<CustAccount>()
            //       select e;
            var query = (from e in entityContext.Set<CustAccount>()
                         select e).Take(5000);
            var results = query;
            return results;
        }

        protected override CustAccount GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CustAccount>()
                         where e.CustAccountId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public List<CustAccount> GetCustomerAccountBySearch(string searchType, string searchValue, int number)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            var custAccts = new List<CustAccount>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_mpr_cor_cust_account", con);
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
                    var custAcct = new CustAccount();

                    if (reader["CustAccountId"] != DBNull.Value)
                        custAcct.CustAccountId = int.Parse(reader["CustAccountId"].ToString());

                    if (reader["CustNo"] != DBNull.Value)
                        custAcct.CustNo = reader["CustNo"].ToString();

                    if (reader["AccountNo"] != DBNull.Value)
                        custAcct.AccountNo = reader["AccountNo"].ToString();

                    if (reader["AccountName"] != DBNull.Value)
                        custAcct.AccountName = reader["AccountName"].ToString();

                    if (reader["Sector"] != DBNull.Value)
                        custAcct.Sector = reader["Sector"].ToString();

                    if (reader["SubSector"] != DBNull.Value)
                        custAcct.SubSector = reader["SubSector"].ToString();

                    if (reader["TeamCode"] != DBNull.Value)
                        custAcct.TeamCode = reader["TeamCode"].ToString();

                    if (reader["AccountOfficerCode"] != DBNull.Value)
                        custAcct.AccountOfficerCode = reader["AccountOfficerCode"].ToString();

                    if (reader["ProductCode"] != DBNull.Value)
                        custAcct.ProductCode = reader["ProductCode"].ToString();

                    if (reader["BranchCode"] != DBNull.Value)
                        custAcct.BranchCode = reader["BranchCode"].ToString();

                    if (reader["Currency"] != DBNull.Value)
                        custAcct.Currency = reader["Currency"].ToString();

                    if (reader["DateOpened"] != DBNull.Value)
                        custAcct.DateOpened = DateTime.Parse(reader["DateOpened"].ToString());

                    if (reader["Status"] != DBNull.Value)
                        custAcct.Status = reader["Status"].ToString();


                    custAccts.Add(custAcct);
                }

                con.Close();
            }

            return custAccts;
        }
      
    }
}
