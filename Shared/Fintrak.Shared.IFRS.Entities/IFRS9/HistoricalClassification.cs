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
    public partial class HistoricalClassification : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int HistoricalClassificationId { get; set; }

        [DataMember]
        [Required]
        public string CustomerNo { get; set; }

        [DataMember]
        [Required]
        public string CustomerName { get; set; }

        [DataMember]
        [Required]
        public string SectorIndustry { get; set; }

        [DataMember]
        [Required]
        public string Classification { get; set; }

        [DataMember]
        public string SubClassification { get; set; }

        [DataMember]
        public string Collateral_Type { get; set; }

        [DataMember]
        [Required]
        public double OutstandingBal { get; set; }

        [DataMember]
        public double RecoverableAmt { get; set; }

        [DataMember]
        [Required]
        public int Period { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return HistoricalClassificationId;
            }
        }
    }
}
