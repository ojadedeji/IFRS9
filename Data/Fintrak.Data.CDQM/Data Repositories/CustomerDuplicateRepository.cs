using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Fintrak.Shared.CDQM.Entities;
using Fintrak.Data.CDQM.Contracts;
using Fintrak.Shared.Common.Data;
using Fintrak.Shared.Common.Extensions;

namespace Fintrak.Data.CDQM
{
    [Export(typeof(ICDQMCustomerDuplicateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CDQMCustomerDuplicateRepository : DataRepositoryBase<CDQMCustomerDuplicate>, ICDQMCustomerDuplicateRepository
    {

        protected override CDQMCustomerDuplicate AddEntity(CDQMContext entityContext, CDQMCustomerDuplicate entity)
        {
            return entityContext.Set<CDQMCustomerDuplicate>().Add(entity);
        }

        protected override CDQMCustomerDuplicate UpdateEntity(CDQMContext entityContext, CDQMCustomerDuplicate entity)
        {
            return (from e in entityContext.Set<CDQMCustomerDuplicate>() 
                    where e.CUST_DUPLICATES_ID == entity.CUST_DUPLICATES_ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CDQMCustomerDuplicate> GetEntities(CDQMContext entityContext)
        {
            return from e in entityContext.Set<CDQMCustomerDuplicate>()
                   select e;
        }

        protected override CDQMCustomerDuplicate GetEntity(CDQMContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CDQMCustomerDuplicate>()
                         where e.CUST_DUPLICATES_ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<string> GetCustomerGroupIDs()
        {
            using (CDQMContext entityContext = new CDQMContext())
            {
                return entityContext.CDQMCustomerDuplicateSet.Select(c=>c.COD_GROUP_ID).ToFullyLoaded();
            }
        }

        public CDQMCustomerDuplicate GetCustomerDuplicate(string customerId)
        {
            using (CDQMContext entityContext = new CDQMContext())
            {
                return entityContext.CDQMCustomerDuplicateSet.Where(c => c.COD_CUST_ID == customerId).FirstOrDefault();
            }
        }

        public IEnumerable<CDQMCustomerDuplicate> GetCustomerDuplicates(string groupId)
        {
            using (CDQMContext entityContext = new CDQMContext())
            {
                return entityContext.CDQMCustomerDuplicateSet.Where(c => c.COD_GROUP_ID == groupId).ToFullyLoaded();
            }

        }
        public List<string> GetDistinctGroupIDs()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["FintrakCDQMDBConnection"].ConnectionString;

            var ids = new List<string>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("sp_cdqm_getdistinctgroupids", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader[0] != DBNull.Value)
                        ids.Add(reader[0].ToString());
                }

                con.Close();
            }

            return ids;
        }

    }
}
