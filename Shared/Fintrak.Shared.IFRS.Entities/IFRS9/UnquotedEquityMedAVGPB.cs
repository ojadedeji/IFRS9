using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Fintrak.Shared.IFRS.Entities
{
    public partial class UnquotedEquityMedAVGPB : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public string Caption { get; set; }

        [DataMember]
        public string Class { get; set; }

        [DataMember]
        public double MedianPB { get; set; }

        [DataMember]
        public double AveragePB { get; set; }

        [DataMember]
        public string ReportType { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
