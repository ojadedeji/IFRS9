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
    public partial class IFRSRevacctRegistry : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int RevenueId { get; set; }

        [DataMember]
        [Required]
        public string Code { get; set; }

        [DataMember]
        [Required]
        public string Caption { get; set; }

        [DataMember]
        [Required]
        public int Position { get; set; }

        [DataMember]
        public string RefNote { get; set; }

        [DataMember]
        public string FinType { get; set; }

        [DataMember]
        public string FinSubType { get; set; }

        [DataMember]
        public int? ParentId { get; set; }

        [DataMember]
        public bool IsTotalLine { get; set; }

        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public int Class { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        public int EntityId
        {
            get
            {
                return RevenueId;
            }
        }
    }
}
