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
    public partial class MPRProduct : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ProductId { get; set; }

        [DataMember]
        [Required]
        public string ProductCode { get; set; }

        [DataMember]
        [Required]
        public string CaptionCode { get; set; }

        [DataMember]
        public string VolumeGL { get; set; }

        [DataMember]
        public string InterestGL { get; set; }

        [DataMember]
        public bool Budgetable { get; set; }

        [DataMember]
        public bool IsNotional { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return ProductId;
            }
        }
    }
}
