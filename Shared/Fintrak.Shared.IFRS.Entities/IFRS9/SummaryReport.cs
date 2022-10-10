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
    public partial class SummaryReport : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int SummaryReportId { get; set; }

        [DataMember]
        [Required]
        public string Bucket { get; set; }

        [DataMember]
        public double Individual { get; set; }

        [DataMember]
        public double IndividualLoss { get; set; }
        [DataMember]
        public double Collective { get; set; }

        [DataMember]
        public double CollectiveLoss { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return SummaryReportId;
            }
        }
    }
}
