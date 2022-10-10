using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.SqlClient;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IBondComputationResultZeroRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BondComputationResultZeroRepository : DataRepositoryBase<BondComputationResultZero>, IBondComputationResultZeroRepository
    {

        protected override BondComputationResultZero AddEntity(IFRSContext entityContext, BondComputationResultZero entity)
        {
            return entityContext.Set<BondComputationResultZero>().Add(entity);
        }

        protected override BondComputationResultZero UpdateEntity(IFRSContext entityContext, BondComputationResultZero entity)
        {
            return (from e in entityContext.Set<BondComputationResultZero>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BondComputationResultZero> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<BondComputationResultZero>()
                   select e;
        }

        protected override BondComputationResultZero GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BondComputationResultZero>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public  IEnumerable<string> GetDistinctBondComputationResultZeroRefNos()
        {
            IFRSContext entityContext = new IFRSContext();

            var query = (entityContext.BondComputationResultZeroSet.Select<BondComputationResultZero, string>(r => r.RefNo)).Distinct();

            return query.ToFullyLoaded();
        }

        public IEnumerable<BondComputationResultZero> GetBondComputationResultZeroRefNos(string bondComputationResultZeroRefNo)
        {
            IFRSContext entityContext = new IFRSContext();

            var query = entityContext.BondComputationResultZeroSet.AsQueryable().Where(r => r.RefNo == bondComputationResultZeroRefNo);
                
            return query.ToFullyLoaded();
        }


        public List<BondComputationResultZero> GetDistinctRefNo()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            var connectionString = IFRSContext.GetDataConnection();

            var BondComputationResultZeros = new List<BondComputationResultZero>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("Get_Distinct_ifrs_bond_computation_result_zero", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var BondComputationResultZero = new BondComputationResultZero();

                    if (reader["RefNo"] != DBNull.Value)
                        BondComputationResultZero.RefNo = reader["RefNo"].ToString();

                    BondComputationResultZeros.Add(BondComputationResultZero);
                }

                con.Close();
            }

            return BondComputationResultZeros;
        }

    }
}

