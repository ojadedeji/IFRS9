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

namespace Fintrak.Shared.CDQM.Entities
{
    public partial class CDQMCustomerMIS : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int CustomerMISId { get; set; }

        [DataMember]
        [Required]
        public string TargetMarketCode { get; set; }

        [DataMember]
        [Required]
        public string TargetMarketName { get; set; }

        [DataMember]
        public string SegmentCode { get; set; }


        [DataMember]
        public string SegmentName { get; set; }

        [DataMember]
        public string GroupCode { get; set; }

        [DataMember]
        public string GroupName { get; set; }

        [DataMember]
        public string DivisionCode { get; set; }

        [DataMember]
        public string DivisionName { get; set; }

        public int EntityId
        {
            get
            {
                return CustomerMISId;
            }
        }
    }
}
