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
    public partial class InputDetail : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int InputDetailId { get; set; }

        //[DataMember]
        //[Required]
        //public string AccountNo { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }

        //[DataMember]
        //[Required]
        //public string ProductCategory { get; set; }

        //[DataMember]
        //public string ProductCode { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string OldRating { get; set; }

        [DataMember]
        public string Rating { get; set; }

        //[DataMember]
        //public Nullable<DateTime> BookingDate { get; set; }

        //[DataMember]
        //public DateTime ValueDate { get; set; }

        //[DataMember]
        //public DateTime MaturityDate { get; set; }

        //[DataMember]
        //public double Amount { get; set; }

        //[DataMember]
        //public double Rate { get; set; }

        //[DataMember]
        //public string Currency { get; set; }

        //[DataMember]
        //public double ExchangeRate { get; set; }

        //[DataMember]
        //public double PrincipalOutstandingBal { get; set; }

        //[DataMember]
        //public double PastDueAmount { get; set; }

        //[DataMember]
        //public DateTime InterestFirstRepaymentdate { get; set; }

        //[DataMember]
        //public Nullable<DateTime> PrincipalFirstRepaymentDate { get; set; }

        //[DataMember]
        //public int InterestRepayFreq { get; set; }

        //[DataMember]
        public int PrincipalRepayFreq { get; set; }

        [DataMember]
        public string OldCollateralType { get; set; }

        [DataMember]
        public string CollateralType { get; set; }

        [DataMember]
        public Nullable<double> OldCollateralValue { get; set; }

        [DataMember]
        public Nullable<double> CollateralValue { get; set; }

        //[DataMember]
        //public Nullable<double> ODLimit { get; set; }

        //[DataMember]
        //public string Sector { get; set; }

        //[DataMember]
        //public string SubSegment { get; set; }

        //[DataMember]
        //public Nullable<DateTime> RunDate { get; set; }

        [DataMember]
        public int? OldStage { get; set; }

        [DataMember]
        public int? Stage { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return InputDetailId;
            }
        }
    }
}
