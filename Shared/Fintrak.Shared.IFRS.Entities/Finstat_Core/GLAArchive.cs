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
    public partial class GLAArchive : EntityBase, IIdentifiableEntity{

        [DataMember]
        [Browsable(false)]
        public int GLAdjustmentId { get; set; }

        [DataMember]
        public string AdjustmentCode { get; set; }

        [DataMember]
        public string GLCode { get; set; }

        [DataMember]
        public string Narration { get; set; }

        [DataMember]
        public int Indicator { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public string BranchCode { get; set; }  

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public int ReportType { get; set; }

        [DataMember]
        public DateTime RunDate { get; set; }

        [DataMember]
        public int AdjustmentType { get; set; }

        [DataMember]
        public bool Posted { get; set; }

        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return GLAdjustmentId;
            }
        }
    }
}
