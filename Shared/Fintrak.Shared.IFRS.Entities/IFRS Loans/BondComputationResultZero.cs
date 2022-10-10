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
    public partial class BondComputationResultZero : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }

        [DataMember]
        [Required]
        public int Day { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public double OpeningBalance { get; set; }

        [DataMember]
        public double DailyCoupon { get; set; }

        [DataMember]
        public double DailyYield { get; set; }

        [DataMember]
        public double AmortizedPremiumDisc { get; set; }

        [DataMember]
        public double ClosingBalance { get; set; }

        [DataMember]
        public double IRR { get; set; }

        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public DateTime RunDate { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }
      
        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}
