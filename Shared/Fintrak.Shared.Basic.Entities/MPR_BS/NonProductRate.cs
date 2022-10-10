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

namespace Fintrak.Shared.Basic.Entities
{
    public partial class NonProductRate : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int NonProductRateId { get; set; }

        [DataMember]
        [Required]
        public string ProductCode { get; set; }

        [DataMember]
        [Required]
        public string Period { get; set; }

        [DataMember]
        [Required]
        public string Year { get; set; }

        [DataMember]
        public double Rate { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        public int EntityId
        {
            get
            {
                return NonProductRateId;
            }
        }
    }
}
