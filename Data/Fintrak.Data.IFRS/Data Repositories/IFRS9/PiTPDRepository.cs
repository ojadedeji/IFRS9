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
    [Export(typeof(IPiTPDRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PiTPDRepository : DataRepositoryBase<PiTPD>, IPiTPDRepository
    {
        protected override PiTPD AddEntity(IFRSContext entityContext, PiTPD entity)
        {
            return entityContext.Set<PiTPD>().Add(entity);
        }

        protected override PiTPD UpdateEntity(IFRSContext entityContext, PiTPD entity)
        {
            return (from e in entityContext.Set<PiTPD>()
                    where e.PiTPDId == entity.PiTPDId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<PiTPD> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<PiTPD>()
                   select e;
        }

        protected override PiTPD GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PiTPD>()
                         where e.PiTPDId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        //public List<PiTPD> GetPiTPD()
        //{
        //    //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
        //    var connectionString = IFRSContext.GetDataConnection();

        //    var computes = new List<PiTPD>();
        //    using (var con = new SqlConnection(connectionString))
        //    {
        //        var cmd = new SqlCommand("spp_ifrs_pitformula", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 0;


        //        con.Open();

        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            var compute = new PiTPD();

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