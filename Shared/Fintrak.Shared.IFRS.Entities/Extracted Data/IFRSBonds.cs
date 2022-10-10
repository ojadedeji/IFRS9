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
    public partial class IFRSBonds : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int BondId { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public string PortfolioID { get; set; }

        [DataMember]
        [Required]
        public Nullable<DateTime> ValueDate { get; set; }

        [DataMember]
        [Required]
        public DateTime MaturityDate { get; set; }
        
        [DataMember]
        [Required]
        public Nullable<DateTime> FirstCouponDate { get; set; }
        
        [DataMember]
        public Nullable<double> FaceValue { get; set; }

        [DataMember]
        public double CleanPrice { get; set; }
           
              
        [DataMember]
        public decimal CouponRate { get; set; }

         [DataMember]
        public decimal CurrentMarketYield { get; set; }
        
        [DataMember]
        public string Classification { get; set; }
        
        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public string SandP_Rating { get; set; }
        
        [DataMember]
        public double Price { get; set; }

        [DataMember]
        public Nullable<bool> Split { get; set; }

        [DataMember]
        public string Classification_Category { get; set; }
        

         [DataMember]
        public string Narration { get; set; }
        
        [DataMember]
        public bool Active { get; set; }


        [DataMember]
        public string Symbol { get; set; }

        [DataMember]
        public Nullable<int> Stage { get; set; }

        [DataMember]
        public string CollateralType { get; set; }

        [DataMember]
        public Nullable<double> CollateralValue { get; set; }


        //ACCESS START

        [DataMember]
        public Nullable<double> redemption { get; set; }

        [DataMember]
        public string Coupon_payment_freg { get; set; }

        [DataMember]
        public string Prin_Repayment_Freg { get; set; }

        [DataMember]
        public Nullable<double> Current_Carrying_amount_GAAP { get; set; }

        [DataMember]
        public Nullable<double> Current_Carrying_amount_IFRS { get; set; }

        [DataMember]
        public Nullable<double> EIR { get; set; }

        [DataMember]
        public Nullable<double> IAS39_Impairment { get; set; }

        [DataMember]
        public int? Princ_Rep_Frq_int { get; set; }

        [DataMember]
        public int? Interest_rep_frg_int { get; set; }

        [DataMember]
        public string CounterParty { get; set; }

        [DataMember]
        public string Previous_rating { get; set; }

        [DataMember]
        public Nullable<DateTime> purchase_date { get; set; }

        [DataMember]
        public string cust_id { get; set; }

        //ACCESS END

        public int EntityId
        {
            get
            {
                return BondId;
            }
        }
    }
}
