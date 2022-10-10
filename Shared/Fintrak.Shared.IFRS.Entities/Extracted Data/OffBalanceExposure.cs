using Fintrak.Shared.IFRS.Framework;
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

namespace Fintrak.Shared.IFRS.Entities
{
    public partial class OffBalanceSheetExposure : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ObeId { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }

        [DataMember]
        public DateTime TRNX_DATE { get; set; }

        [DataMember]
        public string CUSTOMER_NAME { get; set; }

        [DataMember]
        [Required]
        public DateTime MATURITY_DATE { get; set; }
 
        
        [DataMember]
        public string CUR { get; set; }

        [DataMember]
        public string CollateralType { get; set; }

        [DataMember]
        public double CollateralValue { get; set; }

        [DataMember]
        public double Amount_FCY { get; set; }   
              
        [DataMember]
        public Nullable<decimal> Ex_Rate { get; set; }

         [DataMember]
        public double Amount_NGN { get; set; }
        
        [DataMember]
         public int? TenorMonths { get; set; }

         [DataMember]
        public string Maturity_profile { get; set; }

         [DataMember]
         public string RATING { get; set; }
        

         [DataMember]
         public string Portfolio { get; set; }

         [DataMember]
         public string SUB_PORTFOLIO { get; set; }

         [DataMember]
         public int? Staging { get; set; }

         [DataMember]
         public double EIR { get; set; }

         [DataMember]
         public string Type { get; set; }
         [DataMember]

         public Nullable<bool> CanCrystallize { get; set; }

         [DataMember]       
         public Nullable<DateTime> Rundate { get; set; }

        [DataMember]
        public bool Active { get; set; }


        //ACCESS START

        [DataMember]
        //[Required]
        public string ACCOUNT_NUMBER { get; set; }

        [DataMember]
        public string SETTLEMENT_ACCOUNT { get; set; }

        [DataMember]
        public string Forbearance_flag { get; set; }

        [DataMember]
        public string CBN_Sector { get; set; }

        [DataMember]
        public string Initial_Credit_Rating { get; set; }

        [DataMember]
        public int? Days_past_due { get; set; }

        [DataMember]
        public string Classification { get; set; }

        [DataMember]
        public string Principal_payment_structure { get; set; }

        [DataMember]
        public string Interest_payment_structure { get; set; }

        [DataMember]
        public int? CUST_ID { get; set; }

        [DataMember]
        public string Secured_Unsecured { get; set; }

        [DataMember]
        public Nullable<double> Forced_sale_value { get; set; }

        [DataMember]
        public Nullable<double> Cash_OMV { get; set; }

        [DataMember]
        public Nullable<double> Cash_FSV { get; set; }

        [DataMember]
        public Nullable<double> Cost_of_recovery { get; set; }

        [DataMember]
        public string Facility_type { get; set; }

        //ACCESS END


        public int EntityId
        {
            get
            {
                return ObeId;
            }
        }
    }
}
