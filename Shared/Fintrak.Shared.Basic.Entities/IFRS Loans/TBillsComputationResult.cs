using Fintrak.Shared.Basic.Framework;
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

namespace Fintrak.Shared.Basic.Entities
{
    public partial class TBillsComputationResult : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }

        [DataMember]
        [Required]
        public string Description { get; set; }

        [DataMember]
        public string DealTypeId { get; set; }

        [DataMember]
        public DateTime ValueDate { get; set; }

        [DataMember]
        public DateTime MaturityDate { get; set; }

        [DataMember]
        public int TotalTenor { get; set; }

        [DataMember]
        public int UsedDays { get; set; }

        [DataMember]
        public int DaysToMaturity { get; set; }

        [DataMember]
        public decimal Rate { get; set; }

        [DataMember]
        public double FaceValue { get; set; }

        [DataMember]
        public double CleanPrice { get; set; }

        [DataMember]
        public decimal CurrentMarketYield { get; set; }

        [DataMember]
        public double ComputedMarketPrice { get; set; }

        [DataMember]
        public double IntrestReceivable { get; set; }

        [DataMember]
        public double AmortizedCost { get; set; }

        [DataMember]
        public double FairValueGain { get; set; }

        [DataMember]
        public string Classification { get; set; }


        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public DateTime RunDate { get; set; }

        [DataMember]
        public int? FairValueBasis { get; set; }

        [DataMember]
        public string Currency { get; set; }

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
