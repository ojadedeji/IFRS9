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

namespace Fintrak.Shared.MPR.Entities
{
    public partial class Servicese : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ServicesId { get; set; }

        [DataMember]
        [Required]
        public string ServicesCode { get; set; }

        [DataMember]
        [Required]
        public string Service { get; set; }

        [DataMember]
        [Required]
        public double Weight { get; set; }

        [DataMember]
        [Required]
        public string ServiceType { get; set; }

        [DataMember]
        [Required]
        public string ServiceCat { get; set; }
       
        public int EntityId
        {
            get
            {
                return ServicesId;
            }
        }
    }
}
