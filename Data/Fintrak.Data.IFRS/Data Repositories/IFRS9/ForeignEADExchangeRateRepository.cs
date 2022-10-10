using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using System.Data.SqlClient;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IForeignEADExchangeRateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ForeignEADExchangeRateRepository : DataRepositoryBase<ForeignEADExchangeRate>, IForeignEADExchangeRateRepository
    {
        protected override ForeignEADExchangeRate AddEntity(IFRSContext entityContext, ForeignEADExchangeRate entity)
        {
            return entityContext.Set<ForeignEADExchangeRate>().Add(entity);
        }

        protected override ForeignEADExchangeRate UpdateEntity(IFRSContext entityContext, ForeignEADExchangeRate entity)
        {
            return (from e in entityContext.Set<ForeignEADExchangeRate>()
                    where e.ForeignEADExchangeRateId == entity.ForeignEADExchangeRateId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ForeignEADExchangeRate> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<ForeignEADExchangeRate>()
                   select e;
        }

        protected override ForeignEADExchangeRate GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ForeignEADExchangeRate>()
                         where e.ForeignEADExchangeRateId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        //public List<ForeignEADExchangeRate> GetForeignEADExchangeRate()
        //{
        //    //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
        //    var connectionString = IFRSContext.GetDataConnection();

        //    var computes = new List<ForeignEADExchangeRate>();
        //    using (var con = new SqlConnection(connectionString))
        //    {
        //        var cmd = new SqlCommand("spp_ifrs_pitformula", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 0;


        //        con.Open();

        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            var compute = new ForeignEADExchangeRate();

        //            if (reader["ComputedPd"] != DBNull.Value)
        //                compute.ComputedPd = double.Parse(reader["ComputedPd"].ToString());

        //            if (reader["Rundate"] != DBNull.Value)
        //                compute.Rundate = DateTime.Parse(reader["Rundate"].ToString());

        //            if (reader["Sector_code"] != DBNull.Value)
        //                compute.Sector_code = reader["Sector_code"].ToString();

        //            if (reader["Equation"] != DBNull.Value)
        //                compute.Equation = reader["Equation"].ToString();

        //            if (reader["Type"] != DBNull.Value)
        //                compute.Type = reader["Type"].ToString();

        //            computes.Add(compute);
        //        }

        //        con.Close();
        //    }

        //    return computes;
        //}
       
    }
}