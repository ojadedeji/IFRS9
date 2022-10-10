using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fintrak.Shared.Common.Data
{
    public static class SqlDataManager
    {
        public static void RunProcedure(string actionText, SqlParameter[] parameters)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand(actionText,con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddRange(parameters);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public static void RunProcedure(string connectionString,string actionText, SqlParameter[] parameters)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand(actionText, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddRange(parameters);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public static void RunProcedure(string connectionString, string actionText)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand(actionText, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public static string RunProcedureWithMessage(string actionText, SqlParameter[] parameters)
        {
            var result = string.Empty ;

            var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand(actionText, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddRange(parameters);

                SqlDataReader reader = null;

                con.Open();

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader[0] != DBNull.Value)
                            result = reader[0].ToString();
                    }
                }

                con.Close();
            }

            return result;
        }

        public static string RunProcedureWithMessage(string connectionString,string actionText, SqlParameter[] parameters)
        {
            var result = string.Empty;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand(actionText, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddRange(parameters);

                SqlDataReader reader = null;

                con.Open();

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader[0] != DBNull.Value)
                            result = reader[0].ToString();
                    }
                }

                con.Close();
            }

            return result;
        }
    }
}
