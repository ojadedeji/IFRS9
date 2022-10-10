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
    public partial class CDQMProduct : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ProductId { get; set; }

        [DataMember]
        [Required]
        public string ProductCode { get; set; }

        [DataMember]
        [Required]
        public string ProductName { get; set; }

        [DataMember]
        public bool IsCardable { get; set; }

        [DataMember]
        public string CustomerType { get; set; }

        [DataMember]
        public int MinimumAge { get; set; }

        [DataMember]
        public int MaximumAge { get; set; }

        [DataMember]
        public string ProductSegment { get; set; }

        [DataMember]
        public string CustomerMIS { get; set; }

        public int EntityId
        {
            get
            {
                return ProductId;
            }
        }
    }
}
