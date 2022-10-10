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
    public partial class Placement : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int Placement_Id { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string Rating { get; set; }

        [DataMember]
        public Nullable<DateTime> BookingDate { get; set; }

        [DataMember]
        public Nullable<DateTime> ValueDate { get; set; }

        [DataMember]
        public DateTime MaturityDate { get; set; }

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public double Rate { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public double ExchangeRate { get; set; }

        [DataMember]
        public double LCY_Amount { get; set; }

        [DataMember]
        public string CollateralType { get; set; }

        [DataMember]
        public Nullable<double> CollateralValue { get; set; }

        [DataMember]
        public Nullable<double> CollateralHaircut { get; set; }

        [DataMember]
        public Nullable<DateTime> RunDate { get; set; }

        [DataMember]
        public int? Stage { get; set; }

        //ACCESS START

        [DataMember]
        public string asset_classification { get; set; }

        [DataMember]
        public string repayment_term { get; set; }

        [DataMember]
        public string previous_rating { get; set; }

        [DataMember]
        public string asset_type { get; set; }

        [DataMember]
        public string asset_desc { get; set; }

        [DataMember]
        public string rating_agency { get; set; }

        [DataMember]
        public int? days_pass_due { get; set; }

        [DataMember]
        public string prudential_classification { get; set; }

        [DataMember]
        public string forebearance_flag { get; set; }

        //ACCESS END

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return Placement_Id;
            }
        }
    }
}
