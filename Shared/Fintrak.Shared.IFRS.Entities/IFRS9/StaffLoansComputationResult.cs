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
    public partial class StaffLoansComputationResult : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int StaffLoan_Id { get; set; }

        [DataMember]
        public string RefNo { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public string ProductType { get; set; }

        [DataMember]
        public DateTime ValueDate { get; set; }

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public double Rate { get; set; }

        [DataMember]
        public double PrimeLendingRate { get; set; }

        [DataMember]
        public double TenorYear { get; set; }

        [DataMember]
        public double TenorMonth { get; set; }

        [DataMember]
        public double PeriodPaid { get; set; }

        [DataMember]
        public double OutstandPeriod { get; set; }

        [DataMember]
        public double Payments { get; set; }

        [DataMember]
        public double DiscountMarketValue { get; set; }

        [DataMember]
        public double DiscountDifference { get; set; }

        [DataMember]
        public double AmountRecoMonthly { get; set; }

        [DataMember]
        public double PayRecoTotal { get; set; }

        [DataMember]
        public double StraightLineExpense { get; set; }

        [DataMember]
        public double InterestRecoIFRS { get; set; }

        [DataMember]
        public double InterestRecoNGAAP { get; set; }

        [DataMember]
        public double InterestDifferential { get; set; }

        [DataMember]
        public DateTime Rundate { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return StaffLoan_Id;
            }
        }
    }
}
