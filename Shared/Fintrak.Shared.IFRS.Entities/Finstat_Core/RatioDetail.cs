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
    public partial class RatioDetail : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]

        public int RatioID { get; set; }

        [DataMember]
        public string RatioCaption { get; set; }

        [DataMember]
        public string ReportCaption { get; set; }

        [DataMember]
        public string ReportSubCaption { get; set; }

        [DataMember]
        public string ReportSubSubCaption { get; set; }

        [DataMember]
        public string DivisorType { get; set; }

        [DataMember]
        public decimal Multiplier { get; set; }

        [DataMember]
        public string PreviousType { get; set; }

        [DataMember]
        public bool Annualised { get; set; }

        [DataMember]
        public int ReportType { get; set; }

        [DataMember]
        public int Position { get; set; }

        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return RatioID;
            }
        }
    }
}
