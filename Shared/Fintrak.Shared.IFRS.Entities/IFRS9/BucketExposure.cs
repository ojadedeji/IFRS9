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
    public partial class BucketExposure : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int BucketExposureId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public double Exposure { get; set; }
        [DataMember]
        public double TotalECL { get; set; }

        [DataMember]
        public double RecCount { get; set; }


        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return BucketExposureId;
            }
        }
    }
}
