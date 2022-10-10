using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.MPR.Framework;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.MPR.Entities
{
    public partial class ActivityBase : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ActivityBaseId { get; set; }

        [DataMember]
        [Required]
        public string ServiceCode { get; set; }

        [DataMember]
        [Required]
        public string ServiceDescription { get; set; }

        [DataMember]
        [Required]
        public string ServiceCategory { get; set; }

        [DataMember]
        public decimal Weight { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }
        public int EntityId
        {
            get
            {
                return ActivityBaseId;
            }
        }

    }
}
