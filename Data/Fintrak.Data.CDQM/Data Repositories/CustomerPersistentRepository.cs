using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Fintrak.Shared.CDQM.Entities;
using Fintrak.Data.CDQM.Contracts;
using Fintrak.Shared.Common.Extensions;

namespace Fintrak.Data.CDQM
{
    [Export(typeof(ICDQMCustomerPersistentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CDQMCustomerPersistentRepository : DataRepositoryBase<CDQMCustomerPersistent>, ICDQMCustomerPersistentRepository
    {

        protected override CDQMCustomerPersistent AddEntity(CDQMContext entityContext, CDQMCustomerPersistent entity)
        {
            return entityContext.Set<CDQMCustomerPersistent>().Add(entity);
        }

        protected override CDQMCustomerPersistent UpdateEntity(CDQMContext entityContext, CDQMCustomerPersistent entity)
        {
            return (from e in entityContext.Set<CDQMCustomerPersistent>() 
                    where e.CUSTOMER_PERSISTENT_ID == entity.CUSTOMER_PERSISTENT_ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CDQMCustomerPersistent> GetEntities(CDQMContext entityContext)
        {
            return from e in entityContext.Set<CDQMCustomerPersistent>()
                   select e;
        }

        protected override CDQMCustomerPersistent GetEntity(CDQMContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CDQMCustomerPersistent>()
                         where e.CUSTOMER_PERSISTENT_ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<CDQMCustomerPersistent> GetCustomerPersistents(string groupId)
        {
            using (CDQMContext entityContext = new CDQMContext())
            {
                var query = from a in entityContext.CDQMCustomerPersistentSet
                            where a.COD_GROUP_ID == groupId
                            select a;

                return query.ToFullyLoaded();
            }
        }

        public List<CDQMCustomerPersistent> GetCustomerPersistentByGroupId(string groupId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["FintrakCDQMDBConnection"].ConnectionString;

            var customers = new List<CDQMCustomerPersistent>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("sp_cdqm_getcustomerbygroupid", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "GroupID",
                    Value = groupId
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var customer = new CDQMCustomerPersistent();

                    if (reader["CUSTOMER_PERSISTENT_ID"] != DBNull.Value)
                        customer.CUSTOMER_PERSISTENT_ID = int.Parse(reader["CUSTOMER_PERSISTENT_ID"].ToString());

                    if (reader["COD_CUST_ID"] != DBNull.Value)
                        customer.COD_CUST_ID = reader["COD_CUST_ID"].ToString();

                    if (reader["BVN"] != DBNull.Value)
                        customer.BVN = reader["BVN"].ToString();

                    if (reader["COD_ACCT_NO_DEFAULT"] != DBNull.Value)
                        customer.COD_ACCT_NO_DEFAULT = reader["COD_ACCT_NO_DEFAULT"].ToString();

                    if (reader["DAT_CUST_OPEN"] != DBNull.Value)
                        customer.DAT_CUST_OPEN = reader["DAT_CUST_OPEN"].ToString();

                    if (reader["FLG_STAFF"] != DBNull.Value)
                        customer.FLG_STAFF = reader["FLG_STAFF"].ToString();

                    if (reader["FLG_LOC_GLOB"] != DBNull.Value)
                        customer.FLG_LOC_GLOB = reader["FLG_LOC_GLOB"].ToString();

                    if (reader["COD_CC_HOMEBRN"] != DBNull.Value)
                        customer.COD_CC_HOMEBRN = reader["COD_CC_HOMEBRN"].ToString();

                    if (reader["NAM_CUST_SHRT"] != DBNull.Value)
                        customer.NAM_CUST_SHRT = reader["NAM_CUST_SHRT"].ToString();

                    if (reader["NAM_CUST_FULL"] != DBNull.Value)
                        customer.NAM_CUST_FULL = reader["NAM_CUST_FULL"].ToString();

                    if (reader["NAM_CUST_LAST"] != DBNull.Value)
                        customer.NAM_CUST_LAST = reader["NAM_CUST_LAST"].ToString();

                    if (reader["NAM_CUST_MID"] != DBNull.Value)
                        customer.NAM_CUST_MID = reader["NAM_CUST_MID"].ToString();

                    if (reader["NAM_CUST_FIRST"] != DBNull.Value)
                        customer.NAM_CUST_FIRST = reader["NAM_CUST_FIRST"].ToString();

                    if (reader["TXT_PROFESS_CAT"] != DBNull.Value)
                        customer.TXT_PROFESS_CAT = int.Parse(reader["TXT_PROFESS_CAT"].ToString());

                    if (reader["TXT_CUSTADR_ADD1"] != DBNull.Value)
                        customer.TXT_CUSTADR_ADD1 = reader["TXT_CUSTADR_ADD1"].ToString();

                    if (reader["TXT_CUSTADR_ADD2"] != DBNull.Value)
                        customer.TXT_CUSTADR_ADD2 = reader["TXT_CUSTADR_ADD2"].ToString();

                    if (reader["TXT_CUSTADR_ADD3"] != DBNull.Value)
                        customer.TXT_CUSTADR_ADD3 = reader["TXT_CUSTADR_ADD3"].ToString();

                    if (reader["NAM_CUSTADR_CITY"] != DBNull.Value)
                        customer.NAM_CUSTADR_CITY = reader["NAM_CUSTADR_CITY"].ToString();

                    if (reader["NAM_CUSTADR_STATE"] != DBNull.Value)
                        customer.NAM_CUSTADR_STATE = reader["NAM_CUSTADR_STATE"].ToString();

                    if (reader["REF_CUST_PHONE"] != DBNull.Value)
                        customer.REF_CUST_PHONE = reader["REF_CUST_PHONE"].ToString();

                    if (reader["REF_CUST_EMAIL"] != DBNull.Value)
                        customer.REF_CUST_EMAIL = reader["REF_CUST_EMAIL"].ToString();

                    if (reader["DAT_BIRTH_CUST"] != DBNull.Value)
                        customer.DAT_BIRTH_CUST = reader["DAT_BIRTH_CUST"].ToString();

                    if (reader["TXT_CUST_SEX"] != DBNull.Value)
                        customer.TXT_CUST_SEX = reader["TXT_CUST_SEX"].ToString();

                    if (reader["COD_CUST_MARSTAT"] != DBNull.Value)
                        customer.COD_CUST_MARSTAT = int.Parse(reader["COD_CUST_MARSTAT"].ToString());

                    if (reader["NAM_CUST_SPOUSE"] != DBNull.Value)
                        customer.NAM_CUST_SPOUSE = reader["NAM_CUST_SPOUSE"].ToString();

                    if (reader["TXT_SPOUSE_OCCPN"] != DBNull.Value)
                        customer.TXT_SPOUSE_OCCPN = reader["TXT_SPOUSE_OCCPN"].ToString();

                    if (reader["CTR_DPNDT_CHLDRN"] != DBNull.Value)
                        customer.CTR_DPNDT_CHLDRN = int.Parse(reader["CTR_DPNDT_CHLDRN"].ToString());

                    if (reader["TXT_CUST_NATNLTY"] != DBNull.Value)
                        customer.TXT_CUST_NATNLTY = reader["TXT_CUST_NATNLTY"].ToString();

                    if (reader["DAT_INCORPORATED"] != DBNull.Value)
                        customer.DAT_INCORPORATED = reader["DAT_INCORPORATED"].ToString();

                    if (reader["COD_BUSINESS_REGNO"] != DBNull.Value)
                        customer.COD_BUSINESS_REGNO = reader["COD_BUSINESS_REGNO"].ToString();

                    if (reader["TXT_CUST_RESIDENCE"] != DBNull.Value)
                        customer.TXT_CUST_RESIDENCE = reader["TXT_CUST_RESIDENCE"].ToString();

                    if (reader["DAT_LAST_MNT"] != DBNull.Value)
                        customer.DAT_LAST_MNT = reader["DAT_LAST_MNT"].ToString();

                    if (reader["MIS_CODE"] != DBNull.Value)
                        customer.MIS_CODE = reader["MIS_CODE"].ToString();

                    if (reader["RM_CODE"] != DBNull.Value)
                        customer.RM_CODE = reader["RM_CODE"].ToString();

                    if (reader["RM_NAME"] != DBNull.Value)
                        customer.RM_NAME = reader["RM_NAME"].ToString();

                    if (reader["PRODUCT_CODE"] != DBNull.Value)
                        customer.PRODUCT_CODE = reader["PRODUCT_CODE"].ToString();

                    if (reader["PRODUCT_NAME"] != DBNull.Value)
                        customer.PRODUCT_NAME = reader["PRODUCT_NAME"].ToString();

                    if (reader["IS_CARDABLE"] != DBNull.Value)
                        customer.IS_CARDABLE = int.Parse(reader["IS_CARDABLE"].ToString());

                    if (reader["BUSINESS_DIVISION"] != DBNull.Value)
                        customer.BUSINESS_DIVISION = reader["BUSINESS_DIVISION"].ToString();

                    if (reader["CUSTOMER_SEGMENT"] != DBNull.Value)
                        customer.CUSTOMER_SEGMENT = reader["CUSTOMER_SEGMENT"].ToString();

                    if (reader["CUSTOMER_SUB_SEGMENT"] != DBNull.Value)
                        customer.CUSTOMER_SUB_SEGMENT = reader["CUSTOMER_SUB_SEGMENT"].ToString();

                    if (reader["PRODUCT_SEGMENT"] != DBNull.Value)
                        customer.PRODUCT_SEGMENT = reader["PRODUCT_SEGMENT"].ToString();

                    if (reader["IS_CARDHOLDER"] != DBNull.Value)
                        customer.IS_CARDHOLDER = int.Parse(reader["IS_CARDHOLDER"].ToString());

                    if (reader["HAS_INTERNETBANKING"] != DBNull.Value)
                        customer.HAS_INTERNETBANKING = int.Parse(reader["HAS_INTERNETBANKING"].ToString());

                    if (reader["HAS_MOBILEBANKING"] != DBNull.Value)
                        customer.HAS_MOBILEBANKING = int.Parse(reader["HAS_MOBILEBANKING"].ToString());

                    if (reader["CREDIT_RATING"] != DBNull.Value)
                        customer.CREDIT_RATING = reader["CREDIT_RATING"].ToString();

                    if (reader["CBN_SECTOR"] != DBNull.Value)
                        customer.CBN_SECTOR = reader["CBN_SECTOR"].ToString();

                    if (reader["CBN_SUB_SECTOR"] != DBNull.Value)
                        customer.CBN_SUB_SECTOR = reader["CBN_SUB_SECTOR"].ToString();

                    if (reader["AGE"] != DBNull.Value)
                        customer.AGE = int.Parse(reader["AGE"].ToString());

                    if (reader["AMOUNT"] != DBNull.Value)
                        customer.AMOUNT = decimal.Parse(reader["AMOUNT"].ToString());

                    if (reader["TRANS_COUNT"] != DBNull.Value)
                        customer.TRANS_COUNT = int.Parse(reader["TRANS_COUNT"].ToString());

                    if (reader["ACCOUNTCOUNT"] != DBNull.Value)
                        customer.ACCOUNTCOUNT = int.Parse(reader["ACCOUNTCOUNT"].ToString());

                    if (reader["IS_MERCHANTABLE"] != DBNull.Value)
                        customer.IS_MERCHANTABLE = int.Parse(reader["IS_MERCHANTABLE"].ToString());

                    if (reader["IS_MERCHANT"] != DBNull.Value)
                        customer.IS_MERCHANT = int.Parse(reader["IS_MERCHANT"].ToString());

                    if (reader["CUSTOMER_TYPE"] != DBNull.Value)
                        customer.CUSTOMER_TYPE = reader["CUSTOMER_TYPE"].ToString();

                    if (reader["MAXIMUM_AGE"] != DBNull.Value)
                        customer.MAXIMUM_AGE = int.Parse(reader["MAXIMUM_AGE"].ToString());

                    if (reader["MINIMUM_AGE"] != DBNull.Value)
                        customer.MINIMUM_AGE = int.Parse(reader["MINIMUM_AGE"].ToString());

                    if (reader["PRODUCTSTATUS"] != DBNull.Value)
                        customer.PRODUCTSTATUS = int.Parse(reader["PRODUCTSTATUS"].ToString());

                    if (reader["CUST_FULL_NAME_TITLE"] != DBNull.Value)
                        customer.CUST_FULL_NAME_TITLE = reader["CUST_FULL_NAME_TITLE"].ToString();

                    if (reader["cust_birthdate_status"] != DBNull.Value)
                        customer.cust_birthdate_status = reader["cust_birthdate_status"].ToString();

                    if (reader["PRODUCT_STATUS"] != DBNull.Value)
                        customer.PRODUCT_STATUS = reader["PRODUCT_STATUS"].ToString();

                    if (reader["CUST_FULL_ADDRESS"] != DBNull.Value)
                        customer.CUST_FULL_ADDRESS = reader["CUST_FULL_ADDRESS"].ToString();

                    if (reader["DERIVED_STATE"] != DBNull.Value)
                        customer.DERIVED_STATE = reader["DERIVED_STATE"].ToString();

                    if (reader["PostalCode"] != DBNull.Value)
                        customer.PostalCode = reader["PostalCode"].ToString();

                    if (reader["LGA"] != DBNull.Value)
                        customer.LGA = reader["LGA"].ToString();

                    if (reader["NAM_CUSTADR_CNTRY"] != DBNull.Value)
                        customer.NAM_CUSTADR_CNTRY = reader["NAM_CUSTADR_CNTRY"].ToString();

                    if (reader["country_status"] != DBNull.Value)
                        customer.country_status = reader["country_status"].ToString();

                    if (reader["email_status"] != DBNull.Value)
                        customer.email_status = reader["email_status"].ToString();

                    if (reader["gender_status"] != DBNull.Value)
                        customer.gender_status = reader["gender_status"].ToString();

                    if (reader["TXT_CUST_PREFIX"] != DBNull.Value)
                        customer.TXT_CUST_PREFIX = reader["TXT_CUST_PREFIX"].ToString();

                    if (reader["prefix_status"] != DBNull.Value)
                        customer.prefix_status = reader["prefix_status"].ToString();

                    if (reader["DERIVED_GENDER"] != DBNull.Value)
                        customer.DERIVED_GENDER = reader["DERIVED_GENDER"].ToString();

                    if (reader["COD_GROUP_ID"] != DBNull.Value)
                        customer.COD_GROUP_ID = reader["COD_GROUP_ID"].ToString();

                    if (reader["_key_in"] != DBNull.Value)
                        customer._key_in = int.Parse(reader["_key_in"].ToString());

                    if (reader["_key_out"] != DBNull.Value)
                        customer._key_out = int.Parse(reader["_key_out"].ToString());

                    if (reader["_score"] != DBNull.Value)
                        customer._score = double.Parse(reader["_score"].ToString());

                    if (reader["NAM_CUST_FULL_clean"] != DBNull.Value)
                        customer.NAM_CUST_FULL_clean = reader["NAM_CUST_FULL_clean"].ToString();

                    if (reader["DAT_BIRTH_CUST_clean"] != DBNull.Value)
                        customer.DAT_BIRTH_CUST_clean = reader["DAT_BIRTH_CUST_clean"].ToString();

                    if (reader["TXT_CUST_SEX_clean"] != DBNull.Value)
                        customer.TXT_CUST_SEX_clean = reader["TXT_CUST_SEX_clean"].ToString();

                    if (reader["_Similarity_NAM_CUST_FULL"] != DBNull.Value)
                        customer._Similarity_NAM_CUST_FULL = double.Parse(reader["_Similarity_NAM_CUST_FULL"].ToString());

                    if (reader["CustomerMisCode"] != DBNull.Value)
                        customer.CustomerMisCode = reader["CustomerMisCode"].ToString();

                    if (reader["LoadDate"] != DBNull.Value)
                        customer.LoadDate = DateTime.Parse(reader["LoadDate"].ToString());

                    if (reader["IsGolden"] != DBNull.Value)
                        customer.IsGolden = bool.Parse(reader["IsGolden"].ToString());

                    customers.Add(customer);
                }

                con.Close();
            }

            return customers;
        }
      
    }
}
