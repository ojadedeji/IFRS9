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
    [Export(typeof(IPitFormularRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PitFormularRepository : DataRepositoryBase<PitFormular>, IPitFormularRepository
    {
        protected override PitFormular AddEntity(IFRSContext entityContext, PitFormular entity)
        {
            return entityContext.Set<PitFormular>().Add(entity);
        }

        protected override PitFormular UpdateEntity(IFRSContext entityContext, PitFormular entity)
        {
            return (from e in entityContext.Set<PitFormular>()
                    where e.PitFormularId == entity.PitFormularId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<PitFormular> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<PitFormular>()
                   select e;
        }

        protected override PitFormular GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PitFormular>()
                         where e.PitFormularId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public List<PitFormular> GetPitFormular()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
            var connectionString = IFRSContext.GetDataConnection();

            var computes = new List<PitFormular>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_pitformula", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var compute = new PitFormular();

                    if (reader["ComputedPd"] != DBNull.Value)
                        compute.ComputedPd = double.Parse(reader["ComputedPd"].ToString());

                    if (reader["Rundate"] != DBNull.Value)
                        compute.Rundate = DateTime.Parse(reader["Rundate"].ToString());

                    if (reader["Sector_code"] != DBNull.Value)
                        compute.Sector_code = reader["Sector_code"].ToString();

                    if (reader["Equation"] != DBNull.Value)
                        compute.Equation = reader["Equation"].ToString();

                    if (reader["Type"] != DBNull.Value)
                        compute.Type = reader["Type"].ToString();

                    computes.Add(compute);
                }

                con.Close();
            }

            return computes;
        }
       
    }
}