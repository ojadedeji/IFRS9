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
    public partial class LedgerDetailSummary : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int SummaryId { get; set; }

        [DataMember]
        [Required]
        public string InstrumentTypeName { get; set; }

        [DataMember]
        [Required]
        public string Glcode { get; set; }

        [DataMember]
        public double Facevalue { get; set; }

        [DataMember]
        [Required]
        public string Currency { get; set; }

        [DataMember]
        [Required]
        public double LedgerBal { get; set; }

        [DataMember]
        [Required]
        public double Difference { get; set; }

        public int EntityId
        {
            get
            {
                return SummaryId;
            }
        }
    }
}
