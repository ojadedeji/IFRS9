using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Core.Framework;
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
    public partial class ExpenseBasis : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ExpenseBasisId { get; set; }

        [DataMember]
        [Required]
        public string Code { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        [Required]
        public BasisItemType ItemType { get; set; }

        [DataMember]
        public BasisValueType ValueType { get; set; }

        [DataMember]
        public OpexCategory Category { get; set; }

        [DataMember]
        public string TeamDefinitionCode { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        public int EntityId
        {
            get
            {
                return ExpenseBasisId;
            }
        }

    }
}
