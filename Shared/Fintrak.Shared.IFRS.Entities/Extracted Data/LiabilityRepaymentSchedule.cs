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
    public partial class LiabilityRepaymentSchedule : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int LiabilityRepayId { get; set; }

        [DataMember]
        public string CONTRACT_REF_NO { get; set; }

        [DataMember]
        public string CONTRACT_STATUS { get; set; }

        [DataMember]
        public string COUNTERPARTY { get; set; }

        [DataMember]
        public DateTime BOOKING_DATE { get; set; }

        [DataMember]
        public double? AMOUNT { get; set; }

        [DataMember]
        public string PRODUCT_DESCRIPTION { get; set; }
       
        [DataMember]
        public DateTime VALUE_DATE { get; set; }

        [DataMember]
        public string CURRENCY { get; set; }

        [DataMember]
        public double? EXCHANGE_RATE { get; set; }

        [DataMember]
        public double? MAIN_COMP_RATE { get; set; }

        [DataMember]
        public DateTime? LAST_INTEREST_PAYDATE { get; set; }

        [DataMember]
        public DateTime? NEXT_INTEREST_PAYDATE { get; set; }
     
         [DataMember]
        public DateTime? CONTRACT_END_DATE { get; set; }
        
        [DataMember]
        public string CF_CONTRACT_STATUS { get; set; }

        [DataMember]
        public string GL_CODE { get; set; }
        

        [DataMember]
        public double? CF_AMT { get; set; }

        [DataMember]
        public DateTime MATURITY_DATE { get; set; }

        [DataMember]
        public DateTime Rundate { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return LiabilityRepayId;
            }
        }
    }
}
