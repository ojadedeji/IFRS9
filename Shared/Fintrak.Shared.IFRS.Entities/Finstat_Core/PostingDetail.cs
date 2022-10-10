using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.IFRS.Framework;
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
    public partial class PostingDetail : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int PostingDetailId { get; set; }

        [DataMember]
        [Required]
        public string GLCode { get; set; }

        [DataMember]
        [Required]
        public string GLDescription { get; set; }

        [DataMember]
        public string TransDescription { get; set; }

        [DataMember]
        [Required]
        public string Indicator { get; set; }

        [DataMember]
        [Required]
        public decimal GAAPAmount { get; set; }

        [DataMember]
        [Required]
        public decimal ComputedAmount { get; set; }

        [DataMember]
        [Required]
        public decimal IFRSAmount { get; set; }

        [DataMember]
        [Required]
        public string CompanyCode { get; set; }

        [DataMember]
        [Required]
        public string TransactionId { get; set; }

        [DataMember]
        [Required]
        public DateTime RunDate { get; set; }

        [DataMember]
        public ReportType ReportType { get; set; }

        public int EntityId
        {
            get
            {
                return PostingDetailId;
            }
        }
    }
}
