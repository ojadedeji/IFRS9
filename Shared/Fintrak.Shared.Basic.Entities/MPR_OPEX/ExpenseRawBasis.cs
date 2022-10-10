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
    public partial class ExpenseRawBasis : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ExpenseRawBasisId { get; set; }

        [DataMember]
        [Required]
        public string BasisCode { get; set; }

        [DataMember]
        [Required]
        public string MISCode { get; set; }

        [DataMember]
        [Required]
        public double Weight { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        public int EntityId
        {
            get
            {
                return ExpenseRawBasisId;
            }
        }

    }
}
