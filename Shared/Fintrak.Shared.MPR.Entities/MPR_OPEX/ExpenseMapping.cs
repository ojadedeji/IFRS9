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
    public partial class ExpenseMapping : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ExpenseMappingId { get; set; }

        [DataMember]
        [Required]
        public string BasisCode { get; set; }

        [DataMember]
        public string ItemCode { get; set; }

        [DataMember]
        [Required]
        public string ParentMISCode { get; set; }

        [DataMember]
        [Required]
        public string MISCode { get; set; }

        [DataMember]
        public double Weight { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }
      
        public int EntityId
        {
            get
            {
                return ExpenseMappingId;
            }
        }

    }
}
