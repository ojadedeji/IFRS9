using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.CDQM.Entities
{
    public partial class CDQMCustomerPersistent : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int CUSTOMER_PERSISTENT_ID { get; set; }

        [DataMember]
        public string COD_CUST_ID { get; set; }

        [DataMember]
        public string BVN { get; set; }

        [DataMember]
        public string COD_ACCT_NO_DEFAULT { get; set; }

        [DataMember]
        public string DAT_CUST_OPEN { get; set; }

        [DataMember]
        public string FLG_STAFF { get; set; }

        [DataMember]
        public string FLG_LOC_GLOB { get; set; }

        [DataMember]
        public string COD_CC_HOMEBRN { get; set; }

        [DataMember]
        public string NAM_CUST_SHRT { get; set; }

        [DataMember]
        public string NAM_CUST_FULL { get; set; }

        [DataMember]
        public string NAM_CUST_LAST { get; set; }

        [DataMember]
        public string NAM_CUST_MID { get; set; }

        [DataMember]
        public string NAM_CUST_FIRST { get; set; }

        [DataMember]
        public int TXT_PROFESS_CAT { get; set; }

        [DataMember]
        public string TXT_CUSTADR_ADD1 { get; set; }

        [DataMember]
        public string TXT_CUSTADR_ADD2 { get; set; }

        [DataMember]
        public string TXT_CUSTADR_ADD3 { get; set; }

        [DataMember]
        public string NAM_CUSTADR_CITY { get; set; }

        [DataMember]
        public string NAM_CUSTADR_STATE { get; set; }

        [DataMember]
        public string REF_CUST_PHONE { get; set; }

        [DataMember]
        public string REF_CUST_EMAIL { get; set; }

        [DataMember]
        public string DAT_BIRTH_CUST { get; set; }

        [DataMember]
        public string TXT_CUST_SEX { get; set; }

        [DataMember]
        public int COD_CUST_MARSTAT { get; set; }

        [DataMember]
        public string NAM_CUST_SPOUSE { get; set; }

        [DataMember]
        public string TXT_SPOUSE_OCCPN { get; set; }

        [DataMember]
        public int CTR_DPNDT_CHLDRN { get; set; }

        [DataMember]
        public string TXT_CUST_NATNLTY { get; set; }

        [DataMember]
        public string DAT_INCORPORATED { get; set; }

        [DataMember]
        public string COD_BUSINESS_REGNO { get; set; }

        [DataMember]
        public string TXT_CUST_RESIDENCE { get; set; }

        [DataMember]
        public string DAT_LAST_MNT { get; set; }

        [DataMember]
        public string MIS_CODE { get; set; }

        [DataMember]
        public string RM_CODE { get; set; }

        [DataMember]
        public string RM_NAME { get; set; }

        [DataMember]
        public string PRODUCT_CODE { get; set; }

        [DataMember]
        public string PRODUCT_NAME { get; set; }

        [DataMember]
        public int IS_CARDABLE { get; set; }

        [DataMember]
        public string BUSINESS_DIVISION { get; set; }

        [DataMember]
        public string CUSTOMER_SEGMENT { get; set; }

        [DataMember]
        public string CUSTOMER_SUB_SEGMENT { get; set; }

        [DataMember]
        public string PRODUCT_SEGMENT { get; set; }

        [DataMember]
        public int IS_CARDHOLDER { get; set; }

        [DataMember]
        public int HAS_INTERNETBANKING { get; set; }

        [DataMember]
        public int HAS_MOBILEBANKING { get; set; }

        [DataMember]
        public string CREDIT_RATING { get; set; }

        [DataMember]
        public string CBN_SECTOR { get; set; }

        [DataMember]
        public string CBN_SUB_SECTOR { get; set; }

        [DataMember]
        public int AGE { get; set; }

        [DataMember]
        public decimal AMOUNT { get; set; }

        [DataMember]
        public int TRANS_COUNT { get; set; }

        [DataMember]
        public int ACCOUNTCOUNT { get; set; }

        [DataMember]
        public int IS_MERCHANTABLE { get; set; }


        [DataMember]
        public int IS_MERCHANT { get; set; }

        [DataMember]
        public string CUSTOMER_TYPE { get; set; }

        [DataMember]
        public int MAXIMUM_AGE { get; set; }

        [DataMember]
        public int MINIMUM_AGE { get; set; }

        [DataMember]
        public int PRODUCTSTATUS { get; set; }

        [DataMember]
        public string CUST_FULL_NAME_TITLE { get; set; }

        [DataMember]
        public string cust_birthdate_status { get; set; }

        [DataMember]
        public string PRODUCT_STATUS { get; set; }

        [DataMember]
        public string CUST_FULL_ADDRESS { get; set; }

        [DataMember]
        public string DERIVED_STATE { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string LGA { get; set; }

        [DataMember]
        public string NAM_CUSTADR_CNTRY { get; set; }

        [DataMember]
        public string country_status { get; set; }

        [DataMember]
        public string email_status { get; set; }

        [DataMember]
        public string gender_status { get; set; }


        [DataMember]
        public string TXT_CUST_PREFIX { get; set; }

        [DataMember]
        public string prefix_status { get; set; }

        [DataMember]
        public string DERIVED_GENDER { get; set; }

        [DataMember]
        public string COD_GROUP_ID { get; set; }

        [DataMember]
        public int _key_in { get; set; }

        [DataMember]
        public int _key_out { get; set; }

        [DataMember]
        public double _score { get; set; }

        [DataMember]
        public string NAM_CUST_FULL_clean { get; set; }

        [DataMember]
        public string DAT_BIRTH_CUST_clean { get; set; }

        [DataMember]
        public string TXT_CUST_SEX_clean { get; set; }

        [DataMember]
        public double _Similarity_NAM_CUST_FULL { get; set; }

        [DataMember]
        public string CustomerMisCode { get; set; }

        [DataMember]
        public DateTime LoadDate { get; set; }

        [DataMember]
        public bool IsGolden { get; set; }
        public int EntityId
        {
            get
            {
                return CUSTOMER_PERSISTENT_ID;
            }
        }
    }
}
